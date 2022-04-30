using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IGK.ICore.TextEditor.Actions
{
    class MoveBack : TextEditorActionBase
    {
        public override void DoAction()
        {

            if (this.Editor.SelectionLength > 0)
            {
                if (this.Editor.SelectionLength == this.Editor.CharCount)
                {
                    this.Editor.ClearBuffer();
                }
                else
                {
                    this.Editor.ReplaceText(this.Editor.Text.Remove(this.Editor.SelectionStart, this.Editor.SelectionLength));
                }
                return;
            }

            if (this.Editor.Column > 0)
            {
                this.Editor.RemoveAt(this.Editor.Column - 1, 1);
            }
            else if (this.Editor.Line > 0)
            {
                var v = this.Editor.VirtualLines[this.Editor.Line];
                if (v.TextLength == 0)
                {
                    this.Editor.VirtualLines.Remove(v);
                }
                this.Editor.Line--;
                v = this.Editor.VirtualLines[this.Editor.Line];
                this.Editor.Column = v.TextLength;
                if (v.Text.EndsWith(Environment.NewLine))
                {
                    this.Editor.RemoveAt(v.TextLength, Environment.NewLine.Length);
                }
                this.Editor.Update();
            }
        }
    }
}
