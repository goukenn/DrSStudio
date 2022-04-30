using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IGK.ICore.TextEditor.Actions
{
    public class MoveRight : TextEditorActionBase
    {
        public override void DoAction()
        {

            var v = this.Editor.VirtualLines[this.Editor.Line];

            if (this.Editor.Column < v.TextLength)
            {
                this.Editor.Column++;
                this.Editor.Update(false);
            }
            else
            {
                if (this.Editor.Line < this.Editor.LineCount-1)
                {
                    this.Editor.Line++;
                    this.Editor.Column = 0;
                    this.Editor.Update(false);
                }
            }
        }
    }
}
