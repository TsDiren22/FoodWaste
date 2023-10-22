using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace FoodWaste.Domain
{
    public class Product
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [DisplayName("Is Alcoholic")]
        public bool IsAlcoholic { get; set; }
        public byte[] Picture { get; set; }
        public List<Package> Packages { get; set; } = new();
    }
}