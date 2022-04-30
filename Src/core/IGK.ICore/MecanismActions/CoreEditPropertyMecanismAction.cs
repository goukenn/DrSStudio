

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: CoreEditPropertyMecanismAction.cs
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
file:CoreEditPropertyMecanismAction.cs
*/

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace IGK.ICore.MecanismActions
{
    using IGK.ICore;using IGK.ICore.Actions;
    using IGK.ICore.WinUI;
    using IGK.ICore.WinUI.Configuration;
    /// <summary>
    /// represent action to edit current element
    /// </summary>
    sealed class CoreEditPropertyMecanismAction : CoreMecanismActionBase
    {
        protected override bool PerformAction()
        {
            ICoreWorkingConfigurableObject element = this.Mecanism.GetEditElement();
            if (element !=null)
            {
                this.Mecanism.DisposeSnippet();
                if (CoreSystem.Instance.Workbench is ICoreWorkbench a)
                    a.ConfigureWorkingObject(
                    element,
                    "title.editproperty".R(element.Id),
                    false,
                    new Size2i(350, 650) );
                this.Mecanism.GenerateSnippets();
                this.Mecanism.InitSnippetsLocation();
            }
            return false;   
        }
    }
}

