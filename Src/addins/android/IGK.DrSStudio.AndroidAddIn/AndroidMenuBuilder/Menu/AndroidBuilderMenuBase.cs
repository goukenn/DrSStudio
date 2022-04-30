
using IGK.ICore;using IGK.DrSStudio.Android.AndroidMenuBuilder.WinUI;
using IGK.DrSStudio.Android.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.DrSStudio.Android.AndroidMenuBuilder.Menu
{
    public class AndroidBuilderMenuBase : AndroidMenuBase
    {
        public new AndroidMenuBuilderSurface CurrentSurface {
            get {
                return base.CurrentSurface as AndroidMenuBuilderSurface;
            }
        }
        protected override bool IsEnabled()
        {
            return this.CurrentSurface != null;
        }
        protected override bool IsVisible()
        {
            return this.CurrentSurface != null;
        }
    }
}
