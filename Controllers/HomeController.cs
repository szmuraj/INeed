using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using INeed.Data;
using INeed.Models;
using INeed.Services;
using INeed.Helpers;
using System.Diagnostics;
using System.Text;

namespace INeed.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IEmailService _emailService;

        public HomeController(ApplicationDbContext context, IEmailService emailService)
        {
            _context = context;
            _emailService = emailService;
        }

        public async Task<IActionResult> Index(string visitorId)
        {
            // 1. Pobieramy lub ustawiamy domyœlne ID
            visitorId = AppConstants.GetVisitorId(visitorId);
            ViewBag.VisitorId = visitorId;

            // 2. Pobieramy ankiety z bazy (TO BY£O POMINIÊTE)
            var forms = _context.Forms != null
                ? await _context.Forms.Where(f => f.IsActive).ToListAsync()
                : new List<Form>();

            // 3. Przekazujemy je do widoku
            return View(forms);
        }

        // Metoda Info zastêpuj¹ca Privacy i Terms (bez InfoPageVm)
        public IActionResult Info(string id, string visitorId)
        {
            visitorId = AppConstants.GetVisitorId(visitorId);
            ViewBag.VisitorId = visitorId;

            var txt = AppConstants.Texts;

            switch (id?.ToLower())
            {
                case "privacy":
                    ViewData[AppConstants.Keys.Title] = txt.Layout.PrivacyPolicy;
                    ViewBag.MainTitle = txt.Layout.PrivacyPolicy;
                    ViewBag.Subtitle = txt.Layout.CookieHeader;
                    ViewBag.HtmlContent = txt.PolicyContent.PrivacyAndCookies;
                    break;

                case "terms":
                    ViewData[AppConstants.Keys.Title] = txt.Layout.Terms;
                    ViewBag.MainTitle = txt.Layout.Terms;
                    ViewBag.Subtitle = txt.CompanyName;
                    ViewBag.HtmlContent = txt.PolicyContent.Terms;
                    break;

                default:
                    return RedirectToAction(nameof(Index), new { visitorId });
            }

            return View("InfoPage");
        }

        public IActionResult Contact(string visitorId)
        {
            visitorId = AppConstants.GetVisitorId(visitorId);
            ViewBag.VisitorId = visitorId;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SendMessage(string email, string message, bool rodoConsent, string visitorId)
        {
            visitorId = AppConstants.GetVisitorId(visitorId);

            if (!rodoConsent)
            {
                TempData[AppConstants.Keys.ContactError] = AppConstants.Texts.Messages.RodoRequired;
                return RedirectToAction(AppConstants.Routing.ActionContact, new { visitorId });
            }

            if (!string.IsNullOrEmpty(email) && !string.IsNullOrEmpty(message))
            {
                try
                {
                    await _emailService.SendEmailAsync(AppConstants.Texts.Contact.Email, $"{AppConstants.Texts.Messages.NewMessage} {email}", message);
                    TempData[AppConstants.Keys.ContactSuccess] = AppConstants.Texts.Messages.ContactSuccess;
                }
                catch
                {
                    TempData[AppConstants.Keys.ContactError] = AppConstants.Texts.Messages.ContactError;
                }
            }
            else
            {
                TempData[AppConstants.Keys.ContactError] = AppConstants.Texts.Messages.FormIncomplete;
            }

            return RedirectToAction(AppConstants.Routing.ActionContact, new { visitorId });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SendAllResults(string email, string visitorId)
        {
            visitorId = AppConstants.GetVisitorId(visitorId);

            if (string.IsNullOrEmpty(email))
            {
                TempData[AppConstants.Keys.ContactError] = AppConstants.Texts.Messages.EmailRequired;
                return RedirectToAction(AppConstants.Routing.ActionContact, new { visitorId });
            }

            var results = await _context.VisitorResults
                .Include(r => r.Form)
                .Include(r => r.CategoryScores).ThenInclude(cs => cs.Category)
                .Where(r => r.VisitorId == visitorId)
                .OrderByDescending(r => r.Date)
                .ToListAsync();

            if (!results.Any())
            {
                TempData[AppConstants.Keys.ContactError] = AppConstants.Texts.Messages.NoResultsFound;
                return RedirectToAction(AppConstants.Routing.ActionContact, new { visitorId });
            }

            var txt = AppConstants.Texts;
            var sb = new StringBuilder();
            sb.Append($"<div style='font-family: Arial, sans-serif; padding: 20px;'>");
            sb.Append($"<h2 style='color: {AppConstants.Colors.Primary};'>{txt.Messages.CombinedResultsTitle}</h2>");
            sb.Append($"<p>{txt.Messages.ResultsForIdBody}: <strong>{visitorId}</strong></p>");
            sb.Append("<hr>");

            foreach (var result in results)
            {
                sb.Append($"<div style='margin-bottom: 30px; border: 1px solid #eee; padding: 15px; border-radius: 8px;'>");
                sb.Append($"<h3 style='margin-top: 0;'>{result.Form?.Title ?? txt.Messages.UnknownSurvey}</h3>");
                sb.Append($"<p style='color: #666; font-size: 0.9em;'>{txt.Messages.FillDate}: {result.Date:yyyy-MM-dd HH:mm}</p>");

                sb.Append("<table style='width: 100%; border-collapse: collapse;'>");
                foreach (var score in result.CategoryScores)
                {
                    sb.Append("<tr>");
                    sb.Append($"<td style='padding: 5px; border-bottom: 1px solid #f0f0f0;'><strong>{score.Category?.Name ?? txt.Labels.Category}</strong></td>");
                    sb.Append($"<td style='padding: 5px; border-bottom: 1px solid #f0f0f0; text-align: right;'>{txt.Labels.ResultScore}: {score.Score}</td>");
                    sb.Append("</tr>");
                }
                sb.Append("</table>");
                sb.Append("</div>");
            }
            sb.Append("</div>");

            try
            {
                await _emailService.SendEmailAsync(email, $"{txt.Messages.CombinedResultsTitle} - INeed", sb.ToString());
                TempData[AppConstants.Keys.ContactSuccess] = txt.Messages.AllResultsSentSuccess;
            }
            catch
            {
                TempData[AppConstants.Keys.ContactError] = AppConstants.Texts.Messages.EmailSentError;
            }

            return RedirectToAction(AppConstants.Routing.ActionContact, new { visitorId });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteData(string visitorId)
        {
            visitorId = AppConstants.GetVisitorId(visitorId);

            var results = await _context.VisitorResults
                .Where(r => r.VisitorId == visitorId)
                .ToListAsync();

            if (results.Any())
            {
                _context.VisitorResults.RemoveRange(results);
                await _context.SaveChangesAsync();
                TempData[AppConstants.Keys.ContactSuccess] = AppConstants.Texts.Messages.DataDeleted;
            }
            else
            {
                TempData[AppConstants.Keys.ContactError] = AppConstants.Texts.Messages.NoResultsFound;
            }

            return RedirectToAction(AppConstants.Routing.ActionContact, new { visitorId });
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}