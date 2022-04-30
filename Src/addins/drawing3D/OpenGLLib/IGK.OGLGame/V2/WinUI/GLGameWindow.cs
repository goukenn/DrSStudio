using IGK.OGLGame.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace IGK.OGLGame.V2.WinUI
{
    class GLGameWindow : Form
    {
        private GLGameControl m_gameControl;
        private bool m_useCursor;
        private bool m_curHide;

        public GLGameWindow()
        {
            m_useCursor = true;
            m_gameControl = new GLGameControl();
            m_gameControl.CreateControl();
            this.m_gameControl.Dock = DockStyle.None ;
            this.m_gameControl.Size = new System.Drawing.Size(400, 400);
			this.m_gameControl.BackColor = Color.Red;
            this.Controls.Add(m_gameControl);
            m_curHide = false;
            m_gameControl.MouseEnter += m_gameControl_MouseEnter;
            m_gameControl.MouseLeave += m_gameControl_MouseLeave;

            //Cursor.Hide();
        }

        void m_gameControl_MouseLeave(object sender, EventArgs e)
        {
            if (!this.m_useCursor)
            {
                Cursor.Show();
                m_curHide = false;
            }
        }

        void m_gameControl_MouseEnter(object sender, EventArgs e)
        {
            if (!this.m_useCursor){
                Cursor.Hide();
                m_curHide = true;
            }
           
        }

        public OGLGraphicsDevice Device { get { return m_gameControl.Device; } }

        public bool UseMouse { get { 
            return this.m_useCursor;
        } set{
            if (value != this.m_useCursor)
            {
                this.m_useCursor = value;
                OnUseCursorChanged(EventArgs.Empty);
            }
        } }

        private void OnUseCursorChanged(EventArgs eventArgs)
        {
            if (!this.m_useCursor )
            {
                var s = this.m_gameControl.ClientRectangle.Contains(PointToClient(Control.MousePosition));

                if (!s && m_curHide )
                {
                    Cursor.Show();
                    m_curHide = false;
                }
                else if (s && !m_curHide )
                {
                    Cursor.Hide();
                    m_curHide = true;
                }

            }
          

        }
       
    }
}
