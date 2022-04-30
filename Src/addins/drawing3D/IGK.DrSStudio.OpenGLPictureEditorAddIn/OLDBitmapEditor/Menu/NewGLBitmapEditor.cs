

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: NewGLBitmapEditor.cs
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
file:NewGLBitmapEditor.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace IGK.DrSStudio.OGLBitmapEditor.Menu
{
    using OGLBitmapEditor.WinUI;
    [IGK.DrSStudio.Menu.CoreMenu ("Window.OpenGLBitmapEditor", 200)]
    class NewGLBitmapEditor : IGK.DrSStudio.Menu.CoreApplicationMenu 
    {
        protected override bool PerformAction()
        {
            OGLBitmapEditorSurface surface = new OGLBitmapEditorSurface();
            this.Workbench.Surfaces.Add(surface);
            return true;
        }
    }
}

