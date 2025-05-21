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
    public class CartItemRepository : ICartItemRepository
    {
        private readonly ApplicationContext _context;
        public CartItemRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task AddItemAsync(CartItem item)
        {
            await _context.CartItems.AddAsync(item);
            await _context.SaveChangesAsync();
        }

        public async Task<decimal> GetTotalPriceAsync(int cartId)
        {
            var cartItems = _context.CartItems
                .Where(x => x.CartId == cartId)
                .Select(x => x.Price * x.Quantity);

            return await Task.FromResult(cartItems.Sum());
        }

        public async Task RemoveItemAsync(int itemId)
        {
            var item = _context.CartItems.FirstOrDefault(x => x.Id == itemId);

            if (item == null)
                throw new Exception("Item not found in cart.");

            _context.CartItems.Remove(item);
            await _context.SaveChangesAsync();
        }

        //public Task UpdateItemAsync(CartItem item)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
