using UberEats.Core.Application.Interfaces.Repositories;
using UberEats.Core.Application.Interfaces.Services;
using UberEats.Core.Application.ViewModels.Cart;
using UberEats.Core.Domain.Entities;

namespace UberEats.Core.Application.Services
{
    public class CartService : ICartService
    {
        private readonly ICartRepository _cartRepository;
        private readonly ICartItemRepository _cartItemRepository;
        private readonly IFoodRepository _foodRepository;

        public CartService(ICartRepository cartRepository, ICartItemRepository cartItemRepository, IFoodRepository foodRepository)
        {
            _cartItemRepository = cartItemRepository;
            _cartRepository = cartRepository;
            _foodRepository = foodRepository;
        }

        public async Task addItemToCartAsync(int userId, AddCartItemViewModel vm)
        {
            var cart = await GetCartByUserId(userId);

            if (cart == null)
                throw new Exception("Cart not found for this user.");

            var food = await _foodRepository.GetByIdAsync(vm.FoodId);
            if (food == null)
                throw new Exception("Food not found.");

            CartItem cartItem = new();
            cartItem.CartId = cart.Id;
            cartItem.FoodId = vm.FoodId;
            cartItem.Quantity = vm.Quantity;
            cartItem.Price = food.Price;
            cartItem.ImgUrl = food.ImgUrl ?? "";

            await _cartItemRepository.AddItemAsync(cartItem);

        }

        public async Task ClearCartAsync(int userId)
        {
            await _cartRepository.DeleteCartAsync(userId);
        }

        public async Task CreateCart(int userId)
        {
            var cart = await GetCartByUserId(userId);

            if(cart == null)
            {
                // Create a new cart if it doesn't exist
                Cart newCart = new();
                newCart.UserId = userId;
                newCart.CreationAt = DateTime.Now;
                await _cartRepository.AddCartAsync(newCart);
                return;
            }

            throw new Exception("Cart already exists for this user.");
        }

        public async Task DeleteAllItems(int cartId)
        {
            await _cartRepository.DeleteAllItemsAsync(cartId);
        }

        public async Task<CartViewModel> GetCartByUserId(int userId)
        {
            CartItemViewModel cartItemVm = new();

            var cart = await _cartRepository.GetCartByUserIdAsync(userId);

            if (cart == null)
                return null;

            CartViewModel cartVm = new();

            cartVm.Id = cart.Id;
            cartVm.UserId = cart.UserId;
            cartVm.CreationAt = cart.CreationAt;
            cartVm.Items = cart.CartItems.Select(item => new CartItemViewModel
            {
                ItemId = item.Id,
                CartId = item.CartId,
                FoodId = item.Food.Id,
                FoodName = item.Food.Name ?? "",
                Quantity = item.Quantity,
                Price = item.Price,
                ImgUrl = item.Food.ImgUrl ?? ""
            }).ToList();

            return cartVm;
        }

        public async Task<decimal> GetTotalPrice(int cartId)
        {
            var totalCart = await _cartItemRepository.GetTotalPriceAsync(cartId);
            return totalCart;

        }

        public async Task removeItemFromCartAsync(int itemId)
        {
            await _cartItemRepository.RemoveItemAsync(itemId);
        }
    }
}
