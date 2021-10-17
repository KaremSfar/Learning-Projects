using PersonalLogger.Models.Validation;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace PersonalLogger.DTO
{
    public class LogCategoryDTO
    {
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        [MinLength(3)]
        public string CategoryName { get; set; }

        [CategoryFieldsNameValidation]
        public List<CategoryFieldDTO> CategoryFields { get; set; }
    }
}