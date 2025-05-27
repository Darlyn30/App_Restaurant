using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using UberEats.Core.Application.Interfaces.Repositories;
using UberEats.Core.Domain.Entities;
using UberEats.Infrastructure.Persistence.Contexts;

namespace UberEats.Infrastructure.Persistence.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationContext _context;
        public OrderRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task CreateOrderAsync(Order order)
        {
            var orderExist = await GetOrderByIdAsync(order.Id);
            if (orderExist != null)
                throw new Exception("Esta orden ya existe");

            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteOrderAsync(int id)
        {
            var order = await GetOrderByIdAsync(id);
            if (order == null)
                throw new Exception("Esta orden que busca, no existe");

            _context.Orders.Remove(order); //antes de salir de aqui, guardarla en el historial
            await _context.SaveChangesAsync();
        }

        public async Task<ICollection<Order>> GetAllOrdersAync()
        {
            var orders = await _context.Orders.ToListAsync();

            return orders;
        }

        public async Task<Order> GetOrderByIdAsync(int id)
        {
            var order = await _context.Orders
                .Include(c => c.Cart)
                .ThenInclude(ci => ci.CartItems)
                .Include(u => u.User)
                .Include(p => p.PaymentMethod)
                .FirstOrDefaultAsync(o => o.Id == id);

            if (order == null)
                throw new Exception("Este pedido no existe");

            return order;
        }
    }
}
