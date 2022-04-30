using IGK.DrSStudio.Balafon.WinUI;
using IGK.DrSStudio.Balafon.Xml;
using IGK.ICore.Codec;
using IGK.ICore.WinUI;
using IGK.ICore.Xml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.DrSStudio.Balafon.Codec
{
    using IGK.ICore;
    using IGK.ICore.WinUI;
    using IGK.ICore.WinCore;

    [CoreCodec("balafon-project", "application/balafon-project", BalafonConstant.NEW_FILENAME_EXT)]
    class BalafonDecoder : CoreDecoderBase
    {

        public override bool Open(ICoreWorkbench bench, string filename, bool selectCreatedSurface)
        {
            BalafonProject cp =  IO.Utils.OpenFile (filename);
            if (cp != null) {
                var v_s = new BalafonEditorSurface();
                v_s.LoadProject(cp);
                bench.AddSurface(v_s, selectCreatedSurface);
                return true;
            }
            //CoreXmlElement d = CoreXmlElement.LoadFile(filename);
            //var s = d.getElementsByTagName("BalafonProject")[0] as CoreXmlElement;

            //if (s != null) {
            //    BalafonProject b = new BalafonProject();
            //    b.Load(s);
            //    var v_s = new BalafonEditorSurface();
            //    v_s.LoadProject (b);
            //    bench.AddSurface(v_s, selectCreatedSurface);
            //}
            return false;
        }
    }
}
