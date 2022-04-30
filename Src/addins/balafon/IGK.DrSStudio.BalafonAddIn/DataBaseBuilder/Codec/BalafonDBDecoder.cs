
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.DrSStudio.Balafon.DataBaseBuilder.Codec
{
    using IGK.ICore.Codec;
    using IGK.ICore.WinUI;
    using IGK.ICore.Xml;
    using IGK.ICore;
    using IGK.ICore.WinCore ;
    using IGK.DrSStudio.Balafon.DataBaseBuilder.WinUI;

    [CoreCodec("Balafon DB Decoder", "application/xml-balafon", "xml")]
    class BalafonDBDecoder : CoreDecoderBase
    {

        public override bool Open(ICoreWorkbench bench, string filename, bool selectCreatedSurface)
        {
            if (File.Exists(filename))
            {
             var e = CoreXmlElement.LoadFile(filename);             
             if (e != null)
             {
                 BalafonDBBEditorSurface v_s = BalafonDBBEditorSurface.CreateFrom(e, filename);          
                 bench.AddSurface(v_s, selectCreatedSurface);
                 return true;
             }
            }
            return false;
        }
    }
}
