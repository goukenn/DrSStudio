

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: CoreButtonDocumentUtils.cs
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
file:CoreButtonDocumentUtils.cs
*/
using System;
using System.Collections.Generic;
using System.Text;
namespace IGK.ICore.WinUI
{
    using IGK.ICore;using IGK.ICore.Drawing2D;
    /// <summary>
    /// document utils 
    /// </summary>
    public static class CoreButtonDocumentUtils
    {
        public static CoreButtonDocument Rotate(CoreButtonDocument document, enuButtonDocumentRotation rotation )
        {
            if (document == null)
                return null;
            CoreButtonDocument doc = document;
            List<ICore2DDrawingDocument> v_l = new List<ICore2DDrawingDocument>();
            switch (rotation)
            { 
                case enuButtonDocumentRotation.FlipHorizontal :
                    foreach (ICore2DDrawingDocument  item in document.GetDocuments () )
                    {
                        ICore2DDrawingDocument c = item.Clone() as ICore2DDrawingDocument;
                        v_l.Add(c);
                        RotateFlip(c,90);
                    }
                    break;
                case enuButtonDocumentRotation.FlipVertical :
                    foreach (ICore2DDrawingDocument item in document.GetDocuments())
                    {
                        ICore2DDrawingDocument c = item.Clone() as ICore2DDrawingDocument;
                        v_l.Add(c);
                        RotateFlip(c, 180);
                    }
                    break;
                case enuButtonDocumentRotation.None :
                    return doc;
                default :
                    break;
            }
            document = CoreButtonDocument.Create(v_l.ToArray());
            return document;
        }
        private static void RotateFlip(ICore2DDrawingDocument c, float angle)
        {
            Vector2f v_center = CoreMathOperation.GetCenter(c.Bounds);
            foreach (ICore2DDrawingLayer l in c.Layers)
            {
                foreach (ICore2DDrawingLayeredElement  item in l.Elements)
                {
                    //if (item.CanRotate)
                    //    item.Rotate(angle , v_center, enuMatrixOrder.Append);
                }
            }
        }
    }
}

