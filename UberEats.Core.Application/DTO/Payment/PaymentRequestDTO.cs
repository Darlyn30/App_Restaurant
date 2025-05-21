using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UberEats.Core.Application.DTO.Payment
{
    public class PaymentRequestDTO
    {
        public int UserId { get; set; }
        public decimal Amount { get; set; }
        public string Method { get; set; } = null!;

        // Solo si se simula tarjeta
        public string? CardNumber { get; set; }
        public string? CardExpiry { get; set; }
        public string? CardCVC { get; set; }
    }
}
