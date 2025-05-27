using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UberEats.Core.Application.ViewModels.Orders
{
    public class OrderViewModel
    {
        public int OrderId { get; set; }
        public string UserName {  get; set; } // a traves de la PK le pasamos el nombre del cliente
        public int CartId {  get; set; }
        public decimal TotalAmount {  get; set; } //cuanto hace en total el carrito
        public int PaymentMethodId {  get; set; }
        public string PaymentMethodName {  get; set; }
        public DateTime CreationAt {  get; set; }
    }
}
