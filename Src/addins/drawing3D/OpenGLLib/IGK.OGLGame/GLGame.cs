

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: GLGame.cs
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
file:GLGame.cs
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
using System.Text;
//using Microsoft.Xna.Framework.Input ;

using IGK.ICore;using IGK.GLLib ;
using IGK.OGLGame.Graphics;
using IGK.OGLGame.Math;
using IGK.OGLGame.Graphics.Native;
namespace IGK.OGLGame
{
    using IGK.OGLGame.Input;
    using IGK.OGLGame.WinUI;
    /// <summary>
    /// represent the default game 
    /// </summary>
    public abstract class GLGame : IDisposable, IOGLGGraphicsView 
    {
        private GLGameTime m_gameTime;
        private enuGLCreationFlag  m_Flags;
        private enuGLPlane m_Plane;
        private enuGLPixelMode m_pixformat;
		private bool m_isPause;
		private static System.Windows .Forms.MethodInvoker sm_UpdateComplete;
        public OGLGamePIXFormatInitilizer PixelFormatInitilizer;
        public OGLGameCreateDeviceProc CreateDeviceProc;

		internal  static void AddUpdateComplete(System.Windows.Forms.MethodInvoker method)
		{
			sm_UpdateComplete += method ;
		}
		internal  static void RemoveUpdateComplete(System.Windows.Forms.MethodInvoker method)
		{
			sm_UpdateComplete -= method ;
		}
		/// <summary>
		/// Gets a value indicating whether this instance is paused.
		/// </summary>
		/// <value>
		/// <c>true</c> if this instance is paused; otherwise, <c>false</c>.
		/// </value>
		public bool IsPaused{get{return this.m_isPause ;}}
        public event EventHandler Disposing;
        private GLGameWindow m_window;
        private OGLGraphicsDevice m_gl; //graphic library
        /// <summary>
        /// get or set the creation flags of the game windows
        /// </summary>
        internal protected enuGLCreationFlag  Flags {
            get {
                return this.m_Flags;
            }
            set {
                this.m_Flags = value;
            }
        }
        internal protected enuGLPlane Plane {
            get { return this.m_Plane; }
            set { this.m_Plane = value; }
        }
        internal protected enuGLPixelMode PixelFormat
        {
            get { return this.m_pixformat ; }
            set { this.m_pixformat  = value; }
        }
        [Obsolete("used GD instead for shorcut")]
        public OGLGraphicsDevice GraphicsDevice
        {
            get { 
                return m_gl; 
            }
            internal set {
                m_gl = value ; 
            }
        }

        public OGLGraphicsDevice GD {
            get {
                return m_gl;
            }
            internal set {
                m_gl = value;
            }
        }
        public GLGameWindow Window {
            get {
                return this.m_window;
            }
        }
		/// <summary>
		/// Pause the game
		/// </summary>
		public void Pause()
		{
			this.m_isPause = true ;
		}
		public void Play(){
			this.m_isPause = false ;
		}
        public GLGame()
        {
            this.m_gameTime = new GLGameTime();
            this.m_Flags =enuGLCreationFlag .DoubleBuffer  | enuGLCreationFlag .DrawToWindow;
            this.m_pixformat = enuGLPixelMode.RGBA;
            this.m_Plane = enuGLPlane.MainPlane;
        }
        public bool IsDisposed {
            get {
                return this.Window.Form.IsDisposed;
            }
        }
       
        #region IDisposable Members
        public void Dispose()
        {
            OnDisposing(EventArgs.Empty);            
			if (this.GD !=null){
				this.UnloadContent();
            	this.GD.Dispose();
			}
        }
        protected virtual void OnDisposing(EventArgs eventArgs)
        {
                this.Disposing?.Invoke(this, eventArgs);
            
        }
        #endregion
        //class MessageLister : System.Windows.Forms.NativeWindow
        //{
        //    GLGame game;
        //    internal MessageLister(GLGame game)
        //    {
        //        this.game = game;
        //        this.AssignHandle(game.Window.Form.Handle);
        //    }
        //    protected override void WndProc(ref System.Windows.Forms.Message m)
        //    {
        //        switch (m.Msg)
        //        {
        //            case 0x0012 : //"User32Lib.WM_QUIT:
        //                break;
        //            default :
        //                this.game.Tick();
        //                break;
        //        }
        //        base.WndProc(ref m);               
        //    }
        //}
        /// <summary>
        /// run the sampel game
        /// </summary>
        public void Run()
        {
            if (this.m_window == null)
            this.m_window = GLGameWindow.CreateWindow(this, out this.m_gl);
            if (this.m_gl == null)
            {
                throw new OGLGameException (enuGLError.InvalidOperation, "Failed to Create a OpenGL graphics device");
            }
            this.Initialize();
            this.Window.Show();
            this.LoadContent();            
            this.m_gameTime.Init();
            System.Windows.Forms.Application.RaiseIdle(EventArgs.Empty);
            //message loop
            while (this.Window.Created )            
            {           
                    System.Windows.Forms.Application.DoEvents();
                    this.Tick();
            }
            this.UnloadContent();
        }
        public void Exit()
        {
            this.Window.Close();
        }

        /// <summary>
        /// check or throw error
        /// </summary>
        protected virtual  void ThrowError()
        {
            uint i = GL.glGetError();
            if (i != 0)
            { 
                throw new GLGameException($"Error Append Code:{i} : \"{(enuGLError)i}\"");
            }
        }

        /// <summary>
        /// Tick Method. update the current rendering device
        /// </summary>
        public void Tick()
        {
            //if this disposed return
            if (this.IsDisposed)
                return;
            //check taht for some unknow reson graphics disposed
            if (this.GD.IsDisposed)
                return;
            this.GD.MakeCurrent();
			if (!this.m_isPause )
            {
                this.m_gameTime.Tick();
			}
            this.Update(m_gameTime);
			this.RaiseUpdateComplete();
            this.Render (m_gameTime);
            OnGameTick(this);       
        }
		private void RaiseUpdateComplete(){
				sm_UpdateComplete?.Invoke ();
		}
        private static void OnGameTick(GLGame game)
        {
                GameTick?.Invoke(game, EventArgs.Empty);
        }
        /// <summary>
        /// event raised when game tick
        /// </summary>
        public static event EventHandler GameTick;
        /// <summary>
        /// override this method to load your game Content
        /// </summary>
        protected virtual void LoadContent() {
        }
        /// <summary>
        /// override this to unload your game content
        /// </summary>
        protected virtual void UnloadContent() {
        }
        /// <summary>
        /// override this method to update your game logic
        /// </summary>
        /// <param name="time"></param>
        protected virtual void Update(GLGameTime gameTime) {
        }
        /// <summary>
        /// override to render your game logic
        /// </summary>
        /// <param name="gameTime"></param>
        protected abstract  void Render(GLGameTime gameTime);

        /// <summary>
        /// initialize you game work. first call the base class
        /// </summary>
        protected virtual void Initialize()
        {
            //initialize your game logic			
        }

        public virtual Rectanglei GetViewPort ()
		{
			return new Rectanglei(0,0, this.Window.Width, this.Window.Height);
		}

        internal void SetWindow(GLGameWindow gLGameWindow)
        {
            this.m_window = gLGameWindow;
        }
    }
}

