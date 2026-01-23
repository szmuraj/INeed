using System.Globalization;
using INeed.Models;

namespace INeed.Helpers
{
    public static class AppConstants
    {
        public const string FillRoute = "Fill";

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
            public const string Primary = "#1A2D41";
            public const string Accent = "#1565C0";
            public const string BackgroundLight = "#f8f9fa";
            public const string ButtonAction = "#ffc107";
            public const string ActiveNav = "#26A69A";
            public const string ButtonSubmit = "#76FF03";
            public const string ButtonDanger = "#dc3545";
            public const string Black = "#000000";
            public const string White = "#ffffff";
            public const string TextSecondary = "#6c757d";
            public const string TextMain = "#D9D9D9";
            public const string TextHighlight = "#76FF03";
            public const string TextDark = "#333333";
            public const string BorderTransparent = "rgba(217,217,217,0.1)";
            public const string FocusOutline = "#0d6efd";
            public const string InputBackgroundActive = "#f0f8ff";
            public const string BorderDefault = "#dee2e6";

            // --- KOLORY WIDOKU WYNIKÓW (RESULTS) ---
            public const string ProgressBackground = "#f0f0f0"; // Tło paska postępu

            // Sekcja Kobiety
            public const string BgFemale = "#fff0f6";      // Różowe tło
            public const string BorderFemale = "#fcc2d7";  // Różowa ramka
            public const string TextFemale = "#d63384";    // Ciemny róż tekstu

            // Sekcja Mężczyźni
            public const string BgMale = "#f0f7ff";        // Niebieskie tło
            public const string BorderMale = "#bac8ff";    // Niebieska ramka
            public const string TextMale = "#0d6efd";      // Ciemny niebieski tekstu
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
        // MECHANIZM TŁUMACZEŃ I HELPERY
        // =========================================================

        private static bool IsEn => CultureInfo.CurrentUICulture.Name.StartsWith("en");
        public static TextResources Texts => IsEn ? AppConstantsEN.Get() : AppConstantsPL.Get();

        /// Zwraca podany identyfikator lub domyślny ("000000").
        public static string GetVisitorId(string? visitorId)
        {
            return string.IsNullOrEmpty(visitorId) ? Defaults.VisitorId : visitorId;
        }

        /// Wybiera treść w zależności od języka.
        public static string SelectContent(string defaultContent, string? enContent)
        {
            if (IsEn && !string.IsNullOrEmpty(enContent)) return enContent;
            return defaultContent;
        }

        /// Sprawdza dopasowanie kategorii.
        public static bool IsCategoryMatch(Category cat, string key)
        {
            if (cat == null || string.IsNullOrWhiteSpace(key)) return false;
            var k = key.Trim();
            if (cat.Name.Trim().Equals(k, StringComparison.OrdinalIgnoreCase)) return true;
            if (cat.NameEN != null && cat.NameEN.Trim().Equals(k, StringComparison.OrdinalIgnoreCase)) return true;
            return false;
        }
    }
}