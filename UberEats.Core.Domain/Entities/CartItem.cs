
using System.ComponentModel.DataAnnotations;

namespace UberEats.Core.Domain.Entities
{
    public class CartItem
    {
        [Key]
        public int Id { get; set; }
        public int CartId { get; set; }
        public int FoodId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public string ImgUrl { get; set; }
        public Food Food { get; set; }

        public Cart Cart { get; set; }
    }
}
