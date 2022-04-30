

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: GLWindowDevice.cs
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
file:GLWindowDevice.cs
*/
using System;
using System.Drawing;
using System.Runtime.InteropServices;
namespace IGK.GLLib
{
#if OS_WINDOWS
	public class GLWindowDevice : IGLGraphicDevice
	{
        IntPtr m_hdc;
        IntPtr m_gldc;
        IntPtr m_hwnd;
		public void Dispose(){
            User32Lib.ReleaseDC(this.m_hwnd, this.m_hdc);
            WGL.wglDeleteContext(this.m_gldc);
            //free resources
            this.m_gldc = IntPtr.Zero;
            this.m_hdc = IntPtr.Zero ;
            this.m_hwnd = IntPtr.Zero;
		}
		public ISpriteFontInfo CreateSpriteFont(string fontname, float fontsize, int fontstyle)
		{
			Font ft = new Font(fontname, 14,(FontStyle ) fontstyle, GraphicsUnit.Pixel);
            IntPtr HDC = this.m_hdc;
            OGLFont v_font = new  OGLFont(HDC,ft, 14,10);
            uint v_lbase = GL.glGenLists (256);
            GLYPHMETRICSFLOAT[] gv = new GLYPHMETRICSFLOAT[256];
            IntPtr alloc = Marshal.AllocCoTaskMem(256 * Marshal.SizeOf(typeof(GLYPHMETRICSFLOAT)));
            SpriteFontInfo s = null;
            if (WGL.wglUseFontBitmaps (HDC,0,255,v_lbase))
            {
                s = new SpriteFontInfo();
                s.Name = ft.Name;
                /*s.m_fontsize = ft.Size;
                s.m_unit = ft.Unit;
                s.m_font = v_font;
                s.m_listbase =  v_lbase;
                s.m_RangeMin = 0;
                s.m_RangeMax = 255;
                s.m_lineSpacing =(int) ft.GetHeight();*/
            }
            else
            {
                GL.glDeleteLists(v_lbase, 256);                
            }
            Marshal.FreeCoTaskMem(alloc);
            ft.Dispose();
            return s;
		}
        public  bool SwapBuffers()
		{
			return this.SwapBuffers(this.m_hdc);
		}
		public  bool SwapBuffers(IntPtr hdc)
		{
			bool v = Gdi32Lib.SwapBuffers(hdc);
			return v;
		}
		public bool MakeCurrent()
		{
            if (this.IsCurrent) return true;
            //check error before changing context
            if (GL.MakeCurrent(this.m_hdc , this.m_gldc ))
            {
                //OnReceiveContext(EventArgs.Empty);
            }
            else
            {
                var error = GL.glGetError();
                System.Diagnostics.Debug.WriteLine("MakeCurrent failed : " + error);
            }
            return true;
		}
		internal GLWindowDevice (IntPtr hwnd, IntPtr hdc, IntPtr gldc)
		{
            this.m_hwnd = hwnd;
            this.m_hdc = hdc;
            this.m_gldc = gldc;
		}
        public bool IsCurrent
        {
            get { return (this.m_gldc == WGL.wglGetCurrentContext()); }
        }
    }
#endif
}

