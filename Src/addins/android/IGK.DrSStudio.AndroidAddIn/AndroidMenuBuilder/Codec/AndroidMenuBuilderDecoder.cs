
using IGK.ICore;using IGK.DrSStudio.Android.AndroidMenuBuilder.WinUI;
using IGK.ICore.Codec;
using IGK.ICore.WinUI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.DrSStudio.Android.AndroidMenuBuilder.Codec
{
    [CoreCodec("Android Menu Builder", "android/menu+xml", "xml")]
    class AndroidMenuBuilderDecoder : CoreDecoderBase
    {
        public override bool Open(ICoreWorkbench bench, string filename, bool selectCreatedSurface)
        {
            if (File.Exists (filename) && (Path.GetExtension (filename).ToLower() == ".xml"))
            {
                AndroidMenuBuilderSurface c = AndroidMenuBuilderSurface.CreateFromFile(filename );
                if (c!=null)
                {
                    bench.AddSurface(c, selectCreatedSurface);
                }
            }
            return false ;
        }
    }
}
