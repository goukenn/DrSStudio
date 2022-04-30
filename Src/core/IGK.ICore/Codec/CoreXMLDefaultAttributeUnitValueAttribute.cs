using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IGK.ICore.Codec
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]  
    public class CoreXMLDefaultAttributeUnitValueAttribute : CoreXMLDefaultAttributeValueAttribute
    {
        public CoreXMLDefaultAttributeUnitValueAttribute(string unit):base(
           string.IsNullOrEmpty(unit)?0:  unit.ToPixel())
        {

        }
    }
}
