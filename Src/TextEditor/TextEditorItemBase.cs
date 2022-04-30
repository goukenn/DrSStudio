using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.ICore.TextEditor
{
    public class TextEditorItemBase
    {
        private TextHostEditor m_Editor;

        public  TextHostEditor Editor
        {
            get
            {
               return  this.m_Editor;
            }
            internal set {
                if (this.m_Editor != value)
                {
                    this.m_Editor = value;
                    OnEditorChanged(EventArgs.Empty);
                }
            }
        }

        public event EventHandler EditorChanged;

        protected virtual void OnEditorChanged(EventArgs eventArgs)
        {
            if (this.EditorChanged!=null) {
                this.EditorChanged(this, eventArgs); 
            }
        }
    }
}
