

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: _CloseSurface.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
using IGK.ICore.WinCore;
using IGK.ICore;
/*
IGKDEV @ 2008-2016
author: C.A.D . BONDJE DOUE
file:_CloseSurface.cs
*/
using IGK.ICore.Menu;
using IGK.ICore.WinUI;
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
using System.Text;
namespace IGK.DrSStudio.Menu.File
{
    [DrSStudioMenu("File.CloseSurface", 
        Int16.MaxValue - 100,
        Shortcut= enuKeys.Control | enuKeys.W | enuKeys.Shift )]
    class _CloseSurfaceMenu : CoreApplicationSurfaceMenuBase 
    {
        protected override bool PerformAction()
        {
          var fb =  base.Workbench as ICoreSurfaceManagerWorkbench;
            if ((fb!=null) && (this.CurrentSurface != null))
            {
                fb.Surfaces.Remove(this.CurrentSurface);
                return true;
            }
            return false;
        }
        protected override void OnCurrentSurfaceChanged(CoreWorkingElementChangedEventArgs<ICoreWorkingSurface> e)
        {
            this.SetupEnableAndVisibility();
        }
        
        protected override bool IsEnabled()
        {
            return (this.CurrentSurface != null);
        }
    }
}

