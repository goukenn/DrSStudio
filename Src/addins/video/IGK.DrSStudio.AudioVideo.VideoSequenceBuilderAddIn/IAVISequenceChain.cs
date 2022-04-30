using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IGK.DrSStudio.AudioVideo
{
    public interface IAVISequenceChain
    {
        IAVISequence CurrentSequence { get; set; }
    }
}
