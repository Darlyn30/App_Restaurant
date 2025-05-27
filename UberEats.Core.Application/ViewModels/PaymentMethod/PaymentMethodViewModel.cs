using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UberEats.Core.Application.ViewModels.PaymentMethod
{
    public class PaymentMethodViewModel
    {
        public int Id {  get; set; }
        public string Name { get; set; }
        public string ImgUrl { get; set; }
    }
}
