using IGK.ICore;
using IGK.ICore.Codec;
using IGK.ICore.Menu;
using IGK.ICore.WinUI;
using IGK.ICore.Drawing2D;
using IGK.ICore.Drawing2D.Menu;
using IGK.ICore.Drawing2D.WinUI;
using IGK.ICore.WinUI.Configuration;
using IGK.ICore.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel;
using IGK.ICore.Reflection;
using IGK.ICore.Xml;


namespace IGK.ICore.DB
{
    /// <summary>
    /// Represent ICoreDB system db query manager. Must important function are define here event some extensions.
    /// </summary>
    public static class CoreDBManager
    {

        private static Dictionary<string, string> sm_dataLength;
        private static TableRegister sm_register;


        /// <summary>
        /// used to register tables
        /// </summary>
        class TableRegister
        {
            private Dictionary<string, Type> m_regsitreadedTables;
            public TableRegister()
            {
                m_regsitreadedTables = new Dictionary<string, Type>();
            }
        }

        internal class ManagerInstance
        {
            private static ManagerInstance sm_instance;
            public CoreDataAdapterBase Adapter { get; set; }
            private ManagerInstance()
            {
            }

            public static ManagerInstance Instance
            {
                get
                {
                    return sm_instance;
                }
            }
            static ManagerInstance()
            {
                sm_instance = new ManagerInstance();

            }
        }


        static TableRegister Register => sm_register = new TableRegister();
        public static void ReloadTableIndex<T>()
        {

            var r = CoreDBManager.SelectAll<T>();
            Type t = typeof(T);
            var name = CoreDataContext.GetTableName(t);
            if (string.IsNullOrEmpty(name))
                return;
            Adapter.ClearTable(name);

            int i = 1;
            foreach (ICoreDataRow item in r.Rows)
            {
                item[CoreDataConstant.CL_ID] = i.ToString();

                Adapter.Insert(t, item.ToDictionary());

                i++;
            }

        }


        /// <summary>
        /// get the global data adapter
        /// </summary>
        public static CoreDataAdapterBase Adapter
        {
            get
            {
                return ManagerInstance.Instance.Adapter;
            }
            set
            {
                ManagerInstance.Instance.Adapter = value;
            }
        }
        /// <summary>
        /// create new row for data contect
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public static ICoreDataRow CreateNewRow(Type t)
        {
            return CoreDummyDataRow.Create(t);
        }
        public static ICoreDataRow CreateEmptyRow()
        {
            return new CoreEmptyRow();
        }
        /// <summary>
        /// inser data row table.
        /// </summary>
        /// <param name="item">data table item</param>
        /// <returns>query result </returns>
        public static ICoreDataQueryResult Insert(this ICoreDataTable item, CoreDataAdapterBase adpater=null)
        {
            if (item == null)
                return null;
            string g = CoreDataTableAttribute.GetTableName(item.GetType());
            if (g != null)
            {
                var bck = Adapter;
                Adapter = adpater ?? bck;
                ICoreDataQueryResult r = CoreDBManager.Insert(item.GetType(), item.ToDictionary());
                Adapter = bck;
                return r;
            }
            return null;
        }
        /// <summary>
        ///// delete a row item
        ///// </summary>
        ///// <param name="item"></param>
        ///// <returns></returns>
        //public static bool Insert(this IIGKDataTable item,
        //    IGKAdapter adapter)
        //{
        //    if (item == null)
        //        return false;
        //    string g = IGKDataTableAttribute.GetTableName(item.GetType());
        //    if (g != null)
        //        adapter.Insert(item.GetType(), item.ToDictionary());//new Dictionary<string, object>() { { CoreDataConstant.CL_ID, item.clId } });
        //    else { 

        //    }
        //    return true;
        //}

        /// <summary>
        /// update clId info with the last inserted id
        /// </summary>
        /// <param name="item"></param>
        public static void UpdateClId(this ICoreDataTable item)
        {
            item.clId = Convert.ToInt32(CoreDBManager.LastId());
        }
        /// <summary>
        /// delete a row item
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public static ICoreDataQueryResult Insert(this ICoreDataTable item, Type interfaceType,
            CoreDataAdapterBase adapter)
        {
            if (item == null)
                return null;
            string g = CoreDataTableAttribute.GetTableName(interfaceType);
            if (g != null)
            {
                var s = adapter.Insert(interfaceType, item.ToDictionary(adapter));//new Dictionary<string, object>() { { CoreDataConstant.CL_ID, item.clId } });
                                                                                  //if (s.AffectedRow == 1)
                                                                                  //{
                                                                                  //    //update the row info
                                                                                  //    item.clId = Convert.ToInt32(adapter.LastId());
                                                                                  //}
                return s;
            }
            return null;
        }
        /// <summary>
        /// delete a row item
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public static bool Update(this ICoreDataTable item)
        {
            Type t = item.GetSourceTableInterface();
            return CoreDBManager.Update(t != null ? t : item.GetType(), item.ToDictionary(), new Dictionary<string, object>() { { CoreDataConstant.CL_ID, item.clId } });

        }
        public static bool Update(this ICoreDataRow row)
        {
            var b = row.SourceTableInterface;

            return CoreDBManager.Update(b,
                row.ToDictionary(),
                row.GetValue<int>(CoreDataConstant.CL_ID).ToIdentifier().ToDictionary());
        }
        /// <summary>
        /// used to create an identifier for a clId column
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static ICoreDataTableCLIDIdentifier ToIdentifier(this int id)
        {
            return new CoreDataDummyIdentifier()
            {
                clId = id
            };
        }
        public static T SelectRow<T>(this int id, CoreDataAdapterBase adapter = null, string identifierName = CoreDataConstant.CL_ID)
        {
            return CoreDBManager.SelectFirstRowInstance<T>(adapter == null ? Adapter : adapter,
                id.ToIdentifier(identifierName));
        }
        public static T SelectRow<T>(this string id, CoreDataAdapterBase adapter = null, string identifierName = CoreDataConstant.CL_ID)
        {
            return CoreDBManager.SelectFirstRowInstance<T>(adapter == null ? Adapter : adapter,
                id.ToIdentifier(identifierName));
        }
        /// <summary>
        /// used to create an identifier from any kind of object
        /// </summary>
        /// <param name="id"></param>
        /// <param name="columnName"></param>
        /// <returns></returns>
        public static ICoreDataTableColumnIdentifier ToIdentifier(this object id, string columnName = CoreDataConstant.CL_ID)
        {
            return new CoreDataTableColumnIdentifier(columnName, id);
        }

        /// <summary>
        /// retrieve the datat time now
        /// </summary>
        /// <returns></returns>
        public static string GetDateNow()
        {
            return DateTime.Now.ToString("GSComboBoxObjectViewer -MM-dd HH:mm:ss");
        }
        /// <summary>
        /// represent a TogoContextInfo
        /// </summary>
        public sealed class CoreDBManagerContextInfo : ICoreDataContext,
            ICoreDataManagerContext
        {

            public ICoreDataManagerContext DbContext
            {
                get { return this as ICoreDataManagerContext; }
            }

            public string CreateTableQuery(string name, ICoreDataColumnInfo[] columnInfo, string description)
            {
                return CoreDBManager.CreateTableQuery(name, columnInfo, description);
            }
            public ICoreDataQueryResult SelectAll(Type t, Dictionary<string, object> andCondition)
            {
                return CoreDBManager.SelectAll(t, andCondition);
            }
        }


        static CoreDBManager()
        {

            sm_dataLength = new Dictionary<string, string>();
            sm_dataLength.Add("int", "8");
            sm_dataLength.Add("varchar", "30");
        }

        /// <summary>
        /// delete element in the query
        /// </summary>
        /// <param name="type"></param>
        /// <param name="condition"></param>
        /// <returns></returns>
        public static ICoreDataQueryResult Delete(Type type, Dictionary<string, object> condition)
        {
            return CoreDBManager.Adapter.Delete(type, condition);
        }

        public static ICoreDataQueryResult Delete(string tableName, ICoreDataTableColumnIdentifier identifier)
        {
            return CoreDBManager.Adapter.Delete(tableName, identifier.ToDictionary());
        }
        public static void ClearTable(string tablename)
        {
            CoreDBManager.Adapter.ClearTable(tablename);
        }
        public static bool Delete(Type interfaceType, ICoreDataTableCLIDIdentifier item)
        {
            CoreDBManager.Delete(interfaceType, new Dictionary<string, object>() { { CoreDataConstant.CL_ID, item.clId } });
            return true;
        }
        public static bool Delete(Type interfaceType, ICoreDataTableColumnIdentifier item)
        {
            if (item == null)
                return false;
            ICoreDataQueryResult o = CoreDBManager.Delete(interfaceType, item.ToDictionary());
            return (o.AffectedRow > 0);
        }
        public static bool Delete<T>(ICoreDataTableColumnIdentifier item)
        {
            if (item == null)
                return false;
            ICoreDataQueryResult o = CoreDBManager.Delete(typeof(T), item.ToDictionary());
            return (o.AffectedRow > 0);
        }
        /// <summary>
        /// delete a row item
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public static bool Delete(this ICoreDataTable item)
        {
            if (item == null)
                return false;
            CoreDBManager.Delete(item.GetType(), new Dictionary<string, object>() { { CoreDataConstant.CL_ID, item.clId } });
            return true;
        }
        public static void Delete<T>(ICoreDataTableCLIDIdentifier identifier)
        {
            CoreDBManager.Delete(typeof(T), identifier);
        }

