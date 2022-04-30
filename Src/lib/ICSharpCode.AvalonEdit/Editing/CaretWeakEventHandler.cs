

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: CaretWeakEventHandler.cs
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
file:CaretWeakEventHandler.cs
*/
// Copyright (c) AlphaSierraPapa for the SharpDevelop Team (for details please see \doc\copyright.txt)
// This code is distributed under the GNU LGPL (for details please see \doc\license.txt)
using ICSharpCode.AvalonEdit.Utils;
using System;
namespace ICSharpCode.AvalonEdit.Editing
{
	/// <summary>
	/// Contains classes for handling weak events on the Caret class.
	/// </summary>
	public static class CaretWeakEventManager
	{
		/// <summary>
		/// Handles the Caret.PositionChanged event.
		/// </summary>
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1034:NestedTypesShouldNotBeVisible")]
		public sealed class PositionChanged : WeakEventManagerBase<PositionChanged, Caret>
		{
			/// <inheritdoc/>
			protected override void StartListening(Caret source)
			{
				source.PositionChanged += DeliverEvent;
			}
			/// <inheritdoc/>
			protected override void StopListening(Caret source)
			{
				source.PositionChanged -= DeliverEvent;
			}
		}
	}
}

