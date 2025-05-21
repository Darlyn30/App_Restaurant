using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UberEats.Core.Application.ViewModels.Cart;
using UberEats.Core.Domain.Entities;

namespace UberEats.Core.Application.Interfaces.Services
{
    public interface ICartService
    {
        Task<CartViewModel> GetCartByUserId(int userId);
        Task CreateCart(int userId);
        Task addItemToCartAsync(int userId, AddCartItemViewModel vm);
        Task removeItemFromCartAsync(int itemId);
        Task ClearCartAsync(int userId);
        Task DeleteAllItems(int cartId);
        Task<decimal> GetTotalPrice(int cartId);
    }
}
