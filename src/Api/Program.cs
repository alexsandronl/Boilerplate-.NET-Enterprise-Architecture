// Projeto desenvolvido por Alexsandro Nunes Lacerda
// www.alexsandronuneslacerda.com.br | Instagram: @alexsandronl | LinkedIn: @alexsandronuneslacerda

using Infrastructure;
using Infrastructure.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Api.Extensions;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Serilog;
using EventStore.Client;
using Microsoft.Extensions.Localization;
using System.Globalization;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Builder;

try
{
    Console.WriteLine("[Startup] Iniciando configuração do builder...");
    var builder = WebApplication.CreateBuilder(args);
    Console.WriteLine("[Startup] Builder criado com sucesso.");

    // Configuração do Serilog
    Log.Logger = new LoggerConfiguration()
        .Enrich.FromLogContext()
        .WriteTo.Console()
        .WriteTo.Elasticsearch(new Serilog.Sinks.Elasticsearch.ElasticsearchSinkOptions(new Uri(builder.Configuration["ElasticSearch:Uri"] ?? "http://localhost:9200"))
        {
            AutoRegisterTemplate = true,
            IndexFormat = "boilerplate-api-logs-{0:yyyy.MM}"
        })
        .CreateLogger();

    builder.Host.UseSerilog();

    // Configura o DbContext do EF Core
    builder.Services.AddDbContext<OrdersDbContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

    // Registra o repositório de pedidos
    builder.Services.AddScoped<OrderRepository>();

    // Configura o MediatR para CQRS
    builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<Application.Orders.Commands.CreateOrderCommand>());

    // Adiciona controllers e Swagger
    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
    // Health Checks
    builder.Services.AddHealthChecks();

    // Versionamento de API
    builder.Services.AddApiVersioning(options =>
    {
        options.DefaultApiVersion = new Microsoft.AspNetCore.Mvc.ApiVersion(1, 0);
        options.AssumeDefaultVersionWhenUnspecified = true;
        options.ReportApiVersions = true;
    });
    builder.Services.AddVersionedApiExplorer(options =>
    {
        options.GroupNameFormat = "'v'VVV";
        options.SubstituteApiVersionInUrl = true;
    });

    // Swagger customizado para versionamento
    builder.Services.AddSwaggerGen(options =>
    {
        options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "Boilerplate API", Version = "v1" });
        options.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
        {
            Description = "JWT Authorization header usando o esquema Bearer. Exemplo: 'Bearer {token}'",
            Name = "Authorization",
            In = Microsoft.OpenApi.Models.ParameterLocation.Header,
            Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
            Scheme = "bearer"
        });
        options.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement {
            {
                new Microsoft.OpenApi.Models.OpenApiSecurityScheme {
                    Reference = new Microsoft.OpenApi.Models.OpenApiReference {
                        Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                },
                new string[] {}
            }
        });
    });
    builder.Services.AddJwtAuthentication(builder.Configuration);
    builder.Services.AddAuthorization(options =>
    {
        options.AddPolicy("AdminOnly", policy => policy.RequireRole("Admin"));
    });
    builder.Services.AddApplicationInsightsTelemetry(builder.Configuration["ApplicationInsights:ConnectionString"]);

    builder.Services.AddSingleton(sp =>
    {
        var settings = EventStoreClientSettings.Create(builder.Configuration["EventStore:ConnectionString"] ?? "esdb://localhost:2113?tls=false");
        return new EventStore.Client.EventStoreClient(settings);
    });
    builder.Services.AddScoped<Infrastructure.EventSourcing.EventStoreService>();
    builder.Services.AddScoped<Infrastructure.Email.IEmailSender, Infrastructure.Email.EmailSender>();
    builder.Services.AddSingleton<Infrastructure.ServiceBus.IRabbitMqService>(sp =>
        new Infrastructure.ServiceBus.RabbitMqService(builder.Configuration["RabbitMQ:HostName"] ?? "localhost"));
    builder.Services.AddHttpClient<Infrastructure.Resilience.HttpResilientClient>();

    // Configuração de localização (pt-BR por padrão)
    var supportedCultures = new[] { new CultureInfo("pt-BR"), new CultureInfo("en-US") };
    builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");
    builder.Services.Configure<RequestLocalizationOptions>(options =>
    {
        options.DefaultRequestCulture = new Microsoft.AspNetCore.Localization.RequestCulture("pt-BR");
        options.SupportedCultures = supportedCultures;
        options.SupportedUICultures = supportedCultures;
    });

    var app = builder.Build();

    // Middleware de Swagger para documentação da API
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();
    // Middleware de autenticação (JWT)
    app.UseAuthentication();
    // Middleware de autorização (roles/policies)
    app.UseAuthorization();
    // Mapear controllers
    app.MapControllers();
    // Endpoint de health check
    app.MapHealthChecks("/health");

    // Ativa localização na pipeline
    var locOptions = app.Services.GetService<Microsoft.Extensions.Options.IOptions<RequestLocalizationOptions>>();
    app.UseRequestLocalization(locOptions?.Value!);

    // Executa migrations e seeds automáticos ao iniciar a aplicação
    // using (var scope = app.Services.CreateScope())
    // {
    //     var db = scope.ServiceProvider.GetRequiredService<Infrastructure.OrdersDbContext>();
    //     db.Database.Migrate();
    //     // Exemplo de seed: cria um pedido se não houver nenhum
    //     if (!db.Orders.Any())
    //     {
    //         db.Orders.Add(new Domain.Entities.Order(
    //             Guid.NewGuid(),
    //             Guid.NewGuid(), // CustomerId fictício
    //             DateTime.UtcNow
    //         ));
    //         db.SaveChanges();
    //     }
    // }

    app.Run();
}
catch (Exception ex)
{
    Console.WriteLine($"[Startup][ERRO] Exceção durante inicialização: {ex.Message}\n{ex.StackTrace}");
    throw;
}

public partial class Program { }
