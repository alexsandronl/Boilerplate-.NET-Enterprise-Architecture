// Projeto desenvolvido por Alexsandro Nunes Lacerda
// www.alexsandronuneslacerda.com.br | Instagram: @alexsandronl | LinkedIn: @alexsandronuneslacerda

using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    /// <summary>
    /// Repositório de pedidos usando EF Core.
    /// </summary>
    public class OrderRepository
    {
        private readonly OrdersDbContext _context;

        public OrderRepository(OrdersDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Adiciona um novo pedido ao banco de dados.
        /// </summary>
        public async Task AddAsync(Order order)
        {
            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Busca um pedido pelo ID.
        /// </summary>
        public async Task<Order?> GetByIdAsync(Guid id)
        {
            return await _context.Orders.Include(o => o.Items).FirstOrDefaultAsync(o => o.Id == id);
        }
    }
} 