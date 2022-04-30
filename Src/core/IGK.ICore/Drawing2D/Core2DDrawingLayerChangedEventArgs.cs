

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: Core2DDrawingLayerChangedEventArgs.cs
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
file:Core2DDrawingLayerChangedEventArgs.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace IGK.ICore.Drawing2D
{
    public delegate void Core2DDrawingLayerChangedEventHandler(object sender, Core2DDrawingLayerChangedEventArgs e);
    /// <summary>
    /// represent a layer changed event args
    /// </summary>
    public class Core2DDrawingLayerChangedEventArgs : CoreWorkingElementChangedEventArgs<ICore2DDrawingLayer >
    {
        public Core2DDrawingLayerChangedEventArgs(ICore2DDrawingLayer oldlayer, ICore2DDrawingLayer newLayer):
            base(oldlayer, newLayer )
        {
        }
    }
}

