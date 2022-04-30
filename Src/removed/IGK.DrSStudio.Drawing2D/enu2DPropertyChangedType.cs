

/*
IGKDEV @ 2008 - 2014
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
IGKDEV - [2008 - 2014]
author: C.A.D . BONDJE DOUE
file:enu2DPropertyChangedType.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace IGK.DrSStudio.Drawing2D
{
    /// </summary>
    public enum enu2DPropertyChangedType
    {
        DefinitionChanged = enuPropertyChanged.Definition,
        IdChanged = enuPropertyChanged.Id,
        SizeChanged = 3,
        MatrixChanged = 4,
        BrushChanged = 5,
        BitmapChanged = 6,
        LayerChanged = 7,
        FontChanged = 8
    }
}

