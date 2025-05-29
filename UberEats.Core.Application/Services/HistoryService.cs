using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UberEats.Core.Application.Interfaces.Repositories;
using UberEats.Core.Application.Interfaces.Services;
using UberEats.Core.Application.ViewModels.Cart;
using UberEats.Core.Application.ViewModels.History;
using UberEats.Core.Domain.Entities;

namespace UberEats.Core.Application.Services
{
    public class HistoryService : IHistoryService
    {
        private readonly IHistoryRepository _historyRepository;
        public HistoryService(IHistoryRepository historyRepository)
        {
            _historyRepository = historyRepository;
        }

        public async Task AddNewHistory(SaveHistoryViewModel historyVm)
        {
            History history = new();
            historyVm.HistoryId = history.Id;
            historyVm.UserId = history.UserId;
            historyVm.CartId = history.CartId;
            historyVm.PaymentMethodId = history.PaymentMethodId;

            await _historyRepository.AddNewHistoryAsync(history);
        }

        public async Task DeleteHistory(int id)
        {
            await _historyRepository.DeleteHistoryAsync(id);
        }

        public async Task<ICollection<HistoryViewModel>> GetAllHistory()
        {
            var history = await _historyRepository.GetAllHistoryAsync();

            var historyVmList = history.Select(h => new HistoryViewModel
            {
                HistoryId = h.Id,
                UserName = h.User.Name,
                PaymentMethodName = h.PaymentMethod.PaymentName,
                cartItems = h.Cart.CartItems.Select(ci => new CartItemViewModel
                {
                    ItemId = ci.Id,
                    FoodId = ci.FoodId,
                    FoodName = ci.Food.Name,
                    Quantity = ci.Quantity,
                    Price = ci.Price
                }).ToList()
            }).ToList();

            return historyVmList;
            
        }

        public async Task<HistoryViewModel> GetHistoryById(int id)
        {
            var history = await _historyRepository.GetHIstoryByIdAsync(id);

            if (history == null)
                throw new Exception("Historial no encontrado");

            HistoryViewModel historyVm = new();
            historyVm.HistoryId = history.Id;
            historyVm.PaymentMethodName = history.PaymentMethod.PaymentName;
            historyVm.UserName = history.User.Name;
            historyVm.cartItems = history.Cart.CartItems.Select(ci => new CartItemViewModel
            {
                ItemId = ci.Id,
                CartId = ci.CartId,
                FoodName = ci.Food.Name ?? "",
                Quantity = ci.Quantity,
                Price = ci.Price,
            }).ToList();

            return historyVm;
        }
    }
}
