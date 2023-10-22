using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodWaste.Models
{
    public class SignInResponse
    {
        public bool success { get; set; }
        public string token { get; set; } = string.Empty;
    }
}
