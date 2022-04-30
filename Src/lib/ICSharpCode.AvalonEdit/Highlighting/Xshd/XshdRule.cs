

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: XshdRule.cs
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
file:XshdRule.cs
*/
// Copyright (c) AlphaSierraPapa for the SharpDevelop Team (for details please see \doc\copyright.txt)
// This code is distributed under the GNU LGPL (for details please see \doc\license.txt)
using System;
namespace ICSharpCode.AvalonEdit.Highlighting.Xshd
{
	/// <summary>
	/// &lt;Rule&gt; element.
	/// </summary>
	[Serializable]
	public class XshdRule : XshdElement
	{
		/// <summary>
		/// Gets/sets the rule regex.
		/// </summary>
		public string Regex { get; set; }
		/// <summary>
		/// Gets/sets the rule regex type.
		/// </summary>
		public XshdRegexType RegexType { get; set; }
		/// <summary>
		/// Gets/sets the color reference.
		/// </summary>
		public XshdReference<XshdColor> ColorReference { get; set; }
		/// <inheritdoc/>
		public override object AcceptVisitor(IXshdVisitor visitor)
		{
			return visitor.VisitRule(this);
		}
	}
}

