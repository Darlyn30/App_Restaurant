using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UberEats.Core.Application.ViewModels.Cart;
using UberEats.Core.Domain.Entities;

namespace UberEats.Core.Application.ViewModels.History
{
    public class HistoryViewModel
    {
        public int HistoryId {  get; set; }
        public string PaymentMethodName { get; set; }
        public string UserName {  get; set; }
        public List<CartItemViewModel> cartItems { get; set; }
    }
}
