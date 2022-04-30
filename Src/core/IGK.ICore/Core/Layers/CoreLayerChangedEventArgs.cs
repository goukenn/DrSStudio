

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: CoreLayerChangedEventArgs.cs
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
file:CoreLayerChangedEventArgs.cs
*/
using System;
using System.Collections.Generic;
using System.Text;
namespace IGK.ICore.Layers
{
    public delegate void CoreLayerChangedEventHandler (object o, CoreLayerChangedEventArgs e); 
    public class CoreLayerChangedEventArgs : EventArgs 
    {
          private ICoreLayer  m_Layer;
        public ICoreLayer  Layer
        {
            get { return m_Layer; }
        }
        public CoreLayerChangedEventArgs(ICoreLayer layer)
        {
            this.m_Layer = layer;
        }
    }
}

