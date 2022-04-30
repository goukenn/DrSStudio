

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: CoreParameterTabGroup.cs
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
file:CoreParameterTabGroup.cs
*/
using System;
using System.Collections.Generic;
using System.Text;
namespace IGK.ICore.WinCore.Configuration
{
    public sealed class CoreParameterTabGroup: CoreParameterGroup 
    {
        public override bool IsRootGroup
        {
            get
            {
                return true;
            }
        }
        public CoreParameterTabGroup(object obj, string name, string captionKey):base(obj , name , captionKey )
        {
        }
    }
}

