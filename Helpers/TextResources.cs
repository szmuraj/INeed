namespace INeed.Helpers
{
    public class TextResources
    {
        public string CompanyName { get; set; }
        public string Slogan { get; set; }

        public LayoutResources Layout { get; set; }
        public LabelResources Labels { get; set; }
        public ButtonResources Buttons { get; set; }
        public PolicyContentResources PolicyContent { get; set; }
        public ContactResources Contact { get; set; }
        public FillResources Fill { get; set; }
        public MessageResources Messages { get; set; }
        public ResultResources Result { get; set; }
        public HomeResources Home { get; set; }
    }

    public class LayoutResources
    {
        public string Questionnaires { get; set; }
        public string Contact { get; set; }
        public string PrivacyPolicy { get; set; }
        public string Terms { get; set; }
        public string CookieHeader { get; set; }
        public string CookieBody { get; set; }
        public string CookieButton { get; set; }
    }

    public class LabelResources
    {
        public string YourEmailPlaceholder { get; set; }
        public string MessagePlaceholder { get; set; }
        public string EmailPlaceholder { get; set; }
        public string FirstName { get; set; }
    }

    public class ButtonResources
    {
        public string SendMessage { get; set; }
        public string Delete { get; set; }
        public string Back { get; set; }
        public string BackToHome { get; set; }
        public string Continue { get; set; }
        public string SendAnswers { get; set; }
        public string SendResults { get; set; }
        public string Start { get; set; }
    }

    public class PolicyContentResources
    {
        public string PrivacyAndCookies { get; set; }
        public string Terms { get; set; }
    }

    public class ContactResources
    {
        public string PageTitle { get; set; }
        public string Header { get; set; }
        public string SubHeader { get; set; }
        public string OfficeName { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string FormContactHeader { get; set; }
        public string RodoConsent { get; set; }
        public string FormUnsubscribeHeader { get; set; }
        public string ModalTitle { get; set; }
    }

    public class FillResources
    {
        public string PageTitle { get; set; }
        public string Step { get; set; }
        public string Of { get; set; }
        public string SelectOption { get; set; }
        public string Page { get; set; }
        public string NoQuestions { get; set; }
        public string AlertPage { get; set; }
        public string AlertAll { get; set; }
    }

    public class MessageResources
    {
        // Ogólne
        public string Success { get; set; }
        public string Error { get; set; }
        public string ValidationRequired { get; set; }
        public string EmailSent { get; set; }
        public string DataDeleted { get; set; }

        // ContactController
        public string RodoRequired { get; set; }
        public string NewMessage { get; set; }
        public string ContactSuccess { get; set; }
        public string ContactError { get; set; }
        public string FormIncomplete { get; set; }
        public string UnsubscribeSuccess { get; set; }
        public string EmailNotFound { get; set; }

        // --- BRAKUJĄCE POLA DLA QUESTIONNAIRE CONTROLLER ---
        public string Wyniki { get; set; } // Wyniki
        public string EmailThanks { get; set; } // Podziękowanie w mailu
        public string GeneratedBy { get; set; } // Stopka maila
        public string YourResults { get; set; } // Temat maila
        public string EmailSentSuccess { get; set; } // Komunikat sukcesu po wysłaniu
        public string EmailSentError { get; set; } // Komunikat błędu
    }

    public class ResultResources
    {
        public string PageTitle { get; set; }
        public string Header { get; set; }
        public string SubHeader { get; set; }
        public string SaveHeader { get; set; }
        public string SaveSubHeader { get; set; }
        public string RodoConsent { get; set; }
        public string And { get; set; }
        public string AcceptTerms { get; set; }
    }

    public class HomeResources
    {
        public string Header { get; set; }
        public string SubHeader { get; set; }
    }
}