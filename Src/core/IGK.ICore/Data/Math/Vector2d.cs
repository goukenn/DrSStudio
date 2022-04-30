

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: Vector2d.cs
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
file:Vector2d.cs
*/

﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.ComponentModel;
using IGK.ICore;using IGK.ICore.ComponentModel;
namespace IGK.ICore
{
    [System.ComponentModel.TypeConverter(typeof(Vector2dConverter))]
    [StructLayout(LayoutKind.Sequential)]
    public struct Vector2d : IVector2d
    {
        double  m_x;
        double  m_y;
        #region IVector2d Members
        public double X
        {
            get
            {
                return m_x;
            }
            set
            {
                m_x = value;
            }
        }
        public double  Y
        {
            get
            {
                return m_y;
            }
            set
            {
                m_y = value;
            }
        }
        #endregion
        public Vector2f ToFloat() {
            return new Vector2f((float)m_x, (float)m_y);
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }
        public static Vector2i Round(Vector2d v)
        {
            Vector2i v_r = new Vector2i(
                (int)global::System.Math.Round(v.X),
                (int)global::System.Math.Round(v.Y));
            return v_r;
        }
        public static Vector2i Ceiling(Vector2d v)
        {
            Vector2i v_r = new Vector2i(
                (int)global::System.Math.Ceiling(v.X),
                (int)global::System.Math.Ceiling(v.Y));
            return v_r;
        }
        public static Vector2d Zero;
        static Vector2d()
        {
            Zero = new Vector2d(0.0f, 0.0f);
        }
        public Vector2d(double x,  double y)
        {
            this.m_x = x;
            this.m_y = y;
        }
        public void Translate(double x, double y)
        {
            this.m_x += x;
            this.m_y += y;
        }
        public static implicit operator Vector2d(Vector2i v)
        {
            return new Vector2d(v.X, v.Y);
        }
        public static Vector2d operator +(Vector2d v1, Vector2d v2)
        {
            Vector2d v_out = new Vector2d();
            v_out.m_x = v1.m_x + v2.m_x;
            v_out.m_y = v1.m_y + v2.m_y;
            return v_out;
        }
        public static Vector2d operator +(Vector2d v1, float v2)
        {
            Vector2d v_out = new Vector2d();
            v_out.m_x = v1.m_x + v2;
            v_out.m_y = v1.m_y + v2;
            return v_out;
        }
        public static Vector2d operator -(Vector2d v1, float v2)
        {
            Vector2d v_out = new Vector2d();
            v_out.m_x = v1.m_x - v2;
            v_out.m_y = v1.m_y - v2;
            return v_out;
        }
        public static Vector2d operator /(Vector2d v1, double v2)
        {
            if (v2 == 0)
                throw new DivideByZeroException();
            Vector2d v_out = new Vector2d();
            v_out.m_x = v1.m_x / v2;
            v_out.m_y = v1.m_y / v2;
            return v_out;
        }
        public static Vector2d operator -(Vector2d v1, Vector2d v2)
        {
            Vector2d v_out = new Vector2d();
            v_out.m_x = v1.m_x - v2.m_x;
            v_out.m_y = v1.m_y - v2.m_y;
            return v_out;
        }
        public static Vector2d operator *(Vector2d v1, float v2)
        {
            Vector2d v_out = new Vector2d();
            v_out.m_x = v1.m_x * v2;
            v_out.m_y = v1.m_y * v2;
            return v_out;
        }
        public static Vector2d operator /(Vector2d v1, float v2)
        {
            Vector2d v_out = new Vector2d();
            v_out.m_x = v1.m_x / v2;
            v_out.m_y = v1.m_y / v2;
            return v_out;
        }
        public static float Distance(Vector2d i, Vector2d f)
        {
            double  a = f.X - i.X;
            double b = f.Y - i.Y;
            return (int)global::System.Math.Sqrt(a * a + b * b);
        }
        /// <summary>
        /// get angle in radians beetween two Vector2is
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <returns></returns>
        public static double  GetAngle(Vector2d  startVector2i, Vector2d endVector2i)
        {
            double  dx, dy;
            dx = endVector2i.X - startVector2i.X;
            dy = endVector2i.Y - startVector2i.Y;
            if ((dx == 0.0f) && (dy == 0.0f))
            {
                return 0.0f;
            }
            if (dx == 0.0f)
            {
                if (dy > 0)
                {
                    return (Math.PI / 2.0f);
                }
                else
                    return (-Math.PI / 2.0f);
            }
            double  angle = Math.Atan(dy / dx);
            if (dx < 0)
                angle += Math.PI;
            return angle;
        }
        public static Vector2d DistanceP(Vector2d i, Vector2d f)
        {
            double a = f.X - i.X;
            double b = f.Y - i.Y;
            return new Vector2d (a, b);
        }
        public static implicit operator Vector2f(Vector2d f)
        {
            return new Vector2f((float)f.X,(float) f.Y);
        }
        public static explicit operator Vector2d(Vector3f v)
        {
            return new Vector2d(v.X, v.Y);
        }
        public static implicit operator Vector2d(Vector2f f)
        {
            return new Vector2d(f.X, f.Y);
        }
        public override string ToString()
        {
            return string.Format("{0};{1}", this.X, this.Y);
        }
        public static bool operator ==(Vector2d v, Vector2d v2)
        {
            return v.Equals(v2);
        }
        public static bool operator !=(Vector2d v, Vector2d v2)
        {
            return !v.Equals(v2);
        }
        public static Vector2d ConvertFromString(string p)
        {
            string[] v_tb = p.Split(';', ' ');
            if (v_tb.Length == 2)
            {
                Vector2d v_t = new Vector2d(
                    float.Parse(v_tb[0]),
                    float.Parse(v_tb[1]));
                return v_t;
            }
            return Vector2d.Zero;
        }
        public void Normalize()
        {
            float d = (float)Math.Sqrt((this.m_x * this.m_x) + (this.m_y * this.m_y));
            if (d != 0)
            {
                this.m_x = m_x / d;
                this.m_y = m_y / d;
            }
        }
    }
}

