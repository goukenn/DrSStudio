using IGK.GLLib;
using IGK.ICore;
using IGK.ICore.WinUI;
using IGK.OGLGame.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IGK.OGLGame.V2.WinUI
{
    class GLGameControl : Control
    {
        private OGLGraphicsDevice m_device;
        protected override void OnHandleCreated(EventArgs e)
        {
            this.m_device = GLGameUtility.InitDevice(this.Handle, false, 3, 0);
 	         base.OnHandleCreated(e);
        }
        protected override void OnHandleDestroyed(EventArgs e)
        {
          
         
            base.OnHandleDestroyed(e);
            DestroyDevice();
        }

        protected void DestroyDevice()
        {
            if (m_device != null)
            {
                m_device.Dispose();
                m_device = null;
            }
        }
        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            if (this.m_device != null)
            {
                this.m_device.MakeCurrent();
                this.m_device.Viewport = this.GetViewPort();
                this.InitDisplay();
            }
        }
        private void InitDisplay()
        {
            this.m_device.Viewport = this.GetViewPort();
        }
        public Rectanglei GetViewPort()
        {
            return new Rectanglei(0, 0, this.Width, this.Height);
        }
        //public void AddMessageFilter(ICoreMessageFilter filter)
        //{
        //    WinCoreMessageFilter.RegisterFilter(filter);
        //}

        //public void RemoveMessageFilter(ICoreMessageFilter filter)
        //{
        //    WinCoreMenuMessageFilter.RemoveFilter(filter);
        //}

        public OGLGraphicsDevice  Device { get { return m_device; } }
    }
}
