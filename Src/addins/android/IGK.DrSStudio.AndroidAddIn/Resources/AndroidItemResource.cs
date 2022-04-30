using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IGK.DrSStudio.Android.Resources
{
    /// <summary>
    /// represent a android menu Items
    /// </summary>
    public class AndroidItemResource : AndroidResourceItemBase
    {
        public AndroidItemResource():base(AndroidConstant.ITEM_TAG )
        {
            this.SetProperty(AndroidResourceItemBase.ATTRIBUTENAME, "MenuItem");
        }
        public AndroidMenuResource ParentSubMenu { get; set; }
    }
}
