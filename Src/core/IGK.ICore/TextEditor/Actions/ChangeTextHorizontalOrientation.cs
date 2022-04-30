using IGK.ICore.Drawing2D;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.ICore.TextEditor.Actions
{
    class ChangeTextHorizontalOrientation :  TextEditorActionBase
    {
        private enuStringAlignment m_alignment;
        public ChangeTextHorizontalOrientation(enuStringAlignment alignment)
        {
            this.m_alignment = alignment;
        }
        public override void DoAction()
        {
            this.Editor.Font.HorizontalAlignment = m_alignment;
        }
    }
}
