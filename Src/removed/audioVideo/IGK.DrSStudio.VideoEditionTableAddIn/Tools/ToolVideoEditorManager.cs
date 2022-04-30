

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: ToolVideoEditorManager.cs
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
file:ToolVideoEditorManager.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace IGK.DrSStudio.VideoEditionTableAddIn.Tools
{
    using IGK.ICore;using IGK.DrSStudio.VideoEditionTableAddIn.WinUI ;
    class ToolVideoEditorManager : VideoToolBase 
    {
        private static ToolVideoEditorManager sm_instance;
        private ToolVideoEditorManager()
        {
        }
        public static ToolVideoEditorManager Instance
        {
            get
            {
                return sm_instance;
            }
        }
        static ToolVideoEditorManager()
        {
            sm_instance = new ToolVideoEditorManager();
        }
        private IGK.DrSStudio.VideoEditionTableAddIn.WinUI.XVideoEditorSurface m_OpenedSurface;
        /// <summary>
        /// get or set the opened surface
        /// </summary>
        public XVideoEditorSurface OpenedSurface
        {
            get { return m_OpenedSurface; }
            internal set
            {
                if (m_OpenedSurface != value)
                {
                    m_OpenedSurface = value;
                }
            }
        }
    }
}

