using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UberEats.Core.Domain.Entities
{
    public class Payment
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public decimal Amount { get; set; }
        public string Method { get; set; } = null!; // "cash", "card", "paypal"
        public string Status { get; set; } = "Pending"; // "Pending", "Approved", "Rejected"
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public string? CardLast4Digits { get; set; } // Solo si se simula tarjeta
    }
}
