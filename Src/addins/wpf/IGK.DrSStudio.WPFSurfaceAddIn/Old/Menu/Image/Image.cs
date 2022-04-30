

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: Image.cs
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
file:Image.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace IGK.DrSStudio.WPFSurfaceAddIn.Menu.Image
{
    using IGK.ICore.WinCore;
using IGK.ICore;using IGK.DrSStudio.WinUI;
    using IGK.DrSStudio.WPFSurfaceAddIn.WinUI;
    using IGK.DrSStudio.WPFSurfaceAddIn.WorkingObjects;
    [WPFMenu ("Image", 5)]
    public sealed class Image : WPFImageMenuBase  
    {
        public Image()
        {
            this.IsRootMenu = true;
        }
        protected override bool IsVisible()
        {
            bool v = (this.CurrentSurface != null) && (this.ImageElement != null);
            return v;            
        }
    }
}

