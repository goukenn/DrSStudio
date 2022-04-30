

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: _LoadAudioMenu.cs
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
file:_LoadAudioMenu.cs
*/
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
namespace IGK.DrSStudio.AudioBuilder.Menu
{
    [IGK.DrSStudio.Menu.CoreMenu("AudioAction.LoadAudio", 0)]
    class _LoadAudioMenu : AudioMenuBase 
    {
        protected override bool PerformAction()
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter  = "Audio Files | *.mp3;*.wav;*.avi;";
                ofd.FileName = "AudioFile.wav";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    this.CurrentSurface.OpenFile (ofd.FileName);
                }
            }
            return false;
        }
    }
}

