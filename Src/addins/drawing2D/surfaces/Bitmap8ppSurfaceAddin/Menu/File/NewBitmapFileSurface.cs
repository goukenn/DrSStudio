
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bitmap8BppSurfaceAddin.Menu.File
{
    using IGK.ICore;
    using IGK.ICore.Menu;
    using Bitmap8BppSurfaceAddin.WinUI;
    [CoreMenu ("File.New._8BppBitmapSurface", 20)]
    class NewBitmapFileSurface : CoreApplicationMenu
    {
        protected override bool PerformAction()
        {
            var c = new Bitmp8BppSurface();
            this.Workbench.AddSurface(c, true );
            return false;
        }
    }
}
