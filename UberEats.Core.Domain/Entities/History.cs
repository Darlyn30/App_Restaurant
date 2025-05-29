using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UberEats.Core.Domain.Entities
{
    public class History
    {
        public int Id { get; set; }
        public int PaymentMethodId {  get; set; }
        public int UserId {  get; set; }
        public int CartId {  get; set; }
        public PaymentMethod? PaymentMethod {  get; set; }
        public User? User {  get; set; }
        public Cart? Cart { get; set; }
    }
}
