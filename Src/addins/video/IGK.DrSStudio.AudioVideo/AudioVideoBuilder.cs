

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: AudioVideoBuilder.cs
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





﻿using IGK.AVIApi.AVI;
using IGK.AVIApi.MP3;
using IGK.ICore.IO;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.DrSStudio.AudioVideo
{
    public class AudioVideoBuilder
    {

        public static void ConvertMP3ToWave(string filename, string output)
        {
            MP3FileInfo.ConvertToWav(filename,
                            filename,
                            null);
        }
        public static void ConvertWaveToMP3(string filename, string output)
        {
            AVIFile r = AVIFile.Open(filename);
            if (r == null)
                return;
            AVIFile.AudioStream aud = r.GetAudioStream();
            if (aud != null)
            {
                if (!aud.Save(output, true, null, IntPtr.Zero))
                {
                    System.Diagnostics.Debug.WriteLine("Can't save audio file");
                }
                aud.Dispose();
            }
            r.Close();
        }

        public void BuilProject(string filename, int width, int height, TimeSpan duration, float framepersec)
        {
            IGK.AVIApi.AVI.AVIFileManager man = IGK.AVIApi.AVI.AVIFileManager.Instance;
            man.BuildVideo(filename, width, height, duration, framepersec);
        }

        public void MergeVideoFile(string output , params string[] filename)
        {
            Debug.WriteLine("MergeVideoStream");
            List<AVIFile> f = new List<AVIFile>();
            for (int i = 0; i < filename.Length; i++)
            {
                var s = AVIFile.Open(filename[i]);
                if (s != null)
                {
                    f.Add(s);
                }
            }
            if (f.Count > 0)
            {
                System.Diagnostics.Debug.WriteLine("Create Merge Output");
                AVIFile c = AVIFile.CreateFile(output);
                AVIEditableStream s = null;
                foreach (var item in f)
                {
                    if (s == null)
                    {
                        s = AVIEditableStream.CreateFrom(item.GetVideoStream());
                    }
                    else
                    {
                        using (var tc = AVIEditableStream.CreateFrom(item.GetVideoStream()))
                        {//paste new frame at end of the stream
                            s.BeginGetFrame();
                            s.Paste(tc.Handle,
                                0,
                                (int)tc.Length);
                            s.EndGetFrame();
                        }
                        
                    }
                }
                c.AddStream(s);
                s.Dispose();
                c.Close();
                //free stream
                foreach (var item in f)
                {
                    item.Dispose();
                }
            }
        }

        public void MergeAudioFile(string output, params string[] filename)
        {
            Debug.WriteLine("MergeAudioFile");
            List<AVIFile> f = new List<AVIFile>();
            for (int i = 0; i < filename.Length; i++)
            {
                var s = AVIFile.Open(filename[i]);
                if (s != null)
                {
                    f.Add(s);
                }
            }
            if (f.Count > 0)
            {
                System.Diagnostics.Debug.WriteLine("Create Merge Output");
                AVIFile c = AVIFile.CreateFile(output);
                AVIEditableStream s = null;
                foreach (var item in f)
                {
                    if (s == null)
                    {
                        s = AVIEditableStream.CreateFrom(item.GetAudioStream());
                    }
                    else
                    {
                        using (var tc = AVIEditableStream.CreateFrom(item.GetVideoStream()))
                        {//paste new frame at end of the stream
                            s.BeginGetFrame();
                            s.Paste(tc.Handle,
                                0,
                                (int)tc.Length);
                            s.EndGetFrame();
                        }

                    }
                }
                c.AddStream(s);
                s.Dispose();
                c.Close();
                //free stream
                foreach (var item in f)
                {
                    item.Dispose();
                }
            }
        }

        public void CompressVideoFile(string filename, bool chooseCodec)
        {
            string temp = Path.GetTempFileName()+".avi";
            File.Copy(filename, temp);
            AVIFile f = AVIFile.Open(temp);
            AVIFile.VideoStream vid = f.GetVideoStream();
            if (!vid.IsCompressed)
            {
                var v = vid.Compress(IntPtr.Zero, enuDialogFlag.All, chooseCodec,null);
                f.Close();
                f = AVIFile.CreateFileFromStreams(v);
                File.Delete(filename);
                if (f.SaveTo(filename, null))
                {
                    Debug.WriteLine("Save OK");
                }
                else {
                    Debug.WriteLine("Save OK");
                }
                f.Close();
                v.Dispose();
            }
            else {
                f.Close();
            }
            vid.Dispose();

            File.Delete(temp);
        }
        public static bool CompressVideoFile(IntPtr howner, string inputFile, string outputFile, bool chooseCodec)
        {

            using (AVIFile s = AVIFile.Open(inputFile))
            {
                if (s == null)
                    return false;
                if (s.GetNumberOfVideoStreams() != 1)
                {
                    s.Close();
                    return false;
                }
                AVIFile.VideoStream v = s.GetVideoStream();

                AVIFile.VideoStream r = v.Compress(howner, enuDialogFlag.All, chooseCodec);
                

                string f = Path.GetTempFileName();
                File.Move(f, f + ".avi");
                f += ".avi";
                using (AVIFile v_f = AVIFile.CreateFile(f))
                {
                    if (!v_f.AddStream(r))
                    {
                        CoreLog.WriteLine("Stream not added");
                    }
                    v_f.Close();
                    
                }
                r.Dispose();
                v.Dispose();
                s.Close();

                File.Copy(f, outputFile, true);
                PathUtils.RmFile(f);
               
                return true;
            };
        }
        public static bool CompressVideoFile(string inputFile, string outputFile)
        {
            return CompressVideoFile(IntPtr.Zero, inputFile, outputFile, true);

        }
    }
}
