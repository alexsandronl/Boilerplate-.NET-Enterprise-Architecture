using Infrastructure.Resilience;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Threading.Tasks;

namespace Api.Controllers
{
    /// <summary>
    /// Controller para testar resiliência HTTP com Polly.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class PollyDemoController : ControllerBase
    {
        private readonly HttpResilientClient _resilientClient;

        public PollyDemoController(HttpResilientClient resilientClient)
        {
            _resilientClient = resilientClient;
        }

        /// <summary>
        /// Faz uma requisição GET resiliente para o endpoint informado.
        /// </summary>
        [HttpGet("get")]
        public async Task<IActionResult> Get([FromQuery] string url)
        {
            var response = await _resilientClient.GetAsync(url);
            var content = await response.Content.ReadAsStringAsync();
            return Ok(new { status = response.StatusCode, content });
        }
    }
} 