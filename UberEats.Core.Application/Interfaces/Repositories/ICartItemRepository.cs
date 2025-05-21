using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UberEats.Core.Domain.Entities;

namespace UberEats.Core.Application.Interfaces.Repositories
{
    public interface ICartItemRepository
    {
        Task AddItemAsync(CartItem item);
        Task RemoveItemAsync(int item);
        //Task UpdateItemAsync(CartItem item);
        Task<decimal> GetTotalPriceAsync(int cartId);
    }
}
