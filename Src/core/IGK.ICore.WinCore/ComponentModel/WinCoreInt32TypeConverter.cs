using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace IGK.ICore.WinCore.ComponentModel
{
    class WinCoreInt32TypeConverter : TypeConverter
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
                int i = 0;
                if (int.TryParse(g, out i))
                {
                    return i;
                }
                else
                {
                    if (string.IsNullOrEmpty(g))
                        return 0;
                    Int32Converter c = new Int32Converter();
                    return c.ConvertFrom(value);
                }
            }
            return base.ConvertFrom(context, culture, value);
        }
    }
}
