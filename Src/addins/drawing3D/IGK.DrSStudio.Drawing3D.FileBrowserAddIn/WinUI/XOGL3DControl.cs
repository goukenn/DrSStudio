

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: XOGL3DControl.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
/*
IGKDEV - [2008 - 2014]
author: C.A.D . BONDJE DOUE
file:XOGL3DControl.cs
*/
/* 
-------------------------------------------------------------------
Company: IGK-DEV
Author : C.A.D. BONDJE DOUE
SITE : http://www.igkdev.be
Application : DrSStudio
powered by IGK - DEV &copy; 2008-2012
THIS FILE IS A PART OF THE DRSSTUDIO APPLICATION. SEE "License.txt"
FOR MORE INFORMATION ABOUT THE LICENSE
------------------------------------------------------------------- 
*/
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing.Drawing2D ;
using System.Windows.Forms ;
using System.Drawing ;
namespace IGK.DrSStudio.FBAddIn.WinUI
{
    
using IGK.ICore;using IGK.DrSStudio.WinUI;
    using IGK.OGLGame;
    using IGK.OGLGame.Graphics;
    public class XOGL3DControl : IGKXControl, IOGLGGraphicsView 
    {
        private OGLGraphicsDevice m_Device;
        private Timer m_timer;
        public XOGL3DControl()
        {
            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.SetStyle(ControlStyles.UserPaint, true);
            this.SizeChanged += new EventHandler(XOGL3DControl_SizeChanged);
            this.Paint += new PaintEventHandler(_Paint);
        }
        void _Paint(object sender, PaintEventArgs e)
        {
            //this.Render();
        }
        protected virtual bool RequireDoubleBuffer {
            get {
                return true;
            }
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            //base.OnPaint(e);
        }
        protected override void OnPaintBackground(PaintEventArgs pevent)
        {
            //no background painting
            //base.OnPaintBackground(pevent);
        }
        protected override void OnResize(EventArgs e)
        {
            if (this.Device != null)
            {
                this.Device.MakeCurrent();
                this.InitView();
                this.Render();
            }
            base.OnResize(e);            
        }
        void XOGL3DControl_SizeChanged(object sender, EventArgs e)
        {
            if (this.Device != null)
            {
                this.Device.MakeCurrent();
                this.InitView();
            }
        }
        protected override bool IsInputKey(System.Windows.Forms.Keys keyData)
        {
            switch (keyData)
            { 
                case Keys.Left :
                case Keys.Right :
                case Keys.Up :
                case Keys.Down :
                    return true;
            }
            return base.IsInputKey(keyData);
        }
        protected virtual void InitView()
        {
            this.Device.Viewport = this.ClientRectangle;
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (this.m_Device != null)
                {
                    m_Device.Dispose();
                    m_Device = null;
                }
                if (this.m_timer != null)
                {
                    this.m_timer.Dispose();
                    this.m_timer = null;
                }
            }
            base.Dispose(disposing);
        }
        public bool Animated
        {
            get {
                if (this.m_timer !=null)
                return this.m_timer.Enabled;
                return false;
            }
            set {
                if (this.m_timer != null)
                {
                    this.m_timer.Enabled = value;
                }
            }
        }
        public OGLGraphicsDevice Device
        {
            get { return m_Device; }
        }
        public virtual void Render()
        {
            if (Device == null)
                return;
            Device.MakeCurrent();
            Device.Clear(enuBufferBit.Depth, Colorf.Black);
            Device.EndScene();
        }
        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            m_Device = OGLGraphicsDevice.CreateDeviceFromHWND(this.Handle,
                 32, 24,
                 ((this.RequireDoubleBuffer ) ? enuGLCreationFlag.DoubleBuffer :  enuGLCreationFlag .None)
            | enuGLCreationFlag.DrawToWindow | enuGLCreationFlag.SupportOpenGL,
                   IGK.GLLib.enuGLPixelMode.RGBA,
                    IGK.GLLib.enuGLPlane.MainPlane);
            if (m_Device == null)
            {
                throw new InvalidOperationException("Can't create a gl device");
            }
            this.InitDevice();
            this.m_timer = new Timer();
            this.m_timer.Interval = 1;//20
            this.m_timer.Enabled = IsAnimated ;
            this.m_timer.Tick += new EventHandler(m_timer_Tick);
        }
        protected virtual bool IsAnimated{
            get {
                return true;
            }
    }
        protected virtual void InitDevice()
        {
        }
        void m_timer_Tick(object sender, EventArgs e)
        {
           this.Render();
        }
        protected override void OnHandleDestroyed(EventArgs e)
        {
            base.OnHandleDestroyed(e);
        }
        public virtual Rectanglei GetViewPort()
        {
            return this.ClientRectangle;
        }
    }
}

