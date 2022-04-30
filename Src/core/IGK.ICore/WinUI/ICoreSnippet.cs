

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: ICoreSnippet.cs
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
file:ICoreSnippet.cs
*/

﻿using System;
using System.Collections.Generic;
using System.Text;
namespace IGK.ICore.WinUI
{
    public interface ICoreSnippet : IDisposable 
    {
        ICoreWorkingMecanism Mecanism { get; }
        /// <summary>
        /// get the current demand
        /// </summary>
        int Demand { get; }
        /// <summary>
        /// get the current snippet
        /// </summary>
        int Index { get; }
        /// <summary>
        /// get or set the shape of the snippet
        /// </summary>
        enuSnippetShape Shape{get; set;}
        /// <summary>
        /// get the current location
        /// </summary>
        Vector2f Location { get; set; }
        /// <summary>
        /// get client rectangle
        /// </summary>
        Rectanglef ClientRectangle { get; }
        /// <summary>
        /// get or set if this snippet is visible
        /// </summary>
        bool Visible { get; set; }
        /// <summary>
        /// get or set if the snippet is enabled
        /// </summary>
        bool Enabled { get; set; }
        /// <summary>
        /// get or set if the snippet is marked
        /// </summary>
        bool Marked { get; set; }
        /// <summary>
        /// get or set the color of  the snippet
        /// </summary>
        Colorf Color { get; set; }
    }
}

