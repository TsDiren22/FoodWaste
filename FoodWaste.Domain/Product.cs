using Microsoft.AspNetCore.Http;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        public string PictureType { get; set; }
        [NotMapped] // Use this attribute if you don't want to map it to the database
        public IFormFile? PictureFile { get; set; }
        public List<Package> Packages { get; set; } = new();
    }
}