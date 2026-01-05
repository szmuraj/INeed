using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using INeed.Data;
using INeed.Models;
using INeed.Services;

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

        // GET: /Questionnaire/Fill/{id}
        [HttpGet("Questionnaire/Fill/{id}")]
        public async Task<IActionResult> Fill(Guid id)
        {
            if (id == Guid.Empty) return NotFound();

            var form = await _context.Forms
                .Include(f => f.Questions)
                .ThenInclude(q => q.Answers)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (form == null) return NotFound();
            return View(form);
        }

        // POST: /Questionnaire/Fill/{id}
        [HttpPost("Questionnaire/Fill/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Fill(Guid id, IFormCollection collection)
        {
            var questionnaire = await _context.Forms
                .Include(f => f.Questions)
                .ThenInclude(q => q.Answers)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (questionnaire == null) return NotFound();

            // Obliczanie wyników
            var scores = new Dictionary<string, (int actual, int max)>();

            foreach (var q in questionnaire.Questions)
            {
                string cat = string.IsNullOrEmpty(q.Category) ? "INNE" : q.Category.Trim().ToUpper();
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

            // Przypisanie do ViewBag
            ViewBag.FormTitle = questionnaire.Title;
            ViewBag.PercentAchievement = scores.TryGetValue("ACH", out var sAch) && sAch.max > 0 ? Math.Round((double)sAch.actual / sAch.max * 100) : 0;
            ViewBag.PercentAffiliation = scores.TryGetValue("AFF", out var sAff) && sAff.max > 0 ? Math.Round((double)sAff.actual / sAff.max * 100) : 0;
            ViewBag.PercentAutonomy = scores.TryGetValue("AUT", out var sAut) && sAut.max > 0 ? Math.Round((double)sAut.actual / sAut.max * 100) : 0;
            ViewBag.PercentDominance = scores.TryGetValue("DOM", out var sDom) && sDom.max > 0 ? Math.Round((double)sDom.actual / sDom.max * 100) : 0;

            return View("Result");
        }

        // POST: Wysyłka wyników (To naprawia błąd 405 i obsługuje formularz z Result.cshtml)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SendResult(
            string email,
            double percentAchievement,
            double percentAffiliation,
            double percentAutonomy,
            double percentDominance,
            string formTitle)
        {
            if (string.IsNullOrEmpty(email)) return RedirectToAction("Index", "Home");

            // 1. Zapis do bazy
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

            // 2. Wysyłka maila
            try
            {
                string emailBody = $@"
                    <div style='font-family: Arial, sans-serif; max-width: 600px; margin: 0 auto; padding: 20px; border: 1px solid #e0e0e0; border-radius: 10px;'>
                        <h2 style='color: #1A2D41; text-align: center;'>Twoje wyniki: {formTitle}</h2>
                        <p>Dziękujemy za wypełnienie kwestionariusza. Oto Twój profil motywacyjny:</p>
                        
                        <hr style='border: 0; border-top: 1px solid #eee; margin: 20px 0;'>

                        <div style='margin-bottom: 15px;'>
                            <strong>Potrzeba Osiągnięć:</strong> {percentAchievement}%
                            <div style='background-color: #f0f0f0; height: 10px; border-radius: 5px; width: 100%; margin-top: 5px;'>
                                <div style='width: {percentAchievement}%; background-color: #76FF03; height: 100%; border-radius: 5px;'></div>
                            </div>
                        </div>

                        <div style='margin-bottom: 15px;'>
                            <strong>Potrzeba Afiliacji:</strong> {percentAffiliation}%
                            <div style='background-color: #f0f0f0; height: 10px; border-radius: 5px; width: 100%; margin-top: 5px;'>
                                <div style='width: {percentAffiliation}%; background-color: #26A69A; height: 100%; border-radius: 5px;'></div>
                            </div>
                        </div>

                        <div style='margin-bottom: 15px;'>
                            <strong>Potrzeba Autonomii:</strong> {percentAutonomy}%
                            <div style='background-color: #f0f0f0; height: 10px; border-radius: 5px; width: 100%; margin-top: 5px;'>
                                <div style='width: {percentAutonomy}%; background-color: #FFF700; height: 100%; border-radius: 5px;'></div>
                            </div>
                        </div>

                        <div style='margin-bottom: 15px;'>
                            <strong>Potrzeba Dominacji:</strong> {percentDominance}%
                            <div style='background-color: #f0f0f0; height: 10px; border-radius: 5px; width: 100%; margin-top: 5px;'>
                                <div style='width: {percentDominance}%; background-color: #D32F2F; height: 100%; border-radius: 5px;'></div>
                            </div>
                        </div>

                        <hr style='border: 0; border-top: 1px solid #eee; margin: 20px 0;'>
                        <p style='text-align: center; font-size: 12px; color: #888;'>Wiadomość wygenerowana automatycznie przez system INeed.</p>
                    </div>";

                await _emailService.SendEmailAsync(email, $"Twój wynik: {formTitle}", emailBody);
                TempData["SuccessMessage"] = "Wynik został wysłany na Twój adres e-mail.";
            }
            catch
            {
                TempData["ErrorMessage"] = "Nie udało się wysłać wiadomości e-mail.";
            }

            // Przekazanie danych z powrotem do widoku, aby wykresy nie zniknęły
            ViewBag.FormTitle = formTitle;
            ViewBag.PercentAchievement = percentAchievement;
            ViewBag.PercentAffiliation = percentAffiliation;
            ViewBag.PercentAutonomy = percentAutonomy;
            ViewBag.PercentDominance = percentDominance;

            return View("Result");
        }
    }
}