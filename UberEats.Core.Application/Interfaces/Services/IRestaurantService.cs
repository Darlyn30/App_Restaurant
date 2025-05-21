using UberEats.Core.Application.ViewModels.Rstaurant;

namespace UberEats.Core.Application.Interfaces.Services
{
    public interface IRestaurantService
    {
        Task<List<RestaurantViewModel>> GetAllAsync();
        Task<RestaurantViewModel> GetByIdAsync(int id);
        Task AddAsync(RestaurantViewModel entity);
        Task UpdateAsync(RestaurantViewModel entity);
        Task DeleteAsync(int id);
    }
}