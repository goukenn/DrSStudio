using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.ICore.ComponentModel
{
    public class CoreVector2fArrayTypeConverter : TypeConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            if ((sourceType == typeof(string) )||
                (sourceType == typeof(Vector2f[]))
                )
                return true;
            return base.CanConvertFrom(context, sourceType);
        }
        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            if (destinationType == typeof(Vector2f[]))
                return true;
            return base.CanConvertTo(context, destinationType);
        }
        public override object ConvertFrom(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value)
        {
            if (value is string)
            {
                string[] v_t = (value as string).Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries  );
                List<Vector2f> v_r = new List<Vector2f>();
                for (int i = 0; i < v_t.Length; i++)
                {
                    if (string.IsNullOrEmpty(v_t[i]))
                        continue;
                    v_r.Add (Vector2f.ConvertFromString(v_t[i]));
                }
                return v_r.ToArray ();
            }
           
           
            return base.ConvertFrom(context, culture, value);
        }
        public override object ConvertTo(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, Type destinationType)
        {
            if (destinationType == typeof(string))
            {
                StringBuilder sb = new StringBuilder();
                if (value is Vector2f[])
                {
                    Vector2f[] v_t = (Vector2f[])value;
                    for (int i = 0; i < v_t.Length; i++)
                    {
                        if (i > 0)
                            sb.Append(' ');
                        sb.Append(v_t[i].ToString());
                    }
                }
                else if (value is Vector2f[])
                {
                    Vector2f[] v_t = (Vector2f[])value;
                    for (int i = 0; i < v_t.Length; i++)
                    {
                        if (i > 0)
                            sb.Append(' ');
                        sb.Append(string.Format("{0};{1}", v_t[i].X, v_t[i].Y));
                    }
                }
                return sb.ToString();
            }
            if (destinationType == typeof(Vector2f[]))
            {
                Vector2f[] t = (Vector2f[])value;
                Vector2f[] v_t = new Vector2f[t.Length];
                for (int i = 0; i < t.Length; i++)
                {
                    v_t[i] = new Vector2f(t[i].X, t[i].Y);
                }
                return v_t;
            }
            return base.ConvertTo(context, culture, value, destinationType);
        }
 
    }
}
