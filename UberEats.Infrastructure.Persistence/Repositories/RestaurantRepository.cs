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
    public class RestaurantRepository : IRestaurantRepository
    {
        private readonly ApplicationContext _context;

        public RestaurantRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Restaurant entity)
        {
            _context.Restaurants.Add(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var result = await GetByIdAsync(id);

            if (result != null)
            {
                _context.Restaurants.Remove(result);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<Restaurant>> GetAllAsync()
        {
            var result = await _context.Restaurants.ToListAsync();
            return result;
        }

        public async Task<Restaurant> GetByIdAsync(int id)
        {
            var restaurant = await _context.Restaurants.FindAsync(id);
            return restaurant;
        }

        public async Task UpdateAsync(Restaurant entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
