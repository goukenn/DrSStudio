

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: XWinCoreSurfaceContainerTab.cs
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
file:XWinCoreSurfaceContainerTab.cs
*/
using IGK.ICore.WinCore;
using IGK.DrSStudio.Drawing2D;
using IGK.DrSStudio.Drawing2D.WinUI;
using IGK.ICore;
using IGK.ICore.GraphicModels;
using IGK.ICore.Resources;
using IGK.ICore.WinUI;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.Layout;
using IGK.ICore.WinCore.WinUI.Controls;
using IGK.ICore.WinCore.WinUI;
namespace IGK.DrSStudio.WinUI
{
    class XWinCoreSurfaceContainerTab : IGKXUserControl 
    {
        private int m_OngletMinSize;
        private int m_OngletMaxSize;
        private IGKXButton c_options;
        public int OngletMaxSize
        {
            get { return m_OngletMaxSize; }
            set
            {
                if (m_OngletMaxSize != value)
                {
                    m_OngletMaxSize = value;
                }
            }
        }
        public int OngletMinSize
        {
            get { return m_OngletMinSize; }
            set
            {
                if (m_OngletMinSize != value)
                {
                    m_OngletMinSize = value;
                }
            }
        }
        /// <summary>
        /// represent the XWinCoreSurfaceContainerTab onglet
        /// </summary>
        public class SurfaceContainerOnglet : IGKXControl, ICoreMouseStateObject 
        {
            private ICoreWorkingSurface c_Surface; //the current surface
            private XWinCoreSurfaceContainerTab c_tab;
            private IGKXButton c_closeBtn;
            public new ICoreSurfaceManagerWorkbench Workbench {
                get {
                    return base.Workbench as ICoreSurfaceManagerWorkbench;
                }
            }
            /// <summary>
            /// get if this onglet is selected
            /// </summary>
            public bool IsSelected {
                get {
                    var s = this.Workbench.CurrentSurface;
                    var b = this.Workbench;
                    if (b.Surfaces.Contains(this.c_Surface))
                    {                        
                        return (this.c_Surface == b.CurrentSurface) || (s.ParentSurface == this.c_Surface );
                    }
                    else
                    {
                        if ((s!=null) && (s.ParentSurface == this.c_Surface ))
                        {
                            return true;
                        }
                    }
                    return false;
                }
            }
            /// <summary>
            /// get the tab onglet
            /// </summary>
            /// <param name="tab"></param>
            /// <param name="surface"></param>
            public SurfaceContainerOnglet(XWinCoreSurfaceContainerTab 
                tab, ICoreWorkingSurface surface)
            {
                this.c_tab = tab;                
                this.c_Surface = surface;
                if (this.Workbench is ICoreSurfaceManagerWorkbench sm)
                    sm.SurfaceRemoved += Workbench_SurfaceRemoved;
                this.Workbench.CurrentSurfaceChanged += Workbench_CurrentSurfaceChanged;
                this.RegisterSurfaceEvent();
                this.Click += _Click;
                this.Paint += _Paint;
                this.MouseStateChanged += (o, e) => { this.Invalidate(); this.Update(); };
                this.c_closeBtn = new IGKXButton
                {
                    ButtonDocument =
                    CoreButtonDocument.CreateFromRes(CoreImageKeys.BTN_CLOSESURFACE_GKDS),//Create(CoreResources.GetAllDocuments("btn_closesurface"));

                    Size = new System.Drawing.Size(16, 16)
                };
                this.c_closeBtn.Click += (o, e) => { this.CloseSurface(); };                
                this.Controls.Add(c_closeBtn);
                CoreMouseStateManager.Register(this);
            }
            //close the current surface
            private void CloseSurface()
            {
                this.Workbench.Surfaces.Remove(this.c_Surface);
            }
            protected override void OnSizeChanged(EventArgs e)
            {
                base.OnSizeChanged(e);
                this._updateButtonPosition();
            }
            private void _updateButtonPosition()
            {
                this.c_closeBtn .Location = new Point (this.Width - this.c_closeBtn.Width -3, 
                    (this.Height -this.c_closeBtn.Height)/ 2);
            }
            protected override Size SizeFromClientSize(Size clientSize)
            {
                return base.SizeFromClientSize(clientSize);
            }
            private void RegisterSurfaceEvent()
            {
                if (this.c_Surface is ICoreWorkingFilemanagerSurface)
                {
                    ICoreWorkingFilemanagerSurface c_p = this.c_Surface as ICoreWorkingFilemanagerSurface;
                    c_p.FileNameChanged += _c_p_FileNameChanged;
                    c_p.NeedToSaveChanged += _c_p_NeedToSaveChanged;
                    
                }
                this.c_Surface.TitleChanged += _c_Surface_TitleChanged;
            }

