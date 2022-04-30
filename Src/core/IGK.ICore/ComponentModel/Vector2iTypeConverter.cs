

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: Vector2iTypeConverter.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
/*
IGKDEV @ 2008-2016
author: C.A.D . BONDJE DOUE
file:Vector2iTypeConverter.cs
*/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace IGK.ICore.ComponentModel
{
    public class Vector2iTypeConverter : TypeConverter
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
                return Vector2i.ConvertFromString(value.ToString());
            return base.ConvertFrom(context, culture, value);
        }
        public override object ConvertTo(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, Type destinationType)
        {
            if (destinationType == typeof(string))
            {
                StringBuilder sb = new StringBuilder();
                if (value is Vector2i[])
                {
                    Vector2i[] v_t = (Vector2i[])value;
                    for (int i = 0; i < v_t.Length; i++)
                    {
                        if (i > 0)
                            sb.Append(' ');
                        sb.Append(v_t[i].ToString());
                    }
                }
                else if (value is Vector2i[])
                {
                    Vector2i[] v_t = (Vector2i[])value;
                    for (int i = 0; i < v_t.Length; i++)
                    {
                        if (i > 0)
                            sb.Append(' ');
                        sb.Append(string.Format("{0};{1}", v_t[i].X, v_t[i].Y));
                    }
                }
                else
                    sb.Append(((Vector2i)value).ToString());
                return sb.ToString();
            }
            if (destinationType == typeof(Vector2i[]))
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

