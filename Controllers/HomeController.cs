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

        public async Task<IActionResult> Index()
        {
            var forms = _context.Forms != null
                ? await _context.Forms.Where(f => f.IsActive).ToListAsync()
                : new List<Form>();

            return View(forms);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Terms()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SendMessage(string email, string message, bool rodoConsent)
        {
            if (!rodoConsent)
            {
                // POPRAWKA: U퓓wamy Keys dla klucza TempData
                TempData[AppConstants.Keys.ContactError] = AppConstants.Texts.Messages.RodoRequired;
                // POPRAWKA: U퓓wamy Routing dla nazwy akcji
                return RedirectToAction(AppConstants.Routing.ActionContact);
            }

            if (!string.IsNullOrEmpty(email) && !string.IsNullOrEmpty(message))
            {
                try
                {
                    // POPRAWKA: U퓓wamy tekstu z AppConstants.Texts.Contact
                    await _emailService.SendEmailAsync(AppConstants.Texts.Contact.Email, $"{AppConstants.Texts.Messages.NewMessage} {email}", message);

                    // POPRAWKA: U퓓wamy Keys dla klucza
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

            return RedirectToAction(AppConstants.Routing.ActionContact);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Unsubscribe(string email)
        {
            if (string.IsNullOrEmpty(email) || _context.Subs == null)
            {
                return RedirectToAction(AppConstants.Routing.ActionContact);
            }

            var sub = await _context.Subs.FirstOrDefaultAsync(s => s.Email == email);

            if (sub != null)
            {
                sub.IsActive = false;
                sub.Newsletter = false;
                _context.Subs.Update(sub);
                await _context.SaveChangesAsync();

                // POPRAWKA: U퓓wamy Keys dla klucza
                TempData[AppConstants.Keys.ContactSuccess] = AppConstants.Texts.Messages.UnsubscribeSuccess;
            }
            else
            {
                TempData[AppConstants.Keys.ContactError] = AppConstants.Texts.Messages.EmailNotFound;
            }

            return RedirectToAction(AppConstants.Routing.ActionContact);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}