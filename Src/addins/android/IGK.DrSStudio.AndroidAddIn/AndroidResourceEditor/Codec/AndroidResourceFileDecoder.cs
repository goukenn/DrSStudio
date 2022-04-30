
using IGK.ICore;
using IGK.DrSStudio.Android.WinUI;
using IGK.ICore.Codec;
using IGK.ICore.WinUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.DrSStudio.Android.Codec
{
    [CoreCodec ("Android Resource Decoder", "android/xml-resources", "xml")]
    public class AndroidResourceFileDecoder : CoreDecoderBase
    {
        public override bool Open(ICoreWorkbench bench, string filename, bool selectCreatedSurface)
        {

            AndroidResourceEditorSurface c = new AndroidResourceEditorSurface();
            c.OpenFile(filename);
            bench.AddSurface(c , true );
            return false;
        }
    }
}
