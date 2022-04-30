using IGK.ICore.WinCore;
using IGK.ICore;
using IGK.ICore.WinUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IGK.DrSStudio.WinUI
{
    /// <summary>
    /// represent a base splashscreen
    /// </summary>
    public class XSplashScreenForm : XForm, ICoreStartForm
    {
        private Type m_mainFormType;
        protected override System.Drawing.Size DefaultSize
        {
            get
            {
                
                return new System.Drawing.Size(200, 170);
            }
        }

        public virtual void SendMessage(string message)
        {

        }

        public virtual void Run(Type mainFormType)
        {
            this.m_mainFormType = mainFormType;
            Application.Run(this);
        }
    }
}
