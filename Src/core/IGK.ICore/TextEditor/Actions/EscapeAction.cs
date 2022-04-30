using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IGK.ICore.TextEditor.Actions
{
    class EscapeAction : TextEditorActionBase
    {
        public override void DoAction()
       {
           if (this.Editor.SelectionLength > 0)
           {
               this.Editor.SelectionLength = 0;

           }

           // if (this.Editor.Text.Length == 0) {
                this.Editor.CancelEdition();
            //}
         
        }
    }
}
