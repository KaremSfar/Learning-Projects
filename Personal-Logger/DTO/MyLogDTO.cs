using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PersonalLogger.DTO
{
    public class MyLogDTO
    {
        public int Id;

        public DateTime LogDate { get; set; }

        [Required]
        public int LogCategoryId { get; set; }

        //Potentially make a value-type validation, might be unnecessary tho
        public List<FieldDTO> Fields { get; set; }

    }
}