using IGK.ICore.Codec;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IGK.ICore;
using IGK.ICore.WinUI;
using IGK.ICore.Drawing2D;

namespace IGK.DrSStudio.Codec
{
    [CoreCodec("Zlib gkds Encoder", "gkds/zip-lib", "zgkds",
        Category = CoreConstant.CAT_PICTURE + ";" + CoreConstant.CAT_FILE)]
    class LibZGkdsEncoder : CoreEncoderBase
    {
        public override bool Save(ICoreWorkingSurface surface, string filename, params ICoreWorkingDocument[] documents)
        {
            var s = CoreSystemServices.GetServiceByName("zLibEncoder") as  ICoreZLibService;
            if (s != null)
            {
                return s.Encode(filename, documents);
            }
            else {
                CoreMessageBox.NotifyMessage("zLibEncoder Service", "error.noService.found_1".R("zLibEncoder"));
            }
            return false;
        }
    }
}
