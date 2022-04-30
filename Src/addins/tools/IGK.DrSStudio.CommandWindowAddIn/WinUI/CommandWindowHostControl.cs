using IGK.ICore.WinCore;
using IGK.ICore;using IGK.DrSStudio.WinUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IGK.ICore.WinCore.Controls;

namespace IGK.DrSStudio.WinUI
{
    class CommandWindowHostControl : IGKXToolHostedControl
    {
        IGKXCommandTextBox m_command;

        public event EventHandler CommandChanged {
            add {
                this.m_command.CommandChanged += value;
            }
            remove {
                this.m_command.CommandChanged -= value;
            }
        }
        /// <summary>
        /// get or set the command
        /// </summary>
        public string Command {
            get {
                return this.m_command.Command;
            }
            set {
                this.m_command.Command = value;
            }
        }
        

        public CommandWindowHostControl()
        {
            m_command = new IGKXCommandTextBox();
            this.Controls.Add(m_command);
            m_command.Dock = System.Windows.Forms.DockStyle.Fill;
            m_command.BackColor = Colorf.FromFloat(0.7f).ToGdiColor();
        }

        public void EWriteLine(string message)
        {
            this.m_command.EWriteLine(message);
        }

        internal void CWriteLine(string message)
        {
            this.m_command.CWriteLine(message);
        }

        internal void IWriteLine(string message)
        {
            this.m_command.IWriteLine(message);
        }

        internal void IWriteLine(string message, Colorf color)
        {
            this.m_command.WriteLine(message, color.ToGdiColor());
        }
    }
}
