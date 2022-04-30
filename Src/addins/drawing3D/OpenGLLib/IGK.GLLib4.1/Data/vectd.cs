

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: vectd.cs
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
file:vectd.cs
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
using System.Drawing;
namespace IGK.GLLib
{
    /// <summary>
    /// represent vector for double pointer
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    struct vect2d
    {
        internal double m_x , m_y;
        public double X{get{return m_x ;}set{m_x = value;}}
        public double Y{get{return m_y ;}set{m_y = value;}}
        //properties
        public static vect2d Empty;
        static vect2d()
        {
            Empty= new vect2d ();
        }
        public vect2d(double x, double y)
        {
            m_x = x;
            m_y = y;
        }
           public override string ToString()
        {
            return string.Format ("X:{0},Y:{1}", X, Y);
        }
    }
    [TypeConverter (typeof (vect3d .vect3dConverter ))]
    [StructLayout(LayoutKind.Sequential)]
    public struct vect3d
    {
        vect2d _vector;
        internal double m_z;
        public double X{get{return _vector.m_x ;}set{_vector.m_x = value;}}
        public double Y{get{return _vector.m_y ;}set{_vector.m_y = value;}}
        public double Z{get{return m_z ;}set{m_z = value;}}
        public static vect3d Empty;
        static vect3d()
        {
            Empty= new vect3d ();
        }
        public vect3d(double x,double y, double z)
        {
            _vector = new vect2d (x,y);
            m_z = z;
        }
           public override string ToString()
        {
            return string.Format ("X:{0},Y:{1},Z:{2}", X, Y, Z);
        }
           public static vect3d operator +(vect3d x, vect3d y)
           {
               vect3d v_out = new vect3d(
                   x.X + y.X ,
                   x.Y + y.Y ,
                   x.Z + y.Z
                   );
               return v_out;
           }
           public static vect3d operator -(vect3d x, vect3d y)
           {
               vect3d v_out = new vect3d(
                   x.X - y.X,
                   x.Y - y.Y,
                   x.Z - y.Z
                   );
               return v_out;
           }
           public static vect3d operator *(vect3d x, double  y)
           {
               vect3d v_out = new vect3d(
                   x.X * y,
                   x.Y * y,
                   x.Z * y
                   );
               return v_out;
           }
           public static implicit operator vect3d(vect3f x)
           {
               vect3d v_out = new vect3d(
                   x.X ,
                   x.Y ,
                   x.Y 
                   );
               return v_out;
           }
           public static explicit operator vect3f(vect3d x)
           {
               vect3f v_out = new vect3f(
                   (float)x.X,
                   (float)x.Y,
                   (float)x.Y
                   );
               return v_out;
           }
           public class vect3dConverter : TypeConverter
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
                   if (destinationType == typeof(vect4d))
                       return true;
                   if (destinationType == typeof(vect3d))
                       return true;
                   if (destinationType == typeof(vect2d))
                       return true;
                   return base.CanConvertTo(context, destinationType);
               }
               public override bool GetPropertiesSupported(ITypeDescriptorContext context)
               {
                   return true;
               }
               public override object ConvertTo(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, Type destinationType)
               {
                   vect3d v = (vect3d)value;
                   if (destinationType == typeof(string))
                   {                       
                       return v.X + ";" + v.Y + ";" + v.Z ;
                   }
                   if (destinationType == typeof(vect2d))
                       return new vect2d(v.X, v.Y);
                   if (destinationType == typeof(vect4d))
                       return new vect4d(v.X, v.Y, v.Z, 0);
                   return base.ConvertTo(context, culture, value, destinationType);
               }
               public override object ConvertFrom(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value)
               {
                   if (value is string)
                   {
                       string[] tb = value.ToString().Split(';');
                       switch(tb.Length)
                       {
                           case 3:
                               {
                           vect3d v_tb = new vect3d();
                           v_tb.X = (tb[0] == string.Empty) ? 0 : double.Parse(tb[0]);
                           v_tb.Y = (tb[1] == string.Empty) ? 0 : double.Parse(tb[1]);
                                   v_tb.Z =  (tb[2]==string.Empty )?0:double.Parse(tb[2]);                           
                           return v_tb;
                       }
                           case 2:
                           {
                               vect3d v_tb = new vect3d();
                               v_tb.X = double.Parse(tb[0]);
                               v_tb.Y = double.Parse(tb[1]);
                               return v_tb;
                           }
                       }
                   }
                   return base.ConvertFrom(context, culture, value);
               }
           }
    }
    [TypeConverter(typeof(vect4d.vect4dConverter))]
    [StructLayout(LayoutKind.Sequential)]
    public struct vect4d
    {
        internal vect3d _vector;
        internal double m_w;
        public double X{get{return _vector.X ;}set{_vector.X = value;}}
        public double Y{get{return _vector.Y ;}set{_vector.Y= value;}}
        public double Z{get{return _vector.Z ;}set{_vector.Z = value;}}
        public double W{get{return m_w ;}set{m_w = value;}}
        public static vect4d Empty;
        static vect4d()
        {
            Empty= new vect4d ();
        }
        public vect4d (double x,double y, double z, double q)
        {
            _vector = new vect3d  (x,y,z);
            m_w = q;
        }
        public override string ToString()
        {
            return string.Format ("X:{0},Y:{1},Z:{2},Q:{3}", X, Y, Z, W);
        }
        public static implicit operator vect4d (vect3d v){
            return new vect4d(v.X,
                v.Y,
                v.Z, 0);
        }
        public class vect4dConverter  : TypeConverter 
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
                if (destinationType == typeof(vect4d))
                    return true;
                if (destinationType == typeof(vect3d)) 
                    return true;
                if (destinationType == typeof(vect2d))
                    return true;
                return base.CanConvertTo(context, destinationType);
            }
            public override bool GetPropertiesSupported(ITypeDescriptorContext context)
            {
                return true;
            }
            public override object ConvertTo(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, Type destinationType)
            {
                if (destinationType == typeof(string))
                {
                    vect4d v = (vect4d)value;
                    return v.X + ";" + v.Y + ";" + v.Z + ";" + v.W;
                }
                return base.ConvertTo(context, culture, value, destinationType);
            }
            public override object ConvertFrom(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value)
            {
                if (value is string)
                {
                    string[] tb = value.ToString().Split(';');
                    switch (tb.Length)
                    {
                        case 4:
                            {
                                vect4d v_tb = new vect4d();
                                v_tb.X = double.Parse(tb[0]);
                                v_tb.Y = double.Parse(tb[1]);
                                v_tb.Z = double.Parse(tb[2]);
                                v_tb.W = double.Parse(tb[3]);
                                return v_tb;
                            }
                        case 3:
                            {
                                vect4d v_tb = new vect4d();
                                v_tb.X = double.Parse(tb[0]);
                                v_tb.Y = double.Parse(tb[1]);
                                v_tb.Z = double.Parse(tb[2]);
                                return v_tb;
                            }
                        case 2: {
                            vect4d v_tb = new vect4d();
                            v_tb.X = double.Parse(tb[0]);
                            v_tb.Y = double.Parse(tb[1]);
                            return v_tb;
                        }
                    }
                }
                return base.ConvertFrom(context, culture, value);
            }
        }
    }
}

