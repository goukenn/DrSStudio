using IGK.ICore.Drawing2D;
using IGK.ICore.WinUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IGK.ICore.TextEditor
{
    public interface ITextEditorHost: ICore2DDrawingScale
    {
        /// <summary>
        /// get or set the text to edit
        /// </summary>
        string Text { get; set; }
        /// <summary>
        /// get the text bounds zone
        /// </summary>
        Rectanglef TextBound { get; }
        /// <summary>
        /// get a font definition to copy
        /// </summary>
        string TextFontDefinition { get; }
        /// <summary>
        /// get the handle for caret creation
        /// </summary>
        IntPtr Handle { get;  }
        /// <summary>
        /// invalidate the editor surface
        /// </summary>
        void Invalidate();
        void RegisterListener(ITextEditorListener listener);  
        Vector2f GetScreenLocation(Vector2f vector2f);
        ICoreCaret CreateCaret(int width, int height);
        void OnFontDefinitionChanged(string newFontDefinition);
    }
}
