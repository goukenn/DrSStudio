

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: vecTypeConverter.cs
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
file:vecTypeConverter.cs
*/
/* 
-------------------------------------------------------------------
Company: IGK-DEV
Author : C.A.D. BONDJE DOUE
SITE : http://www.igkdev.be
Application : DrSStudio
powered by IGK - DEV &copy; 2008-2011
THIS FILE IS A PART OF THE DRSSTUDIO APPLICATION. SEE "License.txt"
FOR MORE INFORMATION ABOUT THE LICENSE
------------------------------------------------------------------- 
*/
/* 
-------------------------------------------------------------------
Company: IGK-DEV
Author : C.A.D. BONDJE DOUE
SITE : http://www.igk-dev.be
App : DrSStudio
powered by IGK - DEV 2008-2011
------------------------------------------------------------------- 
*/
/* This file is part of iGK-DrawingSoft.
*    iGK-DrawingSoft is free software: you can redistribute it and/or modify
*    it under the terms of the GNU Lesser General Public License as published by
*    the Free Software Foundation, either version 3 of the License, or
*    (at your option) any later version.
*    iGK-DrawingSoft is distributed in the hope that it will be useful,
*    but WITHOUT ANY WARRANTY; without even the implied warranty of
*    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
*    GNU General Public License for more details.
*    You should have received a copy of the GNU Lesser General Public License
*    along with IGK-DRAWING SOFT.  If not, see <http://www.gnu.org/licenses/>.
*
*    Copyright (c) 2008-2009 
*    Author : C.A.D. BONDJE DOUE
*    mail : bondje.doue@hotmail.com
*/
﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.ComponentModel;
namespace IGK.GLLib
{
    class vecfTypeConverter : TypeConverter 
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
                return true;
            return base.CanConvertFrom(context, sourceType);
        }
        public override object ConvertFrom(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value)
        {
            if (value is string)
            {
                string[] v_tb = value.ToString().Split(';');
                float a = 0;
                float b = 0;
                float c = 0;
                float d = 0;
                switch (v_tb.Length)
                {
                    case 3:
                        a = float.Parse(v_tb[0]);
                        b = float.Parse(v_tb[1]);
                        c = float.Parse(v_tb[2]);
                        return new vect3f(a, b, c);
                    case 4:
                        a = float.Parse(v_tb[0]);
                        b = float.Parse(v_tb[1]);
                        c = float.Parse(v_tb[2]);
                        d = float.Parse(v_tb[3]);
                        return new vect4f(a, b, c,d);
                }
            }
            else
            {
                if (value is vect2f) { vect2f d = (vect2f)value; return d.X + ";" + d.Y; }
                if (value is vect3f) { vect3f d = (vect3f)value; return d.X + ";" + d.Y + ";" + d.Z; }
                if (value is vect4f) { vect4f d = (vect4f)value; return d.X + ";" + d.Y + ";" + d.Z + ";" + d.Q; }
            }
            return base.ConvertFrom(context, culture, value);
        }
        public override object ConvertTo(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, Type destinationType)
        {
            if (destinationType == typeof(string))
            {
                if (value is vect2f) { vect2f d = (vect2f)value; return d.X + ";" + d.Y; }
                if (value is vect3f) { vect3f d = (vect3f)value; return d.X + ";" + d.Y + ";" + d.Z; }
                if (value is vect4f) { vect4f d = (vect4f)value; return d.X + ";" + d.Y + ";" + d.Z + ";" + d.Q; }            
            }
            return base.ConvertTo(context, culture, value, destinationType);
        }
    }
}

