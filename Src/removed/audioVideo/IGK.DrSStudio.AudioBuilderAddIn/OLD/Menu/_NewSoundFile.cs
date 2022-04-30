

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: _NewSoundFile.cs
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
file:_NewSoundFile.cs
*/
using System;
using System.Collections.Generic;
using System.Text;
namespace IGK.DrSStudio.AudioBuilder.Menu
{
    using IGK.ICore;using IGK.DrSStudio.AudioBuilder.Tools;
    using IGK.DrSStudio.AudioBuilder.WinUI;
    //audio start with index 20
    [IGK.DrSStudio.Menu.CoreMenu("File.New.Audio", 20)]
    sealed class _NewSoundFile : IGK.DrSStudio.Menu.CoreApplicationMenu 
    {
        public _NewSoundFile()
        {
        }
        WinUI.XAudioBuilderSurface  surface;
        protected override bool PerformAction()
        {
            //check for aleady opened surface 
            if (surface == null)
            {
                surface = new XAudioBuilderSurface ();
                this.Workbench.Surfaces.Add(surface);
                surface.Disposed += new EventHandler(surface_Disposed);
            }
            else {
                this.Workbench.CurrentSurface = surface;
            }
            return false;
        }
        void surface_Disposed(object sender, EventArgs e)
        {
            this.surface = null;
        }
    }
}

