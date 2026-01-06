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
                    FirstName = "Imię"
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
                    Start = "Rozpocznij"
                },

                // --- ZAKTUALIZOWANA TREŚĆ POLITYK (HTML) ---
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
                        Niniejszy Regulamin określa zasady korzystania z serwisu dostępnego pod adresem ineed.com, prowadzonego przez INeed sp. z o.o. z siedzibą w Płocku, Al. Kilińskiego 12.</p>

                        <p><strong>II. Rodzaje usług</strong><br>
                        Usługodawca świadczy usługi: udostępnienia Kwestionariusza oceny potrzeb, generowania wyników oraz wysyłki Newslettera.</p>

                        <p><strong>III. Zasady korzystania</strong><br>
                        <ul>
                            <li>Korzystanie z Serwisu jest bezpłatne.</li>
                            <li>Użytkownik zobowiązany jest do korzystania z Serwisu zgodnie z prawem i dobrymi obyczajami.</li>
                            <li>Zakazane jest dostarczanie treści o charakterze bezprawnym.</li>
                        </ul></p>

                        <p><strong>IV. Własność intelektualna</strong><br>
                        Treści w Serwisie, w tym metodologia Kwestionariusza, są chronione prawem autorskim. Kopiowanie bez zgody jest zabronione.</p>

                        <p><strong>V. Wyłączenie odpowiedzialności</strong><br>
                        Wyniki generowane przez system mają charakter wyłącznie edukacyjny i poglądowy. Nie stanowią diagnozy psychologicznej ani doradztwa zawodowego. Usługodawca nie ponosi odpowiedzialności za decyzje podjęte na podstawie wyników.</p>

                        <p><strong>VI. Reklamacje</strong><br>
                        Reklamacje można składać drogą mailową na adres: bok@ineed.com. Termin rozpatrzenia wynosi 14 dni.</p>
                    "
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
                    EmailSentError = "Wystąpił błąd podczas wysyłania e-maila."
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