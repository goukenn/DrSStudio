

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: UMlMecanismSelection.cs
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
file:UMlMecanismSelection.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace IGK.DrSStudio.UMLAddIn.WorkingElements
{
    
using IGK.ICore;using IGK.ICore.Drawing2D;
    using IGK.ICore.WinUI;
    /// <summary>
    /// represent a base selection mecanism element
    /// </summary>
    public class UMLMecanismSelection : RectangleElement.Mecanism
    {
        public UMLMecanismSelection()
        {
        }
        protected override void RegisterDocumentEvent(ICore2DDrawingDocument document)
        {
            base.RegisterDocumentEvent(document);
        }
        protected override void UnRegisterDocumentEvent(ICore2DDrawingDocument document)
        {
            base.UnRegisterDocumentEvent(document);
        }
       
        protected override void OnMouseDown(CoreMouseEventArgs e)
        {
            base.OnMouseDown(e);
        }
        protected override void OnMouseMove(CoreMouseEventArgs e)
        {
            base.OnMouseMove(e);
        }
        protected override void OnMouseUp(CoreMouseEventArgs e)
        {
            base.OnMouseUp(e);
        }
    }
}

