

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: HistoryToolManager.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
using IGK.ICore.WinCore;
using IGK.ICore;
using IGK.ICore.Menu;
using IGK.ICore.WinUI;
using IGK.ICore.Drawing2D;
using IGK.ICore.History;
using IGK.ICore.Drawing2D.Menu;
using IGK.ICore.Drawing2D.WinUI;
using IGK.DrSStudio;
using IGK.DrSStudio.Menu;
using IGK.DrSStudio.WinUI;
using IGK.DrSStudio.Drawing2D;
using IGK.DrSStudio.Drawing2D.WinUI;
﻿using IGK.ICore.Actions;

using IGK.ICore.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IGK.DrSStudio.Drawing2D.HistoryActions;
using IGK.ICore.Drawing2D.Tools;

namespace IGK.DrSStudio.Drawing2D.Tools
{
    /// <summary>
    /// represent a default manager action tool
    /// </summary>
    [CoreTools("Tool.Drawing2D.HistoryManagerTools")]
    public sealed class HistoryToolManager : Core2DDrawingToolBase 
    {
        private static HistoryToolManager sm_instance;

        private HistoryToolManager()
        {
        }

        public static HistoryToolManager Instance
        {
            get
            {
                return sm_instance;
            }
        }
        static HistoryToolManager()
        {
            sm_instance = new HistoryToolManager();
        }
        protected override void RegisterBenchEvent(ICoreSystemWorkbench Workbench)
        {
            base.RegisterBenchEvent(Workbench);

            ICoreActionManagerWorkbench v = Workbench as ICoreActionManagerWorkbench;
            if (v != null)
            {
                v.ActionPerformed += _ActionPerformed;
            }
        }

        private void _ActionPerformed(object sender, EventArgs e)
        {
            ICoreUndoAndRedoAction ack = sender as ICoreUndoAndRedoAction;
            if ((ack!=null ) && HistoryChainManagerTool.Instance.Chains.Enabled(typeof(HistoryChainActionItem)))
            {

                HistorySurfaceManager.Instance.ActionsList.Add(new Drawing2DHistoryActionPerformed(ack));
            }
        }
        protected override void UnregisterBenchEvent(ICoreSystemWorkbench Workbench)
        {
            ICoreActionManagerWorkbench v = Workbench as ICoreActionManagerWorkbench;
            if (v != null)
            {
                v.ActionPerformed -= _ActionPerformed;
            }
            base.UnregisterBenchEvent(Workbench);
        }
    }
}
