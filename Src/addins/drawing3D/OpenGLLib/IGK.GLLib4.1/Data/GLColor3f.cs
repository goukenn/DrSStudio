

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: GLColor3f.cs
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
file:GLColor3f.cs
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
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Drawing;
namespace IGK.GLLib
{
    [TypeConverter(typeof(GLColor3f.GLTypeConverter))]
    [StructLayout(LayoutKind.Sequential)]
    public struct GLColor3f
    {
        float m_r, m_g, m_b;
        public float R { get { return m_r; } set { m_r = value; } }
        public float G { get { return m_g; } set { m_g = value; } }
        public float B { get { return m_b; } set { m_b = value; } }
        public GLColor3f(float r, float g, float b)
        {
            m_r = r;
            m_g = g;
            m_b = b;
        }
        public override string ToString()
        {
            return string.Format("r:{0}; g:{1}; b:{2};", R, G, B);
        }
        public static implicit operator GLColor3f(Color c)        
        {
            return new GLColor3f(c.R / 255.0f,
                c.G / 255.0f,
                c.B / 255.0f);
        }
        class GLTypeConverter : System.ComponentModel.TypeConverter
        {
            public override bool GetPropertiesSupported(ITypeDescriptorContext context)
            {
                return true;
            }
            public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributes)
            {
                return TypeDescriptor.GetProperties(value);
            }
            public override object CreateInstance(ITypeDescriptorContext context, System.Collections.IDictionary propertyValues)
            {
                GLColor3f v_obj = new GLColor3f();
                v_obj.m_r = (float)propertyValues["R"];
                v_obj.m_g = (float)propertyValues["G"];
                v_obj.m_b = (float)propertyValues["B"];
                return v_obj;
            }
            public override bool GetCreateInstanceSupported(ITypeDescriptorContext context)
            {
                return true;
            }
        }
    }
}

