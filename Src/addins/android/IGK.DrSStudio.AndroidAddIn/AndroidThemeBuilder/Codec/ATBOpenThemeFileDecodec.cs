
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IGK.DrSStudio.Android.AndroidThemeBuilder.WinUI;

namespace IGK.DrSStudio.Android.AndroidThemeBuilder.Codec
{
    using IGK.ICore;
    using IGK.DrSStudio.Android.WinUI;
    using IGK.ICore.Codec;
    using IGK.ICore.WinUI;

    [CoreCodec("AndroidThemeDecoder", "text/xml", "xml")]
    public class ATBOpenThemeFileDecodec : CoreDecoderBase
    {
        public override bool Open(ICoreWorkbench bench, string filename, bool selectCreatedSurface)
        {
            ATBThemeFile g =  ATBThemeFile.LoadFile (filename);
            if (g != null) {
                if (g.DefaultPlateForm == null)
                {
                    g.DefaultPlateForm = AndroidSystemManager.GetAndroidTargetByName(IGK.DrSStudio.Android.Settings.AndroidSetting.Instance.DefaultPlatform);//.GetAndroidTargets()?[0];//.GetDE
                }
                var s = AndroidThemeFileEditorSurface.Create (g);
                 bench.AddSurface(s, true);
                return true ;
            }
            return  false ;
        }
    }
}
