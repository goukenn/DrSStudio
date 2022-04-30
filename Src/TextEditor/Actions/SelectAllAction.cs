using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.ICore.TextEditor.Actions
{
    class SelectAllAction : IGK.ICore.TextEditor.Actions.TextEditorActionBase
    {
        public override void DoAction()
        {
            this.Editor.SelectionStart = 0;
            this.Editor.SelectionLength = this.Editor.CharCount;
            this.Editor.Refresh();
            
        }
    }
}
