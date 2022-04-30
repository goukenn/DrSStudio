

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: VisualLineConstructionStartEventArgs.cs
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
file:VisualLineConstructionStartEventArgs.cs
*/
// Copyright (c) AlphaSierraPapa for the SharpDevelop Team (for details please see \doc\copyright.txt)
// This code is distributed under the GNU LGPL (for details please see \doc\license.txt)
using System;
using ICSharpCode.AvalonEdit.Document;
namespace ICSharpCode.AvalonEdit.Rendering
{
	/// <summary>
	/// EventArgs for the <see cref="TextView.VisualLineConstructionStarting"/> event.
	/// </summary>
	public class VisualLineConstructionStartEventArgs : EventArgs
	{
		/// <summary>
		/// Gets/Sets the first line that is visible in the TextView.
		/// </summary>
		public DocumentLine FirstLineInView { get; private set; }
		/// <summary>
		/// Creates a new VisualLineConstructionStartEventArgs instance.
		/// </summary>
		public VisualLineConstructionStartEventArgs(DocumentLine firstLineInView)
		{
			if (firstLineInView == null)
				throw new ArgumentNullException("firstLineInView");
			this.FirstLineInView = firstLineInView;
		}
	}
}

