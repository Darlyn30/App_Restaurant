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
    public class FoodRepository : IFoodRepository
    {
        private readonly ApplicationContext _context;

        public FoodRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Food food)
        {
            _context.Foods.Add(food);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await GetByIdAsync(id);
            if (entity == null)
            {
                throw new Exception("Food not found");
            }

            _context.Foods.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Food>> GetAllAsync()
        {
            var foods = await _context.Foods
                .Include(f => f.Restaurant) // traemos la relacion con restaurant, podemos acceder a sus propiedades
                //debo usar viewmodels para asi evitar ciclos a la hora de buscar la entidad de food y restaurant, se queda en un loop infinito
                // Restaurant -> Food -> Restaurant -> Food -> Restaurant -> Food
                .ToListAsync();

            return foods;
        }

        public async Task<Food> GetByIdAsync(int id)
        {
            var food = await _context.Foods.FindAsync(id);
            if (food == null)
            {
                throw new Exception("Food not found");
            }

            return food;
        }

        public async Task<IEnumerable<Food>> GetByRestaurantIdAsync(int restaurantId)
        {
            var foodsByRestaurant = await _context.Foods
                .Where(f => f.RestaurantId == restaurantId && f.Active)
                .ToListAsync();

            return foodsByRestaurant;
        }

        public async Task UpdateAsync(Food food)
        {
            _context.Foods.Update(food);
            await _context.SaveChangesAsync();
        }
    }
}
