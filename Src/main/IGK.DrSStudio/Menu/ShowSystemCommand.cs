

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: ShowSystemCommand.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
﻿using IGK.ICore.WinCore;
using IGK.ICore;using IGK.ICore.Windows.Native;
using IGK.ICore.WinUI;
using IGK.Native;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IGK.ICore.Menu;

namespace IGK.DrSStudio.Menu
{
    [DrSStudioMenu("File.ShowSystemCommand", -10, IsVisible=false , Shortcut = enuKeys.Alt | enuKeys.Space )]
    public sealed class ShowSystemCommand : CoreApplicationMenu 
    {
        public ShowSystemCommand()
{

        }
        protected override void InitMenu()
        {
            base.InitMenu();
        }
        protected override bool PerformAction()
        {
            User32.SendMessage(this.MainForm.Handle,
                User32.WM_SYSCOMMAND ,
                IntPtr.Zero ,
                IntPtr.Zero );
            
            return false;
        }
    }
}
