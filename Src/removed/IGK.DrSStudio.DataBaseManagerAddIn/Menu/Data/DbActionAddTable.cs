

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: DbActionAddTable.cs
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
file:DbActionAddTable.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Data;
namespace IGK.DrSStudio.DataBaseManagerAddIn.Menu.Data
{
    using IGK.ICore;using IGK.DrSStudio.WinUI.Configuration;
    using IGK.DrSStudio.WinUI;
    [DbManagerMenu(
        DbManagerConstant.MENU_ADDNEWTABLE, 
        DbManagerConstant.DB_MENU_ACTION_INDEX, 
        SeparatorBefore = true)]
    class DbActionAddTable : DbManagerBaseMenu , ICoreWorkingConfigurableObject 
    {
        protected override bool PerformAction()
        {
            if (Workbench.ConfigureWorkingObject(this) == enuDialogResult.OK)
            {
                DataTable table = GetTable();
                if (table != null)
                {
                    this.CurrentSurface.DataSet.Tables.Add(table);                    
                }
            }
            return base.PerformAction();
        }
        private DataTable GetTable()
        {
            if (string.IsNullOrEmpty(this.TableName) || (this.CurrentSurface.DataSet == null) || (this.CurrentSurface.DataSet.Tables.Contains(this.TableName)))
                return null;
            DataTable v_table = new DataTable(this.TableName);            
            return v_table;
        }
        #region ICoreWorkingConfigurableObject Members
        public ICoreControl GetConfigControl()
        {
            return null;
        }
        public enuParamConfigType GetConfigType()
        {
            return enuParamConfigType.ParameterConfig;
        }
        public IGK.DrSStudio.WinUI.ICoreParameterConfigCollections GetParameters(IGK.DrSStudio.WinUI.ICoreParameterConfigCollections parameters)
        {
            this.TableName = "TableName";
            Type v_t = GetType ();
            parameters.AddGroup(CoreConstant.PARAM_DEFINITION).AddItem(v_t.GetProperty("TableName"));
            return parameters;
        }
        private string m_TableName;
        /// <summary>
        /// get the table name
        /// </summary>
        public string TableName
        {
            get { return m_TableName; }
            set
            {
                if (m_TableName != value)
                {
                    m_TableName = value;
                }
            }
        }
        #endregion
    }
}

