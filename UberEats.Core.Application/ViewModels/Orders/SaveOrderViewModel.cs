using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UberEats.Core.Application.ViewModels.Orders
{
    public class SaveOrderViewModel
    {
        public int OrderId {  get; set; }
        public int PaymentId {  get; set; }
        public int UserId {  get; set; }
        public int CartId { get; set; }
        public decimal TotalAmount {  get; set; }
        public DateTime CreationAt { get; set; } = DateTime.UtcNow;
    }
}
