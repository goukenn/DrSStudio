

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: EditPropertyAction.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
/*
IGKDEV - [2008 - 2014]
author: C.A.D . BONDJE DOUE
file:EditPropertyAction.cs
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
namespace IGK.DrSStudio.Drawing2D.Actions
{
    using IGK.ICore;using IGK.DrSStudio.Actions;
    using IGK.DrSStudio.WinUI.Configuration;
    /// <summary>
    /// represent action to edit current element
    /// </summary>
    sealed class EditPropertyAction : Core2DDrawingMecanismAction
    {
        protected override bool PerformAction()
        {
            ICoreWorkingConfigurableObject element = this.Mecanism.GetEditElement();
            if (element !=null)
            {
                CoreSystem.Instance.Workbench.ConfigureWorkingObject(
                    element);
            }
            return false;   
        }
    }
}

