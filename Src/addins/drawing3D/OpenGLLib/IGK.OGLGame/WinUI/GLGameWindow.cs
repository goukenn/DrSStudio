

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: GLGameWindow.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
/*
IGKDEV @ 2008-2016
author: C.A.D . BONDJE DOUE
file:GLGameWindow.cs
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
/* 
-------------------------------------------------------------
This file is part of iGK-DEV-DrawingStudio
-------------------------------------------------------------
-------------------------------------------------------------
-------------------------------------------------------------
view license file in Documentation folder to get more info
Copyright (c) 2008-2010 
Author  : Charles A.D. BONDJE DOUE 
mail : bondje.doue@hotmail.com
-------------------------------------------------------------
-------------------------------------------------------------
*/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices ;

using IGK.ICore;
using IGK.GLLib;
using IGK.OGLGame.Graphics;
using System.Reflection;

namespace IGK.OGLGame.WinUI
{
    /// <summary>
    /// is the manager of windows game
    /// </summary>
    public partial class GLGameWindow : IGLGameWindow
    {
		/// <summary>
		/// Adds the control.
		/// </summary>
		/// <param name='control'>
		/// Control.
		/// </param>
		public void AddControl (Control control)
		{
			if (control !=null){
			this.m_form.Controls.Add (control );				
			}
		}
		/// <summary>
		/// Removes the control.
		/// </summary>
		/// <param name='control'>
		/// Control.
		/// </param>
		public void RemoveControl(Control control)
		{
			if (control !=null)
			{
				this.m_form.Controls.Remove (control );
			}
		}
        private GLForm m_form;
        private GLGame m_game;
        private bool m_showCursor;
        private bool m_AllowUserResizing;
        private string m_deviceName;
        private bool m_fullmode;
        private bool m_beginDeviceChanged;
        //constante
        //internal const int WM_QUIT = 0x0012;
        //public event KeyPressEventHandler KeyPress {
        //    add {
        //        this.m_form.KeyPress += value;
        //    }
        //    remove {
        //        this.m_form.KeyPress -= value;
        //    }
        //}
        public Icon Icon {
            get {
                return this.m_form.Icon;
            }
            set {
                this.m_form.Icon = value;
            }
        }
		/// <summary>
		/// Sets the client size of the window size.
		/// </summary>
		/// <param name='w'>
		/// Width
		/// </param>
		/// <param name='h'>
		/// Height
		/// </param>
        public void SetSize(int w, int h)
        {
#if OS_WINDOWS
            this.m_form.ClientSize = new Size(w, h);
#elif OS_LINUX
        this.m_form .Size = new Size (w,h);
#endif
        }
        public bool ShowIcon {
            get {
                return this.m_form.ShowIcon;
            }
            set {
                this.m_form.ShowIcon = value;
            }
        }
        public bool AllowUserResizing
        {
            get { return m_AllowUserResizing; }
            set { 
                m_AllowUserResizing = value;
                OnAllowUserResizingChanged(EventArgs.Empty);
            }
        }
        private void OnAllowUserResizingChanged(EventArgs eventArgs)
        {
            if (this.AllowUserResizing)
            {
                this.Form.FormBorderStyle = FormBorderStyle.Sizable;
            }
            else
                this.Form.FormBorderStyle = FormBorderStyle.FixedSingle;
        }
#if OS_LINUX
        public int Width {
            get {
                return this.m_form.Width;
            }
        }
        public int Height
        {
            get{
                return this.m_form.Height;
            }
        }
#elif OS_WINDOWS
        public int Width
        {
            get
            {
                return this.m_form.ClientSize.Width;
            }
        }
        public int Height
        {
            get
            {
                return this.m_form.ClientSize.Height;
            }
        }
#endif
        public event EventHandler ClientSizeChanged;
        public event EventHandler ScreenDeviceNameChanged;
        protected internal virtual void OnClientSizeChanged(EventArgs e)
        {
            this.ClientSizeChanged?.Invoke(this, EventArgs.Empty);
        }
        /// <summary>
        /// get or set the indication of the cursor
        /// </summary>
        public bool ShowCursor {
            get {
                return m_showCursor;
            }
            set {
               // Cursor.Hide ();
              //cursor this.m_form.Cursor.Hide();// = null;
               this.m_showCursor = value;
            }
        }
        internal GLForm Form {
            get { return m_form; }
        }
        public GLGame Game { 
            get{
                return m_game;
            }
        }
        /// <summary>
        /// get or set the title of the gl game window
        /// </summary>
        public string Title {
            get {
                return this.m_form.Text;
            }
            set {
                this.m_form.Text = value;
            }
        }
        public string ScreenDeviceName {
            get {
                return m_deviceName;
            }
            private set {
                m_deviceName = value;
                OnDeviceNameChanged(EventArgs.Empty);
            }
        }
        private void OnDeviceNameChanged(EventArgs eventArgs)
        {
            if (this.ScreenDeviceNameChanged != null)
                this.ScreenDeviceNameChanged(this, eventArgs);
        }
        internal GLGameWindow(GLGame game)
        {
            this.m_form = new GLForm(this);                        
            this.m_game = game;
            this.m_form.HandleDestroyed += new EventHandler(_form_HandleDestroyed);
            this.m_form.SizeChanged += new EventHandler(_form_SizeChanged);
            this.m_form.Paint += new PaintEventHandler(_form_Paint);
            this.m_form.MouseEnter += new EventHandler(m_form_MouseEnter);
            this.m_form.MouseLeave += new EventHandler(m_form_MouseLeave);
            this.m_form.Shown += new EventHandler(m_form_Shown);
            this.m_form.CreateControl();
            this.m_form.Size = Screen.PrimaryScreen.WorkingArea.Size;
            this.Title = System.IO.Path.GetFileNameWithoutExtension(Assembly.GetEntryAssembly().Location);
            this.m_game.SetWindow(this);
            this.OnAllowUserResizingChanged(EventArgs.Empty);
        }
        void m_form_Shown(object sender, EventArgs e)
        {
            OnClientSizeChanged(EventArgs.Empty);
        }
        void m_form_MouseLeave(object sender, EventArgs e)
        {
            Cursor.Show();
        }
        void m_form_MouseEnter(object sender, EventArgs e)
        {
            if (this.ShowCursor)
            {
                Cursor.Show();
            }
            else
                Cursor.Hide();
        }
        void _form_HandleDestroyed(object sender, EventArgs e)
        {
            //this.DestroyHandle();
        }
        private void _form_Paint(object sender, PaintEventArgs e)
        {
            if (this.m_game != null)
                this.m_game.Tick();
        }
        private void _form_SizeChanged(object sender, EventArgs e)
        {
            if (this.m_game.GD == null)
                return;
#if OS_LINUX
            this.m_game.GraphicsDevice.Viewport = new Rectanglei(0,0, this.m_form.Width, this.m_form.Height );//this.m_form.ClientRectangle;
#elif OS_WINDOWS
            this.m_game.GD.Viewport = new Rectanglei(0,0, this.m_form.ClientSize .Width , this.m_form.ClientSize.Height );
#endif
            OnClientSizeChanged(EventArgs.Empty);
        }     
        internal static GLGameWindow CreateWindow(GLGame gLGame, out OGLGraphicsDevice oGLDevice)
        {
            GLGameWindow v_gameWin = new GLGameWindow(gLGame);
            if (gLGame.PixelFormatInitilizer != null) {
                gLGame.PixelFormatInitilizer(v_gameWin.Handle);
            }
            if (gLGame.CreateDeviceProc != null) { 
                WGL.wglMakeCurrent(IntPtr.Zero , IntPtr.Zero);
                IntPtr hdc =IGK.OGLGame.Native.User32Lib.GetDC(v_gameWin.Handle);
                //create normal device handle and make it current
                oGLDevice = OGLGraphicsDevice.CreateDeviceFromHWND(
      v_gameWin.Handle,
      32,
      24,
      gLGame.Flags,
      gLGame.PixelFormat,
      gLGame.Plane);

                IntPtr gldc = gLGame.CreateDeviceProc(hdc);
                if (gldc != IntPtr.Zero)
                {
                    oGLDevice.Dispose();
                    WGL.wglMakeCurrent(IntPtr.Zero, IntPtr.Zero);
                    oGLDevice = OGLGraphicsDevice.CreateFromGLDC(v_gameWin.Handle, hdc, gldc);
                    //gLGame.Info.Version = 
                    //int h = 0;
                    //IGK.GLLib.WGL.wgl
                    //GL.glGetIntegerv((int)IGK.GLLib.WGL.WGL_CONTEXT_MAJOR_VERSION_ARB, ref h);
                    //GL.glGetIntegerv((int)IGK.GLLib.GL.GL_MAJOR_VERSION, ref h);
#if DEBUG
         
                System.Diagnostics.Debug.WriteLine ("GLContext Version "+oGLDevice.Capabilities.Version  + " for "+ GLUtils.GetIntegerv(0x9126));
#endif

                }
                else
                {
                    oGLDevice.Dispose();
                   oGLDevice = null;
                }
                return v_gameWin;
            }

            oGLDevice = OGLGraphicsDevice.CreateDeviceFromHWND(
                v_gameWin.Handle,            
                32,
                24,
                gLGame.Flags,
                gLGame.PixelFormat,
                gLGame.Plane);
            return v_gameWin;
        }
        public  void Close()
        {
            this.m_form.Close();
        }
        public void BeginScreenDeviceChanged(bool fullmode)
        {
            this.m_fullmode = fullmode;
            this.m_beginDeviceChanged = true;
        }
        public void EndScreenDeviceChanged(string devicename, int width, int height)
        {
            if (!this.m_beginDeviceChanged)
                throw new InvalidOperationException("You must first call BeginScreenDeviceChanged");
            this.m_beginDeviceChanged = false;
            this.Form.ClientSize = new Size(width, height);
        }
        internal void Show()
        {
            this.m_form.Show();
        }
        public bool Created {
            get {
                return this.m_form.Created;
            }
        }
        /// <summary>
        /// get the windows handle
        /// </summary>
        public IntPtr Handle {
            get {
                return this.Form.Handle;
            }
        }
        public Vector2f GetMouseLocation()
        {
            Point pt = this.Form.PointToClient(Control.MousePosition);
            return new Vector2f(pt.X, pt.Y);
        }
        /// <summary>
        /// create a game window
        /// </summary>
        /// <returns></returns>
        public static GLGameWindow CreateWindow(GLGame game)
        {
            OGLGraphicsDevice device = null;
            GLGameWindow wnd = GLGameWindow.CreateWindow(game, out device);
            game.GD  = device;
            return wnd;
        }
    }
}

