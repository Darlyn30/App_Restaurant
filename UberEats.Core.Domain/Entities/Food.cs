﻿
namespace UberEats.Core.Domain.Entities
{
    public class Food
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public int RestaurantId { get; set; }
        public bool Active { get; set; }
        public string? ImgUrl { get; set; }

        public Restaurant Restaurant { get; set; }
        public ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();
    }
}
