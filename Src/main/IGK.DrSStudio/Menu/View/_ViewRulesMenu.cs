

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: _ViewRules.cs
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
file:_ViewRules.cs
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
    [DrSStudioMenu("View.ShowRules", 0x010,
       ImageKey = CoreImageKeys.MENU_RULE_GKDS)]
    sealed class _ViewRulesMenu : CoreApplicationSurfaceMenuBase
    {
        public new ICoreWorkingRulesSurface CurrentSurface
        {
            get { return base.CurrentSurface as ICoreWorkingRulesSurface; }
        }
        protected override bool PerformAction()
        {
            ICoreWorkingRulesSurface c = (this.CurrentSurface );
            c.ShowRules = !c.ShowRules;
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
        protected override void OnCurrentSurfaceChanged(CoreWorkingElementChangedEventArgs<ICoreWorkingSurface> e)
        {
            if (e.OldElement is ICoreWorkingRulesSurface)
                UnRegisterSurfaceEvent(e.OldElement as ICoreWorkingRulesSurface);
            if (e.NewElement is ICoreWorkingRulesSurface)
            {
                RegisterSurfaceEvent(e.NewElement as ICoreWorkingRulesSurface);
            }
            UpdateCheck();
            this.SetupEnableAndVisibility();
        }
        private void UpdateCheck()
        {
            if (this.CurrentSurface == null)
                this.MenuItem.Checked = false;
            else
                this.MenuItem.Checked = this.CurrentSurface.ShowRules ;
        }
        private void RegisterSurfaceEvent(ICoreWorkingRulesSurface surface)
        {
            surface.ShowRuleChanged  += new EventHandler(_ShowRuleChanged);
        }
        void _ShowRuleChanged(object sender, EventArgs e)
        {
            this.UpdateCheck();
        }
        private void UnRegisterSurfaceEvent(ICoreWorkingRulesSurface surface)
        {
            surface.ShowRuleChanged  -= new EventHandler(_ShowRuleChanged);
        }
    }
}

