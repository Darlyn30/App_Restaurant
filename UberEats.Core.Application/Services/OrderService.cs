using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UberEats.Core.Application.Interfaces.Repositories;
using UberEats.Core.Application.Interfaces.Services;
using UberEats.Core.Application.ViewModels.Orders;
using UberEats.Core.Domain.Entities;

namespace UberEats.Core.Application.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        public OrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task CreateOrder(SaveOrderViewModel orderVm)
        {
            Order order = new();
            order.Id = orderVm.OrderId;
            order.UserId = orderVm.UserId;
            order.CartId = orderVm.CartId;
            order.PaymentId = orderVm.PaymentId;
            order.CreationAt = DateTime.UtcNow;

            await _orderRepository.CreateOrderAsync(order);
        }

        public async Task DeleteOrder(int id)
        {
            await _orderRepository.DeleteOrderAsync(id);
        }

        public async Task<ICollection<OrderViewModel>> GetAllOrders()
        {
            var order = await _orderRepository.GetAllOrdersAync();

            var orderVmList = order.Select(orders => new OrderViewModel
            {
                OrderId = orders.Id,
                UserName = orders.User.Name,
                PaymentMethodName = orders.PaymentMethod.PaymentName,
                CartId = orders.CartId,
                TotalAmount = orders.TotalAmount,
            }).ToList();

            return orderVmList;
        }

        public async Task<OrderViewModel> GetOrderById(int id)
        {
            var order = await _orderRepository.GetOrderByIdAsync(id);

            OrderViewModel orderVm = new();
            orderVm.OrderId = order.Id;
            orderVm.UserName = order.User.Name;
            orderVm.PaymentMethodName = order.PaymentMethod.PaymentName;
            orderVm.CartId = order.CartId;
            orderVm.CreationAt = order.CreationAt;

            return orderVm;
        }
    }
}
