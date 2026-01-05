using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using INeed.Data;
using INeed.Models;
using System.Diagnostics;

namespace INeed.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var forms = _context.Forms != null
                ? await _context.Forms.Where(f => f.IsActive).ToListAsync()
                : new List<Form>();

            return View(forms);
        }

        public IActionResult Contact()
        {
            return View();
        }

        // --- NOWA METODA: WYPISANIE Z NEWSLETTERA ---
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Unsubscribe(string email)
        {
            if (string.IsNullOrEmpty(email) || _context.Subs == null)
            {
                return RedirectToAction("Contact");
            }

            // Szukamy subskrybenta po mailu
            var sub = await _context.Subs.FirstOrDefaultAsync(s => s.Email == email);

            if (sub != null)
            {
                // Zamiast usuwaæ, zmieniamy statusy na false
                sub.IsActive = false;
                sub.Newsletter = false;

                _context.Subs.Update(sub);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Contact");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
