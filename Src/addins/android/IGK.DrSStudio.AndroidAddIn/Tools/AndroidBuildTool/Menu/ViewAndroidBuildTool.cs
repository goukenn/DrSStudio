

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: ViewAndroidBuildTool.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
using IGK.ICore.WinCore;
using IGK.ICore;
using IGK.DrSStudio.Android.Tools;
using IGK.DrSStudio.Android.WinUI;


using IGK.ICore.Menu;
using IGK.ICore.WinUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IGK.DrSStudio.Android.Tools.AndroidBuildTool.Menu;

namespace IGK.DrSStudio.Android.Menu
{
    [AndroidViewMenu("AndroidBuildTool", AndroidConstant.ANDROIDVEWINDEX,
        ImageKey=AndroidConstant.ANDROID_IMG_APP_ANDROID)]
    class ViewAndroidBuildTool : CoreViewToolMenuBase 
    {
        new AndroidSurfaceBase CurrentSurface { get { return base.CurrentSurface as AndroidSurfaceBase; } }
        protected override bool IsVisible()
        {
            return base.IsVisible() && (this.CurrentSurface !=null);
        }
        public ViewAndroidBuildTool():base(AndroidBuildTools.Instance)
        {

        }
     

    }
}
