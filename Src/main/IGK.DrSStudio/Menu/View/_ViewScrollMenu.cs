

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: _ViewScroll.cs
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
file:_ViewScroll.cs
*/
using System;
using System.Collections.Generic;
using System.Text;
namespace IGK.DrSStudio.Menu.View
{
    using IGK.ICore.WinCore;
using IGK.ICore;
    using IGK.ICore.Menu;
    using IGK.ICore.WinUI;
    /// <summary>
    /// view scroll
    /// </summary>
    [DrSStudioMenu("View.ShowScroll", 0x0011
        , ImageKey = CoreImageKeys.MENU_VIEW_SCROLL_GKDS
        )]
    public class _ViewScrollMenu : CoreApplicationMenu
    {
        public new ICoreWorkingScrollableSurface CurrentSurface
        {
            get { return base.CurrentSurface as ICoreWorkingScrollableSurface; }
        }
        protected override bool PerformAction()
        {
            ICoreWorkingScrollableSurface c = (this.CurrentSurface as ICoreWorkingScrollableSurface);
            c.ShowScroll = !c.ShowScroll;
            return base.PerformAction();
        }
        protected override bool IsEnabled()
        {
            return (this.CurrentSurface != null);
        }
        protected override bool IsVisible()
        {
            return (this.CurrentSurface != null);
        }
        protected override void RegisterBenchEvent(ICoreSystemWorkbench workbench)
        {
            base.RegisterBenchEvent(workbench);
            if (Workbench is ICoreWorkingSurfaceHandler s)
                s.CurrentSurfaceChanged += workbench_CurrentSurfaceChanged;
        }
        private void UpdateCheck()
        {
            if (this.CurrentSurface == null)
                this.MenuItem.Checked = false;
            else 
                this.MenuItem.Checked = this.CurrentSurface.ShowScroll;
        }
        protected override void UnregisterBenchEvent(ICoreSystemWorkbench workbench)
        {
            if (Workbench is ICoreWorkingSurfaceHandler s)
                s.CurrentSurfaceChanged -= workbench_CurrentSurfaceChanged;
            base.UnregisterBenchEvent(workbench);
        }
        void workbench_CurrentSurfaceChanged(object o, CoreWorkingElementChangedEventArgs<ICoreWorkingSurface> e)
        {
            if (e.OldElement is ICoreWorkingScrollableSurface)
                UnRegisterSurfaceEvent(e.OldElement  as ICoreWorkingScrollableSurface);
            if (e.NewElement is ICoreWorkingScrollableSurface)
            {
                RegisterSurfaceEvent(e.NewElement as ICoreWorkingScrollableSurface);
            }
            UpdateCheck();
            this.SetupEnableAndVisibility();
        }
        private void RegisterSurfaceEvent(ICoreWorkingScrollableSurface surface)
        {
            surface.ShowScrollChanged += new EventHandler(_ShowScrollChanged);            
        }
        void _ShowScrollChanged(object sender, EventArgs e)
        {
            this.UpdateCheck();
        }
        private void UnRegisterSurfaceEvent(ICoreWorkingScrollableSurface surface)
        {
            surface.ShowScrollChanged -= new EventHandler(_ShowScrollChanged);
        }
    }
}

