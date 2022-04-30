

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: XWinCoreSurfaceContainer.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
using IGK.ICore.WinCore;
using IGK.ICore;
using IGK.ICore.WinUI;
/*
IGKDEV @ 2008-2016
author: C.A.D . BONDJE DOUE
file:XWinCoreSurfaceContainer.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using IGK.ICore.WinCore.WinUI.Controls;
namespace IGK.DrSStudio.WinUI
{
    /// <summary>
    /// represent the default surface container
    /// </summary>
    public class XWinCoreSurfaceContainer : 
        IGKXUserControl, 
        IXCoreSurfaceContainer
    {
        private List<ICoreWorkingSurface> m_surfaces; // list of opened surface
        private ICoreWorkbenchLayoutManager m_layoutManager;// represent the layout manager
        private XWinCoreSurfaceContainerTab c_surfaceTab; //tag page
        private XWinCoreSurfaceContainerContent c_surfaceContent; //surface container
        private ICoreWorkingSurface m_CurrentSurface; //current opened surface
        private ICoreWorkingSurface m_openedSurface; //opened surface


        public ICoreWorkbenchLayoutManager LayoutManager {
            get {
                return m_layoutManager;
            }
        }
        
        public override System.Drawing.Rectangle DisplayRectangle
        {
            get
            {
                System.Drawing.Rectangle rc =  base.DisplayRectangle;
                return rc;
            }
        }
        /// <summary>
        /// .ctr
        /// </summary>
        private XWinCoreSurfaceContainer()
        {

            this.c_surfaceTab = new XWinCoreSurfaceContainerTab(this);
            this.c_surfaceContent = new XWinCoreSurfaceContainerContent(this);
            this.InitializeComponent();

            this.SuspendLayout();
            // 
            // xWinCoreSurfaceContainerTab1
            // 
            this.c_surfaceTab.Dock = DockStyle.Top;
            this.c_surfaceTab.Location = new System.Drawing.Point(0, 0);
            this.c_surfaceTab.Name = "xWinCoreSurfaceContainerTab1";
            this.c_surfaceTab.Size = new System.Drawing.Size(491, 32);
            this.c_surfaceTab.TabIndex = 0;
            // 
            // xWinCoreSurfaceContainerContent1
            // 
            this.c_surfaceContent.Location = new System.Drawing.Point(0, 32);
            this.c_surfaceContent.Name = "xWinCoreSurfaceContainerContent1";
            this.c_surfaceContent.Size = new System.Drawing.Size(491, 237);
            this.c_surfaceContent.TabIndex = 1;
            // 
            // XWinCoreSurfaceContainer
            // 
            this.Controls.Add(this.c_surfaceContent);
            this.Controls.Add(this.c_surfaceTab);
            this.Name = "XWinCoreSurfaceContainer";
            this.Size = new System.Drawing.Size(491, 269);
            this.ResumeLayout(false);
            this.m_surfaces = new List<ICoreWorkingSurface>();
            this.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CurrentSurfaceChanged += _CurrentSurfaceChanged;
        }
    
        /// <summary>
        /// .ctr
        /// </summary>
        /// <param name="layoutManager"></param>
        public XWinCoreSurfaceContainer(ICoreWorkbenchLayoutManager layoutManager):this()
        {
            this.m_layoutManager = layoutManager;
        }
        void _CurrentSurfaceChanged(object sender, CoreWorkingElementChangedEventArgs<ICoreWorkingSurface> e)
        {
            var sm = this.m_layoutManager.Workbench as ICoreSurfaceManagerWorkbench;
            if (e.NewElement != null)
            {

          
                if ((sm!=null) && !sm.Surfaces.Contains(e.NewElement))
                {
                    e.NewElement.Visible = true;
                    if ((e.NewElement.ParentSurface != null) && (this.m_openedSurface != e.NewElement.ParentSurface))
                    {
                        
                        this.c_surfaceContent.SuspendLayout();
                        this.c_surfaceContent.Controls.Clear();
                        Control c = e.NewElement.ParentSurface as Control;
                        c.Dock = DockStyle.Fill;
                        c.Visible = true;
                        this.c_surfaceContent.Controls.Add(c);
                        this.c_surfaceContent.ResumeLayout();
                        this.m_openedSurface = e.NewElement.ParentSurface;
                    }
                    return;
                }
            }
            //update element styles
            Control p = null;
            this.c_surfaceContent.SuspendLayout();
            this.c_surfaceContent.Controls.Clear();
            if ((e.OldElement !=null) && (sm!=null) && sm.Surfaces.Contains(e.OldElement))
            {

                Control c = e.OldElement as Control; 
                c.Visible = false;
            }

            if (e.NewElement is Control)
            {
                Control c = e.NewElement as Control;
                c.Dock = DockStyle.Fill;
                c.Visible = true;
          
                this.c_surfaceContent.Controls.Add(c);
                m_openedSurface = e.NewElement;
                p = c;
                bool  h = c.Focus();
               /// System.Diagnostics.Debug.WriteLine("focused " + h);
            }
            this.c_surfaceContent.ResumeLayout();
            this.c_surfaceContent.PerformLayout();
            if (p != null)
            {
                bool m = p.Focus();
                
            }
        }
        /// <summary>
        /// get the current surface
        /// </summary>
        public ICoreWorkingSurface  CurrentSurface
        {
            get { return m_CurrentSurface; }
            set
            {
                if (m_CurrentSurface != value)
                {
                    CoreWorkingElementChangedEventArgs<ICoreWorkingSurface > s = new CoreWorkingElementChangedEventArgs<ICoreWorkingSurface> (m_CurrentSurface, value );
                    m_CurrentSurface = value;
                    OnCurrentSurfaceChanged(s);
                }
            }
        }
        protected virtual void OnCurrentSurfaceChanged(CoreWorkingElementChangedEventArgs<ICoreWorkingSurface> s)
        {
            if (this.CurrentSurfaceChanged != null)
                this.CurrentSurfaceChanged(this, s);
        }
        public event EventHandler<CoreWorkingElementChangedEventArgs<ICoreWorkingSurface>> CurrentSurfaceChanged;
        
        public void RegisterSurface(ICoreWorkingSurface surface)
        {
            if ((surface != null) && !IsRegister(surface))
            {
                this.m_surfaces.Add(surface);
            }
        }
        public void UnregisterSurface(ICoreWorkingSurface surface)
        {
            if (this.m_surfaces.Contains(surface))
                this.m_surfaces.Remove(surface);
        }
        public string Id
        {
            get { return this.Name; }
        }
        public bool IsRegister(ICoreWorkingSurface surface)
        {
            return this.m_surfaces.Contains(surface);
        }
        private void InitializeComponent()
        {          
            this.SuspendLayout();
            // 
            // XWinCoreSurfaceContainer
            // 
            this.Name = "XWinCoreSurfaceContainer";
            this.Size = new System.Drawing.Size(530, 146);
            this.ResumeLayout(false);

        }
        public void MoveToNextTab()
        {
            if (this.SurfaceCount == 0)
                return ;
            int i = this.m_surfaces.IndexOf(this.CurrentSurface);
            i++;
            int r = i % this.SurfaceCount;
            this.Workbench.CurrentSurface = this.m_surfaces[r];
        }
        public void MoveToPreviousTab()
        {
            if (this.SurfaceCount == 0)
                return;
            int i = this.m_surfaces.IndexOf(this.CurrentSurface);
            i--;
            int r = 0;
            if (i >= 0)
            {
                r = i;
            }
            else {
                r = this. m_surfaces.Count - 1;
            }
            this.Workbench.CurrentSurface = this.m_surfaces[r];
        }
        public int SurfaceCount { get { return this.m_surfaces.Count; } }
        public ICoreWorkingSurface this[int index]
        {
            get {
                if ((index >= 0) && (index < this.m_surfaces.Count  ))
                   return m_surfaces[index];
                return null;
            }
        }
    }
}

