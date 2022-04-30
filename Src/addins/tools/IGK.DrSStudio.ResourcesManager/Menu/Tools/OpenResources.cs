

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: OpenResources.cs
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
file:OpenResources.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace IGK.DrSStudio.ResourcesManager.Menu
{
    using IGK.ICore.WinCore;
    using IGK.ICore;
    using IGK.DrSStudio.ResourcesManager.WinUI;
    using IGK.DrSStudio.ResourcesManager.Tools;
    using IGK.ICore.Menu;
    using IGK.DrSStudio.Menu;

    [DrSStudioMenu("Tools.StringResourcesManager", RSConstant.TOOL_INDEX)]
    class OpenResources : CoreApplicationMenu 
    {
        protected override bool PerformAction()
        {
            if (RSToolManager.Instance.ResourceSurface == null)
            {
                RSToolManager.Instance.CreateSurface();
            }
            else
            {
                RSToolManager.Instance.ResourceSurface.Reload();
            }
            Workbench.AddSurface (RSToolManager.Instance.ResourceSurface, true );
                return true;
        }
        protected override void InitMenu()
        {
            base.InitMenu();
        }
    }
}

