

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: VideoActionToolStrip.cs
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
file:VideoActionToolStrip.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace IGK.DrSStudio.VideoEditionTableAddIn.Tools.VideoActionsToolsStrip.WinUI
{
    using IGK.ICore;using IGK.DrSStudio.WinUI ;
    using IGK.DrSStudio.VideoEditionTableAddIn.Tools;
    class VideoActionToolStrip : IGK.DrSStudio.WinUI.XToolStripCoreToolHost
    {
        public VideoActionToolStrip(ToolVideoStripActions tool):base(tool)
        {
            InitControl();
        }
        XToolStripButton c_BuildButton;
        private void InitControl()
        {
            c_BuildButton = new XToolStripButton();
            this.Items.Add(c_BuildButton);
        }
    }
}

