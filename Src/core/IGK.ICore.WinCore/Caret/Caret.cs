using IGK.ICore.WinUI;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace IGK.ICore.WinCore.Caret
{
    public sealed class Caret : IDisposable , ICoreCaret
    {
        private IntPtr m_handle;
        /// <summary>
        /// .ctr
        /// </summary>
        private Caret()
        {              
        }
        public void Show() {
            if (!ShowCaret(this.m_handle) &&(  this.m_handle != IntPtr.Zero ))
            {
                SetSize(this.m_width, this.m_height);
                ShowCaret(this.m_handle);
            }
        }
        public void Hide() {
         HideCaret(this.m_handle);
        }
        /// <summary>
        /// create caret 
        /// </summary>
        /// <param name="Hwnd"></param>
        /// <returns></returns>
        public static Caret CreateCaret(IntPtr Hwnd, int width, int height)
        {
            bool v_b = CreateCaret(Hwnd, IntPtr.Zero, width , height);
            if (v_b) {
                Caret c = new Caret();
                c.m_handle = Hwnd;
                c.m_width = width;
                c.m_height = height;
                return c;
            }
            return null;
        }
        public void SetPosition(int x, int y) {
            SetCaretPos(x, y);
        }

        public void Dispose()
        {
            if (this.m_handle != IntPtr.Zero)
            {
                if (GetActiveWindow() == this.m_handle)
                {
                    DestroyCaret();
                }
                this.m_handle = IntPtr.Zero;
            }
        }
        #region DLLCaret function
        const string USER32_LIB = "user32.dll";
        private int m_width;
        private int m_height;
        [DllImport(USER32_LIB )]
        private static extern void DestroyCaret();
        [DllImport(USER32_LIB)]
        private static extern bool ShowCaret(IntPtr hwnd);
        [DllImport(USER32_LIB)]
        private static extern void SetCaretPos(int x, int y);
        [DllImport(USER32_LIB)]
        private static extern void GetCaretPos(ref Point pt);
        [DllImport(USER32_LIB)]
        private static extern bool HideCaret(IntPtr hwnd);
        [DllImport(USER32_LIB)]
        private static extern bool 
CreateCaret(
    IntPtr hWnd,
    IntPtr hBitmap,
    int nWidth,
     int nHeight);


        [DllImport(USER32_LIB)]
        private extern static IntPtr GetActiveWindow();

        #endregion

        //public  void SetHeight(int height)
        //{
        //    DestroyCaret();
        //    bool b = CreateCaret(this.m_handle, IntPtr.Zero, 1, height);
        //}

        public Vector2f Location
        {
            get
            {
                Point pt = Point.Empty;
                GetCaretPos(ref pt);
                return new Vector2f(pt.X, pt.Y);
            }
            set
            {
                this.SetPosition(value.X, value.Y);
            }
        }

        public void SetPosition(float x, float y)
        {
            this.SetPosition ((int)x, (int)y);
        }

        public void Activate(bool activate)
        {
            if (activate)
                this.Show();
            else
                this.Hide();
        }


        public void SetSize(int width, int height)
        {
            if ((width > 0) && (height > 0))
            {
                if (GetActiveWindow() == this.m_handle)
                {
                    DestroyCaret();
                }
                else { 
                }
                bool b = CreateCaret(this.m_handle, IntPtr.Zero, width, height);
                if (b)
                {
                    this.m_width = width;
                    this.m_height = height;
                }
            }
        }
    }
}
