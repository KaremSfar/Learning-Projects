using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PersonalLogger.DTO
{
    public class CategoryFieldDTO
    {
        public int Id { get; set; }

        public string FieldName { get; set; }

        public FieldTypeDTO FieldType { get; set; }
    }
}