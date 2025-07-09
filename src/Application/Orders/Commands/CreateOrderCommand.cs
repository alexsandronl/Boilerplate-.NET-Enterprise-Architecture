// Projeto desenvolvido por Alexsandro Nunes Lacerda
// www.alexsandronuneslacerda.com.br | Instagram: @alexsandronl | LinkedIn: @alexsandronuneslacerda

using MediatR;
using System;
using System.Collections.Generic;

namespace Application.Orders.Commands
{
    /// <summary>
    /// Comando para criar um novo pedido.
    /// </summary>
    public class CreateOrderCommand : IRequest<Guid>
    {
        public Guid CustomerId { get; set; }
        public List<CreateOrderItemDto> Items { get; set; } = new();
    }

    /// <summary>
    /// DTO para item do pedido no comando de criação.
    /// </summary>
    public class CreateOrderItemDto
    {
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }
} 