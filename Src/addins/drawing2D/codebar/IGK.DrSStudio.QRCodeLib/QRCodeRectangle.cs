

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: QRCodeRectangle.cs
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
file:QRCodeRectangle.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace IGK.QRCodeLib
{
    public struct QRCodeRectangle
    {
        private float m_X;
        private float m_Y;
        private float m_Width;
        private float m_Height;
        public float Height
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
        public float Width
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
        public float Y
        {
            get { return m_Y; }
            set
            {
                if (m_Y != value)
                {
                    m_Y = value;
                }
            }
        }
        public float X
        {
            get { return m_X; }
            set
            {
                if (m_X != value)
                {
                    m_X = value;
                }
            }
        }
        public QRCodeRectangle(float x, float y, float width, float height)
        {
            this.m_Height = height;
            this.m_Width = width;
            this.m_X = x;
            this.m_Y = y;
        }
    }
}

