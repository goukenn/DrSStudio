using IGK.ICore.Codec;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IGK.ICore.WinUI;
using IGK.DrSStudio.IO;

namespace IGK.DrSStudio.Codec
{
    [CoreCodec("CR2 Decoder", "canon/raw", "cr2")]
    class CanonRawDecoder : CoreDecoderBase
    {
      
        public override bool Open(ICoreWorkbench bench, string filename, bool selectCreatedSurface)
        {
            if (CanonCR.OpenFile(filename ) is CanonCR f ){

                return true;
            }
            return false;
        }
    }
}
