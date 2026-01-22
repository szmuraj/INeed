using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using INeed.Data;
using INeed.Models;
using INeed.Services;
using INeed.Helpers;
using System.Diagnostics;

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

        // ZMIANA: Parametr nazywa siê teraz 'visitorId', co wymusi przekazanie go w query string (?visitorId=...)
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

        // ZMIANA: Parametr visitorId zamiast id
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

            // Przekierowanie z zachowaniem visitorId w adresie
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