

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: WireBeizerPoint.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
/*
IGKDEV - [2008 - 2014]
author: C.A.D . BONDJE DOUE
file:WireBeizerPoint.cs
*/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace IGK.DrSStudio.WireAddIn
{
    [TypeConverter(typeof(WireBeizerPointTypeConverter))]
    public struct WireBeizerPoint
    {
        private Vector2f m_Definition1;
        private Vector2f m_Definition2;
        public Vector2f Definition2
        {
            get { return m_Definition2; }
            set
            {
                if (m_Definition2 != value)
                {
                    m_Definition2 = value;
                }
            }
        }
        public Vector2f Definition1
        {
            get { return m_Definition1; }
            set
            {
                if (m_Definition1 != value)
                {
                    m_Definition1 = value;
                }
            }
        }
        public WireBeizerPoint(Vector2f def1, Vector2f def2)
        {
            this.m_Definition1 = def1;
            this.m_Definition2 = def2;
        }
        public WireBeizerPoint(float x1, float y1, float x2 , float y2):this(new Vector2f (x1,y1), new Vector2f (x2,y2))
        {
        }
    }
    class WireBeizerPointTypeConverter : TypeConverter
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
                string[] t = (value as string).Split(new char[]{' '}, StringSplitOptions.RemoveEmptyEntries );
                if (t.Length == 4)
                {
                    float x1 = float.Parse(t[0]);
                    float y1 = float.Parse(t[1]);
                    float x2 = float.Parse(t[2]);
                    float y2 = float.Parse(t[3]);
                    return new WireBeizerPoint(x1, y1, x2, y2);
                }
                else if ((t.Length> 0) && ((t.Length % 4) == 4))
                {
                    WireBeizerPoint[] vt = new WireBeizerPoint[t.Length % 4];
                    int o = 0;
                    for (int i = 0; i < vt.Length ; i++)
                    {
                        o = i*4;
                        vt[i] = new WireBeizerPoint(
                            float.Parse(t[o + 0]),
                            float.Parse(t[o + 1]),
                            float.Parse(t[o + 2]),
                            float.Parse(t[o + 3])
                            );
                    }
                    return vt;
                }
                return new WireBeizerPoint();
            }
            return base.ConvertFrom(context, culture, value);
        }
        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            if (destinationType == typeof(string))
                return true;
            return base.CanConvertTo(context, destinationType);
        }
        public override object ConvertTo(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, Type destinationType)
        {
            if (destinationType == typeof(string))
            {
                if (value.GetType().IsArray)
                {
                    WireBeizerPoint[] tpt = (WireBeizerPoint[])value;
                    StringBuilder sb = new StringBuilder();
                    for (int i = 0; i < tpt.Length; i++)
                    {
                        if (i != 0)
                            sb.Append(" ");
                        sb.Append (string.Format("{0} {1} {2} {3}", tpt[i].Definition1.X,
                               tpt[i].Definition1.Y,
                               tpt[i].Definition2.X,
                               tpt[i].Definition2.Y));
                    }
                    return sb.ToString();
                }
                else
                {
                    WireBeizerPoint pt = (WireBeizerPoint)value;
                    return string.Format("{0} {1} {2} {3}", pt.Definition1.X,
                    pt.Definition1.Y,
                    pt.Definition2.X,
                    pt.Definition2.Y);
                }
            }
            return base.ConvertTo(context, culture, value, destinationType);
        }
    }
}

