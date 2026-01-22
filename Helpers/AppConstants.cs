using INeed.Models;
using INeed.Models.ViewModels;
using System.Globalization;

namespace INeed.Helpers
{
    public static class AppConstants
    {
        // =========================================================
        // 1. STAŁE TECHNICZNE (Klucze, Kolory, Routing, Ścieżki)
        // =========================================================

        public const string FillRoute = "Fill";

        // NOWA KLASA: WARTOŚCI DOMYŚLNE
        public static class Defaults
        {
            public const string VisitorId = "000000";
        }

        public static class Keys
        {
            public const string SuccessMessage = "SuccessMessage";
            public const string ErrorMessage = "ErrorMessage";
            public const string Title = "Title";
            public const string ContactSuccess = "ContactSuccess";
            public const string ContactError = "ContactError";
        }

        public static class Colors
        {
            // Primary (Tło paska bocznego): Ciemny granat
            public const string Primary = "#1A2D41";

            // Accent (Elementy dekoracyjne): Niebieski
            public const string Accent = "#1565C0";

            public const string BackgroundLight = "#f8f9fa";
            public const string ButtonAction = "#ffc107";

            // ActiveNav: Turkusowy
            public const string ActiveNav = "#26A69A";

            // ButtonSubmit: Limonkowy
            public const string ButtonSubmit = "#76FF03";

            public const string ButtonDanger = "#dc3545";

            public const string Black = "#000000";
            public const string White = "#ffffff";
            public const string TextSecondary = "#6c757d";

            // Nowe kolory (nagłówek ankiety)
            public const string TextMain = "#D9D9D9";
            public const string TextHighlight = "#76FF03";
            public const string BorderTransparent = "rgba(217,217,217,0.1)";

            // Kolory kategorii
            public const string Achievement = "#76FF03";
            public const string Affiliation = "#26A69A";
            public const string Autonomy = "#FFF700";
            public const string Dominance = "#D32F2F";
        }

        public static class Routing
        {
            public const string HomeController = "Home";
            public const string QuestionnaireController = "Questionnaire";
            public const string CultureController = "Culture";
            public const string ActionIndex = "Index";
            public const string ActionFill = "Fill";
            public const string ActionContact = "Contact";
            public const string ActionSetCulture = "SetCulture";
            public const string ActionInfo = "Info";
        }

        public static class Assets
        {
            public const string LogoPath = "~/images/Logo.png";
            public const string NamePath = "~/images/Name.png";
        }

        // =========================================================
        // 2. MECHANIZM TŁUMACZEŃ
        // =========================================================

        // Sprawdza aktualną kulturę wątku (ustawioną przez Cookie/Middleware)
        private static bool IsEn => CultureInfo.CurrentUICulture.Name.StartsWith("en");

        // Zwraca odpowiedni zestaw tekstów w zależności od języka
        public static TextResources Texts => IsEn ? AppConstantsEN.Get() : AppConstantsPL.Get();


        // =========================================================
        // 3. FABRYKA TREŚCI (NOWE)
        // =========================================================
        
        public static InfoPageVm? GetPageContent(string pageId)
        {
            var txt = Texts;

            return pageId?.ToLower() switch
            {
                "privacy" => new InfoPageVm
                {
                    Title = txt.Layout.PrivacyPolicy,
                    Subtitle = txt.Layout.CookieHeader,
                    HtmlContent = txt.PolicyContent.PrivacyAndCookies
                },

                "terms" => new InfoPageVm
                {
                    Title = txt.Layout.Terms,
                    Subtitle = txt.CompanyName,
                    HtmlContent = txt.PolicyContent.Terms
                },

                _ => null // Nieznana strona
            };
        }

        // =========================================================
        // 4. METODY POMOCNICZE (DRY)
        // =========================================================

        public static string GetVisitorId(string? visitorId)
        {
            return string.IsNullOrEmpty(visitorId) ? Defaults.VisitorId : visitorId;
        }

        // =========================================================
        // 5. HELPERY TREŚCI (SCALABILITY)
        // =========================================================

        public static string SelectContent(string defaultContent, string? enContent)
        {
            if (IsEn && !string.IsNullOrEmpty(enContent))
            {
                return enContent;
            }
            return defaultContent;
        }

        public static bool IsCategoryMatch(Category cat, string key)
        {
            if (cat == null || string.IsNullOrWhiteSpace(key)) return false;

            var k = key.Trim();

            // 1. Sprawdź PL (Default)
            if (cat.Name.Trim().Equals(k, StringComparison.OrdinalIgnoreCase)) return true;

            // 2. Sprawdź EN
            if (cat.NameEN != null && cat.NameEN.Trim().Equals(k, StringComparison.OrdinalIgnoreCase)) return true;

            // 3. W przyszłości:
            // if (cat.NameDE != null && cat.NameDE.Trim().Equals(k, StringComparison.OrdinalIgnoreCase)) return true;

            return false;
        }
    }
}