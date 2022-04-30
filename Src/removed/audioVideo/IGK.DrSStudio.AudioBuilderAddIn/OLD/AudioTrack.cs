

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: AudioTrack.cs
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
file:AudioTrack.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IGK.ICore;using IGK.AudioVideo.AVI;
namespace IGK.DrSStudio.AudioBuilder
{
    using IGK.AudioVideo.MMIO;
    using IGK.AudioVideo.ACM;
    public class AudioTrack : AudioTrackBase 
    {
        public override string Name
        {
            get { return "Audio"; }
        }
        public AudioTrack()
        {
            this.m_duration = new TimeSpan();
        }
        public override IGK.AudioVideo.AVI.AVIEditableStream GetEditableStream()
        {
            IGK.AudioVideo.AVI.AVIEditableStream e = null;
            foreach (IAudioSequence  item in this.Sequences)
            {
                switch (item.SequenceType)
                {
                    case enuAudioSequenceType.WavFile:
                        AudioWavFileSequence seq = (AudioWavFileSequence)(item);
                        AVIFile f =  AVIFile.Open(seq.FileName);
                        AVIFile.AudioStream audio =  f.GetAudioStream();
                        if (audio != null)
                        {
                            e = IGK.AudioVideo.AVI.AVIEditableStream.Create(audio);
                        }
                        audio.Dispose();
                        f.Dispose();
                        break;
                }
            }
            return e;
        }
        TimeSpan m_duration;
        /// <summary>
        /// Get the first sequence
        /// </summary>
        public AudioSequenceBase FirstSequence {
            get {
                if (this.Sequences.Count > 0)
                    return (AudioSequenceBase)Sequences[0];
                return null;
            }
        }
        /// <summary>
        /// get the last sequence
        /// </summary>
        public AudioSequenceBase LastSequence {
            get {
                if (this.Sequences .Count  > 0)
                return (AudioSequenceBase)this.Sequences[this.Sequences.Count - 1];
                return null;
            }
        }
        /// <summary>
        /// get the duration of this audio track
        /// </summary>
        public override TimeSpan Duration
        {
            get { 
                AudioSequenceBase s = this.LastSequence ;
                if (s!=null){
                    TimeSpan t = new TimeSpan(0, 0, 0, 0, s.Duration);
                    return t;
                }
                return m_duration; 
            }
        }
        public override void Play()
        {
            this.FirstSequence.Play();
        }
        public override void Pause()
        {
            throw new NotImplementedException();
        }
    }
}

