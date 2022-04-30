

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: AVIStreamUtils.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
/*
IGKDEV @ 2008-2016
author: C.A.D . BONDJE DOUE
file:AVIStreamUtils.cs
*/
using System;
using System.IO;
namespace IGK.AVIApi
{
    using IGK.ICore;
    using IGK.AVIApi.MMIO ;
    using IGK.AVIApi.AVI;
    using IGK.AVIApi.Native;
    using IGK.AVIApi.MP3;
    using IGK.ICore.IO;
    using System.Runtime.InteropServices;
    /// <summary>
    /// represent a avi stream utils. for public operation
    /// </summary>
    public static class AVIStreamUtils
    {
        internal  static bool ChooseCodec(IntPtr handle, IntPtr hwnd, enuDialogFlag flag, ref AVIApi.AVICOMPRESSOPTIONS ao)
        {
            AVIApi.AVICOMPRESSOPTIONS_CLASS rf = new AVIApi.AVICOMPRESSOPTIONS_CLASS();
            rf.fccHandler = (uint)AVIApi.streamTypeVideo;
            if (AVIApi.AVISaveOptions(hwnd, (uint)flag,
            1,
                ref handle,
                ref rf))
            {
                int h = AVIApi.AVISaveOptionsFree(1, ref rf);
                if (h == (int)enuAviError.NoError)
                {
                    ao = rf.ToStruct();
                    return true;
                }
            }
            return false;
        }
        /// <summary>
        /// choose avi stream codec;
        /// </summary>
        /// <param name="handle"></param>
        /// <param name="hwnd"></param>
        /// <param name="flag"></param>
        /// <returns></returns>
        internal  static AVIApi.AVICOMPRESSOPTIONS_CLASS ChooseVideoCodec(IntPtr handle, IntPtr hwnd, enuDialogFlag flag)
        {
            AVIApi.AVICOMPRESSOPTIONS_CLASS rf = new AVIApi.AVICOMPRESSOPTIONS_CLASS();
            rf.fccHandler = (uint)AVIApi.streamTypeVideo;
            if (AVIApi.AVISaveOptions(hwnd, (uint)flag,
            1,
                ref handle,
                ref rf))
            {
                int h = AVIApi.AVISaveOptionsFree(1, ref rf);
                if (h == (int)enuAviError.NoError)
                {
                    return rf;
                }
            }
            return null;
        }
        /// <summary>
        /// add audio data from audiofile to target avi file
        /// </summary>
        /// <param name="file"></param>
        /// <param name="audioFile"></param>
        /// <returns></returns>
        public static bool AddAudioData(AVIFile file, string audioFile)
        {
            if ((file == null) || !File.Exists(audioFile))
                return false;
            bool v_result = false;
            bool v_isMP3 = false;
            MP3FileInfo mp = MP3FileInfo.OpenFile(audioFile);
            byte[] data = null;
            if (mp != null)
            {
                //mp3 file read all data
                v_isMP3 = true;
                data = File.ReadAllBytes(audioFile);
            }
            if (v_isMP3)
            {
               v_result = file.AddAudioStream(null, data);
            }
            else {
                using (AVIFile v_f = AVIFile.Open(audioFile))
                {
                    if (v_f == null)
                        return false;
                    if (v_f.GetNumberOfVideoStreams() == 0)
                        return false;
                    //get audio stream
                    using (AVIFile.AudioStream v_aud = v_f.GetAudioStream())
                    {
                        v_result = file.CopyStream(v_aud);
                        if (v_result == false)
                        { //possible raison file can't read audio data with avi api
                            string v_tempfile = TempFile(Path.GetTempFileName (),".wav");
                            AVIStreamInfoStruct v_streamInfo = v_aud.GetStreamInfo ();
                            //IWAVEFORMAT 
                            IntPtr v_streamformat = v_aud.GetFormatPtr();
                            MPEGLAYER3WAVEFORMAT[] v_formats = null;
                            int v_formatSize = AVIStream.GetSizeOfFormat(v_aud.Handle);

                            int v_size = 0;
                            if (ExtractAudioData(audioFile, v_tempfile, ref v_size))
                            {
                                //v_result = AddAudioData(file, v_tempfile);                                
                                mp = MP3FileInfo.OpenFile(v_tempfile);
                                if (mp != null)
                                {
                                    v_isMP3 = true;
                                    data = File.ReadAllBytes(v_tempfile );
                                    v_formats = new MPEGLAYER3WAVEFORMAT[v_aud .Length];
                                    IntPtr v_talloc = IntPtr.Zero ;
                                    for (int i = 0; i < v_formats.Length ; i++)
                                    {
                                        v_talloc = v_aud.GetFormatPtr(i);
                                       v_formats[i] =  MPEGLAYER3WAVEFORMAT.GetFromHandle(v_talloc);
                                        AVIStream.FreeFormatPtr(v_talloc);
                                        v_talloc = IntPtr.Zero;
                                    }
                                }
                                if (v_isMP3)
                                {
                                    v_result = file.AddAudioStream(
                                        v_streamformat,
                                        v_formatSize,
                                        v_streamInfo,
                                        data,
                                        v_formats);
                                }
                                else {
                                    v_result = AddAudioData(file, v_tempfile);
                                }
                                File.Delete(v_tempfile);
                            }
                            AVIStream.FreeFormatPtr(v_streamformat);
                        }
                    }
                }
            }
            return v_result;
        }

