using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace IGK.ICore.DB
{
    using System.Reflection.Emit;
    using System.Diagnostics;
    using System.ComponentModel;
    using IGK.ICore;
    using IGK.ICore.Extensions;
    using IGK.ICore.WinUI;
    using IGK.ICore.ComponentModel;
    using IGK.ICore.Xml;
    using System.IO;
   

    /// <summary>
    /// get the data context manager
    /// </summary>
    public static class CoreDataContext
    {
        static object SyncObject = new object();
        static Dictionary<Type, string> sm_dataTables;
        static List<Type> sm_initialised; // used for data table initialize.
                                          // an initialize table is a table a present table
        static bool sm_dataLoaded;


        public static bool IsDataLoaded {
            get {
                return sm_dataLoaded;
            }
        }
        /// <summary>
        /// create new row and convert
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T CreateNewRow<T>()
        {  
            return (T)CreateNewRow(typeof(T));
        }



        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static ICoreDataSet CreateEmptyDateSet()
        {
            return new CoreDataSet();
        }


        
        static CoreDataContext() {
            sm_dataTables = new Dictionary<Type, string>();
        }

        public static CoreDataAdapterBase Init(string name, string connectionString, Assembly tableAssembly) {
            CoreDataAdapterBase adapter = CoreDataAdapterBase.CreateAdapter(name);
            try
            {
                if ((adapter != null) && adapter.Connect(connectionString))
                {
                    LoadDataBase(adapter, tableAssembly);
                    OnLoadingDataAdapter(new ICoreLoadingAdapterEventArgs(adapter));

                    if (adapter != null)
                        CoreLog.WriteDebug(" Init Database ... OK ");
                    return adapter;
                }
                else if (adapter != null)
                {
                    CoreLog.WriteDebug(adapter.ErrorString);
                }
            }
            catch (Exception ex)
            {
#if DEBUG
                CoreLog.WriteDebug(ex.Message);
                CoreMessageBox.Show(ex);
#else
                CoreLog.WriteLine("erreur "+ex.Message);
#endif
            }
            return null;
        }
        public static CoreDataAdapterBase Init(string name,string connectionString)
        {
            CoreLog.WriteLine("[GS] - Create DataAdapter ["+name+"]");
            CoreDataAdapterBase adapter = CoreDataAdapterBase.CreateAdapter(name);
            try
            {
                if ((adapter != null) && adapter.Connect(connectionString))
                {
                    //init data base
                    CoreLog.WriteDebug("Init Database ... ");

                    LoadDataBase(adapter);
                    OnLoadingDataAdapter(new ICoreLoadingAdapterEventArgs(adapter));

                    if (adapter !=null)
                        CoreLog.WriteDebug(" Init Database ... OK ");
                    return adapter;
                }
                else if (adapter != null)
                {
                    CoreLog.WriteDebug(adapter.ErrorString);
                }
            }
            catch (Exception ex) {
#if DEBUG
                CoreLog.WriteDebug(ex.Message);
                CoreMessageBox.Show(ex);                
#else 
                CoreLog.WriteLine("erreur "+ex.Message);
#endif
            }
            return null;
        }
        /// <summary>
        /// event raised after data adapter loaded
        /// </summary>
        public static event EventHandler<ICoreLoadingAdapterEventArgs> LoadingDataAdapter;



        private static Assembly sm_dynamicDataAssembly;
        private static AssemblyBuilder sm_asmBuilder;
        private static ModuleBuilder sm_module;

        private static void OnLoadingDataAdapter(ICoreLoadingAdapterEventArgs e)
        {           
            if (LoadingDataAdapter != null)
            {
                LoadingDataAdapter(SyncObject, e);
            }
        }

      

        /*
         * AFTER CREATE THE ADAPTER YOU NEED TO INITIALIZE DATABASE INFO. 
         * LoadDataBase Find from ASSEMBLY AND INIT DATA BASE
         * 
         * 
         **/ 
        /// <summary>
        /// Create Data base . 
        /// </summary>
        /// <param name="adapter"></param>
        ///<param name="loadAssembly">Assembly that is the base of loaded assembly or null to load all present assembly</param>
       internal static void LoadDataBase(CoreDataAdapterBase adapter, Assembly loadAssembly=null)
        {
            Assembly[] v_asm = null;
            CoreDataBaseInitializerAttribute v_attr = null;
            if (loadAssembly == null)
            {
                v_asm = AppDomain.CurrentDomain.GetAssemblies();
            }
            else {
                v_asm = new Assembly[] { loadAssembly };
                v_attr = loadAssembly.GetCustomAttribute(typeof(CoreDataBaseInitializerAttribute)) as
                    CoreDataBaseInitializerAttribute;

               
            }
            sm_dataTables.Clear();
            foreach (Assembly asm in v_asm)
            {
#if DEBUG
                CoreLog.WriteLine($"dataLib : {asm.FullName}");
#endif
                RegisterTables(asm);
            }
            if (adapter.AdapterName.ToLower() == "mysql")
            {
                //for debuggin purpose only
                #if DEBUG_DEV
                    Log.CWriteLine("DropDataBase : " + adapter.DataBaseName);
                    adapter.SendQuery("DROP DATABASE `" + adapter.DataBaseName + "`");
                #endif
                if (!string.IsNullOrEmpty(adapter.DataBaseName))
                {
                    adapter.SendQuery("CREATE DATABASE IF NOT EXISTS `" + adapter.DataBaseName + "`");
                    adapter.SelectDb();
                    InitTable(adapter, v_attr?.InitializerType );
                    sm_dataLoaded = true;
                }
                else {
                    CoreLog.WriteDebug("Data basename is empty");
                }
            }
            else
            {
                InitTable(adapter, v_attr?.InitializerType);
                sm_dataLoaded = true;
            }
            //register assembly for register aditionnal library width data
            AppDomain.CurrentDomain.AssemblyLoad += new CoreDataContextLoader(adapter).LoadAssembly;
        }
        /// <summary>
        /// register assembly tables
        /// </summary>
        /// <param name="asm"></param>
       internal  static bool  RegisterTables(Assembly asm)
       {
           if ((asm == null) || (asm.IsDynamic ))
               return false ;
           bool v_o = false;
           try
           {
               foreach (Type t in asm.GetTypes())
               {
                   CoreDataTableAttribute c = Attribute.GetCustomAttribute(t, typeof(CoreDataTableAttribute)) as CoreDataTableAttribute;
                   if (c != null)
                   {
                       RegisterTable(c, t);
                       v_o = true;
                   }
               }
           }
           catch (Exception ex) {
                //exceptions raised if this assembly can't load get types
                CoreLog.WriteDebug(ex.Message);
           }
           return v_o;
       }
        /// <summary>
        /// register table
        /// </summary>
        /// <param name="attribute"></param>
        /// <param name="t"></param>
        public static void RegisterTable(CoreDataTableAttribute attribute, Type t)
        {
            string n = CoreDataTableAttribute.GetTableName(t);
            if (sm_dataTables.ContainsKey(t))
            {
                string v = sm_dataTables[t];
                throw new Exception("data exception : table already register " + v);
            }
            sm_dataTables.Add(t, n);
            //RegisterFeature(t, n);
        }

     
        /// <summary>
        /// Initialize the data 
        /// </summary>
        /// <param name="adapter"></param>
        internal static void InitTable(CoreDataAdapterBase adapter, Type initType=null)
        {
            if (sm_initialised == null)
                sm_initialised = new List<Type>();      
            int i = 0;
            var g = sm_dataTables.ToArray().GetEnumerator();
            while (g.MoveNext()) {
                KeyValuePair<Type, string> item = (KeyValuePair<Type, string>)g.Current;
                i++;
                if (IsInitialized(item.Key))
                    continue;
                if (LoadAddInitDataTable(item.Key, adapter, initType))
                {
                    sm_initialised.Add(item.Key);
                }
            }
            //foreach (KeyValuePair<Type, string> item in sm_dataTables)
            //{
            //    i++;
            //    if (IsInitialized (item.Key))
            //        continue ;
            //    if (LoadAddInitDataTable(item.Key, adapter, initType))
            //    {
            //        sm_initialised.Add(item.Key);
            //    }
            //}
            adapter.InitComplete();
        }
#if DEBUG
        public 
#else 
            internal
#endif
            static bool LoadAddInitDataTable(Type type, CoreDataAdapterBase adapter, Type initType=null)
        {
            if ((type == null) ||
                (adapter ==null))
                return false ;
            //if (!IsDataLoaded)
            //    return false;
            CoreDataTableAttribute c = Attribute.GetCustomAttribute(type, typeof(CoreDataTableAttribute)) as CoreDataTableAttribute;
            string q = string.Empty;
            string v_tableName = CoreDataTableAttribute.GetTableName(type);
            if (string.IsNullOrEmpty(v_tableName))
                return false;
            if (!adapter.TableExists(v_tableName))//v_r == 0)
            {//no data table
                //create table query

                if (adapter.CreateTable(v_tableName,
                     CoreDataContext.GetColumnInfo(type, adapter),
                     c.Description))
                {
                    //DONE: INIT DEFAULT DATA PROPERTY
                    if (!InitDataTable(type, adapter, initType  ))
                    {
                        CoreLog.WriteLine("failed to init data for table " + v_tableName);
                    }
                }
                else
                    return false;
            }
            return true ;
        }
        /// <summary>
        /// initialize data table
        /// </summary>
        /// <param name="type"></param>
        /// <param name="adapter"></param>
        /// <returns></returns>
        public static bool InitDataTable(Type type, CoreDataAdapterBase adapter, Type initType=null)
        {
            CoreDataTableAttribute p = Attribute.GetCustomAttribute(type, typeof(CoreDataTableAttribute)) as CoreDataTableAttribute;
            CoreDataInitRequiredAttribute[] s = (CoreDataInitRequiredAttribute[])Attribute.GetCustomAttributes(type, typeof(CoreDataInitRequiredAttribute), false);


            string v_tableName = CoreDataTableAttribute.GetTableName(type);
            Type v_initializer = p.Initializer ?? initType;

            if (p != null)
            {
                for (int i = 0; i < s.Length; i++)
                {
                    if (!IsInitialized(s[i].Type))
                    {
                        if (LoadAddInitDataTable(s[i].Type, adapter))
                        {
                            sm_initialised.Add(s[i].Type);
                        }
                    }
                }
                if (v_initializer != null)
                {
                    ICoreDataTableInitializer v_r =
                    (v_initializer.Assembly.CreateInstance(v_initializer.FullName) as ICoreDataTableInitializer);
                    if (v_r != null)
                        v_r.Initialize(type, adapter);
                    else
                        Debug.WriteLine("Not initialized");
                }
                else
                {
                    Type t = typeof(CoreDataInitializer);
                    MethodInfo m = t.GetMethod("init_" + v_tableName, BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
                    if (m != null)
                    {
                        m.Invoke(null, new object[] { type, adapter });
                    }
                }
                return true;
            }
            return false;
        }

        private static bool IsInitialized(Type type)
        {
            return sm_initialised.Contains(type);
        }
     
        /// <summary>
        /// Get Interface data type from DataTableName
        /// </summary>
        /// <param name="dataTableName"></param>
        /// <returns></returns>
        public static Type GetInterfaceType(string dataTableName)
        {
            if (string.IsNullOrEmpty(dataTableName))
                return null;
            dataTableName = dataTableName.ToLower();
            foreach (var item in sm_dataTables)
            {
                if (item.Value.ToLower() == dataTableName)
                    return item.Key;
            }
            return null;
        }
        /// <summary>
        /// retrieve table nom from table
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public static string GetTableName(Type t)
        {
            if (t == null)
                return null;
            if (sm_dataTables.ContainsKey (t))
                return sm_dataTables[t];
            string p = CoreDataTableAttribute.GetTableName(t);
                if (!string.IsNullOrEmpty (p))
                {
                    sm_dataTables.Add (t, p);
                    return p;
                }
            return null;
        }

        public static string[] GetTables()
        {
            return sm_dataTables.Values.ToArray<string>();
        }

        public static PropertyInfo[] GetPropertiesList(Type t)
        {
            List<PropertyInfo> lc = new List<PropertyInfo>();
            PropertyInfo[] h = t.GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (Type k in t.GetInterfaces())
            {
                lc.AddRange(k.GetProperties(BindingFlags.Public | BindingFlags.Instance));
            }
            lc.AddRange(h);
            return lc.ToArray();
        }
        
        /// <summary>
        /// retrieve the column info table
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
public static ICoreDataColumnInfo[] GetColumnInfo(Type t, CoreDataAdapterBase dataAdapter)
{
    if ((t == null) || (dataAdapter == null))
        return null;
    List<ICoreDataColumnInfo> m = new List<ICoreDataColumnInfo>();

    Dictionary<string, PropertyInfo> lc = new Dictionary<string, PropertyInfo>();
    PropertyInfo[] h = t.GetProperties(BindingFlags.Public | BindingFlags.Instance);
    
    foreach (Type k in t.GetInterfaces ())
    {
        //merge properties
        __appendProperties(lc, k.GetProperties (BindingFlags.Public | BindingFlags.Instance));
    }
    __appendProperties(lc, h);
    
    foreach (PropertyInfo  pr in lc.Values)
    {
        ICoreDataColumnInfo c = CoreDataColumnInfo.CreateTableColumnInfo(t, pr);
        c.clName = pr.Name;
        c.clType = dataAdapter.GetColumnType(pr.PropertyType);
        c.clIsIndex = pr.PropertyType.ImplementInterface(typeof(ICoreDataCell));
        CoreDataTableFieldAttribute d = Attribute.GetCustomAttribute(pr, typeof(CoreDataTableFieldAttribute)) as
                    CoreDataTableFieldAttribute;
        if (d != null)
        {
            if (!string.IsNullOrEmpty(d.ColumnName))
                c.clName = d.ColumnName;
            c.clAutoIncrement = ((d.Binding & enuCoreDataField.AutoIncrement) == enuCoreDataField.AutoIncrement);
            c.clIsUnique = ((d.Binding & enuCoreDataField.Unique) == enuCoreDataField.Unique);
            c.clIsPrimary = ((d.Binding & enuCoreDataField.IsPrimaryKey) == enuCoreDataField.IsPrimaryKey);
            c.clIsUniqueColumnMember = ((d.Binding & enuCoreDataField.UniqueColumnMember) == enuCoreDataField.UniqueColumnMember);

            if (c.clIsUniqueColumnMember)
            {
                c.clColumnMemberIndex = d.MemberIndex;
            }
            c.clDescription = d.Description;
            c.clDefault = d.Default;
            c.clInsertFunction = d.InsertFunction;
            c.clUpdateFunction = d.UpdateFunction;

            if (d.TypeName != null)
                c.clType = d.TypeName;
            c.clTypeLength = d.Length;
            c.clNotNull = ((d.Binding & enuCoreDataField.IsNotNull) == enuCoreDataField.IsNotNull);
        }
        else {
            c.clNotNull = IsNullable(c.clType);
        }
        m.Add(c);

        
    }
 	return m.ToArray ();
}

private static void __appendProperties(Dictionary<string, PropertyInfo> lc, PropertyInfo[] propertyInfos)
{
    foreach (PropertyInfo item in propertyInfos)
    {
        if (lc.ContainsKey(item.Name))
        {
            lc[item.Name] = item;
        }
        else {
            lc.Add(item.Name, item);
        }
    }
}

static bool IsNullable(string type)
{
    switch (type.ToLower())
    { 
        case  "int":
        case "float":
        case "datetime":
            return true;
    }
    return false;
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
      
        /// <summary>
        /// 
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public static ICoreDataRow CreateNewRow(string[] t)
        {
            return CoreDummyDataRow.Create(t);
        }
        private static Assembly DynamicDataAssembly
        {
            get {
                return sm_dynamicDataAssembly;
            }
        }
        private static AssemblyBuilder AsmBuilder {
            get {
                return sm_asmBuilder;
            }
        }

        public static T CreateInterface<T>() {
            return (T)CreateInterface(typeof(T), null, null);
        }
        /// <summary>
        /// Create a dynamic object from datatable interface type
        /// </summary>
        /// <param name="interfaceType">type de l'interface que l'on doit créer</param>
        /// <param name="row">initial row value, can be null</param>
        /// <param name="context">DB Context creation can be null</param>
        /// <returns></returns>
        public static object CreateInterface(Type interfaceType, 
            ICoreDataRow row, ICoreDataContext context)
        {
            //if ((interfaceType == null) || (!interfaceType.IsInterface) || (row == null))
                if ((interfaceType == null) || (!interfaceType.IsInterface) )
                return null;
            string v_name = CoreDataContext.GetTableName(interfaceType);
            if (v_name == null) {
                if (row == null)
                    return null;
                v_name = CoreDataContext.GetTableName(row.SourceTableInterface);
                if (string.IsNullOrEmpty(v_name))
                    throw new CoreException("CoreDataContext::CreateInterface OperationNotValid");
            }
            return CreateInterface(v_name, row, context, interfaceType );
            
        }

        public static object CreateInterface(string tableName, ICoreDataRow row, ICoreDataContext context, Type interfaceType = null)
        {
            if (string.IsNullOrEmpty(tableName))
                return null;
            if (interfaceType == null)
            {
                if (row != null)
                {
                    if (row.SourceTableInterface == null)
                        return null;
                    interfaceType = row.SourceTableInterface;
                }
            }

            string v_tname = "DataTable." + tableName;
            Type v_tt = null;
            Assembly asm = DynamicDataAssembly;
            if (asm != null)
            {
                v_tt = asm.GetType(v_tname);
                if (v_tt != null)
                {
                    return CreateDateObj(v_tt, tableName, row, context);
                }
            }
#if DEBUG
            CoreLog.WriteLine("Create new type Interface Object From : " + tableName);
#endif
            AssemblyBuilder v_asmBuilder = null;
            if (sm_asmBuilder == null)
            {

                AssemblyName v_asmName = new AssemblyName();
                v_asmName.Name = CoreDataConstant.ASM_DYNAMIC_NAME;
                //create assembly builder
                 v_asmBuilder = AppDomain.CurrentDomain.DefineDynamicAssembly(v_asmName, AssemblyBuilderAccess.Run);
                 sm_asmBuilder = v_asmBuilder;
                 sm_module = v_asmBuilder.DefineDynamicModule("DataTable");
            }
            else {
                v_asmBuilder = sm_asmBuilder;
            }
          
            //define module
            //ModuleBuilder v_module = v_asmBuilder.DefineDynamicModule("DataTable");
            //create type
            TypeBuilder v_tbuilder = sm_module.DefineType(v_tname, TypeAttributes.Public, typeof(CoreDataBaseTable));
            //add interface
            v_tbuilder.AddInterfaceImplementation(interfaceType);
            if ((row != null) && (row.SourceTableInterface !=null) && (row.SourceTableInterface != interfaceType))
            {
                v_tbuilder.AddInterfaceImplementation(row.SourceTableInterface);

            }
            //add Attribute 
            CustomAttributeBuilder v_attrbuilder = null;

            v_attrbuilder = new CustomAttributeBuilder(
                typeof(CoreDataTableAttribute).GetConstructor(new Type[]{typeof(string)}),
                new object[] { tableName }
            );

            v_tbuilder.SetCustomAttribute (v_attrbuilder);
            //build properties
            List<PropertyInfo> v_lt = new List<PropertyInfo>();
            Dictionary<string, PropertyInfo> v_pros = new Dictionary<string, PropertyInfo>();
            v_pros.Clear();
            foreach (Type t in interfaceType.GetInterfaces())
            {
                __mergePropertiesList(v_pros, t.GetProperties());
                //v_lt.AddRange(t.GetProperties());
            }

            PropertyInfo[] tab = interfaceType.GetProperties();
            __mergePropertiesList(v_pros, tab);
            if ((row != null) && (row.SourceTableInterface!=null) && (interfaceType != row.SourceTableInterface))
            {
                foreach (Type t in row.SourceTableInterface.GetInterfaces())
                {
                    __mergePropertiesList(v_pros, t.GetProperties());
                    //v_lt.AddRange(t.GetProperties());
                }
                __mergePropertiesList(v_pros, row.SourceTableInterface.GetProperties());
            }

           
           // tab = v_pros.Values.ToArray .ToArray();
            FieldBuilder fb = null;
            PropertyBuilder propertyBuilder = null;
            //build emite properties
            foreach (KeyValuePair<string, PropertyInfo> kprInfo in v_pros)
            {
                var s = kprInfo.Key;
                var prInfo = kprInfo.Value;

                if (s.Contains("::"))
                { 
                    //special properties
                    LoadSpecialTypeIntefaceDeclaration(v_tbuilder, prInfo);

                    continue;
                }
                //default behaviour

                fb = v_tbuilder.DefineField("m_" + prInfo.Name, prInfo.PropertyType,
                       FieldAttributes.Private);
                propertyBuilder = v_tbuilder.DefineProperty(prInfo.Name, PropertyAttributes.None, prInfo.PropertyType, null);               

                if (prInfo.CanWrite)
                {
                    //v_tbuilder.AddME
                    MethodBuilder v_builder = v_tbuilder.DefineMethod("set_" + prInfo.Name,
                         MethodAttributes.Public |
                         MethodAttributes.HideBySig |
                         MethodAttributes.SpecialName |
                         MethodAttributes.Virtual, null, new Type[] { 
                            prInfo.PropertyType 
                        });
                    ILGenerator setIL = v_builder.GetILGenerator();
                    setIL.Emit(OpCodes.Ldarg_0);
                    setIL.Emit(OpCodes.Ldarg_1);
                    setIL.Emit(OpCodes.Stfld, fb);
                    setIL.Emit(OpCodes.Ret);
                    propertyBuilder.SetSetMethod(v_builder);
                }
                if (prInfo.CanRead)
                {
                    MethodBuilder v_builder = v_tbuilder.DefineMethod("get_" + prInfo.Name,
                        MethodAttributes.Public |
    MethodAttributes.HideBySig | MethodAttributes.SpecialName | MethodAttributes.Virtual,
    prInfo.PropertyType, Type.EmptyTypes);

                    ILGenerator getIL = v_builder.GetILGenerator();
                    getIL.Emit(OpCodes.Ldarg_0);
                    getIL.Emit(OpCodes.Ldfld, fb);
                    getIL.Emit(OpCodes.Ret);

                    propertyBuilder.SetGetMethod(v_builder);

                }

            }

            //add getsource method properties
            
            FieldBuilder v_fb = v_tbuilder.DefineField("m_sourceTableInterface", typeof(Type),
                         FieldAttributes.Private);
            MethodBuilder v_mbuilder = v_tbuilder.DefineMethod(
               nameof(ICoreDataTable.GetSourceTableInterface),
                //"getSourceTableInterface",
                        MethodAttributes.Public |
                        MethodAttributes.HideBySig | MethodAttributes.Virtual,
                        typeof(Type), Type.EmptyTypes);

              ILGenerator v_getIL = v_mbuilder.GetILGenerator();
              v_getIL.Emit(OpCodes.Ldarg_0);
              v_getIL.Emit(OpCodes.Ldfld, v_fb);
              v_getIL.Emit(OpCodes.Ret);

              //foreach (var m in typeof(IGSDataValue).GetMethods())
              //{
              //    v_mbuilder = v_tbuilder.DefineMethod(m.Name ,
              //         MethodAttributes.Public |
              //         MethodAttributes.HideBySig | MethodAttributes.Virtual,
              //         m.ReturnType, Type.EmptyTypes);
              //}
            


            Type v_type = v_tbuilder.CreateType();

           sm_dynamicDataAssembly = sm_dynamicDataAssembly ??  FindAssembly(AppDomain.CurrentDomain.GetAssemblies(), CoreDataConstant.ASM_DYNAMIC_NAME);  
            if (v_type != null)
            {
                return CreateDateObj(v_type, tableName, row, context);
            }
            return null;

        }

        private static void LoadSpecialTypeIntefaceDeclaration(TypeBuilder v_tbuilder, PropertyInfo prInfo)
        {
           var  propertyBuilder = v_tbuilder.DefineProperty(prInfo.Name, PropertyAttributes.None, prInfo.PropertyType, null);

            if (prInfo.CanWrite)
            {
                //v_tbuilder.AddME
                MethodBuilder v_methBuilder = v_tbuilder.DefineMethod(prInfo.DeclaringType.Name+".set_" + prInfo.Name,
                     MethodAttributes.Private  |
                     MethodAttributes.HideBySig |
                     MethodAttributes.SpecialName |
                     MethodAttributes.Virtual |
                     MethodAttributes.NewSlot 
                     , null, new Type[] { 
                            prInfo.PropertyType 
                        });
                ILGenerator setIL = v_methBuilder.GetILGenerator();
                setIL.Emit(OpCodes.Ldarg_0);
                setIL.Emit(OpCodes.Ldarg_1);
                //setIL.Emit(OpCodes.Call, 
                setIL.Emit(OpCodes.Stfld, v_tbuilder.Name+"::m_"+prInfo.Name);
                setIL.Emit(OpCodes.Ret);
                
                v_tbuilder.DefineMethodOverride(v_methBuilder , 
                    prInfo.SetMethod);
                //propertyBuilder..SetSetMethod(v_builder);
            }
            if (prInfo.CanRead)
            {
                MethodBuilder v_builder = v_tbuilder.DefineMethod(prInfo.DeclaringType.Name+".get_" + prInfo.Name,
                    MethodAttributes.Public |
MethodAttributes.HideBySig | 
MethodAttributes.SpecialName | 
MethodAttributes.Virtual | MethodAttributes.NewSlot ,
prInfo.PropertyType, Type.EmptyTypes);

                ILGenerator getIL = v_builder.GetILGenerator();
                getIL.Emit(OpCodes.Ldarg_0);
                getIL.Emit (OpCodes.Call , " sample ");// PropertiesEmitSample."TEST::get_Value()
                getIL.Emit(OpCodes.Ret);

                v_tbuilder.DefineMethodOverride(v_builder, prInfo.GetMethod);

            } 
        }

        private static void __mergePropertiesList(Dictionary<string, PropertyInfo> v_pros, PropertyInfo[] tab)
        {
            foreach (var item in tab)
            {
                if (v_pros.ContainsKey(item.Name))
                {
                    PropertyInfo rinfo = v_pros[item.Name];
                    
                    
                    v_pros[item.Name] = item;
                    if (rinfo.PropertyType != item.PropertyType)
                    {
                        //changing the return type
                        v_pros[rinfo.DeclaringType + "::" + item.Name] = rinfo;
                    }
                }
                else
                    v_pros.Add(item.Name, item);

            }
        }

        private static object CreateDateObj(Type v_tt, string typename, ICoreDataRow row, ICoreDataContext context)
        {            
            object obj = v_tt.Assembly.CreateInstance(v_tt.FullName);
            UpdateProperty(obj, row, context);
            obj.GetType().GetField("m_sourceTableInterface",
   BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.IgnoreCase).SetValue(
   obj,
   CoreDataContext.GetInterfaceType(typename));
            return obj;
        }

        private static void UpdateProperty(object obj, ICoreDataRow row, ICoreDataContext context)
        {
            if (row != null)
            {
                PropertyInfo[] v_ctab = obj.GetType().GetProperties();
                foreach (PropertyInfo i in v_ctab)
                {
                    if (i.CanWrite)
                    {
                        if (i.PropertyType.IsInterface)
                        {
                            string v_tname = GetTableName(i.PropertyType);
                            if (!string.IsNullOrEmpty(v_tname))
                            {//set property value according
                                try
                                {
                                    object v_obj = CreateInterface(
                                        i.PropertyType,
                                        context.SelectAll(i.PropertyType, new Dictionary<string, object>(){
                                        {CoreDataConstant.CL_ID, row[i.Name ]}
                                }).GetRowAt(0),
                                        context);
                                    i.SetValue(obj, v_obj);
                                }
                                catch { 
                                }
                            }
                        }
                        else
                        {
                            TypeConverter c = CoreTypeDescriptor.GetConverter(i.PropertyType);
                            object v_tobj  = row[i.Name ];
                            if (v_tobj != null)
                            {
                                string v_str = v_tobj.ToString();
                                if (!string.IsNullOrEmpty(v_str))
                                {
                                    try
                                    {
                                        i.SetValue(obj, c.ConvertFromString(v_tobj.ToString()));
                                    }
                                    catch { 
                                    }
                                }
                            }
                            else
                                i.SetValue(obj, null);
                        }
                    }
                }
            }
        }

        private static Assembly FindAssembly(Assembly[] assembly, string p)
        {
            foreach (var item in assembly)
            {

                if (item.FullName.Split(',')[0].ToLower() == p.ToLower())
                {
                    return item;
                }

            }
            return null;
        }




        internal static bool StoreDataSchema(string destinationFile, CoreDataAdapterBase adapter, bool withEntries=false )
        {
            CoreXmlElement l =
                GetDataSchema(adapter, withEntries);
            if (l != null)
            {
                File.WriteAllText(destinationFile, l.RenderXML(null));
                return true;
            }
            return false;
        }
        /// <summary>
        /// get the data context current schema
        /// </summary>
        /// <param name="adapter"></param>
        /// <returns></returns>
        public static CoreXmlElement GetDataSchema(CoreDataAdapterBase adapter, bool withentries=false)
        {
       
            if (sm_dataTables.Count == 0)
                return null;
            var l =  CoreXmlElement.CreateXmlNode(CoreDataConstant.DATA_SCHEMAS_TAG);
            List<string> v_tables = new List<string>();
                foreach (KeyValuePair<Type, string> k in sm_dataTables)
            {
                var r = l.Add("DataDefinition") as CoreXmlElement;
                r["TableName"] = k.Value;
                v_tables.Add(k.Value);
                //load defintion
                var p = CoreDataContext.GetColumnInfo(k.Key, adapter);
                if (p != null)
                {
                    var props = typeof(ICoreDataColumnInfo).GetProperties();
                    foreach (var item in p)
                    {
                        var s = r.Add("Column") as CoreXmlElement;
                        foreach (var kitem in props)
                        {
                            var ob = kitem.GetValue(item);
                            switch (kitem.Name)
                            {
                                case "clTypeLength":
                                case "clColumnMemberIndex":
                                    if ((int)ob == 0)
                                        continue;
                                    break;
                                case "clType":
                                    break;
                                default:
                                    if ((ob is bool) && (((bool)ob) == false))
                                        continue;

                                    break;
                            }
                            s[kitem.Name] = ob == null ? null : ob.ToString();
                        }

                    }
                }
            }
               if (withentries && (v_tables .Count > 0)) {

                   var ee = l.Add("Entries") as CoreXmlElement ;

                   foreach (var item in v_tables)
                   {
                       var bb = ee.Add(item) as CoreXmlElement;
                       foreach (ICoreDataRow row in CoreDBManager.SelectAll(item).Rows)
                       {
                          var ii =  bb.Add("Row") as CoreXmlElement;
                          ii.LoadDataAsAttribute(row);
                       }
                   }
                }
            return l;
        }

        //public static bool LoadDataSchema(CoreXmlElement schema, CoreDataAdapterBase DataAdapter)
        //{

        //    foreach (CoreXmlElement item in schema.getElementsByTagName(CoreDataConstant.DATA_DEFINITON_TAG))
        //    {
                
        //    }
        //    return false;
        //}

        //public static bool CreateAssemblyFromSchema(CoreXmlElement schema, CoreDataAdapterBase adapter)
        //{
        //    foreach (CoreXmlElement item in schema.getElementsByTagName(CoreDataConstant.DATA_DEFINITON_TAG))
        //    {
        //        string tbName = item.GetAttributeValue<string>("TableName", null);
        //        if (string.IsNullOrEmpty(tbName)) continue;


        //    }
        //    return false;
        //}
        //public static bool ExportInterfaceSchema(CoreXmlElement schema, string outDir, CoreDataAdapterBase adapter)
        //{
        //    foreach (CoreXmlElement item in schema.getElementsByTagName(CoreDataConstant.DATA_DEFINITON_TAG))
        //    {
        //        string tbName = item.GetAttributeValue<string>("TableName", null);
        //        if (string.IsNullOrEmpty(tbName)) continue;
        //    }
        //    return false;
        //}
        /// <summary>
        /// get if contains data keys as data key table
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool Contains(Type type)
        {
            return sm_dataTables.ContainsKey(type);
        }

        internal static void InitTable(CoreDataAdapterBase adapter, Assembly assembly)
        {
            if (CoreDataContext.RegisterTables(assembly)) {
                InitTable(adapter);
            }
        }
    }
}
