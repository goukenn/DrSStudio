

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: GLLinuxDevice.cs
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
file:GLLinuxDevice.cs
*/
using System;
using System.Drawing ;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices ;
using System.Collections.Generic ;

//#pragma warning disable 410

namespace IGK.GLLib
{
#if UNIX
	public class GLWindowDevice : IGLGraphicDevice
	{
		IntPtr m_display;
		IntPtr m_drawable;
		IntPtr m_context;
		public bool IsCurrent{
			get{
				return (this.m_context == XGL.glXGetCurrentContext ());
			}
		}
		internal  GLWindowDevice(IntPtr display, IntPtr drawable, IntPtr context)
		{
			this.m_display = display;
			this.m_drawable = drawable;
			this.m_context = context;
		}
		/// <summary>
		/// Gets the X fonts.
		/// </summary>
		/// <returns>
		/// The X fonts.
		/// </returns>
		public string[] GetXFonts(){
			int numfonts = 0;
			IntPtr t = XLib.XListFonts(this.m_display ,"*" , -1, ref numfonts);		
			List<string> m_l = new List<string>  ();
			string ts = null;
			for (int i = 0; i < numfonts; i++) {
					ts  = Marshal.PtrToStringAnsi (Marshal.ReadIntPtr (t, i*8 ));
					m_l.Add (ts);
			}
			XLib.XFreeFontNames (t);			
			return m_l.ToArray ();
		}
		public ISpriteFontInfo CreateSpriteFont(string fontname, float fontsize, int fontstyle)
		{
			string[] ts = GetXFonts ();
			Font ft = new Font(fontname, fontsize , (FontStyle)fontstyle, GraphicsUnit.Pixel);
		        //XFont v_ft = null;
			IntPtr c = XLib.XLoadQueryFont(this.m_display , ts[30] );
			XLib.XFONTStruct v_hft =  (XLib.XFONTStruct)Marshal.PtrToStructure (c, typeof (XLib.XFONTStruct ));
            uint v_lbase = GL.glGenLists(256);
			IntPtr  hfont = v_hft.fid ;
            //GLYPHMETRICSFLOAT[] gv = new GLYPHMETRICSFLOAT[256];
            //IntPtr alloc = Marshal.AllocCoTaskMem(256 * Marshal.SizeOf(typeof(GLYPHMETRICSFLOAT)));
            //IntPtr hfont = Marshal.AllocCoTaskMem (Marshal.SizeOf (v_hft ));
			//Marshal.StructureToPtr (v_hft, hfont , true);
            SpriteFontInfo s = null;
        	XGL.glXUseXFont (hfont, 0, 255, (int)v_lbase);
            if (true)
            {
                s = new SpriteFontInfo();
				s.Name = fontname;
              /*  s.m_name = ft.Name;
                s.m_fontsize = ft.Size;
                s.m_unit = ft.Unit;
                s.m_font = v_font;
                s.m_listbase =  v_lbase;
                s.m_RangeMin = 0;
                s.m_RangeMax = 255;
                s.m_lineSpacing =(int) ft.GetHeight();*/
            }
            else
            {
                //GL.glDeleteLists(v_lbase, 256);                
            }           
            return s;
		}
		public  bool SwapBuffers()
		{
			XGL.glXSwapBuffers(this.m_display, this.m_drawable);
			return true;
		}
		public bool MakeCurrent()
		{
			return XGL.glXMakeContextCurrent(this.m_display,
			                                 this.m_drawable,
			                                 this.m_drawable,
			                                 this.m_context);
		}
		public void Dispose(){
		}
        public bool SwapBuffers(IntPtr hdc)
        {
            return false;
        }
    }
#endif
}

