

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: IAXmlVisitor.cs
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
file:IAXmlVisitor.cs
*/
// Copyright (c) AlphaSierraPapa for the SharpDevelop Team (for details please see \doc\copyright.txt)
// This code is distributed under the GNU LGPL (for details please see \doc\license.txt)
using System;
using System.Text;
namespace ICSharpCode.AvalonEdit.Xml
{
	/// <summary>
	/// Visitor for the XML tree
	/// </summary>
	public interface IAXmlVisitor
	{
		/// <summary> Visit RawDocument </summary>
		void VisitDocument(AXmlDocument document);
		/// <summary> Visit RawElement </summary>
		void VisitElement(AXmlElement element);
		/// <summary> Visit RawTag </summary>
		void VisitTag(AXmlTag tag);
		/// <summary> Visit RawAttribute </summary>
		void VisitAttribute(AXmlAttribute attribute);
		/// <summary> Visit RawText </summary>
		void VisitText(AXmlText text);
	}
}

