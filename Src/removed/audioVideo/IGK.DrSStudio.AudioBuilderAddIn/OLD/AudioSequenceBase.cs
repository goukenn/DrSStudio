

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: AudioSequenceBase.cs
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
file:AudioSequenceBase.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace IGK.DrSStudio.AudioBuilder
{
    /// <summary>
    /// represent the base sequence type
    /// </summary>
    public abstract class AudioSequenceBase : IAudioSequence
    {
        private IAudioTrack m_Parent;
        public int Form { get{return 0;} }
        public int To { get{return 5;} }
        public virtual int Duration { get {return this.To - this.Form ;} }
        #region IAudioSequence Members
        public IAudioTrack Parent
        {
            get { return this.m_Parent ;}
            internal protected set{ this.m_Parent = value ;}
        }
        public virtual enuAudioSequenceType SequenceType
        {
            get { return enuAudioSequenceType.WavFile ; }
        }
        #endregion
        protected AudioSequenceBase()
        {
        }
        #region IComparer<IAudioSequence> Members
        public int Compare(IAudioSequence x, IAudioSequence y)
        {
            return x.Form.CompareTo(y.Form);
        }
        #endregion
        #region IDisposable Members
        public virtual void Dispose()
        {
        }
        #endregion
        public  abstract void Play();
    }
}

