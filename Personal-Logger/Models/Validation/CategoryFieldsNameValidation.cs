using Microsoft.Ajax.Utilities;
using PersonalLogger.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PersonalLogger.Models.Validation
{
    public class CategoryFieldsNameValidation : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var category = (LogCategoryDTO)validationContext.ObjectInstance;
            
            foreach(var categoryField in category.CategoryFields)
            {
                if(categoryField.FieldName.IsNullOrWhiteSpace() || categoryField.FieldName.Length <= 3)
                {
                    return new ValidationResult("error");
                }
            }

            return ValidationResult.Success;
        }
    }
}