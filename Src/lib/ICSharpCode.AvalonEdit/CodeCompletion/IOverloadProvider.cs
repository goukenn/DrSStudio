

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: IOverloadProvider.cs
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
file:IOverloadProvider.cs
*/
// Copyright (c) AlphaSierraPapa for the SharpDevelop Team (for details please see \doc\copyright.txt)
// This code is distributed under the GNU LGPL (for details please see \doc\license.txt)
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
namespace ICSharpCode.AvalonEdit.CodeCompletion
{
	/// <summary>
	/// Provides the items for the OverloadViewer.
	/// </summary>
	public interface IOverloadProvider : INotifyPropertyChanged
	{
		/// <summary>
		/// Gets/Sets the selected index.
		/// </summary>
		int SelectedIndex { get; set; }
		/// <summary>
		/// Gets the number of overloads.
		/// </summary>
		int Count { get; }
		/// <summary>
		/// Gets the text 'SelectedIndex of Count'.
		/// </summary>
		string CurrentIndexText { get; }
		/// <summary>
		/// Gets the current header.
		/// </summary>
		object CurrentHeader { get; }
		/// <summary>
		/// Gets the current content.
		/// </summary>
		object CurrentContent { get; }
	}
}
