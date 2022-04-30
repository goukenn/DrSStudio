using IGK.ICore;
using IGK.ICore.WinCore.Caret;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IGK.ICore.WinCore.Caret
{
    public class CaretManager : IWinCoreCaretManager
    {
        private Control m_form;
        private Rectanglei m_bounds;
        private Caret m_caret;
        private Vector2f m_Location;
        private bool m_InsertMode;

        public bool InsertMode
        {
            get { return m_InsertMode; }
            set
            {
                if (m_InsertMode != value)
                {
                    m_InsertMode = value;
                    OnInsertModeChanged(EventArgs.Empty);
                }
            }
        }
        public event EventHandler InsertModeChanged;

        protected virtual void OnInsertModeChanged(EventArgs e)
        {
            if (InsertModeChanged != null)
            {
                InsertModeChanged(this, e);
            }
        }

        public IntPtr ControlHandle
        {
            get { 
                return this.m_form.Handle; 
            }
        }
       
       
        public void Activate(bool active)
        {
            if (active)
                __show();
            else
                __hide();
            this.m_active = active;
        }

        private void __show()
        {
            
            this.m_caret.Show();
        }

        public Rectanglei Bounds
        {
            get
            {
                return this.m_bounds;
            }
            set
            {
                this.m_bounds = value;
            }
        }
        public CaretManager(Control frm, bool focusmanager)
        {
            this.m_form = frm;
            
            this.m_form.HandleCreated += m_form_HandleCreated;
            this.m_form.HandleDestroyed += m_form_HandleDestroyed;

            if (focusmanager) { 
                this.m_form.GotFocus +=m_form_GotFocus;
                this.m_form.LostFocus += m_form_LostFocus;
            }
            if (this.m_form.IsHandleCreated) {
                __CreateCaret();
            }

        }

      
        void m_form_LostFocus(object sender, EventArgs e)
        {
            this.Activate(false);
        }

        void m_form_GotFocus(object sender, EventArgs e)
        {
            this.Activate(true);
        }

        void _LocationChanged(object sender, EventArgs e)
        {
            __setLocation();
           
        }

        private void __CreateCaret()
        {
            int w = (this.InsertMode) ? 32 : 1;
            this.m_caret = Caret.CreateCaret(this.ControlHandle, 1, 16);            
            Activate(this.m_active);
            this.__setLocation();
        }

        private void __setLocation()
        {
            if (this.m_caret != null)
            {
                this.m_caret.SetPosition((int)Math.Ceiling(this.m_Location.X), (int)Math.Ceiling(this.m_Location.Y));
            }
        }

        private void __hide()
        {
            if (this.m_caret != null)
            {
                this.m_caret.Hide();
            }
        }

        void m_form_HandleDestroyed(object sender, EventArgs e)
        {
            if (this.m_caret != null)
            {
                this.m_caret.Dispose();
                this.m_caret = null;
            }
        }

        void m_form_HandleCreated(object sender, EventArgs e)
        {
            __CreateCaret();
        }

        public void Dispose()
        {
            if (this.m_caret != null)
            {
                this.m_caret.Hide();
                this.m_caret.Dispose();
                this.m_caret = null;
            }
            //free handle
            this.m_form.HandleCreated -= m_form_HandleCreated;
            this.m_form.HandleDestroyed -= m_form_HandleDestroyed;
        }


        private bool m_active;


        public void Hide()
        {
            __hide();
        }

        public void Show()
        {
            __show();
        }

        public void SetPosition(float x, float y)
        {
            this.m_Location = new Vector2f(x, y);
            this.__setLocation();
        }


        public void SetSize(int width, int height)
        {
            this.m_caret.SetSize(width, height);
        }

        public Vector2f Location
        {
            get {
                return this.m_caret.Location;
            }
        }
    }
}
