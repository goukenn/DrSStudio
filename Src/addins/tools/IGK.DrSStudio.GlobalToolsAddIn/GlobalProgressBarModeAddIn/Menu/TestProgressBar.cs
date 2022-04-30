

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: TestProgressBar.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
﻿using IGK.ICore.WinCore;
using IGK.ICore;using IGK.DrSStudio.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IGK.DrSStudio.Tools.GlobalProgressBarModeAddIn.Menu
{
    [CoreMenu ("Test_Progress", 100)]
    class TestProgressBar : CoreApplicationMenu
    {
        Timer m_timer;
        int v = 0;
        protected override bool PerformAction()
        {
            if (m_timer == null)
            {

                m_timer = new Timer();
                m_timer.Interval = 10;
                m_timer.Tick += m_timer_Tick;
                m_timer.Enabled = true;
                this.Workbench.BeginJob(this);
            }
            return true;
        }

        void m_timer_Tick(object sender, EventArgs e)
        {
            if (v >= 100)
            {
                this.Workbench.EndJob(this);
                this.m_timer.Enabled = false;
                this.m_timer.Dispose();
                this.m_timer = null;
                v = 0;
                return;
            }
            v++;
            this.Workbench.UpdateJob(this, v);
        }
    }
}
