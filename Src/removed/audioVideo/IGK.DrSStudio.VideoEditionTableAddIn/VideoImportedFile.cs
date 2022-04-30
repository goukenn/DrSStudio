

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: VideoImportedFile.cs
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
file:VideoImportedFile.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace IGK.DrSStudio.VideoEditionTableAddIn
{
    using IGK.ICore;using IGK.AudioVideo.AVI;
    /// <summary>
    /// present an imported files
    /// </summary>
    public class VideoImportedFile : IVideoImportedFile , IDisposable
    {
        AVIFile m_file;
        private string m_FileName;
        private bool m_hasVideo;
        private bool m_hasAudio;
        private TimeSpan m_Duration;
        public AVIFile File { get { return this.m_file; } }
        private string m_TempWaveFile;
        /// <summary>
        /// get the tempory wave file
        /// </summary>
        public string TempWaveFileName
        {
            get { return m_TempWaveFile; }
        }
        public string FileName
        {
            get { return m_FileName; }
        }
        private VideoImportedFile() { }
        public static VideoImportedFile Create(string filename)
        {
            AVIFile file = AVIFile.Open(filename);
            if (file == null)return null;
            VideoImportedFile v_img = new VideoImportedFile();            
            v_img.m_file = file;
            v_img.m_FileName = filename;
            v_img.m_hasAudio = v_img .m_file .GetNumberOfAudioStreams () > 0;
            v_img.m_hasVideo = v_img.m_file.GetNumberOfVideoStreams () > 0;
            if (v_img.HasVideo )
            {
                AVIFile.VideoStream vid = v_img.m_file .GetVideoStream ();
                TimeSpan span = vid.GetTimeInfoFromSample(vid.Length).TimeSpan;
                v_img.m_Duration = span;
                vid.Dispose();
            }
            if (v_img.HasAudio)
            { 
                //export audio file file
                AVIFile.AudioStream v_audioStream = v_img.m_file.GetAudioStream();
                string v_tempAudioFile = System.IO.Path.GetTempFileName() + ".wav";
                if (!v_audioStream.ExportToWaveFile(v_tempAudioFile, null,false , IntPtr.Zero ))
                {
                    System.Windows.Forms.MessageBox.Show("Impossisble de lire la piste audio");
                    v_img.m_TempWaveFile = null;
                    v_img.m_hasAudio = false;
                }
                else
                {                    
                    v_img.m_TempWaveFile = v_tempAudioFile;
                }
            }
            return v_img;
        }
        #region IDisposable Members
        public void Dispose()
        {
            m_file.Dispose();
            if (this.HasAudio)
            {
                if (System.IO.File.Exists (this.m_TempWaveFile ))
                    System.IO.File.Delete(this.m_TempWaveFile);
            }
        }
        #endregion
        #region IVideoItemInfo Members
        public bool HasVideo
        {
            get { return m_hasVideo; }
        }
        public bool HasAudio
        {
            get { return m_hasAudio;  }
        }
        public TimeSpan Duration
        {
            get { return this.m_Duration; }
        }
        #endregion
    }
}

