using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PersonalLogger.Models.Fields
{
    public class TimeSpanField : Field
    {
        public TimeSpan Value { get; set; }

        public TimeSpanField()
        {

        }

        public TimeSpanField(dynamic value)
        {
            try
            {
                Value = TimeSpan.Parse(value);
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