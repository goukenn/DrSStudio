

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: AndroidBuildAction.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
﻿using IGK.ICore.WinCore;
using IGK.ICore;
using IGK.ICore.Actions;
using IGK.ICore.WinUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.DrSStudio.Android.Actions
{
    [CoreAction(AndroidConstant.AC_BUILDPROJECT)]
    class AndroidBuildAction : AndroidActionBase
    {
        protected override bool PerformAction()
        {
            if (this.CurrentProject != null)
            {
                ICoreApplicationWorkbench bench = CoreSystem.GetWorkbench<ICoreApplicationWorkbench>();
                if (bench !=null)                
                 bench.MainForm.SetCursor(System.Windows.Forms.Cursors.WaitCursor);
               bool v_r =   this.CurrentProject.Build();
                if (bench !=null)
                    bench.MainForm.SetCursor(System.Windows.Forms.Cursors.Default);
                if (v_r == false)
                {
                    CoreMessageBox.Show("msg.android.compilation.failed".R());
                }
                else      {
                    CoreMessageBox.Show("msg.android.compilation.succeed".R());
                }
            }
            return false;
        }
    }
}
