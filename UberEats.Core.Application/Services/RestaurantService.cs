
using UberEats.Core.Application.Interfaces.Repositories;
using UberEats.Core.Application.Interfaces.Services;
using UberEats.Core.Application.ViewModels.Rstaurant;
using UberEats.Core.Domain.Entities;

namespace UberEats.Core.Application.Services
{
    public class RestaurantService : IRestaurantService
    {
        private readonly IRestaurantRepository _restaurantRepository;

        public RestaurantService(IRestaurantRepository restaurantRepository)
        {
            _restaurantRepository = restaurantRepository;
        }
        public async Task AddAsync(RestaurantViewModel entity)
        {
            Restaurant restaurant = new();
            restaurant.Name = entity.Name;
            restaurant.Description = entity.Description;
            restaurant.ImgUrl = entity.ImgUrl;
            restaurant.CategoryId = entity.CategoryId;
            restaurant.OpeningTime = entity.OpeningTime;
            restaurant.ClosingTime = entity.ClosingTime;

            await _restaurantRepository.AddAsync(restaurant);
        }

        public async Task DeleteAsync(int id)
        {
            await _restaurantRepository.DeleteAsync(id);
        }

        public async Task<List<RestaurantViewModel>> GetAllAsync()
        {
            var restaurants = await _restaurantRepository.GetAllAsync();
            var vmList = new List<RestaurantViewModel>();

            foreach (var restaurant in restaurants)
            {
                vmList.Add(new RestaurantViewModel
                {
                    Id = restaurant.Id,
                    Name = restaurant.Name,
                    Description = restaurant.Description,
                    ImgUrl = restaurant.ImgUrl,
                    CategoryId = restaurant.CategoryId,
                    OpeningTime = restaurant.OpeningTime,
                    ClosingTime = restaurant.ClosingTime,
                });
            }

            return vmList;
        }

        public async Task<RestaurantViewModel> GetByIdAsync(int id)
        {
            var restaurant = await _restaurantRepository.GetByIdAsync(id);

            if (restaurant == null)
                return null;

            var result = new RestaurantViewModel
            {
                Id = restaurant.Id,
                Name = restaurant.Name,
                Description = restaurant.Description,
                ImgUrl = restaurant.ImgUrl,
                CategoryId = restaurant.CategoryId,
                OpeningTime = restaurant.OpeningTime,
                ClosingTime = restaurant.ClosingTime,
            };

            return result;
        }

        public async Task UpdateAsync(RestaurantViewModel entity)
        {
            Restaurant restaurant = new();
            restaurant.Id = entity.Id;
            restaurant.Name = entity.Name;
            restaurant.Description = entity.Description;
            restaurant.ImgUrl = entity.ImgUrl;
            restaurant.CategoryId = entity.CategoryId;
            restaurant.OpeningTime = entity.OpeningTime;
            restaurant.ClosingTime = entity.ClosingTime;

            await _restaurantRepository.UpdateAsync(restaurant);

        }
    }
}
