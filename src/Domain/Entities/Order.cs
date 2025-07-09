// Projeto desenvolvido por Alexsandro Nunes Lacerda
// www.alexsandronuneslacerda.com.br | Instagram: @alexsandronl | LinkedIn: @alexsandronuneslacerda

using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    /// <summary>
    /// Representa um Pedido no domínio. Agregado raiz.
    /// </summary>
    public class Order
    {
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<OrderItem> Items { get; private set; } = new();
        public decimal Total => CalcularTotal();

        // Construtor para EF/Core e Event Sourcing
        protected Order() { }

        public Order(Guid id, Guid customerId, DateTime createdAt)
        {
            Id = id;
            CustomerId = customerId;
            CreatedAt = createdAt;
        }

        public Order(Guid customerId)
        {
            Id = Guid.NewGuid();
            CustomerId = customerId;
            CreatedAt = DateTime.UtcNow;
        }

        /// <summary>
        /// Adiciona um item ao pedido.
        /// </summary>
        public void AddItem(Guid productId, int quantity, decimal unitPrice)
        {
            if (quantity <= 0) throw new ArgumentException("Quantidade deve ser maior que zero.");
            if (unitPrice <= 0) throw new ArgumentException("Preço unitário deve ser maior que zero.");
            Items.Add(new OrderItem(productId, quantity, unitPrice));
        }

        private decimal CalcularTotal()
        {
            decimal total = 0;
            foreach (var item in Items)
                total += item.Quantity * item.UnitPrice;
            return total;
        }
    }

    /// <summary>
    /// Item do pedido.
    /// </summary>
    public class OrderItem
    {
        public Guid ProductId { get; private set; }
        public int Quantity { get; private set; }
        public decimal UnitPrice { get; private set; }

        protected OrderItem() { }

        public OrderItem(Guid productId, int quantity, decimal unitPrice)
        {
            ProductId = productId;
            Quantity = quantity;
            UnitPrice = unitPrice;
        }
    }
} 