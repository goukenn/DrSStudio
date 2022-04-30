

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: AudioVideoSelectAudioEncoder.cs
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
    [CoreMenu("Tools.AudioVideo.ChooseDefaultAudioEncoder", 0x5004)]
    class AudioVideoSelectAudioEncoder : CoreApplicationMenu
    {
        protected override bool PerformAction()
        {
            Workbench.ConfigureWorkingObject(AudioVideoAudioCodecTool.Instance, 
                "title.chooseAudioEndoder".R(), false , Size2i.Empty );

            var c = AudioVideoAudioCodecTool.Instance.SelectedCodec;
            if (c != null)
            {
                if (c.Open())
                {
                    //c.Open(AVIApi.VCM.enuVCMLocateFlags.Compress);
                    //if (.CanConfigure)
                    //    c.Configure(Workbench.MainForm.Handle);
                    /*
                       using (OpenFileDialog ofd = new OpenFileDialog())
                        {
                            ofd.Filter = "filter.audiofiles".R() + "|*.wav;*.mp3;*.ogg";
                            if (ofd.ShowDialog() == DialogResult.OK)
                            {
                                AVIApi.AVI.AVIFile f = AVIApi.AVI.AVIFile.Open(ofd.FileName);
                                if (f != null)
                                {
                                    var vid = f.GetVideoStream();
                                    if (vid != null)
                                    {
                                        var rvid = c.ConvertTo(.Compress(vid, null);

                                        //
                                        AVIApi.AVI.AVIFile vf = AVIApi.AVI.AVIFile.CreateFile(CoreConstant.DEBUG_TEMP_FOLDER+"\\output.avi");
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
                     * */

                      using (OpenFileDialog ofd = new OpenFileDialog())
                        {
                            ofd.Filter = "filter.audiofiles".R() + "|*.wav;*.mp3;*.ogg";
                            if (ofd.ShowDialog() == DialogResult.OK)
                            {
                                
                                var p = c.GetFormatTags();
                                var f = c.GetFormats();
                                        //c.ConvertTo(ofd.FileName, 
                                        //    CoreConstant.DEBUG_TEMP_FOLDER+"\\output.wav",,,
                                        //     AVIApi.ACM.enuACMSuggest.WFormat);


                                //        //
                                //        AVIApi.AVI.AVIFile vf = AVIApi.AVI.AVIFile.CreateFile(CoreConstant.DEBUG_TEMP_FOLDER+"\\output.avi");
                                //        vf.AddStream(rvid);                                    
                                //        vf.Dispose();

                                //        rvid.Dispose();
                                //        vid.Dispose();
                                //    }
                                //    f.Close();
                                //}
                         
                            }
                        }
                    c.Close();
                }
            }

            return false;
        }
    }
}
