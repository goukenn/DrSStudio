

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: ContextMenuSelectionRegion.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
using IGK.ICore.WinCore;
using IGK.ICore;
using IGK.ICore.Menu;
using IGK.ICore.WinUI;
using IGK.ICore.Drawing2D;
using IGK.ICore.Drawing2D.Menu;
using IGK.ICore.Drawing2D.WinUI;
using IGK.DrSStudio;
using IGK.DrSStudio.Menu;
using IGK.DrSStudio.WinUI;
using IGK.DrSStudio.Drawing2D;
using IGK.DrSStudio.Drawing2D.WinUI;
/*
IGKDEV - [2008 - 2014]
author: C.A.D . BONDJE DOUE
file:ContextMenuSelectionRegion.cs
*/
/* 
-------------------------------------------------------------------
Company: IGK-DEV
Author : C.A.D. BONDJE DOUE
SITE : http://www.igkdev.be
Application : DrSStudio
powered by IGK - DEV &copy; 2008-2012
THIS FILE IS A PART OF THE DRSSTUDIO APPLICATION. SEE "License.txt"
FOR MORE INFORMATION ABOUT THE LICENSE
------------------------------------------------------------------- 
*/
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
namespace IGK.DrSStudio.Drawing2D.ImageSelection.ContextMenu
{
    using IGK.DrSStudio.ContextMenu;
    [CoreContextMenu(ImageSelectionElement.CONTEXTMENU_SELECTREGION, 52)]
    sealed class CoreContextMenu_RegionSelection :
         _ImageSelectionContextMenuBase
    {
        class ChildMenu : _ImageSelectionContextMenuBase
        {
            enuCoreRegionOperation m_operation;
            CoreContextMenu_RegionSelection m_owner;
            public ChildMenu(CoreContextMenu_RegionSelection owner, enuCoreRegionOperation operation)
            {
                this.m_operation = operation;
                this.m_owner = owner;
                this.Mecanism = this.m_owner.Mecanism;
                this.m_owner.MecanismChanged += new EventHandler(m_owner_MecanismChanged);
                Tools.SelectionRegionOperationTools.Instance.RegionOperationChanged += new EventHandler(Instance_RegionOperationChanged);
            }
            protected override void InitContextMenu()
            {
                base.InitContextMenu();
                this.MenuItem.Checked = Tools.SelectionRegionOperationTools.Instance.RegionOperation ==
                        this.m_operation;
            }
            void Instance_RegionOperationChanged(object sender, EventArgs e)
            {
                if (this.MenuItem != null)
                    this.MenuItem.Checked = Tools.SelectionRegionOperationTools.Instance.RegionOperation ==
                        this.m_operation;
            }
            void m_owner_MecanismChanged(object sender, EventArgs e)
            {
                this.Mecanism = this.m_owner.Mecanism;
            }
            void m_owner_VisibleChanged(object sender, EventArgs e)
            {
                this.Visible = this.m_owner.Visible;
            }
            void m_owner_EnabledChanged(object sender, EventArgs e)
            {
                this.Enabled = this.m_owner.Enabled;
            }
            protected override bool PerformAction()
            {                
                Tools.SelectionRegionOperationTools.Instance.RegionOperation = this.m_operation ;
                return false;
            }
            protected override void OnWorkbenchChanged(EventArgs eventArgs)
            {
                base.OnWorkbenchChanged(eventArgs);
                this.Visible = this.m_owner.Visible;
                this.Enabled = this.m_owner.Enabled;
                this.m_owner.EnabledChanged += new EventHandler(m_owner_EnabledChanged);
                this.m_owner.VisibleChanged += new EventHandler(m_owner_VisibleChanged);
            }
        }
        public CoreContextMenu_RegionSelection()
        {
            string v_name = string.Empty;
            int v_count = 0;
            foreach (enuCoreRegionOperation item in Enum.GetValues(typeof(enuCoreRegionOperation)))
            {
                v_name = item.ToString();
                RegisterChildMenu(new ChildMenu(this, item),
                    v_name,
                    v_count,
                    string.Format("btn_enuSelectionOperationType_{0}", item.ToString()));
                //CoreContextMenuAttribute v_attr = new CoreContextMenuAttribute(v_name, v_count);
                //v_attr.ImageKey = string.Format("btn_2DAlign_{0}", item.ToString());
                //ChildMenu ch = new ChildMenu(item);
                //ch.SetAttribValue (v_attr);             
                //if (CoreSystem.Instance.ContextMenus.Register(v_attr, ch) == false)
                //{
                //    CoreServices.ShowError(string.Format ("Element {0} not registered", v_name));
                //}
                v_count++;
            }
        }
    }
}

