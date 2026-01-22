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
        public async Task<IActionResult> Fill(int kw, string visitorId)
        {
            visitorId = AppConstants.GetVisitorId(visitorId);

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
            visitorId = AppConstants.GetVisitorId(visitorId);

            var questionnaire = await _context.Forms
                .Include(f => f.Questions).ThenInclude(q => q.Answers)
                .FirstOrDefaultAsync(m => m.Id == kw);

            if (questionnaire == null) return NotFound();

            string formTitle = AppConstants.SelectContent(questionnaire.Title, questionnaire.TitleEN);

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
                var categoryDef = dbCategories.FirstOrDefault(c => AppConstants.IsCategoryMatch(c, group.Key));

                // ZABEZPIECZENIE: Jeśli kategoria nie została znaleziona w bazie, pomijamy te pytania, 
                // aby nie psuć obliczeń (ewentualnie tutaj można dodać logowanie błędu)
                if (categoryDef == null) continue;

                int actualScore = 0;
                int maxScore = 0;

                foreach (var q in group)
                {
                    // Maksymalny możliwy wynik dla tego pytania
                    maxScore += q.Answers.Any() ? q.Answers.Max(a => a.Score) : 0;

                    // KLUCZOWA POPRAWKA PUNKTACJI
                    // Sprawdzamy, czy w formularzu są dane dla tego pytania
                    string key = $"Question_{q.QuestionId}";

                    if (collection.ContainsKey(key))
                    {
                        var submittedValues = collection[key]; // To może zawierać np. ["", "GUID"] lub sam "GUID"

                        // Szukamy pierwszej wartości, która jest poprawnym AnswerId
                        foreach (var value in submittedValues)
                        {
                            if (!string.IsNullOrEmpty(value) && Guid.TryParse(value, out Guid selectedAnswerId))
                            {
                                // Znaleziono ID odpowiedzi - pobieramy jej punkty
                                var answer = q.Answers.FirstOrDefault(a => a.AnswerId == selectedAnswerId);
                                if (answer != null)
                                {
                                    actualScore += answer.Score;
                                    break; // Mamy odpowiedź dla tego pytania, przerywamy szukanie duplikatów
                                }
                            }
                        }
                    }
                }

                int stenF = CalculateSten(actualScore, categoryDef.StenNormsFemale);
                int stenM = CalculateSten(actualScore, categoryDef.StenNormsMale);

                string adviceF = GetAdvice(stenF, categoryDef);
                string adviceM = GetAdvice(stenM, categoryDef);

                int userSten = 0;
                string userAdvice = "";

                if (isMale == false) { userSten = stenF; userAdvice = adviceF; }
                else if (isMale == true) { userSten = stenM; userAdvice = adviceM; }

                string displayName = AppConstants.SelectContent(categoryDef.Name, categoryDef.NameEN);

                finalResult.Categories.Add(new CategoryResultVm
                {
                    CategoryName = displayName,
                    Color = categoryDef.Color ?? AppConstants.Colors.TextSecondary,
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

        private string GetAdvice(int sten, Category cat)
        {
            if (cat == null) return string.Empty;

            string pl = "";
            string en = "";

            if (sten <= 4)
            {
                pl = cat.AdviceLow; en = cat.AdviceLowEN;
            }
            else if (sten <= 6)
            {
                pl = cat.AdviceAvg; en = cat.AdviceAvgEN;
            }
            else
            {
                pl = cat.AdviceHigh; en = cat.AdviceHighEN;
            }

            return AppConstants.SelectContent(pl, en);
        }

        private string GetStenDescription(int sten)
        {
            if (sten == 0) return "-";
            var labels = AppConstants.Texts.Labels;
            if (sten <= 4) return labels.StenLow;
            if (sten <= 6) return labels.StenAverage;
            return labels.StenHigh;
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