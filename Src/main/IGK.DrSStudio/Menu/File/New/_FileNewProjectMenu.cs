

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: _FileNewProject.cs
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
file:_NewProject.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
namespace IGK.DrSStudio.Menu.File
{
    using IGK.ICore.WinCore;
using IGK.ICore;using IGK.ICore.Menu;
    using IGK.ICore.WinUI;

    /// <summary>
    /// create new new project
    /// </summary>
    [DrSStudioMenu("File.New.Project", 0x02, 
        ImageKey = "Menu_NewDrsProj",
        IsVisible=true ,
        Shortcut=enuKeys.Control | enuKeys.Shift | enuKeys.N ,
        SeparatorAfter=true)]
    sealed class _FileNewProject : CoreApplicationMenu 
    {
        protected override bool PerformAction()
        {
            return Workbench.CreateNewProject();            
        }
    }
}

