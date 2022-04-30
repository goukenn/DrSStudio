

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: UXDbManagerGridView.cs
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
file:UXDbManagerGridView.cs
*/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using IGK.ICore;using IGK.DrSStudio.WinUI;
namespace IGK.DrSStudio.DataBaseManagerAddIn.WinUI
{
    [IGK.DrSStudio.CoreSurface("{46D4814A-8166-4bf2-ACD8-BCDDFC14F7E5}",
        EnvironmentName = DbManagerConstant.DB_ENVIRONMENT)]
    public partial class UXDbManagerGridView : IGKXWinCoreWorkingSurface, IDbManagerSurface
    {
        public UXDbManagerGridView()
        {
            InitializeComponent();
            this.DataSetChanged += new EventHandler(_DataSetChanged);
            this.c_dataDisplay.SelectTableChanged += new EventHandler(_SelectTableChanged);
            this.c_dataGridView.SelectionChanged += new EventHandler(c_dataGridView_SelectionChanged);
            this.m_DataSet = new DataSet("Surface");
        }
        void c_dataGridView_SelectionChanged(object sender, EventArgs e)
        {
            if (c_dataGridView.SelectedColumns.Count == 1)
            {
                this.m_SelectedColumn = this.SelectedTable .Columns [ this.c_dataGridView.SelectedColumns[0].Name];
            }
        }
        void _SelectTableChanged(object sender, EventArgs e)
        {
            this.c_dataGridView.DataMember =
                this.c_dataDisplay.SelectTableName;
            this.m_SelectedTable = this.DataSet.Tables[this.c_dataDisplay.SelectTableName];
        }
        void _DataSetChanged(object sender, EventArgs e)
        {
            this.c_dataGridView.DataSource  = this.DataSet;
            int i = 0;
            this.c_dataGridView.DataMember = this.DataSet.Tables[i].TableName;
            this.UpdateDataDisplay();
        }
        private void UpdateDataDisplay()
        {
            this.c_dataDisplay.Tables = DbUtils.GetTables(this.DataSet.Tables);
        }
        ///<summary>
        ///raise the EventName 
        ///</summary>
        protected virtual void OnEventName(CoreWorkingObjectPropertyChangedEventArgs e)
        {
            OnPropertyChanged(CoreWorkingObjectPropertyChangedEventArgs.Definition);
        }
        #region IDbManagerSurface Members
        private DataSet m_DataSet;
        /// <summary>
        /// get the data set attached to this surface
        /// </summary>
        public DataSet DataSet
        {
            get { return this.m_DataSet; }
            internal protected set {
                if (this.m_DataSet != value)
                {
                    if (this.m_DataSet != null) UnregisterDataSet();
                    this.m_DataSet = value;
                    if (this.m_DataSet != null) RegisterDataSet();
                    OnDataSetChanged(EventArgs.Empty);
                }
            }
        }
        private void RegisterDataSet()
        {
            this.DataSet.Tables.CollectionChanged += new CollectionChangeEventHandler(Tables_CollectionChanged);
        }
        private void UnregisterDataSet()
        {
            this.DataSet.Tables.CollectionChanged -= new CollectionChangeEventHandler(Tables_CollectionChanged);
        }
        void Tables_CollectionChanged(object sender, CollectionChangeEventArgs e)
        {
            this.UpdateDataDisplay();
        }
        public event EventHandler DataSetChanged;
        protected virtual  void OnDataSetChanged(EventArgs eventArgs)
        {
            if (this.DataSetChanged != null)
                this.DataSetChanged(this, eventArgs);
        }
        public void LoadXMLFile(string xmlfile)
        {
            if (System.IO.File.Exists(xmlfile))
            {
                DataSet v_dataSet = null;
                try
                {
                    v_dataSet = new DataSet();
                    v_dataSet.ReadXml(xmlfile);
                    this.DataSet = v_dataSet;
                }
                catch (Exception Exception){
                    IGK.DrSStudio.WinUI.CoreExceptionManager.ShowException(Exception);
                }
            }
        }
        #endregion
        #region IDbManagerSurface Members
        private DataColumn m_SelectedColumn;
        private DataTable m_SelectedTable;
        public DataColumn SelectedColumn
        {
            get { return this.m_SelectedColumn ; }
        }
        public DataTable SelectedTable
        {
            get { return this.m_SelectedTable; }
        }
        #endregion
    }
}

