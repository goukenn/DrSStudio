﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.ICore.ComponentModel
{
    /// <summary>
    /// represent a boolean type converter
    /// </summary>
    public class CoreBooleanTypeConverter : TypeConverter
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
                string g = (string)value ;
                if (string.IsNullOrEmpty(g))
                    return false;
                int i = 0;
                if (int.TryParse(g, out i))
                {
                    return (i >= 1);
                }
                else {
                    if (g.ToLower() == "true")
                        return true;
                    return false;
                     
                    //BooleanConverter c = new BooleanConverter();
                    //return (bool)c.ConvertFrom(value);
                }
            }
            return base.ConvertFrom(context, culture, value);
        }
    }
}