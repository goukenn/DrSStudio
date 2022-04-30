

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: XshdSpan.cs
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
file:XshdSpan.cs
*/
// Copyright (c) AlphaSierraPapa for the SharpDevelop Team (for details please see \doc\copyright.txt)
// This code is distributed under the GNU LGPL (for details please see \doc\license.txt)
using System;
namespace ICSharpCode.AvalonEdit.Highlighting.Xshd
{
	/// <summary>
	/// Specifies the type of the regex.
	/// </summary>
	public enum XshdRegexType
	{
		/// <summary>
		/// Normal regex. Used when the regex was specified as attribute.
		/// </summary>
		Default,
		/// <summary>
		/// Ignore pattern whitespace / allow regex comments. Used when the regex was specified as text element.
		/// </summary>
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly",
		                                                 Justification = "Using the same case as the RegexOption")]
		IgnorePatternWhitespace
	}
	/// <summary>
	/// &lt;Span&gt; element.
	/// </summary>
	[Serializable]
	public class XshdSpan : XshdElement
	{
		/// <summary>
		/// Gets/sets the begin regex.
		/// </summary>
		public string BeginRegex { get; set; }
		/// <summary>
		/// Gets/sets the begin regex type.
		/// </summary>
		public XshdRegexType BeginRegexType { get; set; }
		/// <summary>
		/// Gets/sets the end regex.
		/// </summary>
		public string EndRegex { get; set; }
		/// <summary>
		/// Gets/sets the end regex type.
		/// </summary>
		public XshdRegexType EndRegexType { get; set; }
		/// <summary>
		/// Gets/sets whether the span is multiline.
		/// </summary>
		public bool Multiline { get; set; }
		/// <summary>
		/// Gets/sets the rule set reference.
		/// </summary>
		public XshdReference<XshdRuleSet> RuleSetReference { get; set; }
		/// <summary>
		/// Gets/sets the span color.
		/// </summary>
		public XshdReference<XshdColor> SpanColorReference { get; set; }
		/// <summary>
		/// Gets/sets the span begin color.
		/// </summary>
		public XshdReference<XshdColor> BeginColorReference { get; set; }
		/// <summary>
		/// Gets/sets the span end color.
		/// </summary>
		public XshdReference<XshdColor> EndColorReference { get; set; }
		/// <inheritdoc/>
		public override object AcceptVisitor(IXshdVisitor visitor)
		{
			return visitor.VisitSpan(this);
		}
	}
}

