using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.ICore.DB
{
    public abstract class CoreDataAdapterBase
    {

        private string m_ErrorString;
        static Dictionary<string, string> sm_dataLength;

        static CoreDataAdapterBase()
        {
            sm_dataLength = new Dictionary<string, string>();
            sm_dataLength.Add("int", "8");
            sm_dataLength.Add("varchar", "30");
        }
        /// <summary>
        /// register leng data size
        /// </summary>
        /// <param name="name"></param>
        /// <param name="size"></param>
        protected static void RegisterLength(string name, string size)
        {
            if (sm_dataLength.ContainsKey(name))
            {
                if (string.IsNullOrEmpty(size))
                {
                    sm_dataLength.Remove(name);
                }
                else
                    sm_dataLength[name] = size;
            }
            else if (!string.IsNullOrEmpty(name))
            {
                sm_dataLength.Add(name, size);
            }
        }
        public static bool HaveLength(string name)
        {
            return string.IsNullOrEmpty(name) ? false : sm_dataLength.ContainsKey(name.ToLower());
        }
        public string ErrorString
        {
            get { return m_ErrorString; }
            protected set
            {
                if (m_ErrorString != value)
                {
                    m_ErrorString = value;
                }
            }
        }

        public virtual bool Connect(string connexionString)
        {
            return false;
        }
        public virtual bool Close()
        {
            return false;
        }

        internal protected virtual string GetDefaultType(Type propertyType)
        {
            if (propertyType.IsPrimitive) {
                return "0";
            }
            return null;
        }

        /// <summary>
        /// get the adapter name
        /// </summary>
        public abstract string AdapterName
        {
            get;
        }


        public static CoreDataAdapterBase CreateAdapter(string name)
        {
            Type t = null;
            t = GetAdapterType(name);
            if ((t != null) && t.IsSubclassOf (typeof(CoreDataAdapterBase)))
                return t.Assembly.CreateInstance(t.FullName) as CoreDataAdapterBase;

            return null;
        }

        private static Type GetAdapterType(string name)
        {

            foreach (var item in AppDomain.CurrentDomain.GetAssemblies())
            {
                Type[] d = item.GetTypes();
                try
                {
                    var h = (from s in d where s.Name.ToLower().EndsWith((name + "DataAdapter").ToLower()) select s);
                    if ((h != null) && (h.Count() > 0))
                        return h.First();
                    //.First();
                    //if (h == null) continue;
                    //return h;
                }
                catch
                {
                }
                //from s in item.GetTypes ().Select((o,m) => {} where 
            }
            return null;
        }

        public ICoreDataQueryResult SendQuery(string query)
        {
            return this.SendQuery(query, null);
        }
        public virtual ICoreDataQueryResult SendQuery(string query, string sourceTableName)
        {
            return null;
        }

        public abstract int LastId();

        public abstract ICoreDataQueryResult Insert(Type type, Dictionary<string, object> dictionary);

        public virtual string EscapeString(string p)
        {
            string k = p.Replace("'", "\\'");
            return k;
        }
        public virtual string DecodeString(string data)
        {
            return data;
        }

        public virtual string CreateInsertQuery(string tableName, Dictionary<string, object> t)
        {
            if ((t == null) || (t.Count == 0))
                return string.Empty;
            StringBuilder sb = new StringBuilder();
            StringBuilder q = new StringBuilder();
            StringBuilder e = new StringBuilder();
            bool f = false;
            foreach (KeyValuePair<string, object> item in t)
            {
                if (f)
                {
                    q.Append(",");
                    e.Append(",");
                }
                q.Append(string.Format("`{0}`", item.Key));
                if (item.Value == null)
                {
                    e.Append(string.Format("'{0}'", "NULL"));
                }
                else if (item.Value.GetType().GetProperty(CoreDataConstant.CL_ID) != null)
                {
                    e.Append(string.Format("'{0}'", item.Value.GetType().GetProperty(CoreDataConstant.CL_ID).GetValue(item.Value, null).ToString()));
                }
                else
                    e.Append(string.Format("'{0}'", EscapeString(Convert.ToString(item.Value))));
                f = true;
            }
            sb.Append(string.Format("INSERT INTO `{0}` ({1}) VALUES ({2})", tableName, q.ToString(), e.ToString()));
            return sb.ToString();
        }

        public ICoreDataQueryResult SelectAll<T>() {
            return SelectAll(typeof(T));
        }

        public abstract ICoreDataQueryResult SelectAll(Type type);

        public abstract ICoreDataQueryResult SelectAll(Type type, Dictionary<string, object> dictionary);

        public abstract ICoreDataQueryResult SelectTableInfo(string name);

        public virtual string GetFindQuery(string tablename, Dictionary<string, object> dictionary)
        {
            StringBuilder q = new StringBuilder();
            StringBuilder e = new StringBuilder();
            bool f = false;
            foreach (KeyValuePair<string, object> item in dictionary)
            {

                if (f)
                    e.Append(",");
                if ((item.Value != null) && (item.Value.GetType().GetProperty(CoreDataConstant.CL_ID) != null))
                {
                    e.Append(string.Format("`{0}` LIKE '{1}'", item.Key, item.Value.GetType().GetProperty(CoreDataConstant.CL_ID).GetValue(item.Value, null).ToString()));
                }
                else
                    e.Append(string.Format("`{0}` LIKE '{1}'", item.Key, item.Value == null ? "" : item.Value.ToString()));
                f = true;
            }

            q.Append(string.Format("SELECT * FROM `{0}` WHERE {1}",
                tablename,
                e
                ));
            return q.ToString();
        }

        /// <summary>
        /// get create table query
        /// </summary>
        /// <param name="tableName">table name</param>
        /// <param name="column">list of column</param>
        /// <param name="Description">description</param>
        /// <returns></returns>
        public virtual string GetCreateTableQuery(string tableName, ICoreDataColumnInfo[] column, string Description)
        {
            if (column == null)
                return null;
            string query = "CREATE TABLE IF NOT EXISTS `" + tableName + "`(";
            bool tb = false;
            string primary = ""; //primary key
            string unique = ""; //unique row
            string funique = ""; //unique row
            string findex = string.Empty;

            string s = "";

            Dictionary<int, string> tfunique = new Dictionary<int, string>();
            List<string> v_lcolumns = new List<string>();
            foreach (ICoreDataColumnInfo v in column)
            {
                if (v_lcolumns.Contains(v.clName.ToLower()))
                    continue;
                if (tb)
                    query += ",";
                query += "`" + v.clName + "` ";
                query += v.clType;
                s = v.clType.ToLower();
                if (HaveLength(s))
                {

                    if (v.clTypeLength > 0)
                        query += "(" + v.clTypeLength + ") ";
                    else
                        query += " ";
                }
                else
                    query += " ";
                if (v.clNotNull)
                    query += "NOT NULL ";
                else
                    query += "NULL ";
                if (v.clAutoIncrement)
                    query += "AUTO_INCREMENT ";
                tb = true;

                if (!string.IsNullOrEmpty(v.clDefault))
                {
                    query += "DEFAULT '" + EscapeString(v.clDefault) + "' ";
                }

                if (!string.IsNullOrEmpty(v.clDescription))
                {
                    query += "COMMENT '" + EscapeString(v.clDescription) + "' ";
                }

                if (v.clIsUnique)
                {
                    if (!string.IsNullOrEmpty(unique))
                        unique += ",";

                    unique += "UNIQUE KEY `" + v.clName + "` (`" + v.clName + "`)";
                }
                //to merge all unique column members
                if (v.clIsUniqueColumnMember)
                {
                    if (tfunique.ContainsKey(v.clColumnMemberIndex))
                    {
                        funique = tfunique[v.clColumnMemberIndex];
                    }
                    else
                    {
                        tfunique.Add(v.clColumnMemberIndex, string.Empty);
                        funique = string.Empty;
                    }

                    if (string.IsNullOrEmpty(funique))
                    {
                        funique = "UNIQUE KEY `" + v.clName + "`(`" + v.clName + "`";
                    }
                    else
                        funique += ", `" + v.clName + "`";

                    tfunique[v.clColumnMemberIndex] = funique;
                }
                if (v.clIsPrimary)
                {
                    if (!string.IsNullOrEmpty(primary))
                        primary += ",";
                    primary += "`" + v.clName + "`";
                }
                if (v.clIsIndex && !v.clIsPrimary && !v.clIsUnique)
                {
                    if (!string.IsNullOrEmpty(findex))
                        findex += ",";
                    findex += "KEY `" + v.clName.ToLower() + "_index` (`" + v.clName + "`)";
                }
            }
            if (!string.IsNullOrEmpty(primary))
            {
                query += ", PRIMARY KEY  (" + primary + ") ";
            }
            if (!string.IsNullOrEmpty(unique))
            {
                query += ", " + unique + " ";
            }
            if (!string.IsNullOrEmpty(findex))
            {
                query += ", " + findex + " ";
            }
            if (!string.IsNullOrEmpty(funique))
            {
                funique += ")";
                query += ", " + funique + " ";
            }

            if (string.IsNullOrEmpty(Description))
                query += ");";
            else
                query += ") COMMENT ='" + EscapeString(Description) + "'";
            return query;
        }

        public virtual string GetColumnType(Type type)

        {

            if (type.IsEnum)
                return "Int";
            if (type == typeof(int))
                return "Int";
            if (type == typeof(string))
                return "Text";
            if (type == typeof(bool))
                return "INT";
            if (type == typeof(float))
                return "FLOAT";
            if (type == typeof(double))
                return "DOUBLE";
            if (type == typeof(char))
                return "CHAR";

            //if (type.ImplementInterface(typeof(IGSDataCell)))
            //    return "Int";
            if (type == typeof(DateTime))
                return "DateTime";

            if (type.IsArray)
            {
                throw new NotSupportedException();
            }
            return "Text";
        }

        public virtual string GetUpdateQuery(string table, Dictionary<string, object> values, Dictionary<string, object> condition)
        {

            StringBuilder q = new StringBuilder();
            StringBuilder e = new StringBuilder();
            bool f = false;
            foreach (KeyValuePair<string, object> item in values)
            {

                if (f)
                    e.Append(",");
                //if object is Data Adpter
                if (item.Value is ICoreDataTable)
                {
                    e.Append(string.Format("`{0}`='{1}'", item.Key, item.Value.GetType().GetProperty(CoreDataConstant.CL_ID).GetValue(item.Value, null).ToString()));
                }
                else
                    e.Append(string.Format("`{0}`={1}", item.Key, item.Value == null ?
                        "NULL" : string.Format("'{0}'", GetQueryValue(item.Value))));
                f = true;
            }
            var v_condition = GetAndCondition(condition);
            q.Append(string.Format("UPDATE `{0}` SET {1} {2}", table,
                e.ToString(),
                !string.IsNullOrEmpty (v_condition)?$"WHERE {v_condition}" : null
              
                ));
            return q.ToString();
        }
        /// <summary>
        /// get query value
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string GetQueryValue(object value)
        {
            if (value == null)
                return string.Empty;
            if (value is ICoreDataTable)
            {
                return value.GetType().GetProperty(CoreDataConstant.CL_ID).GetValue(value, null).ToString();
            }

            Type t = value.GetType();
            if (t.IsEnum)
            {
                return Convert.ToInt32(value).ToString();
            }
            return value.ToString();
        }

        public virtual string GetAndCondition(Dictionary<string, object> andCondition)
        {
            if (andCondition==null)
                return null;
            StringBuilder sb = new StringBuilder();
            int i = 0;
            foreach (KeyValuePair<string, object> item in andCondition)
            {
                if (item.Value == null)
                    continue;
                if (i != 0)
                    sb.Append(" AND ");
                sb.Append(string.Format("`{0}`='{1}'", item.Key, GetQueryValue(item.Value)));
                i = 1;
            }
            return sb.ToString();
        }

        public virtual string GetOrCondition(Dictionary<string, object> orCondition)
        {
            StringBuilder sb = new StringBuilder();
            int i = 0;
            foreach (KeyValuePair<string, object> item in orCondition)
            {
                if (item.Value == null)
                    continue;
                if (i != 0)
                    sb.Append(" OR ");
                sb.Append(string.Format("`{0}`='{1}'", item.Key, GetQueryValue(item.Value)));
                i = 1;
            }
            return sb.ToString();
        }
        /// <summary>
        /// Retrieve a value according to data type
        /// </summary>
        /// <param name="type"></param>
        /// <param name="p"></param>
        /// <returns></returns>
        public virtual object GetValue(Type type, object value)
        {
            if (value == null)
                return null;
            //if object is Data Adpter
            if (value is ICoreDataTable)
            {
                return value.GetType().GetProperty(CoreDataConstant.CL_ID).GetValue(value, null).ToString();
            }

            if (type == typeof(bool))
            {
                if (Convert.ToBoolean(value))
                    return 1;
                return 0;
            }
            //if (type == typeof(DateTime))
            //{
            //    string p = ((DateTime)value).ToString(CoreDataConstant.MYSQL_DATE_TIME_FORMAT);
            //    return p;
            //}
            return value;

        }


        /// <summary>
        /// get the the data base name
        /// </summary>
        public abstract string DataBaseName { get; }

        public ICoreDataQueryResult Delete(Type type, Dictionary<string, object> condition)
        {
            return this.Delete(CoreDataContext.GetTableName(type), condition);
        }
        public virtual ICoreDataQueryResult Delete(string tableName, Dictionary<string, object> condition)
        {
            string dir = GetAndCondition(condition);
            string q = string.Format("DELETE FROM `{0}` {1} ", tableName,
                !string.IsNullOrEmpty (dir) ? $"WHERE {dir}" : null);
            return SendQuery(q);
        }
        public virtual void SelectDb()
        {
            SendQuery("Use `" + this.DataBaseName + "` ");
        }

        public virtual void BeginTransaction()
        {

        }

        public virtual void CommitTransaction()
        {
        }

        public virtual void RollBackTransaction()
        {
        }
        /// <summary>
        /// get if this table name exists
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public virtual bool TableExists(string tableName)
        {
            string q = "SHOW TABLES LIKE '" + tableName + "'";

            //string q = "SELECT * FROM `MSysObjects`";
            bool  v_r = this.SendQuery(q)?.RowCount == 1;
            return v_r;
        }
        internal virtual protected void ClearTable(string tablename)
        {
            this.SendQuery("TRUNCATE `" + tablename + "` ;");
            this.SendQuery("ALTER TABLE `" + tablename + "` AUTO_INCREMENT =1;");
        }

        internal protected virtual bool CreateTable(string tableName, ICoreDataColumnInfo[] CoreDBManagerColumnInfo, string description)
        {

            var q = this.GetCreateTableQuery(
                tableName,
                CoreDBManagerColumnInfo,
                description);
#if DEBUG
            Debug.WriteLine("Try to Create Table with , " + q);
#endif
            if (!string.IsNullOrEmpty(q))
            {
                ICoreDataQueryResult r = this.SendQuery(q);
                return ((r != null) && (!r.Error));
            }
            return false;
        }
        internal protected virtual void InitComplete()
        {

        }

        public virtual string EscapeString(Type type, string value)
        {
            return value;
        }
    }
}
