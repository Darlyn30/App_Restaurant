
using System.ComponentModel.DataAnnotations;

namespace UberEats.Core.Domain.Entities
{
    public class Cart
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }
        public DateTime CreationAt { get; set; } = DateTime.Now;

        public User User { get; set; }
        public Order? Order {  get; set; }
        public ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();


    }
}
