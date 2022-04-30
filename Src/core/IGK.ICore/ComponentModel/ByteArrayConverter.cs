

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: ByteArrayConverter.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.ICore.ComponentModel
{
    /// <summary>
    /// used to convert byte[] array to string. in PointTypes path definition
    /// </summary>
    public class ByteArrayConverter : TypeConverter
    {
        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            if (destinationType == typeof(string))
                return true;
            return base.CanConvertTo(context, destinationType);
        }
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            if (sourceType == typeof(string))
            {
                return true;
            }
            return base.CanConvertFrom(context, sourceType);
        }
        public override object ConvertFrom(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value)
        {
            string vst = value as string;
            if (vst != null) { 
                string[] t = vst.Split (new string[]{";"}, StringSplitOptions.RemoveEmptyEntries );
                byte[] v_tab = new byte[t.Length];
                for (int i = 0; i < v_tab.Length; i++)
                {
                    v_tab[i] = Convert.ToByte(t[i]);
                }
                return v_tab;
            }
            return null;
            //return base.ConvertFrom(context, culture, value);
        }
        public override object ConvertTo(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, Type destinationType)
        {
            if (destinationType == typeof(string))
            {
                if (value is byte[])
                    return string.Join(";", ((byte[])value));
                return null;
            }
            return base.ConvertTo(context, culture, value, destinationType);
        }
    }
}
