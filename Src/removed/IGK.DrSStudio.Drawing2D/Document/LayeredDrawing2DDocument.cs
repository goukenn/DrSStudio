

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: LayeredDrawing2DDocument.cs
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
file:LayeredDrawing2DDocument.cs
*/
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace IGK.DrSStudio.Drawing2D
{
    /// <summary>
    /// represent the layered drawing 2D Docucment
    /// </summary>
    [Core2DDrawingDocumentAttribute("LayerDocument")]
    public class LayeredDrawing2DDocument : Core2DDrawingDocument
    {
        public LayeredDrawing2DDocument()
        {
        }
        public LayeredDrawing2DDocument(CoreUnit width, CoreUnit height):base(width, height )
        {
        }
        public static Core2DDrawingDocument CreateFromBitmap(Image bmp)
        {
            ImageElement g = ImageElement.FromImage(bmp);
            if (g != null)
            {
                Core2DDrawingDocument doc = new LayeredDrawing2DDocument();
                doc.SetSize(g.Width, g.Height);
                doc.CurrentLayer.Elements.Add(g);
                return doc;
            }
            return null;
        }
    }
}

