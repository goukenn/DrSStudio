

using IGK.ICore.WinCore;
using IGK.ICore;
using IGK.ICore.ComponentModel;
/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: Vector2fTypeConverter.cs
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
file:Vector2fTypeConverter.cs
*/
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace IGK.ICore.WinCore.ComponentModel
{
    class WinCoreVector2fTypeConverter : CoreVector2fTypeConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            if(
                (sourceType == typeof(string)) ||
                (sourceType == typeof(Point)) ||
                (sourceType == typeof(Point[]))||
                (sourceType == typeof(Vector2f))||
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
                            float.Parse(v_tb[i+1]))
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
            else{
            Vector2f cp = Vector2f.Zero;
              cp = ((Vector2f)value);
            if (destinationType == typeof(string))
            {
                return cp.ToString();
            }
            if (destinationType == typeof(Point))
            {
                return new Point((int)Math.Round(cp.X),
                    (int)Math.Round(cp.Y));
            }
            if (destinationType == typeof(PointF))
            {
                return new PointF(
                    cp.X,
                    cp.Y);
            }
            }
            return base.ConvertTo(context, culture, value, destinationType);
        }
        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            if (
               (destinationType == typeof(string)) ||
               (destinationType == typeof(Point)) ||
               (destinationType == typeof(Point[])) ||
               (destinationType == typeof(PointF)) ||
               (destinationType == typeof(PointF[]))
               )
                return true;
            return base.CanConvertTo(context, destinationType);
        }
    }
}

