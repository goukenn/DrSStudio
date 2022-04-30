

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: Size2i.cs
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
file:Size2i.cs
*/

﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.ComponentModel;
using IGK.ICore;using IGK.ICore.ComponentModel;
namespace IGK.ICore
{
    [StructLayout(LayoutKind.Sequential)]
    [TypeConverter(typeof(Size2iConverter))]
    public struct Size2i
    {
        private int m_Width;
        private int m_Height;
        public static readonly Size2i Empty;
        public bool IsEmpty {
            get {
                return this.Equals(Empty);
            }
        }
        static Size2i()
        {
            Empty = new Size2i();
        }
        public int Height
        {
            get { return m_Height; }
            set
            {
                if (m_Height != value)
                {
                    m_Height = value;
                }
            }
        }
        public int Width
        {
            get { return m_Width; }
            set
            {
                if (m_Width != value)
                {
                    m_Width = value;
                }
            }
        }
        public Size2i(int w, int h)
        {
            this.m_Height = h;
            this.m_Width = w;
        }
        public static Size2i operator /(Size2i Size2i, int i)
        {
            if (i == 0)
                throw new System.DivideByZeroException();
            return new Size2i(Size2i.Width / i, Size2i.Height / i);
        }
        public static Size2i operator *(Size2i Size2i, int i)
        {
            return new Size2i(Size2i.Width * i, Size2i.Height * i);
        }
        public static Size2i operator -(Size2i Size2i)
        {
            return new Size2i(-Size2i.Width, -Size2i.Height);
        }
        public override string ToString()
        {
            return string.Format("{0};{1}", this.Width, this.Height);
        }
        public static Size2i ConvertFromString(string p)
        {
            string[] v_tb = p.Split(';', ' ');
            if (v_tb.Length == 2)
            {
                Size2i v_t = new Size2i(
                    int.Parse(v_tb[0]),
                    int.Parse(v_tb[1]));
                return v_t;
            }
            return Size2i.Empty;
        }
    }
}