        private static string TempFile(string tempFile, string extension)
        {
            string f= tempFile + extension;
            if (File.Exists(tempFile))
            {
                File.Move(tempFile, tempFile + extension);
                return f;
            }
            return tempFile;
        }
        /// <summary>
        /// Extract audio to Avi files
        /// </summary>
        /// <param name="sourceFile">AVI file that contains the audio to extract</param>
        /// <param name="file">avifile stream destination</param>
        /// <returns>true on succes otherwise false</returns>
        /// <note>if AVIFile is .avi audio data will not be converted by the API. but if .wav
        /// happy consider the block alignment a every frame will not be aligned correctly. used only
        /// with compressed audio format
        /// </note>
        public static bool ExtractAudioDataToAVIFile(string sourceFile, AVIFile file)
        {
            if (!File.Exists(sourceFile) || (file  == null))
                return false;
            const string pattern = "{0}{1}wb";
            AVIFile.AudioStream v_taudio = null;
            IntPtr v_format = IntPtr.Zero;
            int v_formatSize = 0;
            int v_lenght = 0;
            int bsize = 0;
            using (AVI.AVIFile tfile = AVI.AVIFile.Open(sourceFile))
            {
                if (tfile == null)
                    return false;
                int aud = tfile.GetNumberOfAudioStreams();
                if (aud == 0)
                {
                    return false;
                }
                AVI.AVIFile.AudioStream v_aud = tfile.GetAudioStream();
                v_format = v_aud.GetFormatPtr();
                v_formatSize = v_aud.GetFormatSize();
                v_lenght = v_aud.Length;
                switch (v_formatSize)
                {
                    case 30:
                        MPEGLAYER3WAVEFORMAT v_tformat =  MPEGLAYER3WAVEFORMAT.GetFromHandle(v_format);
                        bsize = v_tformat.nBlockSize;
                        break;
                    case 20:
                    default :
                        break;
                }
                file.AddAudioStream(out v_taudio, v_format, v_formatSize, v_aud.GetStreamInfo());
                AVIStream.FreeFormatPtr(v_format);
                v_aud.Dispose();
            }
            if (v_taudio == null)
                return false;
            using (MMIOStream stream = MMIOManager.OpenFile(sourceFile, enuMMIOAccess.Read | enuMMIOAccess.Buffered))
            {
                if (stream == null)
                    return false;                
                System.Collections.Generic.List<byte> m_byte = new System.Collections.Generic.List<byte>();
                if (stream.Descend(enuMMIOFindMode.FindRiff) && stream.Descend("movi", enuMMIOFindMode.FindList))
                {
                    try
                    {                       
                            while (
                                       stream.Descend(string.Format(pattern, 0,  1), enuMMIOFindMode.FindChunk))//"XVID", enuMMIOFindMode.Current ))
                            {
                                stream.Seek(stream.ChunckOffset, enuMMIOSeek.Origin);
                                byte[] d = stream.Read(stream.ChunckSize);
                                m_byte.AddRange(d);
                                //binW.Write(d, 0, d.Length);
                                stream.Ascend();
                            }
                    }
                    finally
                    {
                    }
                    int c = m_byte.Count;                    
                    for (int i = 0; i < c; i+=bsize  )
                    {
                        AVIStream.WriteData(v_taudio, enuAviWriteMode.None, 1, m_byte.GetRange (i,
                            Math.Min (bsize , c - i) ).ToArray());
                    }
                    v_taudio.Dispose();
                }
                //close the mmio stream
                stream.Close();
            }
            return true;
        }
     
