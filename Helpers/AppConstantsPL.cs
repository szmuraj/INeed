namespace INeed.Helpers
{
    public static class AppConstantsPL
    {
        public static TextResources Get()
        {
            return new TextResources
            {
                CompanyName = "INeed",
                Slogan = "Bo cechuje nas profesjonalizm",

                Layout = new LayoutResources
                {
                    Home = "Strona główna",
                    Privacy = "Prywatność",
                    Questionnaires = "Ankiety",
                    Contact = "Kontakt",
                    PrivacyPolicy = "Polityka Prywatności",
                    Terms = "Regulamin",
                    CookieHeader = "Pliki Cookies",
                    CookieBody = "Używamy plików cookies, aby zapewnić najlepszą jakość korzystania z naszej strony.",
                    CookieButton = "Akceptuję"
                },

                Labels = new LabelResources
                {
                    YourEmailPlaceholder = "Twój adres e-mail",
                    MessagePlaceholder = "Wpisz swoją wiadomość tutaj...",
                    EmailPlaceholder = "Adres e-mail",
                    FirstName = "Imię",
                    Woman = "Kobieta",
                    Man = "Mężczyzna",
                    Category = "Kategoria",
                    ResultScore = "Wynik"
                },

                // ... (Buttons, PolicyContent, Contact, Fill, Result, Home - bez zmian, przekopiuj ze starego pliku lub zostaw jak były) ...
                Buttons = new ButtonResources
                {
                    SendMessage = "Wyślij wiadomość",
                    Delete = "Usuń moje dane",
                    Back = "Powrót",
                    BackToHome = "Powrót na stronę główną",
                    Continue = "Dalej",
                    SendAnswers = "Zakończ i pokaż wynik",
                    SendResults = "Wyślij wynik na e-mail",
                    Start = "Rozpocznij"
                },

                PolicyContent = new PolicyContentResources
                {
                    PrivacyAndCookies = "Tresc polityki...", // (Skróciłem dla czytelności, wklej pełną treść)
                    Terms = "Tresc regulaminu..."
                },

                Contact = new ContactResources
                {
                    PageTitle = "Kontakt",
                    Header = "Jesteśmy tu dla Ciebie",
                    SubHeader = "Masz pytania? Napisz do nas.",
                    OfficeName = "Biuro Obsługi",
                    Address = "al. Kilińskiego 12 p. 8 Płock",
                    Phone = "+48 123 456 789",
                    Email = "bok@ineed.com",
                    FormContactHeader = "Formularz kontaktowy",
                    RodoConsent = "Wyrażam zgodę na przetwarzanie danych.",
                    FormUnsubscribeHeader = "Chcę usunąć swoje dane",
                    ModalTitle = "Czy na pewno chcesz usunąć swoje dane?"
                },

                Fill = new FillResources
                {
                    PageTitle = "Wypełnianie ankiety",
                    Step = "Pytanie",
                    Of = "z",
                    SelectOption = "Wybierz odpowiedź:",
                    Page = "Strona",
                    NoQuestions = "Brak dostępnych pytań.",
                    AlertPage = "Proszę odpowiedzieć na wszystkie pytania na tej stronie.",
                    AlertAll = "Proszę odpowiedzieć na wszystkie pytania przed wysłaniem."
                },

                Messages = new MessageResources
                {
                    Success = "Operacja zakończona sukcesem.",
                    Error = "Wystąpił błąd. Spróbuj ponownie.",
                    ValidationRequired = "To pole jest wymagane.",
                    EmailSent = "Wiadomość została wysłana.",
                    DataDeleted = "Twoje dane zostały usunięte.",
                    RodoRequired = "Musisz wyrazić zgodę na przetwarzanie danych (RODO).",
                    NewMessage = "Nowa wiadomość od użytkownika:",
                    ContactSuccess = "Twoja wiadomość została wysłana pomyślnie.",
                    ContactError = "Nie udało się wysłać wiadomości. Spróbuj ponownie.",
                    FormIncomplete = "Formularz jest niekompletny.",
                    UnsubscribeSuccess = "Twój adres e-mail został usunięty z naszej bazy.",
                    EmailNotFound = "Podany adres e-mail nie został znaleziony.",
                    Wyniki = "Twoje Wyniki",
                    EmailThanks = "Dziękujemy za wypełnienie ankiety. Poniżej przesyłamy zestawienie Twoich wyników.",
                    GeneratedBy = "Wiadomość wygenerowana automatycznie przez system INeed.",
                    YourResults = "Twoje wyniki:",
                    EmailSentSuccess = "Wyniki zostały wysłane na podany adres e-mail.",
                    EmailSentError = "Wystąpił błąd podczas wysyłania e-maila.",

                    // NOWE
                    CombinedResultsTitle = "Twoje zbiorcze wyniki",
                    ResultsForIdBody = "Poniżej znajdują się wszystkie wyniki ankiet powiązane z identyfikatorem",
                    UnknownSurvey = "Nieznana ankieta",
                    FillDate = "Data wypełnienia",
                    NoResultsFound = "Nie znaleziono żadnych wyników dla Twojego identyfikatora.",
                    EmailRequired = "Proszę podać adres e-mail.",
                    AllResultsSentSuccess = "Wszystkie wyniki zostały wysłane na podany adres e-mail."
                },

                Result = new ResultResources
                {
                    PageTitle = "Twój Wynik",
                    Header = "Twój Wynik",
                    SubHeader = "Poniżej znajdziesz szczegółową analizę Twoich odpowiedzi.",
                    SaveHeader = "Zapisz swój wynik",
                    SaveSubHeader = "Podaj adres e-mail, aby otrzymać trwałą kopię wyników oraz interpretację.",
                    RodoConsent = "Akceptuję",
                    And = "oraz",
                    AcceptTerms = ", a także zgadzam się na przetwarzanie danych w celu wysyłki wyniku."
                },

                Home = new HomeResources
                {
                    Header = "Dostępne Kwestionariusze",
                    SubHeader = "Wybierz ankietę z listy poniżej, aby rozpocząć proces oceny."
                }
            };
        }
    }
}