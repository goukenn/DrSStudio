

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: CoreExtensionAttribute.cs
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
file:CoreExtensionAttribute.cs
*/
using System;
using System.Collections.Generic;
using System.Text;
namespace IGK.ICore
{
    [AttributeUsage  (AttributeTargets.Class , AllowMultiple  =false , Inherited = false )]
    public class CoreExtensionAttribute : CoreWorkingObjectAttribute 
    {
        public CoreExtensionAttribute(string id):base(id)
        {
        }
    }
}

