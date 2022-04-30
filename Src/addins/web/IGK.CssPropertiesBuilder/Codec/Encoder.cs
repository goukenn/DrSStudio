using IGK.ICore.Codec;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.CssPropertiesBuilder.Codec
{
    [CoreCodec ("CssBuilder Encoder", "application/igk-css", "css")]
    class CssBuilderEncoder : CoreEncoderBase
    {
        public override bool Save(ICore.WinUI.ICoreWorkingSurface surface, string filename, params ICore.ICoreWorkingDocument[] documents)
        {
            return false;
        }
    }
}
