

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: ICoreParameterGroupCollections.cs
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
file:ICoreParameterGroupCollections.cs
*/
using System;
using System.Collections.Generic;
using System.Text;
namespace IGK.ICore.WinUI.Configuration
{
    public interface ICoreParameterGroupCollections : System.Collections.IEnumerable 
    {
        /// <summary>
        /// Get the number of groups
        /// </summary>
        int Count { get; }
        ICoreParameterGroup this[string key] { get; }
        void Add(ICoreParameterGroup group);
        bool ContainsKey(string name);
        /// <summary>
        /// clear the group
        /// </summary>
        void Clear();
        /// <summary>
        /// remove groups by name
        /// </summary>
        /// <param name="p"></param>
        void Remove(string name);
    }
}

