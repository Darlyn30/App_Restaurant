using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UberEats.Core.Application.Interfaces.Services;
using UberEats.Core.Application.ViewModels.Cart;

namespace WebApi.UberEats.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetCartByUserId(int userId)
        {
            var cart = await _cartService.GetCartByUserId(userId);
            if (cart == null)
                return NotFound(new { Message = "Cart not found" });

            return Ok(cart);
        }

        [HttpGet("GetTotalPrice/{cartId}")]
        public async Task<decimal> GetTotalPrice(int cartId)
        {
            return await _cartService.GetTotalPrice(cartId);
        }

        [HttpPost("add-item")]
        public async Task<IActionResult> AddItemToCart(int userId, [FromBody] AddCartItemViewModel vm)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (vm == null)
                return BadRequest(new { Message = "Invalid cart item" });

            await _cartService.addItemToCartAsync(userId, vm);
            return Ok(new { Message = "Item added to cart" });
        }

        [HttpPost("user/createCart/{userId}")]
        public async Task<IActionResult> CreateCart(int userId)
        {
            var cartExist = await _cartService.GetCartByUserId(userId);
            if (cartExist == null)
            {
                await _cartService.CreateCart(userId);
                return Ok(new { Message = "Cart created" });
            }
            return BadRequest(new { Message = "the cart already exist" });


        }

        [HttpDelete("{userId}")]

        public async Task<IActionResult> ClearCart(int userId)
        {
            await _cartService.ClearCartAsync(userId);
            return Ok(new { Message = "Cart cleared" });
        }

        [HttpDelete("item/{itemId}")]
        public async Task<IActionResult> RemoveItemFromCart(int itemId)
        {
            if (itemId <= 0)
                return BadRequest(new { Message = "Invalid item ID" });

            await _cartService.removeItemFromCartAsync(itemId);
            return Ok(new { Message = "Item removed from cart" });
        }

        [HttpDelete("deleteAllItems/{userId}")]
        public async Task<IActionResult> DeleteAllItems(int userId)
        {
            if (userId <= 0)
                return BadRequest(new { Message = "Invalid user ID" });

            await _cartService.DeleteAllItems(userId);
            return Ok(new { Message = "All items deleted from cart" });
        }
    }
}
