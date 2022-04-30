using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IGK.ICore.TextEditor.Actions
{
    sealed class PasteAction : IGK.ICore.TextEditor.Actions.TextEditorActionBase
    {
        public override void DoAction()
        {
            var s = CoreServices.GetApplicationService<ICoreClipboard>();
            if (s == null)
                return;
            if (s!=null){
                string t = s.GetTextData();
                if (!string.IsNullOrEmpty(t))
                {
                    int i = this.Editor.Column + t.Length;
                    this.Editor.Insert(this.Editor.Column, t);
             
                }
            }
        }
    }
}
