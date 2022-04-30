

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: Core2DDrawingCanvasLayer.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.ICore.Drawing2D
{
    /*
     * represent a layer used to render on element by adding margin and padding properties
     * */
    [Core2DDrawingObject("CanvasLayer")]
    public class Core2DDrawingCanvasLayer : Core2DDrawingLayer 
    {
        public Core2DDrawingCanvasLayer()
        {
            this.ElementAdded += Core2DDrawingCanvasLayer_ElementAdded;
            this.ElementRemoved += Core2DDrawingCanvasLayer_ElementRemoved;
        }

        void Core2DDrawingCanvasLayer_ElementRemoved(object sender, CoreItemEventArgs<ICore2DDrawingLayeredElement> e)
        {
            Core2DDrawingLayeredElement c = e.Item as Core2DDrawingLayeredElement;
            if (c != null)
            {
                c.DetachProperties(this);
            }
        }

        void Core2DDrawingCanvasLayer_ElementAdded(object sender, CoreItemEventArgs<ICore2DDrawingLayeredElement> e)
        {
            //register canvas properties
            Core2DDrawingLayeredElement c = e.Item as Core2DDrawingLayeredElement;
            if (c != null) {
                //c.AttachProperty("MarginLeft", this, typeof(int));
                //c.AttachProperty("MarginRight", this, typeof(int));
                //c.AttachProperty("MarginTop", this, typeof(int));
                //c.AttachProperty("MarginBottom", this, typeof(int));
            }
        }

    }
}
