

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: AudioWavFileSequence.cs
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
file:AudioWavFileSequence.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IGK.ICore;using IGK.AudioVideo.AVI;
using IGK.AudioVideo.MMIO ;
namespace IGK.DrSStudio.AudioBuilder
{
    /// <summary>
    /// user to play Audio Wave Sequence
    /// </summary>
    public class AudioWavFileSequence : AudioSequenceBase
    {
        private readonly string m_FileName;
        private readonly IGK.AudioVideo.AVI.AVIFile m_file;
        private readonly IGK.AudioVideo.AVI.AVIFile.AudioStream m_audio;
        /// <summary>
        /// get the duration of this audio wav track
        /// </summary>
        public override int Duration {
            get {
                return (int)m_audio.GetTimeInfoFromSample(this.m_audio.Length).TimeSpan.TotalMilliseconds;
            }
        }
        public string FileName
        {
            get { return m_FileName; }
        }
        public AudioWavFileSequence(string filename)
        {
            this.m_FileName = filename;
            m_file = AVIFile.Open(filename);
            m_audio = m_file.GetAudioStream();
        }
        public override void Dispose()
        {
            m_audio.Dispose();
            m_file.Dispose();
            System.IO.File.Delete(this.FileName);
            base.Dispose();
        }
        public override void Play()
        {
            //if (MMIOManager.InstallIoProc("WAVE", new MMIOMsgHandler(ReadFile)))
            //{
                byte[] pcmdata = MMIOManager.GetPCMData(this.FileName);
                IGK.AudioVideo.MCI.MCIWaveOutStream v_out = IGK.AudioVideo.MCI.MCIWaveOutManager.CreateWaveOut();
                v_out.Play(pcmdata);
                v_out.Stop();
           // }
        }
        int ReadFile(MMIOMsgEventArgs  handle)
        {
            return 0;
        }
    }
}

