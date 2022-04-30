

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: WPFSpecialGroupItemAttribute.cs
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
file:WPFSpecialGroupItemAttribute.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace IGK.DrSStudio.WPFSurfaceAddIn.WorkingObjects.Special
{
    [AttributeUsage (AttributeTargets.Class , AllowMultiple = false , Inherited = false )]
    public   class WPFSpecialGroupItemAttribute : WPFElementAttribute
    {
        public override string GroupName
        {
            get
            {
                return WPFConstant.SPECIAL_GROUP;
            }
        }
        public WPFSpecialGroupItemAttribute(string name, Type mecanism):base(name, mecanism )
        {
        }
    }
}
