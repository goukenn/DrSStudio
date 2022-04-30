using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.ICore.ComponentModel
{
    public class CoreRectanglefTypeConverter : TypeConverter
    {
        public override object ConvertTo(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, Type destinationType)
        {

            Rectanglef v_rc = (Rectanglef)value;
            if (destinationType == typeof(string))
            {
                return Rectanglef.ConvertToString(v_rc);
            }
          
            return base.ConvertTo(context, culture, value, destinationType);
        }
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            if (
                (sourceType == typeof(string))
            )
            {
                return true;
            }
            return base.CanConvertFrom(context, sourceType);
        }
        public override object ConvertFrom(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value)
        {
            if (value is string)
            {
                return Rectanglef.ConvertFromString(value as string);
            }
            return base.ConvertFrom(context, culture, value);
        }
        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            if (
                (destinationType == typeof(string))
                )
                return true;
            return base.CanConvertTo(context, destinationType);
        }

    }
}
