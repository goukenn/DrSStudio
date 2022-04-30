

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: AudioVideoPlayerTool.cs
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
file:AudioVideoPlayerTool.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace IGK.DrSStudio.AudioBuilder.Tools
{
    using IGK.ICore;using IGK.DrSStudio.WinUI;
    using IGK.DrSStudio.Tools;
    using IGK.DrSStudio.AudioBuilder.WinUI;
    [CoreTools ("Tools.AudioVideoPlayer")]
    class AudioVideoPlayerTool : CoreToolBase
    {
        private static AudioVideoPlayerTool sm_instance;
        private AudioVideoPlayerTool()
        {
        }
        public static AudioVideoPlayerTool Instance
        {
            get
            {
                return sm_instance;
            }
        }
        static AudioVideoPlayerTool()
        {
            sm_instance = new AudioVideoPlayerTool();
        }
        new XAudioVideoPlayerTool HostedControl {
            get {
                return base.HostedControl as XAudioVideoPlayerTool;
            }
            set {
                base.HostedControl = value;
            }
        }
        public new ICoreWorkingConfigElementSurface CurrentSurface {
            get {
                return base.CurrentSurface as ICoreWorkingConfigElementSurface;
            }
        }
        protected override void GenerateHostedControl()
        {
            this.HostedControl = new XAudioVideoPlayerTool ();
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
            if (e.OldSurface  is ICoreWorkingConfigElementSurface)
            {
                UnRegisterSurfaceEvent(e.OldSurface as ICoreWorkingConfigElementSurface);
            }
            if (e.NewSurface is ICoreWorkingConfigElementSurface)
            {
                RegisterSurfaceEvent(e.NewSurface as ICoreWorkingConfigElementSurface);
            }
        }
        private void UnRegisterSurfaceEvent(ICoreWorkingConfigElementSurface surface)
        {
            surface.ElementToConfigureChanged -= new EventHandler(surface_ElementToConfigureChanged);
        }
        void surface_ElementToConfigureChanged(object sender, EventArgs e)
        {
            this.HostedControl.Track = this.CurrentSurface.ElementToConfigure as ITrack;
        }
        private void RegisterSurfaceEvent(ICoreWorkingConfigElementSurface surface)
        {
            surface.ElementToConfigureChanged += new EventHandler(surface_ElementToConfigureChanged);
        }
    }
}

