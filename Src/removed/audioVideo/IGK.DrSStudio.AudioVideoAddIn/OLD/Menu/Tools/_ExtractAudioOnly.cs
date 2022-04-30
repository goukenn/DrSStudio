

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: _ExtractAudioOnly.cs
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
file:_ExtractAudioOnly.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
namespace IGK.DrSStudio.AudioVideo.Menu.Tools
{
    [IGK.DrSStudio.Menu.CoreMenu ("Tools.ExtractAudio", 500)]
    class _ExtractAudioOnly : VideoMenuBase 
    {
        protected override bool PerformAction()
        {
                IGK.DrSStudio.AudioVideo.WinUI.XVideoSurfaceEditor vid =
                    this.CurrentSurface as IGK.DrSStudio.AudioVideo.WinUI.XVideoSurfaceEditor;
                if (vid == null)
                    return false;
                IGK.AudioVideo.AVI.AVIFile.AudioStream aud = vid.AviFile.GetAudioStream();
                using (SaveFileDialog sfd = new SaveFileDialog())
                {
                    sfd.Title = "Export audio to wav";
                    sfd.Filter = "Wave audio|*.wav";
                    if (sfd.ShowDialog() == DialogResult.OK)
                    {
                        aud.Save(sfd.FileName,true, null, IntPtr.Zero );
                    }
                }
                aud.Dispose();
            return base.PerformAction();
        }
    }
}

