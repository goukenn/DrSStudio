

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: enu2DPropertyChangedType.cs
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
file:enu2DPropertyChangedType.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace IGK.ICore.Drawing2D
{
    public enum enu2DPropertyChangedType : int
    {
        DefinitionChanged = enuPropertyChanged.Definition ,
        IdChanged = enuPropertyChanged.Id ,
        MatrixChanged = enuPropertyChanged.Definition + 0x100,
        BrushChanged = enuPropertyChanged.Definition + 0x101,
        LayerChanged = enuPropertyChanged.Definition + 0x102,
        SizeChanged = enuPropertyChanged.Definition + 0x103 ,       
        ViewChanged = enuPropertyChanged.Definition + 0x104   ,
        BitmapChanged = enuPropertyChanged.Definition + 0x105,
        FontChanged = DefinitionChanged + 0x106
    }
}

