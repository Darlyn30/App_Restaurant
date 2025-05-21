using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UberEats.Core.Application.DTO.Payment;
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

        public async Task<Payment> ProccessPayment(PaymentRequestDTO dto)
        {
            string status = "Approved";

            if(dto.Method.ToLower() == "card")
            {
                // Simulate card payment processing
                if (string.IsNullOrEmpty(dto.CardNumber) || string.IsNullOrEmpty(dto.CardExpiry) || string.IsNullOrEmpty(dto.CardCVC))
                {
                    throw new Exception("Datos de tarjeta incompletos");
                }

                if(dto.CardNumber == "0000000000000000")
                {
                    status = "Rejected";
                }
            }

            // creamos la entidad de pago
            var payment = new Payment
            {
                UserId = dto.UserId,
                Amount = dto.Amount,
                Method = dto.Method,
                Status = status,
                CardLast4Digits = dto.CardNumber != null && dto.CardNumber.Length >= 4 ? dto.CardNumber[^4..] : null
            };

            return await _paymentRepository.AddAsync(payment);
        }
    }
}
