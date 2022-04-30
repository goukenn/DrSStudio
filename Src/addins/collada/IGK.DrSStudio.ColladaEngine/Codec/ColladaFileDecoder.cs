using IGK.ICore.Codec;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IGK.ICore.WinUI;

namespace IGK.DrSStudio.ColladaEngine.Codec
{
    [CoreCodec("Collada", "application/collada", 
        "dae;zae")]
    class ColladaFileDecoder : CoreDecoderBase
    {
        public override bool Open(ICoreWorkbench bench, string filename, bool selectCreatedSurface)
        {
            return false;
        }
    }
}