        public static void DropTable(Type type)
        {
            string q = string.Format("DROP TABLE `{0}`;", CoreDataContext.GetTableName(type));
            SendQuery(q);
        }




        /// <summary>
        /// get a item row row by passing a clId name
        /// </summary>
        /// <typeparam name="T">Type that will be return</typeparam>
        /// <param name="id"></param>
        /// <returns>return a new created Instance or defaul(T)</returns>
        public static T GetValueById<T>(this CoreDataAdapterBase adapter, int id)
        {
            ICoreDataQueryResult r = adapter.SelectAll(typeof(T), new Dictionary<string, object>()
            {
                {CoreDataConstant.CL_ID, id}
            });
            if (!r.Error && (r.RowCount > 0))
            {
                var bck = Adapter;
                CoreDBManager.Adapter = adapter;
                var h = CreateInterfaceInstance<T>(typeof(T), r.Rows[0]);
                CoreDBManager.Adapter = bck;
                return h;
            }
            return default(T);

        }

        /// <summary>
        /// get a item row row by passing a clId name
        /// </summary>
        /// <typeparam name="T">Type that will be return</typeparam>
        /// <param name="id"></param>
        /// <returns>return a new created Instance or defaul(T)</returns>
        public static T GetValueById<T>(int id)
        {
            ICoreDataQueryResult r = SelectAll(typeof(T), new Dictionary<string, object>()
            {
                {CoreDataConstant.CL_ID, id}
            });
            if (!r.Error && (r.RowCount > 0))
            {
                return CreateInterfaceInstance<T>(typeof(T), r.Rows[0]);
            }
            return default(T);

        }
        /// <summary>
        /// used to create a table rom item instance.
        /// </summary>
        /// <typeparam name="T">Return interface to pass</typeparam>
        /// <param name="interfaceType">Request interface</param>
        /// <param name="row">row that will populate the item. value can be null</param>
        /// <returns></returns>
        public static T CreateInterfaceInstance<T>(Type interfaceType, ICoreDataRow row)
        {
            var s = CoreDataContext.CreateInterface(interfaceType, row, new CoreDBManagerContextInfo());
            return (T)s;
        }
        /// <summary>
        /// used to create a table row item instance
        /// </summary>
        /// <typeparam name="T">type to convert</typeparam>
        /// <param name="row">row can be null</param>
        /// <returns></returns>
        public static T CreateInterfaceInstance<T>(this ICoreDataRow row)
        {
            var s = CoreDataContext.CreateInterface(typeof(T), row, new CoreDBManagerContextInfo());
            return (T)s;
        }
        /// <summary>
        /// used to create a table row item instance
        /// </summary>
        /// <typeparam name="T">type to convert</typeparam>
        /// <param name="row">row can be null</param>
        /// <returns></returns>
        public static T CreateInterfaceInstance<T>(this ICoreDataRow row, string TableName)
        {
            var s = CoreDataContext.CreateInterface(TableName, row,
                new CoreDBManagerContextInfo());
            return (T)s;
        }
        /// <summary>
        /// create a row properties only contract contract
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="row"></param>
        /// <returns></returns>
        public static T CreateContract<T>(this ICoreDataRow row)
        {
            var s = typeof(T);
            if (!s.IsInterface)
                return default(T);
            T c = CoreContract.CreateContract<T>(row);

            return c;
        }

        /// <summary>
        /// used to create a table row item instance
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="row"></param>
        /// <returns></returns>
        public static T CreateInterfaceInstance<T>()
        {
            return CreateInterfaceInstance<T>(null);
        }

        /// <summary>
        /// create instance
        /// </summary>
        /// <param name="interfaceType"></param>
        /// <param name="row"></param>
        /// <returns></returns>
        public static object CreateInterfaceInstance(Type interfaceType, ICoreDataRow row)
        {
            return CoreDataContext.CreateInterface(interfaceType, row, new CoreDBManagerContextInfo());
        }

        public static string CreateTableQuery(string tableName, ICoreDataColumnInfo[] columns, string Description)
        {
            return Adapter.GetCreateTableQuery(tableName, columns, Description);

        }
        /// <summary>
        /// Global SQL query insert creator
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="t"></param>
        /// <param name="adapter"></param>
        /// <returns></returns>
        public static string CreateInsertQuery(string tableName, Dictionary<string, object> t, CoreDataAdapterBase adapter)
        {
            if (adapter != null)
                return adapter.CreateInsertQuery(tableName, t);
            if ((t == null) || (t.Count == 0))
                return string.Empty;
            StringBuilder sb = new StringBuilder();
            StringBuilder q = new StringBuilder();
            StringBuilder e = new StringBuilder();
            bool f = false;
            object obj = null;
            foreach (KeyValuePair<string, object> item in t)
            {
                if (f)
                {
                    q.Append(",");
                    e.Append(",");
                }
                q.Append(string.Format("`{0}`", item.Key));
                obj = item.Value;

                if (obj == null)
                {
                    //e.Append(string.Format("'{0}'", string.Empty));
                    e.Append("NULL");
                }
                else
                {
                    Type v_vt = obj.GetType();
                    if (v_vt.IsEnum)
                    {
                        e.Append(string.Format("'{0}'", (int)Convert.ToInt32(obj)));
                    }
                    else
                    {
                        if (v_vt.GetProperty(CoreDataConstant.CL_ID) != null)
                        {
                            object i = v_vt.GetProperty(CoreDataConstant.CL_ID).GetValue(item.Value, null);
                            if (i == null || ((int)i) == 0)
                            {
                                e.Append("NULL");
                            }
                            else
                                e.Append(string.Format("'{0}'", v_vt.GetProperty(CoreDataConstant.CL_ID).GetValue(item.Value, null).ToString()));
                        }
                        else
                            e.Append(string.Format("'{0}'", adapter.EscapeString(v_vt, EscapeString(Convert.ToString(item.Value)))));
                    }
                }
                f = true;
            }
            sb.Append(string.Format("INSERT INTO `{0}` ({1}) VALUES ({2})", tableName, q.ToString(), e.ToString()));
            return sb.ToString();
        }

        public static string EscapeString(string p)
        {
            ///:::CONVERTING C# VALUE to MYSQL string value 
            byte[] t = Encoding.UTF8.GetBytes(p);
            string h = Encoding.Default.GetString(t);
            string k = h.Replace("'", "\\'");

            //

            return k;
        }

        /// <summary>
        /// determine que le resultat de la requete retourne une seule entrée
        /// </summary>
        /// <param name="r"></param>
        /// <returns></returns>
        public static bool IsSingle(this ICoreDataQueryResult r)
        {
            return (r != null) && (r.RowCount == 1);
        }
        /// <summary>
        /// insert type type
        /// </summary>
        /// <param name="itableName"></param>
        /// <param name="values"></param>
        /// <param name="adapter"></param>
        /// <returns></returns>
        public static ICoreDataQueryResult Insert(Type itableName, Dictionary<string, object> values, CoreDataAdapterBase adapter=null)
        {
            if (itableName == null)
                return null;
           return Insert(CoreDataContext.GetTableName(itableName), values, adapter);
        }

        /// <summary>
        /// insert data collection
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="values"></param>
        /// <param name="adapter"></param>
        /// <returns></returns>
        public static ICoreDataQueryResult Insert(string tableName, Dictionary<string, object> values, CoreDataAdapterBase adapter = null)
        {
            var adapt = adapter ?? Adapter;
            string q = CreateInsertQuery(tableName, values, adapt);
            if (!string.IsNullOrEmpty(q))
                return adapt.SendQuery(q);
            return null;
        }
        public static ICoreDataQueryResult Insert(Type itableName, ICoreDataTable tableRow)
        {
            return Insert(itableName, tableRow.ToDictionary());
        }
        /// <summary>
        /// Get the last id of inserted query
        /// </summary>
        /// <returns></returns>
        public static int LastId()
        {
            return Adapter.LastId();
        }
        /// <summary>
        /// Update 
        /// </summary>
        /// <param name="itableName"></param>
        /// <param name="values"></param>
        /// <param name="condition"></param>
        /// <returns></returns>
        public static bool Update(Type itableName, Dictionary<string, object> values, Dictionary<string, object> condition)
        {
            return Update(CoreDataContext.GetTableName(itableName), values, condition);
        }
        public static bool Update<T>(Dictionary<string, object> values,
            ICoreDataTableColumnIdentifier identifier)
        {
            return Update(CoreDataContext.GetTableName(typeof(T)), values, identifier.ToDictionary());
        }

        /// <summary>
        /// Update table
        /// </summary>
        /// <param name="table">real table name</param>
        /// <param name="values">dictionnary of data to update</param>
        /// <param name="condition"></param>
        /// <returns></returns>
        public static bool Update(string table, Dictionary<string, object> values, Dictionary<string, object> condition)
        {
            string q = GetUpdateQuery(table, values, condition);
            ICoreDataQueryResult r = Adapter.SendQuery(q);
            return (r != null);
        }
        /// <summary>
        /// select all
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public static ICoreDataQueryResult SelectAll(string tableName)
        {
            ICoreDataQueryResult r = Adapter != null ? Adapter.SendQuery(string.Format("SELECT * FROM `{0}`", tableName), tableName) : null;
            return r;
        }

