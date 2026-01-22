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
        public string Home { get; set; }
        public string Privacy { get; set; }
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
        public string Woman { get; set; }
        public string Man { get; set; }
        public string Category { get; set; }
        public string ResultScore { get; set; }
        public string GenderSelectionTitle { get; set; }
        public string GenderSelectionInfo { get; set; }
        public string DontSpecify { get; set; }
        public string StenLow { get; set; }
        public string StenAverage { get; set; }
        public string StenHigh { get; set; }
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
        public string RetakeTest { get; set; }
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
        public string ResultsHeader { get; set; }
        public string ResultsSubHeader { get; set; }
        public string DeleteIdLabel { get; set; }
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
        public string Success { get; set; }
        public string Error { get; set; }
        public string ValidationRequired { get; set; }
        public string EmailSent { get; set; }
        public string DataDeleted { get; set; }
        public string RodoRequired { get; set; }
        public string NewMessage { get; set; }
        public string ContactSuccess { get; set; }
        public string ContactError { get; set; }
        public string FormIncomplete { get; set; }
        public string UnsubscribeSuccess { get; set; }
        public string EmailNotFound { get; set; }
        public string Wyniki { get; set; }
        public string EmailThanks { get; set; }
        public string GeneratedBy { get; set; }
        public string YourResults { get; set; }
        public string EmailSentSuccess { get; set; }
        public string EmailSentError { get; set; }
        public string CombinedResultsTitle { get; set; }
        public string ResultsForIdBody { get; set; }
        public string UnknownSurvey { get; set; }
        public string FillDate { get; set; }
        public string NoResultsFound { get; set; }
        public string EmailRequired { get; set; }
        public string AllResultsSentSuccess { get; set; }
        public string NoData { get; set; }
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
        public string NormsFemale { get; set; }   // NOWE
        public string NormsMale { get; set; }     // NOWE
        public string YourResultSten { get; set; }
    }

    public class HomeResources
    {
        public string Header { get; set; }
        public string SubHeader { get; set; }
    }
}