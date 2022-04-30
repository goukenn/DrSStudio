

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: RSAddNewStringActionsMenu.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
﻿using IGK.ICore.WinCore;
using IGK.ICore;using IGK.DrSStudio.ResourcesManager.WinUI;
using IGK.ICore.WinUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IGK.ICore.WinUI.Common;

namespace IGK.DrSStudio.ResourcesManager.Menu.Action
{
    [RSMenuAction(RSConstant.RSMENU_ACTION+".addString", RSConstant.RSMENU_ACTION_INDEX + 200, SeparatorBefore=true)]
    class RSAddNewStringActionsMenu : RSMenuActionBase 
    {
        protected override bool PerformAction()
        {
            using (var f = Workbench.CreateNewDialog())
            { 
                XResourcesAddStringGUI ctr = new XResourcesAddStringGUI ();
                f.Controls.Add(ctr);
                if (f.ShowDialog() == enuDialogResult.OK)
                {
                    this.CurrentSurface.AddText(ctr.Key, ctr.Value);
                }
            }
            return base.PerformAction();
        }
    }
}
