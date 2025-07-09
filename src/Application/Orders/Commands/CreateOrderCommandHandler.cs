// Projeto desenvolvido por Alexsandro Nunes Lacerda
// www.alexsandronuneslacerda.com.br | Instagram: @alexsandronl | LinkedIn: @alexsandronuneslacerda

using Domain.Entities;
using Domain.Events;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Orders.Commands
{
    /// <summary>
    /// Handler responsável por processar a criação de um pedido.
    /// </summary>
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, Guid>
    {
        // Aqui normalmente você teria um repositório e um event store injetados
        // Para simplificar, vamos simular a persistência e publicação de evento

        public async Task<Guid> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            // Cria o pedido
            var order = new Order(request.CustomerId);
            foreach (var item in request.Items)
            {
                order.AddItem(item.ProductId, item.Quantity, item.UnitPrice);
            }

            // Simula persistência (em produção, salve no banco e no event store)
            // await _orderRepository.AddAsync(order);
            // await _eventStore.AppendAsync(new OrderCreatedEvent(...));

            // Simula publicação de evento
            var orderCreated = new OrderCreatedEvent(order.Id, order.CustomerId, order.CreatedAt);
            // await _eventBus.PublishAsync(orderCreated);

            // Retorna o ID do pedido criado
            return await Task.FromResult(order.Id);
        }
    }
} 