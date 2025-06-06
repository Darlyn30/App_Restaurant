﻿
namespace UberEats.Core.Domain.Entities
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImgUrl { get; set; }

        public ICollection<Restaurant> Restaurants { get; set; } = new List<Restaurant>();
    }
}
