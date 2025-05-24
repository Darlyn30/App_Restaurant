using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UberEats.Core.Application.ViewModels.PaymentMethod;
using UberEats.Core.Domain.Entities;

namespace UberEats.Core.Application.Interfaces.Services
{
    public interface IPaymentService
    {
        Task<ICollection<PaymentMethodViewModel>> GetAllPaymentMethods();
        Task<PaymentMethodViewModel> GetPaymentMethodById(int id);
        Task AddPaymentMethod(SavePaymentMethodViewModel paymentVm);
        Task UpdatePaymentMethod(PaymentMethodViewModel paymentVm);
        Task DeletePaymentMethod(int id);
    }
}
