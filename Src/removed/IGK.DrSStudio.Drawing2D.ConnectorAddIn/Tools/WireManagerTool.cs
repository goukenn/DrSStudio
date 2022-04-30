

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: WireManagerTool.cs
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
file:WireManagerTool.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace IGK.DrSStudio.Drawing2D.Tools
{
    using IGK.ICore;using IGK.DrSStudio.Drawing2D.WinUI;
    using IGK.DrSStudio.Tools;
    using IGK.DrSStudio.WinUI;
    class WireManagerTool : CoreToolBase
    {
        private static WireManagerTool sm_instance;
        private Dictionary<ICore2DDrawingSurface, WireSurfaceManager> m_wireManager;
        private WireManagerTool()
        {
            m_wireManager = new Dictionary<ICore2DDrawingSurface,WireSurfaceManager> ();
        }
        public static WireManagerTool Instance
        {
            get
            {
                return sm_instance;
            }
        }
        static WireManagerTool()
        {
            sm_instance = new WireManagerTool();
        }
        protected override void RegisterBenchEvent(ICoreWorkbench workbench)
        {
            base.RegisterBenchEvent(workbench);
            workbench.SurfaceAdded += new CoreWorkingSurfaceEventHandler(workbench_SurfaceAdded);
            workbench.SurfaceRemoved += new CoreWorkingSurfaceEventHandler(workbench_SurfaceRemoved);
        }
        void workbench_SurfaceRemoved(object o, CoreItemEventArgs<ICoreWorkingSurface> e)
        {
            ICore2DDrawingSurface v_s = e.Surface as ICore2DDrawingSurface;
            if (this.m_wireManager.ContainsKey(v_s))
            {
                this.m_wireManager.Remove(v_s);
            }
        }
        void workbench_SurfaceAdded(object o, CoreItemEventArgs<ICoreWorkingSurface> e)
        {
            ICore2DDrawingSurface v_s = e.Surface as ICore2DDrawingSurface;
            if ((v_s !=null)&&(!this.m_wireManager .ContainsKey (v_s )))
            {
            this.m_wireManager.Add(v_s, new WireSurfaceManager(v_s ));
            }
        }
        internal WireSurfaceManager GetSurfaceManager(ICore2DDrawingSurface surface)
        {
            if (m_wireManager.ContainsKey(surface))
            {
                return m_wireManager[surface];
            }
            WireSurfaceManager c =  new WireSurfaceManager(surface);
            this.m_wireManager.Add(surface, c);
            return c;
        }
    }
}

