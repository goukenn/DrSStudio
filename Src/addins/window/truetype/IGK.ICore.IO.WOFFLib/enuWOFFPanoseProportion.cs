using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.ICore.IO
{
    public enum enuWOFFPanoseProportion : byte
    {
        Any=0,
        NoFit= 1,
        OldStyle=2,
        Modern=3,
        EvenWidth=4,
        Expanded=5,
        Condensed=6,
        VeryExpanded=7,
        VeryCondensed=8,
        Monospace=9
    }
}
