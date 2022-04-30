using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.ICore.TextEditor.Actions
{
    class ChangeWrapModeAction : TextEditorActionBase
    {
        public override void DoAction()
        {
            this.Editor.Font.WordWrap = !this.Editor.Font.WordWrap;
            this.Editor.Update(false);
        }
    }
}
