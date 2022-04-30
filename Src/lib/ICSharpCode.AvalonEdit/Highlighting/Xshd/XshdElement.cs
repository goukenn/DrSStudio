

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: XshdElement.cs
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
file:XshdElement.cs
*/
// Copyright (c) AlphaSierraPapa for the SharpDevelop Team (for details please see \doc\copyright.txt)
// This code is distributed under the GNU LGPL (for details please see \doc\license.txt)
using System;
namespace ICSharpCode.AvalonEdit.Highlighting.Xshd
{
	/// <summary>
	/// An element in a XSHD rule set.
	/// </summary>
	[Serializable]
	public abstract class XshdElement
	{
		/// <summary>
		/// Gets the line number in the .xshd file.
		/// </summary>
		public int LineNumber { get; set; }
		/// <summary>
		/// Gets the column number in the .xshd file.
		/// </summary>
		public int ColumnNumber { get; set; }
		/// <summary>
		/// Applies the visitor to this element.
		/// </summary>
		public abstract object AcceptVisitor(IXshdVisitor visitor);
	}
}

