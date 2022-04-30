

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: ICoreParameterEntry.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
/*
IGKDEV @ 2008-2016
author: C.A.D . BONDJE DOUE
file:ICoreParameterEntry.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace IGK.ICore.WinUI.Configuration
{
    /// <summary>
    /// represent a basic parameter 
    /// </summary>
    public interface ICoreParameterEntry
    {
        /// <summary>
        /// get the immediate parent
        /// </summary>
        ICoreParameterEntry Parent { get; set; }
        /// <summary>
        /// get the parent host
        /// </summary>
        ICoreDialogToolRenderer Host { get; }
        /// <summary>
        /// get the name of the item
        /// </summary>
        string Name { get; }
        /// <summary>
        /// get the caption key of the item
        /// </summary>
        string CaptionKey { get; set;  }
    }
}

