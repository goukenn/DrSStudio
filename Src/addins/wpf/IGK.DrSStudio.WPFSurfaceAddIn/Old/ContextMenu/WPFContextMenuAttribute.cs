

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: WPFContextMenuAttribute.cs
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
file:WPFContextMenuAttribute.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace IGK.DrSStudio.WPFSurfaceAddIn.ContextMenu
{
    [AttributeUsage (AttributeTargets.Class , Inherited =false , AllowMultiple = false )]
    public class WPFContextMenuAttribute : IGK.DrSStudio.ContextMenu.CoreContextMenuAttribute 
    {
        public WPFContextMenuAttribute(string name, int index)
            : base(
            string.Format ("{0}.{1}",WPFConstant .WPF_CTXMENU,name),
            index )
        {
            this.CaptionKey = name;
        }
    }
}

