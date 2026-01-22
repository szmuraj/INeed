using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Globalization;
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

        [HttpGet(AppConstants.FillRoute)]
        // ZMIANA: Usunięto = "000000" z sygnatury
        public async Task<IActionResult> Fill(int kw, string visitorId)
        {
            // ZMIANA: Logika przeniesiona do środka
            if (string.IsNullOrEmpty(visitorId)) visitorId = AppConstants.Defaults.VisitorId;

            if (kw == 0) return NotFound();

            var form = await _context.Forms
                .Include(f => f.Questions).ThenInclude(q => q.Answers)
                .FirstOrDefaultAsync(m => m.Id == kw);

            if (form == null || !form.IsActive) return NotFound();

            ViewBag.VisitorId = visitorId;

            return View(form);
        }

        [HttpPost(AppConstants.FillRoute)]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Fill(int kw, string visitorId, bool? isMale, IFormCollection collection)
        {
            // ZMIANA: Użycie stałej AppConstants.Defaults.VisitorId
            if (string.IsNullOrEmpty(visitorId)) visitorId = AppConstants.Defaults.VisitorId;

            var questionnaire = await _context.Forms
                .Include(f => f.Questions).ThenInclude(q => q.Answers)
                .FirstOrDefaultAsync(m => m.Id == kw);

            if (questionnaire == null) return NotFound();

            bool isEn = CultureInfo.CurrentUICulture.Name.StartsWith("en");
            string formTitle = (isEn && !string.IsNullOrEmpty(questionnaire.TitleEN)) ? questionnaire.TitleEN : questionnaire.Title;

            var finalResult = new FinalResultVm
            {
                FormId = kw,
                FormTitle = formTitle,
                VisitorId = visitorId,
                IsMale = isMale
            };

            var dbCategories = await _context.Categories.AsNoTracking().ToListAsync();

            var resultDb = new VisitorResult
            {
                Id = Guid.NewGuid(),
                VisitorId = visitorId,
                FormId = questionnaire.Id,
                IsMale = isMale,
                Date = DateTime.UtcNow
            };

            var groupedQuestions = questionnaire.Questions.GroupBy(q => q.Category?.Trim() ?? "General");

            foreach (var group in groupedQuestions)
            {
                var categoryDef = dbCategories.FirstOrDefault(c =>
                    c.Name.Trim().Equals(group.Key, StringComparison.OrdinalIgnoreCase) ||
                    (c.NameEN != null && c.NameEN.Trim().Equals(group.Key, StringComparison.OrdinalIgnoreCase)));

                if (categoryDef == null) continue;

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

                int stenF = CalculateSten(actualScore, categoryDef.StenNormsFemale);
                int stenM = CalculateSten(actualScore, categoryDef.StenNormsMale);

                string adviceF = GetAdvice(stenF, categoryDef, isEn);
                string adviceM = GetAdvice(stenM, categoryDef, isEn);

                int userSten = 0;
                string userAdvice = "";

                if (isMale == false) { userSten = stenF; userAdvice = adviceF; }
                else if (isMale == true) { userSten = stenM; userAdvice = adviceM; }

                string displayName = (isEn && !string.IsNullOrEmpty(categoryDef.NameEN)) ? categoryDef.NameEN : categoryDef.Name;

                finalResult.Categories.Add(new CategoryResultVm
                {
                    CategoryName = displayName,
                    Color = categoryDef.Color ?? "#6c757d",
                    ScoreObtained = actualScore,
                    ScoreMax = maxScore,
                    StenUser = userSten,
                    StenFemale = stenF,
                    StenMale = stenM,
                    Advice = userAdvice,
                    AdviceFemale = adviceF,
                    AdviceMale = adviceM,
                    DescFemale = GetStenDescription(stenF),
                    DescMale = GetStenDescription(stenM)
                });

                resultDb.CategoryScores.Add(new VisitorCategoryScore
                {
                    Id = Guid.NewGuid(),
                    CategoryId = categoryDef.Id,
                    Score = actualScore
                });
            }

            _context.VisitorResults.Add(resultDb);
            await _context.SaveChangesAsync();

            return View("Result", finalResult);
        }

        private int CalculateSten(int score, string normsString)
        {
            if (string.IsNullOrEmpty(normsString)) return 0;
            try
            {
                var thresholds = normsString.Split(',').Select(int.Parse).ToArray();
                for (int i = 0; i < thresholds.Length; i++) if (score <= thresholds[i]) return i + 1;
                return 10;
            }
            catch { return 0; }
        }

        private string GetAdvice(int sten, Category cat, bool isEn)
        {
            if (cat == null) return string.Empty;
            if (sten <= 4) return isEn ? (cat.AdviceLowEN ?? "") : (cat.AdviceLow ?? "");
            if (sten <= 6) return isEn ? (cat.AdviceAvgEN ?? "") : (cat.AdviceAvg ?? "");
            return isEn ? (cat.AdviceHighEN ?? "") : (cat.AdviceHigh ?? "");
        }

        private string GetStenDescription(int sten)
        {
            if (sten == 0) return "-";
            bool isEn = CultureInfo.CurrentUICulture.Name.StartsWith("en");
            if (sten <= 4) return isEn ? "Low" : "Niski";
            if (sten <= 6) return isEn ? "Average" : "Przeciętny";
            return isEn ? "High" : "Wysoki";
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SendResult(FinalResultVm model, string email)
        {
            if (string.IsNullOrEmpty(email)) return RedirectToAction("Index", "Home");

            var txt = AppConstants.Texts;

            string rows = "";
            foreach (var cat in model.Categories)
            {
                string adviceText = model.IsMale == null
                    ? $"{txt.Labels.Woman}: {cat.AdviceFemale} <br> {txt.Labels.Man}: {cat.AdviceMale}"
                    : cat.Advice;

                rows += GenerateEmailRow(cat, adviceText, model.IsMale);
            }

            string emailBody = $@"
                <div style='font-family: Arial, sans-serif; max-width: 600px; margin: 0 auto; padding: 20px; border: 1px solid #e0e0e0; border-radius: 10px;'>
                    <h2 style='color: {AppConstants.Colors.Primary}; text-align: center;'>{txt.Messages.Wyniki}: {model.FormTitle}</h2>
                    {rows}
                </div>";

            try
            {
                await _emailService.SendEmailAsync(email, $"{txt.Messages.YourResults} {model.FormTitle}", emailBody);
                TempData[AppConstants.Keys.SuccessMessage] = txt.Messages.EmailSentSuccess;
            }
            catch
            {
                TempData[AppConstants.Keys.ErrorMessage] = txt.Messages.EmailSentError;
            }

            return View("Result", model);
        }

        private string GenerateEmailRow(CategoryResultVm cat, string advice, bool? isMale)
        {
            string stenInfo = isMale == null
                ? $"F: {cat.StenFemale} / M: {cat.StenMale}"
                : cat.StenUser.ToString();

            return $@"
                <div style='margin-bottom: 25px;'>
                    <div style='display: flex; justify-content: space-between;'>
                        <strong>{cat.CategoryName}</strong>
                        <span>{cat.ScoreObtained}/{cat.ScoreMax}</span>
                    </div>
                    <div>STEN: {stenInfo}</div>
                    <p style='font-style: italic;'>{advice}</p>
                </div>";
        }
    }
}