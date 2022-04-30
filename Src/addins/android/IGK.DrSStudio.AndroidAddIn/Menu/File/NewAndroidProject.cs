

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: NewAndroidProject.cs
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
file:NewAndroidProject.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace IGK.DrSStudio.Android.Menu.File
{

    
using IGK.ICore;
    using IGK.ICore.Menu;
    using IGK.ICore.WinUI;
    using System.Windows.Forms;
    [CoreMenu("File.New.Android.NewProject", 300, ImageKey = "app_android_proj")]
    sealed class NewAndroidProject : CoreApplicationMenu
    {
        protected override bool PerformAction()
        {
            //start wizard
           // AndroidSetting.Instance.JavaSDK
            using (AndroidProjectWizard pWizzard = new AndroidProjectWizard())
            {
                if (pWizzard.RunConfigurationWizzard(this.Workbench) == enuDialogResult.OK)
                {
                    IGK.DrSStudio.Android.WinUI.AndroidProjectEditorSurface c = pWizzard.Surface
                        as IGK.DrSStudio.Android.WinUI.AndroidProjectEditorSurface;
                    this.Workbench.AddSurface (c,true );
                }
            }
            return false;
        }
    }
}

