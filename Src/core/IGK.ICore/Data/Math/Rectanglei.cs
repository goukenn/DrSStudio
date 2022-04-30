

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: Rectanglei.cs
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
file:Rectanglei.cs
*/

﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
namespace IGK.ICore
{
    [StructLayout (LayoutKind.Sequential)]
    [Serializable ()]
    public struct Rectanglei :         
        IRectanglei 
    {
        private int m_x;
        private int m_y;
        private int m_w;
        private int m_h;
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
        public int Width
        {
            get
            {
                return m_w;
            }
            set
            {
                m_w = value;
            }
        }
        public int Height
        {
            get
            {
                return m_h;
            }
            set
            {
                m_h = value;
            }
        }
        #endregion
        #region IRectanglei Members
        public int Right
        {
            get { return this.m_x + this.m_w; }
        }
        public int Bottom
        {
            get { return this.m_y + this.m_h; }
        }
        public int Top
        {
            get { return this.m_y; }
        }
        public int Left
        {
            get { return this.m_x; }
        }
        #endregion
        /// <summary>
        /// offset the current rectangle
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void Offset(int x, int y)
        {
            this.m_x += x;
            this.m_y += y;
        }
        public Vector2i Location { get { return new Vector2i(m_x, m_y); } set { this.m_x = value.X; m_y = value.Y; } }
        public Size2i Size{ get { return new Size2i(m_w, m_h); } set { this.m_w = value.Width; this.m_h = value.Height; } }
		public Vector2i Center{get{return new Vector2i ( m_x + m_w /2, m_y + m_h /2);}}
        public static readonly Rectanglei Empty;
        static Rectanglei()
        {
            Empty = new Rectanglei(0,0,0,0);
        }
        public override string ToString()
        {
            return string.Format("{0};{1};{2};{3}",
                this.X,
                this.Y,
                this.Width,
                this.Height);
        }
        public static string ConvertToString(Rectanglei v_rc)
        {
            return string.Format("{0};{1};{2};{3}",
                v_rc.X,
                v_rc.Y,
                v_rc.Width,
                v_rc.Height);
        }
        //public static Rectanglei ConvertFromString(string p)
        //{
        //    p = p.Trim();
        //    string[] t = p.Split(';', ' ');
        //    if (t.Length == 4)
        //    {
        //        return new Rectanglei(
        //            Convert.ToInt32(t[0].ToPixel()),
        //            Convert.ToInt32(t[1].ToPixel()),
        //            Convert.ToInt32(t[2].ToPixel()),
        //            Convert.ToInt32(t[3].ToPixel()));
        //    }
        //    return Rectanglei.Empty;
        //}
        public bool IsEmpty
        {
            get { return (this.Width == 0) && (this.Height == 0); }
        }
        public static bool operator == (Rectanglei rc1, Rectanglei rc2)
        {
            return (rc1.X == rc2.X) &&
                (rc1.Y == rc2.Y) &&
                (rc1.Width == rc2.Width) &&
                (rc1.Height == rc2.Height);
        }
        public static bool operator !=(Rectanglei rc1, Rectanglei rc2)
        {
            return !(rc1 == rc2);
        }
        public static Rectanglei operator +(Rectanglei rc1, Rectanglei rc2)
        {
            return new Rectanglei(rc1 .X + rc2 .X , 
                rc1 .Y + rc2 .Y ,
                rc1 .Width + rc2 .Width ,
                rc1 .Height +rc2 .Height );
        }
        public static Rectanglei operator -(Rectanglei rc1, Rectanglei rc2)
        {
            return new Rectanglei(rc1.X - rc2.X,
                rc1.Y - rc2.Y,
                rc1.Width - rc2.Width,
                rc1.Height - rc2.Height);
        }
        public static Rectanglei operator *(Rectanglei rc1, int i)
        {
            return new Rectanglei(rc1.X * i,
                rc1.Y * i,
                rc1.Width * i,
                rc1.Height * i);
        }
        public static Rectanglei operator /(Rectanglei rc1, int i)
        {
            if (i == 0)
                throw new DivideByZeroException();
            return new Rectanglei(rc1.X / i,
                rc1.Y / i,
                rc1.Width / i,
                rc1.Height / i);
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }
        public Rectanglei(int x, int y, int width, int height)
        {
            this.m_x = x;
            this.m_y = y;
            this.m_w = width;
            this.m_h = height;
        }
        public Rectanglei(Vector2i location, Size2i  Size2i)
        {
            this.m_x = location.X;
            this.m_y = location.Y;
            this.m_w = Size2i.Width;
            this.m_h = Size2i.Height;
        }
        public bool Contains(Vector2i  pt)
        {
            return ((pt.X >= this.X) &&
                    (pt.X <= this.Right) &&
                    (pt.Y >= this.Y) &&
                    (pt.Y <= this.Bottom));
        }
        public void Inflate(int x, int y)
        {
            this.m_x -= x;
            this.m_w += 2*x;
            this.m_y -= y;
            this.m_h += 2 * y;
        }
        /// <summary>
        /// intersect rectangle
        /// </summary>
        /// <param name="rc"></param>
        public void Intersect(Rectanglei rc1)
        {
            Rectanglei r = Intersect(this, rc1);
            this.m_x = r.X;
            this.m_y = r.Y;
            this.m_w = r.Height;
            this.m_h = r.Height;

        }
        /// <summary>
        /// intersect rectangle
        /// </summary>
        /// <param name="rc"></param>
        public static Rectanglei Intersect(Rectanglei rc1, Rectanglei rc2)
        {
            int H = rc1.Height + rc2.Height;
            int W = rc1.Width + rc2.Width;
            int minx = Math.Min(rc1.X, rc2.X);
            int maxx = Math.Max(rc1.X + rc1.Width, rc2.X + rc2.Width);
            int miny = Math.Min(rc1.Y, rc2.Y);
            int maxy = Math.Max(rc1.Y + rc1.Height, rc2.Y + rc2.Height);
            int w1 = W - (maxx - minx);
            int h1 = H - (maxy - miny);
            if (
                (w1 >= 0)
                &&
                (h1 >= 0)
                )
            {
                int k = Math.Min(rc1.Right, rc2.Right) - w1;
                int r = Math.Min(rc1.Bottom, rc2.Bottom) - h1;
                return new Rectanglei(k, r, w1, h1);
            }
            return Rectanglei.Empty;
        }
        /// <summary>
        /// converting from string.
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public static Rectanglei ConvertFromString(string p)
        {
            p = p.Trim();
            string[] t = p.Split(';', ' ');
            if (t.Length == 4)
            {
                return new Rectanglei(
                    Convert.ToInt32 (t[0]),
                    Convert.ToInt32(t[1]),
                    Convert.ToInt32(t[2]),
                    Convert.ToInt32(t[3]));
            }
            return Rectanglei.Empty;
        }
        public static Rectanglei Round(Rectanglef rc)
        {
            return new Rectanglei(
                (int)Math.Round(rc.X),
                (int)Math.Round(rc.Y),
                (int)Math.Round(rc.Width),
                (int)Math.Round(rc.Height));
        }
        public static Rectanglei Round(Rectangled rc)
        {
            return new Rectanglei(
                (int)Math.Round(rc.X),
                (int)Math.Round(rc.Y),
                (int)Math.Round(rc.Width),
                (int)Math.Round(rc.Height));
        }
        public static Rectanglei Ceiling(Rectanglef rc)
        {
            return new Rectanglei(
                (int)Math.Ceiling (rc.X),
                (int)Math.Ceiling(rc.Y),
                (int)Math.Ceiling(rc.Width),
                (int)Math.Ceiling(rc.Height));
        }
        public static Rectanglei Ceiling(Rectangled rc)
        {
            return new Rectanglei(
                (int)Math.Ceiling(rc.X),
                (int)Math.Ceiling(rc.Y),
                (int)Math.Ceiling(rc.Width),
                (int)Math.Ceiling(rc.Height));
        }
        public static Rectanglei FromLTRB(int left, int top, int right, int bottom)
        {
            return new Rectanglei(left, top,
                right - left,
                bottom - top);
        }
    }
}

