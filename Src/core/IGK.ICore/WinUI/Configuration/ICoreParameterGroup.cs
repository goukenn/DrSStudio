

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: ICoreParameterGroup.cs
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
file:ICoreParameterGroup.cs
*/

﻿using System;
using System.Collections.Generic;
using System.Text;
namespace IGK.ICore.WinUI.Configuration
{
    using IGK.ICore;
    /// <summary>
    /// represent a parameter group
    /// </summary>
    public interface ICoreParameterGroup : 
        ICoreParameterContainer ,
        System.Collections.IEnumerable ,
        ICoreParameterEntry
    {
        bool IsRootGroup { get; }
        /// <summary>
        /// number of childs
        /// </summary>
        int Count { get; }

        ICoreParameterEntry this[string id] { get;  }
        /// <summary>
        /// get the parent group
        /// </summary>
        ICoreParameterGroup ParentGroup { get; }
        /// <summary>
        /// add configurable object
        /// </summary>
        /// <param name="obj"></param>
        void AddConfigObject(ICoreWorkingConfigurableObject obj);
        /// <summary>
        /// add enum items
        /// </summary>
        /// <param name="itemName">item name</param>
        /// <param name="captionKey">caption key</param>
        /// <param name="enumType">type of the enum</param>
        /// <param name="defaultValue">the default value</param>
        /// <param name="paramChanged">event when parameter changed</param>
        void AddEnumItem(string itemName, string captionKey, Type enumType, object defaultValue, CoreParameterChangedEventHandler paramChanged);
        /// <summary>
        /// add enum items
        /// </summary>
        /// <param name="itemName"></param>
        /// <param name="captionKey"></param>
        /// <param name="dataSource"></param>
        /// <param name="paramChanged"></param>
        void AddEnumItem(string itemName, string captionKey, object dataSource, CoreParameterChangedEventHandler paramChanged);
         /// <summary>
        /// add a new parameter to this collection
        /// </summary>
        /// <param name="d"></param>
        void AddParameters(string name, ICoreParameterConfigCollections d);
        /// <summary>
        /// register action type
        /// </summary>
        /// <param name="name"></param>
        /// <param name="captionkey"></param>
        /// <param name="action"></param>
        void AddActions(ICoreParameterAction action);
        /// <summary>
        /// replace an item in this parameter group
        /// </summary>
        /// <param name="itemName"></param>
        /// <param name="captionKey"></param>
        /// <param name="type"></param>
        /// <param name="eventHandler"></param>
        /// <returns></returns>
        ICoreParameterItem ReplaceItem(string itemName, string captionKey, enuParameterType type, CoreParameterChangedEventHandler eventHandler);

        ICoreParameterItem AddLabel(string p);
    }
}

