using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.GS
{

    /// <summary>
    /// dummy implementatation of IDataReader. not yet used.
    /// </summary>
    public class GSDataRowReader : IDataReader
    {
        IGSDataQueryResult m_result;
        private int m_offsetindex;
        internal GSDataRowReader(IGSDataQueryResult result)
        {
            this.m_result = result;
        }

        public static IDataReader Create(IGSDataQueryResult p)
        {
            
            if (p == null)
                return null;
            return new GSDataRowReader(p);
        }

        public string GetName(int i)
        {//return name 
            return m_result.Columns[i];
        }
        public virtual  Type GetFieldType(int i)
        {
            return typeof(string);
        }
        public int FieldCount
        {
            get { return this.m_result.Columns.FieldCount; }
        }

        public bool NextResult()
        {
            //if (m_offsetindex < this.m_result.Count)
            //{
            //    m_offsetindex++;
            //    return true;
            //}
            return false;
        }

        public bool Read()
        {
            if (m_offsetindex < this.m_result.RowCount)
            {
                return true;
            }
            return false;
        }
        public System.Data.DataTable GetSchemaTable()
        {
            return null;
        }

        public void Close()
        {
            
        }

        public int Depth
        {
            get { return 0; }
        }

        public bool IsClosed
        {
            get { return false; }
        }

        public string GetString(int i)
        {
             return  this.m_result.GetRowAt(this.m_offsetindex)[this.GetName (i)];
        }

        public virtual int RecordsAffected
        {
            get { return 0; }
        }

        public void Dispose()
        {
         
        }


        public virtual bool GetBoolean(int i)
        {
            return false;
        }

        public virtual byte GetByte(int i)
        {
            return 0;
        }

        public virtual long GetBytes(int i, long fieldOffset, byte[] buffer, int bufferoffset, int length)
        {
            return 0;
        }

        public virtual char GetChar(int i)
        {
            return '0';
        }

        public long GetChars(int i, long fieldoffset, char[] buffer, int bufferoffset, int length)
        {
            return 0;
        }

        public IDataReader GetData(int i)
        {
            return null;
        }

        public string GetDataTypeName(int i)
        {
            return null;
        }

        public DateTime GetDateTime(int i)
        {
            return DateTime.Now;
        }

        public decimal GetDecimal(int i)
        {
            return 0.0M;
        }

        public double GetDouble(int i)
        {
            return 0.0;
        }

        public float GetFloat(int i)
        {
            return 0.0f;
        }

        public Guid GetGuid(int i)
        {
            return Guid.Empty;
        }

        public short GetInt16(int i)
        {
            return (short)0;
        }

        public int GetInt32(int i)
        {
            return 0;
        }

        public long GetInt64(int i)
        {
            return (long)0;
        }

        public int GetOrdinal(string name)
        {
            return 0;
        }

  

        public object GetValue(int i)
        {
            return null;
        }

        public int GetValues(object[] values)
        {
            //charge la ligne
            int i = 0;
            foreach(var r in  this.m_result.GetRowAt(this.m_offsetindex))
            {
                values[i] = ((KeyValuePair<string,object>)r).Value;
                i++;
            }
            this.m_offsetindex++;
            return 0;// i;
        }

        public virtual bool IsDBNull(int i)
        {
            return false;
        }

        public object this[string name]
        {
            get {
                return null;
            }
        }

        public object this[int i]
        {
            get {
                return null;
            }
        }

       
    }
}
