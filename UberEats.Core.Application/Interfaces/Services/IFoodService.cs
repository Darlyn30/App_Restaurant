using UberEats.Core.Application.ViewModels.Food;
using UberEats.Core.Domain.Entities;

namespace UberEats.Core.Application.Interfaces.Services
{
    public interface IFoodService
    {
        Task<List<FoodViewModel>> GetAllFoods();
        Task<FoodViewModel> GetFoodById(int id);
        Task AddFood(SaveFoodViewModel foodVm);
        Task UpdateFood(FoodViewModel foodVm);
        Task DeleteFood(int id);
        Task<List<FoodViewModel>> GetByRestaurantId(int restaurantId);
    }
}
