

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: Rectanglef.cs
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
file:Rectanglef.cs
*/

﻿using IGK.ICore;
using IGK.ICore.ComponentModel;
using System;
using System.Collections.Generic;
using System.Text;
namespace IGK.ICore
{
    public struct Rectanglef
    {
        float m_x;
        float m_y;
        float m_w;
        float m_h;
        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        public static bool operator ==(Rectanglef rc1, Rectanglef rc2)
        {
            return (rc1.X == rc2.X) &&
                (rc1.Y  == rc2.Y) &&
                (rc1.Width  == rc2.Width) &&
                (rc1.Height  == rc2.Height );
        }
        public static bool operator !=(Rectanglef rc1, Rectanglef rc2)
        {
            return (rc1.X != rc2.X) ||
                (rc1.Y != rc2.Y) ||
                (rc1.Width != rc2.Width) ||
                (rc1.Height != rc2.Height);
        }
        public static Rectanglef Empty;
		public Vector2f Center{get{return new Vector2f ( m_x + m_w /2.0f, m_y + m_h /2.0f);}}
        public float Diagonal { get { return (float)(Math.Sqrt ((this.m_w * this.m_w) + (this.m_h * this.m_h ))); } }
        public Vector2f MiddleRight { get { return new Vector2f(this.Right , m_y + (m_h / 2.0f)); } }
        public Vector2f MiddleLeft { get { return new Vector2f(m_x  , m_y + (m_h / 2.0f)); } }
        public Vector2f MiddleTop { get { return new Vector2f(m_x + (m_w / 2.0f), m_y); } }
        public Vector2f MiddleBottom { get { return new Vector2f(m_x + (m_w / 2.0f), this.Bottom); } }
        public Vector2f BottomRight { get { return new Vector2f(this.Right, this.Bottom); } }
        public Vector2f BottomLeft { get { return new Vector2f(this.Left , this.Bottom); } }
        public Vector2f TopRight { get { return new Vector2f(this.Right , this.Top); } }

        /// <summary>
        /// create a new rectangle by replacing the with
        /// </summary>
        /// <param name="width"></param>
        /// <returns></returns>
        public Rectanglef SetWidth(float width) {
            return new Rectanglef(this.X, this.Y, width, this.Height);
        }
        /// <summary>
        /// create an new rectangle y by replaction the height
        /// </summary>
        /// <param name="height"></param>
        /// <returns></returns>
        public Rectanglef SetHeight(float height)
        {
            return new Rectanglef(this.X, this.Y, this.Width, height );
        }

