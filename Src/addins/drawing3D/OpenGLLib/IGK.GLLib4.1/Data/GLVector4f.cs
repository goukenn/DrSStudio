

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: GLVector4f.cs
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
file:GLVector4f.cs
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
using System;
using System.ComponentModel ;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Runtime.InteropServices ;
namespace IGK.GLLib
{
    [TypeConverter(typeof(GLVector2f.GLTypeConverter ))]
    [StructLayout(LayoutKind.Sequential)]
    public struct GLVector2f
    {
        internal float m_x , m_y;
        public float X{get{return m_x ;}set{m_x = value;}}
        public float Y{get{return m_y ;}set{m_y = value;}}
        //properties
        public static GLVector2f Empty;
        static GLVector2f()
        {
            Empty= new GLVector2f ();
        }
        public GLVector2f(float x, float y)
        {
            m_x = x;
            m_y = y;
        }
           public override string ToString()
        {
            return string.Format ("X:{0},Y:{1}", X, Y);
        }
           class GLTypeConverter : System.ComponentModel.TypeConverter
        {
            public override bool GetPropertiesSupported(ITypeDescriptorContext context)
            {
                return true;
            }
            public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributes)
            {
                return TypeDescriptor.GetProperties (value);
            }
            public override object CreateInstance(ITypeDescriptorContext context, System.Collections.IDictionary propertyValues)
            {
                GLVector2f v_obj = new GLVector2f();
                v_obj.X=(float)propertyValues["X"];
                v_obj.Y =(float)propertyValues["Y"];                              
                return v_obj;
            }
            public override bool GetCreateInstanceSupported(ITypeDescriptorContext context)
            {
                return true;
            }            
        }
    }
    [TypeConverter(typeof(GLVector3f.GLTypeConverter ))]
    [StructLayout(LayoutKind.Sequential)]
    public struct GLVector3f
    {
        GLVector2f _vector;
        internal float m_z;
        public float X{get{return _vector.m_x ;}set{_vector.m_x = value;}}
        public float Y{get{return _vector.m_y ;}set{_vector.m_y = value;}}
        public float Z{get{return m_z ;}set{m_z = value;}}
        public static GLVector3f Empty;
        static GLVector3f()
        {
            Empty= new GLVector3f ();
        }
        public GLVector3f(float x,float y, float z)
        {
            _vector = new GLVector2f (x,y);
            m_z = z;
        }
           public override string ToString()
        {
            return string.Format ("X:{0},Y:{1},Z:{2}", X, Y, Z);
        }
         class GLTypeConverter : System.ComponentModel.TypeConverter
        {
            public override bool GetPropertiesSupported(ITypeDescriptorContext context)
            {
                return true;
            }
            public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributes)
            {
                return TypeDescriptor.GetProperties (value);
            }
            public override object CreateInstance(ITypeDescriptorContext context, System.Collections.IDictionary propertyValues)
            {
                GLVector3f v_obj = new GLVector3f();
                v_obj.X=(float)propertyValues["X"];
                v_obj.Y =(float)propertyValues["Y"];
                v_obj.Z =(float)propertyValues["Z"];                
                return v_obj;
            }
            public override bool GetCreateInstanceSupported(ITypeDescriptorContext context)
            {
                return true;
            }            
        }
    }
    [TypeConverter(typeof(GLVector4f.GLTypeConverter ))]
    [StructLayout(LayoutKind.Sequential)]
    public struct GLVector4f
    {
        internal GLVector3f _vector;
        internal float m_w;
        public float X{get{return _vector.X ;}set{_vector.X = value;}}
        public float Y{get{return _vector.Y ;}set{_vector.Y= value;}}
        public float Z{get{return _vector.Z ;}set{_vector.Z = value;}}
        public float W{get{return m_w ;}set{m_w= value;}}
        public static GLVector4f Empty;
        static GLVector4f()
        {
            Empty= new GLVector4f ();
        }
        public GLVector4f (float x,float y, float z, float w)
        {
            _vector = new GLVector3f  (x,y,z);
            m_w = w;
        }
        public override string ToString()
        {
            return string.Format ("X:{0},Y:{1},Z:{2},W:{3}", X, Y, Z, W);
        }
        public static GLVector4f operator  - (GLVector4f point)
        {
            return new GLVector4f(-point.X,
                -point.Y,
                -point.Z,
                -point.W);
        }
        class GLTypeConverter : System.ComponentModel.TypeConverter
        {
            public override bool GetPropertiesSupported(ITypeDescriptorContext context)
            {
                return true;
            }
            public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributes)
            {
                return TypeDescriptor.GetProperties (value);
            }
            public override object CreateInstance(ITypeDescriptorContext context, System.Collections.IDictionary propertyValues)
            {
                GLVector4f v_obj = new GLVector4f();
                v_obj.X=(float)propertyValues["X"];
                v_obj.Y =(float)propertyValues["Y"];
                v_obj.Z =(float)propertyValues["Z"];
                v_obj.W =(float)propertyValues["W"];
                return v_obj;
            }
            public override bool GetCreateInstanceSupported(ITypeDescriptorContext context)
            {
                return true;
            }            
        }
    }
}

