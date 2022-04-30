

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: ICoreParameterIContainer.cs
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
file:ICoreParameterIContainer.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
namespace IGK.ICore.WinUI.Configuration
{
    /// <summary>
    /// represent a Core Parameter Container
    /// </summary>
    public interface ICoreParameterContainer
    {
        //add property item to default value
        ICoreParameterItem AddItem(PropertyInfo propertyInfo);
        /// <summary>
        /// add property Info by requested the default configuration type
        /// </summary>
        /// <param name="propertyInfo"></param>
        /// <param name="requestedType"></param>
        /// <returns></returns>
        ICoreParameterItem AddItem(PropertyInfo propertyInfo, enuParameterType requestedType);        
        ICoreParameterItem AddItem(PropertyInfo name, string captionkey);
        ICoreParameterItem AddItem(string name, string captionkey);
        ICoreParameterItem AddItem(string name, string captionkey, ICoreControl control);

        void AddItem(ICoreParameterItem item);
         /// <summary>
        /// add item width name to 
        /// </summary>
        /// <param name="itemName"></param>
        /// <param name="captionKey"></param>
        /// <param name="eventHandler"></param>
        /// <returns></returns>
        ICoreParameterItem AddItem(string itemName, string captionKey, CoreParameterChangedEventHandler eventHandler);
        ICoreParameterItem AddItem(string itemName, string captionKey, enuParameterType type, CoreParameterChangedEventHandler eventHandler);
        ICoreParameterItem AddItem(string itemName, string captionKey, object defaultValue, enuParameterType type, CoreParameterChangedEventHandler eventHandler);
        ICoreParameterItem AddTrackbar(string itemName, string captionkey, int begin, int end, int defaulValue, CoreParameterChangedEventHandler PROC);
        ICoreParameterItem AddTrackbar(System.Reflection.PropertyInfo propertyInfo, int begin, int end, int defaulValue, CoreParameterChangedEventHandler PROC);
    }
}

