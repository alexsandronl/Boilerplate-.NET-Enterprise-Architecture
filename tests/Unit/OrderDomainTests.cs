// Projeto desenvolvido por Alexsandro Nunes Lacerda
// www.alexsandronuneslacerda.com.br | Instagram: @alexsandronl | LinkedIn: @alexsandronuneslacerda

using Domain.Entities;
using System;
using Xunit;

namespace Unit
{
    /// <summary>
    /// Testes unitários para a entidade Order.
    /// </summary>
    public class OrderDomainTests
    {
        [Fact]
        public void Deve_Criar_Order_Com_Propriedades_Corretas()
        {
            // Arrange
            var customerId = Guid.NewGuid();

            // Act
            var order = new Order(customerId);

            // Assert
            Assert.NotEqual(Guid.Empty, order.Id);
            Assert.Equal(customerId, order.CustomerId);
            Assert.True((DateTime.UtcNow - order.CreatedAt).TotalSeconds < 5);
        }
    }
} 