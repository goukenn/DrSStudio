#define GS_PROFESSIONAL

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.IO;



namespace IGK.GS
{
    using IGK.ICore;
    using IGK.GS.WinUI;
    using IGK.GS.DataTable;
    using IGK.ICore.IO;
    using IGK.GS.Settings;
    using IGK.ICore.Codec;
    using IGK.ICore.Menu;
    using IGK.ICore.WinUI;
    using IGK.ICore.Drawing2D;
    using IGK.ICore.Drawing2D.Menu;
    using IGK.ICore.Drawing2D.WinUI;
    using IGK.ICore.WinUI.Configuration;
    using IGK.ICore.Settings;
    ﻿using IGK.GS.Actions;
    using IGK.ICore.Actions;

    /// <summary>
    /// represent the global GSSystem
    /// </summary>
    public class GSSystem
    {
        #region static members
        private static IGSSystemSetting sm_setting;
        private static Dictionary<string, IGSModule> sm_modules;

        #endregion
        
        private GSDataAdapter m_DataAdapter;
        private IGSMainForm m_MainForm;
        private string m_OperationMode;
        private Dictionary<string, Type> m_regsitreadedTables;
        private IGSModule m_currentModule;

        /// <summary>
        /// get the current base module.
        /// </summary>
        public IGSModule CurrentModule {
            get {
                return m_currentModule;
            }
        }

        public GSVersion CurrentVersion {
            get { 
#if GS_DEMO
                return GSVersion.Demo;
#elif GS_PROFESSIONAL
                return GSVersion.Professional;
#elif GS_FREE
                return GSVersion.Free;

#else
                return GSVersion.Debugging;
#endif
            }
        }
        public IGSApplication Application {
            get {
                return CoreApplicationManager.Application  as IGSApplication;
            }
        }

        /// <summary>
        /// Run a GS Program
        /// </summary>
        public static bool Run()
        {
            System.Diagnostics.StackTrace stackTrace = new System.Diagnostics.StackTrace();
            Assembly v_entryAsm = stackTrace.GetFrame(1).GetMethod().DeclaringType.Assembly;
            return RunWithAssembly(v_entryAsm);
        }

        public static bool RunWithAssembly(Assembly entryAsm)
        {
            GSLog.CWriteLine("[GS]-Init ... CoreSystem");
            CoreSystem.InitWithEntryAssembly(entryAsm, true);
            GSLog.CWriteLine("[GS]-Init ... GSSystem");
            GSSystem.Init();
            if (GSSystem.Instance.DataAdapter != null)
            {
                return true;
            }
            return false;
        }
        /// <summary>
        /// call this method before running.
        /// </summary>
        /// <param name="login"></param>
        /// <param name="pwd"></param>
        /// <param name="dbname"></param>
        public static void BindLoginData(string login, string pwd, string dbname)
        {
            var app = GSSettings.Instance;
            app.DataBase = dbname;
            app.Login = login;
            app.Password = pwd;

        }

        internal LoadingTypeHandler m_loadingTypeHandler; // load type handler

       private List<GSActionBase> m_taskAction;
       private Dictionary<string, GSActionBase> m_globalActions;

       public static IGSSystemSetting Settings {
           get {
               
               if (sm_setting == null)
               {
                   sm_setting = LoadSetting() ??  GSSystemDefaultSetting.Instance;
                   
               }
               return sm_setting;
           }
       }

       private static IGSSystemSetting LoadSetting()
       {
           return null;
       }
        /// <summary>
        /// you need to connect system
        /// </summary>
        /// <param name="login"></param>
        /// <param name="PWD"></param>
        /// <returns></returns>
        public static bool Connect(string login, string PWD)
        {
            GSDataAdapter adapt = Instance.DataAdapter;
            if (adapt == null)
            {
                var d = CreateAdapter();
                if (d == null)
                {
                    GSLog.EWriteLine("Aucun adapter créer - la connexion vas echouer");
                    return false;
                }
                Instance.DataAdapter = d;
            }
            IGSDataQueryResult r = GSDB.SelectAll(GSSystemDataTables.Users, new Dictionary<string,object>(){
                {"clLogin",login },
                {"clPwd", PWD.MD5 () },
            });

            if ((r != null)&&(r.RowCount == 1))
            {
                if (r.Rows[0].GetValue<int>("clBlock") != 1)
                {
                    User = r.Rows[0];
                    iConnect(User);
                    return true;
                }
            }
            User = null;
            GSLog.EWriteLine((r != null) ? r.ErrorMessage : "");
            return false;
        }
        public static bool Connect(IGSDataRow user)
        {
            if (iConnect(User))
            {
                User = user;
                OnCurrentUserChanged(EventArgs.Empty);
                return true;
            }
            return false;
        }
        internal  static bool iConnect(IGSDataRow User)
        {
            ITbGSUsers v_users = User.CreateInterfaceInstance<ITbGSUsers>();// GSDB.CreateInterfaceInstance<ITbGSUsers>(r.Rows[0]);
            if (v_users == null)
                return false;

            GSSystemParams.LastLogin = v_users.clLastLogin;
            v_users.clLastLogin = DateTime.Now;
            v_users.Update();
            GSSystemParams.WelcomeMessage = string.Format(@"
/*-----------------------------------------------------*/
Welcome {0} to GS console plateform 
Last login : {1}
/*------------------------------------------------------*/
", v_users.clLogin,
       GSSystemParams.LastLogin.ToString("HH:mm:ss dd/MM/yy"));
            return true;
        }
        /// <summary>
        /// get the data adapter
        /// </summary>
        public GSDataAdapter DataAdapter
        {
            get { return m_DataAdapter; }
            internal set { m_DataAdapter = value; }
        }

