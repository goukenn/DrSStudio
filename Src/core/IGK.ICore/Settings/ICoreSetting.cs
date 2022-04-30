

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: ICoreSetting.cs
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
file:ICoreSetting.cs
*/

﻿using System;
using System.Collections.Generic;
using System.Text;
namespace IGK.ICore.Settings
{
    using IGK.ICore;using IGK.ICore.WinUI.Configuration;
    /// <summary>
    /// represent the setting
    /// </summary>
    public interface ICoreSetting :
        ICoreWorkingObject ,
        System.Collections.IEnumerable,
        ICoreWorkingConfigurableObject 
    {
        bool CanConfigure { get; }
        /// <summary>
        /// get the application setting
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        ICoreSettingValue this[string key]{ get;}
        /// <summary>
        /// get the number of child setting property
        /// </summary>
        int Count { get; }
        /// <summary>
        /// get the setting index
        /// </summary>
        int Index { get; }    
        /// <summary>
        /// get the group index
        /// </summary>
        int GroupIndex { get; }
        /// <summary>
        /// get the name of the group the string
        /// </summary>
        string GroupName { get; }
        /// <summary>
        /// get the image key
        /// </summary>
        string ImageKey { get; }
        /// <summary>
        /// load setting 
        /// </summary>
        /// <param name="setting"></param>
        void Load(ICoreSetting setting);
    }
}

