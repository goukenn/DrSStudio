

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: SearchStrategyFactory.cs
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
file:SearchStrategyFactory.cs
*/
// Copyright (c) AlphaSierraPapa for the SharpDevelop Team (for details please see \doc\copyright.txt)
// This code is distributed under the GNU LGPL (for details please see \doc\license.txt)
using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Controls;
using ICSharpCode.AvalonEdit.Document;
namespace ICSharpCode.AvalonEdit.Search
{
	/// <summary>
	/// Provides factory methods for ISearchStrategies.
	/// </summary>
	public class SearchStrategyFactory
	{
		/// <summary>
		/// Creates a default ISearchStrategy with the given parameters.
		/// </summary>
		public static ISearchStrategy Create(string searchPattern, bool ignoreCase, bool matchWholeWords, SearchMode mode)
		{
			if (searchPattern == null)
				throw new ArgumentNullException("searchPattern");
			RegexOptions options = RegexOptions.Compiled | RegexOptions.Multiline;
			if (ignoreCase)
				options |= RegexOptions.IgnoreCase;
			switch (mode) {
				case SearchMode.Normal:
					searchPattern = Regex.Escape(searchPattern);
					break;
				case SearchMode.Wildcard:
					searchPattern = ConvertWildcardsToRegex(searchPattern);
					break;
			}
			try {
				Regex pattern = new Regex(searchPattern, options);
				return new RegexSearchStrategy(pattern, matchWholeWords);
			} catch (ArgumentException ex) {
				throw new SearchPatternException(ex.Message, ex);
			}
		}
		static string ConvertWildcardsToRegex(string searchPattern)
		{
			if (string.IsNullOrEmpty(searchPattern))
				return "";
			StringBuilder builder = new StringBuilder();
			foreach (char ch in searchPattern) {
				switch (ch) {
					case '?':
						builder.Append(".");
						break;
					case '*':
						builder.Append(".*");
						break;
					default:
						builder.Append(Regex.Escape(ch.ToString()));
						break;
				}
			}
			return builder.ToString();
		}
	}
}

