using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace Api.Controllers
{
    /// <summary>
    /// Controller para demonstrar localização (multilíngue).
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class LocalizationDemoController : ControllerBase
    {
        private readonly IStringLocalizer<LocalizationDemoController> _localizer;

        public LocalizationDemoController(IStringLocalizer<LocalizationDemoController> localizer)
        {
            _localizer = localizer;
        }

        /// <summary>
        /// Retorna uma mensagem localizada conforme o idioma da requisição.
        /// </summary>
        [HttpGet("hello")]
        public IActionResult Hello()
        {
            var msg = _localizer["HelloMessage"];
            return Ok(new { message = msg });
        }
    }
} 