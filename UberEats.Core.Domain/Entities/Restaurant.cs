
namespace UberEats.Core.Domain.Entities
{
    public class Restaurant
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int CategoryId { get; set; }
        public TimeSpan OpeningTime { get; set; }
        public TimeSpan ClosingTime { get; set; }
        public bool DeliveryAvailable { get; set; }
        public bool Active { get; set; }
        public string? ImgUrl { get; set; }

        public Category Category { get; set; }
        public ICollection<Food> Foods { get; set; } = new List<Food>();
    }
}
