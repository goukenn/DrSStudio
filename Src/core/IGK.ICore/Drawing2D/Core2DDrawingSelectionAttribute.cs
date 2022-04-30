

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: Core2DDrawingSelectionAttribute.cs
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
file:Core2DDrawingSelectionAttribute.cs
*/

﻿using System;
using System.Collections.Generic;
using System.Text;
namespace IGK.ICore.Drawing2D
{
    public class Core2DDrawingSelectionAttribute : 
        Core2DDrawingGroupAttribute 
    {
        public override string GroupName { 
            get { return "Selection"; } }
        public override string Environment
        {
            get { return CoreConstant.DRAWING2D_ENVIRONMENT; }
        }
        public Core2DDrawingSelectionAttribute(string name, Type  mecanism):base(name, mecanism  )
        {
        }
        /// <summary>
        /// overrided return the group image key
        /// </summary>
        public override string GroupImageKey
        {
            get { return "Group_Selection"; }
        }
    }
}

