using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.ICore.ComponentModel
{
    public class CoreVector2fTypeConverter : TypeConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            if (
                (sourceType == typeof(string)) ||
                (sourceType == typeof(Vector2f)) ||
                (sourceType == typeof(Vector2f[]))
                )
                return true;
            return base.CanConvertFrom(context, sourceType);
        }
        public override object ConvertFrom(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value)
        {
            if (value is string)
            {
                string p = value as string;
                string[] v_tb = p.Split(new char[] { ';', ' ' }, StringSplitOptions.RemoveEmptyEntries);
                if (v_tb.Length == 2)
                {
                    Vector2f v_t = new Vector2f(
                        float.Parse(v_tb[0]),
                        float.Parse(v_tb[1]));
                    return v_t;
                }
                else if ((v_tb.Length > 2) && ((v_tb.Length % 2) == 0))
                {
                    List<Vector2f> v = new List<Vector2f>();
                    for (int i = 0; i < v_tb.Length; i += 2)
                    {
                        v.Add(new Vector2f(
                            float.Parse(v_tb[i]),
                            float.Parse(v_tb[i + 1]))
                            );
                    }
                    return v.ToArray();
                }
                return Vector2f.ConvertFromString(value.ToString());
            }
            return base.ConvertFrom(context, culture, value);
        }
        public override object ConvertTo(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, Type destinationType)
        {
            if (destinationType.IsArray)
            {
                Array v_c = (Array)value;
                Type v_t = v_c.GetValue(0).GetType();
                Type v_target = destinationType.GetElementType();
                ArrayList p = new ArrayList();
                foreach (var item in v_c)
                {
                    p.Add(ConvertTo(item, v_target));
                }
                return p.ToArray(v_target);
            }
            else
            {
                Vector2f cp = Vector2f.Zero;
                //if (value?.GetType().IsArray) {

                //}
                cp = ((Vector2f)value);
                if (destinationType == typeof(string))
                {
                    return cp.ToString();
                }
               
               
            }
            return base.ConvertTo(context, culture, value, destinationType);
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
