using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UberEats.Core.Application.Interfaces.Repositories;
using UberEats.Core.Domain.Entities;
using UberEats.Infrastructure.Persistence.Contexts;

namespace UberEats.Infrastructure.Persistence.Repositories
{
    public class OrderItemsRepository : IOrderItemRepository
    {
        private readonly ApplicationContext _context;

        public OrderItemsRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task AddItemsAsync(List<OrderItem> items)
        {
            await _context.OrderItems.AddRangeAsync(items);
            await _context.SaveChangesAsync();
        }
    }   
}
