using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UberEats.Core.Application.Interfaces.Repositories;
using UberEats.Core.Application.Interfaces.Services;
using UberEats.Core.Application.ViewModels.PaymentMethod;
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

        public async Task AddPaymentMethod(SavePaymentMethodViewModel paymentVm)
        {
            PaymentMethod payment = new();
            payment.PaymentName = paymentVm.Name;
            payment.ImgUrl = paymentVm.ImgUrl;

            await _paymentRepository.AddAsync(payment);
        }

        public async Task DeletePaymentMethod(int id)
        {
            await _paymentRepository.DeleteAsync(id);
        }

        public async Task<ICollection<PaymentMethodViewModel>> GetAllPaymentMethods()
        {
            var result = await _paymentRepository.GetAllPaymentMethodsAsync();

            var vmResult = result.Select(payment => new PaymentMethodViewModel
            {
                Id = payment.Id,
                Name = payment.PaymentName,
                ImgUrl = payment.ImgUrl
            }).ToList();

            return vmResult;
        }

        public async Task<PaymentMethodViewModel> GetPaymentMethodById(int id)
        {
            var payment = await _paymentRepository.GetPaymentMethodByIdAsync(id);

            PaymentMethodViewModel vm = new();
            vm.Id = payment.Id;
            vm.Name = payment.PaymentName;
            vm.ImgUrl = payment.ImgUrl;

            return vm;
        }

        public async Task UpdatePaymentMethod(PaymentMethodViewModel paymentVm)
        {
            PaymentMethod payment = new();
            payment.Id = paymentVm.Id;
            payment.PaymentName = paymentVm.Name;
            payment.ImgUrl = paymentVm.ImgUrl;

            await _paymentRepository.UpdateAsync(payment);
        }
    }
}
