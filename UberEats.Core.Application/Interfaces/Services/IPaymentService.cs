using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UberEats.Core.Application.DTO.Payment;
using UberEats.Core.Domain.Entities;

namespace UberEats.Core.Application.Interfaces.Services
{
    public interface IPaymentService
    {
        Task<Payment> ProccessPayment(PaymentRequestDTO dto);
    }
}
