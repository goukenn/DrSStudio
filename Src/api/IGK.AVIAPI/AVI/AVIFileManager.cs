

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: AVIFileManager.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.AVIApi.AVI
{
    /// <summary>
    /// represent a avi file manager
    /// </summary>
    public  class AVIFileManager
    {
        private static AVIFileManager sm_instance;
        private AVIFileManager()
        {
        }
        public  static AVIFileManager Instance
        {
            get
            {
                return sm_instance;
            }
        }
        static AVIFileManager()
        {
            sm_instance = new AVIFileManager();
        }


        public bool BuildVideo(string filename , TimeSpan duration, float framepersec, AVIFileUpdateCallBack update, AVIApiProgressEventHandler progression)
        { 
            if (update==null)
                return false ;
             AVIFile f = null;
            AVIApiProgressEventHandler proc = null;
            try
            {
                f = AVIFile.CreateFile(filename);
                if (f == null)
                    return false;
                float fps = framepersec <= 0 ? 24 : framepersec;
                Bitmap bmp = update(0, TimeSpan.FromSeconds (0));                
                AVIFile.VideoStream vid = f.AddNewVideoStream(fps, 1, 0, bmp);
                //synchronize
                if (vid == null)
                {
                    f.Dispose();
                    return false;
                }

                if (vid.BeginGetFrame())
                {
                    double v_T = duration.TotalSeconds;
                    float nbr_frame = (float) (1 * duration.TotalSeconds * fps);

                    for (int i = 1; i < nbr_frame; i++)
                    {
                        bmp = update (i, TimeSpan .FromSeconds ((i/(float)nbr_frame ) * v_T ));
                        vid.AddFrame(i, 1, bmp);
                        if (proc != null)
                            proc.BeginInvoke(this, 
                                new AVIApiProgressEventArgs ((int) ((i / nbr_frame) * 100.0f)), null, null);
                    }

                    vid.EndGetFrame();
                 
                    if (proc != null)
                        proc.BeginInvoke(this,
                                new AVIApiProgressEventArgs(100), null, null);
                }

                bmp.Dispose();

                vid.Dispose();
            }
            catch (Exception Exception)
            {
                //CoreLog
                //MessageBox.Show("Exception: " + Exception.Message, "Error");
                Debug.WriteLine(Exception.Message);
            }
            finally
            {
                if (f != null)
                    f.Close();
            }
            return true;
        }
        public bool BuildVideo(string filename, int width, int height, TimeSpan duration, float framepersec)
        {
            AVIFile f = null;
            AVIApiProgressEventHandler proc = null;
            try
            {
                f = AVIFile.CreateFile(filename);
                if (f == null)
                    return false;


                float fps = framepersec <= 0 ? 24 : framepersec;
                Bitmap bmp = new Bitmap(width, height, PixelFormat.Format24bppRgb);
                
                AVIFile.VideoStream vid = f.AddNewVideoStream(fps, 1, 0, width,height );
                //synchronize

                if (vid.BeginGetFrame())
                {
                    float nbr_frame = 1 * duration.Seconds * fps;

                    for (int i = 1; i < nbr_frame; i++)
                    {
                        vid.AddFrame(i, 1, bmp);
                        if (proc != null)
                            proc.BeginInvoke(this, 
                                new AVIApiProgressEventArgs ((int) ((i / nbr_frame) * 100.0f)), null, null);
                    }

                    vid.EndGetFrame();
                    /*
                    //compress
                    //
                    AVIFile.VideoStream sem = vid.Compress(IntPtr.Zero, enuDialogFlag.All, true);
                    sem.Save(CoreConstant.DEBUG_TEMP_FOLDER+"\\compress_avi.avi", null);
                    AVIFile f2 = AVIFile.CreateFileFromStreams(sem);

                    if (f2.SaveTo(CoreConstant.DEBUG_TEMP_FOLDER+"\\compress.avi", null))
                    {
                        Console.WriteLine("compressed OK ");
                    }
                    else {
                        Console.WriteLine("compressed not OK ");
                    }
                    
                    f2.Close();
                     * */
                   // sem.Dispose();
                    if (proc != null)
                        proc.BeginInvoke(this,
                                new AVIApiProgressEventArgs(100), null, null);
                }

                bmp.Dispose();

                vid.Dispose();
            }
            catch (Exception Exception)
            {
                //CoreLog
                //MessageBox.Show("Exception: " + Exception.Message, "Error");
                Debug.WriteLine(Exception.Message);
            }
            finally
            {
                if (f != null)
                    f.Close();
            }
            return true;
        }
    }
}
