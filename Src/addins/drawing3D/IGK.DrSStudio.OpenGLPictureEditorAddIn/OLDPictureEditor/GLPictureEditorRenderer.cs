

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: GLPictureEditorRenderer.cs
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
file:GLPictureEditorRenderer.cs
*/

using IGK.ICore;using IGK.DrSStudio.WinUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace IGK.DrSStudio.GLPictureEditorAddIn
{
    class GLPictureEditorRenderer
    {
        public static Colorf GLPictureEditorSceneBackgroundColor { get { return CoreRenderer.GetColor("GLPictureEditorSceneBackgroundColor", Colorf.FromFloat (0.4f)); } }
    }
}

