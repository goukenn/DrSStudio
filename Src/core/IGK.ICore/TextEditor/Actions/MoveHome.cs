using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IGK.ICore.TextEditor.Actions
{
    class MoveHome : TextEditorActionBase
    {
        public override void DoAction()
        {
            var v = this.Editor.VirtualLines[this.Editor.Line];
            this.Editor.Column = 0;
            this.Editor.Update(false);
        }
    }
}
