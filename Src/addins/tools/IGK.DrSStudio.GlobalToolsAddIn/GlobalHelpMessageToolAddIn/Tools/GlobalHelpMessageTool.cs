

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: GlobalHelpMessageTool.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
﻿using IGK.ICore.WinCore;
using IGK.ICore;using IGK.DrSStudio.WinUI;
using IGK.ICore.Tools;
using IGK.ICore.WinUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.DrSStudio.Tools
{
    [CoreTools("Tool.GlobalHelpMessageTool")]
    public class GlobalHelpMessageTool : 
        CoreToolBase ,
        ICoreWorkbenchHelpMessageListener
    {
        private static GlobalHelpMessageTool sm_instance;
        private IGKWinCoreStatusTextItem m_textLabel;
        private GlobalHelpMessageTool()
        {
        }

        public static GlobalHelpMessageTool Instance
        {
            get
            {
                return sm_instance;
            }
        }
        static GlobalHelpMessageTool()
        {
            sm_instance = new GlobalHelpMessageTool();
        }
        protected override void GenerateHostedControl()
        {
            this.HostedControl = null;
            this.m_textLabel = new IGKWinCoreStatusTextItem();
            this.m_textLabel.Spring = true;
            this.m_textLabel.Text = "tip.Help".R();
            this.m_textLabel.Index = 0;
            this.Workbench.GetLayoutManager()?.StatusControl.Items.Add(this.m_textLabel);
            
        }
        protected override void RegisterBenchEvent(ICoreSystemWorkbench workbench)
        {
            base.RegisterBenchEvent(workbench);
            var v = (workbench as ICoreHelpWorkbench);
                if (v!=null)
                    v.SetHelpMessageListener (this);
        }
        protected override void UnregisterBenchEvent(ICoreSystemWorkbench Workbench)
        {
            base.UnregisterBenchEvent(Workbench);
        }

        void Workbench_HelpMessageChanged(object sender, EventArgs e)
        {
            
        }


        public void SendMessage(string message)
        {
            this.m_textLabel.Text = message;
        }
    }
}
