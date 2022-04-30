

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: ViewToolPinceauManager.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
/*
IGKDEV - [2008 - 2014]
author: C.A.D . BONDJE DOUE
file:ViewToolPinceauManager.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace IGK.DrSStudio.Drawing2D.Menu.View
{
    [IGK.DrSStudio.Menu.CoreMenu ("View.PinceauToolManager", 410)]
    class ViewToolPinceauManager : IGK.DrSStudio.Drawing2D.Menu.Core2DMenuViewToolBase 
    {
        public ViewToolPinceauManager():base(IGK.DrSStudio.Drawing2D.Tools.ToolPinceauManager.Instance)
        {
        }
    }
}