        /// <summary>
        /// Extract audio to stream
        /// </summary>
        /// <param name="sourceFile">source file</param>
        /// <param name="targetStream">target stream</param>
        /// <returns></returns>
        public static bool ExtractAudioData(string sourceFile, Stream targetStream)
        {
            if (!File.Exists(sourceFile) || (targetStream ==null))
                return false;
            const string pattern = "{0}{1}wb";
            string dd = sourceFile + ".cp.avi";
            File.Move(sourceFile, sourceFile + ".cp.avi");

            IGK.AVIApi.AVI.AVIFile file = //IGK.AVIApi.AVI.AVIFile.Open(sourceFile);
                IGK.AVIApi.AVI.AVIFile.Open(dd);
            int aud = 0;
            if (file != null)
            {
                aud = file.GetNumberOfAudioStreams();
                file.Dispose();
                if (aud == 0)
                    return false;
            }
            bool result = false;
            using (MMIOStream stream = MMIOManager.OpenFile(sourceFile, enuMMIOAccess.Read | enuMMIOAccess.Buffered))
            {
                if (stream == null)
                    return false;
                int v_datalength = 0;
                BinaryWriter binW = null;
                if (stream.Descend(enuMMIOFindMode.FindRiff) && stream.Descend("movi", enuMMIOFindMode.FindList))
                {
                    try
                    {
                        binW = new BinaryWriter(targetStream);
                        for (int i = 0; i < aud; i++)
                        {
                            while (
                                       stream.Descend(string.Format(pattern, 0, i + 1), enuMMIOFindMode.FindChunk))//"XVID", enuMMIOFindMode.Current ))
                            {
                                stream.Seek(stream.ChunckOffset, enuMMIOSeek.Origin);
                                byte[] d = stream.Read(stream.ChunckSize);
                                binW.Write(d, 0, d.Length);
                                stream.Ascend();
                                v_datalength += stream.ChunckSize;
                            }
                        }
                        result = true;
                    }
                    finally
                    {
                        if (binW != null)
                        {
                            binW.Flush();                            
                        }
                    }
                }
                //close the mmio stream
                stream.Close();
            }
            return result;
        }
        /// <summary>
        /// Extract audio from Avi file. expected wb file
        /// </summary>
        /// <param name="sourceFile">avi filename</param>
        /// <param name="destination">destination</param>
        /// <param name="datalength">return the data read</param>
        /// <returns>true if audio file extracted otherwise false</returns>
        public static bool ExtractAudioData(string sourceFile, string destination, ref int datalength)
        {
            if (!File.Exists (sourceFile )  ||string.IsNullOrEmpty (destination ))
                return false ;

            
            MemoryStream mem = new MemoryStream();
            bool r = ExtractAudioData(sourceFile, mem);
            if (r) {
                var fs = System.IO.File.Create (destination);            
                mem.WriteTo(fs);
                fs.Close();
            }
            mem.Dispose();
            return r;
            /*

            const string pattern = "{0}{1}wb";            
            AVI.AVIFile file = AVI.AVIFile.Open(sourceFile);
            if (file == null)
                return false;

            int aud = file.GetNumberOfAudioStreams();
            file.Dispose();
            if (aud == 0)
                return false;
            using (MMIOStream stream = MMIOManager.OpenFile(sourceFile, enuMMIOAccess.Read | enuMMIOAccess.Buffered))
            {
                if (stream == null)
                    return false;
                string v_path = Path.GetFileNameWithoutExtension(destination);
                string v_dir = PathUtils.GetDirectoryName(destination);
                string v_ext = Path.GetExtension(destination);
                int v_datalength = 0;
                BinaryWriter binW = null;
                if (stream.Descend(enuMMIOFindMode.FindRiff) && stream.Descend("movi", enuMMIOFindMode.FindList))
                {
                    try
                    {
                        binW = new BinaryWriter(File.Create(destination));
                        for (int i = 0; i < aud; i++)
                        {
                            while (
                                       stream.Descend(string.Format(pattern, 0, i+1), enuMMIOFindMode.FindChunk))//"XVID", enuMMIOFindMode.Current ))
                            {
                                stream.Seek(stream.ChunckOffset, enuMMIOSeek.Origin);
                              //  PMMCHKInfo chunkinfo = stream.ReadChunck();
                                byte[] d = stream.Read(stream.ChunckSize);
                                binW.Write(d, 0, d.Length);
                                stream.Ascend();
                                v_datalength += stream.ChunckSize;
#if DEBUG
                                System.Diagnostics.Debug.WriteLine("writing chunk ....." + stream.ChunckSize + "/" + v_datalength);
                               stream.Flush();
#endif
                            }
                        }
                    }
                    finally {
                        if (binW != null)
                        {
                            binW.Flush();
                            binW.Close();
                        }
                    }
                }
                    datalength = v_datalength;
                stream.Close();
            }
            return true;*/
        }


