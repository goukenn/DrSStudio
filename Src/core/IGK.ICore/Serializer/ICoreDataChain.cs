

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: ICoreDataChain.cs
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
file:ICoreDataChain.cs
*/
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace IGK.ICore
{
    public interface  ICoreDataChain : IEnumerable 
    {
        ICoreDataEntityCreator EntityCreator { get; set; }
        ICoreDataEntity GetEntity(string name);
        void Add(ICoreDataEntity entity);
        void Remove(ICoreDataEntity entity);
        bool LoadFile(string filename);
        string Render();
        void SaveTo(string filename);
    }
}

