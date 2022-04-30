using IGK.ICore.Codec;
using IGK.ICore.Xml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.DrSStudio.Android.Xamarin.Codec
{

    using IGK.DrSStudio.Android.Xamarin.WinUI;
    using IGK.ICore;
    using System.IO;

    [CoreCodec ("Xamarin_android_Project", "Xamarin/CsPro", "csproj")]
    class XamarinCsProjDecoder : CoreDecoderBase
    {
        public override bool Open(ICore.WinUI.ICoreWorkbench bench, string filename, bool selectCreatedSurface)
        {
            var v_i  = CoreXmlElement.LoadFile(filename);
            if (v_i == null)
                return false;

            var v_s = new XamarinEditorSurface();
            v_s.LoadProject(Path.GetFileNameWithoutExtension (filename), v_i);
            bench.AddSurface(v_s, selectCreatedSurface);
            return true;
        }
    }
}
