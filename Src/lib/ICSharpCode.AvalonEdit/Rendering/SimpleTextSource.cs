

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: SimpleTextSource.cs
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
file:SimpleTextSource.cs
*/
// Copyright (c) AlphaSierraPapa for the SharpDevelop Team (for details please see \doc\copyright.txt)
// This code is distributed under the GNU LGPL (for details please see \doc\license.txt)
using System;
using System.Diagnostics;
using System.Windows.Media.TextFormatting;
namespace ICSharpCode.AvalonEdit.Rendering
{
	sealed class SimpleTextSource : TextSource
	{
		readonly string text;
		readonly TextRunProperties properties;
		public SimpleTextSource(string text, TextRunProperties properties)
		{
			this.text = text;
			this.properties = properties;
		}
		public override TextRun GetTextRun(int textSourceCharacterIndex)
		{
			if (textSourceCharacterIndex < text.Length)
				return new TextCharacters(text, textSourceCharacterIndex, text.Length - textSourceCharacterIndex, properties);
			else
				return new TextEndOfParagraph(1);
		}
		public override int GetTextEffectCharacterIndexFromTextSourceCharacterIndex(int textSourceCharacterIndex)
		{
			throw new NotImplementedException();
		}
		public override TextSpan<CultureSpecificCharacterBufferRange> GetPrecedingText(int textSourceCharacterIndexLimit)
		{
			throw new NotImplementedException();
		}
	}
}

