

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: Vector2i.cs
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
file:Vector2i.cs
*/

﻿using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Text;
using IGK.ICore;using IGK.ICore.Data.Math;
using IGK.ICore.ComponentModel;
namespace IGK.ICore
{
    [System.ComponentModel.TypeConverter(typeof(Vector2iTypeConverter))]
    public struct  Vector2i : IVector2i 
    {
        private int m_x;
        private int m_y;
        public static readonly Vector2i Zero;
        #region IVector2i Members
        public int X
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
        public int Y
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
        static Vector2i()
        {
            Zero = new Vector2i(0, 0);
        }
        public Vector2i(int x, int y)
        {
            this.m_x = x;
            this.m_y = y;
        }
        public static Vector2i Round(Vector2f v)
        {
            return new Vector2i((int)Math.Round(v.X),
                (int)Math.Round(v.Y));
        }
        public void Offset(int x, int y)
        {
            this.m_x += x;
            this.m_y += y;
        }
        public static Vector2i operator +(Vector2i v1, Vector2i v2)
        {
            Vector2i v_out = new Vector2i();
            v_out.m_x = v1.m_x + v2.m_x;
            v_out.m_y = v1.m_y + v2.m_y;
            return v_out;
        }
        public static Vector2i operator +(Vector2i v1, Size2i v2)
        {
            Vector2i v_out = new Vector2i();
            v_out.m_x = v1.m_x + v2.Width ;
            v_out.m_y = v1.m_y + v2.Height ;
            return v_out;
        }
        public static Vector2i operator -(Vector2i v1, Size2i v2)
        {
            Vector2i v_out = new Vector2i();
            v_out.m_x = v1.m_x - v2.Width;
            v_out.m_y = v1.m_y - v2.Height;
            return v_out;
        }
        public static Vector2i operator -(Vector2i v1, Vector2i v2)
        {
            Vector2i v_out = new Vector2i();
            v_out.m_x = v1.m_x - v2.m_x;
            v_out.m_y = v1.m_y - v2.m_y;
            return v_out;
        }
        public static Vector2i operator *(Vector2i v1, float v2)
        {
            Vector2i v_out = new Vector2i();
            v_out.m_x =(int) (v1.m_x * v2);
            v_out.m_y =(int)( v1.m_y * v2);
            return v_out;
        }
        public static Vector2i operator /(Vector2i v1, float v2)
        {
            Vector2i v_out = new Vector2i();
            v_out.m_x =(int)( v1.m_x / v2);
            v_out.m_y =(int)( v1.m_y / v2);
            return v_out;
        }
        public static float Distance(Vector2i i, Vector2i f)
        {
            int a = f.X - i.X;
            int b = f.Y - i.Y;
            return (int)global::System.Math.Sqrt (a*a + b*b);
        }
        public override string ToString()
        {
            return string.Format("{0};{1}", this.X, this.Y);
        }
        public static Vector2i ConvertFromString(string p)
        {
            string[] v_tb = p.Split(new string[]{";", " "}, StringSplitOptions.RemoveEmptyEntries);
            if (v_tb.Length == 2)
            {
                Vector2i v_t = new Vector2i(
                    int.Parse(v_tb[0]),
                    int.Parse(v_tb[1]));
                return v_t;
            }
            return Vector2i.Zero;
        }
        public static bool operator ==(Vector2i e, Vector2i f)
        {
            return (e.X == f.X) && (e.Y == f.Y);
        }
        public static bool operator !=(Vector2i e, Vector2i f)
        {
            return (e.X != f.X) || (e.Y != f.Y);
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }
    }
}

