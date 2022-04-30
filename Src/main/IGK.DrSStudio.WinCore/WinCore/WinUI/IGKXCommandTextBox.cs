using IGK.ICore.WinCore;
using IGK.ICore;using IGK.DrSStudio.WinUI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using IGK.ICore.WinCore.WinUI.Controls;

namespace IGK.DrSStudio.WinUI
{
    /// <summary>
    /// execute command line per line 
    /// </summary>
    [DefaultEvent("CommandChanged")]
    public class IGKXCommandTextBox : IGKXUserControl 
    {
        private MemoryStream m_buffer;
        private CommandRichTextBox m_textBox;

        /// <summary>
        /// represent a comman richtext box
        /// </summary>
        class CommandRichTextBox : RichTextBox 
        {
            private IGKXCommandTextBox m_owner;
            public CommandRichTextBox(IGKXCommandTextBox owner)
            {
                this.SetStyle(ControlStyles.SupportsTransparentBackColor, false);
                this.m_owner = owner;
                this.Dock = DockStyle.Fill;
                this.BorderStyle = System.Windows.Forms.BorderStyle.None;
                this.KeyPress += _KeyPress;
            }
            protected override void OnFontChanged(EventArgs e)
            {
                base.OnFontChanged(e);
            }
            protected override void OnParentFontChanged(EventArgs e)
            {
                base.OnParentFontChanged(e);
                this.Font = this.Parent.Font;
            }
            protected override void OnParentBackColorChanged(EventArgs e)
            {
                base.OnParentBackColorChanged(e);
                this.BackColor = Color.FromArgb (255, this.Parent.BackColor);
            }
            void _KeyPress(object sender, KeyPressEventArgs e)
            {
                  int sline = getSelectedLine();            
            int v_llength = this.Lines.Length;
            if ((sline >= 0) && (sline < v_llength- 1))
            {
                e.Handled = true;
                return;
            }

            switch ((Keys)e.KeyChar )
            {
                case Keys.Enter:
                    sline--;
                    var v_buff = this.m_owner.m_buffer;
                    
                    if ((v_llength > 0) && (sline >= 0))
                    {
                        var m = new StreamWriter(v_buff);
                        m.WriteLine(this.Lines[sline]);
                        m.Flush();
                        v_buff.Seek(0, SeekOrigin.Begin);
                        var s = new StreamReader(v_buff).ReadLine();
                        //clear buffer 
                        this.m_owner.m_buffer.SetLength(0);
                        this.m_owner.Command = s;
                    }
                    break;
                default:
                    break;
            }
        }
           
            internal int getSelectedLine()
            {
                int x = this.SelectionStart;
                string txt = this.Text;
                string g2 = txt.Substring(x);
                var l = new string[] { "\n" };
                int lx = txt.Split(l, StringSplitOptions.None).Length;
                int ly = g2.Split(l, StringSplitOptions.None).Length;
                int i = lx - ly;
                return (i <= 0) ? 0 : i;
            }
        }

        [Browsable (false)]
        [DesignerSerializationVisibility (DesignerSerializationVisibility.Hidden )]
        /// <summary>
        /// get or set the text
        /// </summary>
        public override string Text
        {
            get { return this.m_textBox.Text; }
            set
            {
                this.m_textBox.Text = value;
            }
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                m_buffer.Dispose();
                m_buffer = null;
            }
            base.Dispose(disposing);
        }
        public IGKXCommandTextBox()
        {
            this.m_buffer = new MemoryStream();
            this.m_textBox = new CommandRichTextBox(this);
            this.InfoColor = Color.DeepSkyBlue;
            this.ConsoleColor = Color.Black;
            this.ErrorColor = Color.Red;
            this.Controls.Add(this.m_textBox);
        }

        private string m_Command;
        /// <summary>
        /// get or set the requested command
        /// </summary>
        public string Command
        {
            get { return m_Command; }
            set
            {
                 //you can execute command over and over again
                 m_Command = value;
                 OnCommandChanged(EventArgs.Empty);               
            }
        }
        public event EventHandler CommandChanged;
        ///<summary>
        ///raise the CommandChanged 
        ///</summary>
        protected virtual void OnCommandChanged(EventArgs e)
        {
            if (CommandChanged != null)
                CommandChanged(this, e);
        }


        public void EWriteLine(string message)
        {
            this.m_textBox.SelectionLength = 0;
            Color cl = this.m_textBox.SelectionColor;
            this.m_textBox.SelectionColor = this.ErrorColor;
            this.m_textBox.AppendText(message+"\n");
            this.m_textBox.SelectionColor = cl;
        }
        public void CWriteLine(string message)
        {
            this.m_textBox.SelectionLength = 0;
            Color cl = this.m_textBox.SelectionColor;
            this.m_textBox.SelectionColor = this.ConsoleColor;
            this.m_textBox.AppendText(message + "\n");
        }

        public void IWriteLine(string message)
        {
            this.m_textBox.SelectionLength = 0;
            Color cl = this.m_textBox.SelectionColor;
            this.m_textBox.SelectionColor = this.InfoColor;
            this.m_textBox.AppendText(message + "\n");
            this.m_textBox.SelectionColor = cl;
        }

        public Color InfoColor { get; set; }
        public Color ErrorColor { get; set; }
        public Color ConsoleColor { get; set; }

        public void WriteLine(string message, Color color)
        {
            this.m_textBox.SelectionLength = 0;
            Color cl = this.m_textBox.SelectionColor;
            this.m_textBox.SelectionColor = color;
            this.m_textBox.AppendText(message + "\n");
            this.m_textBox.SelectionColor = cl;
        }
    }
}
