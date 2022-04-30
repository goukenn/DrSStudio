using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.ICore.IO
{
    [Flags()]
    public enum enuWOFFGaspFlag : ushort
    {
        None = 0,
        GridFit = 1,
        Dogray = 2,
        SymmetricGridFit = 4,
        Smoothing = 8
    }
}
