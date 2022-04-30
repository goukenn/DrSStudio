

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: enuRainbowColor.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
using IGK.ICore.WinCore;
using IGK.ICore;using IGK.ICore.Drawing2D;
/*
IGKDEV @ 2008-2016
author: C.A.D . BONDJE DOUE
file:enuRainbowColor.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace IGK.DrSStudio
{
    /// <summary>
    /// represent web color
    /// </summary>
    public enum enuRainbowColor : uint
    {
        Red = enuCoreWebColors .Red ,
        Orange = enuCoreWebColors.Orange ,
        Yellow = enuCoreWebColors.Yellow , 
        Lime  = enuCoreWebColors.Lime   ,
        Blue = enuCoreWebColors.Blue ,
        Indigo = enuCoreWebColors.Indigo ,
        Violet = enuCoreWebColors.Violet 
    }
}

