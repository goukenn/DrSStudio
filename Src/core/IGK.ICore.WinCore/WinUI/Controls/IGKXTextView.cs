using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.ICore.WinCore.WinUI.Controls
{
    using IGK.ICore.Drawing2D;
    using IGK.ICore.TextEditor;
    using IGK.ICore.WinUI;
    using System.ComponentModel;

    /// <summary>
    /// represent a simple text view
    /// </summary>
    public class IGKXTextView : IGKXControl , ITextEditorHost
    {
        private TextHostEditor m_editor;
        private Vector2f m_OffSet;
        private ITextEditorListener m_editorListener;
        private CoreFont m_CoreFont;
        private Colorf m_textColor;
        private bool m_configuring;
        public string TextFontDefinition
        {
            get
            {
             return this.m_CoreFont.GetDefinition();                
            }
        }
        public CoreFont TextFont
        {
            get { return m_CoreFont; }
        }

        public Vector2f OffSet
        {
            get { return m_OffSet; }
            set
            {
                if (m_OffSet != value)
                {
                    m_OffSet = value;
                }
            }
        }
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [EditorBrowsable(EditorBrowsableState.Never )]
        public override System.Drawing.Font Font
        {
            get
            {
                return base.Font;
            }
            set
            {
                base.Font = value;
            }
        }

        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);
            if (!Focused)
            {
                this.Focus();
            }
        }
        public void OnFontDefinitionChanged(string newFontDefinition)
        {
            if (this.m_configuring)
                return;
            this.m_configuring = true;
            this.m_CoreFont.CopyDefinition(newFontDefinition);
            this.m_configuring = false;
        }

        /// <summary>
        /// .ctr
        /// </summary>
        public IGKXTextView()
        {
            this.SetStyle(System.Windows.Forms.ControlStyles.Selectable, true);
            this.SetStyle(System.Windows.Forms.ControlStyles.AllPaintingInWmPaint , true);
            this.SetStyle(System.Windows.Forms.ControlStyles.OptimizedDoubleBuffer, true);
            this.SetStyle(System.Windows.Forms.ControlStyles.ResizeRedraw , true);
            this.SetStyle(System.Windows.Forms.ControlStyles.UserPaint, true);
            this.SetStyle(System.Windows.Forms.ControlStyles.SupportsTransparentBackColor, true);
            this.BackColor = global::System.Drawing.Color.Transparent;

            //System.Windows.Forms.MessageBox.Show("DesignMode "+this.DesignMode);
            if (this.DesignMode == false)
            {
                try
                {
                    this.CreateHandle();
                    this.m_CoreFont = "FontName:consolas; size:8pt";
                    this.m_CoreFont.FontDefinitionChanged += _FontDefinitionChanged;
                    this.m_textColor = Colorf.Black;
                    this.m_OffSet = Vector2f.Zero;
                    this.m_editor = new TextHostEditor(); 
                    this.m_editor.SelectionFillColor = Colorf.DarkGray;
     
                    this.Paint += _Paint;
                }
                catch { 
                }
            }
        }
        protected override void OnPaintBackground(System.Windows.Forms.PaintEventArgs pevent)
        {
            base.OnPaintBackground(pevent);
        }
     
        protected override void OnHandleDestroyed(EventArgs e)
        {
            this.m_editor.Bind(null);
            base.OnHandleDestroyed(e);
        }
        void _FontDefinitionChanged(object sender, EventArgs e)
        {
            if (this.m_configuring)
                return;
            this.m_editor.Font.CopyDefinition(this.m_CoreFont.GetDefinition());
        }

        void _Paint(object sender, CorePaintEventArgs e)
        {
            e.Graphics.Clear(Colorf.WhiteSmoke);
            //e.Graphics.DrawString(this.Handle.ToString(),
            //    this.TextFont,
            //    this.TextColor,
            //    this.TextBound);
            e.Graphics.DrawRectangle(Colorf.Black, 0, 0, this.Width-1, this.Height-1);
            if (string.IsNullOrEmpty(this.Text))
                return;


            e.Graphics.DrawString(this.Text ,
               (CoreFont) this.m_editor.Font,
                this.TextColor,
                this.TextBound);
            this.m_editor.Render(e.Graphics, this.TextBound);
        }


        public Rectanglef TextBound
        {
            get {
                return new Rectanglef(0, 0, this.Width, this.Height);
                 }
        }

        public void RegisterListener(ITextEditorListener listener)
        {
            this.m_editorListener = listener;
        }

        public Vector2f GetScreenLocation(Vector2f factorLocation)
        {
            return new Vector2f(
                factorLocation.X * ZoomX ,
                factorLocation.Y * ZoomY 
                );
        }

        public float ZoomX
        {
            get { return 1.0f; }
        }

        public float ZoomY
        {
            get { return 1.0f; }
        }

        protected override bool IsInputKey(System.Windows.Forms.Keys keyData)
        {
            switch (keyData)
            {
                case System.Windows.Forms.Keys.Left:
                case System.Windows.Forms.Keys.Right:
                case System.Windows.Forms.Keys.Up:
                case System.Windows.Forms.Keys.Down:
                    return true;
                default:
                    break;
            }
            return base.IsInputKey(keyData);
        }
        protected override bool ShowFocusCues
        {
            get
            {
                return false;
            }
        }
        
        protected override void OnGotFocus(EventArgs e)
        {
            if (this.m_editorListener != null)
            {
                this.m_editorListener.OnGotFocus();
            }
            else {              
                this.m_editor.Bind(this);             
            }
            base.OnGotFocus(e);
        }
        protected override void OnLostFocus(EventArgs e)
        {
            if (this.m_editorListener != null) {
                this.m_editorListener.OnLostFocus();
            }
            base.OnLostFocus(e);
        }
        protected override void OnCoreKeyDown(ICore.WinUI.CoreKeyEventArgs e)
        {
            if (this.m_editorListener != null)
                this.m_editorListener.OnKeyPress((int)(e.KeyCode | e.Modifiers ), false);
            base.OnCoreKeyDown(e);
        }
        protected override void OnCoreKeyPress(ICore.WinUI.CoreKeyEventArgs e)
        {
            if (this.m_editorListener != null)
            {
                char ch = (char)e.KeyCode;
                if (char.IsControl(ch) == false)
                {
                        this.m_editorListener.OnKeyPress((int)(e.KeyCode), true);
                }
            }
            base.OnCoreKeyPress(e);
        }



        public ICoreCaret CreateCaret(int width, int height)
        {
            return IGK.ICore.WinCore.Caret.Caret.CreateCaret(this.Handle, width, height);
        }

        public Colorf TextColor { get { return this.m_textColor; } set { this.m_textColor = value; } }
    }
}
