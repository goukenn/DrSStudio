

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: AudioMenuBase.cs
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
file:AudioMenuBase.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace IGK.DrSStudio.AudioBuilder.Menu
{
    /// <summary>
    /// represet the default acion audio action menu
    /// </summary>
    public class AudioMenuBase : IGK.DrSStudio.Menu.CoreApplicationMenu 
    {
        public new WinUI.XAudioBuilderSurface CurrentSurface {
            get {
                return base.CurrentSurface as WinUI.XAudioBuilderSurface;
            }
        }
        protected override bool IsVisible()
        {
            return (this.CurrentSurface != null);
        }
        protected override bool IsEnabled()
        {
            return (this.CurrentSurface != null);
        }
        protected override void InitMenu()
        {
            base.InitMenu();
            this.Visible = false;
            this.Enabled = false;
        }
        protected override void RegisterBenchEvent(IGK.DrSStudio.WinUI.ICoreWorkbench workbench)
        {
            base.RegisterBenchEvent(workbench);
            workbench.CurrentSurfaceChanged += new CoreWorkingSurfaceChangedEventHandler(workbench_CurrentSurfaceChanged);
        }
        protected override void UnregisterBenchEvent(IGK.DrSStudio.WinUI.ICoreWorkbench workbench)
        {
            workbench.CurrentSurfaceChanged -= new CoreWorkingSurfaceChangedEventHandler(workbench_CurrentSurfaceChanged);
            base.UnregisterBenchEvent(workbench);
        }
        void workbench_CurrentSurfaceChanged(object o, CoreWorkingSurfaceChangedEventArgs e)
        {
            this.Visible = this.IsVisible();
            this.Enabled = this.IsEnabled();
        }
    }
}

