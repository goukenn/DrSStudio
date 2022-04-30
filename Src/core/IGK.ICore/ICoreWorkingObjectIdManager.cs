

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: ICoreWorkingObjectIdManager.cs
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
file:ICoreWorkingObjectIdManager.cs
*/
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace IGK.ICore
{
    /// <summary>
    /// interface used to manage element in a document hierarchie. scene-hierarchie.
    /// </summary>
    public interface  ICoreWorkingObjectIdManager : IEnumerable 
    {
        /// <summary>
        /// get maximum number of item
        /// </summary>
        int Count { get; }
        /// <summary>
        /// get element by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        ICoreWorkingObject GetElementById(string id);
        /// <summary>
        /// retrieve all element in this object manager
        /// </summary>
        /// <returns></returns>
        ICoreWorkingObject[] GetAllElements();
        /// <summary>
        /// used to change element id
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="newId"></param>
        /// <returns></returns>
        bool ChangeId(ICoreWorkingObject obj, string newId);
        /// <summary>
        /// register a working object
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        bool Register(ICoreWorkingObject obj);
        /// <summary>
        /// unregister working object
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        bool Unregister(ICoreWorkingObject obj);
        /// <summary>
        /// element removed
        /// </summary>
        event EventHandler<CoreWorkingObjectEventArgs<ICoreWorkingObject>> ElementAdded;
        /// <summary>
        /// element added
        /// </summary>
        event EventHandler<CoreWorkingObjectEventArgs<ICoreWorkingObject>> ElementRemoved;
    }
}

