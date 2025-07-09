// Projeto desenvolvido por Alexsandro Nunes Lacerda
// www.alexsandronuneslacerda.com.br | Instagram: @alexsandronl | LinkedIn: @alexsandronuneslacerda

using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Xunit;
using Microsoft.AspNetCore.Mvc.Testing;

namespace Integration
{
    /// <summary>
    /// Teste de integração para o endpoint de criação de pedidos.
    /// </summary>
    public class OrderApiIntegrationTests : IClassFixture<CustomWebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;

        public OrderApiIntegrationTests(CustomWebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task Deve_Criar_Pedido_Via_Api()
        {
            // Arrange
            var request = new {
                CustomerId = System.Guid.NewGuid(),
                Items = new[] {
                    new {
                        ProductId = System.Guid.NewGuid(),
                        Quantity = 1,
                        UnitPrice = 10.0m
                    }
                }
            };

            // Act
            var response = await _client.PostAsJsonAsync("/api/v1/orders", request);

            // Assert
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            Assert.Contains("id", content.ToLower());
        }

        [Fact]
        public async Task HealthCheck_Deve_Retornar_OK()
        {
            var response = await _client.GetAsync("/health");
            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task Ping_Deve_Retornar_Pong()
        {
            var response = await _client.GetAsync("/ping");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            Assert.Equal("\"pong\"", content);
        }
    }
} 