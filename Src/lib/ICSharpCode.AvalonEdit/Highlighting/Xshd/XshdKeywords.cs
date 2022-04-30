

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: XshdKeywords.cs
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
file:XshdKeywords.cs
*/
// Copyright (c) AlphaSierraPapa for the SharpDevelop Team (for details please see \doc\copyright.txt)
// This code is distributed under the GNU LGPL (for details please see \doc\license.txt)
using System;
using System.Collections.Generic;
using ICSharpCode.AvalonEdit.Utils;
namespace ICSharpCode.AvalonEdit.Highlighting.Xshd
{
	/// <summary>
	/// A list of keywords.
	/// </summary>
	[Serializable]
	public class XshdKeywords : XshdElement
	{
		/// <summary>
		/// The color.
		/// </summary>
		public XshdReference<XshdColor> ColorReference { get; set; }
		readonly NullSafeCollection<string> words = new NullSafeCollection<string>();
		/// <summary>
		/// Gets the list of key words.
		/// </summary>
		public IList<string> Words {
			get { return words; }
		}
		/// <inheritdoc/>
		public override object AcceptVisitor(IXshdVisitor visitor)
		{
			return visitor.VisitKeywords(this);
		}
	}
}

