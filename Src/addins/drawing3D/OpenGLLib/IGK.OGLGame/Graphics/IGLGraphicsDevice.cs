

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: IGLGraphicsDevice.cs
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
file:IGLGraphicsDevice.cs
*/
using System;
namespace IGK.OGLGame
{
	/// <summary>
	/// represent graphics device
	/// </summary>
	public interface IGLGraphicsDevice : IDisposable 
	{
		/// <summary>
		/// Gets the GL hdc.
		/// </summary>
		IntPtr HGLDC{get;}
		/// <summary>
		/// Makes the current.
		/// </summary>
		void MakeCurrent();
	}
}

