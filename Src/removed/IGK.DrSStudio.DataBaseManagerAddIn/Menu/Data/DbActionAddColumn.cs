

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: DbActionAddColumn.cs
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
file:DbActionAddColumn.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
namespace IGK.DrSStudio.DataBaseManagerAddIn.Menu.Data
{
    using IGK.ICore;using IGK.DrSStudio.WinUI.Configuration;
    using IGK.DrSStudio.WinUI;
    [DbManagerMenu(DbManagerConstant.MENU_ADDCOLUMNTABLE , 
        DbManagerConstant.DB_MENU_ACTION_INDEX+1)]
    class DbActionAddColumn: DbManagerBaseMenu , ICoreWorkingConfigurableObject 
    {
        protected override bool PerformAction()
        {
            DataTable v_table = this.CurrentSurface.SelectedTable ;
            if (v_table == null)
                return false ;
            if (Workbench.ConfigureWorkingObject(this) == enuDialogResult.OK)
            {
                DataColumn v_column = GetTableColumn();
                if (v_column != null)
                {
                    v_table.Columns.Add(v_column);
                    return true;
                }
                return false;
            }
            return base.PerformAction();
        }
        private DataColumn GetTableColumn()
        {
            if (string.IsNullOrEmpty(this.ColumnName) || 
                (this.CurrentSurface .SelectedTable ==null)||
                (this.CurrentSurface.SelectedTable.Columns.Contains(this.ColumnName)))
                return null;
            DataColumn v_column = new DataColumn(this.ColumnName);
            v_column.AllowDBNull = this.DbNull;
            v_column.AutoIncrement = this.AutoIncrement;
            if (v_column.AutoIncrement)
            {
                v_column.AutoIncrementSeed = this.AutoIncrementSeed;
                v_column.AutoIncrementStep = this.AutoIncrementStep;
            }
            v_column.Caption = this.DbColumnCaption;
            v_column.DataType = this.GetDbDataType(this.DbDataType);
            if (v_column.DataType == typeof(String))
            {
                v_column.MaxLength = this.DbMaxLength;
            }
            v_column.Unique = this.DbUnique;            
            return v_column;
        }
        private Type GetDbDataType(enuDbDataType enuDbDataType)
        {
            switch (enuDbDataType)
            {
                case enuDbDataType.Int16:
                    return typeof(Int16);
                case enuDbDataType.Int32:
                    return typeof(Int32);
                case enuDbDataType.Bool:
                    return typeof(Boolean);
                case enuDbDataType.Char:
                    return typeof(Char);
                case enuDbDataType.String:
                    return typeof(String);
                default:
                    break;
            }
            return (typeof(String));
        }
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
            //init default property
            this.ColumnName = "ColumnName";
            this.DbColumnCaption = string.Empty;
            this.AutoIncrement = false;
            this.AutoIncrementSeed = 1;
            this.AutoIncrementStep = 1;
            this.DbNull = false;
            this.DbMaxLength = 255;
            this.DbUnique = false;
            Type v_t = GetType();
            var g = parameters.AddGroup(CoreConstant.PARAM_GROUP_DEFAULT);
            g.AddItem(v_t.GetProperty("ColumnName"));
            var group = parameters.AddGroup("Definition");
            group.AddItem(v_t.GetProperty("DbColumnCaption"));
            group.AddItem(v_t.GetProperty("DbDataType"));
            group.AddItem(v_t.GetProperty("DbMaxLength"));
            group.AddItem(v_t.GetProperty("DBNull"));
            group.AddItem(v_t.GetProperty("DbUnique"));
            group.AddItem(v_t.GetProperty("DbExpression"));
            group = parameters.AddGroup("AutoIncrement");
            group.AddItem(v_t.GetProperty("AutoIncrement"));
            group.AddItem(v_t.GetProperty("AutoIncrementSeed"));
            group.AddItem(v_t.GetProperty("AutoIncrementStep"));
            group = parameters.AddGroup("AutoIncrement");
            return parameters;
        }
        private string m_ColumnName;
        private string m_DbColumnCaption;
        private enuDbDataType  m_DbDataType;
        private int m_DbMaxLength;
        private string m_DbExpression;
        private bool m_DbUnique;
        /// <summary>
        /// get or set if the db is unique
        /// </summary>
        public bool DbUnique
        {
            get { return m_DbUnique; }
            set
            {
                if (m_DbUnique != value)
                {
                    m_DbUnique = value;
                }
            }
        }
        /// <summary>
        /// get the db expression
        /// </summary>
        public string DbExpression
        {
            get { return m_DbExpression; }
            set
            {
                if (m_DbExpression != value)
                {
                    m_DbExpression = value;
                }
            }
        }
        /// <summary>
        /// get the db max length
        /// </summary>
        public int DbMaxLength
        {
            get { return m_DbMaxLength; }
            set
            {
                if (m_DbMaxLength != value)
                {
                    m_DbMaxLength = value;
                }
            }
        }
        /// <summary>
        /// get or set the the dbDataType        
        /// </summary>
        public enuDbDataType  DbDataType
        {
            get { return m_DbDataType; }
            set
            {
                if (m_DbDataType != value)
                {
                    m_DbDataType = value;
                }
            }
        }
        /// <summary>
        /// get or set de default DbColumnCaption
        /// </summary>
        public string DbColumnCaption
        {
            get { return m_DbColumnCaption; }
            set
            {
                if (m_DbColumnCaption != value)
                {
                    m_DbColumnCaption = value;
                }
            }
        }
        public string ColumnName
        {
            get { return m_ColumnName; }
            set
            {
                if (m_ColumnName != value)
                {
                    m_ColumnName = value;
                }
            }
        }
        private bool m_DbNull;
        /// <summary>
        /// allow db Null
        /// </summary>
        public bool DbNull
        {
            get { return m_DbNull; }
            set
            {
                if (m_DbNull != value)
                {
                    m_DbNull = value;
                }
            }
        }
        private bool m_AutoIncrement;
        private int m_AutoIncrementSeed;
        private int m_AutoIncrementStep;
        public int AutoIncrementStep
        {
            get { return m_AutoIncrementStep; }
            set
            {
                if (m_AutoIncrementStep != value)
                {
                    m_AutoIncrementStep = value;
                }
            }
        }
        public int AutoIncrementSeed
        {
            get { return m_AutoIncrementSeed; }
            set
            {
                if (m_AutoIncrementSeed != value)
                {
                    m_AutoIncrementSeed = value;
                }
            }
        }
        public bool AutoIncrement
        {
            get { return m_AutoIncrement; }
            set
            {
                if (m_AutoIncrement != value)
                {
                    m_AutoIncrement = value;
                }
            }
        }
    }
}