            void _c_Surface_TitleChanged(object sender, EventArgs e)
            {
                this.c_tab.InitLayout();
            }

            void _c_p_NeedToSaveChanged(object sender, EventArgs e)
            {
                this.c_tab.InitLayout();
                this.Refresh();
            }
            private void UnregisterSurfaceEvent()
            {
                if (this.c_Surface != null)
                {
                    if (this.c_Surface is ICoreWorkingFilemanagerSurface)
                    {
                        ICoreWorkingFilemanagerSurface c_p = this.c_Surface as ICoreWorkingFilemanagerSurface;
                        c_p.FileNameChanged -= _c_p_FileNameChanged;
                        c_p.NeedToSaveChanged -= _c_p_NeedToSaveChanged;
                    }
                    this.c_Surface.TitleChanged -= _c_Surface_TitleChanged;
                }
            }
            void _c_p_FileNameChanged(object sender, EventArgs e)
            {
                this.c_tab.PerformLayout();
            }
            void _Click(object sender, EventArgs e)
            {
                if (!this.IsSelected)
                {
                    this.Workbench.CurrentSurface = this.c_Surface;
                }
            }
            void Workbench_CurrentSurfaceChanged(object sender, CoreWorkingElementChangedEventArgs<ICoreWorkingSurface> e)
            {
                this.Invalidate();
                this.Update();
                
            }
            void _Paint(object sender, CorePaintEventArgs e)
            {
                Colorf v_ftcolor = WinCoreControlRenderer.SurfaceContainerTabOngletForeColor;
                Colorf v_bgColor = WinCoreControlRenderer.SurfaceContainerTabBackgroundColor;
                if (this.IsSelected)
                {
                    v_bgColor = WinCoreControlRenderer.SurfaceContainerTabBorderColor;
                    v_ftcolor = WinCoreControlRenderer.SurfaceContainerTabOngletSelectedForeColor;
                }
                else {
                    if (this.MouseState == enuMouseState.Hover)
                    {
                        v_bgColor = WinCoreControlRenderer.SurfaceContainerTabHoverBackgroundColor;
                        v_ftcolor = WinCoreControlRenderer.SurfaceContainerTabHoverForeColor;
                    }
                    else
                        v_bgColor = WinCoreControlRenderer.SurfaceContainerTabOngletBackgroundColor;
                }
                e.Graphics.FillRectangle(
                   CoreBrushRegisterManager.GetBrush<Brush>(
                    v_bgColor 
                   ),
                   this.ClientRectangle);
                Rectanglef rc = new Rectanglef (0,0, this.ClientRectangle.Width , this.ClientRectangle.Height) ;
                rc.Width -= 51;
                //rc.Inflate (-1,-1);
                StringFormat v_sfg = new StringFormat()
                {
                    Trimming = StringTrimming.EllipsisPath ,
                    FormatFlags = StringFormatFlags.NoWrap,
                    Alignment =  StringAlignment.Center,
                    LineAlignment = StringAlignment.Center 
                };
                string v_text = this.GetSurfaceTitle();
                e.Graphics.DrawString(
                    v_text 
                    , this.Font,
                    CoreBrushRegisterManager.GetBrush<Brush>(v_ftcolor),
                    rc,
                    v_sfg 
                    );
                e.Graphics.FillRectangle(
                   CoreBrushRegisterManager.GetBrush<Brush>(
                    WinCoreControlRenderer.SurfaceContainerTabBorderColor
                   ),
                   0, this.Height - 2, this.Width, 2);
            }
            private string GetSurfaceTitle()
            {
                string t = this.c_Surface.Title;
                if (this.c_Surface is ICoreWorkingFilemanagerSurface)
                {
                    if ((this.c_Surface as ICoreWorkingFilemanagerSurface).NeedToSave)
                        t += "*";
                }
                return t;
            }
            void Workbench_SurfaceRemoved(object sender, CoreItemEventArgs<ICoreWorkingSurface> e)
            {
                if (e.Item == c_Surface)
                {
                    this.Parent.Controls.Remove(this);
                    this.UnregisterSurfaceEvent();
                    this.c_Surface = null;
                    this.Dispose();
                }
            }
            /// <summary>
            /// return the prefered width of this onglet
            /// </summary>
            /// <returns></returns>
            internal int GetPreferredWidth()
            {
                Graphics g = CreateGraphics();
                int w = 0;
                w +=( (16+1)*3); //button size
                string V_title = this.GetSurfaceTitle();
                SizeF v = g.MeasureString(V_title, this.Font, new SizeF(short.MaxValue, short.MaxValue));
                w +=(int) (Math.Ceiling(v.Width));
                g.Dispose();
                return w;
            }
            protected override void Dispose(bool disposing)
            {
                if (disposing)
                {
                    UnregisterSurfaceEvent();
                }
                base.Dispose(disposing);
            }
        }
        sealed class SurfaceContainerTabContainterLayoutEngine : LayoutEngine
        {
            private XWinCoreSurfaceContainerTab c_xWinCoreSurfaceContainerTab;
            public SurfaceContainerTabContainterLayoutEngine(XWinCoreSurfaceContainerTab xWinCoreSurfaceContainerTab)
            {                
                this.c_xWinCoreSurfaceContainerTab = xWinCoreSurfaceContainerTab;
                this.c_xWinCoreSurfaceContainerTab.SizeChanged += (o,e) => { this.c_xWinCoreSurfaceContainerTab.PerformLayout();  };
            }
            public override void InitLayout(object child, BoundsSpecified specified)
            {
                base.InitLayout(child, specified);
            }
            public override bool Layout(object container, LayoutEventArgs layoutEventArgs)
            {
                int x = 4;
                int y = 1;
                int w = 0;
                //return base.Layout(container, layoutEventArgs);
                foreach (Control item in c_xWinCoreSurfaceContainerTab.Controls)
                {
                    if (item is SurfaceContainerOnglet)
                    {
                        SurfaceContainerOnglet obj = item as SurfaceContainerOnglet;
                        w  = Math.Max(this.c_xWinCoreSurfaceContainerTab.OngletMinSize, Math.Min (this.c_xWinCoreSurfaceContainerTab.OngletMaxSize, obj.GetPreferredWidth()));
                        item.Bounds = new Rectangle(
                            x,y,
                            w,
                            c_xWinCoreSurfaceContainerTab.Height -1
                            );
                        x += w+1;
                    }
                }
                return true;
            }
        }
        public override System.Windows.Forms.Layout.LayoutEngine LayoutEngine
        {
	        get 
	        {
                if (m_layoutEngine == null)
                {
                    m_layoutEngine = new SurfaceContainerTabContainterLayoutEngine(this);               
                }
                return m_layoutEngine;
	        }
        }
      
