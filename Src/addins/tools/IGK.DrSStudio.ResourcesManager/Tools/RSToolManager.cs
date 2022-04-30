

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: RSToolManager.cs
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
file:RSToolManager.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace IGK.DrSStudio.ResourcesManager.Tools
{
    using IGK.ICore.WinCore;
    using IGK.ICore;
    using IGK.DrSStudio.ResourcesManager.WinUI ;
    using IGK.ICore.Tools;

    [CoreTools (RSConstant.TOOL_NAME)]
    public class RSToolManager : CoreToolBase 
    {
        private static RSToolManager sm_instance;
        private XResourceSurface  m_ResourceSurface;
        public XResourceSurface  ResourceSurface
        {
            get { return m_ResourceSurface; }
            set
            {
                if (m_ResourceSurface != value)
                {
                    m_ResourceSurface = value;
                }
            }
        }
        private RSToolManager()
        {
        }
        public static RSToolManager Instance
        {
            get
            {
                return sm_instance;
            }
        }
        static RSToolManager()
        {
            sm_instance = new RSToolManager();
        }
        /// <summary>
        /// create a new surface
        /// </summary>
        internal void CreateSurface()
        {
            this.m_ResourceSurface = new XResourceSurface();
            this.m_ResourceSurface.Disposed += new EventHandler(m_ResourceSurface_Disposed);
        }
        void m_ResourceSurface_Disposed(object sender, EventArgs e)
        {
            this.m_ResourceSurface = null;
        }
    }
}

