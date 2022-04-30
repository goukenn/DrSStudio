using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IGK.ICore.TextEditor.Actions
{
    class Enter : TextEditorActionBase
    {
        public override void DoAction()
       {
            if (this.Editor.CurrentLine.IsLastLine &&
                (this.Editor.SelectionStart == this.Editor.CharCount ))
            {
                this.Editor.AddLine();
            }
            else {
                CoreLog.WriteDebug("Inserting new Line At line");
                this.Editor.InsertLine();
            }
        }
    }
}
