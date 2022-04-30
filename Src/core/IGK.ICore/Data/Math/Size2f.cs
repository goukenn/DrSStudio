

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: Size2f.cs
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
file:Size2f.cs
*/

﻿using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Runtime.InteropServices;
using IGK.ICore;using IGK.ICore.ComponentModel;
using IGK.ICore.Data.Math;

namespace IGK.ICore
{
    [StructLayout (LayoutKind.Sequential )]
    [TypeConverter (typeof (Size2fConverter))]
   public  struct Size2f : ISize2f
    {
        private float m_Width;
        private float  m_Height;
        public static readonly Size2f Empty;
        public static readonly Size2f MaxShortSize;
        static Size2f(){ 
 Empty = new Size2f ();
            MaxShortSize = new Size2f (short.MaxValue, short.MaxValue );
}
        public float  Height
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
        public float  Width
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
        public Size2f(float w,float  h)
        {
            this.m_Height = h;
            this.m_Width = w;
        }
        public static implicit operator Size2f (Size2i size)
        {
            return new Size2f (size.Width, size.Height );
        }
        public override string ToString()
        {
            return string.Format("{0};{1}", this.Width, this.Height);
        }
        public static Size2f ConvertFromString(string p)
        {
            string[] v_tb = p.Split(';', ' ');
            if (v_tb.Length == 2)
            {
                Size2f v_t = new Size2f(
                    float.Parse(v_tb[0]),
                    float.Parse(v_tb[1]));
                return v_t;
            }
            return Size2f.Empty;
        }
    }
}

