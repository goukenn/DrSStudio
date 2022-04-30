using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.ICore.TextEditor
{
    public class TextEditorVirtualLine : TextEditorItemBase
    {
        private float m_height;
        private int m_textLength;

        /// <summary>
        /// store the next top position of this virtual line
        /// </summary>
        public float Top { get {
            int i = this.Index;
            if ((this.Editor == null) || (i == -1) || (i == 0))
                return 0;
            var v = this.Editor.VirtualLines[i - 1];
            return v.Top + v.m_height;
        } }
        /// <summary>
        /// get the index of this virtual line
        /// </summary>
        public int Index { get { return this.Editor.VirtualLines.IndexOf(this);  } }
        /// <summary>
        /// get the start index of this virtual line
        /// </summary>
        //public int StartIndex { get; set; }
        public int StartIndex { get { 
            int i = this.Index ;
            if ((this.Editor == null)|| (i==-1) || (i==0))
                return 0;
            var v = this.Editor.VirtualLines[i - 1];
            return v.StartIndex + v.TextLength + this.Editor.NewLineLength;
            
        } }
        public string Text { get {
            return this.Editor.GetText(this.StartIndex, this.TextLength);
        } }

        public int TextLength { 
            get{
                return m_textLength;
            }
            internal set {
                if ((this.m_textLength !=value) && (value >= 0))
                {
                    this.m_textLength = value;
                    _measure();
                }
            }
        }

        protected override void OnEditorChanged(EventArgs eventArgs)
        {
            base.OnEditorChanged(eventArgs);
            _measure();
        }

        private void _measure()
        {
            if (Editor != null)
            {
                var t = this.Text;
                t = string.IsNullOrEmpty(t) ? "_" : t;
                this.m_height =
                    this.Editor.Measure(t, 0, t.Length).Height;
            }
        }


        public bool IsLastLine { get {
            if (Editor != null)
                return this.Editor.VirtualLines.IndexOf(this) == (this.Editor.LineCount - 1);
            return false ;
        } }

        internal protected void Update()
        {
            this._measure();
        }
    }
}
