

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: XResourcesItemChangedEventArgs.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace IGK.DrSStudio.ResourcesManager.WinUI
{
    class XResourcesItemChangedEventArgs : EventArgs 
    {
        private System.Windows.Forms.ListViewItem listViewItem;
        private System.Windows.Forms.ListViewItem.ListViewSubItem listViewSubItem;
        public ListViewItem ListViewItem { get { return this.listViewItem; } }
        public ListViewItem.ListViewSubItem SubItem { get { return this.listViewSubItem;  } }
        public XResourcesItemChangedEventArgs(System.Windows.Forms.ListViewItem listViewItem, 
            System.Windows.Forms.ListViewItem.ListViewSubItem listViewSubItem)
        {
            this.listViewItem = listViewItem;
            this.listViewSubItem = listViewSubItem;
        }
    }
}
