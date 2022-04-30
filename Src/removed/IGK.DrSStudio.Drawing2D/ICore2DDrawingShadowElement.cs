

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: ICore2DDrawingShadowElement.cs
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
file:ICore2DDrawingShadowElement.cs
*/
using IGK.ICore;using IGK.DrSStudio.Rendering;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
namespace IGK.DrSStudio.Drawing2D
{
    public interface ICore2DDrawingShadowElement : ICoreBrushOwner 
    {
        bool AllowShadow { get; set; }
        ICore2DShadowProperty ShadowProperty { get; }
        CoreGraphicsPath GetShadowPath();
        void InvalidateDesignSurface(bool visibility);
        void RenderShadow(Graphics g);
    }
}

