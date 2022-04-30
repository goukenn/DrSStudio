using IGK.DrSStudio.Balafon.WinUI;
using IGK.ICore;
using IGK.ICore.Codec;
using IGK.ICore.WinUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.DrSStudio.Balafon.Codec
{
    [CoreCodec("balafon-project", "application/balafon-project", 
        BalafonConstant.NEW_FILENAME_EXT,
        Category=CoreConstant.CAT_FILE)]
    class BalafonEncoder : CoreEncoderBase
    {
        public override bool Save(ICoreWorkingSurface surface, string filename, params ICore.ICoreWorkingDocument[] documents)
        {
            if (surface is BalafonEditorSurface)
            {
                (surface as BalafonEditorSurface).SaveAs(filename);
                return true;
            }
            return false;
        }
    }
}
