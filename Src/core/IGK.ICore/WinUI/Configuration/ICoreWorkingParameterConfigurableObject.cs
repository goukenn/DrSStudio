using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IGK.ICore.WinUI.Configuration
{
    public interface  ICoreWorkingParameterConfigurableObject : 
        ICoreWorkingConfigurableObject 
    {
        /// <summary>
        /// parameter to configure
        /// </summary>
        /// <param name="obj"></param>
        void SetConfigParameter(object obj);
    }
}