        public static bool CopyStream(AVIStream sourceStream, AVIStream destination)
        {
            return CopyStream(sourceStream.Handle, destination.Handle, AVIStream.GetStreamInfo(sourceStream.Handle));
        }

        internal static bool CopyStream(IntPtr stream, IntPtr v_outStream, AVIStreamInfoStruct v_streamInfo)
        {
            uint r = 0;
            int v_rbytes = 0;
            int v_rsamples = 0;
            int v_wbytes = 0;
            int v_wsamples = 0;
            bool v_result = false;
            int v_index = 0;
            enuAviWriteMode v_writemode = enuAviWriteMode.None;
            int v_r = AVIApi.NOERROR;
          
            int v_bufferSize = v_streamInfo.dwSuggestedBufferSize;//v_rbytes;
            int v_bufferSample = 1;
            IntPtr allocOld = Marshal.AllocCoTaskMem(v_bufferSize);
            while (((v_r = AVIApi.AVIStreamRead(stream, v_index,
                v_bufferSample,
                allocOld,
                v_bufferSize,
                ref v_rbytes,
                ref v_rsamples)) == 0)
           && (v_rbytes > 0))
            {
                r = AVIApi.AVIStreamWrite(
                     v_outStream, v_index, v_rsamples, allocOld, v_rbytes,
                     v_writemode,
                     ref v_wsamples,
                     ref v_wbytes);
                if (r != 0)
                {
                    break;
                }
                v_index += v_wsamples;
#if DEBUG
                System.Diagnostics.Debug.WriteLine("AddStream()->DataSampleWrite: Progress ... writesamples:" + v_wsamples
                    + " writebytes: " + v_wbytes + " " + v_index + "/" + v_streamInfo.dwLength);
#endif
                // }

            }
            v_result = (v_index == v_streamInfo.dwLength);
            return v_result;
        }
    }
}

