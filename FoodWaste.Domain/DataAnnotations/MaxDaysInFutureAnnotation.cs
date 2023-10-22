using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodWaste.Domain.DataAnnotations
{
    public class MaxDaysInFutureAnnotation : ValidationAttribute
    {
        private readonly int _days;

        public MaxDaysInFutureAnnotation(int days)
        {
            _days = days;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is DateTime date)
            {
                if (date.Date > DateTime.Now.AddDays(_days).Date)
                {
                    return new ValidationResult($"Date cannot be more than {_days} days in the future.");
                }
                
                return ValidationResult.Success;
            }

            return new ValidationResult("Invalid date format.");
        }
    }
}
