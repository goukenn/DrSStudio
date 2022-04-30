

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: Layer.cs
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
file:Layer.cs
*/
// Copyright (c) AlphaSierraPapa for the SharpDevelop Team (for details please see \doc\copyright.txt)
// This code is distributed under the GNU LGPL (for details please see \doc\license.txt)
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Media;
namespace ICSharpCode.AvalonEdit.Rendering
{
	/// <summary>
	/// Base class for known layers.
	/// </summary>
	class Layer : UIElement
	{
		protected readonly TextView textView;
		protected readonly KnownLayer knownLayer;
		public Layer(TextView textView, KnownLayer knownLayer)
		{
			Debug.Assert(textView != null);
			this.textView = textView;
			this.knownLayer = knownLayer;
			this.Focusable = false;
		}
		protected override GeometryHitTestResult HitTestCore(GeometryHitTestParameters hitTestParameters)
		{
			return null;
		}
		protected override HitTestResult HitTestCore(PointHitTestParameters hitTestParameters)
		{
			return null;
		}
		protected override void OnRender(DrawingContext drawingContext)
		{
			base.OnRender(drawingContext);
			textView.RenderBackground(drawingContext, knownLayer);
		}
	}
}

