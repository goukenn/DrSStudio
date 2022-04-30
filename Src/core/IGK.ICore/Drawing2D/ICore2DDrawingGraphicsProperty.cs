

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: ICore2DDrawingGraphicsProperty.cs
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
file:ICore2DDrawingGraphicsProperty.cs
*/
using IGK.ICore;using IGK.ICore.Codec;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace IGK.ICore.Drawing2D
{
    /// <summary>
    /// reprsent an interface for graphics properties
    /// </summary>
    public interface ICore2DDrawingGraphicsProperty : ICore2DDrawingElement
    {
        [CoreXMLAttribute()]
        [CoreXMLDefaultAttributeValue(enuSmoothingMode.AntiAliazed)]
        enuSmoothingMode SmoothingMode { get; set; }
        [CoreXMLAttribute()]
        [CoreXMLDefaultAttributeValue(enuCompositingMode.Over)]
        enuCompositingMode CompositingMode { get; set; }        
    }
}

