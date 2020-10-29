using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GreenDoorV1.Helpers
{
    public class DateAfterNow: ValidationAttribute
    {
        public override string FormatErrorMessage(string name)
        {
            return "Date can't be earlier than now!";
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var dateValue = value as DateTime? ?? new DateTime();

            if (dateValue > DateTime.Now)
            {
                return ValidationResult.Success;
            }

            return new ValidationResult(FormatErrorMessage(validationContext.DisplayName));
        }
    }
}
