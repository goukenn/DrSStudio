

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: WinCoreMatrixTypeConverter.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
﻿using IGK.ICore.WinCore;
using IGK.ICore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace IGK.ICore.WinCore.ComponentModel
{
    class WinCoreMatrixTypeConverter : TypeConverter
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
                return true;
            if (destinationType == typeof(System.Drawing.Drawing2D.Matrix))
            {
                return true;
            }
            return base.CanConvertTo(context, destinationType);
        }
        public override object ConvertTo(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, Type destinationType)
        {
            if (destinationType == typeof(string))
                return Matrix.ToString(((Matrix)value));
            if (destinationType == typeof(System.Drawing.Drawing2D.Matrix))
            {
                Matrix m = value as Matrix;
                System.Drawing.Drawing2D.Matrix v_matrix = new System.Drawing.Drawing2D.Matrix(
                    m.Elements[0], m.Elements[1],
                    m.Elements[4], m.Elements[5],
                    m.Elements[12], m.Elements[13]
                    );

                return v_matrix;
            }
            return base.ConvertTo(context, culture, value, destinationType);
        }
        public override object ConvertFrom(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value)
        {
            if (value is string)
            {
                Matrix m = new Matrix();
                m.LoadString(value as string);
                return m;
            }
            return base.ConvertFrom(context, culture, value);
        }
    }
}