        /// <summary>
        /// get or set the current operation mode
        /// </summary>
        public string OperationMode
        {
            get { return m_OperationMode; }
            set
            {
                if (m_OperationMode != value)
                {
                    m_OperationMode = value;
                    OnOperationModeChanged(EventArgs.Empty);
                }
            }
        }
        public event EventHandler OperationModeChanged;
        ///<summary>
        ///raise the OperationModeChanged 
        ///</summary>
        protected virtual void OnOperationModeChanged(EventArgs e)
        {
            if (OperationModeChanged != null)
                OperationModeChanged(this, e);
        }

        /// <summary>
        /// get or set the MainForm
        /// </summary>
        public IGSMainForm MainForm
        {
            get { return m_MainForm; }
            set
            {
                if (m_MainForm != value)
                {
                    m_MainForm = value;
                    OnMainFormChanged(EventArgs.Empty);
                }
            }
        }
        public event EventHandler MainFormChanged;

        ///<summary>
        ///raise the EventName 
        ///</summary>
        protected virtual void OnMainFormChanged(EventArgs e)
        {
            if (MainFormChanged != null)
                MainFormChanged(this, e);
        }

     
        private static GSSystem sm_instance;
        
        private GSSystem()
        {
            this.m_regsitreadedTables = new Dictionary<string, Type>();
            new GSTaskActionLoader(this);
            new GSActionLoader(this);
        }

        public static GSSystem Instance
        {
            get
            {
                return sm_instance;
            }
        }
        static GSSystem()
        {
            sm_instance = new GSSystem();
            GSSystemParams.Prompt  = ">";
            GSSystemParams.WelcomeMessage = string.Format ("Welcome to GS console plateform \n{0}",
                DateTime.Now.ToString ("dd/MM/yy hh-mm-ss"));
        }
       
        internal static void Init()
        {//init gs system           
            //init module directory
            CoreSystem.Instance.LoadAssemblyDir(Path.Combine(
               CoreApplicationManager.Application .StartupPath, GSConstant.MODULE_DIR));
            Instance.Load();
            Instance.Initialize();
            IGSLib.Init(new GSDB.GSDBContextInfo());
            __initCreateDataAdapter();
            CoreApplicationManager.ApplicationExit += _ApplicationExit;
        }

        private static void _ApplicationExit(object sender, EventArgs e)
        {
            if (sender != null) 
            {
                Stop();
            }
        }

