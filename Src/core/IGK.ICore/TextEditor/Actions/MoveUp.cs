using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IGK.ICore.TextEditor.Actions
{
    class MoveUp : TextEditorActionBase
    {
        public override void DoAction()
        {
                if (this.Editor.Line > 0)
                {
                    this.Editor.Line--;
                    var v = this.Editor.VirtualLines[this.Editor.Line];
                    this.Editor.Column = Math.Min(v.TextLength, this.Editor.Column);
                    //this.Editor.SelectionStart = this.Editor.Column;
                    this.Editor.Update(false);
                }
            
        }
    }
}
