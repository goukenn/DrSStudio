

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: DbDataPropertyAction.cs
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
file:DbDataPropertyAction.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using IGK.ICore;using IGK.DrSStudio.WinUI;
namespace IGK.DrSStudio.DataBaseManagerAddIn.Menu.Data
{
    [DbManagerMenu("Data.Property",DbManagerConstant.DB_MENU_PROPERTY_INDEX)]
    class DbDataPropertyAction : DbManagerBaseMenu , 
        IGK.DrSStudio.WinUI.Configuration.ICoreWorkingConfigurableObject 
    {
        protected override bool PerformAction()
        {
            DataSet v_dataset = this.CurrentSurface.DataSet;
            if (v_dataset != null)
            {
                Workbench.ConfigureWorkingObject(this);
            }
            return base.PerformAction();
        }
        #region ICoreWorkingConfigurableObject Members
        public  ICoreControl GetConfigControl()
        {
            return null;
        }
        public IGK.DrSStudio.WinUI.Configuration.enuParamConfigType GetConfigType()
        {
            return IGK.DrSStudio.WinUI.Configuration.enuParamConfigType.ParameterConfig;
        }
        public IGK.DrSStudio.WinUI.ICoreParameterConfigCollections GetParameters(IGK.DrSStudio.WinUI.ICoreParameterConfigCollections parameters)
        {
            parameters.BuildParameterInfo(this);
            return parameters;
        }
        #endregion
    }
}

