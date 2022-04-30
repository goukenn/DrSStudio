

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: _ExtractAudio.cs
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
file:_ExtractAudio.cs
*/
/* 
-------------------------------------------------------------------
Company: IGK-DEV
Author : C.A.D. BONDJE DOUE
SITE : http://www.igkdev.be
Application : DrSStudio
powered by IGK - DEV &copy; 2008-2012
THIS FILE IS A PART OF THE DRSSTUDIO APPLICATION. SEE "License.txt"
FOR MORE INFORMATION ABOUT THE LICENSE
------------------------------------------------------------------- 
*/
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
namespace IGK.DrSStudio.AudioVideo.Menu.Tools
{
    using IGK.ICore;using IGK.AudioVideo.AVI ;
    [IGK.DrSStudio.Menu.CoreMenu("Tools.Video.ExtractAudio", 0)]
    class _ExtractAudio : IGK.DrSStudio.Menu.CoreApplicationMenu 
    {
        protected override bool PerformAction()
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = CoreSystem.GetString("Filter.Video") + "|*.avi";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    AVIFile v_f = AVIFile.Open(ofd.FileName);
                            if (v_f != null)
                            {
                                using (SaveFileDialog sfd = new SaveFileDialog())
                                {
                                    sfd.Filter = CoreSystem.GetString("Filter.Vid.ExtractWAV") + "|*.wav";
                                    if (sfd.ShowDialog() == DialogResult.OK)
                                    {
                                            using (AVIFile.AudioStream v_audio = v_f.GetAudioStream())
                                            {
                                                if (v_audio != null)
                                                {
                                                    if (v_audio.ExportToWaveFile(sfd.FileName, null, false, IntPtr.Zero))
                                                    {
                                                        MessageBox.Show("Extraction audio Terminer", "Exraction Audio");
                                                    }
                                                }
                                            }
                                    }
                                }
                    }
                }
            }
            return base.PerformAction();
        }
    }
}

