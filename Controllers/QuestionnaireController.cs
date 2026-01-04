using INeed.Data;
using INeed.Models;
using INeed.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

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

        // --- GET: Wyświetlanie formularza (Standardowy) ---
        [HttpGet("Questionnaire/Fill/{id}")]
        public async Task<IActionResult> Fill(Guid id)
        {
            if (id == Guid.Empty) return NotFound();

            var form = await _context.Forms
                .Include(f => f.Questions)
                .ThenInclude(q => q.Answers)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (form == null) return NotFound();
            if (!form.IsActive) return Content("Ten formularz jest nieaktywny.");

            return View(form);
        }

        // --- POST: Przesłanie odpowiedzi (Standardowy) ---
        [HttpPost("Questionnaire/Fill/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Fill(Guid id, IFormCollection collection)
        {
            return await ProcessFormSubmission(id, collection);
        }

        // --- GET: Wyświetlanie formularza (KOP) ---
        [HttpGet("Questionnaire/KOP/{id}")]
        public async Task<IActionResult> KOP(Guid id)
        {
            if (id == Guid.Empty) return NotFound();

            var form = await _context.Forms
                .Include(f => f.Questions)
                .ThenInclude(q => q.Answers)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (form == null) return NotFound();
            if (!form.IsActive) return Content("Ten formularz jest nieaktywny.");

            return View("Fill", form);
        }

        // --- POST: Przesłanie odpowiedzi (KOP) ---
        [HttpPost("Questionnaire/KOP/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> KOP(Guid id, IFormCollection collection)
        {
            return await ProcessFormSubmission(id, collection);
        }

        // --- POST: Wysłanie wyniku na email ---
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
            if (string.IsNullOrEmpty(email))
            {
                return RedirectToAction("Index", "Home");
            }

            // 1. Zapis do bazy (Newsletter)
            var existingSub = await _context.Subs.FirstOrDefaultAsync(s => s.Email == email);
            if (existingSub == null)
            {
                var newSub = new Sub
                {
                    Email = email,
                    IsActive = true,
                    Newsletter = true,
                    AddedAt = DateTime.Now
                };
                _context.Subs.Add(newSub);
                await _context.SaveChangesAsync();
            }

            // 2. Treść maila
            string body = $@"
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

            // 3. Wysyłka
            try
            {
                await _emailService.SendEmailAsync(email, $"Twój wynik: {formTitle}", body);
                TempData["SuccessMessage"] = "Wyniki zostały wysłane na Twój adres e-mail.";
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Błąd wysyłki: {ex.Message}";
            }

            return RedirectToAction("Index", "Home");
        }

        // --- LOGIKA PUNKTACJI ---
        private async Task<IActionResult> ProcessFormSubmission(Guid id, IFormCollection formCollection)
        {
            var questionnaire = await _context.Forms
                .Include(f => f.Questions)
                .ThenInclude(q => q.Answers)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (questionnaire == null) return NotFound();

            var groupAchievement = new[] { 1, 5, 9, 13, 17 };
            var groupAffiliation = new[] { 2, 6, 10, 14 };
            var groupAutonomy = new[] { 3, 7, 11, 15, 18 };
            var groupDominance = new[] { 4, 8, 12, 16, 19 };

            int scoreAchievement = 0, maxAchievement = 0;
            int scoreAffiliation = 0, maxAffiliation = 0;
            int scoreAutonomy = 0, maxAutonomy = 0;
            int scoreDominance = 0, maxDominance = 0;

            foreach (var question in questionnaire.Questions)
            {
                int maxQ = question.Answers.Any() ? question.Answers.Max(a => a.Score) : 0;
                int points = 0;

                string formKey = $"Question_{question.QuestionId}";
                if (formCollection.ContainsKey(formKey))
                {
                    if (Guid.TryParse(formCollection[formKey], out Guid selectedAnswerId))
                    {
                        var answer = question.Answers.FirstOrDefault(a => a.AnswerId == selectedAnswerId);
                        if (answer != null) points = answer.Score;
                    }
                }

                if (groupAchievement.Contains(question.Number)) { scoreAchievement += points; maxAchievement += maxQ; }
                else if (groupAffiliation.Contains(question.Number)) { scoreAffiliation += points; maxAffiliation += maxQ; }
                else if (groupAutonomy.Contains(question.Number)) { scoreAutonomy += points; maxAutonomy += maxQ; }
                else if (groupDominance.Contains(question.Number)) { scoreDominance += points; maxDominance += maxQ; }
            }

            ViewBag.FormTitle = questionnaire.Title;
            double CalcPercent(int score, int max) => max > 0 ? Math.Round((double)score / max * 100, 0) : 0;

            ViewBag.PercentAchievement = CalcPercent(scoreAchievement, maxAchievement);
            ViewBag.PercentAffiliation = CalcPercent(scoreAffiliation, maxAffiliation);
            ViewBag.PercentAutonomy = CalcPercent(scoreAutonomy, maxAutonomy);
            ViewBag.PercentDominance = CalcPercent(scoreDominance, maxDominance);

            return View("Result");
        }
    }
}