        /// <summary>
        /// select all
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public static ICoreDataQueryResult SelectAll(string tableName, ICoreDataTableColumnIdentifier identifier)
        {
            if (identifier != null)
                return SelectAll(tableName, identifier.ToDictionary());
            return SelectAll(tableName);
        }
        public static ICoreDataQueryResult SelectAll(Type type)
        {
            string tbName = CoreDataContext.GetTableName(type);
            if (tbName == null)
                return null;
            return SelectAll(tbName);
        }

        public static ICoreDataQueryResult SelectAll(string tableName, Dictionary<string, object> andCondition)
        {
            if (Adapter == null)
                return null;
            //resolv table name
            tableName = CoreDBManager.GetTableName(tableName);



            string v_condition = GetAndCondition(andCondition);
            if (!string.IsNullOrWhiteSpace(v_condition))
            {
                return Adapter.SendQuery(string.Format("SELECT * FROM `{0}` WHERE {1}",
                    tableName, v_condition), tableName);
            }
            return Adapter.SendQuery(string.Format("SELECT * FROM `{0}`",
                    tableName), tableName);
        }

        private static string GetTableName(string tableName)
        {
            throw new NotImplementedException();
        }

        public static ICoreDataQueryResult SelectAll<T>(CoreQueryConditionalExpression exp)
        {
            if (Adapter == null)
                return null;
            //resolv table name
            string tableName = GetTableName(CoreDataContext.GetTableName(typeof(T)));

            string v_condition = exp.Expression;
            if (!string.IsNullOrWhiteSpace(v_condition))
            {
                return Adapter.SendQuery(string.Format("SELECT * FROM `{0}` WHERE {1}",
                    tableName, v_condition), tableName);
            }
            return Adapter.SendQuery(string.Format("SELECT * FROM `{0}`",
                    tableName), tableName);
        }
        /// <summary>
        /// select all width and condition
        /// </summary>
        /// <param name="type"></param>
        /// <param name="andCondition"></param>
        /// <returns></returns>
        public static ICoreDataQueryResult SelectAll(Type type, Dictionary<string, object> andCondition)
        {
            if ((andCondition == null) || (andCondition.Count == 0))
                return SelectAll(type);

            string tbName = CoreDataContext.GetTableName(type);
            if (tbName == null)
                return null;
            return SelectAll(tbName, andCondition);
        }
        /// <summary>
        /// select all width and condition
        /// </summary>
        /// <param name="type"></param>
        /// <param name="andCondition"></param>
        /// <returns></returns>
        public static ICoreDataQueryResult SelectAll(Type type, ICoreDataTableColumnIdentifier identifier)
        {
            return SelectAll(type, identifier == null ? null : identifier.ToDictionary());
        }
        public static ICoreDataQueryResult SelectAll<T>()
        {
            return SelectAll(typeof(T));
        }

        /// <summary>
        /// select all item and create rows intance
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T[] SelectAllAndCreateRowInstance<T>()
        {
            var m = SelectAll<T>();
            if ((m != null))
                return m.CreateRowsItemInstance<T>();
            return null;
        }
        /// <summary>
        /// select all item and create rows intance
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T[] SelectAllAndCreateRowInstance<T>(Dictionary<string, object> dictionary)
        {
            var m = SelectAll<T>(dictionary);
            if ((m != null))
                return m.CreateRowsItemInstance<T>();
            return null;
        }
        /// <summary>
        /// select all item and create rows intance
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T[] SelectAllAndCreateRowInstance<T>(CoreDataAdapterBase adapter)
        {
            adapter = adapter ?? Adapter;
            if (adapter == null)
                return null;
            var m = adapter.SelectAll(typeof(T));
            if ((m != null))
                return m.CreateRowsItemInstance<T>();
            return null;
        }
        ///<summary>
        /// select all item and create rows intance
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T[] SelectAllAndCreateRowInstance<T>(CoreDataAdapterBase adapter, Dictionary<string, object> dictionary)
        {
            adapter = adapter ?? Adapter;
            if (adapter == null)
                return null;
            var m = adapter.SelectAll(typeof(T), dictionary);
            if ((m != null))
                return m.CreateRowsItemInstance<T>();
            return null;
        }

        ///<summary>
        /// select all item and create rows intance
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T[] SelectAllAndCreateRowInstance<T>(CoreDataAdapterBase adapter, ICoreDataTableColumnIdentifier identifier)
        {
            return SelectAllAndCreateRowInstance<T>(adapter, identifier.ToDictionary());
        }
        /// <summary>
        /// select first row instance
        /// </summary>
        /// <param name="tablename"></param>
        /// <param name="identifier"></param>
        /// <returns></returns>
        public static ICoreDataRow SelectFirstRowInstance(string tablename, ICoreDataTableColumnIdentifier identifier)
        {
            var q = SelectAll(tablename, identifier != null ? identifier.ToDictionary() : null);
            if ((q != null) && (q.RowCount == 1))
            {
                return q.Rows[0];
            }
            return null;
        }
        public static ICoreDataRow SelectFirstRowInstance(string tableName, Dictionary<string, object> dictionary)
        {
            var q = SelectAll(tableName, dictionary);
            if ((q != null) && (q.RowCount == 1))
            {
                return q.Rows[0];
            }
            return null;
        }
        /// <summary>
        /// create first row for selection instance.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="adapter"></param>
        /// <param name="identifier"></param>
        /// <returns></returns>
        public static T SelectFirstRowInstance<T>(CoreDataAdapterBase adapter,
            ICoreDataTableColumnIdentifier identifier)
        {
            var m = SelectAllAndCreateRowInstance<T>(adapter, identifier.ToDictionary());
            if ((m != null) && (m.Length > 0))
                return m[0];
            return default(T);

        }
        /// <summary>
        /// select all item and create rows intance
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T[] SelectAllAndCreateRowInstance<T>(ICoreDataTableColumnIdentifier identifier)
        {
            var m = SelectAll<T>(identifier);
            if ((m != null))
                return m.CreateRowsItemInstance<T>();
            return null;
        }
        public static ICoreDataQueryResult SelectAll<T>(ICoreDataTableColumnIdentifier identifier)
        {
            if (identifier == null)
                return null;
            CoreQueryDictionary<T> p = new CoreQueryDictionary<T>(identifier.ToDictionary());
            return SelectAll(typeof(T), p.ToDictionary(true));
        }
        /// <summary>
        /// select all with and conditions
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="andCondition"></param>
        /// <returns></returns>
        public static ICoreDataQueryResult SelectAll<T>(Dictionary<string, object> andCondition)
        {
            return SelectAll(typeof(T), andCondition);
        }

