

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: GlobalProgressBarAddIn.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
﻿using IGK.ICore.WinCore;
using IGK.ICore;using IGK.ICore.WinUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IGK.ICore.Tools;
using IGK.DrSStudio.WinUI;

namespace IGK.DrSStudio.Tools
{
    [CoreTools("Tool.GlobalProgressBarTool")]
    class GlobalProgressBarAddIn : CoreToolBase 
    {
        IGKWinCoreStatusProgressBar m_progressLabel;
        private static GlobalProgressBarAddIn sm_instance;
        private GlobalProgressBarAddIn()
        {
        }

        public static GlobalProgressBarAddIn Instance
        {
            get
            {
                return sm_instance;
            }
        }
        static GlobalProgressBarAddIn()
        {
            sm_instance = new GlobalProgressBarAddIn();

        }
        protected override void GenerateHostedControl()
        {
            this.HostedControl = null;
            this.m_progressLabel = new IGKWinCoreStatusProgressBar();
            this.m_progressLabel.Bounds = new Rectanglef(0, 0, 120, 0);
            this.m_progressLabel.Index = 0x2;
            this.m_progressLabel.Visible = false;
            this.Workbench.GetLayoutManager()?.StatusControl.Items.Add(this.m_progressLabel);
        }

        protected override void RegisterBenchEvent(ICoreSystemWorkbench workbench)
        {
            base.RegisterBenchEvent(workbench);

            ICoreJobManagerWorckbench v = workbench as ICoreJobManagerWorckbench;
            if (v != null)
            {
                v.JobStart += Workbench_JobStart;
                v.JobComplete += Workbench_JobComplete;
                v.JobProgress += Workbench_JobProgress;
            }
        }
        protected override void UnregisterBenchEvent(ICoreSystemWorkbench workbench)
        {
            ICoreJobManagerWorckbench v = workbench as ICoreJobManagerWorckbench;
            if (v != null)
            {
                v.JobStart -= Workbench_JobStart;
                v.JobComplete -= Workbench_JobComplete;
                v.JobProgress -= Workbench_JobProgress;
            }
            base.UnregisterBenchEvent(workbench);

        }

        void Workbench_JobProgress(object obj, int value)
        {
            this.m_progressLabel.Value = value;
        }

        void Workbench_JobComplete(object sender, EventArgs e)
        {
            this.m_progressLabel.Visible = false;
            this.m_progressLabel.Value = 0;
        }

        void Workbench_JobStart(object sender, EventArgs e)
        {
            this.m_progressLabel.Value = 0;
            this.m_progressLabel.Visible = true;
        }
    }
}
