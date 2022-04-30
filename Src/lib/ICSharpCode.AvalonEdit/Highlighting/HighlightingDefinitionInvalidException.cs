

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: HighlightingDefinitionInvalidException.cs
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
file:HighlightingDefinitionInvalidException.cs
*/
// Copyright (c) AlphaSierraPapa for the SharpDevelop Team (for details please see \doc\copyright.txt)
// This code is distributed under the GNU LGPL (for details please see \doc\license.txt)
using System;
using System.Runtime.Serialization;
namespace ICSharpCode.AvalonEdit.Highlighting
{
	/// <summary>
	/// Indicates that the highlighting definition that was tried to load was invalid.
	/// </summary>
	[Serializable()]
	public class HighlightingDefinitionInvalidException : Exception
	{
		/// <summary>
		/// Creates a new HighlightingDefinitionInvalidException instance.
		/// </summary>
		public HighlightingDefinitionInvalidException() : base()
		{
		}
		/// <summary>
		/// Creates a new HighlightingDefinitionInvalidException instance.
		/// </summary>
		public HighlightingDefinitionInvalidException(string message) : base(message)
		{
		}
		/// <summary>
		/// Creates a new HighlightingDefinitionInvalidException instance.
		/// </summary>
		public HighlightingDefinitionInvalidException(string message, Exception innerException) : base(message, innerException)
		{
		}
		/// <summary>
		/// Creates a new HighlightingDefinitionInvalidException instance.
		/// </summary>
		protected HighlightingDefinitionInvalidException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
