

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: IBackgroundRenderer.cs
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
file:IBackgroundRenderer.cs
*/
// Copyright (c) AlphaSierraPapa for the SharpDevelop Team (for details please see \doc\copyright.txt)
// This code is distributed under the GNU LGPL (for details please see \doc\license.txt)
using System;
using System.Windows.Media;
namespace ICSharpCode.AvalonEdit.Rendering
{
	/// <summary>
	/// Background renderers draw in the background of a known layer.
	/// You can use background renderers to draw non-interactive elements on the TextView
	/// without introducing new UIElements.
	/// </summary>
	public interface IBackgroundRenderer
	{
		/// <summary>
		/// Gets the layer on which this background renderer should draw.
		/// </summary>
		KnownLayer Layer { get; }
		/// <summary>
		/// Causes the background renderer to draw.
		/// </summary>
		void Draw(TextView textView, DrawingContext drawingContext);
	}
}

