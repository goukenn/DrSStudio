using IGK.ICore.GraphicModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IGK.ICore.TextEditor
{
    public interface  ITextEditorListener
    {
        bool Visible { get; set; }
        void OnKeyPress(int KeyCode, bool ischar);
        void OnGotFocus();
        void OnLostFocus();
        void Render(ICoreGraphics graphics, Rectanglef rectangle);

        void Refresh();
        void Reset();
    }
}
