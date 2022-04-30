using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.ICore.IO
{
    [Flags()]
    public enum enuWOFFFileUnicodeRange1
    {
        BasicLatin=0x1,
        LatinSupplement = 0x2,
        LatinExtendedA =0x4,
        LatinExtendedB=0x8,
        Latin=0x7

    }
}
