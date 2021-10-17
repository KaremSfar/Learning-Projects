using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Vidly.Models
{
    public class Min18YearsValidation : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            Customer customer = (Customer)validationContext.ObjectInstance;
            if(customer.MembershipId == 0)
            {
                return ValidationResult.Success;
            }
            if (customer.Birthdate == null)
            {
                return new ValidationResult("Please enter your age");
            }
            var age = DateTime.Now.Year - customer.Birthdate.Value.Year;
            return (age > 18)
                ? ValidationResult.Success
                : new ValidationResult("Too young for membership");

        }
    }
}