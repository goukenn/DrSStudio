

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: AudioVideoSelectVideoEncoder.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/


﻿
using IGK.DrSStudio.AudioVideo.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IGK.DrSStudio.AudioVideo.Menu.Tools
{
    using IGK.ICore;
    using IGK.ICore.Codec;
    using IGK.ICore.Menu;
    using IGK.ICore.WinUI;
    using IGK.ICore.Drawing2D;
    using IGK.ICore.Drawing2D.Menu;
    using IGK.ICore.Drawing2D.WinUI;

    [CoreMenu("Tools.AudioVideo.ChooseDefaultVideoEncoder", 0x5003)]
    class AudioVideoSelectVideoEncoder : CoreApplicationMenu
    {
        protected override bool PerformAction()
        {
            Workbench.ConfigureWorkingObject(AudioVideoCodecTool.Instance,
                "title.chooseVideoEndoder".R(),
                false,
                Size2i.Empty);

            var c = AudioVideoCodecTool.Instance.SelectedCodec;
            if (c != null)
            {
                c.Open(AVIApi.VCM.enuVCMLocateFlags.Compress);
                if (c.CanConfigure)
                    c.Configure(MainForm.Handle);

                   using (OpenFileDialog ofd = new OpenFileDialog())
                    {
                        ofd.Filter = "filter.avifiles".R() + "|*.avi";
                        if (ofd.ShowDialog() == DialogResult.OK)
                        {
                            AVIApi.AVI.AVIFile f = AVIApi.AVI.AVIFile.Open(ofd.FileName);
                            if (f != null)
                            {
                                var vid = f.GetVideoStream();
                                if (vid != null)
                                {
                                    var rvid = c.Compress(vid, null);

                                    //
                                    AVIApi.AVI.AVIFile vf = AVIApi.AVI.AVIFile.CreateFile(CoreConstant.DebugTempFolder+"\\output.avi");
                                    vf.AddStream(rvid);                                    
                                    vf.Dispose();

                                    rvid.Dispose();
                                    vid.Dispose();
                                }
                                f.Close();
                            }
                         
                        }
                    }
                   c.Close();
            }

            return false;
        }
    }
}
