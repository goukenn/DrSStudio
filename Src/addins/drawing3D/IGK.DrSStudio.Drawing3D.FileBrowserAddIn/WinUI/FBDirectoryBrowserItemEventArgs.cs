

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: DirectoryBrowserItemEventArgs.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
/*
IGKDEV @ 2008-2016
author: C.A.D . BONDJE DOUE
file:DirectoryBrowserItemEventArgs.cs
*/
/* 
-------------------------------------------------------------------
Company: IGK-DEV
Author : C.A.D. BONDJE DOUE
SITE : http://www.igkdev.be
Application : DrSStudio
powered by IGK - DEV &copy; 2008-2012
THIS FILE IS A PART OF THE DRSSTUDIO APPLICATION. SEE "License.txt"
FOR MORE INFORMATION ABOUT THE LICENSE
------------------------------------------------------------------- 
*/
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace IGK.DrSStudio.Drawing3D.FileBrowser.WinUI
{
    public delegate void FBDirectoryBrowserItemEventHandler(Object sender, FBDirectoryBrowserItemEventArgs e);
    public class FBDirectoryBrowserItemEventArgs : EventArgs 
    {
        private IDirectoryBrowserItem m_Item;
        public IDirectoryBrowserItem Item
        {
            get { return m_Item; }
        }
        public FBDirectoryBrowserItemEventArgs(IDirectoryBrowserItem item)
        {
            this.m_Item = item;
        }
    }
}