        internal static void Stop() {
            if (GSSystem.Instance.DataAdapter != null)
            {
                GSSystem.Instance.DataAdapter.Close();
            }
        }
        /// <summary>
        /// initialize GSApplication data type
        /// </summary>
        private void Initialize()
        {
            GSApplicationInitializerAttribute r = CoreSystemEnvironment.GetEntryAssembly().GetCustomAttribute(typeof(GSApplicationInitializerAttribute))
                as GSApplicationInitializerAttribute;
            if ((r != null)&& (r.Type !=null))
            {
               IGSInitializer i =  r.Type.Assembly.CreateInstance(r.Type.FullName) as 
                   IGSInitializer;
               i.Initialize();
            }
        }
        /// <summary>
        /// create a data adapter.
        /// </summary>
        static void __initCreateDataAdapter()
        {
            GSLog.CWriteLine("[GS] - __initCreateDataAdapter");
            GSDataAdapter adpater = CreateAdapter();
            if (adpater == null)
            {
                GSLog.EWriteLine("[GS] - DataAdapter failed to be created .... ");
                GSSystem.Instance.DataAdapter = null;
            }
            else
            {
                GSSystem.Instance.DataAdapter = adpater;
            }
        }
        /// <summary>
        /// Create DataAdapter and init data system
        /// </summary>
        /// <returns></returns>
        internal static GSDataAdapter CreateAdapter()
        {
            var app = GSSettings.Instance;
            string connex =    
            string.Format(app.ConnectionString,
            app.Server,
            app.Login,
            app.Password,
            app.DataBase
            );
            GSLog.CWriteLine("[GS] - connexion :" + connex);
            return GSDataContext.Init(app.AdapterName,   connex );
        }
        private void Load()
        {
            if (m_loadingTypeHandler == null)
                return;

            if (sm_modules == null)
            {
                sm_modules = new Dictionary<string, IGSModule>();
            }            
            
            AppDomain.CurrentDomain.TypeResolve += CurrentDomain_TypeResolve;
            AppDomain.CurrentDomain.AssemblyResolve += CurrentDomain_AssemblyResolve;
            AppDomain.CurrentDomain.AssemblyLoad +=_AssemblyLoad;
            GSLog.CWriteLine("Loading assemblies....");
            foreach (Assembly b in AppDomain.CurrentDomain.GetAssemblies())
            {
                GSLog.CWriteLine("Init assembly " + b.FullName);
                try
                {
                    LoadAssembly(b);
                    __LoadModule(b);
                }
                catch (Exception ex){
                    System.Diagnostics.Debug.WriteLine(ex.Message);
                    GSLog.EWriteLine("Init assembly " + b.FullName);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="b"></param>
        private void LoadAssembly(Assembly b)
        {
            foreach (Type item in b.GetTypes())
            {
                m_loadingTypeHandler(item);
            }
        }

        private void _AssemblyLoad(object sender, AssemblyLoadEventArgs args)
        {
            //update loading type when assembly loaded
            if (!args.LoadedAssembly.IsDynamic)
            {
                LoadAssembly(args.LoadedAssembly);
            }
        }

        Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
        {
            return null;
        }

        Assembly CurrentDomain_TypeResolve(object sender, ResolveEventArgs args)
        {
            return null;
        }
        /// <summary>
        /// get task actions
        /// </summary>
        /// <returns></returns>
        public GSActionBase[] GetTaskActions()
        {
            GSActionBase[] p =  m_taskAction.ToArray();
            return p;
        }

        public delegate void LoadingTypeHandler(Type t);

        abstract class GSLoader
        {
            protected  GSSystem gSSystem;

            public GSLoader(GSSystem gSSystem)
            {
                
                this.gSSystem = gSSystem;
                gSSystem.m_loadingTypeHandler +=   LoadType;
            }
            public virtual void LoadType(Type t)
            { 

            }
        }

        class GSActionLoader : GSLoader
        {
            public GSActionLoader(GSSystem gSSystem)
                : base(gSSystem)
            {

            }
            public override void LoadType(Type t)
            {
                if (t.IsSubclassOf(typeof(GSActionBase)))
                {
                    GSActionAttribute attr = GSTaskAttribute.GetCustomAttribute(t, typeof(GSActionAttribute)) as GSActionAttribute;
                    if (attr != null)
                    {
                        try
                        {
                            if ((this.gSSystem.m_globalActions ==null) ||(this.gSSystem.m_globalActions.ContainsKey(attr.Name) == false))
                            {
                                GSActionBase ack = t.Assembly.CreateInstance(t.FullName) as GSActionBase;
                                if (ack != null)
                                {
                                    ack.InitAttribute(attr);
                                    this.gSSystem.RegisterAction(attr.Name, ack);
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                           CoreMessageBox.Show(ex.Message, "GSSystem - LoadType");
                        }
                    }
                }
            }
        }
        sealed class GSTaskActionLoader : GSLoader
        {
           public GSTaskActionLoader(GSSystem gSSystem):base(gSSystem)
           {
               gSSystem.m_taskAction = new List<GSActionBase>();
 
           }
           public override void LoadType(Type t)
           {
               //register new 
               if (t.IsSubclassOf(typeof(GSTaskActionBase)))
               {
                   GSTaskAttribute attr = GSTaskAttribute.GetCustomAttribute(t, typeof(GSTaskAttribute)) as GSTaskAttribute;
                   if (attr != null)
                   {
                       try
                       {
                           GSActionBase ack = t.Assembly.CreateInstance(t.FullName) as GSActionBase;
                           if (ack != null)
                           {
                               
                               this.gSSystem.m_taskAction.Add(ack);
                               this.gSSystem.RegisterAction(attr.Name, ack);
                           }
                       }
                       catch (Exception ex)
                       {
                           CoreLog.WriteLine("Exception : " + ex.Message);
#if DEBUG
                           CoreMessageBox.Show(ex.Message, "Erreur - GSSystem.GSTaskActionLoader.LoadType");
#endif
                       }
                   }
               }
           }
        }

    

        /// <summary>
        /// get a action registratrated
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public  GSActionBase GetAction(string p)
        {
            if (m_globalActions.ContainsKey(p))
                return m_globalActions[p];
                    return null;
        }

        internal void RegisterAction(string name, GSActionBase ack)
        {
            if (m_globalActions == null)
                m_globalActions = new Dictionary<string, GSActionBase>();
            if (!m_globalActions.ContainsKey(name))
            {
                m_globalActions.Add(name, ack);
            }
            else {
                CoreMessageBox.Show("Une action portant le nom "+name +" existe déjà dans l'application");
                GSLog.WriteLog("Une action portant le nom " + name + " existe déjà dans l'application");
            }
        }

        /// <summary>
        /// call a gs system actions
        /// </summary>
        /// <param name="p"></param>
        public  void CallAction(string p)
        {
            if (m_globalActions.ContainsKey(p))
            { 
                m_globalActions[p].DoAction ();
            }
        }
        /// <summary>
        /// call a gs system actions with parametter
        /// </summary>
        /// <param name="p"></param>
        public void CallAction(string p, object parameter)
        {
            if (m_globalActions.ContainsKey(p))
            {
                m_globalActions[p].Param = parameter;
                m_globalActions[p].DoAction();
                m_globalActions[p].Param = null;
            }
        }
        //internal static void PrintCode(IGK.DrSStudio.Drawing2D.ICore2DDrawingDocument document)
        //{
        //    if (document != null)
        //    {
        //        new GSPrintSystem(document).PrintCodeBar();
        //    }
        //}

      
        /// <summary>
        /// get the current logged user
        /// </summary>
        public static IGSDataRow User { get; internal set; }

        /// <summary>
        /// get the current user 
        /// </summary>
        public ITbGSUsers CurrentUser { get { return GetCurrentUser<ITbGSUsers>(); } }

        public static event EventHandler CurrentUserChanged;

        private static void OnCurrentUserChanged(EventArgs e)
        {
            if (CurrentUserChanged != null)
                CurrentUserChanged(sm_instance, e);
        }
        /// <summary>
        /// get the current user
        /// </summary>
        //public IGSDataRow CurrentUser { get { return GSSystem.User; } }

        public static void RegisterTable<T>(string p)
        {
            var s = Instance;
            if (s.m_regsitreadedTables == null)
            {
                s.m_regsitreadedTables = new Dictionary<string, Type>();
            }
            if (s.m_regsitreadedTables.ContainsKey(p))
            { //replace
                s.m_regsitreadedTables[p] = typeof(T);
            }
            else 
                s.m_regsitreadedTables.Add(p, typeof(T));
            
        }
        /// <summary>
        /// get the table name by registrated key
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        internal string GetTableName(string tableName)
        {
            if (this.m_regsitreadedTables.ContainsKey(tableName))
                return GSDataContext.GetTableName(this.m_regsitreadedTables[tableName]);
            return tableName;
        }
        /// <summary>
        /// used to avoid malfunctional system registration
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="source"></param>
        /// <returns></returns>
        internal string GetTableName(string tableName, Type source)
        {
            if (this.m_regsitreadedTables.ContainsKey(tableName))
            {
                Type g = (this.m_regsitreadedTables[tableName]);
                if (g == source)
                    return null;
                return GSDataContext.GetTableName(g);
            }
            return tableName;
        }
        /// <summary>
        /// execute a command action
        /// </summary>
        /// <param name="cmd">command action to execute</param>
        public static void ExecCmd(string cmd)
        {
            if (string.IsNullOrEmpty(cmd))
            {
                GSLog.EWriteLine(string.Format("Notvalidcommand [{0}] ", cmd));
                return;
            }

            string[] args = cmd.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            
            ICoreAction c = CoreSystem.GetAction(args[0]);
            if (c != null)
            {
                if (args.Length > 0)
                {
                    GSActionBase gs = c as GSActionBase;
                    if (gs!=null){
                        gs.Param = string.Join(" ", args, 1, args.Length - 1);
                    }
                }
                c.DoAction();
            }
            else {
                GSLog.EWriteLine(string.Format("command [{0}] not found", cmd));
            }
        }



        public static T GetCurrentUser<T>()
        {
            if (User!=null)
                return User.CreateInterfaceInstance<T>();
            return default(T);
        }
        /// <summary>
        /// get module by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static IGSModule GetModuleByName(string name)
        {
           
            if (sm_modules.ContainsKey(name))
                return sm_modules[name];
            return null;
        }

        private static void __LoadModule()
        {
            foreach (Assembly asm in AppDomain.CurrentDomain.GetAssemblies())
            {
                if (GSModuleAttribute.IsDefined(asm, typeof(GSModuleAttribute)))
                {
                    GSModuleAttribute r = GSModuleAttribute.GetCustomAttribute(asm, typeof(GSModuleAttribute)) as GSModuleAttribute;
                    sm_modules.Add(r.ModuleName, GSModuleBase.CreateFrom(r, asm));
                }
            }
        }
        /// <summary>
        /// load assembly as a gs module 
        /// </summary>
        /// <param name="asm"></param>
        private static void __LoadModule(Assembly asm)
        {
            if (GSModuleAttribute.IsDefined(asm, typeof(GSModuleAttribute)))
            {
                GSModuleAttribute r = GSModuleAttribute.GetCustomAttribute(asm, typeof(GSModuleAttribute)) as GSModuleAttribute;
                sm_modules.Add(r.ModuleName, GSModuleBase.CreateFrom(r, asm));
            }          
        }
        /// <summary>
        /// represent a base implementation of a GS module
        /// </summary>
        public abstract class GSModuleBase : IGSModule 
        {
            private string m_name;
            private FunctionsCollection m_functions;
            public IGSDataRow User { get {
                return GSSystem.User;
            } }
            protected  GSModuleBase() {
                m_functions = new FunctionsCollection(this);
            }
            internal static IGSModule CreateFrom(GSModuleAttribute r, Assembly asm)
            {
                if (r.ModuleType == null)
                    return null;
                var p = r.ModuleType.GetProperty("Instance",
                     BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
                if (p == null)
                    return null;

                IGSModule mod = p.GetValue(null,null)
                     as IGSModule ;

                if ((mod !=null) && mod.Initilalize())
                {
                    if (mod is GSModuleBase)
                    {
                        GSModuleBase v_mod = mod as GSModuleBase;
                        v_mod.m_name = r.ModuleName;                                      
                    }
                    return mod;
                }
                return null;
            }

            public string Name
            {
                get { return this.m_name; }
            }


            public FunctionsCollection Functions
            {
                get { return this.m_functions; }
            }

            public class FunctionsCollection : IGSFunctionCollections
            {
                Dictionary<string, GSActionBase> m_functions;
                private GSModuleBase gSModule;
                internal void Register(Dictionary<string, GSActionBase> functions)
                {
                    if ((functions != null) && (functions.Count > 0))
                    {
                        foreach (var item in functions)
                        {
                            Register(item.Key, item.Value);
                        }
                    }
                }

                public void Register(string key, GSActionBase value)
                {

                    if (!this.m_functions.ContainsKey(key))
                    {
                        this.m_functions.Add(key, value);
                    }
                    else
                    {
                        this.m_functions[key] = value;
                    }
                }
                public FunctionsCollection(GSModuleBase gSModule)
                {
                    this.m_functions = new Dictionary<string, GSActionBase>();
                    this.gSModule = gSModule;
                }
                public int Count
                {
                    get { return this.m_functions.Count; }
                }
                public object Call(string name, object[] args)
                {
                    if (this.m_functions.ContainsKey(name))
                    {
                        GSActionBase ack = this.m_functions[name];

                        ack.Param = args;
                        ack.DoAction();
                        return ack.Response;
                    }
                    return null;

                }

            }


            public abstract bool Initilalize();

            IGSFunctionCollections IGSModule.Functions
            {
                get { return this.Functions; }
            }
        }
        /// <summary>
        /// show presentation 
        /// </summary>
        /// <param name="module"></param>
        /// <param name="presentationName"></param>
        /// <param name="asDialog"></param>
        public void ShowPresentationTask(IGSModule module, string presentationName, bool asDialog=false)
        {
            if (module == null)
                return;
            var bench = CoreSystem.GetWorkbench<IGSWorkbench>();
            
            if (bench != null)
            {
                this.m_currentModule = module;
                bench.ShowPresentation(presentationName, asDialog );
            }
          //  MainForm.View.NavigateTo(new PalettiseGUI());
        }
        public static ICoreIcon AppIcon { get {
            return CoreApplicationManager.Application.ResourcesManager.GetIcon("AppIcon");
        } }
    }
}
