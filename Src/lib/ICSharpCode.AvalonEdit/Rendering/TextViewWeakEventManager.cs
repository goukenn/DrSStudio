

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: TextViewWeakEventManager.cs
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
file:TextViewWeakEventManager.cs
*/
// Copyright (c) AlphaSierraPapa for the SharpDevelop Team (for details please see \doc\copyright.txt)
// This code is distributed under the GNU LGPL (for details please see \doc\license.txt)
using System;
using ICSharpCode.AvalonEdit.Utils;
namespace ICSharpCode.AvalonEdit.Rendering
{
	/// <summary>
	/// Contains weak event managers for the TextView events.
	/// </summary>
	public static class TextViewWeakEventManager
	{
		/// <summary>
		/// Weak event manager for the <see cref="TextView.DocumentChanged"/> event.
		/// </summary>
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1034:NestedTypesShouldNotBeVisible")]
		public sealed class DocumentChanged : WeakEventManagerBase<DocumentChanged, TextView>
		{
			/// <inheritdoc/>
			protected override void StartListening(TextView source)
			{
				source.DocumentChanged += DeliverEvent;
			}
			/// <inheritdoc/>
			protected override void StopListening(TextView source)
			{
				source.DocumentChanged -= DeliverEvent;
			}
		}
		/// <summary>
		/// Weak event manager for the <see cref="TextView.VisualLinesChanged"/> event.
		/// </summary>
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1034:NestedTypesShouldNotBeVisible")]
		public sealed class VisualLinesChanged : WeakEventManagerBase<VisualLinesChanged, TextView>
		{
			/// <inheritdoc/>
			protected override void StartListening(TextView source)
			{
				source.VisualLinesChanged += DeliverEvent;
			}
			/// <inheritdoc/>
			protected override void StopListening(TextView source)
			{
				source.VisualLinesChanged -= DeliverEvent;
			}
		}
		/// <summary>
		/// Weak event manager for the <see cref="TextView.ScrollOffsetChanged"/> event.
		/// </summary>
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1034:NestedTypesShouldNotBeVisible")]
		public sealed class ScrollOffsetChanged : WeakEventManagerBase<ScrollOffsetChanged, TextView>
		{
			/// <inheritdoc/>
			protected override void StartListening(TextView source)
			{
				source.ScrollOffsetChanged += DeliverEvent;
			}
			/// <inheritdoc/>
			protected override void StopListening(TextView source)
			{
				source.ScrollOffsetChanged -= DeliverEvent;
			}
		}
	}
}

