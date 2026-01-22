using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using INeed.Data;
using INeed.Models;
using INeed.Services;
using INeed.Helpers;
using System.Diagnostics;
using System.Text; // Potrzebne do StringBuilder

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

        public async Task<IActionResult> Index(string visitorId = "000000")
        {
            ViewBag.VisitorId = visitorId;

            var forms = _context.Forms != null
                ? await _context.Forms.Where(f => f.IsActive).ToListAsync()
                : new List<Form>();

            return View(forms);
        }

        public IActionResult Privacy(string visitorId = "000000")
        {
            ViewBag.VisitorId = visitorId;
            return View();
        }

        public IActionResult Terms(string visitorId = "000000")
        {
            ViewBag.VisitorId = visitorId;
            return View();
        }

        public IActionResult Contact(string visitorId = "000000")
        {
            ViewBag.VisitorId = visitorId;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SendMessage(string email, string message, bool rodoConsent, string visitorId = "000000")
        {
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

        // --- NOWA METODA: WYSY£ANIE ZBIORCZYCH WYNIKÓW ---
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SendAllResults(string email, string visitorId)
        {
            // Zabezpieczenie visitorId
            if (string.IsNullOrEmpty(visitorId)) visitorId = "000000";

            if (string.IsNullOrEmpty(email))
            {
                TempData[AppConstants.Keys.ContactError] = "Proszê podaæ adres e-mail.";
                return RedirectToAction(AppConstants.Routing.ActionContact, new { visitorId });
            }

            // Pobranie wszystkich wyników dla danego u¿ytkownika wraz z relacjami
            var results = await _context.VisitorResults
                .Include(r => r.Form)
                .Include(r => r.CategoryScores).ThenInclude(cs => cs.Category)
                .Where(r => r.VisitorId == visitorId)
                .OrderByDescending(r => r.Date) // Najnowsze na górze
                .ToListAsync();

            if (!results.Any())
            {
                TempData[AppConstants.Keys.ContactError] = "Nie znaleziono ¿adnych wyników dla Twojego identyfikatora.";
                return RedirectToAction(AppConstants.Routing.ActionContact, new { visitorId });
            }

            // Budowanie treœci maila
            var sb = new StringBuilder();
            sb.Append($"<div style='font-family: Arial, sans-serif; padding: 20px;'>");
            sb.Append($"<h2 style='color: {AppConstants.Colors.Primary};'>Twoje zbiorcze wyniki</h2>");
            sb.Append($"<p>Poni¿ej znajduj¹ siê wszystkie wyniki ankiet powi¹zane z identyfikatorem: <strong>{visitorId}</strong></p>");
            sb.Append("<hr>");

            foreach (var result in results)
            {
                sb.Append($"<div style='margin-bottom: 30px; border: 1px solid #eee; padding: 15px; border-radius: 8px;'>");
                sb.Append($"<h3 style='margin-top: 0;'>{result.Form?.Title ?? "Nieznana ankieta"}</h3>");
                sb.Append($"<p style='color: #666; font-size: 0.9em;'>Data wype³nienia: {result.Date:yyyy-MM-dd HH:mm}</p>");

                sb.Append("<table style='width: 100%; border-collapse: collapse;'>");
                foreach (var score in result.CategoryScores)
                {
                    sb.Append("<tr>");
                    sb.Append($"<td style='padding: 5px; border-bottom: 1px solid #f0f0f0;'><strong>{score.Category?.Name ?? "Kategoria"}</strong></td>");
                    sb.Append($"<td style='padding: 5px; border-bottom: 1px solid #f0f0f0; text-align: right;'>Wynik: {score.Score}</td>");
                    sb.Append("</tr>");
                }
                sb.Append("</table>");
                sb.Append("</div>");
            }
            sb.Append("</div>");

            try
            {
                await _emailService.SendEmailAsync(email, "Twoje zbiorcze wyniki - INeed", sb.ToString());
                TempData[AppConstants.Keys.ContactSuccess] = "Wszystkie wyniki zosta³y wys³ane na podany adres e-mail.";
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
            if (string.IsNullOrEmpty(visitorId)) visitorId = "000000";

            var results = await _context.VisitorResults
                .Where(r => r.VisitorId == visitorId)
                .ToListAsync();

            if (results.Any())
            {
                _context.VisitorResults.RemoveRange(results);
                await _context.SaveChangesAsync();
                TempData[AppConstants.Keys.ContactSuccess] = "Wszystkie dane powi¹zane z Twoim ID zosta³y usuniête.";
            }
            else
            {
                TempData[AppConstants.Keys.ContactError] = "Nie znaleziono danych dla tego identyfikatora.";
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