

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: ConnectorManager.cs
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
file:ConnectorManager.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace IGK.DrSStudio.Drawing2D.Tools
{
    class ConnectorManager
    {
        internal IConnectorElement connector;
        internal ICore2DDrawingLayeredElement lStart;
        internal ICore2DDrawingLayeredElement lTarget;
        internal ICore2DDrawingLayer lLayer;
        public ConnectorManager(
            ICore2DDrawingLayer lLayer,
            IConnectorElement connector,
            ICore2DDrawingLayeredElement lStart, 
            ICore2DDrawingLayeredElement lTarget)
        {
            this.lLayer = lLayer;
            this.connector = connector;
            this.lStart = lStart;
            this.lTarget = lTarget;
            RegisterLayerEvent();
        }
        private void RegisterLayerEvent()
        {
            this.lLayer.ElementRemoved += new Core2DDrawingElementEventHandler(lLayer_ElementRemoved);
        }
        void lLayer_ElementRemoved(object o, Core2DDrawingElementEventArgs e)
        {
            if ((e.Element == lStart)||(e.Element == lTarget ))
            {
                this.lLayer.ElementRemoved -= new Core2DDrawingElementEventHandler(lLayer_ElementRemoved);
                this.lLayer.Elements.Remove(this.connector);
            }
        }
    }
}

