using IGK.ICore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.OGLGame.V2
{

    using IGK.OGLGame.Graphics;
    using IGK.OGLGame.V2.WinUI;
    using System.Windows.Forms;

    /// <summary>
    /// represent a basic glgame component
    /// </summary>
    public abstract class GLGame : IDisposable, IGLGame
    {
        #region members declaration
        private Size2i m_WindowSize;        
        private GLGameWindow m_windows;
     
        #endregion

        /// <summary>
        /// get or set if this game support mouse view
        /// </summary>
        public bool UseMouse { get { return m_windows.UseMouse; } set { m_windows.UseMouse = value; } }
        /// <summary>
        /// get the graphics device of this game window
        /// </summary>
        public OGLGraphicsDevice Device
        {
            get {
                return m_windows.Device;
            }
        }
        public string Title
        {
            get { return this.m_windows.Text; }
            set
            {
                this.m_windows.Text = value;
            }
        }


        public event EventHandler TitleChanged {
            add {
                this.m_windows.TextChanged += value;
            }
            remove {
                this.m_windows.TextChanged -= value;
            }
        }
        public event EventHandler WindowSizeChanged;
 
        /// <summary>
        /// get or se tthe windows size
        /// </summary>
        public Size2i WindowSize
        {
            get { return m_WindowSize; }
            set
            {
                if (m_WindowSize.Equals(value))
                {
                    m_WindowSize = value;
                    OnWindowSizeChanged(EventArgs.Empty );
                }
            }
        }

        private void OnWindowSizeChanged(EventArgs eventArgs)
        {
            if (this.WindowSizeChanged != null) {
                this.WindowSizeChanged(this, eventArgs);
            }
        }
        public virtual void Init()
        {
            
        }

        public virtual void Update()
        {
            
        }

        public virtual void Render()
        {
            
        }
        

       

       

        public virtual void Dispose()
        {
            this.m_windows.Dispose();
        }
        /// <summary>
        /// run the game logic
        /// </summary>
        public void Run() {
            this.m_windows.Show();
            GLGameTime time = GLGameTime.Start();
			if (this.Device == null) {
				System.Diagnostics.Debug.WriteLine ("Device is Null");
				Application.Exit ();
				return;
			}
			this.Device.MakeCurrent ();
            while (this.m_windows.Created) {
                Application.DoEvents();
                if (this.Device != null)
                {
                    time.Update();
                    this.Update(time);
                    this.Render(time);
                }
            }
        }
        /// <summary>
        /// update your game logic
        /// </summary>
        /// <param name="time"></param>
        protected virtual void Update(GLGameTime time)
        {            
        }
        public virtual void Render(GLGameTime time)
        {
        }
        public GLGame()
        {
            this.m_windows = new GLGameWindow();
            this.m_WindowSize = new Size2i(900, 400);
            this.WindowSizeChanged += __sizeChanged;
            this.TitleChanged += GLGame_TitleChanged;


            __sizeChanged(this, EventArgs.Empty);
            
        }

        void GLGame_TitleChanged(object sender, EventArgs e)
        {
            this.m_windows.Text = this.Title;
        }

        private void __sizeChanged(object sender, EventArgs e)
        {
            this.m_windows.Size = new System.Drawing.Size (this.WindowSize.Width, this.WindowSize.Height );
        }
    }
}
