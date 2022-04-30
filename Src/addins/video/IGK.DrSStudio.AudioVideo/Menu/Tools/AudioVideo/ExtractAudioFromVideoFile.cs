

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: ExtractAudioFromVideoFile.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/

using IGK.ICore;
using IGK.ICore.Codec;
using IGK.ICore.Menu;
using IGK.ICore.WinUI;
using IGK.ICore.Drawing2D;
using IGK.ICore.Drawing2D.Menu;
using IGK.ICore.Drawing2D.WinUI;





﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IGK.DrSStudio.AudioVideo.Menu.Tools.AudioVideo
{
    [CoreMenu("Tools.AudioVideo.ExtractAudioFromFile", 0x100)]
    class ExtractAudioFromVideoFile : CoreApplicationMenu
    {
        protected override bool PerformAction()
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Title = "title.extractaudio".R();
                ofd.Filter = "Videos files | *.avi;*.divx;*.mp4;*.mov;*.mpeg | all files|*.*";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    using (SaveFileDialog sfd = new SaveFileDialog())
                    {
                        sfd.Title = "title.destination".R();
                        sfd.Filter = "audio files|*.mp3;*.wav;";
                        if (sfd.ShowDialog() == DialogResult.OK)
                        {
                            Stream sm = File.Create(sfd.FileName);
                            var v =IGK.AVIApi.AVIStreamUtils.ExtractAudioData(ofd.FileName,
                                sm);
                            
                            sm.Flush();
                            sm.Close();
                            if (!v)
                            {
                                try
                                {
                                    File.Delete(sfd.FileName);
                                }
                                catch
                                {
                                }                         
                          
                                CoreMessageBox.Show("err.aviaddin.cantextractaudio".R());
                            }
                        }
                    }
                    

                }
            }
            return false;
        }
    }
}