        public static ICoreDataQueryResult SendQuery(string query)
        {
            return Adapter.SendQuery(query);
        }
        /*
            /// <summary>
            /// obtient la table des caratéristique en fonction  du name et des product
            /// </summary>
            /// <param name="name"></param>
            /// <param name="productTypeId"></param>
            /// <returns></returns>
            public static string GetFeatureTableName(string productTypeId)
            {
                try
                {
                    return SelectAll(typeof(ITogoProductTypes), new Dictionary<string, object>() { 
                    {CoreDataConstant.CL_ID, productTypeId }
                }).GetRowAt(0)["clFeatureTableName"];
                }
                catch { 
                }
                return null;

            }

            /// <summary>
            /// retreive the column for informations
            /// </summary>
            /// <param name="tableName"></param>
            /// <returns></returns>
            public static IGKDBColumnInfo[] GetFeatureColumnInfo(string tableName)
            {
                Type t = IGKDataContext.GetInterfaceType(tableName);
                if (t != null)
                    return IGKDataContext.GetColumnInfo(t);
                return feature_getCustomFeatureColumn(tableName);

            }


            public static IGKDBColumnInfo[] GetFeatureColumnInfo(Type dataInterfaceType)
            {
                return IGKDataContext.GetColumnInfo(dataInterfaceType);
            }


            /// <summary>
            /// remove association
            /// </summary>
            /// <param name=CoreDataConstant.CL_ID></param>
            /// <returns></returns>
            public static bool RemoveAssociation(string clId)
            {
                IIGKDataRow r = null;
                bool v_f = true ;
                try
                {
                    r = CoreDBManager.SelectAll(typeof(ITogoProductCodeBarAssociation), new Dictionary<string, object>() { 
                    {CoreDataConstant.CL_ID, clId}
                }).GetRowAt(0);
                    if (r == null)
                        return false ;
                    if (v_f)
                     v_f =  CoreDBManager.Delete(typeof (ITogoProductStock ), new Dictionary<string, object>() { 
                    {"clCBExt", r["clCBExt"]}
                }) != null;

                    if (v_f)
                     v_f =   CoreDBManager.Delete(typeof(ITogoProductHistory), new Dictionary<string, object>() { 
                    {"clCBExt", r["clCBExt"]}
                }) != null;

                    if (v_f)
                        v_f = CoreDBManager.Delete(typeof(ITogoProductCodeBarAssociation), new Dictionary<string, object>() { 
                    {CoreDataConstant.CL_ID, r[CoreDataConstant.CL_ID]}
                }) != null;
                }
                catch { 

                }
                return v_f;

            }



            /*
            /// <summary>
            /// associate CodeIn/CodeBarExt
            /// </summary>
            /// <param name="readCode">Code bar lue</param>
            /// <param name="generateCode">Code bar Généere</param>
            /// <param name="ProductTypeId">Type du produit</param>
            public static IIGKDataQueryResult AssociateCodeBar(string readCode, string generateCode, string ProductTypeId)
            {
                Type t = typeof(ITogoProductCodeBarAssociation);
                IIGKDataRow r =  IGKDataContext.CreateNewRow(t);
                r["clCBExt"] = readCode;
                r["clCBIn"] = generateCode;
                r[CoreDataConstant.CL_DATETIME] = GetDateNow();
                r["clUserId"] = GSSystem.User[CoreDataConstant.CL_ID];
                r["clProductType_Id "] = ProductTypeId;
                IIGKDataQueryResult q = Insert(IGKDataContext.GetTableName(t), r.ToDictionary());
                if (q == null)
                {//impossibble d'inserer
                    GSLog.WriteLog("Impossible d'ajouter un article avec le code bar car le code bar est déjà enregistrer:"+readCode );
                    return null;
                }
                var id = LastId();
                if (id == "0")
                    return null;
                r[CoreDataConstant.CL_ID] = id;



                q = SelectAll(t, r.ToDictionary());

                if (( q !=null) && (q.Rows.Count == 1))
                {
                    Type v_stock = typeof (ITogoProductStock );
                    Type v_history = typeof(ITogoProductHistory);

                    IIGKDataRow v_sr = IGKDataContext.CreateNewRow (v_stock );
                    v_sr["clCBExt"] = readCode ;
                    v_sr["clState"] = ((int)enuProductState.In ).ToString();
                    v_sr["clProductType_Id "] = ProductTypeId;
                    v_sr["clQte"] = "1";
                    IIGKDataQueryResult v_q = Insert(v_stock, v_sr.ToDictionary());
                    if (q.AffectedRow == 1)
                    {
                        v_sr = IGKDataContext.CreateNewRow(v_history);
                        v_sr["clQte"] = "1";
                        v_sr["clCBExt"] = readCode;
                        v_sr["clOperation"] = ((int)enuProductOperation.OpIn).ToString();
                        v_sr["clUserId"] = GSSystem.User[CoreDataConstant.CL_ID];
                        v_sr[CoreDataConstant.CL_DATETIME] = r[CoreDataConstant.CL_DATETIME];
                        v_q = Insert(v_history, v_sr.ToDictionary());
                    }


                }
                return q;
            }
            /// <summary>
            /// get product result from external code bar
            /// </summary>
            /// <param name="clCBExt">external code bar</param>
            /// <returns></returns>
            public static IIGKDataQueryResult GetProductTypeFromCodeBar(string clCBExt)
            {
                Type t = typeof(ITogoProductCodeBarAssociation);
                Dictionary<string, object> r = new Dictionary<string, object>();
                r.Add("clCBExt", clCBExt);
                IIGKDataQueryResult q = SelectAll(t, r);
                return q;
            }

            /// <summary>
            /// retrouve toute les l'association entre code bar privée et code bar externe
            /// </summary>
            /// <returns></returns>
            public static IIGKDataQueryResult GetCodeBarAssociations()
            {
                return SelectAll (typeof(ITogoProductCodeBarAssociation));
            }
            /// <summary>
            /// retrouve toute les l'association entre code bar privée et code bar externe en fonction du type
            /// </summary>
            /// <returns></returns>
            public static IIGKDataQueryResult GetCodeBarAssociationFromProductTypeId(string ProdutTypeId)
            {
                Dictionary<string, object> r = new Dictionary<string, object>();
                r.Add("clProductType_Id ", ProdutTypeId);
                return SelectAll(typeof(ITogoProductCodeBarAssociation), r);
            }
            /// <summary>
            /// supprime tous les éléments d'une table
            /// </summary>
            /// <param name="type"></param>
            public static void ClearTable(Type type)
            {
                string tbName = IGKDataContext.GetTableName(type);
                if (tbName == null)
                    return ;
                Adapter.SendQuery(string.Format("DROP `{0}` ",
               tbName ));
            }

            /// <summary>
            /// determinser si le code bar externe est déjà enregister
            /// </summary>
            /// <param name="exCode"></param>
            /// <returns></returns>
            public static bool IsCodeBarAssociate(string exCode)
            {
                Type t = typeof(ITogoProductCodeBarAssociation);
                IIGKDataRow r = IGKDataContext.CreateNewRow(t);
                r["clCBExt"] = exCode;
                IIGKDataQueryResult q = SelectAll(t, r.ToDictionary());
                return (q != null) && (q.Rows.Count == 1);
            }
            public static IIGKDataRow GetCodeBarAssociationRow(string exCode)
            {
                Type t = typeof(ITogoProductCodeBarAssociation);
                IIGKDataRow r = IGKDataContext.CreateNewRow(t);
                r["clCBExt"] = exCode;
                IIGKDataQueryResult q = SelectAll(t, r.ToDictionary());
                return q.GetRowAt(0);
            }

            public static bool IsProductType(string productTypeName)
            {
                Type t = typeof(ITogoProductTypes);
                IIGKDataRow r = IGKDataContext.CreateNewRow(t);
                r["clName"] = productTypeName;
                IIGKDataQueryResult q = SelectAll(t, r.ToDictionary());
                return (q != null) && (q.Rows.Count == 1);
            }
            public static bool IsFeatureTable(string featureTableName, string CodeBarPrefix)
            {
                IIGKDataQueryResult q = SelectAll(typeof(ITogoProductTypes), new Dictionary<string, object>() { 
                        {"clFeatureTableName", featureTableName },                       
                        {"clCodeBarPrefix", CodeBarPrefix  }

                        });
                return (q != null) && (q.Rows.Count == 1);
            }

            public static bool AddProductType(string productTypeName, 
                string CodeBarPrefix,  
                int Position,
                string featuretableName, 
                int model,
                bool palettisable)
            {
                string v_prodTypeId = string.Empty ;
                if (!IsProductType(productTypeName))
                {
                    Type t = typeof(ITogoProductTypes);
                    IIGKDataRow r = IGKDataContext.CreateNewRow(t);
                    r["clName"] = productTypeName;
                    r["clCodeBarPrefix"] = CodeBarPrefix;
                    r["clPosition"] = Position.ToString();
                    r["clFeatureTableName"] = featuretableName;
                    r["clEncodeMode"] = model.ToString();
                    r["clPalettisable"] = palettisable ? "1": "0";
                    IIGKDataQueryResult q =  Insert(t, r.ToDictionary());
                    if ((q != null) && (q.AffectedRow == 1))
                    {
                        return true;
                    }
                }
                return false;

            }

            /// <summary>
            /// destock product out
            /// </summary>
            /// <param name="p"></param>
            public static void StockOut(string p)
            {
                Update(typeof(ITogoProductStock), new Dictionary<string, object>() { 
                    {"clState" , ((int)enuProductState .Out )}
                },
                new Dictionary<string, object >(){
                    {"clCBExt", p}
                }
                );

                string time = DateTime.Now.ToString("GSComboBoxObjectViewer -MM-dd HH:mm:ss");

                Insert(typeof(ITogoProductHistory), new Dictionary<string, object>() {                 
                    {CoreDataConstant.CL_DATETIME, time},
                    {"clOperation",((int) enuProductOperation .OpOut )},
                    {"clUserId", GSSystem.User[CoreDataConstant.CL_ID]},     
                    {"clCBExt", p}
                }            
             );
            }

            public static bool IsProductInStock(string p)
            {
             bool v= IsSingle ( CoreDBManager.SelectAll(typeof(ITogoProductStock), new Dictionary<string, object>() { 
                    {"clCBExt", p},
                    {"clState", 1}
                }));//.Rows.Count  == 1;

             return v;
            }

            public static IIGKDataQueryResult GetProductTypes()
            {
                return SelectAll(typeof(ITogoProductTypes));
            }

            public static string GetStockEntry(string p)
            {
                int i = 0;
                foreach (IIGKDataRow r in SelectAll(typeof(ITogoProductStock), new Dictionary<string, object>() { 
                    {"clProductType_Id ", p}
                })
                .Rows)
                {
                    i += Convert.ToInt32 (r["clQte"]) ;//.Count.ToString ();
                }
                return i.ToString();
            }

            public static string GetStockOut(string p)
            {
                int i = 0;
                foreach (IIGKDataRow r in SelectAll(typeof(ITogoProductStock), new Dictionary<string, object>() { 
                    {"clProductType_Id ", p},
                    {"clState",((int)enuProductState.Out).ToString() }
                }).Rows)
                {
                    i += Convert.ToInt32(r["clQte"]);
                }

                return i.ToString();
            }

            public static string GetStock(string productId)
            {
                  int i = 0;
                  foreach (IIGKDataRow r in SelectAll(typeof(ITogoProductStock), new Dictionary<string, object>() { 
                    {"clProductType_Id ", productId},
                    {"clState",((int)enuProductState.In).ToString() }
                }).Rows)
                  {
                      i += Convert.ToInt32(r["clQte"]);
                  }
                  return i.ToString();
            }
            /// <summary>
            /// get the product base stock row
            /// </summary>
            /// <param name="productId"></param>
            /// <param name="codeBarExt"></param>
            /// <returns></returns>
            public static IIGKDataRow GetStock(string productId, string codeBarExt)
            {            
                return SelectAll(typeof(ITogoProductStock), new Dictionary<string, object>() { 
                    {"clProductType_Id ", productId},
                    {"clState",((int)enuProductState.In).ToString() },
                    {"clCBExt", codeBarExt  }
                }).GetRowAt (0);
            }

            public static IIGKDataRowCollections  GetLevels()
            {
                return SelectAll(typeof(ITogoTbLevel)).Rows ;
            }



            public static string GetProductTypeName(string p)
            {
                try
                {
                    IIGKDataQueryResult q =  SelectAll(typeof(ITogoProductTypes), new Dictionary<string, object>() {
                    {CoreDataConstant.CL_ID, p}
                });
                    IIGKDataRow r = q.GetRowAt(0);
                    if (r!=null) return r["clName"];
                }
                catch { 
                }
                return null;
            }


            public static string GetUserName(string id)
            {
                var e = SelectAll(typeof(ITogoTbUsers), new Dictionary<string, object>() { 
                    {CoreDataConstant.CL_ID, id}
                });
                if ((e !=null) && (e.RowCount ==1))
                {
                IIGKDataRow ro= e.GetRowAt (0);
                return ro[CoreDataConstant.CL_FIRSTNAME] + " " + ro[CoreDataConstant.CL_LASTNAME].ToUpper ();
                }
                return null;
            }
            /// <summary>
            /// get the feature value
            /// </summary>
            /// <param name="columnInfo"></param>
            /// <param name="id"></param>
            /// <returns></returns>
            public static string GetFeatureValue(IGKDBColumnInfo columnInfo, string id)
            {
                Type t = columnInfo.PropertyInfo.PropertyType;


                var d = CoreDBManager.SelectAll(t, new Dictionary<string, object>() { 
                {CoreDataConstant.CL_ID, id}
                });

                if ((d != null) && (d.RowCount == 1))
                {
                    return d.GetRowAt(0)["clName"];
                }
                return string.Empty;


            }

            /// <summary>
            /// retrieve feature for association
            /// </summary>
            /// <param name="codeBarIn"></param>
            /// <returns></returns>
            public static string GetFeatureInfo(string codeBarIn, string productTypeId)
            {
                var e = SelectAll(typeof(ITogoProductCodeBarAssociation), new Dictionary<string, object>() { 
                    {"clCBIn", codeBarIn},
                    {"clProductType_Id ", productTypeId}
                });
                if ((e == null)|| (e.RowCount ==0)) return null;

                var l = e.GetRowAt(0);
                var prodid = l["clProductType_Id "];
                string featTableName = GetFeatureTableName(prodid);
                IGKDBColumnInfo[] inf = GetFeatureColumnInfo(featTableName );
                IIGKDataRow r = SelectAll(featTableName ,  new Dictionary<string, object>() { 
                    {"clIntCodeBar", codeBarIn}
                }).GetRowAt (0);
                string info = "";
                if (r != null)
                {
                    StringBuilder sb = new StringBuilder();
                    bool v_c = false;
                    for (int i = 0; i < inf.Length; i++)
                    {
                        switch (inf[i].clName.ToLower())
                        {
                            case "clintcodebar":
                            case CoreDataConstant.CL_ID:
                            case "clproducttypeid":
                                continue;
                        }
                        if (r.GetValue<int>(inf[i].clName) == 0)
                        {
                            continue;
                        }
                        info = GetFeatureValue(inf[i], r[inf[i].clName]);
                        if (string.IsNullOrEmpty(info))
                            continue;
                        if (v_c)
                            sb.Append(",");
                        sb.Append(
                          info
                         );
                        v_c = true;
                    }


                    return sb.ToString();
                }
                return "NOTDEFINE";
            }

            ////------------------------------------------------------------------
            ////USER FUNCTION
            ////------------------------------------------------------------------
            //public static bool users_AddUser(ITogoTbUsers user)
            //{ 
            //    Type v_t =  MethodInfo.GetCurrentMethod ().GetParameters ()[0].ParameterType;
            //    return (Insert(v_t,
            //        GetValues(v_t, user)).AffectedRow == 1);
            //}
            //public static void users_RemoveUser(string id)
            //{
            //    Delete(typeof(ITogoTbUsers), new Dictionary<string, object>() { 
            //        {CoreDataConstant.CL_ID, id}
            //    });
            //}

            //public static void users_updateUser(ITogoTbUsers user)
            //{
            //    Type t = typeof(ITogoTbUsers);
            //    Update(t, GetValues(t, user), new Dictionary<string, object>() { {CoreDataConstant.CL_ID, user.clId }});
            //}

            /// <summary>
            /// get values
            /// </summary>
            /// <param name="type"></param>
            /// <param name="user"></param>
            /// <returns></returns>
            public static Dictionary<string, object> GetValues(Type type, object  user)
            {
                IGKDBColumnInfo[] inf = GetFeatureColumnInfo(type);
                if ((inf != null) && (user !=null))
                {
                    Dictionary<string, object> v_o = new Dictionary<string, object>();

                    object v_v = null;
                    for (int i = 0; i < inf.Length; i++)
                    {
                        v_v = user.GetType().GetProperty(inf[i].clName).GetValue(user);
                        if (v_v !=null)
                            v_o.Add(inf[i].clName, v_v);
                    }
                    return v_o;
                }
                return null;
            }

            public static IIGKDataQueryResult GetProductTypesStock(string clName)
            {
                return null;
            }

            public static IIGKDataQueryResult GetProductTypesStockDetails(string productTypeId)
            {
                IIGKDataQueryResult e = SelectAll(typeof(ITogoProductStock), new Dictionary<string, object>() { 
                    {"clProductType_Id ", productTypeId }
                });
                if ((e == null) && (e.RowCount == 0))
                    return null;

                IIGKDataQueryResult r = SelectAll(typeof(ITogoProductCodeBarAssociation), new Dictionary<string, object>() { 
                    {"clProductType_Id ", productTypeId }
                });
                Dictionary<string, List<IIGKDataRow>> v_c = new Dictionary<string, List<IIGKDataRow>>();
                string v_key = string.Empty;
                foreach (IIGKDataRow  v_row in r.Rows)
                {
                    v_key = v_row["clCBIn"];
                    if (!v_c.ContainsKey(v_key))
                    {
                        v_c.Add(v_key, new List<IIGKDataRow>());                    
                    }
                    v_c[v_key].Add(v_row);
                }
                IIGKDataSet v_dataset = IGKDataContext.CreateEmptyDateSet();
                v_dataset.AddColumn("clProductType_Id ");
                v_dataset.AddColumn("clName");
                v_dataset.AddColumn("clCBIn");
                v_dataset.AddColumn("clDescription");
                v_dataset.AddColumn("clTraited");
                v_dataset.AddColumn("clOut");
                v_dataset.AddColumn("clStock");



                string v_productName = GetProductTypeName(productTypeId);
                int v_stock = 0;
                foreach (KeyValuePair<string, List<IIGKDataRow>> item in v_c)
                {
                    IIGKDataRow v_row = v_dataset.CreateRow();
                    v_stock = 0;
                    object v_state = null;
                    foreach (IIGKDataRow  v_r2 in item.Value)
                    {
                        var v_r3 = GetStock(productTypeId, v_r2["clCBExt"]);
                        if (v_r3 != null)
                        {
                            v_state =v_r3 ["clState"];
                            if (v_state.Equals ("1"))
                                v_stock++;
                        }
                    }



                    v_row["clName"] = v_productName;
                    v_row["clCBIn"] = item.Key;
                    v_row["clDescription"] = CoreDBManager.GetFeatureInfo(item.Key, productTypeId);
                    v_row["clProductType_Id "] = productTypeId;
                    v_row["clTraited"] = item.Value.Count.ToString();
                    v_row["clOut"] = (item.Value.Count - v_stock).ToString();


                    v_row["clStock"] = v_stock.ToString();

                    v_dataset.AddRow(v_row);
                }
                return v_dataset;
            }


           /// <summary>
           /// Create a new pallete with apllete reference
           /// </summary>
           /// <param name="palref"></param>
           /// <param name="dateTime"></param>
           /// <param name="userid"></param>
           /// <param name="productTypeId"></param>
           /// <param name="Qte"></param>
           /// <param name="paletteState"></param>
           /// <returns></returns>
            public static IIGKDataRow palette_AddNewPalette(
                string palref, 
                string dateTime,
                string userid, 
                string productTypeId, 
                int Qte,
                string paletteState)
            {

                Type dtype = typeof(ITogoTbPalettes);

                IIGKDataRow v_row = IGKDataContext.CreateNewRow(dtype);
                v_row["clUserId"] = userid;
                v_row["clRef"] = palref;
                v_row[CoreDataConstant.CL_DATETIME] = dateTime;
                v_row["clState"] = ((int)enuPaletteState.Started ).ToString();
                v_row["clQte"] = Qte.ToString();
                v_row["clProductType_Id "] = productTypeId;

                IIGKDataQueryResult r = Insert(dtype,v_row.ToDictionary());
                if ((r !=null) && (r.AffectedRow == 1))
                {
                          r = SelectAll(dtype, new Dictionary<string, object>() { 
                        {"clRef" , v_row["clRef"]}
                    });
                    if ((r !=null) && (r.Rows.Count == 1))
                          {
                              IIGKDataRow v_result = r.GetRowAt(0);
                              AssociateCodeBar(v_result["clRef"], "PALETTE", "0");
                              return v_result;
                          }
                }
                return null;
            }
            public static bool palette_updateQte(string paletteid, int qte)
            {
                return Update(typeof(ITogoTbPalettes),
                    new Dictionary<string, object>() { 
                        {"clQte", qte }
                    }, new Dictionary<string, object>() { 
                        {CoreDataConstant.CL_ID,paletteid }
                    });
            }
            /// <summary>
            /// associate
            /// </summary>
            /// <param name="paletteid">identifiant de la palette</param>
            /// <param name="clProductCodeBarExt">code bar externe / numéros de serie du produit déjà encoder</param>
            /// <returns></returns>
            public static bool palette_AssociateProduct(string paletteid, string clProductCodeBarExt)
            {
                ///check if the product code bar is associated

                    IIGKDataQueryResult r  = SelectAll(typeof(ITogoProductCodeBarAssociation),
                    new Dictionary<string, object>() { 
                        {"clCBExt", clProductCodeBarExt }
                    });
                if (IsSingle(r))
                {
                    string clCodeAssociationId = r.GetRowAt(0)[CoreDataConstant.CL_ID];
                    //assoction the code bar

                      Dictionary<string, object> data = new Dictionary<string, object>() { 
                        {"clPaletteId", paletteid },
                        {"clCodeBarAssociationId", clCodeAssociationId }
                      };
                      if ((IsSingle(SelectAll(typeof(ITogoTbPalettesInfo), data)) == false)
                           &&
                    (IsSingle(SelectAll(typeof(ITogoTbPalettesInfo),
                     new Dictionary<string, object>() { 
                        {"clCodeBarAssociationId", clCodeAssociationId }
                      })) == false ))
                      {
                          r = Insert(typeof(ITogoTbPalettesInfo), data);
                          bool v_out  =(r != null) && (r.AffectedRow == 1);
                          if (v_out)
                          {
                              palette_updateQte(paletteid, SelectAll(typeof(ITogoTbPalettesInfo), new Dictionary<string, object>() { 
                        {"clPaletteId", paletteid }}).RowCount);
                          }

                          return v_out;
                      }
                }
                return false;
            }

            /// <summary>
            /// determine que le resultat de la requete retourne une seule entrée
            /// </summary>
            /// <param name="r"></param>
            /// <returns></returns>
            public static bool IsSingle(IIGKDataQueryResult r)
            {
                return (r != null) && (r.RowCount == 1);
            }

            public static bool palette_IsCreatedPallete(string clRef)
            {
                return IsSingle(SelectAll(typeof(ITogoTbPalettes), new Dictionary<string, object>() { 
                    {"clRef", clRef }
                }));
            }
            /// <summary>
            /// get the palle by ref
            /// </summary>
            /// <param name="clRef"></param>
            /// <returns></returns>
            public static IIGKDataQueryResult palette_getAssociatePaletteByRef(string clRef)
            {
                IIGKDataQueryResult e = SelectAll(typeof(ITogoTbPalettes), new Dictionary<string, object>() { 
                    {"clRef", clRef }
                });
                if (IsSingle(e))
                {
                    return palette_getAssociatePalette(e.GetRowAt(0)[CoreDataConstant.CL_ID]);
                }
                return null;
            }
            public static IIGKDataQueryResult palette_getAssociatePalette(string palId)
            {
               IIGKDataQueryResult r = SelectAll(typeof(ITogoTbPalettesInfo), new Dictionary<string, object>() { 
                        {"clPaletteId" , palId }
                    });



                             /*this.c_lstAssocInfo.Columns.Add("title.PaletteCodeBar");
                this.c_lstAssocInfo.Columns.Add("title.ProductCodeBar");
                this.c_lstAssocInfo.Columns.Add("title.ProductType");
                this.c_lstAssocInfo.Columns.Add("title.ProductDescription");
                this.c_lstAssocInfo.Columns.Add("title.Date");
                this.c_lstAssocInfo.Columns.Add("title.User");

                IIGKDataSet v_dataset = IGKDataContext.CreateEmptyDateSet();
                v_dataset.AddColumn("clPaletteCodeBar");
                v_dataset.AddColumn("clProductCodeBar");
                v_dataset.AddColumn("clProductType");
                v_dataset.AddColumn("clProductDescription");
                v_dataset.AddColumn("clDateTime");
                v_dataset.AddColumn("clUser");

                string v_assocId = string.Empty;
                IIGKDataRow v_palRow = SelectAll(typeof(ITogoTbPalettes), new Dictionary<string, object>() { 
                    {CoreDataConstant.CL_ID, palId   }
                }).GetRowAt(0) ;
                if (v_palRow == null)
                    return v_dataset;

                IIGKDataRow v_assocRow = null;


                foreach (IIGKDataRow item in r.Rows)
                {

                    v_assocId = item["clCodeBarAssociationId"];
                    v_assocRow = GetCodeBarAssociationsFromId(v_assocId);

                    IIGKDataRow v_row = v_dataset.CreateRow();
                    v_row["clPaletteCodeBar"] = v_palRow["clRef"];

                    v_row["clProductCodeBar"] = v_assocRow["clCBExt"];
                    v_row["clProductType"] = GetProductTypeName(v_assocRow["clProductType_Id "]);
                    v_row["clProductDescription"] = GetFeatureInfo(v_assocRow["clCBIn"], v_assocRow["clProductType_Id "]);
                    v_row[CoreDataConstant.CL_DATETIME] = v_palRow[CoreDataConstant.CL_DATETIME];
                    v_row["clUser"] = CoreDBManager.GetUserName(v_palRow["clUserId"]); 

                    v_dataset.AddRow(v_row);
                }
                /*

                string v_productName = GetProductTypeName(productTypeId);
                int v_stock = 0;
                foreach (KeyValuePair<string, List<ITogoDataRow>> item in v_c)
                {
                    ITogoDataRow v_row = v_dataset.CreateRow();
                    v_stock = 0;
                    object v_state = null;
                    foreach (ITogoDataRow  v_r2 in item.Value)
                    {
                        var v_r3 = GetStock(productTypeId, v_r2["clCBExt"]);
                        if (v_r3 != null)
                        {
                            v_state =v_r3 ["clState"];
                            if (v_state.Equals ("1"))
                                v_stock++;
                        }
                    }



                    v_row["clName"] = v_productName;
                    v_row["clDescription"] = CoreDBManager.GetFeatureInfo(item.Key );
                    v_row["clProductType_Id "] = productTypeId;
                    v_row["clTraited"] = item.Value.Count.ToString();
                    v_row["clOut"] = (item.Value.Count - v_stock).ToString();


                    v_row["clStock"] = v_stock.ToString();

                    v_dataset.AddRow(v_row);
                }
                return v_dataset;
               //return r;
            }


            public static void palette_printAllPalette()
            {
                //foreach (IIGKDataRow r in SelectAll(typeof(ITogoTbPalettes)).Rows)
                //{
                //    new GSPrintPaletteInfo(null, r).Print();
                //}
            }

            public static IIGKDataRow GetCodeBarAssociationsFromId(string v_assocId)
            {
                return GetSingleRow(typeof(ITogoProductCodeBarAssociation), v_assocId);
            }
            /// <summary>
            /// retrieve single row of a data elements
            /// </summary>
            /// <param name="dataType">data table </param>
            /// <param name="id">clId in the table</param>
            /// <returns></returns>
            public static IIGKDataRow GetSingleRow(Type dataType, string id)
            {
                return SelectAll(dataType ,  new Dictionary<string, object>() { 
                    {CoreDataConstant.CL_ID , id }
                }).GetRowAt(0);
            }

            public static IIGKDataRow palette_getPaletteItemByRef(string clRef)
            {
                return SelectAll(typeof (ITogoTbPalettes ), new Dictionary<string, object>() { 
                    {CoreDataConstant.CL_REF , clRef }
                }).GetRowAt(0);
            }
            /// <summary>
            /// determine if product is associated to a palette
            /// </summary>
            /// <param name="clRef"></param>
            /// <returns></returns>
            public static bool palette_isProductAssociated(string clPalleteId, string clCBExt)
            {
                string classocId = string.Empty;
                IIGKDataRow r = SelectAll(typeof(ITogoProductCodeBarAssociation), new Dictionary<string, object>()
                {
                    {"clCBExt",clCBExt }
                }).GetRowAt(0);
                if (r != null)
                {
                    classocId = r[CoreDataConstant.CL_ID];
                    return IsSingle(SelectAll(typeof(ITogoTbPalettesInfo), new Dictionary<string, object>() { 
                        {"clPaletteId", clPalleteId },
                        {"clCodeBarAssociationId", classocId}
                    }));
                }
                return false;
            }
            public static string palette_NewId(string time, int qte, string productTypeId)
            {
                time = time.Replace (":", "");
                time = time.Replace ("-", "");
                time = time.Replace (" ","");

                return "PAL" + qte + "-" + GSSystem.User[CoreDataConstant.CL_ID]+"-"+ productTypeId+" "+time;

            }

            public static void palette_Cloture(string paletteid)
            {
                Update(typeof(ITogoTbPalettes),
                  new Dictionary<string, object>() { 
                        {"clState", 2 }
                    }, new Dictionary<string, object>() { 
                        {CoreDataConstant.CL_ID,paletteid }
                    });
            }
            public static void palette_update(string paletteid, Dictionary<string, object> data)
            {
                Update(typeof(ITogoTbPalettes),
               data, new Dictionary<string, object>() { 
                        {CoreDataConstant.CL_ID,paletteid }
                    });
            }
            public static void palette_Sold(string paletteid)
            {
                Update(typeof(ITogoTbPalettes),
                  new Dictionary<string, object>() { 
                        {"clState", 3 }
                    }, new Dictionary<string, object>() { 
                        {CoreDataConstant.CL_ID,paletteid }
                    });
            }

            /// <summary>
            /// add a new client
            /// </summary>
            /// <param name="type"></param>
            /// <param name="?"></param>
            public static void client_addClient(int type, string  name, string desc, string address)
            {
                IIGKDataRow v_r = IGKDataContext.CreateNewRow(typeof(ITogoClient));
                v_r["clName"] = name;
                v_r["clDescription"] = desc;
                v_r["clAddress"] = address;
                v_r["clClientType"] = type.ToString();
                Insert(typeof(ITogoClient),v_r.ToDictionary());
            }

            public static IIGKDataQueryResult client_getAllClient()
            {
                return SelectAll(typeof(ITogoClient));
            }

            /// <summary>
            /// sold command 
            /// </summary>
            /// <param name="clientid"></param>
            /// <param name="articles"></param>
            public static void sold_validateCommand(string clientid , string[] articles)
            {

                //mise en etat sortie
                foreach (string item in articles)
                {
                    CoreDBManager.StockOut(item);
                }
            }


            public static void product_dropPropductFromInCB(string clCBIn)
            {
                foreach (IIGKDataRow r in
                    SelectAll(typeof(ITogoProductCodeBarAssociation),
                    new Dictionary<string, object>(){
                {"clCBIn", clCBIn }
                    }
                    ).Rows)
                {
                    string clid = r[CoreDataConstant.CL_ID];
                    if (!RemoveAssociation(clid))
                    { 
                        //remove                 
                        GSLog.WriteLog("Mis a jour impossible");
                    }

                }
            }
            public static void product_movetostock(string  clCBIn)
            {
                foreach( IIGKDataRow r  in 
                    SelectAll (typeof (ITogoProductCodeBarAssociation ), 
                    new Dictionary<string,object>  (){
                {"clCBIn", clCBIn }
                    }
                    ).Rows)
                {
                    string codebar = r["clCBExt"];

                    if (!Update(
                        typeof(ITogoProductStock),
                        new Dictionary<string, object>() { 
                            {"clState", 1}
                        },
                        new Dictionary<string, object>() { 
                            {"clCBExt", codebar}
                        }
                    ))
                    {
                        GSLog.WriteLog("Mis a jour impossible");
                    }

                }
            }



            public static void group_addNewGroup(string groupname)
            { 
                Type t = typeof(ITogoTbGroups);
                var v_t = IGKDataContext.CreateNewRow(t);
                v_t["clName"] = groupname;
                Insert(t, v_t.ToDictionary());
            }

            public static void group_associateUser(string groupname, string UserId)
            {
                Type t = typeof(ITogoTbGroups);
                IIGKDataQueryResult v_r =  SelectAll(t, new Dictionary<string, object>() { 
                    {"clName" , groupname}
                });
                if (IsSingle(v_r))
                {
                    var v_t = IGKDataContext.CreateNewRow(typeof(ITogoTbUserGroups));
                    v_t["clUserId"] = UserId;
                    v_t["clGroupId"] = v_r.GetRowAt(0)[CoreDataConstant.CL_ID];            
                    Insert(t, v_t.ToDictionary());
                }
            }
            public static void group_removeUser(string groupname, string UserId)
            {
                Type t = typeof(ITogoTbGroups);
                IIGKDataQueryResult v_r = SelectAll(t, new Dictionary<string, object>() { 
                    {"clName" , groupname}
                });
                if (IsSingle(v_r))
                {
                    var v_t = IGKDataContext.CreateNewRow(typeof(ITogoTbUserGroups));
                    v_t["clUserId"] = UserId;
                    v_t["clGroupId"] = v_r.GetRowAt(0)[CoreDataConstant.CL_ID];
                    Delete(t, v_t.ToDictionary());
                }
            }

            public static void group_applyAuth(string groupname, string authid, int auth)
            {
                Type t = typeof(ITogoTbGroups);
                IIGKDataQueryResult v_r = SelectAll(t, new Dictionary<string, object>() { 
                    {"clName" , groupname}
                });
                if (IsSingle(v_r))
                {
                    var v_t = IGKDataContext.CreateNewRow(typeof(ITogoTbGroupAutorisations));
                    v_t["clGroupId"] = v_r.GetRowAt(0)[CoreDataConstant.CL_ID];
                    v_t["clAuthId"] = authid;
                    v_t["clAutorisation"] = auth.ToString();
                    Delete(t, v_t.ToDictionary());
                }
            }

            public static string auth_getId(string name)
            {
                return GetTableId(typeof(ITogoTbGroupAutorisations), new Dictionary<string, object>() { 
                    {"clName", name}
                });
            }

            public static string GetTableId(Type table, Dictionary<string, object> filter){
                var r = SelectAll(table, filter);
                if (IsSingle(r))
                {
                    return r.GetRowAt(0)[CoreDataConstant.CL_ID];
                }
                return null;
            }
            /// <summary>
            /// get if data feature exist
            /// </summary>
            /// <param name="name"></param>
            /// <returns></returns>
            private static bool IsDataFeatures(string name)
            {            
                string[] t = IGKDataContext.GetTables();
                List<string> b = new List<string>();
                return t.Contains(name);
            }
            internal static IIGKDataRowCollections feature_getAllCustomFeatures()
            {
                Type v_t = typeof(ITogoTbCustomFeatureTables);
                return CoreDBManager.SelectAll(v_t).Rows ;
            }

            /// <summary>
            /// retreive column information associate to a custom feature
            /// </summary>
            /// <param name="name"></param>
            /// <returns></returns>
            private static IGKDBColumnInfo[] feature_getCustomFeatureColumn(string name)
            {
                Type v_t = typeof(ITogoTbCustomFeatureTables);
                var q = CoreDBManager.SelectAll(v_t, new Dictionary<string, object>() { { "clName", name } });
                if (q.RowCount == 1)
                {
                    var r = q.GetRowAt(0);
                    int featId = r.GetValue<int>(CoreDataConstant.CL_ID);
                    List<IGKDBColumnInfo> tb = new List<IGKDBColumnInfo>();

                    var qq = SelectAll(typeof(ITogoTbFeatureTableValue), new Dictionary<string, object>() { 
                        {"clTable", featId }
                    });
                    Dictionary<string, IIGKDataRow> c = new Dictionary<string, IIGKDataRow>();
                    foreach (IIGKDataRow  item in qq.Rows)
                    {
                        c.Add(item["clColumnName"], item);
                    }

                    IIGKDataQueryResult v_rr =  SelectTableInfo(name);

                    foreach (IIGKDataRow v_row in v_rr.Rows )
                    {
                        IGKDBColumnInfo v_c = new IGKDBColumnInfo();
                        v_c.clName = v_row["Field"];
                        if (c.ContainsKey(v_c.clName))
                        {
                            v_c.clType = "Int";// c[v_c.clName]["clColumnType"];
                        }
                        else {
                            v_c.clType = v_row["Type"];
                        }
                        tb.Add(v_c);
                    }

                    return tb.ToArray();
                }
                //not found
                return null;

            }

            private static IIGKDataQueryResult SelectTableInfo(string name)
            {
                return SendQuery (string.Format ("SHOW COLUMNS FROM `{0}`", name ));
            }
            /// <summary>
            /// add a new custom feature
            /// </summary>
            /// <param name="p"></param>
            /// <param name="togoDbColumnInfo"></param>
            /// <param name="description"></param>
            /// <returns></returns>
            public static bool  feature_addCustomFeatureTable(string p, ITogoDbColumnInfo[] togoDbColumnInfo, string description)
            {
                if (string.IsNullOrWhiteSpace(p))
                    return false ;
                p = p.ToLower();
                var info = (new[] { new {Name = "" , Table = "" } }).ToList();
                info.Clear();
                foreach (var item in togoDbColumnInfo)
                {
                    if (IsDataFeatures(item.clType))
                    {
                        info.Add ( new {
                        Name = item.clName ,
                        Table  = item.clType 
                        });
                        item.clType = "Int";
                    }
                }
               IIGKDataQueryResult r =  SendQuery(CoreDBManager.CreateTableQuery(p, togoDbColumnInfo, description));
               if (r != null)
               {
                   Type v_t = typeof(ITogoTbCustomFeatureTables);
                   var q = IGKDataContext.CreateNewRow(v_t);
                   q["clName"] = p;
                   IIGKDataQueryResult b = Insert(v_t, q.ToDictionary());
                   if (b.AffectedRow == 1)
                   {
                      int i=  SelectAll(v_t, new Dictionary<string, object>() { 
                           {"clName", p} 
                       }).GetRowAt (0).GetValue<int>(CoreDataConstant.CL_ID);

                      foreach (var item in info)
                      {
                          Insert(typeof(ITogoTbFeatureTableValue), new Dictionary<string, object>() { 
                           {"clTable", i} ,
                           {"clColumnName", item.Name} ,
                           {"clColumnType", item.Table } 
                       });
                      }
                   }
                   return true;
               }
               return false;
            }
            /// <summary>
            /// add a new custom feature
            /// </summary>
            /// <param name="p"></param>
            /// <param name="togoDbColumnInfo"></param>
            /// <param name="description"></param>
            /// <returns></returns>
            public static bool feature_removecustomFeatTable(string customfeatTableName)
            {
                if (string.IsNullOrWhiteSpace(customfeatTableName))
                    return false;
                customfeatTableName = customfeatTableName.ToLower();
                Type v_t = typeof(ITogoTbCustomFeatureTables);
                var q = IGKDataContext.CreateNewRow(v_t);
                q["clName"] = customfeatTableName;
                int i = SelectAll(v_t, new Dictionary<string, object>() { 
                           {"clName", customfeatTableName} 
                       }).GetRowAt(0).GetValue<int>(CoreDataConstant.CL_ID);
               IIGKDataQueryResult  v = Delete(typeof(ITogoTbCustomFeatureTables), q.ToDictionary());
               if ((v != null) && (v.AffectedRow == 1))
               {
                   SendQuery(string.Format("DROP TABLE `{0}`;", customfeatTableName));
                   Delete(typeof(ITogoTbFeatureTableValue),
                        new Dictionary<string, object>() { 
                           {CoreDataConstant.CL_ID, i} 
                       });
                   return true;
               }
                return false;
            }

            public static IIGKDataRowCollections task_getAllTask()
            {

                var q = CoreDBManager.SelectAll(typeof(ITogoTbTask));
                if (q!=null)
                return q.Rows;
                return null;
            }
            private static IIGKContext sm_context;
            /// <summary>
            /// get the context
            /// </summary>
            public static IIGKContext Context { get {
                if (sm_context == null)
                    sm_context = new TogoContextInfo();
                return sm_context;  } }

            public static void users_regTaskHistory(string clUserId, string cltaskId)
            {
                Type t = typeof(ITogoTbTaskHistory);
                IIGKDataRow row = IGKDataContext.CreateNewRow(t);
                row["clUserId"] = clUserId ;
                row["clTaskId"] = cltaskId ;
                 row["clDateTime"] = GetDateNow ();
                Insert (t, row.ToDictionary());
            }

            internal static bool  task_addNew(string taskName, string Number, string desc)
            {
                try
                {
                    int n = Convert.ToInt32(Number);

                    IIGKDataQueryResult r = Insert(typeof(ITogoTbTask), new Dictionary<string, object>() { 
                    {"clName", taskName },
                    {"clCodeNumber", n},
                    {"clDescription", desc}
                });
                    return (r.AffectedRow == 1);
                }
                catch { 
                }
                return false;
            }
            /// <summary>
            /// return all registrated users types
            /// </summary>
            /// <returns></returns>
            internal static IIGKDataRowCollections GetAllUserTypes()
            {
                return SelectAll(typeof(ITogoTbUserType)).Rows;
            }
                 * */


