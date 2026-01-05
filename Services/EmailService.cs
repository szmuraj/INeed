using System.Net;
using System.Net.Mail;

namespace INeed.Services
{
    public interface IEmailService
    {
        Task SendEmailAsync(string toEmail, string subject, string body);
    }

    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendEmailAsync(string toEmail, string subject, string body)
        {
            var settings = _configuration.GetSection("EmailSettings");

            // Używamy 'using', aby SmtpClient został poprawnie zamknięty po wysłaniu
            using (var smtpClient = new SmtpClient(settings["Host"]))
            {
                smtpClient.Port = int.Parse(settings["Port"]);
                smtpClient.Credentials = new NetworkCredential(settings["Username"], settings["Password"]);
                smtpClient.EnableSsl = true;

                var mailMessage = new MailMessage
                {
                    From = new MailAddress(settings["SenderEmail"], settings["SenderName"]),
                    Subject = subject,
                    Body = body,
                    IsBodyHtml = true,
                };

                mailMessage.To.Add(toEmail);

                try
                {
                    await smtpClient.SendMailAsync(mailMessage);
                }
                catch (Exception ex)
                {
                    // Tutaj warto dodać logowanie błędów, jeśli mail nie wyjdzie
                    Console.WriteLine($"Błąd wysyłki e-mail: {ex.Message}");
                    throw; // Rzucamy dalej, by kontroler wiedział o błędzie
                }
            }
        }
    }
}