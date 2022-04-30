
using IGK.DrSStudio.WebGLEngine.WinUI;
using IGK.ICore;
using IGK.ICore.WinUI;

namespace IGK.DrSStudio.WebGLEngine.Menu
{
    [ICore.Menu.CoreMenu("File.New.WebGLGameProject", 0x20)]
    class NewWebGLGameProjectMenu : ICore.Menu.CoreApplicationMenu
    {
        protected override bool PerformAction()
        {

            var l = (this.Workbench as ICoreWorkbench).FindSurface<WebGLDesignSurface>();
            if (l == null)
            {
                WinUI.WebGLDesignSurface c = new WebGLDesignSurface();
                this.Workbench.AddSurface(c, true);
            }
            else
            {

                l.AddNewScene();
                this.Workbench.SetCurrentSurface( l);
            }
            return false;
        }
    }
}
