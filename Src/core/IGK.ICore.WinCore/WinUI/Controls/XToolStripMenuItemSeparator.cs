

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: XToolStripMenuItemSeparator.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
using IGK.ICore.WinCore;
using IGK.ICore;using IGK.ICore.WinUI;
/*
IGKDEV @ 2008-2016
author: C.A.D . BONDJE DOUE
file:XToolStripMenuItemSeparator.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IGK.ICore.WinUI
{
    /// <summary>
    /// represent a tool strip separator of menu stip
    /// </summary>
    public class XToolStripMenuItemSeparator : ToolStripSeparator, IXCoreMenuItemSeparator
    {
        public XToolStripMenuItemSeparator()
        {            
        }

        public event EventHandler DropDownClosed;

        public new ICoreMenu Owner
        {
            get { return base.Owner as ICoreMenu; }
        }

        private void OnDropDownClosed(EventArgs e) {
            if (DropDownClosed != null)
                DropDownClosed(this, e);
        }
    }
}

