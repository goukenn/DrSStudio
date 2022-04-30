

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: HighlightingSpan.cs
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
file:HighlightingSpan.cs
*/
// Copyright (c) AlphaSierraPapa for the SharpDevelop Team (for details please see \doc\copyright.txt)
// This code is distributed under the GNU LGPL (for details please see \doc\license.txt)
using System;
using System.Text.RegularExpressions;
namespace ICSharpCode.AvalonEdit.Highlighting
{
	/// <summary>
	/// A highlighting span is a region with start+end expression that has a different RuleSet inside
	/// and colors the region.
	/// </summary>
	[Serializable]
	public class HighlightingSpan
	{
		/// <summary>
		/// Gets/Sets the start expression.
		/// </summary>
		public Regex StartExpression { get; set; }
		/// <summary>
		/// Gets/Sets the end expression.
		/// </summary>
		public Regex EndExpression { get; set; }
		/// <summary>
		/// Gets/Sets the rule set that applies inside this span.
		/// </summary>
		public HighlightingRuleSet RuleSet { get; set; }
		/// <summary>
		/// Gets the color used for the text matching the start expression.
		/// </summary>
		public HighlightingColor StartColor { get; set; }
		/// <summary>
		/// Gets the color used for the text between start and end.
		/// </summary>
		public HighlightingColor SpanColor { get; set; }
		/// <summary>
		/// Gets the color used for the text matching the end expression.
		/// </summary>
		public HighlightingColor EndColor { get; set; }
		/// <inheritdoc/>
		public override string ToString()
		{
			return "[" + GetType().Name + " Start=" + StartExpression + ", End=" + EndExpression + "]";
		}
	}
}

