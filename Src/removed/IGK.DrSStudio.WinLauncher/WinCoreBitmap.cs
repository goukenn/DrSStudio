

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: WinCoreBitmap.cs
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
file:WinCoreBitmap.cs
*/
using IGK.ICore;using IGK.DrSStudio.Drawing2D;
using IGK.DrSStudio.Rendering;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace IGK.DrSStudio.WinLauncher
{
    class WinCoreBitmap : MarshalByRefObject ,  ICoreBitmap
    {
        private Bitmap m_bitmap;
        private enuPixelFormat m_PixelFormat;
        public WinCoreBitmap()
        {
        }
        internal static WinCoreBitmap Create(int width, int height)
        {
            Bitmap v_bitmap = new Bitmap (width, height ,   System.Drawing.Imaging.PixelFormat.Format32bppArgb );
            WinCoreBitmap bmp = new WinCoreBitmap ();
            bmp.m_bitmap = v_bitmap;
            bmp.m_PixelFormat = enuPixelFormat.Format32bppArgb;
            return bmp;
        }
        public ICoreGraphics CreateDevice()
        {
            Graphics g = Graphics.FromImage(m_bitmap);
            return WinCoreGraphics.Create(g);
        }
        public int Height
        {
            get { return this.m_bitmap.Height; }
        }
        public enuPixelFormat PixelFormat
        {
            get { return m_PixelFormat; }
        }
        public int With
        {
            get { return this.m_bitmap.Width ; }
        }
        public void Dispose()
        {
            this.m_bitmap.Dispose();
            this.m_bitmap = null;
            this.m_PixelFormat = enuPixelFormat.Undefined;
        }
    }
}

