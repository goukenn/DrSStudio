

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: HighlightingRule.cs
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
file:HighlightingRule.cs
*/
// Copyright (c) AlphaSierraPapa for the SharpDevelop Team (for details please see \doc\copyright.txt)
// This code is distributed under the GNU LGPL (for details please see \doc\license.txt)
using System;
using System.Text.RegularExpressions;
namespace ICSharpCode.AvalonEdit.Highlighting
{
	/// <summary>
	/// A highlighting rule.
	/// </summary>
	[Serializable]
	public class HighlightingRule
	{
		/// <summary>
		/// Gets/Sets the regular expression for the rule.
		/// </summary>
		public Regex Regex { get; set; }
		/// <summary>
		/// Gets/Sets the highlighting color.
		/// </summary>
		public HighlightingColor Color { get; set; }
		/// <inheritdoc/>
		public override string ToString()
		{
			return "[" + GetType().Name + " " + Regex + "]";
		}
	}
}

