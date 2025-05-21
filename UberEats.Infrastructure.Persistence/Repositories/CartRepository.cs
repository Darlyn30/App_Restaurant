using Microsoft.EntityFrameworkCore;
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
    public class CartRepository : ICartRepository
    {
        private readonly ApplicationContext _context;

        public CartRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task AddCartAsync(Cart cart)
        {
            var cartExist = await GetCartByUserIdAsync(cart.UserId);

            if (cartExist != null)
                throw new Exception("Cart already exists for this user.");


            await _context.Carts.AddAsync(cart);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAllItemsAsync(int userId)
        {
            var cart = await _context.Carts
                .Include(c => c.CartItems)
                .FirstOrDefaultAsync(c => c.UserId == userId);

            if (cart == null)
                throw new Exception("Carrito no encontrado para el usuario.");

            if (cart.CartItems == null || !cart.CartItems.Any())
                throw new Exception("El carrito ya está vacío.");

            // Borrar todos los items
            _context.CartItems.RemoveRange(cart.CartItems);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteCartAsync(int userId)
        {
            //la logica es buscar el carrito por el userId y eliminarlo
            var userCart = await GetCartByUserIdAsync(userId);

            if(userCart == null)
                throw new Exception("Cart not found for this user.");

            _context.Carts.Remove(userCart);
            await _context.SaveChangesAsync();
        }

        public async Task<Cart> GetCartByUserIdAsync(int userId)
        {
            var cart = await _context.Carts
                .Include(c => c.CartItems)
                .ThenInclude(ci => ci.Food)
                .FirstOrDefaultAsync(c => c.UserId == userId);

            return cart;
        }

    }
}
