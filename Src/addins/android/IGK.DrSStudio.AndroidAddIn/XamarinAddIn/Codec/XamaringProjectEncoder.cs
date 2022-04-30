
using IGK.DrSStudio.Android.Xamarin.WinUI;
using IGK.ICore.Codec;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.DrSStudio.Android.Xamarin.Codec
{
    [CoreCodec ("Xamaring_android_encoder", "xamarin/androi-project", "csproj")]
    class XamaringProjectEncoder : CoreEncoderBase
    {
        public override bool Save(ICore.WinUI.ICoreWorkingSurface surface, string filename, params ICore.ICoreWorkingDocument[] documents)
        {
            if (surface is XamarinEditorSurface)
            {
                (surface as XamarinEditorSurface).SaveAs(filename);
            }
            return false;
        }
    }
}
