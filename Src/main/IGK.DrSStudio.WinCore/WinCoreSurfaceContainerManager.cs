

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: WinCoreSurfaceContainerManager.cs
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
file:WincoreSurfaceContainerManager.cs
*/
using IGK.ICore.WinCore;
using IGK.DrSStudio.WinUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
namespace IGK.DrSStudio
{
    using IGK.DrSStudio.WinUI;
    using IGK.DrSStudio;
    using IGK.ICore;
    using IGK.ICore.WinUI;

    /// <summary>
    /// represent surface manager
    /// </summary>
    public class WinCoreSurfaceContainerManager
    {
        private IXCoreSurfaceContainer c_iXCoreSurfaceContainer;
        private WinCoreLayoutManagerBase m_layoutManager;
        public WinCoreSurfaceContainerManager(WinCoreLayoutManagerBase lmanager, IXCoreSurfaceContainer surfaceContainer)
        {
            if (surfaceContainer == null)
                throw new ArgumentNullException("surfacecontainer");
            if (lmanager == null)
                throw new ArgumentNullException("lmanager");
            this.c_iXCoreSurfaceContainer = surfaceContainer;
            this.m_layoutManager = lmanager;
            this.Init();
        }
        private void Init()
        {
            this.m_layoutManager.Workbench.CurrentSurfaceChanged += Workbench_CurrentSurfaceChanged;
            ICoreSurfaceManagerWorkbench sm = this.m_layoutManager.Workbench as ICoreSurfaceManagerWorkbench;
            if (sm != null)
            {
                sm.SurfaceAdded += Workbench_SurfaceAdded;
                sm.SurfaceRemoved += Workbench_SurfaceRemoved;
            }
            this.m_layoutManager.RegisterToContextMenu(this.c_iXCoreSurfaceContainer as Control);
        }
        void Workbench_SurfaceRemoved(object o, CoreItemEventArgs<ICoreWorkingSurface> e)
        {
            this.c_iXCoreSurfaceContainer.UnregisterSurface(e.Item);
        }
        void Workbench_SurfaceAdded(object o, CoreItemEventArgs<ICoreWorkingSurface> e)
        {
            this.c_iXCoreSurfaceContainer.RegisterSurface(e.Item);
        }
        void Workbench_CurrentSurfaceChanged(object sender, CoreWorkingElementChangedEventArgs<ICoreWorkingSurface> e)
        {
            if (e.NewElement != null)
            {
                this.c_iXCoreSurfaceContainer.RegisterSurface(e.NewElement);
                this.c_iXCoreSurfaceContainer.CurrentSurface = e.NewElement;
            }
            else
                this.c_iXCoreSurfaceContainer.CurrentSurface = null;
        }
    }
}

