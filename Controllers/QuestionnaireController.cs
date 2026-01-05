using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using INeed.Data;
using INeed.Models;
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

        // POPRAWKA: Łączenie stałych za pomocą '+' tworzy wyrażenie stałe, które jest dozwolone w atrybutach.
        // Usunięto klamry {} wokół stałej, bo to nie jest parametr trasy, tylko część ścieżki.
        [HttpGet(AppConstants.Texts.FillRoute + "/{id}")]
        public async Task<IActionResult> Fill(Guid id)
        {
            if (id == Guid.Empty) return NotFound();

            var form = await _context.Forms
                .Include(f => f.Questions)
                .ThenInclude(q => q.Answers)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (form == null) return NotFound();
            if (!form.IsActive) return Content(AppConstants.Texts.Fill.InactiveForm);

            return View(form);
        }

        // POPRAWKA: Analogicznie dla metody POST
        [HttpPost(AppConstants.Texts.FillRoute + "/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Fill(Guid id, IFormCollection collection)
        {
            var questionnaire = await _context.Forms
                .Include(f => f.Questions)
                .ThenInclude(q => q.Answers)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (questionnaire == null) return NotFound();

            var scores = new Dictionary<string, (int actual, int max)>();
            foreach (var q in questionnaire.Questions)
            {
                string cat = string.IsNullOrEmpty(q.Category) ? AppConstants.Texts.Layout.Another : q.Category.Trim().ToUpper();
                if (!scores.ContainsKey(cat)) scores[cat] = (0, 0);

                int maxQ = q.Answers.Any() ? q.Answers.Max(a => a.Score) : 0;
                int actualQ = 0;
                if (collection.TryGetValue($"Question_{q.QuestionId}", out var val) && Guid.TryParse(val, out Guid aId))
                {
                    actualQ = q.Answers.FirstOrDefault(a => a.AnswerId == aId)?.Score ?? 0;
                }

                var current = scores[cat];
                scores[cat] = (current.actual + actualQ, current.max + maxQ);
            }

            ViewBag.FormTitle = questionnaire.Title;
            ViewBag.PercentAchievement = CalculatePercent(scores, AppConstants.Texts.Labels.AchievementShort);
            ViewBag.PercentAffiliation = CalculatePercent(scores, AppConstants.Texts.Labels.AffiliationShort);
            ViewBag.PercentAutonomy = CalculatePercent(scores, AppConstants.Texts.Labels.AutonomyShort);
            ViewBag.PercentDominance = CalculatePercent(scores, AppConstants.Texts.Labels.DominanceShort);

            // Zwracamy widok z wynikami (korzystając ze stałej "Result")
            return View(AppConstants.Texts.Messages.Result);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SendResult(
            string email, double percentAchievement, double percentAffiliation, double percentAutonomy, double percentDominance, string formTitle)
        {
            if (string.IsNullOrEmpty(email)) return RedirectToAction(AppConstants.Texts.Messages.Index, AppConstants.Texts.Messages.Home);

            var existingSub = await _context.Subs.FirstOrDefaultAsync(s => s.Email == email);
            if (existingSub == null)
            {
                _context.Subs.Add(new Sub { Email = email, IsActive = true, Newsletter = true, AddedAt = DateTime.Now });
            }
            else
            {
                existingSub.IsActive = true;
                existingSub.Newsletter = true;
            }
            await _context.SaveChangesAsync();

            try
            {
                string emailBody = $@"
                    <div style='font-family: Arial, sans-serif; max-width: 600px; margin: 0 auto; padding: 20px; border: 1px solid #e0e0e0; border-radius: 10px;'>
                        <h2 style='color: {AppConstants.Colors.Primary}; text-align: center;'>{AppConstants.Texts.Messages.YourResults} {formTitle}</h2>
                        <p>{AppConstants.Texts.Messages.EmailThanks}</p>
                        
                        <hr style='border: 0; border-top: 1px solid #eee; margin: 20px 0;'>

                        {GenerateProgressBar(AppConstants.Texts.Labels.Achievement, percentAchievement, AppConstants.Colors.Achievement)}
                        {GenerateProgressBar(AppConstants.Texts.Labels.Affiliation, percentAffiliation, AppConstants.Colors.Affiliation)}
                        {GenerateProgressBar(AppConstants.Texts.Labels.Autonomy, percentAutonomy, AppConstants.Colors.Autonomy)}
                        {GenerateProgressBar(AppConstants.Texts.Labels.Dominance, percentDominance, AppConstants.Colors.Dominance)}

                        <hr style='border: 0; border-top: 1px solid #eee; margin: 20px 0;'>
                        <p style='text-align: center; font-size: 12px; color: #888;'>{AppConstants.Texts.Messages.GeneratedBy}</p>
                    </div>";

                await _emailService.SendEmailAsync(email, $"{AppConstants.Texts.Messages.YourResults} {formTitle}", emailBody);
                TempData[AppConstants.Texts.Messages.SuccessMessage] = AppConstants.Texts.Messages.EmailSentSuccess;
            }
            catch
            {
                TempData[AppConstants.Texts.Messages.ErrorMessage] = AppConstants.Texts.Messages.EmailSentError;
            }

            ViewBag.FormTitle = formTitle;
            ViewBag.PercentAchievement = percentAchievement;
            ViewBag.PercentAffiliation = percentAffiliation;
            ViewBag.PercentAutonomy = percentAutonomy;
            ViewBag.PercentDominance = percentDominance;

            return View(AppConstants.Texts.Messages.Result);
        }

        private double CalculatePercent(Dictionary<string, (int actual, int max)> scores, string key)
        {
            return scores.TryGetValue(key, out var s) && s.max > 0 ? Math.Round((double)s.actual / s.max * 100) : 0;
        }

        private string GenerateProgressBar(string label, double percent, string color)
        {
            return $@"
                <div style='margin-bottom: 15px;'>
                    <strong>{label}:</strong> {percent}%
                    <div style='background-color: #f0f0f0; height: 10px; border-radius: 5px; width: 100%; margin-top: 5px;'>
                        <div style='width: {percent}%; background-color: {color}; height: 100%; border-radius: 5px;'></div>
                    </div>
                </div>";
        }
    }
}