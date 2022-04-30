

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: Rectangled.cs
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
file:Rectangled.cs
*/

﻿using IGK.ICore;using IGK.ICore.ComponentModel;
using System;
using System.Collections.Generic;
using System.Text;
namespace IGK.ICore
{
    [System.ComponentModel.TypeConverter(typeof(RectangledTypeConverter))]
    public struct Rectangled
    {
        double  m_x;
        double m_y;
        double m_w;
        double m_h;
        public static Rectangled Empty;
        public bool IsEmpty
        {
            get
            {
                return ((m_w == 0) && (m_h == 0));
            }
        }
        static Rectangled()
        {
            Empty = new Rectangled();
        }
        public Rectangled(double x, double y, double width, double height)
        {
            this.m_x = x;
            this.m_y = y;
            this.m_w = width;
            this.m_h = height;
        }
        #region IVector2f Members
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
        public double Y
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
        public double Width
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
        public double Height
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
        public double Right
        {
            get { return this.m_x + this.m_w; }
        }
        public double Bottom
        {
            get { return this.m_y + this.m_h; }
        }
        public double Top
        {
            get { return this.m_y; }
        }
        public double Left
        {
            get { return this.m_x; }
        }
public Vector2d Center{get{return new Vector2d ( m_x + m_w /2.0, m_y + m_h /2.0);}}
        #endregion
        /// <summary>
        /// return location
        /// </summary>
        public Vector2d Location
        {
            get
            {
                return new Vector2d(this.m_x, this.m_y);
            }
            set{
                this.m_x = value.X;
                this.m_y = value.Y;
            }
        }
        public Vector2d Size2i
        {
            get
            {
                return new Vector2d(this.m_w, this.m_h);
            }
        }
        public static implicit operator Rectangled(Rectanglei rec)
        {
            return new Rectangled(
                rec.X,
                rec.Y,
                rec.Width,
                rec.Height);
        }
     //glei Round(Rectangled rec)
     //   {
     //       return new Rectanglei(
     //           (int)global::System.Math.Round(rec.X),
     //           (int)global::System.Math.Round(rec.Y),
     //          (int)global::System.Math.Round(rec.Width),
     //           (int)global::System.Math.Round(rec.Height));
     //   }
        public static Rectanglei Ceiling(Rectangled rec)
        {
            return new Rectanglei(
                (int)global::System.Math.Ceiling(rec.X),
                (int)global::System.Math.Ceiling(rec.Y),
               (int)global::System.Math.Ceiling(rec.Width),
                (int)global::System.Math.Ceiling(rec.Height));
        }
        public static string ConvertToString(Rectangled v_rc)
        {
            return string.Format("{0};{1};{2};{3}",
                v_rc.X,
                v_rc.Y,
                v_rc.Width,
                v_rc.Height);
        }
        public static Rectangled ConvertFromString(string p)
        {
            p = p.Trim();
            string[] t = p.Split(';', ' ');
            if (t.Length == 4)
            {
                return new Rectangled(
                    Convert.ToSingle(t[0]),
                    Convert.ToSingle(t[1]),
                    Convert.ToSingle(t[2]),
                    Convert.ToSingle(t[3]));
            }
            return Rectangled.Empty;
        }
        public override string ToString()
        {
            return string.Format("{0};{1};{2};{3}",
                this.X,
                this.Y,
                this.Width,
                this.Height);
        }
        public void Inflate(double  x, double y)
        {
            this.m_x -= x;
            this.m_w += 2 * x;
            this.m_y -= y;
            this.m_h += 2 * y;
        }
        /// <summary>
        /// intersect rectangle
        /// </summary>
        /// <param name="rc"></param>
        public void Intersect(Rectangled rc1)
        {
            Rectangled r = Intersect(this, rc1);
            this.m_x = r.X;
            this.m_y = r.Y;
            this.m_w = r.Height;
            this.m_h = r.Height;

        }
        /// <summary>
        /// intersect rectangle
        /// </summary>
        /// <param name="rc"></param>
        public static Rectangled Intersect(Rectangled rc1, Rectangled rc2)
        {
            double H = rc1.Height + rc2.Height;
            double W = rc1.Width + rc2.Width;
            double minx = Math.Min(rc1.X, rc2.X);
            double maxx = Math.Max(rc1.X + rc1.Width, rc2.X + rc2.Width);
            double miny = Math.Min(rc1.Y, rc2.Y);
            double maxy = Math.Max(rc1.Y + rc1.Height, rc2.Y + rc2.Height);
            double w1 = W - (maxx - minx);
            double h1 = H - (maxy - miny);
            if (
                (w1 >= 0)
                &&
                (h1 >= 0)
                )
            {
                double k = Math.Min(rc1.Right, rc2.Right) - w1;
                double r = Math.Min(rc1.Bottom, rc2.Bottom) - h1;
                return new Rectangled(k, r, w1, h1);
            }
            return Rectangled.Empty;
        }

        public bool Contains(Vector2d pt)
        {
            return ((pt.X >= this.X) &&
           (pt.X <= this.Right) &&
           (pt.Y >= this.Y) &&
           (pt.Y <= this.Bottom));
        }
    }
}

