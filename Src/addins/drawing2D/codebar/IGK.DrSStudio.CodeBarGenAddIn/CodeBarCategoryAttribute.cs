

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: CodeBarCategoryAttribute.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/

using IGK.ICore;using IGK.ICore.Drawing2D;
/*
IGKDEV @ 2008-2016
author: C.A.D . BONDJE DOUE
file:CodeBarCategoryAttribute.cs
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
    [AttributeUsage (AttributeTargets.Class , Inherited=false , AllowMultiple = false )]
    public class CodeBarCategoryAttribute : Core2DDrawingStandardElementAttribute 
    {       
        public CodeBarCategoryAttribute(string name, Type mecanism):base(name , mecanism )
        {
        }
        /// <summary>
        /// same environment target as Standart item
        /// </summary>
        public override string Environment
        {
            get
            {
                return base.Environment;
            }
        }
        public override string GroupName
        {
            get
            {
                return CodeBarConstant.CAT_NAME;
            }
        }
    }
}

