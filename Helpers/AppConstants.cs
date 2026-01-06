// Ignore Spelling: Rodo Wyniki

using System;

namespace INeed.Helpers
{
    public static class AppConstants
    {
        public static class Colors
        {
            public const string White = "#FFFFFF";
            public const string Primary = "#1A2D41";
            public const string Accent = "#1565C0";
            public const string ActiveNav = "#26A69A";
            public const string BackgroundLight = "#F9FDF4";
            public const string Achievement = "#76FF03";
            public const string Affiliation = "#26A69A";
            public const string Autonomy = "#FFF700";
            public const string Dominance = "#D32F2F";
            public const string ButtonSubmit = "#FFF700";
            public const string ButtonAction = "#76FF03";
            public const string ButtonDanger = "#FF0000";
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

            // Tutaj znajduje się treść HTML wstrzykiwana do Modali
            public static class PolicyContent
            {
                public static readonly string PrivacyAndCookies = @"
                    <h4 class='mb-3'>POLITYKA PRYWATNOŚCI INeed sp. z o.o.</h4>
                    
                    <h6>1. Informacje ogólne</h6>
                    <p>
                        Administratorem danych osobowych jest: <strong>INeed sp. z o.o.</strong><br/>
                        Adres siedziby: Al. Kilińskiego 12, 09-402 Płock<br/>
                        NIP: 123-456-78-90 | KRS: 0001234567 | REGON: 123456789<br/>
                        E-mail: bok@ineed.com | Tel.: 661 121 122
                    </p>
                    <p>Niniejsza Polityka prywatności określa zasady przetwarzania Danych osobowych przez INeed sp. z o.o. zgodnie z Rozporządzeniem Parlamentu Europejskiego i Rady (UE) 2016/679 z 27 kwietnia 2016 r. (RODO).</p>
                    
                    <h6>2. Cele i podstawy przetwarzania</h6>
                    <p>Dane przetwarzane są w celu:</p>
                    <ul>
                        <li>Świadczenia usług drogą elektroniczną (np. wysyłka wyników ankiety) – art. 6 ust. 1 lit. b RODO.</li>
                        <li>Komunikacji z Użytkownikami (odpowiedzi na zapytania) – art. 6 ust. 1 lit. f RODO.</li>
                        <li>Marketingu (Newsletter) – art. 6 ust. 1 lit. a RODO (zgoda).</li>
                        <li>Analityki i bezpieczeństwa serwisu – art. 6 ust. 1 lit. f RODO.</li>
                    </ul>

                    <h6>3. Odbiorcy danych</h6>
                    <p>Odbiorcami danych mogą być podmioty świadczące usługi na rzecz Administratora (np. hosting, obsługa IT) na podstawie umów powierzenia.</p>

                    <h6>4. Prawa osób, których dane dotyczą</h6>
                    <p>Posiadasz prawo dostępu do treści swoich danych, ich sprostowania, usunięcia, ograniczenia przetwarzania, przenoszenia, wniesienia sprzeciwu oraz cofnięcia zgody w dowolnym momencie. W celu realizacji praw prosimy o kontakt: bok@ineed.com.</p>

                    <h6>5. Profilowanie</h6>
                    <p>Dane ankietowe podlegają zautomatyzowanej analizie w celu wygenerowania wyniku. Nie wywołuje to skutków prawnych względem Użytkownika.</p>
                    
                    <hr class='my-4'>
                    
                    <h4 class='mb-3'>POLITYKA PLIKÓW COOKIES</h4>
                    
                    <h6>1. Informacje wstępne</h6>
                    <p>Serwis ineed.com wykorzystuje pliki cookies (ciasteczka) w celu zapewnienia poprawnego działania, analityki oraz marketingu.</p>
                    
                    <h6>2. Rodzaje cookies</h6>
                    <ul>
                        <li><strong>Niezbędne:</strong> Wymagane do działania ankiety (zapamiętywanie postępu).</li>
                        <li><strong>Analityczne:</strong> Anonimowe statystyki (np. Google Analytics), pomagające ulepszać serwis.</li>
                    </ul>
                    
                    <h6>3. Zarządzanie</h6>
                    <p>
                        Użytkownik może w każdej chwili zmienić ustawienia dotyczące plików cookies w swojej przeglądarce internetowej lub poprzez baner cookies na stronie. Wyłączenie cookies niezbędnych może uniemożliwić wypełnienie ankiety.<br/>
                        Kontakt: W sprawach cookies prosimy o kontakt: bok@ineed.com.
                    </p>";

                public static readonly string Terms = @"
                    <h4 class='mb-3'>REGULAMIN SERWISU INeed</h4>
                    
                    <h6>I. Postanowienia ogólne</h6>
                    <p>Niniejszy Regulamin określa zasady korzystania z serwisu dostępnego pod adresem ineed.com, prowadzonego przez INeed sp. z o.o. z siedzibą w Płocku, Al. Kilińskiego 12.</p>
                    
                    <h6>II. Rodzaje usług</h6>
                    <p>Usługodawca świadczy usługi: udostępnienia Kwestionariusza oceny potrzeb, generowania wyników oraz wysyłki Newslettera.</p>
                    
                    <h6>III. Zasady korzystania</h6>
                    <ul>
                        <li>Korzystanie z Serwisu jest bezpłatne.</li>
                        <li>Użytkownik zobowiązany jest do korzystania z Serwisu zgodnie z prawem i dobrymi obyczajami.</li>
                        <li>Zakazane jest dostarczanie treści o charakterze bezprawnym.</li>
                    </ul>
                    
                    <h6>IV. Własność intelektualna</h6>
                    <p>Treści w Serwisie, w tym metodologia Kwestionariusza, są chronione prawem autorskim. Kopiowanie bez zgody jest zabronione.</p>
                    
                    <h6>V. Wyłączenie odpowiedzialności</h6>
                    <p>Wyniki generowane przez system mają charakter wyłącznie edukacyjny i poglądowy. Nie stanowią diagnozy psychologicznej ani doradztwa zawodowego. Usługodawca nie ponosi odpowiedzialności za decyzje podjęte na podstawie wyników.</p>
                    
                    <h6>VI. Reklamacje</h6>
                    <p>Reklamacje można składać drogą mailową na adres: bok@ineed.com. Termin rozpatrzenia wynosi 14 dni.</p>";
            }

            public static class Layout
            {
                public const string Home = "Strona główna";
                public const string Questionnaires = "Kwestionariusze";
                public const string Contact = "Kontakt";
                public const string PrivacyPolicy = "Polityka Prywatności";
                public const string Terms = "Regulamin Serwisu";
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
                public const string EmailSentSuccess = "Wiadomość została wysłana. Dziękujemy!";
                public const string EmailSentError = "Nie udało się wysłać wiadomości e-mail. Spróbuj ponownie później.";
                public const string RodoRequired = "Musisz wyrazić zgodę na przetwarzanie danych, aby wysłać wiadomość.";
                public const string FormIncomplete = "Proszę wypełnić wszystkie pola.";
                public const string UnsubscribeSuccess = "Twój adres email został usunięty z listy subskrybentów.";
                public const string EmailNotFound = "Podany adres email nie znajduje się w naszej bazie.";
                public const string GeneratedBy = "Wiadomość wygenerowana automatycznie przez system INeed.";
                public const string PrivacyPolicyLinkText = "Polityką Prywatności";
                public const string TermsLinkText = "Regulaminem Serwisu";
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