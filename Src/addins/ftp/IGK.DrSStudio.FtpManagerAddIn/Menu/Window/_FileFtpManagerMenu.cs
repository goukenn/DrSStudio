

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: _FileFtpManagerMenu.cs
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
file:_FileFtpManagerMenu.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace IGK.DrSStudio.FtpManagerAddIn.Menu
{
    
using IGK.ICore;
    using IGK.DrSStudio.FtpManagerAddIn.Tools;
    using IGK.DrSStudio.Menu;
    using IGK.ICore.Menu;
    //[DrSStudioMenu("Window.FtpManager", 20)]
    sealed class _FileFtpManagerMenu : CoreApplicationMenu
    {
        protected override bool PerformAction()
        {
            //create and generate ftp manager
            //-----------------------------
            if (FtpManagerTool.Instance.FtpSurface == null)
            {
                FtpManagerTool.Instance.CreateNewSurface();
                this.Workbench.AddSurface (FtpManagerTool.Instance.FtpSurface,true );
            }
            else 
                this.Workbench.SetCurrentSurface (FtpManagerTool.Instance.FtpSurface);
            return base.PerformAction();
        }
    }
}

