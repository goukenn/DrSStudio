using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.DrSStudio.AudioVideo
{
    public interface IAVIVideoSequence : IAVISequence
    {
        /// <summary>
        /// get the source of this sequence
        /// </summary>
        string Source { get; }
        /// <summary>
        /// delete this sequence from parent
        /// </summary>
        void Delete();
        /// <summary>
        /// split this sequence
        /// </summary>
        /// <param name="at"></param>
        IAVIVideoSequence[]  Split(int at);
        /// <summary>
        /// insert a video sequence
        /// </summary>
        /// <param name="sequence"></param>
        /// <param name="at"></param>
        void Insert(IAVIVideoSequence sequence, int at);
    }
}
