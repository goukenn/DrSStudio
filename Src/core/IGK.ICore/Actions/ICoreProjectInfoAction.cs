

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: ICoreProjectInfoAction.cs
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
file:ICoreProjectInfoAction.cs
*/
using System;
using System.Collections.Generic;
using System.Text;
namespace IGK.ICore.Actions
{
    using IGK.ICore;using IGK.ICore.Codec ;
    public interface ICoreProjectAction : ICoreAction 
    {        
        ICoreProject ProjectInfo { get; set; }
        bool Visible { get; set; }
        bool Enabled { get; set; }
    }
}

