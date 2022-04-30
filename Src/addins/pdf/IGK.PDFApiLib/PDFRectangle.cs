

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: PDFRectangle.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
/*
IGKDEV - [2008 - 2014]
author: C.A.D . BONDJE DOUE
file:PDFRectangle.cs
*/
/* 
-------------------------------------------------------------------
Company: IGK-DEV
Author : C.A.D. BONDJE DOUE
SITE : http://www.igkdev.be
Application : DrSStudio
powered by IGK - DEV &copy; 2008-2012
THIS FILE IS A PART OF THE DRSSTUDIO APPLICATION. SEE "License.txt"
FOR MORE INFORMATION ABOUT THE LICENSE
------------------------------------------------------------------- 
*/
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace IGK.PDFApi
{
    public class PDFRectangle : PDFDataBase 
    {
        private float m_X;
        private float m_Y;
        private float m_Width;
        private float m_Height;
        public PDFRectangle(float x, float y, float width, float height)
        {
            this.m_X = x;
            this.m_Y = y;
            this.m_Width = width;
            this.m_Height = height;
        }
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
        public override void Render(System.IO.Stream stream)
        {
            Utils.TextUtils.WriteString(stream, "[");
            Utils.TextUtils.WriteString(stream, this.X.ToString() );
            stream.WriteByte(PDFConstant.SPACE);
            Utils.TextUtils.WriteString(stream, this.Y.ToString());
            stream.WriteByte(PDFConstant.SPACE);
            Utils.TextUtils.WriteString(stream, this.Width.ToString());
            stream.WriteByte(PDFConstant.SPACE);
            Utils.TextUtils.WriteString(stream, this.Height.ToString());
            Utils.TextUtils.WriteString(stream, "]");
        }
    }
}

