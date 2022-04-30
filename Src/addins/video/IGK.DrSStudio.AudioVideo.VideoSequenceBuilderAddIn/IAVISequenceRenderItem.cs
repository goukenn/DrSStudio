using IGK.ICore.GraphicModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IGK.DrSStudio.AudioVideo
{
    public interface IAVISequenceRenderItem
    {
        void Render(ICoreGraphics graphics);
    }
}
