using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IGK.DrSStudio.AudioVideo
{
    public interface  IAVISequence : IAVISequenceUpdateItem , IAVISequenceProjectItem
    {
        
        TimeSpan Duration { get; }
        /// <summary>
        /// initialize the current sequence
        /// </summary>
        void InitSequence();

        void Render(ICore.GraphicModels.ICoreGraphics v_device);
    }
}
