

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: AndroidSurfaceAttribute.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.DrSStudio.Android
{
    
using IGK.ICore;

    [AttributeUsage (AttributeTargets.Class , AllowMultiple =false , Inherited=false )]
    public class AndroidSurfaceAttribute : CoreSurfaceAttribute 
    {
        public override string NameSpace
        {
            get
            {
                return AndroidConstant.NAME_SPACE;
            }
        }
        public AndroidSurfaceAttribute(string name):base(name )
        {
            this.EnvironmentName = AndroidConstant.ENVIRONMENT;
        }
    }
}
