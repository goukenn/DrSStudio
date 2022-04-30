

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: IGLGraphicDevice.cs
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
file:IGLGraphicDevice.cs
*/
using System;
namespace IGK.GLLib
{
	public interface IGLGraphicDevice : IDisposable 
	{
		bool IsCurrent{get;}
		bool MakeCurrent();
		bool SwapBuffers();
        bool SwapBuffers(IntPtr hdc);
		ISpriteFontInfo CreateSpriteFont(string fontname, float fontsize, int fontstyle);
	}
}

