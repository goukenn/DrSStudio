

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: GlobalSolutionExplorerTool.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
﻿using IGK.ICore.WinCore;
using IGK.ICore;
using IGK.ICore.Settings;
using IGK.ICore.WinUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IGK.DrSStudio.Tools
{
    using IGK.ICore.Tools;
    using IGK.DrSStudio.WinUI;
    using IGK.DrSStudio.Settings;
    /// <summary>
    /// solution explorer . manage solution in worbench
    /// </summary>
    [CoreTools("Tool.GlobalSolutionExplorer",
        ImageKey=CoreImageKeys.TOOL_GLOBAL_SOLUTION_GKDS)]
    public class GlobalSolutionExplorerTool : CoreToolBase 
    {
        private static GlobalSolutionExplorerTool sm_instance;

        public new ICoreWorkingProjectSolutionSurface CurrentSurface {
            get {
                return base.CurrentSurface as ICoreWorkingProjectSolutionSurface;
            }
        }

        private GlobalSolutionExplorerTool()
        {
        }

        public static GlobalSolutionExplorerTool Instance
        {
            get
            {
                return sm_instance;
            }
        }
        static GlobalSolutionExplorerTool()
        {
            sm_instance = new GlobalSolutionExplorerTool();

        }

        public new GlobalSolutionExplorerGUI HostedControl
        {
            get {
                return base.HostedControl as GlobalSolutionExplorerGUI;
            }
        }
        protected override void GenerateHostedControl()
        {
            GlobalSolutionExplorerGUI c = new GlobalSolutionExplorerGUI(this)
            {
                CaptionKey = this.Id
            };
            base.HostedControl = c;
            c.ContextMenuStrip = this.MainForm.AppContextMenu
                as ContextMenuStrip;

        }

        protected override void RegisterBenchEvent(ICoreSystemWorkbench Workbench)
        {
            base.RegisterBenchEvent(Workbench);
            if (Workbench is ICoreWorkingSurfaceHandler s)
                s.CurrentSurfaceChanged += _CurrentSurfaceChanged;
        }
        protected override void UnregisterBenchEvent(ICoreSystemWorkbench Workbench)
        {
            if (Workbench is ICoreWorkingSurfaceHandler s)
                s.CurrentSurfaceChanged -= _CurrentSurfaceChanged;
            base.UnregisterBenchEvent(Workbench);
        }
        
        void _CurrentSurfaceChanged(object sender, CoreWorkingElementChangedEventArgs<ICoreWorkingSurface> e)
        {

            var s = this.CurrentSurface;
            if ((s != null) && (s.Solution!=null))
            {
                var solution = s.Solution;
                SolutionExplorerSetting.Instance.Solutions.Add(solution.Name, solution.FileName);
                this.HostedControl.Solution = s.Solution;
                
            }
            else
            {
                this.HostedControl.Solution = null;

            }
        }
      
        /// <summary>
        /// opent a solution item
        /// </summary>
        /// <param name="item"></param>
        internal void Open(ICoreWorkingProjectSolutionItem item)
        {
            
            if (item.Solution!=null)
                item.Solution.Open(this.Workbench, item);
            else
                item.Open(this.Workbench);
        }
    }
}
