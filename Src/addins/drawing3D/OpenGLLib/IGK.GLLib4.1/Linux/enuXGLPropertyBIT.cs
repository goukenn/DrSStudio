

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: enuXGLPropertyBIT.cs
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
file:enuXGLPropertyBIT.cs
*/
using System;
namespace IGK.GLLib
{
	public enum  enuXGLPropertyBIT : int
	{
		Window = XGL.  GLX_WINDOW_BIT	,
  Pixmap = XGL. GLX_PIXMAP_BIT			,
  Buffer = XGL. GLX_PBUFFER_BIT			,
  AuxBuffers = XGL. GLX_AUX_BUFFERS_BIT		,
  LeftBuffer = XGL. GLX_FRONT_LEFT_BUFFER_BIT	,
  RightBuffer = XGL. GLX_FRONT_RIGHT_BUFFER_BIT,
  BackLeftBuffer = XGL. GLX_BACK_LEFT_BUFFER_BIT	,
  BackRightBuffer = XGL. GLX_BACK_RIGHT_BUFFER_BIT	,
  DepthBuffer = XGL. GLX_DEPTH_BUFFER_BIT	,	
  StencilBiffer = XGL. GLX_STENCIL_BUFFER_BIT,
  AccumBuffer = XGL. GLX_ACCUM_BUFFER_BIT	,
		RGBA = XGL. GLX_RGBA_BIT,
Index = XGL.  GLX_COLOR_INDEX_BIT
	}
}

