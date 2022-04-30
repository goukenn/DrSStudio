

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: IGKXWebBrowserManager.cs
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
file:XWebBrowserManager.cs
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
using System.Text;
using System.Windows.Forms;
namespace IGK.DrSStudio.WinUI
{
    /// <summary>
    /// represent custom web browser. that disable some stuff
    /// </summary>
    public class IGKXWebBrowserManager : WebBrowser 
    {
        public IGKXWebBrowserManager()
        {
            this.WebBrowserShortcutsEnabled = false;
            this.AllowWebBrowserDrop = false;
            this.IsWebBrowserContextMenuEnabled = false;
#if DEBUG
            this.ScriptErrorsSuppressed = false ;
#else
            this.ScriptErrorsSuppressed = true;
#endif
            this.ScrollBarsEnabled = false;
            this.SetStyle(ControlStyles.Selectable, false);
            this.TabStop = false;
        }
        protected override void OnGotFocus(EventArgs e)
        {
            base.OnGotFocus(e);
            this.Capture = false;              
        }
    }
}

