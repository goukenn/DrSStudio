

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: ICoreExtensionContextCollections.cs
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
file:ICoreExtensionContextCollections.cs
*/
using System;
using System.Collections.Generic;
using System.Text;
namespace IGK.ICore
{
    public interface ICoreExtensionContextCollections : 
        System.Collections.IEnumerable 
    {
        /// <summary>
        /// get the extension context
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        ICoreExtensionContext this[string key] { get; }
        /// <summary>
        /// get the arrays of context name
        /// </summary>
        /// <returns></returns>
        string[] ContextNames();
        /// <summary>
        /// Get the extens count
        /// </summary>
        int Count { get; }
        /// <summary>
        /// extension context
        /// </summary>
        /// <param name="extension"></param>
        void Add(ICoreExtensionContext extension);
        /// <summary>
        /// remove extension context
        /// </summary>
        /// <param name="remove"></param>
        void Remove(ICoreExtensionContext remove);
        /// <summary>
        /// get if this element support extension name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        bool SupportExtension(string name);
        /// <summary>
        /// get if this element support extension
        /// </summary>
        /// <param name="extension"></param>
        /// <returns></returns>
        bool SupportExtension(ICoreExtensionContext extension);
    }
}

