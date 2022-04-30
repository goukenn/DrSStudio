

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: OverloadInsightWindow.cs
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
file:OverloadInsightWindow.cs
*/
// Copyright (c) AlphaSierraPapa for the SharpDevelop Team (for details please see \doc\copyright.txt)
// This code is distributed under the GNU LGPL (for details please see \doc\license.txt)
using System;
using System.Windows;
using System.Windows.Input;
using ICSharpCode.AvalonEdit.Editing;
namespace ICSharpCode.AvalonEdit.CodeCompletion
{
	/// <summary>
	/// Insight window that shows an OverloadViewer.
	/// </summary>
	public class OverloadInsightWindow : InsightWindow
	{
		OverloadViewer overloadViewer = new OverloadViewer();
		/// <summary>
		/// Creates a new OverloadInsightWindow.
		/// </summary>
		public OverloadInsightWindow(TextArea textArea) : base(textArea)
		{
			overloadViewer.Margin = new Thickness(2,0,0,0);
			this.Content = overloadViewer;
		}
		/// <summary>
		/// Gets/Sets the item provider.
		/// </summary>
		public IOverloadProvider Provider {
			get { return overloadViewer.Provider; }
			set { overloadViewer.Provider = value; }
		}
		/// <inheritdoc/>
		protected override void OnKeyDown(KeyEventArgs e)
		{
			base.OnKeyDown(e);
			if (!e.Handled && this.Provider.Count > 1) {
				switch (e.Key) {
					case Key.Up:
						e.Handled = true;
						overloadViewer.ChangeIndex(-1);
						break;
					case Key.Down:
						e.Handled = true;
						overloadViewer.ChangeIndex(+1);
						break;
				}
				if (e.Handled) {
					UpdateLayout();
					UpdatePosition();
				}
			}
		}
	}
}

