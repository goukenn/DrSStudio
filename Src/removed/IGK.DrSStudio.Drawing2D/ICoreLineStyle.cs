

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: ICoreLineStyle.cs
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
file:ICoreLineStyle.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace IGK.DrSStudio.Drawing2D
{
    using IGK.ICore;using IGK.DrSStudio.WinUI.Configuration;
    /// <summary>
    /// represent the line style
    /// </summary>
    public interface ICoreLineStyle : ICoreWorkingConfigurableObject
    {
        /// <summary>
        /// get the path name of the anchor
        /// </summary>
        string DisplayName { get; }
        /// <summary>
        /// Get the dash style
        /// </summary>
        enuDashStyle Style { get; }
        /// <summary>
        /// get the unit
        /// </summary>
        CoreUnit[] Units { get; }
        /// <summary>
        /// get or set the offset
        /// </summary>
        float DashOffset { get; set; }
    }
}

