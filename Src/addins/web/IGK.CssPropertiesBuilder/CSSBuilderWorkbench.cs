using IGK.ICore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IGK.ICore.WinUI.Common;

namespace IGK.CssPropertiesBuilder
{
    /// <summary>
    /// represent thte CSSbuilder workbench
    /// </summary>
    class CSSBuilderWorkbench : CoreSingleWorkbench
    {
        public override IXCoreColorDialog CreateColorDialog()
        {
            return base.CreateColorDialog();
        }
        
    }
}
