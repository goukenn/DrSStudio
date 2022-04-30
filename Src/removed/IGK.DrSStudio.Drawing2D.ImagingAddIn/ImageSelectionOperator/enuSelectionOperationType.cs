

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: enuSelectionOperationType.cs
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
file:enuSelectionOperationType.cs
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
    using IGK.ICore;using IGK.DrSStudio.Drawing2D.ImageSelection;
    public enum enuSelectionOperationType
    {
        Invert = _ImageSelection.SOP_INVERT,
        MakeTransparent = _ImageSelection.SOP_MAKETRANSPARENT,
        Replace = _ImageSelection.SOP_REPLACE,
        ToggleR2G = _ImageSelection.SOP_R2G,
        ToggleR2B = _ImageSelection.SOP_R2B,
        toggleG2B = _ImageSelection.SOP_G2B,
        Add = _ImageSelection.SOP_ADD,
        AddWithAlpha = _ImageSelection.SOP_ADDWAlpha,
        Subdest = _ImageSelection.SOP_SUBDEST,
        SubdestWAlpha = _ImageSelection.SOP_SUBDESTWAlpha,
        Subsrc = _ImageSelection.SOP_SUBSRC,
        SubsrcWAlpha = _ImageSelection.SOP_SUBSRCWAlpha
    }
}

