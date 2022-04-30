

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: DbManagerOpenerConfig.cs
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
file:DbManagerOpenerConfig.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace IGK.DrSStudio.DataBaseManagerAddIn
{
    using IGK.ICore;using IGK.DrSStudio.WinUI;
    using IGK.DrSStudio.WinUI.Configuration;
    public sealed class DbManagerOpenerConfig : ICoreWorkingConfigurableObject 
    {
        #region ICoreWorkingConfigurableObject Members
      
        public enuParamConfigType GetConfigType()
        {
            return enuParamConfigType.ParameterConfig;
        }
        public IGK.DrSStudio.WinUI.ICoreParameterConfigCollections GetParameters(IGK.DrSStudio.WinUI.ICoreParameterConfigCollections parameters)
        {
            var group = parameters.AddGroup("DataBase");
            group = parameters.AddGroup("Provider");
            return parameters;
        }
        #endregion
        #region ICoreIdentifier Members
        public string Id
        {
            get { throw new NotImplementedException(); }
        }
        #endregion

        ICoreControl ICoreWorkingConfigurableObject.GetConfigControl()
        {
            return null;
        }
    }
}

