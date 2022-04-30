

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: ContextMenuSetSelectionType.cs
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
file:ContextMenuSetSelectionType.cs
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
/* 
-------------------------------------------------------------
This file is part of iGK-DEV-DrawingStudio
-------------------------------------------------------------
-------------------------------------------------------------
-------------------------------------------------------------
view license file in Documentation folder to get more info
Copyright (c) 2008-2010 
Author  : Charles A.D. BONDJE DOUE 
mail : bondje.doue@hotmail.com
-------------------------------------------------------------
-------------------------------------------------------------
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using IGK.DrSStudio.ContextMenu;
namespace IGK.DrSStudio.Drawing2D.ImageSelection.ContextMenu
{
    using IGK.DrSStudio.Drawing2D.ContextMenu;
    [CoreContextMenu(ImageSelectionElement.CONTEXTMENU_SETSELECTIONMODE, 50, SeparatorBefore=true)]
    sealed class CoreContextMenu_SetSelection : _ImageSelectionContextMenuBase
    {
        class ChildMenu : _ImageSelectionContextMenuBase      
        {
            enuSelectionOperationType m_prop;
            CoreContextMenu_SetSelection m_owner;
            public ChildMenu(CoreContextMenu_SetSelection owner, enuSelectionOperationType prop)
            {
                this.m_owner = owner;
                this.m_prop = prop;
                this.IsRootMenu = false;
                this.Mecanism = this.m_owner.Mecanism;
                this.m_owner.MecanismChanged += new EventHandler(m_owner_MecanismChanged);
                Tools.SelectionRegionOperationTools.Instance.RegionSelectionOperationTypeChanged += new EventHandler(Instance_RegionSelectionTypeChanged);
            }
            protected override void InitContextMenu()
            {
                base.InitContextMenu();
                CheckMenuItem();
            }
            private void CheckMenuItem()
            {
                if (this.MenuItem != null)
                    this.MenuItem.Checked =
                        Tools.SelectionRegionOperationTools.Instance.RegionSelectionOperationType
                        == m_prop;   
            }
            void Instance_RegionSelectionTypeChanged(object sender, EventArgs e)
            {
                this.CheckMenuItem();
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
                Tools.SelectionRegionOperationTools.Instance.RegionSelectionOperationType =
                    m_prop;
                return false;
            }
            protected override void OnWorkbenchChanged(EventArgs eventArgs)
            {
                base.OnWorkbenchChanged(eventArgs);
                this.Visible = m_owner.Visible; ;
                this.Enabled = m_owner .Enabled;
                this.m_owner.EnabledChanged += new EventHandler(m_owner_EnabledChanged);
                this.m_owner.VisibleChanged += new EventHandler(m_owner_VisibleChanged);
            }
        }
        public CoreContextMenu_SetSelection():base()
        {
            this.IsRootMenu = true;
            string v_name = string.Empty;
            int v_count = 0;
            foreach (enuSelectionOperationType item in Enum.GetValues(typeof(enuSelectionOperationType)))
            {
                v_name = item.ToString();
                RegisterChildMenu(new ChildMenu(this, item), 
                    v_name, 
                    v_count, 
                    string.Format("btn_enuSelectionOperationType_{0}", item.ToString()));                 
                v_count++;
            }
        }
    }
}

