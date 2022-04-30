

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: _MenuGLImageInvert.cs
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
file:_MenuGLImageInvert.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace IGK.DrSStudio.GLPictureEditorAddIn.Menu.GLImage
{
    
using IGK.ICore;using IGK.DrSStudio.GLPictureEditorAddIn.Effect;
    [IGK.DrSStudio.Menu.CoreMenu("GLImage.Invert", 1, ImageKey="Menu_Invert")]
    class _MenuGLImageInvert : GLEditorMenuBase 
    {
        protected override bool PerformAction()
        {
            GLInvertEffect c = new GLInvertEffect();
            this.CurrentSurface.Effects.Add(c);
            return base.PerformAction();
        }
    }
}

