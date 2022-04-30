using IGK.ICore.Drawing2D.Menu;
using IGK.ICore.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.DrSStudio.Drawing2D.Menu
{

    /// <summary>
    /// represent path root model
    /// </summary>
    [CoreMenu("Path", 0x50)]
    sealed class PathElementMenu : Core2DDrawingMenuBase
    {
        ///<summary>
        ///public .ctr
        ///</summary>
        public PathElementMenu()
        {

        }
        protected override void InitMenu()
        {
            base.InitMenu();
            this.IsRootMenu = true;
        }
    }
}
