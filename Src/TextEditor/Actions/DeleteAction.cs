using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IGK.ICore.TextEditor.Actions
{
    /// <summary>
    /// delete text action
    /// </summary>
    sealed class DeleteAction : TextEditorActionBase
    {
        public override void DoAction()
        {
            if (this.Editor.SelectionLength > 0)
            {
                if (this.Editor.SelectionLength == this.Editor.CharCount)
                {
                    this.Editor.ClearBuffer();
                }
                else {
                    this.Editor.ReplaceText(this.Editor.Text.Remove(this.Editor.SelectionStart, this.Editor.SelectionLength));
                }
            }
            else
            {

                var v = this.Editor.VirtualLines[this.Editor.Line];
                var c = this.Editor.Column;
                if (c < (v.TextLength))
                {
                    this.Editor.RemoveAt(c, 1);
                }
                else
                {
                    if (this.Editor.Line < (this.Editor.LineCount - 1))
                    {//append next line to current line
                        if (c == 0)
                        {//at the begin
                            this.Editor.VirtualLines.Remove(v);
                            this.Editor.Update();
                        }
                        else
                        {
                            var nextLine = this.Editor.VirtualLines[v.Index + 1];
                            if (nextLine.TextLength == 0)
                            {
                                this.Editor.VirtualLines.Remove(nextLine);
                                this.Editor.Update();
                            }
                            else
                            {
                                string vt = nextLine.Text;
                                this.Editor.Insert(c, vt);
                                this.Editor.VirtualLines.Remove(nextLine);
                                this.Editor.Update();
                            }
                        }
                    }
                    else { 
                        //remove this elent
                        
                    }
                }
            }
        }
    }
}
