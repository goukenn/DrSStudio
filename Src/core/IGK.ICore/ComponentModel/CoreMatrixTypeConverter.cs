

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: CoreMatrixTypeConverter.cs
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
file:CoreMatrixTypeConverter.cs
*/

﻿using IGK.ICore;using IGK.ICore.Drawing2D;
using System;
using System.Collections.Generic;
using System.Text;
namespace IGK.ICore.ComponentModel
{
    public sealed class CoreMatrixTypeConverter : System.ComponentModel.TypeConverter
    {
        public override object ConvertFrom(System.ComponentModel.ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value)
        {
            if (value is string)
            {
                Matrix M = null;
                string[] t = value.ToString().Split(';');
                if (t.Length  == 6)
                {
                    float[] vt = new float[6];
                    for (int i = 0; i < 6; i++)
			{
                        vt[i]= float.Parse(t[i]);
			}
                    M = new Matrix(
                        vt[0], vt[1],
                        vt[2], vt[3],
                        vt[4], vt[5]);
                    return M;
                }
                return null;
            }
            return base.ConvertFrom(context, culture, value);
        }
        public override object ConvertTo(System.ComponentModel.ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, Type destinationType)
        {
            if (destinationType == typeof(string))
            {
                Matrix m = (Matrix)value ;
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < 6; i++)
                {
                    if (i > 0)
                        sb.Append(";");
                    sb.Append(m.Elements[i]);
                }
                return sb.ToString ();
            }
            return base.ConvertTo(context, culture, value, destinationType);
        }
    }
}

