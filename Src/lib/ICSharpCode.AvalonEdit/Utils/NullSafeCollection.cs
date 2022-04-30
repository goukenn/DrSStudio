

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: NullSafeCollection.cs
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
file:NullSafeCollection.cs
*/
// Copyright (c) AlphaSierraPapa for the SharpDevelop Team (for details please see \doc\copyright.txt)
// This code is distributed under the GNU LGPL (for details please see \doc\license.txt)
using System;
using System.Collections.ObjectModel;
namespace ICSharpCode.AvalonEdit.Utils
{
	/// <summary>
	/// A collection that cannot contain null values.
	/// </summary>
	[Serializable]
	public class NullSafeCollection<T> : Collection<T> where T : class
	{
		/// <inheritdoc/>
		protected override void InsertItem(int index, T item)
		{
			if (item == null)
				throw new ArgumentNullException("item");
			base.InsertItem(index, item);
		}
		/// <inheritdoc/>
		protected override void SetItem(int index, T item)
		{
			if (item == null)
				throw new ArgumentNullException("item");
			base.SetItem(index, item);
		}
	}
}

