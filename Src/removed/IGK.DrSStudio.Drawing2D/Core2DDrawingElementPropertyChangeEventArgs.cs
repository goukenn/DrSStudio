

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: Core2DDrawingElementPropertyChangeEventArgs.cs
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
file:Core2DDrawingElementPropertyChangeEventArgs.cs
*/
/* 
-------------------------------------------------------------------
Company: IGK-DEV
Author : C.A.D. BONDJE DOUE
SITE : http://www.igkdev.be
Application : DrSStudio
powered by IGK - DEV &copy; 2008-2012
THIS FILE IS A PART OF THE DRSSTUDIO APPLICATION. SEE "License.txt"
FOR MORE INFORMATION ABOUT THE LICENSE
------------------------------------------------------------------- 
*/
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace IGK.DrSStudio.Drawing2D
{
    /// <summary>
    /// represent property type changed for 2D Element
    /// </summary>
    public class Core2DDrawingElementPropertyChangeEventArgs :
        CoreWorkingObjectPropertyChangedEventArgs
    {
        public static readonly Core2DDrawingElementPropertyChangeEventArgs BrushChanged = new Core2DDrawingElementPropertyChangeEventArgs(enu2DPropertyChangedType.BrushChanged);
        public static readonly Core2DDrawingElementPropertyChangeEventArgs BitmapChanged = new Core2DDrawingElementPropertyChangeEventArgs(enu2DPropertyChangedType.BitmapChanged);
        /// <summary>
        /// Get the property changed type
        /// </summary>
        public new enu2DPropertyChangedType ID
        {
            get { return (enu2DPropertyChangedType)base.ID; }
        }
        public Core2DDrawingElementPropertyChangeEventArgs(enu2DPropertyChangedType type)
            : base((int)type, null)
        {
        }
    }
}

