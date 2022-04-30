using IGK.AVIApi.AVI;
using IGK.DrSStudio.AudioVideo.FX;
using IGK.ICore;
using IGK.ICore.IO;
using IGK.ICore.WinCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IGK.DrSStudio.AudioVideo
{
#pragma warning disable 1998 //await warning disable

    public class AVISequenceBuilder
    {
        private AVISequenceChain m_chain; //build in chain
        private IAVISequenceProject m_currentProject;

        public AVISequenceChain Chain { get { return this.m_chain; } }
        public AVISequenceBuilder()
        {
            this.m_chain = new AVISequenceChain(this);
            this.m_chain.FX.Add(new AVISequenceFXInvertColor()
            {
                From = TimeSpan.Zero,
                Duration = TimeSpan.FromSeconds(1),
                Infinite = false
            });
        }


        public int Width { get { return this.m_currentProject.OutputFormat.Width ; } }
        public int Height { get { return this.m_currentProject.OutputFormat.Height;  } }

        //public async Task<bool> Build(IAVISequenceProject project)
        //{
        //    retur n await BuildProject(project);
           
        //}

        //public async Task<bool> BuildProject(IAVISequenceProject project)
        //{
        //    if(project.Sequences.Count == 0)
        //        return false ;
        //    string dest = System.IO.Path.Combine(project.OutFolder, project.Name + ".avi");
        //    int v_FramePerSec = project.OutputFormat.FramePerSec;
        //    int v_Tframe = (int)(v_FramePerSec * project.GetTotalSeconds());
        //    AVISequenceUpdateInfo inf = new AVISequenceUpdateInfo();
        //    IAVISequence seq = project.Sequences[0];
        //    __initSequence(seq);
        //    int v_frame = 0; //current sequence frame renderer
        //    int v_tframe = 0;//total frame render
        //    TimeSpan v_span = TimeSpan.FromSeconds(0);

        //    var v_temp = PathUtils.GetTempFileWithExtension ("tmp.avi");
        //    var v_f = AVIFile.CreateFile(v_temp );
        //    if (v_f == null)
        //        return false;

        //    AVIFile.VideoStream vid = null;

        //    using (var v_bmp = WinCoreBitmap.Create(project.OutputFormat.Width, project.OutputFormat.Height))
        //    {
        //        var v_device = v_bmp.CreateDevice();


        //        while (seq != null)
        //        {
        //            CoreLog.WriteDebug("[AVISequenceBuilder] - " + v_tframe + " / " + v_Tframe);
        //            //update all info
        //            seq.Update(inf);

        //            //render all
        //            seq.Render(v_device);

        //            if (v_tframe == 0)
        //            {
        //                vid = v_f.AddNewVideoStream(v_FramePerSec, 1, 0, v_bmp.Width, v_bmp.Height);
        //                if (!vid.BeginGetFrame())
        //                {
        //                    vid.Dispose();
        //                    vid = null;
        //                    break;
        //                }
        //            }
        //            else
        //            {
        //                vid.AddFrame(v_tframe, 1, v_bmp.Bitmap);
        //            }
        //            v_frame++;
        //            v_tframe++;
        //            v_span = TimeSpan.FromSeconds((v_frame / (float)v_FramePerSec));
        //            inf.Update(v_frame, v_span,
        //                  TimeSpan.FromSeconds((v_tframe / (float)v_FramePerSec))
        //                  );
        //            if (inf.TimeSpan >= seq.Duration)
        //            {
        //                seq = project.Sequences.GetNext(seq);
        //                if (seq != null)
        //                {
        //                    __initSequence(seq);
        //                    v_frame = 0;
        //                    v_span = TimeSpan.Zero;
        //                }
        //            }
        //        }
        //    }
        //    bool ok = false;
        //    if (vid != null)
        //    {
        //        vid.EndGetFrame();
        //        vid.Dispose();
        //        ok = true;
        //    }
        //    v_f.Close();
        //    if (ok)
        //    {
        //        try
        //        {

        //            File.Delete(v_temp);
        //        }
        //        catch(Exception ex) {
        //            CoreLog.WriteLine(ex.Message);
        //        }
        //        return true;
        //    }
        //    return false;
        //}

        private void __initSequence(IAVISequence seq)
        {
            seq.InitSequence();
            this.m_chain.CurrentSequence = seq;
        }
        //public bool T(IAVISequenceProject project)
        //{
        //    if (project.Sequences.Count == 0)
        //        return false;
        //    this.m_currentProject = project;
        //    int v_FramePerSec = project.OutputFormat.FramePerSec;
        //    int v_Tframe = (int)(v_FramePerSec * project.GetTotalSeconds());
        //    AVISequenceUpdateInfo inf = new AVISequenceUpdateInfo();
        //    IAVISequence seq = project.Sequences[0];
        //    __initSequence(seq);
        //    int v_frame = 0; //current sequence frame renderer
        //    int v_tframe = 0;//total frame render
        //    TimeSpan v_span = TimeSpan.FromSeconds(0);

        //    var v_temp = PathUtils.GetTempFileWithExtension("tmp.avi");
        //    var v_f = AVIFile.CreateFile(v_temp);
        //    if (v_f == null)
        //        return false;

        //    AVIFile.VideoStream vid = null;
        //    var dvid = v_f.AddNewVideoStream(v_FramePerSec, 1, 0, project.OutputFormat.Width, project.OutputFormat.Height);
        //    vid = dvid.Compress(IntPtr.Zero, enuDialogFlag.All, true, null);
        //    var edit = AVIEditableStream.CreateFrom(vid);
        //    edit.Empty();
        //    var bb = edit.BeginGetFrame();
            

        //    using (var v_bmp = WinCoreBitmap.Create(project.OutputFormat.Width, project.OutputFormat.Height))
        //    {
        //        var v_device = v_bmp.CreateDevice();


        //        while (seq != null)
        //        {
        //            CoreLog.WriteDebug("[AVISequenceBuilder] - " + v_tframe + " / " + v_Tframe);
        //            //update all info
        //            seq.Update(inf);

        //            //render all
        //            seq.Render(v_device);

        //            if (v_tframe == 0)
        //            {
        //               // vid = v_f.AddNewVideoStream(v_FramePerSec, 1, 0, v_bmp.Width, v_bmp.Height);
        //                if (!vid.BeginGetFrame())
        //                {
        //                    vid.Dispose();
        //                    vid = null;
        //                    break;
        //                }
        //            }
        //            else
        //            {
        //                //dvid.AddFrame(v_tframe, 1, v_bmp.Bitmap);
        //                //edit.Paste(dvid, dvid.Frames -1, dvid.Frames);
        //                edit.AddFrame(v_tframe, 1, v_bmp.Bitmap);
        //            }
        //            v_frame++;
        //            v_tframe++;
        //            v_span = TimeSpan.FromSeconds((v_frame / (float)v_FramePerSec));
        //            inf.Update(v_frame, v_span,
        //                   TimeSpan.FromSeconds((v_tframe / (float)v_FramePerSec))
        //                   );
        //            if (inf.TimeSpan >= seq.Duration)
        //            {
        //                seq = project.Sequences.GetNext(seq);
        //                if (seq != null)
        //                {
        //                    __initSequence(seq);
        //                    v_frame = 0;
        //                    v_span = TimeSpan.Zero;
        //                }
        //            }
        //        }
        //    }
        //    bool ok = false;
        //    if (vid != null)
        //    {
        //        vid.EndGetFrame();
        //        vid.Dispose();
        //        ok = true;
        //    }
        //    v_f.Close();
        //    if (ok)
        //    {
        //        var ff = AVIFile.CreateFileFromStreams(
        //        edit);
        //        ff.SaveTo(CoreConstant.DEBUG_TEMP_FOLDER+"\\compresss.avi", null);
        //        ff.Dispose();
        //        edit.Dispose();

        //        try
        //        {

        //            string dest = System.IO.Path.Combine(project.OutFolder, project.Name + ".avi");
        //            PathUtils.MoveFile(v_temp, dest);
        //        }
        //        catch (Exception ex)
        //        {
        //            CoreLog.WriteLine(ex.Message);
        //        }
        //        return true;
        //    }
        //    return false;
        //}

        public bool BuildProject(IAVISequenceProject project)
        {

            if (project.Sequences.Count == 0)
                return false;
            string dest = System.IO.Path.Combine(project.OutFolder, project.Name + ".avi");
            if (File.Exists(dest))
                File.Delete(dest);
            bool v_result = false;
            this.m_currentProject = project;
            
            int v_FramePerSec = project.OutputFormat.FramePerSec;
            enuAviSequenceSplitJoinMethod v_method = project.JoinMethod;
            int v_bufferFileSize = project.BufferFileSize * 1048576;
            int v_Tframe = (int)(v_FramePerSec * project.GetTotalSeconds());
            AVISequenceUpdateInfo inf = new AVISequenceUpdateInfo();
            IAVISequence seq = project.Sequences[0];
            __initSequence(seq);
            this.ih = 0;
            int v_frame = 0; //current sequence frame renderer
            int v_tframe = 0;//total frame render
            TimeSpan v_span = TimeSpan.FromSeconds(0);

            var v_temp = PathUtils.GetTempFileWithExtension("avi");
            var v_f = AVIFile.CreateFile(v_temp);
            if (v_f == null)
               return false;

            
            var dvid = v_f.AddNewVideoStream(
                v_FramePerSec,
                1,
                AVIFile.NO_COMPRESSION,
                enuAVIVideoFormat.Format32,
                project.OutputFormat.Width, 
                project.OutputFormat.Height);
             AVIVideoCompressionOption op = new AVIVideoCompressionOption ();
            dvid.GetCompressionOption(IntPtr.Zero, enuDialogFlag.All, op);
            
            using (var v_bmp = WinCoreBitmap.Create(project.OutputFormat.Width, project.OutputFormat.Height))
            {
                var v_device = v_bmp.CreateDevice();
                int index = 0;
                int sub = 0;
                while (seq != null)
                {
#if DEBUG
                    CoreLog.WriteDebug("[AVISequenceBuilder] - " + v_tframe + " / " + v_Tframe);
#endif
                    //update all info
                    Chain.Update(inf);// seq.Update(inf);

                    Chain.Render(v_device);
                    //render all
                    //seq.Render(v_device);

                    if (v_tframe == 0)
                    {
                        // vid = v_f.AddNewVideoStream(v_FramePerSec, 1, 0, v_bmp.Width, v_bmp.Height);
                       
                        if (!dvid.BeginGetFrame())
                        {
                            dvid.Dispose();
                            dvid = null;
                            break;
                        }
                    }
                      dvid.AddFrame(index, 1, v_bmp.Bitmap);                  
                    v_frame++;
                    v_tframe++;
                    index++;
                    v_span = TimeSpan.FromSeconds((v_frame / (float)v_FramePerSec));

                    inf.Update(v_frame, v_span,
                        TimeSpan.FromSeconds((v_tframe / (float)v_FramePerSec))
                        );

                    if (dvid.GetLongLength() > v_bufferFileSize)
                    {
                        __flushSequence(dest, dvid, op, v_method);
                        index = 0;
                        sub++;
                        dvid.EndGetFrame();
                        dvid.Dispose();
                        v_f.Dispose();
                        File.Delete(v_temp);
                        v_f = AVIFile.CreateFile(v_temp);
                        if (v_f != null)
                        {
                            dvid = v_f.AddNewVideoStream(
                    v_FramePerSec,
                    1,
                    AVIFile.NO_COMPRESSION,
                    enuAVIVideoFormat.Format32,
                    project.OutputFormat.Width,
                    project.OutputFormat.Height);
                            dvid.BeginGetFrame();
                        }
                    }

                    if (inf.TimeSpan >= seq.Duration)
                    {
                        seq = project.Sequences.GetNext(seq);
                        if (seq != null)
                        {
                            __initSequence(seq);
                            v_frame = 0;
                            v_span = TimeSpan.Zero;
                        }
                    }
                }
            }
            bool ok = false;
            if (dvid != null)
            {
                dvid.EndGetFrame();
                __flushSequence(dest, dvid, op, v_method );         
                dvid.Dispose();
                ok = true;

            }
            v_f.Close();
            if (ok)
            {
                try
                {
                    File.Delete(v_temp);
                    v_result = true;
                }
                catch (Exception ex)
                {
                    CoreLog.WriteLine("Error : "+ ex.Message);
                }                
            }
            this.m_currentProject = null;
            return v_result; 
            //v_result;
        }
        int ih = 0;
        private void __flushSequence(string dest , AVIFile.VideoStream dvid, AVIVideoCompressionOption op, 
            enuAviSequenceSplitJoinMethod method)
        {
            string k = dest;
            string c = dest + "_" + ih + "_.data.avi";          

            using (var cvid = dvid.Compress(op))
            {
                if (cvid != null)
                {

                  // cvid.WriteAllData(data);
                    AVIFile f = null;
                    if (File.Exists(k))
                    {
                        f = AVIFile.Open(k, enuAviAccess.ReadWrite);
                    }
                    else
                    {
                        AVIFile.CreateFileFromStreams(k, new AVIFrameStream[] { cvid }).Close();
                        return;
                        //////if (cvid.Save(k, null))
                        //////{
                        //////    cvid.Dispose();
                        //////    return;
                        //////}
                     //   f = AVIFile.CreateFile(k);
                    }
                    AVIFile.VideoStream vid = f.GetVideoStream();
                  
                    if (vid == null)
                    {
                        f.AddStream(cvid);
                    }
                    else
                    {
                        switch (method)
                        {
                            case enuAviSequenceSplitJoinMethod.EditStream :
                                //EDIT STREAM METHOD TO APPEN FILE ------------
                                AVIEditableStream e = AVIEditableStream.CreateFrom(vid);
                                e.Paste(cvid, 0, cvid.Length);
                                AVIFile t = AVIFile.CreateFile(c);
                                bool cc =  t.AddStream(e);
                                t.Close();
                                e.Dispose();
                                break;
                            case enuAviSequenceSplitJoinMethod.AppendStream:
                            default:

                           

                                //var p = AVIFile.CreateFileFromStreams(e);

                                //p.Save(c);                        
                                //p.Dispose();

                                ////AVIFile.CreateFileFromStreams(c, new AVIFrameStream []{ cvid }).Dispose();



                                //append stream method-------------------------
                                vid.Append(cvid);
                                break;
                        }
                        vid.Dispose();
                        ih++;
                    }
                    f.Close();
                    //EDIT STREAM METHOD TO APPEN FILE ------------
                    if (File.Exists(c))
                        PathUtils.MoveFile(c, k);
                }
            }
        }
    }
}
