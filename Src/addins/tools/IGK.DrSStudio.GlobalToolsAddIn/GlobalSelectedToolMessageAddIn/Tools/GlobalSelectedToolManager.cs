

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: GlobalSelectedToolManager.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
﻿using IGK.ICore.WinCore;
using IGK.ICore;
using IGK.ICore.WinUI;
using System; 
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.DrSStudio.Tools
{
    using IGK.ICore.Tools;
    using IGK.DrSStudio.WinUI;

    [CoreTools("Tool.GlobalSelectedToolManager")]
    sealed class GlobalSelectedToolManager : CoreToolBase 
    {
        private IGKWinCoreStatusTextItem m_textLabel;
        private static GlobalSelectedToolManager sm_instance;
        private GlobalSelectedToolManager()
        {
        }

        public static GlobalSelectedToolManager Instance
        {
            get
            {
                return sm_instance;
            }
        }
        protected override void GenerateHostedControl()
        {
            this.HostedControl = null;
            this.m_textLabel = new IGKWinCoreStatusTextItem();            
            this.m_textLabel.Bounds = new Rectanglef(0, 0, 120, 0);
            this.m_textLabel.Index = 0x15;
            this.m_textLabel.Autosize = true;

            this.Workbench.GetLayoutManager().StatusControl.Items.Add(this.m_textLabel);
        }
        static GlobalSelectedToolManager()
        {
            sm_instance = new GlobalSelectedToolManager();
        }
        public new ICoreWorkingToolManagerSurface CurrentSurface {
            get {
                return base.CurrentSurface as ICoreWorkingToolManagerSurface;
            }
        }
        
        protected override void RegisterBenchEvent(ICoreSystemWorkbench Workbench)
        {
            base.RegisterBenchEvent(Workbench);
            if (Workbench is ICoreWorkingSurfaceHandler s)
                s.CurrentSurfaceChanged += Workbench_CurrentSurfaceChanged;
        }
        protected override void UnregisterBenchEvent(ICoreSystemWorkbench Workbench)
        {
            if (Workbench is ICoreWorkingSurfaceHandler s)
                s.CurrentSurfaceChanged -= Workbench_CurrentSurfaceChanged;
            base.UnregisterBenchEvent(Workbench);
            
        }

        void Workbench_CurrentSurfaceChanged(object sender, CoreWorkingElementChangedEventArgs<ICoreWorkingSurface> e)
        {
            this.m_textLabel.Visible = false;
            if (e.OldElement is ICoreWorkingToolManagerSurface)
                UnRegisterSurfaceEvent(e.OldElement as ICoreWorkingToolManagerSurface);
            if (e.NewElement is ICoreWorkingToolManagerSurface)
                RegisterSurfaceEvent(e.NewElement as ICoreWorkingToolManagerSurface);
            
            
        }

        private void RegisterSurfaceEvent(ICoreWorkingToolManagerSurface s)
        {
            s.CurrentToolChanged += s_CurrentToolChanged;
            this.UpdateToolName();
            this.m_textLabel.Visible = true;
        }

        void s_CurrentToolChanged(object sender, EventArgs e)
        {
            this.UpdateToolName();
        }

        private void UpdateToolName()
        {
            this.m_textLabel.Bounds = new Rectanglef(0, 0, 220, 0);
            this.m_textLabel.Text ='@'+ CoreWorkingObjectAttribute.GetObjectName (CurrentSurface.CurrentTool);
            
            
        }

        private void UnRegisterSurfaceEvent(ICoreWorkingToolManagerSurface s)
        {
            
            s.CurrentToolChanged -= s_CurrentToolChanged;
        }
        
    }
}
