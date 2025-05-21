using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UberEats.Core.Domain.Entities;

namespace UberEats.Core.Application.Interfaces.Repositories
{
    public interface IRestaurantRepository
    {
        Task<List<Restaurant>> GetAllAsync();
        Task<Restaurant> GetByIdAsync(int id);
        Task AddAsync(Restaurant entity);
        Task UpdateAsync(Restaurant entity);
        Task DeleteAsync(int id);
    }
}
