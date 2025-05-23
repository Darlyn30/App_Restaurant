using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UberEats.Core.Application.ViewModels.Orders_Payments;
using UberEats.Core.Domain.Entities;

namespace UberEats.Core.Application.Interfaces.Services
{
    public interface IPaymentService
    {
        Task<ICollection<PaymentMethod>> GetAllPaymentMethods();
        Task<PaymentMethod> GetPaymentMethodById(int id);
        Task AddPaymentMethod(PaymentMethod paymentMethod);
        Task UpdatePaymentMethod(PaymentMethod paymentMethod);
        Task DeletePaymentMethod(int id);
    }
}
