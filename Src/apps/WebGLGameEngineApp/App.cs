using IGK.ICore;
using IGK.ICore.IO;
using IGK.ICore.WinCore;

namespace WebGLGameEngineApp
{
    [CoreApplication("WebGLTest")]
    class App : WinCoreApplication
    {
        public override void Initialize()
        {
            base.Initialize();
            WinCoreService.RegisterIE11WebService();
            CoreUtils.LoadEmbededLibrary(this.GetType());

            PathUtils.CreateDir("Assets");
         
        }
    }
}
