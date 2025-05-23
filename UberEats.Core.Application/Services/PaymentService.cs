using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UberEats.Core.Application.Interfaces.Repositories;
using UberEats.Core.Application.Interfaces.Services;
using UberEats.Core.Domain.Entities;

namespace UberEats.Core.Application.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentRepository _paymentRepository;
        public PaymentService(IPaymentRepository paymentRepository)
        {
            _paymentRepository = paymentRepository;
        }

        public async Task AddPaymentMethod(PaymentMethod paymentMethod)
        {
            await _paymentRepository.AddAsync(paymentMethod);
        }

        public async Task DeletePaymentMethod(int id)
        {
            await _paymentRepository.DeleteAsync(id);
        }

        public async Task<ICollection<PaymentMethod>> GetAllPaymentMethods()
        {
            var result = await _paymentRepository.GetAllPaymentMethodsAsync();
            return result;
        }

        public async Task<PaymentMethod> GetPaymentMethodById(int id)
        {
            var result = await _paymentRepository.GetPaymentMethodByIdAsync(id);
            return result;
        }

        public async Task UpdatePaymentMethod(PaymentMethod paymentMethod)
        {
            await _paymentRepository.UpdateAsync(paymentMethod);
        }
    }
}
