using IGK.WebGLLib;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;

namespace IGK.DrSStudio.WebGLEngine.WinUI
{
    [ComVisible(true)]
    public class WebGLDesignScriptProxy
    {
        private WebGLDesignSurface webGLDesignSurface;

        public WebGLDesignScriptProxy(WebGLDesignSurface webGLDesignSurface)
        {
            this.webGLDesignSurface = webGLDesignSurface;
        }

        public void Log(string tag, string message) {
            switch (tag) {
                case "debug":
                default:
                System.Diagnostics.Debug.WriteLine($"{message}");
                    break;
            }
            var s = $"$igk('#debug').setHtml('{JSUtils.EscapeString(message)}');";
            
            this.webGLDesignSurface.EvalCall(s);
        }
    }
}