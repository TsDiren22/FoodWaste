using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodWaste.Domain
{
    public class Cafeteria
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public City City { get; set; }
        public bool HasWarmMeals { get; set; }
    }
}
