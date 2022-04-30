

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: ICoreParameterConfigCollections.cs
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
file:ICoreParameterConfigCollections.cs
*/

﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Remoting;
using System.Reflection;

namespace IGK.ICore.WinUI.Configuration
{
    /// <summary>
    /// represent configuration collections parameters
    /// </summary>
    public interface ICoreParameterConfigCollections :
        //ICoreParameterContainer,
        System.Collections.IEnumerable 
    {
        /// <summary>
        /// raise when a property changed
        /// </summary>
        event CoreParameterChangedEventHandler PropertyChanged;
        ICoreParamterTabCollections Tabs { get; }
        /// <summary>
        /// get or set the host
        /// </summary>
        ICoreDialogToolRenderer Host { get; set; }
        void Reload(); 
        void Clear();
        /// <summary>
        /// get or set if pro
        /// </summary>
        bool CanCancel { get; set; }
        ICoreParameterGroup this[string name] { get; }
        ICoreParameterTab AddTab(string tabName, string tabCaptionKey);
        ICoreParameterGroup AddGroup(string groupName);
        ICoreParameterGroup AddGroup(string groupName, string captionKey);
        ICoreParameterItem AddItem(string itemName, string captionKey, string group, CoreParameterChangedEventHandler eventHandler);
        ICoreParameterItem AddItem(string itemName, string captionKey, string group, enuParameterType type, CoreParameterChangedEventHandler eventHandler);        
        ICoreParameterItem AddItem(System.Reflection.PropertyInfo propertyInfo, string captionKey, string group);
        //add enum type group
        ICoreParameterItem AddEnumItem(System.Reflection.PropertyInfo propertyInfo, string captionKey, string group);
        ICoreParameterConfigCollections CreateEmptyConfig(object target);
        /// <summary>
        /// remove the group
        /// </summary>
        /// <param name="groupName"></param>
        void RemoveGroup(string groupName);
        /// <summary>
        /// restore the default value
        /// </summary>
        void RestoreDefault();
        /// <summary>
        /// create a status info item
        /// </summary>
        /// <param name="Name">Name for </param>
        /// <returns></returns>
        ICoreParameterStatus CreateStatusItem(string Name);
        /// <summary>
        /// used this method to auto build parameter info. new in version 8.2
        /// </summary>
        /// <param name="objet"></param>
        void BuildParameterInfo(ICoreWorkingConfigurableObject objet);
        /// <summary>
        /// get a registered parameter item
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        ICoreParameterItem GetItem(string id);

        Size2i PreferedSize { get; set; }
    }
}

