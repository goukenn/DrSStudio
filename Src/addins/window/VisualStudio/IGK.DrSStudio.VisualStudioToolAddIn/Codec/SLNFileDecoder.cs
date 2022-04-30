using IGK.ICore.Codec;
using IGK.ICore.WinUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.DrSStudio.Window.VS.Codec
{
    [CoreCodec ("visualstudio solution file", "application/visual-studio", "sln;csproj")]
    class SLNFileDecoder : CoreDecoderBase
    {
        public override bool Open(ICoreWorkbench bench, string filename, bool selectCreatedSurface)
        {
            return false;
        }
    }
}
