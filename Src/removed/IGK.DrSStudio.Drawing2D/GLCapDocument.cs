

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: GLCapDocument.cs
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
file:GLCapDocument.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace IGK.DrSStudio.Drawing2D
{
    [Core2DDrawingDocumentAttribute("GLCapDocument")]
    public class GLCapDocument : Core2DDrawingDocument 
    {
        //Get the surface type
        public override Type DefaultSurfaceType
        {
            get { return CoreSystem.GetWorkingObjectType(CoreConstant.DRAWING2D_SURFACE); }
        }
        public GLCapDocument(CoreUnit width, CoreUnit height):base(width, height )
        {
        }
    }
}

