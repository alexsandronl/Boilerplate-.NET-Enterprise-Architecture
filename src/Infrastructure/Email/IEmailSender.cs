// Projeto desenvolvido por Alexsandro Nunes Lacerda
// www.alexsandronuneslacerda.com.br | Instagram: @alexsandronl | LinkedIn: @alexsandronuneslacerda

using System.Threading.Tasks;

namespace Infrastructure.Email
{
    /// <summary>
    /// Abstração para envio de e-mails.
    /// </summary>
    public interface IEmailSender
    {
        Task SendEmailAsync(string to, string subject, string body);
    }
} 