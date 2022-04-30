using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IGK.ICore.TextEditor.Actions
{
    public class MoveLeft : TextEditorActionBase 
    {
        public override void DoAction()
        {
            if (this.Editor.Column > 0)
            {
                this.Editor.Column--;
                this.Editor.Update(false);
            }
            else
            {
                if (this.Editor.Line > 0)
                {
                    this.Editor.Line--;
                    var v = this.Editor.VirtualLines[this.Editor.Line];
                    this.Editor.Column = v.TextLength;
                    this.Editor.Update(false);
                }
            }
        }
    }
}
