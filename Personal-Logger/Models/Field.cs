using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalLogger.Models
{
    public abstract class Field : Entity
    {
        public CategoryField CategoryField { get; set; }
        public int CategoryFieldId { get; set; }

        public abstract dynamic GetValue();
    }
}
