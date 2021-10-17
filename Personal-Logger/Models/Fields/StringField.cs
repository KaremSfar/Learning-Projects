using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PersonalLogger.Models.Fields
{
    public class StringField : Field
    {
        public string Value { get; set; }


        public StringField()
        {

        }

        public StringField(dynamic value)
        {
            Value = value;
        }

        public override dynamic GetValue()
        {
            return Value;
        }
    }
}