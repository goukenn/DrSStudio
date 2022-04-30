using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IGK.ICore.TextEditor.Actions
{
    sealed class CutSelectionAction : IGK.ICore.TextEditor.Actions.TextEditorActionBase
    {
        public override void DoAction()
        {
            var s = CoreServices.GetApplicationService<ICoreClipboard>();
            if (s == null)
                return;
            string v = this.Editor.GetText(this.Editor.SelectionStart, this.Editor.SelectionLength);
            if (string.IsNullOrEmpty(v) == false)
            {
                s.CopyToClipboard(
                    "Text", v);
                string k = this.Editor.Text;
                k = k.Remove(this.Editor.SelectionStart, this.Editor.SelectionLength);
                this.Editor.ReplaceText(k);
            }
        }
    }
}
