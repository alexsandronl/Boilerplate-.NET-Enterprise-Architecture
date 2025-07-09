// Projeto desenvolvido por Alexsandro Nunes Lacerda
// www.alexsandronuneslacerda.com.br | Instagram: @alexsandronl | LinkedIn: @alexsandronuneslacerda

using Infrastructure.Email;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Api.Controllers
{
    /// <summary>
    /// Controller para envio de e-mails de teste.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class EmailController : ControllerBase
    {
        private readonly IEmailSender _emailSender;

        public EmailController(IEmailSender emailSender)
        {
            _emailSender = emailSender;
        }

        /// <summary>
        /// Envia um e-mail de teste.
        /// </summary>
        [HttpPost("send")]
        public async Task<IActionResult> SendEmail([FromBody] EmailRequest request)
        {
            await _emailSender.SendEmailAsync(request.To, request.Subject, request.Body);
            return Ok(new { status = "E-mail enviado", to = request.To });
        }
    }

    /// <summary>
    /// Modelo para requisição de envio de e-mail.
    /// </summary>
    public class EmailRequest
    {
        public string To { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
    }
} 