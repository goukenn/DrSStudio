using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.ICore.TextEditor.Actions
{
    class CopySelecionAction : IGK.ICore.TextEditor.Actions.TextEditorActionBase
    {
        public override void DoAction()
        {
            var s = CoreServices.GetApplicationService<ICoreClipboard>();            
            if (s == null)
                return;
            if (this.Editor.SelectionStart > 0)
            {
                string v = this.Editor.GetText(this.Editor.SelectionStart, this.Editor.SelectionLength);
                if (string.IsNullOrEmpty(v) == false)
                {
                    s.CopyToClipboard( 
                        "Text",v);
                }                
            }
            
        }
    }
}
