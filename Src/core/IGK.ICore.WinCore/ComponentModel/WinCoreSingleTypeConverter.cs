using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace IGK.ICore.WinCore.ComponentModel
{
    class WinCoreSingleTypeConverter : TypeConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            if (sourceType == typeof(string))
                return true;
            return base.CanConvertFrom(context, sourceType);
        }
        public override object ConvertFrom(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value)
        {
            if (value is string)
            {
                string g = (string)value;
                float i = 0;
                if (float.TryParse(g, out i))
                {
                    return i;
                }
                else
                {
                    if (string.IsNullOrEmpty(g))
                        return 0.0f;
                  //  CoreUnit h = g;
                    SingleConverter c = new SingleConverter();
                    return (float)c.ConvertFrom(value);
                }
            }
            return base.ConvertFrom(context, culture, value);
        }
    }
}
