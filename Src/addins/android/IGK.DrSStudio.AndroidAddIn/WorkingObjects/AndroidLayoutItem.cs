

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: AndroidLayoutItem.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
﻿

using IGK.ICore;using IGK.ICore.Drawing2D;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.DrSStudio.Android
{
    /// <summary>
    /// represent a android base layout class item
    /// </summary>
    public class AndroidLayoutItem : RectangleElement, ICore2DDrawingVisitable 
    {
        protected override void InitGraphicPath(CoreGraphicsPath p)
        {
            base.InitGraphicPath(p);
        }
        protected override void InitializeElement()
        {
            base.InitializeElement();
        }

        public virtual bool Accept(ICore2DDrawingVisitor visitor)
        {
            return (visitor != null);
        }

        public virtual void Visit(ICore2DDrawingVisitor visitor)
        {
            if (visitor == null)
                return;
            object obj = visitor.Save();
            var g = this.GetPath();
            visitor.FillPath(this.FillBrush, g);
            visitor.DrawPath(this.StrokeBrush, g);
            
            visitor.Restore(obj);
        }
    }
}
