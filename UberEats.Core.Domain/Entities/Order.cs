using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UberEats.Core.Domain.Entities
{
    public class Order
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("User")]
        public int UserId {  get; set; }
        [ForeignKey("PaymentMethod")]
        public int PaymentId {  get; set; }
        [ForeignKey("Cart")]
        public int CartId {  get; set; }
        public decimal TotalAmount {  get; set; }
        public DateTime CreationAt { get; set; } = DateTime.UtcNow;

        public User? User {  get; set; }
        public Cart? Cart {  get; set; }
        public PaymentMethod? PaymentMethod {  get; set; }
    }
}
