

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: ICoreSettingValue.cs
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
file:ICoreApplicationSetting.cs
*/

﻿using System;
using System.Collections.Generic;
using System.Text;
namespace IGK.ICore.Settings
{
    /// <summary>
    /// represent the base application setting interface
    /// </summary>
    public interface ICoreSettingValue : 
        System.Collections.IEnumerable
    {
        bool HasChild { get; }
        /// <summary>
        /// get the child of the setting
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        ICoreSettingValue this[string name]{get;}
        /// <summary>
        /// the value of this setting
        /// </summary>
        object Value { get; set; }
        /// <summary>
        /// name of this setting
        /// </summary>
        string Name { get; }
        event EventHandler ValueChanged;
        void Add(ICoreSettingValue setting);
        void Remove(ICoreSettingValue setting);
    }
}

