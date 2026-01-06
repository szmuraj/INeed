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
            scores[AppConstants.Texts.Labels.AchievementShort] = (0, 0);
            scores[AppConstants.Texts.Labels.AffiliationShort] = (0, 0);
            scores[AppConstants.Texts.Labels.AutonomyShort] = (0, 0);
            scores[AppConstants.Texts.Labels.DominanceShort] = (0, 0);

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

            var ach = scores[AppConstants.Texts.Labels.AchievementShort];
            var aff = scores[AppConstants.Texts.Labels.AffiliationShort];
            var aut = scores[AppConstants.Texts.Labels.AutonomyShort];
            var dom = scores[AppConstants.Texts.Labels.DominanceShort];

            // Obliczanie STEN
            int achStenF = StenHelper.GetSten(StenCategory.Achievement, ach.actual, false);
            int achStenM = StenHelper.GetSten(StenCategory.Achievement, ach.actual, true);

            int affStenF = StenHelper.GetSten(StenCategory.Affiliation, aff.actual, false);
            int affStenM = StenHelper.GetSten(StenCategory.Affiliation, aff.actual, true);

            int autStenF = StenHelper.GetSten(StenCategory.Autonomy, aut.actual, false);
            int autStenM = StenHelper.GetSten(StenCategory.Autonomy, aut.actual, true);

            int domStenF = StenHelper.GetSten(StenCategory.Dominance, dom.actual, false);
            int domStenM = StenHelper.GetSten(StenCategory.Dominance, dom.actual, true);

            ViewBag.AchVal = ach.actual; ViewBag.AchMax = ach.max; ViewBag.AchStenF = achStenF; ViewBag.AchStenM = achStenM;
            ViewBag.AffVal = aff.actual; ViewBag.AffMax = aff.max; ViewBag.AffStenF = affStenF; ViewBag.AffStenM = affStenM;
            ViewBag.AutVal = aut.actual; ViewBag.AutMax = aut.max; ViewBag.AutStenF = autStenF; ViewBag.AutStenM = autStenM;
            ViewBag.DomVal = dom.actual; ViewBag.DomMax = dom.max; ViewBag.DomStenF = domStenF; ViewBag.DomStenM = domStenM;

            return View(AppConstants.Texts.Messages.Result);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SendResult(
            string email,
            int achVal, int achMax, int achStenF, int achStenM,
            int affVal, int affMax, int affStenF, int affStenM,
            int autVal, int autMax, int autStenF, int autStenM,
            int domVal, int domMax, int domStenF, int domStenM,
            string formTitle)
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

                        {GenerateResultBlock(AppConstants.Texts.Labels.Achievement, achVal, achMax, achStenF, achStenM, AppConstants.Colors.Achievement)}
                        {GenerateResultBlock(AppConstants.Texts.Labels.Affiliation, affVal, affMax, affStenF, affStenM, AppConstants.Colors.Affiliation)}
                        {GenerateResultBlock(AppConstants.Texts.Labels.Autonomy, autVal, autMax, autStenF, autStenM, AppConstants.Colors.Autonomy)}
                        {GenerateResultBlock(AppConstants.Texts.Labels.Dominance, domVal, domMax, domStenF, domStenM, AppConstants.Colors.Dominance)}

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

            ViewBag.AchVal = achVal; ViewBag.AchMax = achMax; ViewBag.AchStenF = achStenF; ViewBag.AchStenM = achStenM;
            ViewBag.AffVal = affVal; ViewBag.AffMax = affMax; ViewBag.AffStenF = affStenF; ViewBag.AffStenM = affStenM;
            ViewBag.AutVal = autVal; ViewBag.AutMax = autMax; ViewBag.AutStenF = autStenF; ViewBag.AutStenM = autStenM;
            ViewBag.DomVal = domVal; ViewBag.DomMax = domMax; ViewBag.DomStenF = domStenF; ViewBag.DomStenM = domStenM;

            return View(AppConstants.Texts.Messages.Result);
        }

        private string GenerateResultBlock(string label, int actual, int max, int stenF, int stenM, string color)
        {
            double percent = max > 0 ? (double)actual / max * 100 : 0;
            string descF = StenHelper.GetDescription(stenF);
            string descM = StenHelper.GetDescription(stenM);

            return $@"
                <div style='margin-bottom: 25px;'>
                    <div style='display: flex; justify-content: space-between; align-items: baseline;'>
                        <strong style='font-size: 1.1em;'>{label}</strong>
                        <span style='font-weight: bold;'>{actual}/{max}</span>
                    </div>
                    <div style='background-color: #f0f0f0; height: 10px; border-radius: 5px; width: 100%; margin: 5px 0 10px 0;'>
                        <div style='width: {percent.ToString("0.##", System.Globalization.CultureInfo.InvariantCulture)}%; background-color: {color}; height: 100%; border-radius: 5px;'></div>
                    </div>
                    <div style='font-size: 0.9em; color: #555; background-color: #f9f9f9; padding: 10px; border-radius: 5px; border: 1px solid #eee;'>
                        <div style='margin-bottom: 4px;'><strong>Kobieta:</strong> STEN {stenF} ({descF})</div>
                        <div><strong>Mężczyzna:</strong> STEN {stenM} ({descM})</div>
                    </div>
                </div>";
        }
    }
}