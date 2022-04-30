using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.DrSStudio.AudioVideo.Transitions
{
    public interface IAVISequenceTransition : IAVISequenceUpdateItem
    {

        /// <summary>
        /// get the parent sequence of the transition
        /// </summary>
        IAVIVideoSequence Sequence { get; }

        /// <summary>
        /// get the next sequence
        /// </summary>
        IAVIVideoSequence NextSequence { get; }
    }
}
