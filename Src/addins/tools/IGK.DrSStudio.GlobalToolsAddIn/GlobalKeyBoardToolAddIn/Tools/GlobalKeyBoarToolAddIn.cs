

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: GlobalKeyBoarToolAddIn.cs
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
    [CoreTools("Tool.GlobalKeyBoarToolAddIn")]
    class GlobalKeyBoarToolAddIn : CoreToolBase 
    {
        private static GlobalKeyBoarToolAddIn sm_instance;
        private GlobalKeyBoarToolAddIn()
        {
        }

        public static GlobalKeyBoarToolAddIn Instance
        {
            get
            {
                return sm_instance;
            }
        }
        static GlobalKeyBoarToolAddIn()
        {
            sm_instance = new GlobalKeyBoarToolAddIn();
        }
        private IGKWinCoreStatusTextItem m_textLabel;

        protected override void GenerateHostedControl()
        {
            var l = this.Workbench.GetLayoutManager();
            if (l != null)
            {
                this.m_textLabel = new IGKWinCoreStatusTextItem
                {
                    Bounds = new Rectanglef(0, 0, 100, 0)
                };
                l.StatusControl.Items.Add(this.m_textLabel);
            }
        }

        private void UpdateText()
        {
            
        }
    }
}
