

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: TextType.cs
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
file:TextType.cs
*/
// Copyright (c) AlphaSierraPapa for the SharpDevelop Team (for details please see \doc\copyright.txt)
// This code is distributed under the GNU LGPL (for details please see \doc\license.txt)
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Linq;
using ICSharpCode.AvalonEdit.Document;
namespace ICSharpCode.AvalonEdit.Xml
{
	/// <summary> Identifies the context in which the text occured </summary>
	enum TextType
	{
		/// <summary> Ends with non-whitespace </summary>
		WhiteSpace,
		/// <summary> Ends with "&lt;";  "]]&gt;" is error </summary>
		CharacterData,
		/// <summary> Ends with "-->";  "--" is error </summary>
		Comment,
		/// <summary> Ends with "]]&gt;" </summary>
		CData,
		/// <summary> Ends with "?>" </summary>
		ProcessingInstruction,
		/// <summary> Ends with "&lt;" or ">" </summary>
		UnknownBang,
		/// <summary> Unknown </summary>
		Other
	}
}

