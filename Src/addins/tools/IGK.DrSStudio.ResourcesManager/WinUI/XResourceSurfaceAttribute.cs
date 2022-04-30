using IGK.ICore.WinCore;
using IGK.ICore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IGK.DrSStudio.ResourcesManager.WinUI
{
    [AttributeUsage (AttributeTargets.Class )]
    public class XResourceSurfaceAttribute : CoreSurfaceAttribute
    {
        public override string NameSpace
        {
            get
            {
                return XResourceConstant.NAMESPACE;
            }
        }
        public XResourceSurfaceAttribute(string name, string guid):base(name ,
            guid)
        {

        }
    }
}
