using Infrastructure.ServiceBus;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    /// <summary>
    /// Controller para testar publicação e consumo de mensagens no RabbitMQ.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class ServiceBusController : ControllerBase
    {
        private readonly IRabbitMqService _rabbitMqService;
        private static string _lastMessage = string.Empty;

        public ServiceBusController(IRabbitMqService rabbitMqService)
        {
            _rabbitMqService = rabbitMqService;
            // Inicia o consumidor apenas uma vez (para exemplo)
            _rabbitMqService.Subscribe("order-events", msg => _lastMessage = msg);
        }

        /// <summary>
        /// Publica uma mensagem de teste na fila "order-events".
        /// </summary>
        [HttpPost("publish")]
        public IActionResult Publish([FromBody] string message)
        {
            _rabbitMqService.Publish("order-events", message);
            return Ok(new { status = "Mensagem publicada", message });
        }

        /// <summary>
        /// Retorna a última mensagem consumida da fila "order-events".
        /// </summary>
        [HttpGet("last")] 
        public IActionResult GetLastMessage()
        {
            return Ok(new { lastMessage = _lastMessage });
        }
    }
} 