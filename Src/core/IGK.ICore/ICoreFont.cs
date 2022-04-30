

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: ICoreFont.cs
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
file:ICoreFont.cs
*/

﻿using IGK.ICore;using IGK.ICore.Drawing2D;
using System;
using System.Collections.Generic;
using System.Text;
namespace IGK.ICore
{
    /// <summary>
    /// represent the core font of the object
    /// </summary>
    public interface ICoreFont : 
        ICoreWorkingDefinitionObject ,
        ICoreDisposableObject 
    {
        /// <summary>
        /// get or set the font name
        /// </summary>
        string FontName { get; set; }
        /// <summary>
        /// get or set the font size in pixel. use CoreUnit for unit transformation
        /// </summary>
        float FontSize { get; set; }
        /// <summary>
        /// represent the hot key prefix
        /// </summary>
        enuHotKeyPrefix HotKeyPrefix {get;set;}
        /// <summary>
        /// get or set the font style
        /// </summary>
        enuFontStyle FontStyle { get; set; }
        /// <summary>
        /// get or set the horizontal alignment
        /// </summary>
        enuStringAlignment HorizontalAlignment { get; set; }
        /// <summary>
        /// get or set the vertical alignment
        /// </summary>
        enuStringAlignment VerticalAlignment { get; set; }
        /// <summary>
        /// get or set if this core font allow word wrap
        /// </summary>
        bool WordWrap { get; set; }

        void Copy(ICoreFont def);
        //ICoreFont GetFont();
        event EventHandler FontDefinitionChanged;
        bool IsStyleAvailable(enuFontStyle enuFontStyle);
    }
}

