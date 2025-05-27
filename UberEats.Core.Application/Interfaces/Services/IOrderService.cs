using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UberEats.Core.Application.ViewModels.Orders;

namespace UberEats.Core.Application.Interfaces.Services
{
    public interface IOrderService
    {
        Task<ICollection<OrderViewModel>> GetAllOrders();
        Task<OrderViewModel> GetOrderById(int id);
        Task CreateOrder(SaveOrderViewModel orderVm);
        Task DeleteOrder(int id);
    }
}
