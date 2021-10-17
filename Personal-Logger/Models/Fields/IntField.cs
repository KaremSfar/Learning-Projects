using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PersonalLogger.Models.Fields
{
    public class IntField : Field
    {
        public int? Value { get; set; }

        public IntField()
        {

        }

        public IntField(dynamic value)
        {
            try
            {
                Value = int.Parse(value);
            }catch(Exception e)
            {

            }
        }
        public override dynamic GetValue()
        {
            return Value;
        }
    }
}