        /// <summary>
        /// find in table width a Like method on all dictionary.
        /// </summary>
        /// <typeparam name="T">type to request</typeparam>
        /// <param name="dictionary">dictionary</param>
        /// <returns></returns>
        public static ICoreDataQueryResult FindLike<T>(Dictionary<string, object> dictionary)
        {
            string v_typename = CoreDataContext.GetTableName(typeof(T));
            if (string.IsNullOrEmpty(v_typename))
                return null;
            string query = GetFindQuery(v_typename, dictionary);
            return SendQuery(query);
        }


        #region  QUERY UTILS FUNCTION
        private static string GetFindQuery(string tablename, Dictionary<string, object> dictionary)
        {
            return Adapter.GetFindQuery(tablename, dictionary);
        }

        public static string GetUpdateQuery(string table, Dictionary<string, object> values, Dictionary<string, object> condition)
        {
            return Adapter.GetUpdateQuery(table, values, condition);
        }

        public static string GetAndCondition(Dictionary<string, object> andCondition)
        {

            return Adapter == null ? String.Empty : Adapter.GetAndCondition(andCondition);
        }
        public static string GetOrCondition(Dictionary<string, object> orCondition)
        {
            return Adapter.GetOrCondition(orCondition);

        }

        #endregion

        /// <summary>
        /// create a first row for single instance
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="identifier"></param>
        /// <returns></returns>
        public static T SelectFirstRowInstance<T>(ICoreDataTableColumnIdentifier identifier)
        {
            return CreateFirstRowItemInstance<T>(SelectAll<T>(identifier));
        }
        /// <summary>
        /// create a first row for single instance
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="identifier"></param>
        /// <returns></returns>
        public static T SelectFirstRowInstance<T>(Dictionary<string, object> identifier)
        {
            return CreateFirstRowItemInstance<T>(SelectAll<T>(identifier));
        }
        /// <summary>
        /// create a first row for single instance
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="identifier"></param>
        /// <returns></returns>
        public static T SelectFirstRowInstance<T>(int identifier)
        {
            return CreateFirstRowItemInstance<T>(SelectAll<T>(identifier.ToIdentifier()));
        }
        /// <summary>
        /// create all row instance from data query
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="r"></param>
        /// <returns></returns>
        public static T[] CreateRowsItemInstance<T>(this ICoreDataQueryResult r)
        {
            List<T> v_t = new List<T>();
            if ((r != null) && (r.RowCount > 0))
            {
                foreach (ICoreDataRow item in r.Rows)
                {
                    v_t.Add(item.CreateInterfaceInstance<T>());
                }
            }
            return v_t.ToArray();
        }
        /// <summary>
        /// Create instance row with query result. item
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="r"></param>
        /// <returns></returns>
        public static T CreateFirstRowItemInstance<T>(this ICoreDataQueryResult r)
        {
            if ((r != null) && (r.RowCount > 0))
            {
                return r.GetRowAt(0).CreateInterfaceInstance<T>();
            }
            return default(T);
        }

