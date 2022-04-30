

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: AVVideoMenu.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
/*
IGKDEV - [2008 - 2014]
author: C.A.D . BONDJE DOUE
file:AVVideoMenu.cs
*/
using IGK.ICore;using IGK.DrSStudio.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace IGK.DrSStudio.AudioVideo.Menu.Tools.VideoTools
{
     [IGK.DrSStudio.Menu.CoreMenu("Tools.Video", 400)] 
    class AVVideoMenu : CoreApplicationMenu
    {
         protected override bool IsVisible()
         {
             return IsChildVisible();
         }
         protected override bool IsEnabled()
         {
             return true;
         }
         private bool IsChildVisible()
         {
             foreach (ICoreMenuAction  item in this.Childs)
             {
                 if (item.Visible)
                     return true;
             }
             return false;
         }
         protected override void RegisterBenchEvent(DrSStudio.WinUI.ICoreWorkbench workbench)
         {
             base.RegisterBenchEvent(workbench);
             workbench.CurrentSurfaceChanged += workbench_CurrentSurfaceChanged;
         }
         protected override void UnregisterBenchEvent(DrSStudio.WinUI.ICoreWorkbench workbench)
         {
             workbench.CurrentSurfaceChanged -= workbench_CurrentSurfaceChanged;
             base.UnregisterBenchEvent(workbench);
         }
         void workbench_CurrentSurfaceChanged(object o, CoreWorkingSurfaceChangedEventArgs e)
         {
             this.SetupEnableAndVisibility();
         }
    }
}

