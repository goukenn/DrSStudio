

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: Core2DDrawingLayerEventArgs.cs
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
file:Core2DDrawingLayerEventArgs.cs
*/

﻿using IGK.ICore;
using IGK.ICore.Layers;
using System;
using System.Collections.Generic;
using System.Text;
namespace IGK.ICore.Drawing2D
{
    public delegate void Core2DDrawingLayerEventHandler(object o, Core2DDrawingLayerEventArgs e);
    public class Core2DDrawingLayerEventArgs : CoreLayerChangedEventArgs 
    {
        private int m_ZIndex;
        public int ZIndex
        {
            get { return m_ZIndex; }
        }
        public new ICore2DDrawingLayer Layer
        {
            get { return base.Layer as ICore2DDrawingLayer ; }
        }
        //public Core2DDrawingLayerEventArgs(ICore2DDrawingLayer layer):base(layer )
        //{
        //    this.m_ZIndex = layer.ZIndex;
        //}
        public Core2DDrawingLayerEventArgs(ICore2DDrawingLayer layer, int zindex)
            : base(layer)
        {
            this.m_ZIndex = zindex;
        }
    }
}

