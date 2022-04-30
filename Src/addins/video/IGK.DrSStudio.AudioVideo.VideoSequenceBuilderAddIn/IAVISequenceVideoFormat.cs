using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IGK.DrSStudio.AudioVideo
{
    /// <summary>
    /// represent a video sequence format
    /// </summary>
    public interface  IAVISequenceVideoFormat
    {
        int FramePerSec { get;  }
        int Width { get;  }
        int Height { get;  }
    }
}
