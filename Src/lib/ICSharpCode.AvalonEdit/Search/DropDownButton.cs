

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: DropDownButton.cs
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
file:DropDownButton.cs
*/
// Copyright (c) AlphaSierraPapa for the SharpDevelop Team (for details please see \doc\copyright.txt)
// This code is distributed under the GNU LGPL (for details please see \doc\license.txt)
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
namespace ICSharpCode.AvalonEdit.Search
{
	/// <summary>
	/// A button that opens a drop-down menu when clicked.
	/// </summary>
	class DropDownButton : ButtonBase
	{
		public static readonly DependencyProperty DropDownContentProperty
			= DependencyProperty.Register("DropDownContent", typeof(Popup),
			                              typeof(DropDownButton), new FrameworkPropertyMetadata(null));
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes")]
		protected static readonly DependencyPropertyKey IsDropDownContentOpenPropertyKey
			= DependencyProperty.RegisterReadOnly("IsDropDownContentOpen", typeof(bool),
			                                      typeof(DropDownButton), new FrameworkPropertyMetadata(false));
		public static readonly DependencyProperty IsDropDownContentOpenProperty = IsDropDownContentOpenPropertyKey.DependencyProperty;
		static DropDownButton()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof(DropDownButton), new FrameworkPropertyMetadata(typeof(DropDownButton)));
		}
		public Popup DropDownContent {
			get { return (Popup)GetValue(DropDownContentProperty); }
			set { SetValue(DropDownContentProperty, value); }
		}
		public bool IsDropDownContentOpen {
			get { return (bool)GetValue(IsDropDownContentOpenProperty); }
			protected set { SetValue(IsDropDownContentOpenPropertyKey, value); }
		}
		protected override void OnClick()
		{
			if (DropDownContent != null && !IsDropDownContentOpen) {
				DropDownContent.Placement = PlacementMode.Bottom;
				DropDownContent.PlacementTarget = this;
				DropDownContent.IsOpen = true;
				DropDownContent.Closed += DropDownContent_Closed;
				this.IsDropDownContentOpen = true;
			}
		}
		void DropDownContent_Closed(object sender, EventArgs e)
		{
			((Popup)sender).Closed -= DropDownContent_Closed;
			this.IsDropDownContentOpen = false;
		}
	}
}

