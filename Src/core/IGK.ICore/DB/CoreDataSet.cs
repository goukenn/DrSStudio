
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace IGK.ICore.DB
{
    /// <summary>
    /// represent the base Core dataset
    /// </summary>
    public class CoreDataSet : ICoreDataSet 
    {
        private CoreDataSetColumnCollections m_columns;
        private CoreDataSetRowCollections m_rows;
        private string m_errorMessage;
        private bool m_error;


        class CoreDataSetColumnCollections : ICoreDataColumnCollections
        {
            private CoreDataSet togoDataSet;
            private List<String> m_columnsName;

            public CoreDataSetColumnCollections(CoreDataSet togoDataSet)
            {
                
                this.togoDataSet = togoDataSet;
                this.m_columnsName = new List<string>();
            }

            internal void Add(string name)
            {
                if (this.m_columnsName.Contains (name))
                    return;
                this.m_columnsName.Add(name);
            }

            public int FieldCount
            {
                get { return this.m_columnsName.Count; }
            }

            public string this[int index]
            {
                get { 
                    if ((index>=0) && (index < FieldCount )) return this.m_columnsName[index];
                        return string.Empty;
                }
            }

            public System.Collections.IEnumerator GetEnumerator()
            {
                return this.m_columnsName.GetEnumerator();
            }
        }
        class CoreDataSetRowCollections : ICoreDataSetCollections
        {
            private CoreDataSet togoDataSet;
            private List<ICoreDataSet> m_rows;

            public CoreDataSetRowCollections(CoreDataSet togoDataSet)
            {
                
                this.togoDataSet = togoDataSet;
                this.m_rows = new List<ICoreDataSet>();
            }

            internal void Add(ICoreDataSet row)
            {
                this.m_rows.Add(row);
            }

            public int Count
            {
                get { return this.m_rows.Count; }
            }

            public System.Collections.IEnumerator GetEnumerator()
            {
                return this.m_rows.GetEnumerator();
            }
            /// <summary>
            /// get the index. throw out of range if index not in the current range
            /// </summary>
            /// <param name="index"></param>
            /// <returns></returns>
            public ICoreDataSet this[int index]
            {
                get { return this.m_rows[index]; }
            }
        }
        class CoreDataSetRow : CoreDataRowBase 
        {
            private CoreDataSet _dataSet;

            public CoreDataSetRow(CoreDataSet togoDataSet):base()
            {
                
                this._dataSet = togoDataSet;
                //init row with data
            
                foreach (string item in togoDataSet.m_columns)
                {
                    this.m_entries.Add(item, null);
                }
            }


            public void UpdateValue(ICoreDataTable tItem)
            {
                PropertyDescriptorCollection c = TypeDescriptor.GetProperties(tItem);

                foreach (var item in this.m_entries.Keys)
                {
                    this.m_entries[item] = c[item].GetValue(tItem);
                }
            }
        }

        public CoreDataSet()
        {
            this.m_columns = new CoreDataSetColumnCollections(this);
            this.m_rows = new CoreDataSetRowCollections(this);
            this.m_error = false;
            this.m_errorMessage = null ;
        }

        public void AddColumn(string name)
        {
            this.m_columns.Add(name);
        }

        public void AddRow(ICoreDataSet row)
        {
            this.m_rows.Add(row);
        }

        public ICoreDataRow CreateRow()
        {
            return new CoreDataSetRow(this);
        }

        public ICoreDataSetCollections Rows
        {
            get { return this.m_rows; }
        }

        public ICoreDataColumnCollections Columns
        {
            get { return this.m_columns; }
        }

        public int RowCount
        {
            get { return this.m_rows.Count; }
        }

        public int FieldCount
        {
            get { return this.m_columns.FieldCount ; }
        }

        public ICoreDataSet GetRowAt(int p)
        {
            return this.m_rows[p];
        }

        public void SortBy(string p)
        {
            
        }

        public void AddRow(ICoreDataRow row)
        {
            throw new NotImplementedException();
        }

        ICoreDataRow ICoreDataSet.CreateRow()
        {
            throw new NotImplementedException();
        }

        ICoreDataRow ICoreDataQueryResult.GetRowAt(int p)
        {
            throw new NotImplementedException();
        }

        public int AffectedRow
        {
            get { return 0; }
        }

        public bool Error
        {
            get { return this.m_error; }
        }

        public string ErrorMessage
        {
            get { return this.m_errorMessage; }
        }

        ICoreDataRowCollections ICoreDataQueryResult.Rows
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        ICoreDataColumnCollections ICoreDataQueryResult.Columns
        {
            get
            {
                throw new NotImplementedException();
            }
        }
    }
}
