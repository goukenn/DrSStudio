

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: XmlEditorManager.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/

using IGK.ICore;
using IGK.ICore.Codec;
using IGK.ICore.Menu;
using IGK.ICore.WinUI;
using IGK.ICore.Drawing2D;
using IGK.ICore.Drawing2D.Menu;
using IGK.ICore.Drawing2D.WinUI;
using IGK.DrSStudio;
using IGK.DrSStudio.Menu;
using IGK.DrSStudio.WinUI;
using IGK.DrSStudio.Drawing2D;
using IGK.DrSStudio.Drawing2D.WinUI;
/*
IGKDEV @ 2008-2016
author: C.A.D . BONDJE DOUE
file:XmlEditorManager.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace IGK.DrSStudio.XmlTextEditor
{
    using IGK.DrSStudio.XmlTextEditor.WinUI;
    class XmlEditorManager
    {
        private static XmlEditorManager sm_instance;
        private XmlEditorSurface m_currentEditorSurface;
        private XmlEditorManager()
        {
            m_currentEditorSurface = null;
        }
        public static XmlEditorManager Instance
        {
            get
            {
                return sm_instance;
            }
        }
        static XmlEditorManager()
        {
            sm_instance = new XmlEditorManager();
        }
        /// <summary>
        /// create or get the m_currentEditorSurface
        /// </summary>
        /// <param name="bench"></param>
        /// <returns></returns>
        internal XmlEditorSurface GetSurface(ICoreWorkbench bench)
        {
            if (m_currentEditorSurface != null)
                return m_currentEditorSurface;
            m_currentEditorSurface = new XmlEditorSurface();
            bench.AddSurface(m_currentEditorSurface,true );
            bench.CurrentSurface = m_currentEditorSurface;
            return m_currentEditorSurface;
        }
    }
}

