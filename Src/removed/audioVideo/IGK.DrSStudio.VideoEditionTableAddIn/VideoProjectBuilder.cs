

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: VideoProjectBuilder.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
/*
IGKDEV - [2008 - 2014]
author: C.A.D . BONDJE DOUE
file:VideoProjectBuilder.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace IGK.DrSStudio.VideoEditionTableAddIn
{
    using IGK.ICore;using IGK.DrSStudio.WinUI;
    using IGK.DrSStudio.WinUI.Configuration;
    class VideoProjectBuilder : ICoreWorkingConfigurableObject 
    {
        #region ICoreWorkingConfigurableObject Members
        public  ICoreControl GetConfigControl()
        {
            throw new NotImplementedException();
        }
        public enuParamConfigType GetConfigType()
        {
            return enuParamConfigType.ParameterConfig;
        }
        public IGK.DrSStudio.WinUI.ICoreParameterConfigCollections GetParameters(IGK.DrSStudio.WinUI.ICoreParameterConfigCollections parameters)
        {
            return parameters;
        }
        #endregion
        #region ICoreIdentifier Members
        public string Id
        {
            get { return VideoConstant.VIDEOPROJECTBUILDER_ID; }
        }
        #endregion
    }
}

