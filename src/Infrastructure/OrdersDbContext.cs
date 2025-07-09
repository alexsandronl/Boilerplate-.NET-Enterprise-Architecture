// Projeto desenvolvido por Alexsandro Nunes Lacerda
// www.alexsandronuneslacerda.com.br | Instagram: @alexsandronl | LinkedIn: @alexsandronuneslacerda

using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure
{
    /// <summary>
    /// DbContext para pedidos. Responsável por mapear entidades para o banco.
    /// </summary>
    public class OrdersDbContext : DbContext
    {
        public OrdersDbContext(DbContextOptions<OrdersDbContext> options) : base(options) { }

        public DbSet<Order> Orders => Set<Order>();
        public DbSet<OrderItem> OrderItems => Set<OrderItem>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configuração básica do mapeamento
            modelBuilder.Entity<Order>().HasKey(o => o.Id);
            modelBuilder.Entity<Order>().OwnsMany(o => o.Items);
        }
    }
} 