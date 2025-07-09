using Application.Orders.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/v1/orders")]
    public class OrdersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OrdersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Cria um novo pedido.
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateOrderCommand command)
        {
            if (command == null || command.Items.Count == 0)
                return BadRequest("Pedido inválido ou sem itens.");

            var orderId = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetById), new { id = orderId }, new { id = orderId });
        }

        /// <summary>
        /// Consulta um pedido pelo ID.
        /// </summary>
        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        {
            // Exemplo: aqui você buscaria o pedido no banco
            // return Ok(order);
            return Ok(new { id });
        }
    }
} 