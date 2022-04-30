

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: AudioVideoCompressAVIFileMenu.cs
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
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IGK.DrSStudio.AudioVideo.Drawing2D.Menu
{
    [CoreMenu("Tools.AudioVideo.CompressAudioVideoFile", 0x5001)]
    class AudioVideoCompressAVIFileMenu : CoreApplicationMenu
    {
        protected override bool PerformAction()
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "filter.avifiles".R() + "|*.avi";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    if (AudioVideoBuilder.CompressVideoFile(MainForm.Handle, ofd.FileName, ofd.FileName+".com.avi", true))
                    {
                        MessageBox.Show("Compression OK", "title.compressionResult".R());
                    }
                    else {
                        CoreMessageBox.NotifyMessage("title.CompressionError".R(), "file not compressed");
                    }
                }
            }
            return false;
        }
    }
}
