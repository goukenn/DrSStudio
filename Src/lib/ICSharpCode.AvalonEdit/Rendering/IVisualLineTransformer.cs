

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: IVisualLineTransformer.cs
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
file:IVisualLineTransformer.cs
*/
// Copyright (c) AlphaSierraPapa for the SharpDevelop Team (for details please see \doc\copyright.txt)
// This code is distributed under the GNU LGPL (for details please see \doc\license.txt)
using System;
using System.Collections.Generic;
namespace ICSharpCode.AvalonEdit.Rendering
{
	/// <summary>
	/// Allows transforming visual line elements.
	/// </summary>
	public interface IVisualLineTransformer
	{
		/// <summary>
		/// Applies the transformation to the specified list of visual line elements.
		/// </summary>
		void Transform(ITextRunConstructionContext context, IList<VisualLineElement> elements);
	}
}

