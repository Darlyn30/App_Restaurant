using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UberEats.Core.Application.ViewModels.Orders_Payments
{
    public class CreatePaymentViewModel
    {
        public int UserId { get; set; }
        public string PaymentMethod { get; set; } // "paypal" o "card"
        public List<PaymentItemViewModel> Items { get; set; }
    }
}
