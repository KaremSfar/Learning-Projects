using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PersonalLogger.DTO
{
    public class FieldDTO
    {
        public int Id { get; set; }

        public CategoryFieldDTO CategoryField { get; set; }
        public int CategoryFieldId { get; set; }

        public dynamic Value { get; set; }
    }
}