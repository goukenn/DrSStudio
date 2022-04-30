

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: Vector2f.cs
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
file:Vector2f.cs
*/

using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.ComponentModel;
using IGK.ICore.ComponentModel;
using IGK.ICore;
using System.Collections;

namespace IGK.ICore
{    
    [StructLayout (LayoutKind.Sequential )]
    public struct Vector2f : IVector2f 
    {
        float m_x;
        float m_y;
        #region IVector2f Members
        public float X
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
        public float Y
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
        public static Vector2f UpY { get { return new Vector2f(0.0f, 1.0f); } }
        public static Vector2f UpX { get { return new Vector2f(1.0f, 0.0f); } }
        public void Offset(float x, float y)
        {
            this.m_x += x;
            this.m_y += y;
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }
        public static Vector2i Round(Vector2f v)
        {
            Vector2i v_r = new Vector2i(
                (int)global::System.Math.Round(v.X),
                (int)global::System.Math.Round(v.Y));
            return v_r;
        }
        public Vector2i Round() {
            return Round(this);
        }
        public static Vector2i Ceiling(Vector2f v)
        {
            Vector2i v_r = new Vector2i(
                (int)global::System.Math.Ceiling(v.X),
                (int)global::System.Math.Ceiling(v.Y));
            return v_r;
        }
        public static Vector2f Zero;

        static  Vector2f()
        {
            Zero = new Vector2f(0.0f, 0.0f);
        }
        public Vector2f(float  x,  float y)
        {
            this.m_x = x;
            this.m_y = y;
        }
        public Vector2f(CoreUnit x, CoreUnit y)
        {
            this.m_x = ((ICoreUnitPixel)x).Value;
            this.m_y = ((ICoreUnitPixel)y).Value;
        }

        public Vector2f(float all) : this(all,all)
        {
        }

        /// <summary>
        /// stranslat the current vector
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void Translate(float x, float y)
        {
            this.m_x += x;
            this.m_y += y;
        }
        public void Scale(float x, float y) {
            this.m_x *= x;
            this.m_y *= y;
        }
        public void Rotate(float angle)
        {
            Matrix m = new Matrix();
            m.Rotate(angle);
            var t = this.Transform(m);
            m.Dispose();
            this.X = t.X;
            this.Y = t.Y;
        }
        public Vector2f Transform(Matrix matrix)
        {
            if (matrix.Elements == null)
                return this;
            Vector2f[] t = { this };
            t = CoreMathOperation.MultMatrixTransformPoint(matrix, t);
            return t[0];
        }
        public Vector2f TransformVector(Matrix matrix)
        {
            if (matrix.Elements == null)
                return this;
            Vector2f[] t = { this };
            t = CoreMathOperation.MultMatrixTransformVector(matrix, t);
            return t[0];
        }

        public static implicit operator Vector2f(Vector2i v)
        {
            return new Vector2f(v.X, v.Y);
        }
        public static Vector2f operator +(Vector2f v1, Vector2f v2)
        {
            Vector2f v_out = new Vector2f();
            v_out.m_x = v1.m_x + v2.m_x;
            v_out.m_y = v1.m_y + v2.m_y;
            return v_out;
        }
        public static Vector2f operator +(Vector2f v1, float v2)
        {
            Vector2f v_out = new Vector2f();
            v_out.m_x = v1.m_x + v2;
            v_out.m_y = v1.m_y + v2;
            return v_out;
        }
        public static Vector2f operator -(Vector2f v1, float v2)
        {
            Vector2f v_out = new Vector2f();
            v_out.m_x = v1.m_x - v2;
            v_out.m_y = v1.m_y - v2;
            return v_out;
        }
        public static Vector2f operator -(Vector2f v1, Vector2f v2)
        {
            Vector2f v_out = new Vector2f();
            v_out.m_x = v1.m_x - v2.m_x;
            v_out.m_y = v1.m_y - v2.m_y;
            return v_out;
        }
        public static Vector2f operator *(Vector2f v1, float  v2)
        {
            Vector2f v_out = new Vector2f();
            v_out.m_x = v1.m_x * v2;
            v_out.m_y = v1.m_y * v2;
            return v_out;
        }
        public static Vector2f operator *(float v2, Vector2f v1)
        {
            Vector2f v_out = new Vector2f();
            v_out.m_x = v1.m_x * v2;
            v_out.m_y = v1.m_y * v2;
            return v_out;
        }
        public static Vector2f operator /(Vector2f v1, float v2)
        {
            Vector2f v_out = new Vector2f();
            v_out.m_x = v1.m_x / v2;
            v_out.m_y = v1.m_y / v2;
            return v_out;
        }
        public float Distance(Vector2f f)
        {
            return Vector2f.Distance(this, f);
        }
        public float Distance()
        {
            return Vector2f.Distance(this, Vector2f.Zero);
        }
        public static float Distance(Vector2f i, Vector2f f)
        {
            float a = f.X - i.X;
            float b = f.Y - i.Y;
            return (int)global::System.Math.Sqrt(a * a + b * b);
        }
        public static explicit operator Vector2f(Vector3f v)
        {
            return new Vector2f(v.X, v.Y);
        }
        public override string ToString()
        {
            return string.Format("{0};{1}", this.X, this.Y);
        }
        public static bool operator == (Vector2f v, Vector2f v2)
        {
            return v.Equals(v2);
        }
        public static bool operator !=(Vector2f v, Vector2f v2)
        {
            return !v.Equals(v2);
        }
        public static  implicit operator Vector2f(Vector2d d)
        {
            return new Vector2f ((float)d.X,(float) d.Y );
        }
        public static Vector2f ConvertFromString(string p)
        {
            string[] v_tb = p.Split(new char[]{';', ' ',','}, StringSplitOptions.RemoveEmptyEntries );
            if (v_tb.Length == 2)
            {
                Vector2f v_t = new Vector2f(
                    v_tb[0].ToPixel(),
                    v_tb[1].ToPixel());
                return v_t;
            }          
            return Vector2f.Zero;
        }
        public void Normalize()
        {
            float d =(float)Math.Sqrt ( (this.m_x * this.m_x) + (this.m_y * this.m_y));
            if (d != 0)
            {
                this.m_x = m_x / d;
                this.m_y = m_y / d;
            }
        }
        /// <summary>
        /// return a vector with those value
        /// </summary>
        /// <param name="size">float of the point</param>
        /// <returns></returns>
        public static Vector2f From(float size)
        {
            return new Vector2f(size, size);
        }
        public static float GetAngle(Vector2f startVector2i, Vector2f endVector2i)
        {
            double dx, dy;
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
                    return(float) (Math.PI / 2.0f);
                }
                else
                    return(float) (-Math.PI / 2.0f);
            }
            double angle = Math.Atan(dy / dx);
            if (dx < 0)
                angle += Math.PI;
            return (float)angle;
        }
    }
}

