

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: WorkingToolMenu.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
using IGK.ICore.WinCore;
using IGK.DrSStudio.Menu;
using IGK.ICore;
/*
IGKDEV @ 2008-2016
author: C.A.D . BONDJE DOUE
file:ViewToolMenu.cs
*/
using IGK.ICore.Menu;
using IGK.ICore.Resources;
using IGK.ICore.WinUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace IGK.DrSStudio.Tools.Menu
{
#if DEBUG
    [DrSStudioMenu("Tools.WorkingToolMenu", 0x40)]
    class WorkingToolMenu : CoreApplicationMenu
    {
        public new ICoreWorkingToolManagerSurface CurrentSurface {
            get {
                return base.CurrentSurface as ICoreWorkingToolManagerSurface;
            }
        }
        protected override bool PerformAction()
        {
            return base.PerformAction();
        }
        protected override bool IsVisible()
        {
            return base.IsVisible() && (this.CurrentSurface !=null);
        }
        protected override void OnWorkbenchChanged(EventArgs eventArgs)
        {
            base.OnWorkbenchChanged(eventArgs);
            this.SetupEnableAndVisibility();
        }
        protected override void RegisterBenchEvent(ICoreSystemWorkbench Workbench)
        {
            base.RegisterBenchEvent(Workbench);
            if (Workbench is ICoreWorkingSurfaceHandler s)
                s.CurrentSurfaceChanged += Workbench_CurrentSurfaceChanged;
        }
        protected override void UnregisterBenchEvent(ICoreSystemWorkbench Workbench)
        {
            if (Workbench is ICoreWorkingSurfaceHandler s)
                s.CurrentSurfaceChanged -= Workbench_CurrentSurfaceChanged;
            base.UnregisterBenchEvent(Workbench);
        }
        void Workbench_CurrentSurfaceChanged(object sender, CoreWorkingElementChangedEventArgs<ICoreWorkingSurface> e)
        {
            this.SetupEnableAndVisibility();
        }
        protected override void InitMenu()
        {
            base.InitMenu();
            int i = 0;
            IWorkingItemGroup[] v_groups = IGKGTShortcutManager.Instance.GetGroups();
            string v_key = null;
            foreach (IWorkingItemGroup group in v_groups)
            {
                v_key = this.Id + "."+ group.Name;

                    ToolGroupMenu s = new ToolGroupMenu(group);
                    CoreMenuAttribute v_attr = new CoreMenuAttribute(this.Id + "." + group.Name, i) { 
                        CaptionKey = "[group."+group.Name +"]"
                    };                    
                    s.SetAttribute(v_attr);
                    if (this.Register(v_attr, s))
                    {
                        this.Childs.Add(s);
                        i++;
                    }
            }
        }
        class ToolGroupMenu : CoreApplicationMenu
        {
            private IWorkingItemGroup group;
            protected override bool IsVisible()
            {
                return group.Visible;
            }
            public ToolGroupMenu(IWorkingItemGroup group)
            {
                this.group = group;
                this.group.VisibleChanged += group_VisibleChanged;
            }
            void group_VisibleChanged(object sender, EventArgs e)
            {
                this.SetupEnableAndVisibility();
            }
            protected override void InitMenu()
            {
                base.InitMenu();
                int i = 0;
                CoreMenuAttribute v_attr = null;
                foreach (ICoreGTWorkingItem item in group.Items)
                {
                    ToolItemMenu s = new ToolItemMenu(item);
                    v_attr = new CoreMenuAttribute(string.Format(this.Id + "." + item.CaptionKey), i) { 
                        CaptionKey  = "["+item.CaptionKey+"]"
                    };
                    if (item.Keys != enuKeys.None)
                    {
                        v_attr.ShortcutText = "Ctrl+T," + CoreResources.GetShortcutText(item.Keys);
                    }
                    v_attr.ImageKey = item.ImageKey;
                    s.SetAttribute(v_attr);
                    if (this.Register(v_attr, s))
                    {
                        //s.SetAttribute(v_attr);
                        this.Childs.Add(s);
                        i++;
                    }
                }
            }

         
        }
        class ToolItemMenu : CoreApplicationSurfaceMenuBase
        {
            private ICoreGTWorkingItem item;
            public new ICoreWorkingToolManagerSurface CurrentSurface
            {
                get
                {
                    return base.CurrentSurface as ICoreWorkingToolManagerSurface;
                }
            }
            public ToolItemMenu(ICoreGTWorkingItem item)
            {
                this.item = item;
            }
            protected override void OnCurrentSurfaceChanged(CoreWorkingElementChangedEventArgs<ICoreWorkingSurface> e)
            {
                base.OnCurrentSurfaceChanged(e);
                if (e.OldElement  is ICoreWorkingToolManagerSurface)
                {
                    ICoreWorkingToolManagerSurface c = e.OldElement  as ICoreWorkingToolManagerSurface;
                    c.CurrentToolChanged -= c_CurrentToolChanged;
                }
                if (e.NewElement is ICoreWorkingToolManagerSurface)
                {
                    ICoreWorkingToolManagerSurface c = e.NewElement as ICoreWorkingToolManagerSurface;
                    c.CurrentToolChanged += c_CurrentToolChanged;
                }
                this.SetupIsChecked();
            }
            void c_CurrentToolChanged(object sender, EventArgs e)
            {
                this.SetupIsChecked();
            }
            private void SetupIsChecked()
            {
                this.MenuItem.Checked = (this.CurrentSurface != null) && (this.CurrentSurface.CurrentTool == this.item.ToolType);
            }
            protected override bool PerformAction()
            {
                this.CurrentSurface.CurrentTool = this.item.ToolType;
                return base.PerformAction();
            }
            protected override bool IsEnabled()
            {
                return true;
            }
            protected override bool IsVisible()
            {
                return true;
            }
           
        }
    }
#endif
}

