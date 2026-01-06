using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;

namespace INeed.Controllers
{
    public class CultureController : Controller
    {
        [HttpPost]
        public IActionResult SetCulture(string culture, string returnUrl)
        {
            if (!string.IsNullOrEmpty(culture))
            {
                Response.Cookies.Append(
                    CookieRequestCultureProvider.DefaultCookieName,
                    CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                    new CookieOptions
                    {
                        Expires = DateTimeOffset.UtcNow.AddYears(1),
                        IsEssential = true, // <--- TO JEST KLUCZOWE! Bez tego RODO blokuje ciasteczko.
                        SameSite = SameSiteMode.Strict
                    }
                );
            }

            return LocalRedirect(string.IsNullOrEmpty(returnUrl) ? "~/" : returnUrl);
        }
    }
}