namespace INeed.Helpers
{
    public static class AppConstantsEN
    {
        public static TextResources Get()
        {
            return new TextResources
            {
                CompanyName = "INeed",
                Slogan = "Because we are characterized by professionalism",

                Layout = new LayoutResources
                {
                    // NOWE WARTOŚCI
                    Home = "Home",
                    Privacy = "Privacy",

                    Questionnaires = "Questionnaires",
                    Contact = "Contact",
                    PrivacyPolicy = "Privacy Policy",
                    Terms = "Terms of Service",
                    CookieHeader = "Cookies",
                    CookieBody = "We use cookies to ensure best experience.",
                    CookieButton = "Accept"
                },

                Labels = new LabelResources
                {
                    YourEmailPlaceholder = "Your email address",
                    MessagePlaceholder = "Type your message here...",
                    EmailPlaceholder = "Email address",
                    FirstName = "First Name"
                },

                Buttons = new ButtonResources
                {
                    SendMessage = "Send Message",
                    Delete = "Delete my data",
                    Back = "Go Back",
                    BackToHome = "Back to Home",
                    Continue = "Next",
                    SendAnswers = "Finish and see results",
                    SendResults = "Send results via email",
                    Start = "Start"
                },

                PolicyContent = new PolicyContentResources
                {
                    PrivacyAndCookies = @"
                        <h4 class='fw-bold'>PRIVACY POLICY INeed sp. z o.o.</h4>
                        <p><strong>1. General Information</strong><br>
                        The Data Controller is: <strong>INeed sp. z o.o.</strong><br>
                        Registered office: Al. Kilińskiego 12, 09-402 Płock, Poland<br>
                        NIP: 123-456-78-90 | KRS: 0001234567 | REGON: 123456789<br>
                        E-mail: bok@ineed.com | Tel.: 661 121 122<br>
                        This Privacy Policy sets out the rules for the processing of Personal Data by INeed sp. z o.o. in accordance with the Regulation of the European Parliament and of the Council (EU) 2016/679 of 27 April 2016 (GDPR).</p>
                        <p><strong>2. Purposes and legal basis of processing</strong><br>
                        Data is processed for the purpose of:
                        <ul>
                            <li>Providing services electronically (e.g., sending survey results) – Art. 6(1)(b) GDPR.</li>
                            <li>Communication with Users (responding to inquiries) – Art. 6(1)(f) GDPR.</li>
                            <li>Marketing (Newsletter) – Art. 6(1)(a) GDPR (consent).</li>
                            <li>Analytics and service security – Art. 6(1)(f) GDPR.</li>
                        </ul></p>
                        <p><strong>3. Data Recipients</strong><br>
                        Data recipients may be entities providing services to the Controller (e.g., hosting, IT support) based on data processing agreements.</p>
                        <p><strong>4. Rights of Data Subjects</strong><br>
                        You have the right to access your data, rectify it, delete it, restrict processing, transfer it, object, and withdraw consent at any time. To exercise your rights, please contact: bok@ineed.com.</p>
                        <p><strong>5. Profiling</strong><br>
                        Survey data is subject to automated analysis to generate a result. This does not produce legal effects concerning the User.</p>
                        <hr class='my-4'>
                        <h4 class='fw-bold'>COOKIES POLICY</h4>
                        <p><strong>1. Preliminary Information</strong><br>
                        The ineed.com website uses cookies to ensure proper operation, analytics, and marketing.</p>
                        <p><strong>2. Types of Cookies</strong><br>
                        <ul>
                            <li><strong>Necessary:</strong> Required for the survey to work (saving progress).</li>
                            <li><strong>Analytical:</strong> Anonymous statistics (e.g., Google Analytics) helping to improve the service.</li>
                        </ul></p>
                        <p><strong>3. Management</strong><br>
                        The User can change cookie settings in their web browser or via the cookie banner on the site at any time. Disabling necessary cookies may prevent the completion of the survey.<br>
                        <strong>Contact:</strong> regarding cookies, please contact: bok@ineed.com.</p>
                    ",
                    Terms = @"
                        <h4 class='fw-bold'>TERMS OF SERVICE INeed</h4>
                        <p><strong>I. General Provisions</strong><br>
                        These Terms of Service specify the rules for using the website available at ineed.com, operated by INeed sp. z o.o. with its registered office in Płock, Al. Kilińskiego 12.</p>
                        <p><strong>II. Types of Services</strong><br>
                        The Service Provider provides the following services: making the Needs Assessment Questionnaire available, generating results, and sending the Newsletter.</p>
                        <p><strong>III. Rules of Use</strong><br>
                        <ul>
                            <li>Using the Website is free of charge.</li>
                            <li>The User is obliged to use the Website in accordance with the law and good manners.</li>
                            <li>It is forbidden to provide unlawful content.</li>
                        </ul></p>
                        <p><strong>IV. Intellectual Property</strong><br>
                        The content on the Website, including the Questionnaire methodology, is protected by copyright. Copying without permission is prohibited.</p>
                        <p><strong>V. Disclaimer</strong><br>
                        The results generated by the system are for educational and illustrative purposes only. They do not constitute a psychological diagnosis or career counseling. The Service Provider is not responsible for decisions made based on the results.</p>
                        <p><strong>VI. Complaints</strong><br>
                        Complaints can be submitted via email to: bok@ineed.com. The consideration period is 14 days.</p>
                    "
                },

                Contact = new ContactResources
                {
                    PageTitle = "Contact Us",
                    Header = "We are here for you",
                    SubHeader = "Any questions? Write to us.",
                    OfficeName = "Customer Support",
                    Address = "al. Kilińskiego 12 p. 8 Płock, Poland",
                    Phone = "+48 123 456 789",
                    Email = "bok@ineed.com",
                    FormContactHeader = "Contact Form",
                    RodoConsent = "I consent to data processing.",
                    FormUnsubscribeHeader = "I want to delete my data",
                    ModalTitle = "Are you sure you want to delete your data?"
                },

                Fill = new FillResources
                {
                    PageTitle = "Questionnaire",
                    Step = "Question",
                    Of = "of",
                    SelectOption = "Select an option:",
                    Page = "Page",
                    NoQuestions = "No questions available.",
                    AlertPage = "Please answer all questions on this page.",
                    AlertAll = "Please answer all questions before sending."
                },

                Messages = new MessageResources
                {
                    Success = "Operation successful.",
                    Error = "An error occurred. Please try again.",
                    ValidationRequired = "This field is required.",
                    EmailSent = "Message has been sent.",
                    DataDeleted = "Your data has been deleted.",
                    RodoRequired = "You must consent to data processing (GDPR).",
                    NewMessage = "New message from user:",
                    ContactSuccess = "Your message has been sent successfully.",
                    ContactError = "Failed to send message. Please try again.",
                    FormIncomplete = "The form is incomplete.",
                    UnsubscribeSuccess = "Your email address has been removed from our database.",
                    EmailNotFound = "The provided email address was not found.",
                    Wyniki = "Your Results",
                    EmailThanks = "Thank you for completing the questionnaire. Below are your results.",
                    GeneratedBy = "This message was automatically generated by the INeed system.",
                    YourResults = "Your results:",
                    EmailSentSuccess = "The results have been sent to the provided email address.",
                    EmailSentError = "An error occurred while sending the email."
                },

                Result = new ResultResources
                {
                    PageTitle = "Your Result",
                    Header = "Your Result",
                    SubHeader = "Below you will find a detailed analysis of your answers.",
                    SaveHeader = "Save your result",
                    SaveSubHeader = "Enter your email address to receive a permanent copy of the results and interpretation.",
                    RodoConsent = "I accept the",
                    And = "and",
                    AcceptTerms = ", and I agree to data processing for sending the result."
                },

                Home = new HomeResources
                {
                    Header = "Available Questionnaires",
                    SubHeader = "Select a questionnaire from the list below to start the assessment process."
                }
            };
        }
    }
}