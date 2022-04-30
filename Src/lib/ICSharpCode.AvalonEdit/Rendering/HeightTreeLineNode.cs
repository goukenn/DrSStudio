

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: HeightTreeLineNode.cs
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
file:HeightTreeLineNode.cs
*/
// Copyright (c) AlphaSierraPapa for the SharpDevelop Team (for details please see \doc\copyright.txt)
// This code is distributed under the GNU LGPL (for details please see \doc\license.txt)
using System;
using System.Collections.Generic;
using System.Diagnostics;
namespace ICSharpCode.AvalonEdit.Rendering
{
	struct HeightTreeLineNode
	{
		internal HeightTreeLineNode(double height)
		{
			this.collapsedSections = null;
			this.height = height;
		}
		internal double height;
		internal List<CollapsedLineSection> collapsedSections;
		internal bool IsDirectlyCollapsed {
			get { return collapsedSections != null; }
		}
		internal void AddDirectlyCollapsed(CollapsedLineSection section)
		{
			if (collapsedSections == null)
				collapsedSections = new List<CollapsedLineSection>();
			collapsedSections.Add(section);
		}
		internal void RemoveDirectlyCollapsed(CollapsedLineSection section)
		{
			Debug.Assert(collapsedSections.Contains(section));
			collapsedSections.Remove(section);
			if (collapsedSections.Count == 0)
				collapsedSections = null;
		}
		/// <summary>
		/// Returns 0 if the line is directly collapsed, otherwise, returns <see cref="height"/>.
		/// </summary>
		internal double TotalHeight {
			get {
				return IsDirectlyCollapsed ? 0 : height;
			}
		}
	}
}

