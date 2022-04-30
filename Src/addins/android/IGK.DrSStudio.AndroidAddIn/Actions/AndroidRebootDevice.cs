

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: AndroidRebootDevice.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
﻿using IGK.ICore.WinCore;
using IGK.ICore;using IGK.DrSStudio.Android.Tools;
using IGK.ICore.Actions;
using IGK.ICore.WinUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IGK.DrSStudio.Android.Actions
{
    [CoreAction(AndroidConstant.AC_REBOOTDEVICE)]
    class AndroidRebootDevice : AndroidActionBase 
    {
        protected override bool PerformAction()
        {
            //CoreMessageBox confirm
            if (CoreMessageBox.Confirm("q.android.reboot".R()) == enuDialogResult.Yes)
            {
                Thread th = new Thread(delegate()
                {
                    string response = AndroidTool.Instance.Cmd("adb reboot");
                });
                th.SetApartmentState(ApartmentState.STA);
                th.Start();
            }

            return false;
        }
    }
}
