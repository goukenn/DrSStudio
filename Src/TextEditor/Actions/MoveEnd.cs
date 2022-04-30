using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IGK.ICore.TextEditor.Actions
{
    class MoveEnd : TextEditorActionBase
    {
        public override void DoAction()
        {
            var v = this.Editor.VirtualLines[this.Editor.Line];
            this.Editor.Column = v.TextLength;
            this.Editor.SelectionStart = v.StartIndex + this.Editor.Column;
            this.Editor.Update();
        }
    }
}