        /// <summary>
        /// get a value indicate that the bound Size2i Width or Height is = 0
        /// </summary>
        public bool IsEmpty {
            get {
                return ((m_w == 0) || (m_h == 0));
            }
        }
        /// <summary>
        /// get a value indicate that the bound Size2i Width and Height is = 0
        /// </summary>
        public bool IsBothSizeEmpty {
            get
            {
                return ((m_w == 0) && (m_h == 0));
            }
        }
        /// <summary>
        /// get a value indicate that the bound Size2i Width or Height is <= 0 
        /// </summary>
        public bool IsEmptyOrSizeNegative {
            get {
                return IsEmpty || (m_w < 0) || (m_h < 0);
            }
        }
        static  Rectanglef()
        {
            Empty = new Rectanglef();
        }
        /// <summary>
        /// define a rectangle with string value
        /// </summary>
        /// <param name="x">x unit</param>
        /// <param name="y">y uni</param>
        /// <param name="width">width unit</param>
        /// <param name="height">height uni</param>
        public Rectanglef(string x, string y, string width, string height)
        {
            this.m_x = x.ToPixel();
            this.m_y = y.ToPixel() ;
            this.m_w = width.ToPixel();
            this.m_h = height.ToPixel();
        }
        public Rectanglef(float x, float y, float width, float height)
        {
            this.m_x = x;
            this.m_y = y;
            this.m_w = width;
            this.m_h = height;
        }
        public Rectanglef(Vector2f  Vector2i, Size2f Size2i)
        {
            this.m_x = Vector2i.X ;
            this.m_y = Vector2i.Y ;
            this.m_w = Size2i.Width ;
            this.m_h = Size2i.Height ;
        }
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
        public float Width
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
        public float Height
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
        public float Right
        {
            get { return this.m_x + this.m_w; }
        }
        public float Bottom
        {
            get { return this.m_y + this.m_h; }
        }
        public float Top
        {
            get { return this.m_y; }
        }
        public float Left
        {
            get { return this.m_x; }
        }
        #endregion
        /// <summary>
        /// return location
        /// </summary>
        public Vector2f Location {
            get {
                return new Vector2f(this.m_x , this.m_y);
            }
            set {
                this.m_x = value.X;
                this.m_y = value.Y;
            }
        }
        public Size2f Size {
            get
            {
                return new Size2f(this.m_w, this.m_h);
            }
            set {
                this.m_w = value.Width;
                this.m_h = value.Height;
            }
        }
        public static implicit operator Rectanglef(Rectanglei rec)
        {
            return new Rectanglef(
                rec.X,
                rec.Y,
                rec.Width,
                rec.Height);
        }
        public static Rectanglei Round(Rectanglef rec)
        {
            return new Rectanglei(
                (int)global::System.Math.Round(rec.X),
                (int)global::System.Math.Round(rec.Y),
               (int)global::System.Math.Round(rec.Width),
                (int)global::System.Math.Round(rec.Height));
        }
        public static Rectanglei Ceiling(Rectanglef rec)
        {
            return new Rectanglei(
                (int)global::System.Math.Ceiling(rec.X),
                (int)global::System.Math.Ceiling(rec.Y),
               (int)global::System.Math.Ceiling(rec.Width),
                (int)global::System.Math.Ceiling(rec.Height));
        }
        public static Rectanglef FromLTRB(int left, int top, int right, int bottom)
        {
            return new Rectanglef(left, top,
                right - left,
                bottom - top);
        }
        public static string ConvertToString(Rectanglef v_rc)        
        {
            return string.Format("{0};{1};{2};{3}",
                v_rc.X,
                v_rc.Y,
                v_rc.Width,
                v_rc.Height);
        }
        public static Rectanglef ConvertFromString(string p)
        {
            p = p.Trim();
            string[] t = p.Split(';',' ');
            if (t.Length == 4)
            {
                return new Rectanglef(
                    Convert.ToSingle(t[0].ToPixel()),
                    Convert.ToSingle(t[1].ToPixel()),
                    Convert.ToSingle(t[2].ToPixel()),
                    Convert.ToSingle(t[3].ToPixel()));
            }
            return Rectanglef.Empty;
        }
        public override string ToString()
        {
            return string.Format("{0};{1};{2};{3}",
                this.X,
                this.Y,
                this.Width,
                this.Height);
        }
        public void Inflate(float x, float y)
        {
            this.m_x -= x;
            this.m_w += 2 * x;
            this.m_y -= y;
            this.m_h += 2 * y;
        }
        public bool Contains(Vector2f pt)
        {
            return ((pt.X >= this.X) &&
           (pt.X <= this.Right) &&
           (pt.Y >= this.Y) &&
           (pt.Y <= this.Bottom));
        }
        /// <summary>
        /// intersect rectangle
        /// </summary>
        /// <param name="rc"></param>
        public void Intersect(Rectanglef rc1)
        {
            Rectanglef r = Intersect(this, rc1);
            this.m_x = r.X;
            this.m_y = r.Y;
            this.m_w = r.Width ;
            this.m_h = r.Height;

        }
        /// <summary>
        /// intersect rectangle
        /// </summary>
        /// <param name="rc"></param>
        public static Rectanglef  Intersect(Rectanglef rc1, Rectanglef rc2)
        {
            float H = rc1.Height + rc2.Height;
            float W = rc1.Width + rc2.Width;
            float minx = Math.Min(rc1.X, rc2.X);
            float maxx = Math.Max(rc1.X + rc1.Width, rc2.X + rc2.Width);
            float miny = Math.Min(rc1.Y, rc2.Y);
            float maxy = Math.Max(rc1.Y + rc1.Height, rc2.Y + rc2.Height);
            float w1 = W - (maxx - minx);
            float h1 = H - (maxy - miny);
            if (
                (w1 >= 0)
                &&
                (h1 >= 0)
                )
            {
                float x = Math.Min(rc1.Right, rc2.Right) - w1;
                float y = Math.Min(rc1.Bottom, rc2.Bottom) - h1;
                return new Rectanglef(x, y, w1, h1);
            }
            return Rectanglef.Empty;
        }
    }
}

