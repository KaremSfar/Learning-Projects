using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PersonalLogger.Models.Fields
{
    public class DecimalField : Field
    {
        public decimal? Value { get; set; }

        public DecimalField()
        {

        }

        public DecimalField(dynamic value)
        {
            try
            {
                Value = Decimal.Parse(value);
            }
            catch (Exception e)
            {

            }
        }

        public override dynamic GetValue()
        {
            return Value;
        }
    }
}