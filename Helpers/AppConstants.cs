using System.Globalization;

namespace INeed.Helpers
{
    public static class AppConstants
    {
        // =========================================================
        // 1. STAŁE TECHNICZNE (Klucze, Kolory, Routing, Ścieżki)
        // =========================================================

        public const string FillRoute = "Fill";

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

            // ActiveNav: Turkusowy (zgodnie z Twoim życzeniem)
            public const string ActiveNav = "#26A69A";

            // ButtonSubmit: Limonkowy
            public const string ButtonSubmit = "#76FF03";

            public const string ButtonDanger = "#dc3545";

            public const string Black = "#000000";
            public const string White = "#ffffff";
            public const string TextSecondary = "#6c757d";

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
    }
}