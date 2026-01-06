using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using INeed.Data;
using INeed.Models;
using INeed.Models.ViewModels;
using INeed.Services;
using INeed.Helpers;

namespace INeed.Controllers
{
    public class QuestionnaireController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IEmailService _emailService;

        public QuestionnaireController(ApplicationDbContext context, IEmailService emailService)
        {
            _context = context;
            _emailService = emailService;
        }

        [HttpGet(AppConstants.Texts.FillRoute + "/{id}")]
        public async Task<IActionResult> Fill(Guid id)
        {
            if (id == Guid.Empty) return NotFound();

            var form = await _context.Forms
                .Include(f => f.Questions)
                .ThenInclude(q => q.Answers)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (form == null || !form.IsActive) return NotFound();
            return View(form);
        }

        [HttpPost(AppConstants.Texts.FillRoute + "/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Fill(Guid id, IFormCollection collection)
        {
            var questionnaire = await _context.Forms
                .Include(f => f.Questions)
                .ThenInclude(q => q.Answers)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (questionnaire == null) return NotFound();

            // 1. Pobieramy wszystkie definicje kategorii (normy i kolory) z bazy
            var dbCategories = await _context.Categories.AsNoTracking().ToListAsync();
            var finalResult = new FinalResultVm { FormTitle = questionnaire.Title };

            // 2. Grupowanie dynamiczne pytań po nazwie kategorii przypisanej w tabeli Questions
            var questionsByCatName = questionnaire.Questions
                .GroupBy(q => q.Category?.Trim() ?? AppConstants.Texts.Layout.Another);

            foreach (var group in questionsByCatName)
            {
                string catName = group.Key;

                // Szukamy definicji w bazie danych pasującej do nazwy z pytania
                var categoryDef = dbCategories.FirstOrDefault(c =>
                    c.Name.Trim().Equals(catName, StringComparison.OrdinalIgnoreCase));

                int actualScore = 0;
                int maxScore = 0;

                foreach (var q in group)
                {
                    maxScore += q.Answers.Any() ? q.Answers.Max(a => a.Score) : 0;
                    if (collection.TryGetValue($"Question_{q.QuestionId}", out var val) && Guid.TryParse(val, out Guid aId))
                    {
                        actualScore += q.Answers.FirstOrDefault(a => a.AnswerId == aId)?.Score ?? 0;
                    }
                }

                // Obliczamy STENy tylko jeśli kategoria istnieje w tabeli Categories
                int stenF = categoryDef != null ? CalculateSten(actualScore, categoryDef.StenNormsFemale) : 0;
                int stenM = categoryDef != null ? CalculateSten(actualScore, categoryDef.StenNormsMale) : 0;

                finalResult.Categories.Add(new CategoryResultVm
                {
                    CategoryName = catName,
                    CategoryCode = categoryDef?.Code ?? "CUSTOM",
                    Color = categoryDef?.Color ?? AppConstants.Colors.TextSecondary,
                    ScoreObtained = actualScore,
                    ScoreMax = maxScore,
                    StenFemale = stenF,
                    DescFemale = GetStenDescription(stenF),
                    StenMale = stenM,
                    DescMale = GetStenDescription(stenM)
                });
            }

            // Opcjonalnie: Sortowanie kategorii alfabetycznie, aby wynik był przewidywalny
            finalResult.Categories = finalResult.Categories.OrderBy(c => c.CategoryName).ToList();

            return View("Result", finalResult);
        }

        private int CalculateSten(int score, string normsString)
        {
            if (string.IsNullOrEmpty(normsString)) return 0;
            try
            {
                var thresholds = normsString.Split(',').Select(int.Parse).ToArray();
                for (int i = 0; i < thresholds.Length; i++)
                {
                    if (score <= thresholds[i]) return i + 1;
                }
                return 10;
            }
            catch { return 0; }
        }

        private string GetStenDescription(int sten)
        {
            if (sten == 0) return "-";
            if (sten >= 1 && sten <= 4) return "Niski";
            if (sten >= 5 && sten <= 6) return "Przeciętny";
            if (sten >= 7 && sten <= 10) return "Wysoki";
            return "-";
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SendResult(FinalResultVm model, string email)
        {
            if (string.IsNullOrEmpty(email)) return RedirectToAction("Index", "Home");

            var existingSub = await _context.Subs.FirstOrDefaultAsync(s => s.Email == email);
            if (existingSub == null)
            {
                _context.Subs.Add(new Sub { Email = email, IsActive = true, Newsletter = true, AddedAt = DateTime.Now });
                await _context.SaveChangesAsync();
            }
            else if (!existingSub.IsActive)
            {
                existingSub.IsActive = true;
                await _context.SaveChangesAsync();
            }

            string rows = "";
            foreach (var cat in model.Categories)
            {
                rows += GenerateEmailRow(cat);
            }

            string emailBody = $@"
                <div style='font-family: Arial, sans-serif; max-width: 600px; margin: 0 auto; padding: 20px; border: 1px solid #e0e0e0; border-radius: 10px;'>
                    <h2 style='color: {AppConstants.Colors.Primary}; text-align: center;'>{AppConstants.Texts.Messages.Wyniki}: {model.FormTitle}</h2>
                    <p style='text-align: center;'>{AppConstants.Texts.Messages.EmailThanks}</p>
                    <hr style='border: 0; border-top: 1px solid #eee; margin: 20px 0;'>
                    {rows}
                    <hr style='border: 0; border-top: 1px solid #eee; margin: 20px 0;'>
                    <p style='text-align: center; font-size: 12px; color: #888;'>{AppConstants.Texts.Messages.GeneratedBy}</p>
                </div>";

            try
            {
                await _emailService.SendEmailAsync(email, $"{AppConstants.Texts.Messages.YourResults} {model.FormTitle}", emailBody);
                TempData[AppConstants.Texts.Messages.SuccessMessage] = AppConstants.Texts.Messages.EmailSentSuccess;
            }
            catch
            {
                TempData[AppConstants.Texts.Messages.ErrorMessage] = AppConstants.Texts.Messages.EmailSentError;
            }

            return View("Result", model);
        }

        private string GenerateEmailRow(CategoryResultVm cat)
        {
            return $@"
                <div style='margin-bottom: 25px;'>
                    <div style='display: flex; justify-content: space-between; align-items: baseline;'>
                        <strong style='font-size: 1.1em;'>{cat.CategoryName}</strong>
                        <span style='font-weight: bold;'>{cat.ScoreObtained}/{cat.ScoreMax}</span>
                    </div>
                    <div style='background-color: #f0f0f0; height: 10px; border-radius: 5px; width: 100%; margin: 5px 0 10px 0;'>
                        <div style='width: {cat.Percent.ToString("0", System.Globalization.CultureInfo.InvariantCulture)}%; background-color: {cat.Color}; height: 100%; border-radius: 5px;'></div>
                    </div>
                    <div style='font-size: 0.9em; color: #555; background-color: #f9f9f9; padding: 10px; border-radius: 5px; border: 1px solid #eee;'>
                        <div><strong>Kobieta:</strong> STEN {cat.StenFemale} ({cat.DescFemale})</div>
                        <div><strong>Mężczyzna:</strong> STEN {cat.StenMale} ({cat.DescMale})</div>
                    </div>
                </div>";
        }
    }
}