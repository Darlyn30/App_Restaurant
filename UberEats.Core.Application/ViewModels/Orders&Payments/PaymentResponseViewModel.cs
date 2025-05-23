using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UberEats.Core.Application.ViewModels.Orders_Payments
{
    public class PaymentResponseViewModel
    {
        public int OrderId { get; set; }
        public string Status { get; set; } // "Paid", "Pending", "Failed"
        public decimal TotalAmount { get; set; }
        public string PaymentMethod { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
