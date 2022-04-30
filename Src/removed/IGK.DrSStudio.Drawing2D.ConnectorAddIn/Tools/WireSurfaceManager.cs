

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: WireSurfaceManager.cs
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
file:WireSurfaceManager.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace IGK.DrSStudio.Drawing2D.Tools
{
    using IGK.ICore;using IGK.DrSStudio.Drawing2D.WinUI ;
    class WireSurfaceManager
    {
        private IGK.DrSStudio.Drawing2D.WinUI.ICore2DDrawingSurface m_Surface;
        private Dictionary<ConnectorKey, IConnectorElement> m_dictionary;
        public IGK.DrSStudio.Drawing2D.WinUI.ICore2DDrawingSurface Surface
        {
            get { return m_Surface; }            
        }
        public WireSurfaceManager(ICore2DDrawingSurface surface)
        {
            this.m_Surface = surface;
            this.m_dictionary = new Dictionary<ConnectorKey, IConnectorElement>();
        }
        /// <summary>
        /// get if connected
        /// </summary>
        /// <param name="start"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        public bool Contains(ICore2DDrawingLayeredElement start,ICore2DDrawingLayeredElement target)
        {
            if ((start == null) || (target == null))
                return true;
            ConnectorKey c = new ConnectorKey();
            c.lStart = start;
            c.lTarget = target;
            return m_dictionary.ContainsKey(c);
        }
        internal void Add(ICore2DDrawingLayeredElement start, ICore2DDrawingLayeredElement target, IConnectorElement cl)
        {
            ConnectorKey c = new ConnectorKey();
            c.lStart = start;
            c.lTarget = target;
            if (!m_dictionary.ContainsKey(c))
            {
                m_dictionary.Add(c, cl);
                new ConnectorManager(
                    this.m_Surface.CurrentLayer,
                    cl,
                    start,
                    target);
            }
        }
    }
}

