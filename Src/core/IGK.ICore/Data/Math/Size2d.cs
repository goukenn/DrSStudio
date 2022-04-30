

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: Size2d.cs
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
file:Size2d.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
namespace IGK.ICore
{
     [StructLayout(LayoutKind.Sequential)]  
    public struct Size2d
    {
           private double  m_Width;
        private double  m_Height;
        public static readonly Size2d Empty;
        static Size2d(){ 
 Empty = new Size2d ();
}
        public double  Height
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
        public double  Width
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
        public Size2d(float w,float  h)
        {
            this.m_Height = h;
            this.m_Width = w;
        }
        public override string ToString()
        {
            return string.Format("{0};{1}", this.Width, this.Height);
        }
        public static Size2d ConvertFromString(string p)
        {
            string[] v_tb = p.Split(';', ' ');
            if (v_tb.Length == 2)
            {
                Size2d v_t = new Size2d(
                    float.Parse(v_tb[0]),
                    float.Parse(v_tb[1]));
                return v_t;
            }
            return Size2d.Empty;
        }
    }
}

