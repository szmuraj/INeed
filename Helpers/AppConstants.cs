namespace INeed.Helpers
{
    public static class AppConstants
    {
        public static class Colors
        {
            public const string White = "#FFFFFF";

            // UI & Layout
            public const string Primary = "#1A2D41";
            public const string Accent = "#1565C0";
            public const string ActiveNav = "#26A69A";
            public const string BackgroundLight = "#F9FDF4";

            // Wykresy
            public const string Achievement = "#76FF03";
            public const string Affiliation = "#26A69A";
            public const string Autonomy = "#FFF700";
            public const string Dominance = "#D32F2F";

            // Przyciski
            public const string ButtonSubmit = "#FFF700";
            public const string ButtonAction = "#76FF03";
            public const string ButtonDanger = "#FF0000";

            //Text
            public const string TextPrimary = "#212121";
            public const string TextSecondary = "#757575";
            public const string TextLink = "#1565C0";
            public const string TextOnPrimary = "#FFFFFF";
            public const string Black = "#000";
        }

        public static class Texts
        {
            public const string CompanyName = "INeed";
            public const string Slogan = "Bo cechuje nas profesjonalizm";
            public const string FillRoute = "/Questionnaire/Fill";
            public const string ResultRoute = "/Questionnaire/Result";
            public const string Privacy = "Polityka Prywatności";

            public static class Layout
            {
                public const string Home = "Strona główna";
                public const string Questionnaires = "Kwestionariusze";
                public const string Contact = "Kontakt";
                public const string PrivacyPolicy = "Polityka Prywatności";
                public const string Terms = "Warunki korzystania";

                public const string CookieHeader = "Dbamy o Twoją prywatność";
                public const string CookieBody = "Nasza strona używa plików cookies (ciasteczek), aby zapewnić Ci najlepsze doświadczenie. Korzystając z serwisu, zgadzasz się na ich użycie zgodnie z naszą";
                public const string CookieButton = "Rozumiem, akceptuję";
                public const string Another = "INNE";
            }

            public static class Labels
            {
                public const string EmailPlaceholder = "adres@poczta.pl";
                public const string YourEmailPlaceholder = "Twój email";
                public const string MessagePlaceholder = "Treść wiadomości...";

                public const string Achievement = "Potrzeba Osiągnięć";
                public const string Affiliation = "Potrzeba Afiliacji";
                public const string Autonomy = "Potrzeba Autonomii";
                public const string Dominance = "Potrzeba Dominacji";

                public const string AchievementShort = "ACH";
                public const string AffiliationShort = "AFF";
                public const string AutonomyShort = "AUT";
                public const string DominanceShort = "DOM";
            }

            public static class Buttons
            {
                public const string Send = "WYŚLIJ";
                public const string SendMessage = "WYŚLIJ WIADOMOŚĆ";
                public const string SendResults = "WYŚLIJ WYNIKI";
                public const string SendAnswers = "WYŚLIJ ODPOWIEDZI";
                public const string Continue = "KONTYNUUJ";
                public const string Back = "WRÓĆ";
                public const string Delete = "Usuń";
                public const string BackToHome = "POWRÓT DO STRONY GŁÓWNEJ";
            }

            public static class Contact
            {
                public const string PageTitle = "Kontakt";
                public const string Header = "Kontakt";
                public const string SubHeader = "Masz pytania dotyczące systemu ocen? Jesteśmy do Twojej dyspozycji.";

                public const string OfficeName = "Biuro INeed";
                public const string Address = "al. Kilińskiego 12 p. 8";
                public const string Phone = "661 121 122";
                public const string Email = "bok@ineed.com";

                public const string FormContactHeader = "Napisz do nas";
                public const string FormUnsubscribeHeader = "Formularz prośby usunięcia z Newslettera";

                public const string ModalTitle = "Czy jesteś pewien/pewna?";
                public const string RodoConsent = "Wyrażam zgodę na przetwarzanie moich danych osobowych w celu obsługi zapytania. Administratorem danych jest INeed. Więcej w";
            }

            public static class Result
            {
                public const string PageTitle = "Wyniki Twojej Ankiety";
                public const string Header = "Twój Profil Motywacyjny";
                public const string SubHeader = "Poniżej przedstawiamy szczegółowe wyniki Twojego testu.";

                public const string SaveHeader = "Zapisz swoje wyniki";
                public const string SaveSubHeader = "Podaj swój adres e-mail, aby otrzymać kopię raportu.";

                public const string RodoConsent = "Wyrażam zgodę na przetwarzanie moich danych osobowych w celu przesłania wyników ankiety. Oświadczam, że zapoznałem/am się z";
                public const string And = "oraz";
                public const string AcceptTerms = "i akceptuję ich postanowienia.";
            }

            public static class Fill
            {
                public const string Page = "Strona";
                public const string InactiveForm = "Ten formularz jest nieaktywny.";
                public const string NoQuestions = "Brak pytań.";
                public const string AlertPage = "Proszę udzielić odpowiedzi na wszystkie pytania na tej stronie.";
                public const string AlertAll = "Proszę udzielić odpowiedzi na wszystkie pytania.";
            }

            public static class Messages
            {
                // To jest ten nowy tekst, który powinien się pojawić po poprawnej kompilacji
                public const string EmailSentSuccess = "Wiadomość została wysłana. Dziękujemy!";
                public const string EmailSentError = "Nie udało się wysłać wiadomości e-mail. Spróbuj ponownie później.";
                public const string RodoRequired = "Musisz wyrazić zgodę na przetwarzanie danych, aby wysłać wiadomość.";
                public const string FormIncomplete = "Proszę wypełnić wszystkie pola.";
                public const string UnsubscribeSuccess = "Twój adres email został usunięty z listy subskrybentów.";
                public const string EmailNotFound = "Podany adres email nie znajduje się w naszej bazie.";
                public const string GeneratedBy = "Wiadomość wygenerowana automatycznie przez system INeed.";
                public const string PrivacyPolicyLinkText = "Polityką Prywatności";
                public const string TermsLinkText = "Warunkami korzystania";
                public const string EmailThanks = "Dziękujemy za skorzystanie z naszego kwestionariusza. Poniżej znajdziesz szczegółowe wyniki swojej ankiety.";
                public const string SuccessMessage = "SuccessMessage";
                public const string ErrorMessage = "ErrorMessage";
                public const string YourResults = "Twoje wyniki";
                public const string Index = "Index";
                public const string Home = "Home";
                public const string Result = "Result";
                public const string Error = "Error";
                public const string Fill = "Fill";
                public const string Contact = "Contact";
                public const string Questionnaires = "Questionnaires";
                public const string Questionnaire = "Questionnaire";
                public const string Title = "Title";
                public const string Wyniki = "Wyniki";
                public const string ContactError = "ContactError";
                public const string ContactSuccess = "ContactSuccess";
                public const string NewMessage = "Nowa wiadomość od:";
            }
        }
    }
}