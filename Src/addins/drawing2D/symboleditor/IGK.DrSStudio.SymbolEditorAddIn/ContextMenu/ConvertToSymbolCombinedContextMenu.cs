using IGK.DrSStudio.Drawing2D;
using IGK.DrSStudio.Drawing2D.ContextMenu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.DrSStudio.SymbolEditorAddIn.ContextMenu
{
    [IGKD2DConvertToContextMenu("CombinedPathContextMenu", 0xC001)]
    class ConvertToSymbolCombinedContextMenu :  IGKD2DChildContextMenuBase 
    {
        protected override bool PerformAction()
        {
            Workbench.CallAction(ConverterConstant.MENU_CONVERTTO_COMBINEPATH_ELEMENT);
            return false;
        }
    }
}
