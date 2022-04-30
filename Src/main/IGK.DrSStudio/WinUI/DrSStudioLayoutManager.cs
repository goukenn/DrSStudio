

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: DrSStudioLayoutManager.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
using IGK.ICore.WinCore;
using IGK.DrSStudio.Actions;
using IGK.DrSStudio.Tools;
using IGK.DrSStudio.WinLauncher.Tools;
using IGK.DrSStudio.WinUI.MainFormDisposition;
using IGK.ICore;
/*
IGKDEV @ 2008-2016
author: C.A.D . BONDJE DOUE
file:DrSStudioLayoutManager.cs
*/
using IGK.ICore.Actions;
using IGK.ICore.Tools;
using IGK.ICore.WinUI;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;  
using IGK.ICore.Settings;
using IGK.ICore.Design;

namespace IGK.DrSStudio.WinUI
{
    [Serializable()]
    public class DrSStudioLayoutManager : WinCoreLayoutManagerBase
    {
        private GlobalApplicationMessageFilter m_GlobalAction;
        private ICoreWorkingSurface m_StartSurface;
        private ToolManager m_ToolManager;

        public ToolManager  ToolManager
        {
            get { return m_ToolManager; }
        }
        public override ICoreMenu CreateMenu()
        {
            var g = base.CreateMenu();
            var r = (g as global::System.Windows.Forms.Control);
            if (r != null)
            {
                if (r is IGKXWinCoreMenu m)
                {
                    m.CoreFont = IGK.DrSStudio.Settings.DrSStudioMenuFontSetting.MenuBaseFont;
                }
                else
                {
                    r.FontChanged += R_FontChanged;
                    //  r.Font = IGK.DrSStudio.Settings.DrSStudioMenuFontSetting.Instance.MenuFont.ToGdiFont();//DrSStudioM
                    r.Font = IGK.DrSStudio.Settings.DrSStudioMenuFontSetting.MenuBaseFont.ToGdiFont();
                    CoreRenderer.RenderingValueChanged += (o, e) =>
                    {
                        CorePropertySetting _g = o as CorePropertySetting;
                        if (_g.Name == nameof(IGK.DrSStudio.Settings.DrSStudioMenuFontSetting.MenuBaseFont))
                            r.Font = IGK.DrSStudio.Settings.DrSStudioMenuFontSetting.MenuBaseFont.ToGdiFont();
                    };
                }
            }
            return g;
        }
        protected override ICoreMenuHostControl CreateMenuContainer()
        {
            var v =  base.CreateMenuContainer();
         
           

            return v;
        }

        private void R_FontChanged(object sender, EventArgs e)
        {
           Console.WriteLine ("Main Font Changed");

        }

        protected override IXCoreCaptionBarControl CreateCaptionBar()
        {
            XMainFormCaptionBar c = new XMainFormCaptionBar();
            this.Workbench.MainForm.TitleChanged += (o,e) =>
            {
                c.Invalidate();
            };
            return c;
        }
        public GlobalApplicationMessageFilter GlobalAction
        {
            get
            {
                return m_GlobalAction;
            }
        }
        public event EventHandler LoadingToolComplete;
        
        /// <summary>
        /// override generate  tool specification
        /// </summary>
        protected override void GenerateTool()
        {
            base.GenerateTool();
            LoadingToolComplete?.Invoke(this, EventArgs.Empty);
        }
        public DrSStudioLayoutManager(ICoreApplicationWorkbench bench):base(bench )
        {
        }
        public override void InitLayout()
        {
            m_ToolManager = new ToolManager(this);
            CoreControlFactory.RegisterControl(CoreConstant.CTRL_MENU_SEPARATOR, typeof(XToolStripMenuItemSeparator));            
            base.InitLayout();


            /// initilize startup surface
            DrSStartSurface c = new DrSStartSurface();
            c.WebBrowser.AccelerateKeyEvent += WebBrowser_AccelerateKeyEvent;
            if (this.Workbench is ICoreSurfaceManagerWorkbench fb)
            {
                fb.SurfaceClosed += Workbench_SurfaceClosed;
            }
            this.m_StartSurface = c;



            this.Workbench.AddSurface (this.m_StartSurface, true );
            m_GlobalAction = new GlobalApplicationMessageFilter(this);
            this.Workbench.ActionRegister.AddFilterMessage(this.GlobalAction);
            this.RegisterGlobalMenu();

        }

        private void WebBrowser_AccelerateKeyEvent(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            this.Workbench.DispatchMessage(WinCoreMessageFilter.WinCoreMessageHost.CreateMessage(
                this.m_StartSurface.Handle,
                e));
        }

        private void RegisterGlobalMenu()
        {
            this.GlobalAction.Add(enuKeys.LWin, new MainForm_WindowKey());
            this.GlobalAction.Add(enuKeys.Left, new MainForm_DockToLeft());
            this.GlobalAction.Add(enuKeys.Right, new MainForm_DockToRight());
            this.GlobalAction.Add(enuKeys.Up, new MainForm_DockToUp());
            this.GlobalAction.Add(enuKeys.Down, new MainForm_DockToDown());
            this.GlobalAction.Add(enuKeys.Shift | enuKeys.Up , new MainForm_Maximize ());
            this.GlobalAction.Add(enuKeys.Shift | enuKeys.Down, new MainForm_Reduce ());
        }
        protected override void GenerateMainContain()
        {
            IXCoreSurfaceContainer c = this.CreateSurfaceContainer();
            if (c != null)
            {
                this.SurfaceContainer  = c;
                this.Workbench.MainForm.Controls.Add(c);
                new WinCoreSurfaceContainerManager(this, c);
            }

            this.m_ToolManager.InitLayout(this.Workbench.MainForm.Controls );
        }
        protected override void GenerateToolStripPanel()
        {
            base.GenerateToolStripPanel();
        }
        protected override System.Windows.Forms.ToolStripPanel CreateToolStripPanel()
        {
            return base.CreateToolStripPanel();
        }
        

        void Workbench_SurfaceClosed(object sender, CoreSurfaceClosedEventArgs e)
        {
            if ((e.Surface == this.StartSurface) && (e.Reason == enuSurfaceCloseReason.SurfaceRemoved))
            {
                e.CancelDispose = true;
            }
        }
        internal void ShowStartPage()
        {
            (this.m_StartSurface as DrSStartSurface).Reload();
            this.Workbench.AddSurface (this.m_StartSurface,true );
        }
        public override void ShowTool(ICoreTool tool)
        {
            this.ToolManager.ShowTool(this, tool);
        }
        public override void HideTool(ICoreTool tool)
        {
            this.ToolManager.HideTool(this, tool);
        }
        public ICoreWorkingSurface StartSurface { get { return this.m_StartSurface; } }

        internal IDockingForm CreateDockingToolForm()
        {
            return new XDockingForm(this.m_ToolManager);
        }

        internal void RaiseToolRemoved(CoreItemEventArgs<CoreToolBase> e)
        {
            this.OnToolRemoved(e);
        }

        internal void RaiseToolAdded(CoreItemEventArgs<CoreToolBase> e)
        {
            this.OnToolAdded(e);
        }

        internal void ResumeLayout()
        {
            this.MainForm.ResumeLayout();
        }

        internal void SuspendLayout()
        {
            this.MainForm.SuspendLayout();
        }
    }
}

