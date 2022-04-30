

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: Vector4i.cs
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
file:Vector4i.cs
*/

﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
namespace IGK.ICore
{
    /// <summary>
    /// represent a rectangle struct 
    /// </summary>
    [StructLayout(LayoutKind.Sequential )]
    public struct Vector4i
    {
        private int m_X;
        private int m_Y;
        private int m_Width;
        private int m_Height;
        public int Width
        {
            get { return m_Width; }
            set { m_Width = value; }
        }
        public int Height
        {
            get { return m_Height; }
            set { m_Height = value; }
        }
        public int Y
        {
            get { return m_Y; }
            set { m_Y = value; }
        }
        public int X
        {
            get { return m_X; }
            set { m_X = value; }
        }
        public int Left{
            get { return m_X; }
        }
        public int Top
        {
            get { return m_Y; }
        }
        public int Bottom
        {
            get { return m_Y + m_Height ; }
        }
        public int Right {
            get { return m_X + m_Width; }
        }
        public static readonly Vector4i Empty;
        static Vector4i()
        {
            Empty = new Vector4i();
        }
        public override string ToString()
        {
            return string.Format("x:{0} y:{1} width:{2} height:{3} ",
                this.X,
                this.Y,
                this.Width,
                this.Height);
        }
        public Vector4i(int x, int y, int width, int height)
        {
            this.m_X  = x;
            this.m_Y  = y;
            this.m_Height  = height ;
            this.m_Width = width ;
        }
    }
}

