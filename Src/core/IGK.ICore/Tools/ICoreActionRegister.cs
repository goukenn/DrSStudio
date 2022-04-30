

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: ICoreActionRegister.cs
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
file:ICoreActionRegister.cs
*/
using IGK.ICore;using IGK.ICore.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace IGK.ICore.Tools
{
    /// <summary>
    /// Represent a register actions tools
    /// </summary>
    public interface  ICoreActionRegisterTool : ICoreTool 
    {
        void AddFilterMessage(ICoreFilterMessageAction regActions);
        void RemoveFilterMessage(ICoreFilterMessageAction regActions);
    }
}

