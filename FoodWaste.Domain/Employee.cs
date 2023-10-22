using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodWaste.Domain
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Email { get; set; }
        public int EmployeeNumber { get; set; }
        public Cafeteria? Cafeteria { get; set; }
        public int CafeteriaId { get; set; }
    }
}
