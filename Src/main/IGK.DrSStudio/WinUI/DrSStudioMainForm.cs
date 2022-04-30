

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: DrsStudioMainForm.cs
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
file:DrsStudioMainForm.cs
*/
using IGK.ICore.WinCore;
using IGK.ICore;
using IGK.ICore.Drawing2D;
using IGK.ICore.Settings;
using IGK.ICore.Windows.Native;
using IGK.ICore.WinUI;
using IGK.Native;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using IGK.ICore.WinCore.WinUI;
using IGK.DrSStudio.Settings;
namespace IGK.DrSStudio.WinUI
{
    /// <summary>
    /// represent a drsstudio main form
    /// </summary>
    public partial class DrSStudioMainForm : XForm, ICoreMainForm, ICoreRunnableMainForm, ICoreWorkbenchHost
    {
        private DrSStudioWorkbench m_workench;
        private bool m_FullScreen;

        public override string Text
        {
            get
            {
                return base.Text;   
            }

            set
            {
                base.Text = value;
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
                }
            }
        }
        public new DrSStudioWorkbench Workbench
        {
            get {
                return m_workench;
            }
        }
        ICoreSystemWorkbench ICoreWorkbenchHost.Workbench {
            get {
                return this.m_workench;
            }
        }

        void sr_ValueChanged(object sender, EventArgs e)
        {
            var s = CoreFont.CreateFrom((string)((ICoreSettingValue )sender).Value ,null);
            if (s != null)
            {
                this.Font = s.ToGdiFont();
                s.Dispose();
                this.Refresh();
            }
        }
     
        void DrsStudioMainForm_SizeChanged(object sender, EventArgs e)
        {
            if (this.WindowState != FormWindowState.Minimized)
            {
                DrSStudioSetting.Instance.MainFormSize = new Size2i (this.Size.Width , this.Size.Height );
                DrSStudioSetting.Instance.MainFormState = this.WindowState;
            }
        }
        void DrsStudioMainForm_LocationChanged(object sender, EventArgs e)
        {
            if (this.WindowState != FormWindowState.Minimized )
                DrSStudioSetting.Instance.Location = new Vector2i (this.Location.X , this.Location.Y );
        }
        public DrSStudioMainForm():base()
        {
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.m_workench = new DrSStudioWorkbench(this);
            this.m_ShowMenu = true;
            InitializeComponent();
            this.MinimumSize = new Size(600, 400);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.AllowHitTest = true;
            this.Load += _Load;
           
        }

