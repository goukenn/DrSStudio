

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: VisualLineElementGenerator.cs
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
file:VisualLineElementGenerator.cs
*/
// Copyright (c) AlphaSierraPapa for the SharpDevelop Team (for details please see \doc\copyright.txt)
// This code is distributed under the GNU LGPL (for details please see \doc\license.txt)
using System;
namespace ICSharpCode.AvalonEdit.Rendering
{
	/// <summary>
	/// Abstract base class for generators that produce new visual line elements.
	/// </summary>
	public abstract class VisualLineElementGenerator
	{
		/// <summary>
		/// Gets the text run construction context.
		/// </summary>
		protected ITextRunConstructionContext CurrentContext { get; private set; }
		/// <summary>
		/// Initializes the generator for the <see cref="ITextRunConstructionContext"/>
		/// </summary>
		public virtual void StartGeneration(ITextRunConstructionContext context)
		{
			if (context == null)
				throw new ArgumentNullException("context");
			this.CurrentContext = context;
		}
		/// <summary>
		/// De-initializes the generator.
		/// </summary>
		public virtual void FinishGeneration()
		{
			this.CurrentContext = null;
		}
		/// <summary>
		/// Should only be used by VisualLine.ConstructVisualElements.
		/// </summary>
		internal int cachedInterest;
		/// <summary>
		/// Gets the first offset >= startOffset where the generator wants to construct an element.
		/// Return -1 to signal no interest.
		/// </summary>
		public abstract int GetFirstInterestedOffset(int startOffset);
		/// <summary>
		/// Constructs an element at the specified offset.
		/// May return null if no element should be constructed.
		/// </summary>
		/// <remarks>
		/// Avoid signalling interest and then building no element by returning null - doing so
		/// causes the generated <see cref="VisualLineText"/> elements to be unnecessarily split
		/// at the position where you signalled interest.
		/// </remarks>
		public abstract VisualLineElement ConstructElement(int offset);
	}
	internal interface IBuiltinElementGenerator
	{
		void FetchOptions(TextEditorOptions options);
	}
}

