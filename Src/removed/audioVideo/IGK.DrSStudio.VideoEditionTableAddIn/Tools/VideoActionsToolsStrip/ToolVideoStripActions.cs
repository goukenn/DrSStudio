

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: ToolVideoStripActions.cs
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
file:ToolVideoStripActions.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace IGK.DrSStudio.VideoEditionTableAddIn.Tools.VideoActionsToolsStrip
{
    using IGK.ICore;using IGK.DrSStudio.VideoEditionTableAddIn.WinUI;
    [CoreTools ("Tool.VideoActions")]
    class ToolVideoStripActions : VideoToolBase 
    {
        private static ToolVideoStripActions sm_instance;
        private ToolVideoStripActions()
        {
        }
        public static ToolVideoStripActions Instance
        {
            get
            {
                return sm_instance;
            }
        }
        static ToolVideoStripActions()
        {
            sm_instance = new ToolVideoStripActions();
        }
        protected override void GenerateHostedControl()
        {
            this.HostedControl = new WinUI.VideoActionToolStrip(this);
        }
    }
}

