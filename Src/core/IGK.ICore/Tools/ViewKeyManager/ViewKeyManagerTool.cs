

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: ViewKeyManagerTool.cs
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
file:ViewKeyManagerTool.cs
*/

﻿using System;
using System.Collections.Generic;
using System.Text;



namespace IGK.ICore.Tools
{
    using IGK.ICore;
    using IGK.ICore.WinUI;
    using IGK.ICore.Tools;
    using IGK.ICore.Menu;
    using IGK.ICore.Actions;
    [CoreTools(
     "Tool.ViewToolManager"
     )]
    class ViewKeyManagerTool :
        CoreShortCutMenuContainerToolBase,
        ICoreMenuMessageShortcutContainer
    {


        //  [CoreMenu("ViewTools", -1, IsVisible = false,
        //Shortcut = enuKeys.Control | enuKeys.W)]
        class ViewKeyToolManagerToolMenu : CoreApplicationMenu
        {
            const string MENUNAME = "viewkeymanagermenu";
            public ViewKeyToolManagerToolMenu()
            {
                this.IsRootMenu = true;
            }
            protected override bool PerformAction()
            {
                if (this.Workbench is ICoreWorkbenchMessageFilter s)
                CoreActionBase.StartFilteringAction(
                    s,
                    ViewKeyManagerTool.Instance);
                return false;
            }

            internal void Register(ICoreSystemWorkbench wbench)
            {
                CoreMenuAttribute attr = new CoreMenuAttribute(MENUNAME, -1);
                attr.IsVisible = true;
                attr.Shortcut = enuKeys.Control | enuKeys.W;
                this.SetAttribute(attr);
                base.Register(
                    attr,
                     this);

            }
            protected override void InitMenu()
            {
                base.InitMenu();
            }

            internal void Unregister()
            {
                base.Unregister(MENUNAME);
            }
        }

        public static ViewKeyManagerTool Instance
        {
            get
            {

                return sm_instance;
            }
        }
        static ViewKeyManagerTool()
        {
            sm_instance = new ViewKeyManagerTool();
        }
        private static ViewKeyManagerTool sm_instance;
        private ViewKeyToolManagerToolMenu c;
        public override bool CanShow
        {
            get { return false; }
        }
        protected override void GenerateHostedControl()
        {
            this.HostedControl = null;
            var m = this.Workbench;
            if (m != null)
            {
                c = new ViewKeyToolManagerToolMenu();
                c.Register(m);
            }
        }
        private ViewKeyManagerTool()
        {
            this.Key = enuKeys.W;
        }
        protected override void UnregisterBenchEvent(ICoreSystemWorkbench Workbench)
        {
            base.UnregisterBenchEvent(Workbench);
            if (c != null)
                c.Unregister();

        }


        protected override void AddTool(CoreMenuActionBase item)
        {
            if (item is CoreViewToolMenuBase)
                base.AddTool(item as CoreViewToolMenuBase);
        }

    }
}

