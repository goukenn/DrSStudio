

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: ColorfTypeConverter.cs
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
file:ColorfTypeConverter.cs
*/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace IGK.ICore.ComponentModel
{
    /// <summary>
    /// represent a class converter. to Convert from string
    /// </summary>
    public class CoreColorfTypeConverter : TypeConverter
    {
        public override object ConvertFrom(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value)
        {
            if (value is string)
            {
                return Colorf.Convert(value as string);
            }
            else if ((value != null) && (value.GetType() == typeof(Colorf)))
                return value;
          
            return base.ConvertFrom(context, culture, value);
        }
        public override object ConvertTo(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, Type destinationType)
        {
            if (value == null)
                base.ConvertTo(context, culture, value, destinationType);
            Type v_type = value.GetType();
            //Type v_rtype = Type.GetType (.GetType("Colorf");

            if (v_type == typeof(Colorf))
            {
                Colorf t = (Colorf)value;
                if (destinationType == typeof(string))
                    return Colorf.ConvertToString(t);
                if (destinationType == typeof(Colorf))
                {
                    return Colorf.FromString((string)value);
                }
                if (destinationType == typeof(InstanceDescriptor))
                {

                    MethodInfo ci = typeof(Colorf).GetMethod("FromFloat", new Type[] { typeof(float),
                typeof(float),
                typeof(float),
                typeof(float)});
                    return new InstanceDescriptor(ci, new object[] { t.A, t.R, t.G, t.B });
                }
             
            }
            //else if(v_type == typeof (Colorf[]))
            //{
              
            //}
          
            if (destinationType == typeof(string))
            {
                return value.ToString();
            }
            if (destinationType == typeof(string))
                return Colorf.ConvertToString((Colorf)value);

             return null;// base.ConvertTo(context, culture, value, destinationType);
        }
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            if ((sourceType == typeof(string)))
                return true;
            return base.CanConvertFrom(context, sourceType);
        }
        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            if (destinationType == typeof(string))
                return true;
            if (destinationType == typeof(InstanceDescriptor))
            {
                return true;
            }
        
            return base.CanConvertTo(context, destinationType);
        }
        public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
        {
            return true;
        }
        public override TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
        {
            Type t = typeof(Colorf);
            List<object> v_cl = new List<object>();
            foreach (System.Reflection.PropertyInfo pr in t.GetProperties(System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.Public))
            {
                if (pr.PropertyType == t)
                {
                    v_cl.Add(pr.GetValue(null, null));
                }
            }
            return new StandardValuesCollection(v_cl.ToArray());
        }
    }
}

