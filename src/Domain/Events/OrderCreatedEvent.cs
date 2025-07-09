// Projeto desenvolvido por Alexsandro Nunes Lacerda
// www.alexsandronuneslacerda.com.br | Instagram: @alexsandronl | LinkedIn: @alexsandronuneslacerda

using System;

namespace Domain.Events
{
    /// <summary>
    /// Evento de domínio disparado quando um pedido é criado.
    /// </summary>
    public class OrderCreatedEvent
    {
        public Guid OrderId { get; }
        public Guid CustomerId { get; }
        public DateTime CreatedAt { get; }

        public OrderCreatedEvent(Guid orderId, Guid customerId, DateTime createdAt)
        {
            OrderId = orderId;
            CustomerId = customerId;
            CreatedAt = createdAt;
        }
    }
} 