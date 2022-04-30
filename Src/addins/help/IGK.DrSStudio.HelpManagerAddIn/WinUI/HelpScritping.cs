

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: HelpScritping.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/

using IGK.ICore;using IGK.ICore.WinUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace IGK.DrSStudio.HelpManagerAddIn.WinUI
{
    [ComVisible(true)]
    public class HelpScritping
    {
        private HelpControlGUI helpControlGUI;

        public HelpScritping(HelpControlGUI helpControlGUI)
        {
            
            this.helpControlGUI = helpControlGUI;
        }
        public HelpScritping()
        {

        }

        public void jsShowMsgbox(string message)
        {
            CoreMessageBox.Show(message);
        }
    }
}
