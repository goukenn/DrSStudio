

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: AndroidBuildToolGUI.cs
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
using System.Threading.Tasks;

namespace IGK.DrSStudio.Android.WinUI
{


    using IGK.ICore;
    using IGK.ICore.Resources;
    using IGK.ICore.WinCore.WinUI.Controls;
    using IGK.ICore.WinUI;

    public class AndroidBuildToolGUI : IGKXToolStripCoreToolHost
    {
        public AndroidBuildToolGUI()
        {
            this.InitControl();
        }
        public override void LoadDisplayText()
        {
            base.LoadDisplayText();
        }
        void InitControl()
        {
            //create the release
            this.Items.Add(new IGKXToolStripButton() {
                Action = CoreSystem.GetAction(AndroidConstant.AC_BUILDPROJECT), 
                ImageDocument=CoreResources.GetDocument(AndroidConstant.ANDROID_IMG_APP_ANDROID ),
                ToolTipKey = "tip.buildandroidproject-unsignedrelease"
            });
            this.Items.Add(new IGKXToolStripButton()
            {
                Action = CoreSystem.GetAction(AndroidConstant.AC_BUILDANDRUNPROJECT),
                ImageDocument = CoreResources.GetDocument("android_img_buildandrun"),
                ToolTipKey = "tip.buildandrunandroidproject-unsignedrelease"
            });

            this.Items.Add(new IGKXToolStripButton()
            {
                Action = CoreSystem.GetAction(AndroidConstant.AC_BUILDEBUGPROJECT),
                ImageDocument = CoreResources.GetDocument("android_img_debug"),
                ToolTipKey = "tip.builddebugandroidproject"
            });
            this.Items.Add(new IGKXToolStripButton()
            {
                Action = CoreSystem.GetAction(AndroidConstant.AC_BUILDEBUGANDRUNPROJECT),
                ImageDocument = CoreResources.GetDocument("android_img_debugandrun"),
                ToolTipKey = "tip.builddebugandrunAndroidproject"
            });
            this.Items.Add(new IGKXToolStripButton()
            {
                Action = CoreSystem.GetAction(AndroidConstant.AC_REBOOTDEVICE),
                ImageDocument = CoreResources.GetDocument("android_img_poweroff"),
                ToolTipKey = "tip.rebootdevice"
            });
        }
    }
}
