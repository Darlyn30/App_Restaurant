using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UberEats.Core.Domain.Entities
{
    public class PaymentMethod
    {
        public int Id { get; set; }
        public string PaymentName {  get; set; }
        public string ImgUrl { get; set; }
        public ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}
