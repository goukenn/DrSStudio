using IGK.DrSStudio.WebGLEngine.Settings;

namespace IGK.DrSStudio.WebGLEngine.WinUI
{
    internal class WebGLDesignSceneSetting
    {
        private WebGLDesignSurface webGLDesignSurface;

        public WebGLDesignSceneSetting(WebGLDesignSurface webGLDesignSurface)
        {
            this.webGLDesignSurface = webGLDesignSurface;
        }
        public string SceneBackgroundColor =>
            WebGLDesignSettings.SceneBackgroundColor.ToString (true);

        public WebGLDesignSurface DesignScene => webGLDesignSurface;
    }
}