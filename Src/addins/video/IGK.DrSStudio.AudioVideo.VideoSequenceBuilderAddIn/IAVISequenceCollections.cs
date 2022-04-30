using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IGK.DrSStudio.AudioVideo
{
    public interface IAVISequenceCollections : IEnumerable 
    {
        IAVISequence this[int index] {get;}
        int Count { get; }
        IAVISequence GetNext(IAVISequence seq);
        void Add(IAVISequence  sequence);
        void Remove(IAVISequence sequence);
    }
}
