

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: WebAddControllerMenu.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/

using IGK.ICore;
using IGK.ICore.Codec;
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
﻿
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.DrSStudio.Web.Menu.Web
{
    using IGK.ICore.WinUI.Configuration;
    using IGK.ICore.Settings;
    [DrSStudioMenu("Web.AddController", 0x01)]
    sealed class WebAddControllerBrowserMenu : WebSolutionMenuBase
    {
        protected override bool PerformAction()
        {
            var ctr = new controllerName();
            if (Workbench.ConfigureWorkingObject(ctr, "title.newController".R(), false, Size2i.Empty) == enuDialogResult.OK)
            {
                string v_response = WebSolutionUtility.AddController(this.CurrentSolution, ctr.Name);
            }
            return false;
        }

        class controllerName : CoreConfigurableObjectBase
        {
            private string m_Name;
            [CoreConfigurableProperty()]
            [CoreXMLDefaultAttributeValue("ControllerName")]
            [CoreSettingDefaultValue("ControllerName")]
            public string Name
            {
                get { return m_Name; }
                set
                {
                    if (m_Name != value)
                    {
                        m_Name = value;
                    }
                }
            }

            public controllerName()
            {
                this.m_Name = "ControllerName";
            }
        }
    }
}
