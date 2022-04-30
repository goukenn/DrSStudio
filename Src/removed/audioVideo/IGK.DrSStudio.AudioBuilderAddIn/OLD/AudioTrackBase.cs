

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: AudioTrackBase.cs
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
file:AudioTrackBase.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace IGK.DrSStudio.AudioBuilder
{
    using IGK.ICore;using IGK.AudioVideo;
    using IGK.AudioVideo.MMIO;
    /// <summary>
    /// represent a base abract sequence collection
    /// </summary>
    public abstract class AudioTrackBase :  IAudioTrack 
    {
        protected AudioTrackBase()
        {
            this.m_Sequences = new AudioSequenceCollections(this);
        }
        #region IAudioTrack Members
        public abstract  string Name
        {
            get;
        }
        public abstract TimeSpan Duration
        {
            get;
        }
        public abstract void Play();
        public abstract  void Pause();
        public IAudioTrack Copy(TimeSpan from, TimeSpan to)
        {
            throw new NotImplementedException();
        }
        public IAudioTrack Cut(TimeSpan from, TimeSpan to)
        {
            throw new NotImplementedException();
        }
        public void Delete(TimeSpan from, TimeSpan to)
        {
            throw new NotImplementedException();
        }
        #endregion
        private AudioSequenceCollections m_Sequences;
        public AudioSequenceCollections Sequences
        {
            get { return m_Sequences; }
        }
        IAudioSequenceCollections IAudioTrack.Sequences
        {
            get { return m_Sequences; }
        }
        public class AudioSequenceCollections : IAudioSequenceCollections, ICoreCountEnumerable
        {
            List<IAudioSequence> m_sequences;
            AudioTrackBase m_owner;
            public AudioSequenceCollections(AudioTrackBase track)
            {
                this.m_sequences = new List<IAudioSequence>();
                this.m_owner = track;
            }
            #region ICoreCountEnumerable Members
            public int Count
            {
                get { return this.m_sequences.Count; }
            }
            #endregion
            #region IEnumerable Members
            public System.Collections.IEnumerator GetEnumerator()
            {
                return this.m_sequences.GetEnumerator();
            }
            #endregion
            public void Add(AudioSequenceBase seq)
            {
                this.m_sequences.Add(seq);
            }
            #region IAudioSequenceCollections Members
            public void Add(IAudioSequence sequence)
            {
                this.m_sequences.Add(sequence);
            }
            public void Remove(IAudioSequence sequence)
            {
                this.m_sequences.Remove(sequence);
            }
            #endregion
            #region IAudioSequenceCollections Members
            public IAudioSequence this[int index]
            {
                get { return this.m_sequences[index]; }
            }
            #endregion
        }
        #region IAudioTrack Members
        public TimeSpan PlayFrom
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }
        public void Stop()
        {
            throw new NotImplementedException();
        }
        public void SaveTo(string filename)
        {
            throw new NotImplementedException();
        }
        public void Export()
        {
            throw new NotImplementedException();
        }
        public void Edit()
        {
            throw new NotImplementedException();
        }
        public void CopyAll()
        {
            throw new NotImplementedException();
        }
        public void Copy(TimeSpan From)
        {
            throw new NotImplementedException();
        }
        void IAudioTrack.Copy(TimeSpan From, TimeSpan To)
        {
            throw new NotImplementedException();
        }
        #endregion
        #region IDisposable Members
        public virtual void Dispose()
        {
            foreach (IAudioSequence  item in this.Sequences )
            {
                item.Dispose();
            }
        }
        #endregion
        public abstract IGK.AudioVideo.AVI.AVIEditableStream GetEditableStream();
    }
}

