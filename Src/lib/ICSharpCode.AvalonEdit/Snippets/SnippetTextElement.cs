

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: SnippetTextElement.cs
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
file:SnippetTextElement.cs
*/
// Copyright (c) AlphaSierraPapa for the SharpDevelop Team (for details please see \doc\copyright.txt)
// This code is distributed under the GNU LGPL (for details please see \doc\license.txt)
using System;
using System.Windows.Documents;
using ICSharpCode.AvalonEdit.Document;
namespace ICSharpCode.AvalonEdit.Snippets
{
	/// <summary>
	/// Represents a text element in a snippet.
	/// </summary>
	[Serializable]
	public class SnippetTextElement : SnippetElement
	{
		string text;
		/// <summary>
		/// The text to be inserted.
		/// </summary>
		public string Text {
			get { return text; }
			set { text = value; }
		}
		/// <inheritdoc/>
		public override void Insert(InsertionContext context)
		{
			if (text != null)
				context.InsertText(text);
		}
		/// <inheritdoc/>
		public override Inline ToTextRun()
		{
			return new Run(text ?? string.Empty);
		}
	}
}

