

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: Localization.cs
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
file:Localization.cs
*/
// Copyright (c) AlphaSierraPapa for the SharpDevelop Team (for details please see \doc\copyright.txt)
// This code is distributed under the GNU LGPL (for details please see \doc\license.txt)
using System;
using System.ComponentModel;
namespace ICSharpCode.AvalonEdit.Search
{
	/// <summary>
	/// Holds default texts for buttons and labels in the SearchPanel. Override properties to add other languages.
	/// </summary>
	public class Localization
	{
		/// <summary>
		/// Default: 'Match case'
		/// </summary>
		public virtual string MatchCaseText {
			get { return "Match case"; }
		}
		/// <summary>
		/// Default: 'Match whole words'
		/// </summary>
		public virtual string MatchWholeWordsText {
			get { return "Match whole words"; }
		}
		/// <summary>
		/// Default: 'Use regular expressions'
		/// </summary>
		public virtual string UseRegexText {
			get { return "Use regular expressions"; }
		}
		/// <summary>
		/// Default: 'Find next (F3)'
		/// </summary>
		public virtual string FindNextText {
			get { return "Find next (F3)"; }
		}
		/// <summary>
		/// Default: 'Find previous (Shift+F3)'
		/// </summary>
		public virtual string FindPreviousText {
			get { return "Find previous (Shift+F3)"; }
		}
		/// <summary>
		/// Default: 'Error: '
		/// </summary>
		public virtual string ErrorText {
			get { return "Error: "; }
		}
		/// <summary>
		/// Default: 'No matches found!'
		/// </summary>
		public virtual string NoMatchesFoundText {
			get { return "No matches found!"; }
		}
	}
}

