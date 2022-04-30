

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: SnippetContainerElement.cs
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
file:SnippetContainerElement.cs
*/
// Copyright (c) AlphaSierraPapa for the SharpDevelop Team (for details please see \doc\copyright.txt)
// This code is distributed under the GNU LGPL (for details please see \doc\license.txt)
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Documents;
using ICSharpCode.AvalonEdit.Editing;
using ICSharpCode.AvalonEdit.Utils;
namespace ICSharpCode.AvalonEdit.Snippets
{
	/// <summary>
	/// A snippet element that has sub-elements.
	/// </summary>
	[Serializable]
	public class SnippetContainerElement : SnippetElement
	{
		NullSafeCollection<SnippetElement> elements = new NullSafeCollection<SnippetElement>();
		/// <summary>
		/// Gets the list of child elements.
		/// </summary>
		public IList<SnippetElement> Elements {
			get { return elements; }
		}
		/// <inheritdoc/>
		public override void Insert(InsertionContext context)
		{
			foreach (SnippetElement e in this.Elements) {
				e.Insert(context);
			}
		}
		/// <inheritdoc/>
		public override Inline ToTextRun()
		{
			Span span = new Span();
			foreach (SnippetElement e in this.Elements) {
				Inline r = e.ToTextRun();
				if (r != null)
					span.Inlines.Add(r);
			}
			return span;
		}
	}
}

