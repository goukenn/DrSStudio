

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: WPF3DElementCategoryAttribute.cs
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
file:WPF3DElementCategoryAttribute.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace IGK.DrSStudio.WPFSurfaceAddIn.WorkingObjects.WPF3DObjects
{
    [AttributeUsage (AttributeTargets.Class , AllowMultiple = false , Inherited = false )]
    /// <summary>
    /// 
    /// </summary>
    public class WPF3DElementCategoryAttribute : WPFElementAttribute  
    {
        public override string GroupName
        {
            get
            {
                return "WPF3DElement";
            }
        }
        public WPF3DElementCategoryAttribute(string name, Type mecanism):base(name , mecanism )
        {
        }
    }
}

