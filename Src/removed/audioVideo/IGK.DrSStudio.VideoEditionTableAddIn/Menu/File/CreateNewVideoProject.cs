

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: CreateNewVideoProject.cs
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
file:CreateNewVideoProject.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace IGK.DrSStudio.VideoEditionTableAddIn.Menu
{
    using IGK.ICore;using IGK.DrSStudio.VideoEditionTableAddIn.Tools;
    [IGK.DrSStudio.Menu.CoreMenu ( VideoConstant .MENU_NEWVID_PROJECT,200)]
    public class CreateNewVideoProject : IGK.DrSStudio.Menu.CoreApplicationMenu  
    {
        protected override bool PerformAction()
        {
            if (ToolVideoEditorManager.Instance.OpenedSurface == null)
            {
                WinUI.XVideoEditorSurface vid = new IGK.DrSStudio.VideoEditionTableAddIn.WinUI.XVideoEditorSurface();
                vid.VideoProject = new VideoProject();
                this.Workbench.Surfaces.Add(vid);
                ToolVideoEditorManager.Instance.OpenedSurface = vid;
            }
            else {
                this.Workbench.CurrentSurface = ToolVideoEditorManager.Instance.OpenedSurface;
            }
            return base.PerformAction();
        }
    }
}