        private void _Load(object sender, EventArgs e)
        {
            Vector2i pt = DrSStudioSetting.Instance.Location;
            FormWindowState st = DrSStudioSetting.Instance.MainFormState;
            if (Screen.FromPoint(new Point(pt.X, pt.Y)) == null)
                this.Location = new Point(pt.X, pt.Y);
            this.WindowState = st;
            this.LocationChanged += DrsStudioMainForm_LocationChanged;
            this.SizeChanged += DrsStudioMainForm_SizeChanged;
            var m = WinCoreControlRenderer.DefaultFont;
            this.Font = m.ToGdiFont();
            m.FontDefinitionChanged += (o, ee) =>
            {
                this.Font = m.ToGdiFont();
            };
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            RectangleElement rc = new RectangleElement();
            rc.SuspendLayout();
            rc.Bounds = new Rectanglef(this.ClientRectangle.X,
                this.ClientRectangle.Y,
                this.ClientRectangle.Width,
                this.ClientRectangle.Height);
            rc.FillBrush.SetLinearBrush(
                WinCoreControlRenderer.MainFormStartBorderColor,
                WinCoreControlRenderer.MainFormEndBorderColor
                , 90.0f);
            rc.StrokeBrush.SetSolidColor(Colorf.Transparent);
            rc.ResumeLayout();
            e.Graphics.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.Half;
            rc.Draw(e.Graphics);
        }
        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // DrsStudioMainForm
            // 
            this.ClientSize = new System.Drawing.Size(795, 333);
            this.Name = "DrsStudioMainForm";
            this.ResumeLayout(false);
        }
        private bool m_ShowMenu;
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
        /// <summary>
        /// raised the ShowMenuChanged Event
        /// </summary>
        /// <param name="eventArgs"></param>
        protected void OnShowMenuChanged(EventArgs eventArgs)
        {
            if (ShowMenuChanged != null)
                this.ShowMenuChanged(this, eventArgs);
        }
        /// <summary>
        /// Show menu changed event
        /// </summary>
        public event EventHandler ShowMenuChanged;

        /// <summary>
        /// run main form
        /// </summary>
        public void Run()
        {
            string[] g = Environment.GetCommandLineArgs();
            if ((g != null) && (g.Length > 0))            
            {
                string f = g[g.Length - 1];
                if (System.IO.File.Exists(f)) {
                    this.Workbench.OpenFile(new string[] { f });
                }
            }
                Application.Run(this);
        }
        public override Rectangle DisplayRectangle
        {
            get
            {
                Rectangle v_rc = base.DisplayRectangle;
                if (this.WindowState == FormWindowState.Normal)
                    v_rc.Inflate(-4, -4);
                return v_rc;
            }
        }

        

        protected override void WndProc(ref Message m)
        {
            switch ((enuWindowMessage)m.Msg)
            {
                case  (enuWindowMessage)0x88://WM_NCSYNCPAINT
                    return;
                case enuWindowMessage.WM_ACTIVATE ://activating
                    break;
                case enuWindowMessage.WM_ACTIVATEAPP :// app activate
                    //if (this.WindowState == FormWindowState.Minimized)
                    //    return;
                    break;
                case enuWindowMessage.WM_GETMINMAXINFO:
                    {
                        //override maximize info
                        //to maintain main foromat in the visible working area
                        //------------------------------------------------------------
                        //get the current screen


                        Screen v_src = Screen.FromRectangle(this.Bounds); //Screen.FromPoint(Control.MousePosition);
                        User32.MINMAXINFO v_maxinfo = (User32.MINMAXINFO)Marshal.PtrToStructure(m.LParam, typeof(User32.MINMAXINFO));
                    
                        Rectangle v_rc = Rectangle.Empty;
                        Point v_mouseLocation = this.RestoreBounds.Location;
                        v_maxinfo.ptMaxPosition = new Point(0, 0);
                        if (this.FullScreen == false)
                        {
                            //specify the size 
                            v_maxinfo.ptMaxSize = new Point(v_src.WorkingArea.Width, v_src.WorkingArea.Height);
                            //specify the maximum track size
                            v_maxinfo.ptMaxTrackSize = new Point(v_src.WorkingArea.Width, v_src.WorkingArea.Height);
                            //specify the minimum track size
                            v_maxinfo.ptMinTrackSize = new Point(this.MinimumSize.Width, this.MinimumSize.Height);
                            m.Result = IntPtr.Zero;
                            Marshal.StructureToPtr(v_maxinfo, m.LParam, true);
                            return;
                        }
                    }
                    break;
                case enuWindowMessage.WM_NCACTIVATE:
                    if (this.WindowState != FormWindowState.Minimized)
                    {
                        RenderNonClientArea(m.HWnd, new IntPtr(1));
                        m.Result = new IntPtr(1);
                        Invalidate(true);
                        return;
                    }
                    break;
                case enuWindowMessage.WM_NCHITTEST:
                    if (AllowHitTest)
                    {
                        OnNCHitTest(ref m);
                        return;
                    }
                    break;
                //case  (enuWindowMessage) 0x46: //windows pos changing
                //case (enuWindowMessage)0x47: //windowsposchanged
                //    m.Result = new IntPtr(1);
                //    return;
                //    break;
            }
            base.WndProc(ref m);
        }
    }
}

