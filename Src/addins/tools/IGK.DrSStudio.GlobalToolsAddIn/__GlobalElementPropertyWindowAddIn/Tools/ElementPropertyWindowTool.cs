

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: ElementPropertyWindowTool.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
﻿using IGK.ICore.WinCore;
using IGK.ICore;using IGK.DrSStudio.Tools.WinUI;
using IGK.DrSStudio.WinUI;
using IGK.DrSStudio.WinUI.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.DrSStudio.Tools
{
    [CoreTools ("Tools.ElementPropertyWindowMenu")]
    class ElementPropertyWindowTool : CoreToolBase 
    {
        private static ElementPropertyWindowTool sm_instance;
        private ElementPropertyWindowTool()
        {
        }

        public new ElementPropertyWindowGUI HostedControl
        {
            get { return base.HostedControl as ElementPropertyWindowGUI ; }
        }
        public new ICoreWorkingConfigElementSurface CurrentSurface {
            get {
                return base.CurrentSurface as ICoreWorkingConfigElementSurface;
            }
        }
        public static ElementPropertyWindowTool Instance
        {
            get
            {
                return sm_instance;
            }
        }
        static ElementPropertyWindowTool()
        {
            sm_instance = new ElementPropertyWindowTool();
        }
        protected override void GenerateHostedControl()
        {
            base.HostedControl = new ElementPropertyWindowGUI();
        }
        protected override void RegisterBenchEvent(ICoreWorkbench Workbench)
        {
            base.RegisterBenchEvent(Workbench);
            Workbench.CurrentSurfaceChanged += Workbench_CurrentSurfaceChanged;
        }
        protected override void UnregisterBenchEvent(DrSStudio.WinUI.ICoreWorkbench Workbench)
        {
            Workbench.CurrentSurfaceChanged -= Workbench_CurrentSurfaceChanged;
            base.UnregisterBenchEvent(Workbench);
        }

        void Workbench_CurrentSurfaceChanged(object sender, CoreWorkingElementChangedEventArgs<DrSStudio.WinUI.ICoreWorkingSurface> e)
        {
            ICoreWorkingConfigElementSurface s = e.OldElement as ICoreWorkingConfigElementSurface;
            ICoreWorkingConfigElementSurface n = e.NewElement as ICoreWorkingConfigElementSurface;

            if (s != null)
                UnregisterSurfaceEvent(s);
            if (n != null)
                RegisterSurfaceEvent(n);
            this.UpdateView();
        }

        private void UpdateView()
        {
            if ((this.HostedControl !=null) && (this.HostedControl.Visible))
            {
                this.HostedControl.ElementToConfigure =
                    this.CurrentSurface == null ? null :
                    this.CurrentSurface.ElementToConfigure
                    as ICoreWorkingConfigurableObject;
            }
        }

        private void UnregisterSurfaceEvent(ICoreWorkingConfigElementSurface s)
        {
            s.ElementToConfigureChanged -= n_ElementToConfigureChanged;
        }

        private void RegisterSurfaceEvent(ICoreWorkingConfigElementSurface n)
        {
            n.ElementToConfigureChanged += n_ElementToConfigureChanged;
            
        }

        void n_ElementToConfigureChanged(object sender, EventArgs e)
        {
            this.UpdateView();
        }
        

    }
}
