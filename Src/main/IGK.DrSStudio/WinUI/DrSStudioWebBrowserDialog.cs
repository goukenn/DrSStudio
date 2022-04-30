
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IGK.DrSStudio.WinUI
{
    using IGK.ICore.Web;
    using IGK.ICore.WinCore.WinUI.Controls;
    using IGK.ICore.WinUI;
    using IGK.ICore.WinUI.Registrable;

    public class DrSStudioWebBrowserDialog : XFormDialog, ICoreWebDialogForm 
    {
        private IWebBrowserControl m_control;

        public DrSStudioWebBrowserDialog():base()
        {
            var c  = new IGKXWebBrowserControl();
            c.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Controls.Add(c);
            m_control = c;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
        }

        public DrSStudioWebBrowserDialog(IWebBrowserControl control):base()
        {
            System.Windows.Forms.Control c = control as System.Windows.Forms.Control;
            if (c == null)
            {
                throw new ArgumentException ($"{nameof(control)} not a windows form control");
            }
            this.m_control = control;
            this.Controls.Add(c);
            this.m_control = control;
            c.Dock = System.Windows.Forms.DockStyle.Fill;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
        }

        public ICoreWebControl WebControl
        {
            get { return this.m_control; }
        }
       
    }
}
