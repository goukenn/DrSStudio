using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.ICore.IO
{
    [Flags()]
    public enum enuWOFFFileGlyfPointFlags
    {
        OnCurve=1,
        XByte=2,
        YByte=4,
        RepeatNumber=8,
        /// <summary>
        /// if XByte then XSign (sign of the number)
        /// </summary>
        XSign=16,
        /// <summary>
        /// if YByte then YSign (sign of the number)
        /// </summary>
        YSign = 32
    }
}
