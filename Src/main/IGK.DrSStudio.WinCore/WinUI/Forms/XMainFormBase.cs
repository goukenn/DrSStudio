using IGK.ICore.WinCore;
using IGK.ICore;
using IGK.ICore.WinUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.DrSStudio.WinUI
{
    /// <summary>
    /// represent a base mainform class 
    /// </summary>
    public class XMainFormBase : XForm , ICoreMainForm
    {
        private bool m_FullScreen;
        private bool m_ShowMenu;


       
        public XMainFormBase():base()
        {
            CoreRenderer.RenderingValueChanged += _RenderingValueChanged;    
        }
        /// <summary>
        /// run app
        /// </summary>
        public virtual void Run()
        {
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                CoreRenderer.RenderingValueChanged -= _RenderingValueChanged;    
            }
            base.Dispose(disposing);
        }

        void _RenderingValueChanged(object sender, EventArgs e)
        {
            this.Invalidate();
        }


        public bool ShowMenu
        {
            get { return m_ShowMenu; }
            set
            {
                if (m_ShowMenu != value)
                {
                    m_ShowMenu = value;
                    OnShowMenuChanged(EventArgs.Empty);
                }
            }
        }
        public event EventHandler ShowMenuChanged;

        protected virtual void OnShowMenuChanged(EventArgs e)
        {
            if (ShowMenuChanged != null)
            {
                ShowMenuChanged(this, e);
            }
        }



        public bool FullScreen
        {
            get { return m_FullScreen; }
            set
            {
                if (m_FullScreen != value)
                {
                    m_FullScreen = value;
                    OnFullScreenChanged(EventArgs.Empty);
                }
            }
        }

        ICoreSystemWorkbench ICoreWorkbenchHost.Workbench => this.Workbench;

        public event EventHandler FullScreenChanged;

        protected virtual void OnFullScreenChanged(EventArgs e)
        {
            FullScreenChanged?.Invoke(this, e);
        }


    }
}
