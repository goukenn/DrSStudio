

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: _NewWPFSurface.cs
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
file:_NewWPFSurface.cs
*/
using IGK.ICore.WinCore;
using IGK.ICore;using IGK.DrSStudio.Menu;
using IGK.DrSStudio.WPFSurfaceAddIn.WinUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace IGK.DrSStudio.WPFSurfaceAddIn.Menu.File
{
    [CoreMenu ("File.New.CreateNewWPFSurface", 0x400)]
    class _NewWPFSurface : CoreApplicationMenu 
    {
        public _NewWPFSurface()
        {
        }
        protected override bool PerformAction()
        {
                    WPFHostSurface c = new WPFHostSurface ();
                    Workbench.Surfaces.Add (c);
            this.Workbench.CurrentSurface = c;
 	                return false;
        }
    }
}

