

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: ILayoutPanelPageProperty.cs
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
file:ILayoutPanelPageProperty.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace IGK.DrSStudio.WinUI
{    
    public interface ILayoutPanelPageProperty
    {
        /// <summary>
        /// get the tool name
        /// </summary>
        string ToolName { get; }
        /// <summary>
        /// get or set the le location 
        /// </summary>
        Vector2i Location { get; set; }
        /// <summary>
        /// get or set the le location 
        /// </summary>
        Size2i Size { get; set; }
        /// <summary>
        /// get or set the le location 
        /// </summary>
        enuLayoutToolDisplay ToolDisplay { get; set;  }
        event EventHandler SizeChanged;
        event EventHandler LocationChanged;
    }
}