        protected override void InitLayout()
        {
            base.InitLayout();
            this.LayoutEngine.Layout(this, null);
        }
           //get the container
            private XWinCoreSurfaceContainer m_container;
            private SurfaceContainerTabContainterLayoutEngine m_layoutEngine;
            const int TABHEIGHT = 26;
            const int ONGLETMINSIZE = 150;
            const int ONGLETMAXSIZE = 200;
            private IGKXButton m_closeButton;//close the current surface
            private IGKXButton m_viewSurface;//view all surface  
            private int m_OverIndex;
            //private Rectangle[] m_onglet;

        //design mode
            public  XWinCoreSurfaceContainerTab()
            {
                this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
                this.SetStyle(ControlStyles.ResizeRedraw, true);
                this.SetStyle(ControlStyles.UserPaint, true);
                this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
                this.m_closeButton = new IGKXButton();
                this.m_viewSurface = new IGKXButton();
                this.m_closeButton.ButtonDocument = 
                    CoreButtonDocument.CreateFromRes (CoreImageKeys.BTN_CLOSE_GKDS);//.Create(CoreResources.GetAllDocuments("btn_closesurface"));
                this.m_closeButton.Visible = false;
                this.m_closeButton.Size = new Size(16, 16);
                this.m_viewSurface.ButtonDocument =
                    CoreButtonDocument.CreateFromRes(CoreImageKeys.BTN_SHOWCONTEXT_GKDS);
               // CoreButtonDocument.Create(CoreResources.GetAllDocuments("Btn_showContext"));
                this.m_viewSurface.Visible = false;
                this.m_viewSurface.Size = new Size(16, 16);                
                this.OverIndex = -1;
                this.Controls.Add(this.m_closeButton);
                this.Controls.Add(this.m_viewSurface);                
                this.Dock = DockStyle.Top;
                this.MinimumSize = new Size(0, TABHEIGHT);
                this.MaximumSize = new Size(0, TABHEIGHT);
                this.Click += new EventHandler(_Click);
                this.MouseHover += new EventHandler(_MouseHover);
                this.MouseMove += _MouseMove;
                this.MouseDown += _MouseDown;
                this.MouseLeave += new EventHandler(LayoutTabPanel_MouseLeave);
                this.SizeChanged += new EventHandler(LayoutTabPanel_SizeChanged);
                this.m_closeButton.Click += new EventHandler(_m_closeButton_Click);
                this.m_viewSurface.Click += new EventHandler(_m_viewSurface_Click);
            this.c_options = new IGKXButton
            {
                ButtonDocument =
                CoreButtonDocument.CreateFromRes(CoreImageKeys.IMG_SURFACEOPTIONS_GKDS),//(CoreResources.GetAllDocuments("img_surfaceoptions"));
                Size = WinCoreConstant.DUMMY_MENU_PICTURE_SIZE_16x16
            };
            this.Controls.Add(this.c_options);
                this.UpdateOptionLocation();
                this.Paint += _Paint;
            }
            private int SelectedIndex {
                get
                {
                    ICoreWorkbench b = this.SurfaceContainer.LayoutManager.Workbench;
                    if (b.CurrentSurface != null)
                    {
                        ICoreWorkingSurface v_surface = b.CurrentSurface;
                        if (b is ICoreSurfaceManagerWorkbench)
                        return (b as ICoreSurfaceManagerWorkbench).Surfaces.IndexOf(v_surface);
                    }
                    return -1;
                }
            }
            protected override void OnSizeChanged(EventArgs e)
            {
                base.OnSizeChanged(e);
                UpdateOptionLocation();
            }
            private void UpdateOptionLocation()
            {
                if (this.c_options == null) return;
                this.c_options.Location = new Point(
                    this.Width - this.c_options.Width  - 4,
                    (this.Height - this.c_options.Height )/2
                    );
            }
            private void CloseSelectedSurface()
            {
                if (this.SurfaceContainer.LayoutManager.Workbench.CurrentSurface != null)
                {
                    ICoreWorkingSurface v_surface =
                        this.SurfaceContainer.LayoutManager.Workbench.CurrentSurface;
                    var b = this.SurfaceContainer.LayoutManager.Workbench as ICoreSurfaceManagerWorkbench ;
                //close current surface
                if (v_surface is ICoreWorkingFilemanagerSurface v_f)
                {
                    if (v_f.NeedToSave)
                    {
                        enuDialogResult v_dial = CoreMessageBox.Show(CoreSystem.GetString("DLG.NeedToSave",
                        v_f.FileName), CoreSystem.GetString("DLG.NeedToSaveTitle"),
                        enuCoreMessageBoxButtons.YesNoCancel);
                        switch (v_dial)
                        {
                            case enuDialogResult.Yes:
                                v_f.Save();
                                if (v_f.NeedToSave)
                                    return;
                                break;
                        }
                    }
                    b.Surfaces.Remove(v_f);
                }
                else
                {
                    b.Surfaces.Remove(
                        v_surface);
                }
            }
            }
            private int OverIndex
            {
                get
                {
                    return this.m_OverIndex;
                }
                set
                {
                    if (this.m_OverIndex != value)
                    {
                        this.m_OverIndex = value;
                        this.Invalidate();
                    }
                }
            }
        /// <summary>
        /// force dockstyle to be top
        /// </summary>
            public override DockStyle Dock
            {
                get
                {
                    return base.Dock;
                }
                set
                {
                    if (value == DockStyle.Top)
                        base.Dock = value;
                }
            }
            /// <summary>
            /// get the surface container
            /// </summary>
            public XWinCoreSurfaceContainer SurfaceContainer { get { return this.m_container; } }
          
