using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace IGK.DrSStudio.Resources
{
    public static  class Keys
    {
        public static readonly string button_120x24;


        static Keys() {
            foreach (var item in MethodInfo.GetCurrentMethod().DeclaringType .GetFields(BindingFlags.Static | BindingFlags.Public ))
            {
                item.SetValue(null, item.Name);
            }
        }
    }
}
