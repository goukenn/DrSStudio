

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: ICoreProjectInfoActions.cs
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
file:ICoreProjectInfoActions.cs
*/
using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
namespace IGK.ICore.Codec
{
    using IGK.ICore;using IGK.ICore.Actions ;
    public interface ICoreProjectActions : IEnumerable, ICoreCountEnumerable
    {        
        void Add(ICoreProjectAction action);
        void Remove(ICoreProjectAction actions);
    }
}

