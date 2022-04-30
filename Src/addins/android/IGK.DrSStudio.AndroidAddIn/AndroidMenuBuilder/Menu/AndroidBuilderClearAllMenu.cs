using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.DrSStudio.Android.AndroidMenuBuilder.Menu
{
    [CoreAndroidBuilderMenu("ClearAlllAddMenu", 1)]
    public class AndroidBuilderClearAlllAddMenu : AndroidBuilderMenuBase
    {
        protected override bool PerformAction()
        {
            this.CurrentSurface.ClearAll();
            return false;
        }
    }
}
