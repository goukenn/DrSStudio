using IGK.ICore.Codec;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IGK.ICore;
using IGK.ICore.WinUI;

namespace IGK.DrSStudio.Window.VS.Codec
{
    /// <summary>
    /// visual studio solution file encoder
    /// </summary> 
    class SLNFileEncoder : CoreEncoderBase
    {
        public override bool Save(ICoreWorkingSurface surface, string filename, params ICoreWorkingDocument[] documents)
        {
            return false;
        }
    }
}
