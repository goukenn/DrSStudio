

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: WPFLayerChangeEventArgs.cs
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
file:WPFLayerChangeEventArgs.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace IGK.DrSStudio.WPFSurfaceAddIn.WorkingObjects
{
    public delegate void WPFLayerChangeEventHandler(object o, WPFLayerChangeEventArgs e);
    /// <summary>
    /// reprensent the layer changed event args
    /// </summary>
    public class WPFLayerChangeEventArgs : EventArgs 
    {
        private WPFLayer m_OldLayer;
        private WPFLayer  m_NewLayer;
        public WPFLayer  NewLayer
        {
            get { return m_NewLayer; }
        }
        public WPFLayer OldLayer
        {
            get { return m_OldLayer; }
        }
        public WPFLayerChangeEventArgs(WPFLayer oldlayer, WPFLayer newLayer)
        {
            this.m_NewLayer = newLayer;
            this.m_OldLayer = oldlayer;
        }
    }
}

