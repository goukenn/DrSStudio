

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: MergedCollection.cs
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
file:MergedCollection.cs
*/
// Copyright (c) AlphaSierraPapa for the SharpDevelop Team (for details please see \doc\copyright.txt)
// This code is distributed under the GNU LGPL (for details please see \doc\license.txt)
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
namespace ICSharpCode.AvalonEdit.Xml
{
	/// <summary>
	/// Two collections in sequence
	/// </summary>
	public class MergedCollection<T, TCollection> : ObservableCollection<T> where TCollection : INotifyCollectionChanged, IList<T>
	{
		TCollection a;
		TCollection b;
		/// <summary> Create a wrapper containing elements of 'a' and then 'b' </summary>
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
		public MergedCollection(TCollection a, TCollection b)
		{
			this.a = a;
			this.b = b;
			this.a.CollectionChanged += SourceCollectionAChanged;
			this.b.CollectionChanged += SourceCollectionBChanged;
			Reset();
		}
		void Reset()
		{
			this.Clear();
			foreach(T item in a) this.Add(item);
			foreach(T item in b) this.Add(item);
		}
		void SourceCollectionAChanged(object sender, NotifyCollectionChangedEventArgs e)
		{
			SourceCollectionChanged(0, e);
		}
		void SourceCollectionBChanged(object sender, NotifyCollectionChangedEventArgs e)
		{
			SourceCollectionChanged(a.Count, e);
		}
		void SourceCollectionChanged(int collectionStart, NotifyCollectionChangedEventArgs e)
		{
			switch(e.Action) {
				case NotifyCollectionChangedAction.Add:
					for (int i = 0; i < e.NewItems.Count; i++) {
						this.InsertItem(collectionStart + e.NewStartingIndex + i, (T)e.NewItems[i]);
					}
					break;
				case NotifyCollectionChangedAction.Remove:
					for (int i = 0; i < e.OldItems.Count; i++) {
						this.RemoveAt(collectionStart + e.OldStartingIndex);
					}
					break;
				case NotifyCollectionChangedAction.Reset:
					Reset();
					break;
				default:
					throw new NotSupportedException(e.Action.ToString());
			}
		}
	}
}

