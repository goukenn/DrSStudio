using IGK.ICore;
using IGK.ICore.Codec;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.DrSStudio.AudioVideo
{
    public interface IAVISequenceProject
    {
        /// <summary>
        /// save the project
        /// </summary>
        void Save();
        [IGK.ICore.Codec.CoreXMLElement]
        string OutFolder { get; }
        string RawFolder { get; }
        string ResFolder { get; }
        string TextFolder { get; }
        [IGK.ICore.Codec.CoreXMLElement]
        enuAviSequenceSplitJoinMethod JoinMethod { get; set; }
        [IGK.ICore.Codec.CoreXMLElement]
        int BufferFileSize { get; set; }
        [IGK.ICore.Codec.CoreXMLElement]
        IAVISequenceVideoFormat OutputFormat{get; set; }
        /// <summary>
        /// get or set the name of the project
        /// </summary>
        string Name { get; set; }
        /// <summary>
        /// Save project to target directory
        /// </summary>
        /// <param name="directory"></param>
        /// <returns></returns>
        bool SaveProject(string directory);

       [IGK.ICore.Codec.CoreXMLElement]
        IAVISequenceCollections Sequences { get; }

        /// <summary>
        /// get the total duration of this sequence
        /// </summary>
        /// <returns></returns>
        double GetTotalSeconds();

        event EventHandler<CoreItemEventArgs<IAVISequence>> SequenceAdded;
        event EventHandler<CoreItemEventArgs<IAVISequence>> SequenceRemoved;





    }
}
