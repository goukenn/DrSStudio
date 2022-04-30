using IGK.ICore;using IGK.ICore.Drawing2D;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.ICore.ComponentModel
{
    /// <summary>
    /// represent a core font type converter
    /// </summary>
    public class CoreFontTypeConverter : TypeConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            if (sourceType == typeof(string))
                return true;
            return base.CanConvertFrom(context, sourceType);
        }
        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            if (destinationType == typeof(string))
            {
                return true;
            }
            return base.CanConvertTo(context, destinationType);
        }
        public override object ConvertTo(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, Type destinationType)
        {
            CoreFont ft = value as CoreFont;
            if (ft != null)
            {
                return ft.GetDefinition();
            }
            return base.ConvertTo(context, culture, value, destinationType);
        }
        public override object ConvertFrom(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value)
        {
            if (value is String)
            {
                //return WinCoreFont.CreateFont(value.ToString(), 12, enuFontStyle.Regular, enuRenderingMode.Pixel);

                return CoreFont.CreateFrom (value.ToString(), null);// .CreateFont(value.ToString(), 12, enuFontStyle.Regular, enuRenderingMode.Pixel);
            }
            return base.ConvertFrom(context, culture, value);
        }
    }
}
