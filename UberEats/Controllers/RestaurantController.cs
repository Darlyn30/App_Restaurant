using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UberEats.Core.Application.Interfaces.Services;
using UberEats.Core.Application.ViewModels.Rstaurant;

namespace WebApi.UberEats.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RestaurantController : ControllerBase
    {
        private readonly IRestaurantService _restaurantService;

        public RestaurantController(IRestaurantService restaurantService)
        {
            _restaurantService = restaurantService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var restaurants = await _restaurantService.GetAllAsync();
            return Ok(restaurants);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var restaurant = await _restaurantService.GetByIdAsync(id);
            if (restaurant == null)
            {
                return NotFound(new { Message = $"Objeto con el id: {id} no encontrado" });
            }
            return Ok(restaurant);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] RestaurantViewModel restaurant)
        {
            if (restaurant == null)
            {
                return BadRequest(new { Message = "El objeto no puede ser nulo" });
            }
            await _restaurantService.AddAsync(restaurant);
            return CreatedAtAction(nameof(GetById), new { id = restaurant.Id }, restaurant);
        }

        [HttpPut("{id}")]

        public async Task<IActionResult> Update(int id, [FromBody] RestaurantViewModel restaurant)
        {
            if (restaurant == null || restaurant.Id != id)
            {
                return BadRequest(new { Message = "El objeto no puede ser nulo o el id no coincide" });
            }
            await _restaurantService.UpdateAsync(restaurant);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var restaurant = await _restaurantService.GetByIdAsync(id);
            if (restaurant == null)
                return NotFound(new { Message = $"Objeto con el id: {id} no encontrado" });
            

            await _restaurantService.DeleteAsync(id);
            return NoContent();
        }
    }
}
