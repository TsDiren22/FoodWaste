using System.ComponentModel.DataAnnotations;

namespace FoodWaste.WebApp.Models
{
    public class ReserveModel
    {
        [Display(Name = "Pickup Date")]
        public DateTime PickupDate { get; set; }
        public int PackageId { get; set; }
    }
}
