using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodWaste.Domain.DataAnnotations
{
    public class AgeDataAnnotations : ValidationAttribute
    {
        private readonly int _minimumAge;

        public AgeDataAnnotations(int minimumAge)
        {
            _minimumAge = minimumAge;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is DateTime date)
            {
                if (date > DateTime.Now)
                {
                    return new ValidationResult("Date cannot be in the future.");
                }

                if (date.AddYears(_minimumAge) > DateTime.Now)
                {
                    return new ValidationResult($"You must be at least {_minimumAge} years old.");
                }

                return ValidationResult.Success;
            }

            return new ValidationResult("Invalid date format.");
        }
    }
}
