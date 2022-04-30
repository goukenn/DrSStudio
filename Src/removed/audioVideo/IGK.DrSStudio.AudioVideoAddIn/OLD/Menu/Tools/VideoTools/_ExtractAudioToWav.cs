

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: _ExtractAudioToWav.cs
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
file:_ExtractAudioToWav.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace IGK.DrSStudio.AudioVideo.Menu
{
    [IGK.DrSStudio.Menu.CoreMenu("Tools.Video.ExtractCurrentAudio", 1)]
    /// <summary>
    /// extract audio to wav 
    /// </summary>
    class _ExtractAudioToWav
        : VideoMenuBase
    {
        protected override bool PerformAction()
        {
            switch (this.CurrentSurface.VideoSurfaceType)
            { 
                case IGK.DrSStudio.AudioVideo.WinUI.enuVideoSurfaceType.VideoEditor :
                    break;
                case IGK.DrSStudio.AudioVideo.WinUI.enuVideoSurfaceType .VideoPlayer :
                    using (System.Windows.Forms.SaveFileDialog  ofd = new System.Windows.Forms.SaveFileDialog ())
                    {
                        ofd.Filter = "wav file | *.wav";
                        if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                        {
                            this.CurrentSurface.Stop();
                            IGK.AudioVideo.AVI.AVIFile f = IGK.AudioVideo.AVI.AVIFile.Open(this.CurrentSurface.FileName);
                            if (f != null)
                            {
                                IGK.AudioVideo.AVI.AVIFile.AudioStream aud = f.GetAudioStream();
                                aud.ExportToWaveFile(ofd.Filter, null, false, IntPtr.Zero);
                                f.Close();
                                System.Windows.Forms.MessageBox.Show("OK");
                            }
                            else
                            {
                                System.Windows.Forms.MessageBox.Show("Impossible d'extraire la piste audio");
                            }
                        }
                    }
                    break;
            }
            return base.PerformAction();
        }
    }
}

