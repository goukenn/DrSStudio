

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: Core2DDrawingStandardItemAttribute.cs
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
file:Core2DDrawingStandardItemAttribute.cs
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
    using IGK.ICore;using IGK.DrSStudio.Drawing2D;
    /// <summary>
    /// used to register element on the standard groups
    /// </summary>
    [AttributeUsage (AttributeTargets.Class ,
        AllowMultiple = false,
        Inherited=false )]
    public class Core2DDrawingStandardItemAttribute :
        Core2DDrawingGroupAttribute 
    {
        public override string GroupName
        {
            get { return CoreConstant.DEFAULTGROUPNAME; }
        }
        public override string Environment
        {
            get { return CoreConstant.DRAWING2D_ENVIRONMENT; }
        }
        public Core2DDrawingStandardItemAttribute(string name, Type mecanism):base(name, mecanism )
        {
        }
        public override string GroupImageKey
        {
            get { return "Group_Standard"; }
        }
    }
}

