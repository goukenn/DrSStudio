

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: enuXGLProperty.cs
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
file:enuXGLProperty.cs
*/
using System;
namespace IGK.GLLib
{
	public enum enuXGLProperty : int
	{
		DrawableType = XGL.GLX_DRAWABLE_TYPE,
		RenderType = XGL.GLX_RENDER_TYPE,
		RGBAType =   XGL.GLX_RGBA_TYPE,
		IndexType =  XGL.GLX_COLOR_INDEX_TYPE,
		DoubleBuffer = XGL.GLX_DOUBLEBUFFER,
		RedSize = XGL.GLX_RED_SIZE,
		GreenSize = XGL.GLX_GREEN_SIZE,
		BlueSize = XGL.GLX_BLUE_SIZE,
		None = XGL.GLX_NONE
	}
}

