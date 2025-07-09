// Projeto desenvolvido por Alexsandro Nunes Lacerda
// www.alexsandronuneslacerda.com.br | Instagram: @alexsandronl | LinkedIn: @alexsandronuneslacerda

using MailKit.Net.Smtp;
using MimeKit;
using System.Threading.Tasks;

namespace Infrastructure.Email
{
    /// <summary>
    /// Serviço para envio de e-mails usando SMTP (MailKit).
    /// </summary>
    public class EmailSender : IEmailSender
    {
        private readonly string _smtpServer = "smtp.example.com"; // Configurar via appsettings
        private readonly int _smtpPort = 587;
        private readonly string _smtpUser = "user@example.com";
        private readonly string _smtpPass = "password";
        private readonly string _from = "no-reply@example.com";

        public async Task SendEmailAsync(string to, string subject, string body)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Boilerplate", _from));
            message.To.Add(new MailboxAddress(to, to));
            message.Subject = subject;
            message.Body = new TextPart("plain") { Text = body };

            using var client = new SmtpClient();
            await client.ConnectAsync(_smtpServer, _smtpPort, false);
            await client.AuthenticateAsync(_smtpUser, _smtpPass);
            await client.SendAsync(message);
            await client.DisconnectAsync(true);
        }
    }
} 