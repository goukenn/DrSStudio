using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IGK.DrSStudio.AudioVideo
{
    public interface  IAVISequenceUpdateInfo 
    {
        int Frame { get; }
        int TotalFrame { get; }
        TimeSpan TimeSpan { get; }
        TimeSpan TotalTimeSpan { get; }
    }
}
