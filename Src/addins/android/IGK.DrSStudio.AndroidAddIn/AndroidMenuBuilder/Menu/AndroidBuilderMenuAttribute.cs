
using IGK.ICore;using IGK.ICore.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IGK.DrSStudio.Android.AndroidMenuBuilder.Menu
{
    /// <summary>
    /// android builder menu
    /// </summary>
    public class CoreAndroidBuilderMenuAttribute : CoreMenuAttribute
    {
        public CoreAndroidBuilderMenuAttribute(string name, int index)
            :
            base(string.Format(AndroidMenuBuilderConstant.MENU_ROOT_MENU +".{0}",name ), index)
        {

        }
    }
}
