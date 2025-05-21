using UberEats.Core.Application.Interfaces.Repositories;
using UberEats.Core.Application.Interfaces.Services;
using UberEats.Core.Application.ViewModels.Food;
using UberEats.Core.Domain.Entities;

namespace UberEats.Core.Application.Services
{
    public class FoodService : IFoodService
    {
        //TODO: voy a necesitar viemodels para el food por las navs props de EF
        private readonly IFoodRepository _foodRepository;

        public FoodService(IFoodRepository foodRepository)
        {
            _foodRepository = foodRepository;
        }

        public async Task AddFood(SaveFoodViewModel foodVm)
        {
            Food food = new();
            food.Id = foodVm.Id;
            food.Name = foodVm.Name;
            food.Description = foodVm.Description;
            food.Price = foodVm.Price;
            food.ImgUrl = foodVm.ImgUrl;
            food.RestaurantId = foodVm.RestaurantId;
            food.Active = foodVm.IsActive;

            

            await _foodRepository.AddAsync(food);
        }

        public async Task DeleteFood(int id)
        {
            await _foodRepository.DeleteAsync(id);
        }

        public async Task<List<FoodViewModel>> GetAllFoods()
        {
            var result = await _foodRepository.GetAllAsync();

            var vmList = result.Select(food => new FoodViewModel
            {
                Id = food.Id,
                Name = food.Name,
                Description = food.Description,
                Price = food.Price,
                ImgUrl = food.ImgUrl,
                RestaurantId = food.RestaurantId,
                IsActive = food.Active
            }).ToList();

            return vmList;
        }

        public async Task<List<FoodViewModel>> GetByRestaurantId(int restaurantId)
        {
            var foods = await _foodRepository.GetByRestaurantIdAsync(restaurantId);

            var foodsByRestaurant = foods.Select(food => new FoodViewModel
            {
                Id = food.Id,
                Name = food.Name,
                Description = food.Description,
                Price = food.Price,
                ImgUrl = food.ImgUrl,
                RestaurantId = food.RestaurantId,
                IsActive = food.Active
            }).ToList();

            return foodsByRestaurant;
        }

        public async Task<FoodViewModel> GetFoodById(int id)
        {
            var result = await _foodRepository.GetByIdAsync(id);
            if (result == null)
                return null;

            FoodViewModel food = new();
            food.Id = result.Id;
            food.Name = result.Name;
            food.Description = result.Description;
            food.Price = result.Price;
            food.ImgUrl = result.ImgUrl;
            food.RestaurantId = result.RestaurantId;
            food.IsActive = result.Active;

            return food;
        }

        public async Task UpdateFood(FoodViewModel foodVm)
        {
            Food food = new();
            food.Id = foodVm.Id;
            food.Name = foodVm.Name;
            food.Description = foodVm.Description;
            food.Price = foodVm.Price;
            food.ImgUrl = foodVm.ImgUrl;
            food.RestaurantId = foodVm.RestaurantId;
            food.Active = foodVm.IsActive;

            await _foodRepository.UpdateAsync(food);
        }
    }
}
