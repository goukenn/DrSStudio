

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: ICoreProjectItemContainer.cs
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
file:ICoreProjectItemContainer.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace IGK.ICore.Codec
{
    public interface  ICoreProjectItemContainer : ICoreProjectItem 
    {
        event EventHandler ChildAdded;
        event EventHandler ChildRemoved;
        ICoreProjectItemCollections this[string key] { get; }
        ICoreProjectItem[] GetChildsToArray(string key);
        bool HasChild { get; }
        bool ContainsKey(string key);
        void Add(string key, string value);
        void Add(string key, ICoreProjectItem item);
        void Remove(string key);
        string[] GetKeys();
        /// <summary>
        /// numver of child
        /// </summary>
        int Count { get; }
    }
}