            internal XWinCoreSurfaceContainerTab(XWinCoreSurfaceContainer container)
                : this()
            {
                this.m_container = container;
                this.m_OngletMaxSize = 300;
                this.m_OngletMinSize = 100;
            if (this.m_container.Workbench is ICoreSurfaceManagerWorkbench sm)
            {
                sm.SurfaceAdded += _SurfaceAdded;
            }
        }        
            void _SurfaceAdded(object sender, CoreItemEventArgs<ICoreWorkingSurface> e)
            {
                this.SuspendLayout();
                this.Controls.Add(new SurfaceContainerOnglet(this, e.Item));
                this.ResumeLayout();
                this.PerformLayout();
            }
            protected override void OnPaint(PaintEventArgs e)
            {
                base.OnPaint(e);
            }
            void _Paint(object sender, CorePaintEventArgs e)
            {
                RenderTab(e);          
            }
            void _Container_LayoutManagerChanged(object sender, EventArgs e)
            {
                if (this.m_container.LayoutManager != null)
                {
                if (this.m_container.LayoutManager.Workbench is ICoreSurfaceManagerWorkbench sm)
                {
                    sm.SurfaceAdded += WorkBench_SurfaceAdded;
                    sm.SurfaceRemoved += WorkBench_SurfaceRemoved;
                }
                this.m_container.LayoutManager.Workbench.CurrentSurfaceChanged += WorkBench_CurrentSurfaceChanged;             
                }
            }
            protected override void OnPaintBackground(PaintEventArgs pevent)
            {
                //no paint background
                base.OnPaintBackground(pevent);
            }
            void _m_viewSurface_Click(object sender, EventArgs e)
            {
                //view context surface
                ContextMenuStrip menu = new ContextMenuStrip();
                menu.Closed += new ToolStripDropDownClosedEventHandler(_menu_Closed);
                menu.RenderMode = ToolStripRenderMode.Professional;
            //

            if (SurfaceContainer.LayoutManager.Workbench is ICoreSurfaceManagerWorkbench b)
            {
                foreach (ICoreWorkingSurface s in b.Surfaces)
                {
                    ToolStripMenuItem item = new ToolStripMenuItem(s.Title)
                    {
                        Tag = s
                    };
                    item.Click += new EventHandler(_item_Click);
                    menu.Items.Add(item);
                    if (s == b.CurrentSurface)
                        item.Checked = true;
                }
            }
            menu.Show(this, new Point(this.m_viewSurface.Location.X, this.m_viewSurface.Bounds.Bottom));
            }
            void _item_Click(object sender, EventArgs e)
            {
                ToolStripMenuItem m = sender as ToolStripMenuItem;
                this.SurfaceContainer.LayoutManager.Workbench.CurrentSurface = m.Tag as ICoreWorkingSurface;
            }
            void _menu_Closed(object sender, ToolStripDropDownClosedEventArgs e)
            {
                //free all items
                ContextMenuStrip menu = sender as ContextMenuStrip;
                menu.Items.Clear();
            }
            void _m_closeButton_Click(object sender, EventArgs e)
            {
                this.CloseSelectedSurface();             
            }
            void LayoutTabPanel_SizeChanged(object sender, EventArgs e)
            {
                this.InitControlBound();
            }
            private void InitControlBound()
            {
                this.m_closeButton.Bounds = new Rectangle(this.Width - this.m_closeButton.Width - 4,
                    (this.Height - this.m_closeButton.Height) / 2, this.m_closeButton.Width,
                    this.m_closeButton.Height);
                this.m_viewSurface.Bounds = new Rectangle(
                    this.m_closeButton.Bounds.X - this.m_viewSurface.Width - 6,
                    this.m_closeButton.Bounds.Y,
                    this.m_viewSurface.Width,
                    this.m_viewSurface.Height);
            }
            void LayoutTabPanel_MouseLeave(object sender, EventArgs e)
            {
                this.OverIndex = -1;
            }
            void WorkBench_CurrentSurfaceChanged(object o, CoreWorkingElementChangedEventArgs<ICoreWorkingSurface >  e)
            {
                //invalidate bar when surface changed
                //unregister surface event
                if (e.OldElement   is ICoreWorkingFilemanagerSurface)
                    (e.OldElement   as ICoreWorkingFilemanagerSurface).NeedToSaveChanged -= new EventHandler(LayoutTabPanel_NeedToSaveChanged);
                //register surface event
                if (e.NewElement  is ICoreWorkingFilemanagerSurface)
                (e.NewElement  as ICoreWorkingFilemanagerSurface).NeedToSaveChanged += new EventHandler(LayoutTabPanel_NeedToSaveChanged);
                this.Invalidate();
            }
            void LayoutTabPanel_NeedToSaveChanged(object sender, EventArgs e)
            {
                this.Invalidate();
            }
            void WorkBench_SurfaceRemoved(object sender, CoreItemEventArgs<ICoreWorkingSurface> e)
            {
                this.m_closeButton.Visible = (this.SurfaceContainer.SurfaceCount > 0);
                this.m_viewSurface.Visible = this.m_closeButton.Visible;
                this.PerformLayout();
                this.Invalidate();
            }
            void WorkBench_SurfaceAdded(object sender, CoreItemEventArgs<ICoreWorkingSurface> e)
            {
                this.m_closeButton.Visible = true;
                this.m_viewSurface.Visible = true;
                this.PerformLayout();
                this.Invalidate();
            }
            //private void GenerateOngletBound()
            //{
            //    this.m_onglet = new Rectangle[this.SurfaceContainer.SurfaceCount];
            //}
            void _MouseDown(object sender, CoreMouseEventArgs e)
            {
                CheckOverIndex(e);
            }
            void _MouseMove(object sender, CoreMouseEventArgs e)
            {
                CheckOverIndex(e);
            }
            void _MouseHover(object sender, EventArgs e)
            {
            }
            void _Click(object sender, EventArgs e)
            {
            if ((this.SurfaceContainer.LayoutManager.Workbench is ICoreSurfaceManagerWorkbench b) && (this.OverIndex >= 0))
            {
                this.SurfaceContainer.LayoutManager.Workbench.CurrentSurface
                    = b.Surfaces[this.OverIndex];
            }
        }
            void CheckOverIndex(CoreMouseEventArgs e)
            {
                //if ((this.m_onglet == null) || (this.m_onglet.Length == 0))
                //{
                //    this.OverIndex = -1;
                //    return;
                //}
                //if (this.m_onglet.Length < this.m_endindex)
                //    this.m_endindex = this.m_onglet.Length;
                //for (int i = m_startindex; i < this.m_endindex; i++)
                //{
                //    if (this.m_onglet[i].Contains(e.Location))
                //    {
                //        this.OverIndex = i;
                //        return;
                //    }
                //}
                this.OverIndex = -1;
            }
            /// <summary>
            /// get surface name info
            /// </summary>
            /// <param name="surface"></param>
            /// <returns></returns>
            string GetInfo(ICoreWorkingSurface surface)
            {
                string v_txt = string.Empty;
            if (surface is ICoreWorkingFilemanagerSurface v_f)
            {
                v_txt = System.IO.Path.GetFileName(v_f.Title);
                if (v_f.NeedToSave)
                    v_txt += CoreConstant.SURFACE_CHANGED_CHAR;
            }
            else
            {
                v_txt = surface.Title;
            }
            return v_txt;
            }
            /// <summary>
            /// used to render tab onglet
            /// </summary>
            /// <param name="e"></param>
            private void RenderTab(CorePaintEventArgs e)
            {
                e.Graphics.FillRectangle(
                CoreBrushRegisterManager.GetBrush<Brush>(
                 WinCoreControlRenderer.SurfaceContainerTabBackgroundColor
                ),
                0,0, this.Width, this.Height );
                e.Graphics.FillRectangle(
                    CoreBrushRegisterManager.GetBrush<Brush>(
                     WinCoreControlRenderer.SurfaceContainerTabBorderColor
                    ),
                    0, this.Height - 2, this.Width, 2);
            }
    }
}

