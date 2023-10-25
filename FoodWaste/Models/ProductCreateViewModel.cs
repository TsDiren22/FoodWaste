using FoodWaste.Domain;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;

namespace FoodWaste.WebApp.Models
{
    public class ProductCreateViewModel
    {
        public string Name { get; set; }
        [DisplayName("Is Alcoholic")]
        public bool IsAlcoholic { get; set; }
        public IFormFile? PictureFile { get; set; }
    }
}
