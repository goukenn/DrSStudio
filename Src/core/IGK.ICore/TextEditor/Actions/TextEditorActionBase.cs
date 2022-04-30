using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IGK.ICore.TextEditor.Actions
{
    public abstract class TextEditorActionBase
    {
        internal TextHostEditor m_editor;

        public TextHostEditor Editor {
            get {
                return m_editor;
            }
        }
        public abstract void DoAction() ;
    }
}
