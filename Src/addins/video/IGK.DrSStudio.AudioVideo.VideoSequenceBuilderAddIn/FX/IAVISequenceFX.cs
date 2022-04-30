using IGK.ICore.GraphicModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.DrSStudio.AudioVideo.FX
{
    /// <summary>
    /// represent a fx 
    /// </summary>
    public interface  IAVISequenceFX : IAVISequenceUpdateItem
    {
        string Name{get;}
        bool IsActive { get; }
        /// <summary>
        /// get or set the start timespan of this effect
        /// </summary>
        TimeSpan From { get; set; }
        /// <summary>
        /// get or set effect duration
        /// </summary>
        TimeSpan Duration { get; set; }
        void Render(ICoreGraphics graphics);
    }
}
