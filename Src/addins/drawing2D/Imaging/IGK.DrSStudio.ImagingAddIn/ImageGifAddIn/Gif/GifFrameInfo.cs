

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: GifFrameInfo.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
using IGK.ICore.WinCore;
using IGK.ICore;
using IGK.ICore.Menu;
using IGK.ICore.WinUI;
using IGK.ICore.Drawing2D;
using IGK.ICore.Drawing2D.Menu;
using IGK.ICore.Drawing2D.WinUI;
using IGK.DrSStudio;
using IGK.DrSStudio.Menu;
using IGK.DrSStudio.WinUI;
using IGK.DrSStudio.Drawing2D;
using IGK.DrSStudio.Drawing2D.WinUI;
/*
IGKDEV @ 2008-2016
author: C.A.D . BONDJE DOUE
file:GifFrameInfo.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace IGK.DrSStudio.GifAddIn.Gif
{
    /// <summary>
    /// represent a frame info
    /// </summary>
    public sealed class GifFrameInfo
    {
        private int m_Width;
        private int m_Height;
        private byte[][] m_Data;
        public byte[][] Data
        {
            get { return m_Data; }
        }
        public int Height
        {
            get { return m_Height; }
        }
        public int Width
        {
            get { return m_Width; }
        }
        internal GifFrameInfo()
        {
        }
        internal static GifFrameInfo CreateInfo(int width, int height, byte[][] data)
        {
            GifFrameInfo frm = new GifFrameInfo();
            frm.m_Width = width;
            frm.m_Height = height;
            frm.m_Data = data;
            return frm;
        }
    }
}

