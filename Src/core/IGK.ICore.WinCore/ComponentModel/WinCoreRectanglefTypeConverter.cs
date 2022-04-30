

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: RectanglefTypeConverter.cs
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
file:RectanglefTypeConverter.cs
*/
/* 
-------------------------------------------------------------------
Company: IGK-DEV
Author : C.A.D. BONDJE DOUE
SITE : http://www.igkdev.be
Application : DrSStudio
powered by IGK - DEV &copy; 2008-2012
THIS FILE IS A PART OF THE DRSSTUDIO APPLICATION. SEE "License.txt"
FOR MORE INFORMATION ABOUT THE LICENSE
------------------------------------------------------------------- 
*/
﻿using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using IGK.ICore.WinCore;
using IGK.ICore.ComponentModel;
using IGK.ICore;
namespace IGK.ICore.WinCore.ComponentModel
{
    /// <summary>
    /// rectangle f type convertor
    /// </summary>
    public class WinCoreRectanglefTypeConverter : CoreRectanglefTypeConverter
    {
        public override object ConvertTo(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, Type destinationType)
        {
            if (value is Rectanglei)
            {
                Rectanglei v_rc = (Rectanglei)value;
                if (destinationType == typeof(string))
                {
                    return Rectanglei.ConvertToString(v_rc);
                }
                if (destinationType == typeof(RectangleF))
                {
                    return new RectangleF(v_rc.X, v_rc.Y, v_rc.Width, v_rc.Height);
                }
                if (destinationType == typeof(Rectangle))
                {
                    Rectanglei b = v_rc;
                    return new Rectangle(b.X, b.Y, b.Width, b.Height);
                }
            }
            else
            {
                Rectanglef v_rc = (Rectanglef)value;
                if (destinationType == typeof(string))
                {
                    return Rectanglef.ConvertToString(v_rc);
                }
                if (destinationType == typeof(RectangleF))
                {
                    return new RectangleF(v_rc.X, v_rc.Y, v_rc.Width, v_rc.Height);
                }
                if (destinationType == typeof(Rectangle))
                {
                    Rectanglei b = Rectanglei.Round(v_rc);
                    return new Rectangle(b.X, b.Y, b.Width, b.Height);
                }
            }
            return base.ConvertTo(context, culture, value, destinationType);
        }
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            if (
                (sourceType == typeof(string)) ||
                (sourceType == typeof(global::System.Drawing.RectangleF))||
                (sourceType == typeof(global::System.Drawing.Rectangle))
            )
            {
                return true;
            }
            return base.CanConvertFrom(context, sourceType);
        }
        public override object ConvertFrom(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value)
        {
            if (value is string)
            {
                return Rectanglef.ConvertFromString(value as string);
            }
            if (value is global::System.Drawing.Rectangle)
            {
                global::System.Drawing.Rectangle r = (global::System.Drawing.Rectangle)value;
                return new Rectanglef(r.X, r.Y, r.Width, r.Height);
            }
            if (value is global::System.Drawing.RectangleF)
            {
                global::System.Drawing.RectangleF r = (global::System.Drawing.RectangleF)value;
                return new Rectanglef(r.X, r.Y, r.Width, r.Height);
            }
            return base.ConvertFrom(context, culture, value);
        }
        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            if (
                (destinationType == typeof(string))||
                (destinationType == typeof(RectangleF))||
                (destinationType == typeof(Rectangle))
                )
                return true;
            return base.CanConvertTo(context, destinationType);
        }
    }
}

