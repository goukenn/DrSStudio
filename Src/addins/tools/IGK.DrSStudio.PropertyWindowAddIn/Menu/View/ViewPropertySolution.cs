

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: ViewPropertySolution.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
using IGK.ICore.WinCore;
using IGK.ICore;
using IGK.ICore.Codec;
using IGK.ICore.Menu;
using IGK.ICore.WinUI;
using IGK.ICore.Drawing2D;
using IGK.ICore.Drawing2D.Menu;
using IGK.ICore.Drawing2D.WinUI;
using IGK.DrSStudio;
using IGK.DrSStudio.Menu;
using IGK.DrSStudio.WinUI;
using IGK.DrSStudio.Drawing2D;
using IGK.DrSStudio.Drawing2D.WinUI;
/*
IGKDEV @ 2008-2016
author: C.A.D . BONDJE DOUE
file:ViewPropertySolution.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace IGK.DrSStudio.PropertyWindowAddIn.Menu.View
{
    using IGK.DrSStudio.PropertyWindowAddIn.Tools;
    using IGK.ICore.Tools;
    [DrSStudioMenu("View.PropertyWindow",150,
        Shortcut = enuKeys.Shift | enuKeys.P,
        ShortcutText="X",
        ImageKey = CoreImageKeys.MENU_PROPERTY_WINDOW_GKDS)]
    sealed class ViewPropertySolution :  CoreViewToolMenuBase 
    {
        public ViewPropertySolution():base(PropertyObjectTool.Instance)
        {
        }
    }
}