        /// <summary>
        /// Create and instance by selecting the row by type
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="andCondition"></param>
        /// <returns></returns>
        public static T CreateSelectionInterfaceInstance<T>(Dictionary<string, object> andCondition)
        {
            return CoreDBManager.CreateFirstRowItemInstance<T>(CoreDBManager.SelectAll<T>(andCondition));
        }
        /// <summary>
        /// Create and instance by selecting the row by type. or inserting the new data if not registrated.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="andCondition"></param>
        /// <returns></returns>
        public static T CreateNewSelectionInterfaceInstance<T>(Dictionary<string, object> andCondition)
        {
            if (CoreDBManager.SelectAll<T>(andCondition).RowCount == 0)
            {
                var q = CoreDBManager.Insert(typeof(T), andCondition);
                if (q.AffectedRow == 0)
                {
                    throw new Exception("Can't add new item to data base");
                }
            }
            return CoreDBManager.CreateFirstRowItemInstance<T>(CoreDBManager.SelectAll<T>(andCondition));
        }
        /// <summary>
        /// Create and instance by selecting the row by type. or inserting the new data if not registrated.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="andCondition"></param>
        /// <returns></returns>
        public static T CreateNewSelectionInterfaceInstance<T>(ICoreDataTableColumnIdentifier id)
        {
            return CoreDBManager.CreateNewSelectionInterfaceInstance<T>(id.ToDictionary());
        }

        public static void CommitTransaction()
        {
            Adapter.CommitTransaction();
        }

        public static void RollBackTransaction()
        {
            Adapter.RollBackTransaction();
        }

        public static void BeginTransaction()
        {
            Adapter.BeginTransaction();
        }
        /// <summary>
        /// store the current data schema info
        /// </summary>
        /// <param name="destinationFile"></param>
        public static bool StoreDataSchema(string destinationFile, bool withentries = false)
        {
            return CoreDataContext.StoreDataSchema(destinationFile, Adapter, withentries);
        }

        //public static bool LoadDataSchema(string filename)
        //{
        //    try
        //    {
        //        var s = CoreXmlElement.LoadFile(filename);
        //        return CoreDataContext.LoadDataSchema(s, Adapter);
        //    }
        //    catch(Exception ex)
        //    {
        //    }
        //    return false;
        //}

        public static bool RowExists<T>(ICoreDataTableColumnIdentifier identifier)
        {
            var r = SelectFirstRowInstance<T>(identifier);
            return r != null;
        }

        public static void ClearTable<T>()
        {
            string tbname = CoreDataContext.GetTableName(typeof(T));
            if (string.IsNullOrEmpty(tbname) == false)
                ClearTable(tbname);
        }

    }
}
