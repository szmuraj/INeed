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
                    CookieButton = "Akceptuję",
                    Slogan = "Bo cechuje nas profesjonalizm",
                    NavSurveys = "Ankiety",
                    NavContact = "Kontakt",
                    FooterRights = "Wszelkie prawa zastrzeżone."
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
                    ResultScore = "Wynik",
                    GenderSelectionTitle = "Wybierz swoją płeć:",
                    GenderSelectionInfo = "Wybór ma wpływ na normy. Wybierz 'Nie podaję', aby zobaczyć wyniki dla obu płci.",
                    DontSpecify = "Nie podaję",
                    StenLow = "Niski",
                    StenAverage = "Przeciętny",
                    StenHigh = "Wysoki",
                    NoImage = "Brak grafiki"
                },

                Buttons = new ButtonResources
                {
                    SendMessage = "Wyślij wiadomość",
                    Delete = "Usuń moje dane",
                    Back = "Powrót",
                    BackToHome = "Powrót na stronę główną",
                    Continue = "Dalej",
                    SendAnswers = "Zakończ i pokaż wynik",
                    SendResults = "Wyślij wynik na e-mail",
                    Start = "Rozpocznij",
                    RetakeTest = "Wypełnij ponownie",
                    StartTest = "Rozpocznij test",
                    Check = "SPRAWDŹ"
                },

                PolicyContent = new PolicyContentResources
                {
                    PrivacyAndCookies = @"
                        <h4 class='fw-bold'>POLITYKA PRYWATNOŚCI INeed sp. z o.o.</h4>
                        <p><strong>1. Informacje ogólne</strong><br>
                        Administratorem danych osobowych jest: <strong>INeed sp. z o.o.</strong><br>
                        Adres siedziby: Al. Kilińskiego 12, 09-402 Płock<br>
                        NIP: 123-456-78-90 | KRS: 0001234567 | REGON: 123456789<br>
                        E-mail: bok@ineed.com | Tel.: 661 121 122<br>
                        Niniejsza Polityka prywatności określa zasady przetwarzania Danych osobowych przez INeed sp. z o.o. zgodnie z Rozporządzeniem Parlamentu Europejskiego i Rady (UE) 2016/679 z 27 kwietnia 2016 r. (RODO).</p>

                        <p><strong>2. Cele i podstawy przetwarzania</strong><br>
                        Dane przetwarzane są w celu:
                        <ul>
                            <li>Świadczenia usług drogą elektroniczną (np. wysyłka wyników ankiety) – art. 6 ust. 1 lit. b RODO.</li>
                            <li>Komunikacji z Użytkownikami (odpowiedzi na zapytania) – art. 6 ust. 1 lit. f RODO.</li>
                            <li>Marketingu (Newsletter) – art. 6 ust. 1 lit. a RODO (zgoda).</li>
                            <li>Analityki i bezpieczeństwa serwisu – art. 6 ust. 1 lit. f RODO.</li>
                        </ul></p>

                        <p><strong>3. Odbiorcy danych</strong><br>
                        Odbiorcami danych mogą być podmioty świadczące usługi na rzecz Administratora (np. hosting, obsługa IT) na podstawie umów powierzenia.</p>

                        <p><strong>4. Prawa osób, których dane dotyczą</strong><br>
                        Posiadasz prawo dostępu do treści swoich danych, ich sprostowania, usunięcia, ograniczenia przetwarzania, przenoszenia, wniesienia sprzeciwu oraz cofnięcia zgody w dowolnym momencie. W celu realizacji praw prosimy o kontakt: bok@ineed.com.</p>

                        <p><strong>5. Profilowanie</strong><br>
                        Dane ankietowe podlegają zautomatyzowanej analizie w celu wygenerowania wyniku. Nie wywołuje to skutków prawnych względem Użytkownika.</p>

                        <hr class='my-4'>

                        <h4 class='fw-bold'>POLITYKA PLIKÓW COOKIES</h4>
                        <p><strong>1. Informacje wstępne</strong><br>
                        Serwis ineed.com wykorzystuje pliki cookies (ciasteczka) w celu zapewnienia poprawnego działania, analityki oraz marketingu.</p>

                        <p><strong>2. Rodzaje cookies</strong><br>
                        <ul>
                            <li><strong>Niezbędne:</strong> Wymagane do działania ankiety (zapamiętywanie postępu).</li>
                            <li><strong>Analityczne:</strong> Anonimowe statystyki (np. Google Analytics), pomagające ulepszać serwis.</li>
                        </ul></p>

                        <p><strong>3. Zarządzanie</strong><br>
                        Użytkownik może w każdej chwili zmienić ustawienia dotyczące plików cookies w swojej przeglądarce internetowej lub poprzez baner cookies na stronie. Wyłączenie cookies niezbędnych może uniemożliwić wypełnienie ankiety.<br>
                        <strong>Kontakt:</strong> W sprawach cookies prosimy o kontakt: bok@ineed.com.</p>
                    ",

                    Terms = @"
                        <h4 class='fw-bold'>REGULAMIN SERWISU INeed</h4>
                        
                        <p><strong>I. Postanowienia ogólne</strong><br>
                        Niniejszy Regulamin określa zasady korzystania z serwisu Kwestionariusz Oceny Potrzeb, prowadzonego przez INeed sp. z o.o. (Al. Kilińskiego 12, 09-402 Płock).<br>
                        Serwis jest darmowy, a świadczona usługa ma charakter wyłącznie edukacyjny i doradczy.</p>

                        <p><strong>II. Prawa autorskie</strong><br>
                        Aplikacja bazuje na opracowaniu Anny Paszkowskiej-Rogacz. Wszelkie prawa do kodu oraz marki należą do Administratora.</p>

                        <p><strong>III. Wyłączenie odpowiedzialności (Disclaimer)</strong><br>
                        Wyniki generowane przez ankietę nie stanowią diagnozy psychologicznej ani medycznej. Aplikacja służy jedynie wsparciu rozwoju osobistego. Administrator nie ponosi odpowiedzialności za decyzje życiowe lub zawodowe podjęte przez Użytkownika na podstawie wyniku.</p>

                        <p><strong>IV. Zasady korzystania</strong><br>
                        Użytkownik zobowiązany jest do korzystania z serwisu zgodnie z prawem. Zabronione jest dostarczanie treści o charakterze bezprawnym.</p>

                        <p><strong>V. Ochrona danych i Newsletter</strong><br>
                        Zasady przetwarzania danych określa Polityka Prywatności. Zapis na Newsletter wymaga dobrowolnej zgody (Double Opt-in).</p>
                    "
                },

                Contact = new ContactResources
                {
                    PageTitle = "Kontakt",
                    Header = "Jesteśmy tu dla Ciebie",
                    SubHeader = "Masz pytania? Napisz do nas.",
                    OfficeName = "Biuro Obsługi",
                    Address = "Al. Kilińskiego 12 p. 8 Płock",
                    Phone = "+48 123 456 789",
                    Email = "bok@ineed.com",
                    FormContactHeader = "Formularz kontaktowy",
                    RodoConsent = "Wyrażam zgodę na przetwarzanie danych.",
                    FormUnsubscribeHeader = "Chcę usunąć swoje dane",
                    ModalTitle = "Czy na pewno chcesz usunąć swoje dane?",
                    ResultsHeader = "Pobierz swoje wyniki",
                    ResultsSubHeader = "Wyślij wszystkie wyniki powiązane z Twoim identyfikatorem na e-mail.",
                    DeleteIdLabel = "Identyfikator danych do usunięcia:"
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
                    CombinedResultsTitle = "Twoje zbiorcze wyniki",
                    ResultsForIdBody = "Poniżej znajdują się wszystkie wyniki ankiet powiązane z identyfikatorem",
                    UnknownSurvey = "Nieznana ankieta",
                    FillDate = "Data wypełnienia",
                    NoResultsFound = "Nie znaleziono żadnych wyników dla Twojego identyfikatora.",
                    EmailRequired = "Proszę podać adres e-mail.",
                    AllResultsSentSuccess = "Wszystkie wyniki zostały wysłane na podany adres e-mail.",
                    NoData = "Brak danych do wyświetlenia."
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
                    AcceptTerms = ", a także zgadzam się na przetwarzanie danych w celu wysyłki wyniku.",
                    NormsFemale = "Normy dla Kobiet",
                    NormsMale = "Normy dla Mężczyzn",
                    YourResultSten = "Twój wynik (STEN)",
                    CategoryAriaLabelFormat = "Kategoria: {0}. Twój wynik: {1} na {2}. Naciśnij Enter, aby odczytać szczegółowy opis.",
                    EmailInstruction = "Jeżeli chcesz by wysłać wynik twojej ankiety na maila wprowadź go teraz, naciśnij przycisk Akceptuję, a następnie przycisk wyślij."
                },

                Home = new HomeResources
                {
                    Header = "Dostępne Kwestionariusze",
                    SubHeader = "Wybierz ankietę z listy poniżej, aby rozpocząć proces oceny.",
                    NoActiveQuestionnaires = "Brak aktywnych kwestionariuszy."
                }
            };
        }
    }
}