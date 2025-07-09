// Projeto desenvolvido por Alexsandro Nunes Lacerda
// www.alexsandronuneslacerda.com.br | Instagram: @alexsandronl | LinkedIn: @alexsandronuneslacerda

using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Infrastructure;

namespace Integration
{
    /// <summary>
    /// Customização do WebApplicationFactory para garantir ambiente de teste igual ao de produção.
    /// </summary>
    public class CustomWebApplicationFactory<TProgram> : WebApplicationFactory<TProgram> where TProgram : class
    {
        protected override IHost CreateHost(IHostBuilder builder)
        {
            // Garante que o ambiente seja Development para Swagger e versionamento
            builder.UseEnvironment("Development");
            builder.ConfigureServices(services =>
            {
                // Remove o DbContext do SQL Server
                var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<OrdersDbContext>));
                if (descriptor != null)
                    services.Remove(descriptor);

                // Adiciona o DbContext in-memory
                services.AddDbContext<OrdersDbContext>(options =>
                {
                    options.UseSqlite("DataSource=:memory:");
                });

                // Força o carregamento dos controllers do assembly da API
                var provider = services.BuildServiceProvider();
                var loggerFactory = provider.GetRequiredService<ILoggerFactory>();
                loggerFactory.CreateLogger("TestStartup").LogInformation("Registrando controllers explicitamente no ambiente de teste.");

                services.AddControllers().AddApplicationPart(typeof(Api.Controllers.OrdersController).Assembly);
            });
            return base.CreateHost(builder);
        }
    }
} 