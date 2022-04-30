

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: TextEditorComponent.cs
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
file:TextEditorComponent.cs
*/
// Copyright (c) AlphaSierraPapa for the SharpDevelop Team (for details please see \doc\copyright.txt)
// This code is distributed under the GNU LGPL (for details please see \doc\license.txt)
using System;
using System.ComponentModel;
using ICSharpCode.AvalonEdit.Document;
using ICSharpCode.AvalonEdit.Editing;
using ICSharpCode.AvalonEdit.Rendering;
namespace ICSharpCode.AvalonEdit
{
	/// <summary>
	/// Represents a text editor control (<see cref="TextEditor"/>, <see cref="TextArea"/>
	/// or <see cref="TextView"/>).
	/// </summary>
	public interface ITextEditorComponent : IServiceProvider
	{
		/// <summary>
		/// Gets the document being edited.
		/// </summary>
		TextDocument Document { get; }
		/// <summary>
		/// Occurs when the Document property changes (when the text editor is connected to another
		/// document - not when the document content changes).
		/// </summary>
		event EventHandler DocumentChanged;
		/// <summary>
		/// Gets the options of the text editor.
		/// </summary>
		TextEditorOptions Options { get; }
		/// <summary>
		/// Occurs when the Options property changes, or when an option inside the current option list
		/// changes.
		/// </summary>
		event PropertyChangedEventHandler OptionChanged;
	}
}

