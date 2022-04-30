

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: ICoreParameterTab.cs
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
file:ICoreParameterTab.cs
*/
using System;
using System.Collections.Generic;
using System.Text;
namespace IGK.ICore.WinUI
{
    using IGK.ICore;using IGK.ICore.WinUI.Configuration;
    /// <summary>
    /// represent a ICore Parameter for Tab. Must Contains on only groups
    /// </summary>
    public interface ICoreParameterTab
    {
        string Name { get; }
        string CaptionKey { get; }
        ICoreWorkingConfigurableObject Target { get; }
        ICoreParameterGroupCollections Groups { get; }
        ICoreParameterGroup AddGroup(string groupName, string groupCaptionKey);
        ICoreParameterGroup AddConfigObject(ICoreWorkingConfigurableObject element);
    }
}

