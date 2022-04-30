

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: IGLEditorLayerElement.cs
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
file:IGLEditorLayerElement.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace IGK.DrSStudio.GLPictureEditorAddIn
{
    public interface IGLEditorLayerElement : IGLEditorRenderer , IDisposable 
    {
        void FreeResources(IGK.OGLGame.Graphics.OGLGraphicsDevice device);
    }
}

