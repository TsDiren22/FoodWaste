using FoodWaste.Domain.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace FoodWaste.Domain
{
    public class Package
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [DisplayName("Time of pickup")]
        public DateTime? PickupDate { get; set; }
        [MaxDaysInFutureAnnotation(2)]
        [DisplayName("Time until package can be picked up")]
        public DateTime PickupExpiry { get; set; }
        [DisplayName("Contains adult products")]
        public bool ContainsAdultProducts { get; set; } = false;
        public decimal Price { get; set; }
        [DisplayName("Type of meal")]
        public string MealType { get; set; }
        public Student? Student { get; set; }
        [DisplayName("Reserved By")]
        public int? StudentId { get; set; }
        public List<Product> Products { get; set; } = new();
        public Cafeteria? Cafeteria { get; set; }
        [DisplayName("Cafeteria")]
        public int CafeteriaId { get; set; }
    }
}
