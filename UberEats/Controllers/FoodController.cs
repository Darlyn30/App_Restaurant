using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UberEats.Core.Application.Interfaces.Services;
using UberEats.Core.Application.ViewModels.Food;

namespace WebApi.UberEats.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FoodController : ControllerBase
    {
        private readonly IFoodService _foodService;
        public FoodController(IFoodService _foodService)
        {
            this._foodService = _foodService;
        }

        [HttpGet]

        public async Task<IActionResult> GetAll()
        {
            var result = await _foodService.GetAllFoods();
            return Ok(result);
        }

        [HttpGet("{id}")]

        public async Task<IActionResult> GetById(int id)
        {
            var result = await _foodService.GetFoodById(id);
            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpGet("restaurant/{restaurantId}")]
        public async Task<IActionResult> GetByRestaurantId(int restaurantId)
        {
            var result = await _foodService.GetByRestaurantId(restaurantId);
            if (result == null || !result.Any())
                return NotFound(new {Message = "No hay comidas disponibles para este restaurante"});

            return Ok(result);
        }

        [HttpPost]

        public async Task<IActionResult> Add([FromBody] SaveFoodViewModel foodVm)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if(foodVm == null)
                return BadRequest(new { Message = "El objeto no puede ser nulo" });

            await _foodService.AddFood(foodVm);
            return CreatedAtAction(nameof(GetById), new { id = foodVm.Id }, foodVm);
        }

        [HttpPut("update")]

        public async Task<IActionResult> Update([FromBody] FoodViewModel foodVm)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var food = await _foodService.GetFoodById(foodVm.Id);
                if (food == null)
                    return NotFound(new {Message = "Objeto no encontrado"});

                await _foodService.UpdateFood(foodVm);
                return Ok(new {Message = $"Comida con el id: {foodVm.Id} Actualizado"});
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            
        }

        [HttpDelete("{id}")]

        public async Task<IActionResult> Delete(int id)
        {
            var food = await _foodService.GetFoodById(id);

            if (food == null)
                return NotFound();

            await _foodService.DeleteFood(id);
            return NoContent();
        }
    }
}
