

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: CoreSystem.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
/*
IGKDEV @ 2008-2016
author: C.A.D . BONDJE DOUE
file:CoreSystem.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Threading;
using System.IO;
using System.Collections;
using System.Globalization;
using System.ComponentModel;



namespace IGK.ICore
{
    using IGK.ICore;
    using IGK.ICore.Codec;
    using IGK.ICore.Settings;
    using IGK.ICore.WinUI;
    using IGK.ICore.Drawing2D;
    using IGK.ICore.Threading;
    using IGK.ICore.Menu;
    using IGK.ICore.Actions;
    using IGK.ICore.Tools;
    using IGK.ICore.ContextMenu;
    using IGK.ICore.IO;
    using IGK.ICore.Dependency;
    
    [Serializable()]
    [ComVisible(true)]
    public class CoreSystem : MarshalByRefObject, IDisposable, ICoreSystem
    {
        internal ICoreSystemWorkbench m_Workbench;
        internal CoreTypeLoader m_typeLoader;
        internal CoreActionContextCollections m_actionContext;
        internal CoreAssemblyLoadedHandler m_asmLoaderHandler;
        internal CoreMethodHandler m_loadingCompleted;
        internal CoreWorkingActionCollections m_actions; //global actions collections
        internal CoreWorkingMenuCollections m_menus; //global menu collection
        internal CoreWorkingContextMenuCollections m_contextMenu; //global context menu collection
       // internal CoreTemplateRegisterCollections m_templates; // global template collection
        internal CoreToolsCollections m_tools; //global tools collection
        internal ICoreCodecCollections m_codecs; // global codec collection
        internal CoreSettingCollections m_settings;
        internal bool m_isResourcesLoaded;
        internal CoreAddinCollections m_addins;
        internal CoreWorkingObjectCollections m_wregobjects;
        internal CoreResourcesManager m_resources;
        internal Dictionary<string, Assembly> m_loadedAssembly;
        internal List<string> m_clients; //client register to this server
        private CoreAssemblyLoader m_asmLoader;
        private static CoreSystem sm_instance;


        /*
         * 
         * if you call initation with the Init() not parameter then is the CoreApplication.CreateNewWorkbench that will
         * be called. otherwise the mainform must deliver a workben
         * 
         * */
        /// <summary>
        /// Get the workbench attached to this system instance.
        /// </summary>
        /// <remarks>
        /// Came either from the the MainForm or CoreApplication
        /// </remarks>
        public ICoreSystemWorkbench Workbench {
            get {
                return this.m_Workbench;
            }
            private set {
                if (this.m_Workbench != value)
                {
                    this.m_Workbench = value;
                    OnWorkbenchChanged(EventArgs.Empty);
                }
            }
        }
        public event EventHandler WorkbenchChanged;

        /// <summary>
        /// get the assebly loader object
        /// </summary>
        public CoreAssemblyLoader AssemblyLoader {
            get {
                return this.m_asmLoader;
            }
        }
        
        ///<summary>
        ///raise the WorkbenchChanged 
        ///</summary>
        protected virtual void OnWorkbenchChanged(EventArgs e)
        {
            WorkbenchChanged?.Invoke(this, e);
        }

        public ICoreMainForm MainForm {
            get { 
                var h = GetWorkbench<ICoreApplicationWorkbench>();
                if (h!= null)
                    return h.MainForm;
                return null;
            }
        }
        
        /// <summary>
        /// pctr
        /// </summary>
        private CoreSystem()
        {
        }

        /// <summary>
        /// actions registrated
        /// </summary>
        public event EventHandler<CoreItemEventArgs<ICoreAction>> ActionRegistered {
            add {
                this.m_actions.ActionRegistered += value;
            }
            remove {
                this.m_actions.ActionRegistered -= value;
            }
        }

        //init instance
        private void _initInstance()
        {
            this.m_isResourcesLoaded = false;
            this.m_clients = new List<string>();
            this.m_resources = new CoreResourcesManager(this);
            this.m_addins = new CoreAddinCollections();
            this.m_wregobjects = new CoreWorkingObjectCollections(this);
            this.m_menus = new CoreWorkingMenuCollections(this);
            this.m_contextMenu = new CoreWorkingContextMenuCollections(this);
            this.m_tools = new CoreToolsCollections(this);
            this.m_codecs = new CoreCodecCollections(this);

            this.m_actions = new CoreWorkingActionCollections(this);
            this.m_actionContext = new CoreActionContextCollections(this);
            //this.m_templates = new CoreTemplateRegisterCollections(this);
            this.m_settings = new CoreSettingCollections(this);
            this.m_loadedAssembly = new Dictionary<string, Assembly>();
            SettingManager.Configure(this);
            //setup thread intfo
            Thread.CurrentThread.CurrentCulture = CoreThreadManager.CultureInfo;
            Thread.CurrentThread.CurrentUICulture = CoreThreadManager.CultureInfo;
        }
        /// <summary>
        /// get the core system instance
        /// </summary>
        public static CoreSystem Instance
        {
            get
            {
                return sm_instance;
            }
        }
        public CoreSettingCollections Settings { get { return this.m_settings; } }
        ///// <summary>
        ///// initialize form all default assembly
        ///// </summary>
        //private void InitAssemblies()
        //{
        //    //init assmblies from startup folder
        //    //for android startup folder mean nothing. you have to check some loaded assmblies firs
        //    new CoreAssemblyLoader(this, CoreApplicationManager.StartupPath).Load();
        //}
        private void OnLoadingAssemblyComplete()
        {
            this.m_loadingCompleted?.Invoke();
        }
        internal bool IsLoadedAssembly(string p)
        {
            return this.m_loadedAssembly.ContainsKey(p);
        }
        public void RegisterTypeLoader(CoreTypeLoader loaderFunc)
        {
            if (loaderFunc != null)
                this.m_typeLoader += loaderFunc;
        }
        public void UnregisterTypeLoader(CoreTypeLoader loaderFunc)
        {
            if (loaderFunc != null)
                this.m_typeLoader -= loaderFunc;
        }
        public void RegisterLoadingComplete(CoreMethodHandler LoadAsmResourcesFunc)
        {
            if (LoadAsmResourcesFunc != null)
                this.m_loadingCompleted += LoadAsmResourcesFunc;
        }
        public void UnRegisterLoadingComplete(CoreMethodHandler LoadAsmResourcesFunc)
        {
            if (LoadAsmResourcesFunc != null)
                this.m_loadingCompleted -= LoadAsmResourcesFunc;
        }
        public void RegisterAssemblyLoader(CoreAssemblyLoadedHandler assemblyFunc)
        {
            if (assemblyFunc != null)
                this.m_asmLoaderHandler += assemblyFunc; 
        }
        public void UnRegisterAssemblyLoader(CoreAssemblyLoadedHandler assemblyFunc)
        {
            if (assemblyFunc != null)
                this.m_asmLoaderHandler -= assemblyFunc;
        }
        public Type GetWorkingTypeByName(string key)
        {
            return this.m_wregobjects.GetWorkingType(key);
        }
        public static ICoreMainForm GetMainForm()
        {
            if (GetWorkbench() is ICoreApplicationWorkbench v_bench)
                return v_bench.MainForm;
            return null;
        }
        public static ICoreWorkbench GetWorkbench()
        {
            if ((sm_instance == null)||(IsCoreTransparantProxy ()))
                return null;
            
            return sm_instance.Workbench as ICoreWorkbench;
        }
        public CoreResourcesManager Resources
        {
            get
            {
                return this.m_resources;
            }
        }
        public void Dispose()
        {
        }
        #region static functions
        #endregion
        public static CoreToolBase GetTool(string name)
        {
            return sm_instance.m_tools[name] as CoreToolBase;
        }
        public static IEnumerable GetTools()
        {
            return sm_instance.m_tools;
        }
        public static CoreResourcesManager GetResources()
        {
            return sm_instance.__GetIResources();
        }
        //return resources to avoid remoting exception 
        public CoreResourcesManager __GetIResources()
        {
            return this.m_resources;
        }
        public static ICoreCodec[] GetDecoders(string category)
        {
            return sm_instance.m_codecs.GetDecoders(category);
        }
        public static ICoreCodec[] GetDecoders()
        {
            return sm_instance.m_codecs.GetDecoders();
        }
        public static ICoreCodec[] GetDecodersByExtension(string ext)
        {
            return sm_instance.m_codecs.GetDecodersByExtension(ext);
        }
/// <summary>
/// retrieve a working object collections
/// </summary>
/// <returns></returns>
        public static ICoreWorkingObjectCollections GetWorkingObjects()
        {
            return CoreSystem.Instance.m_wregobjects;
        }
        /// <summary>
        /// get the string keys
        /// </summary>
        /// <param name="key"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public static string GetString(string key, params object[] param)
        {
            if (key == null)
                return null;
            var r = GetResources();
            if (r==null)
                return key;
            return r.GetString(key, param);
        }
        public static ICoreCodec[] GetEncoders(string category)
        {
            return sm_instance.m_codecs.GetEncoders(category);
        }
        public static Type GetWorkingObjectType(string typeName)
        {
            return sm_instance.GetWorkingTypeByName(typeName);
        }
        internal void LoadAssembly(string p, AssemblyName assemblyName)
        {
            Assembly.Load(assemblyName);
        }
        internal static ICoreCodec[] GetEncoders()
        {
            return sm_instance.m_codecs.GetEncoders();
        }
        internal static ICoreCodec[] GetEncoder(string category)
        {
            return sm_instance.m_codecs.GetEncoders(category);
        }
        public static ICoreCodec[] GetEncodersByExtension(string ext)
        {
            return sm_instance.m_codecs.GetEncodersByExtension(ext);
        }
        public static ICoreContextMenuCollections GetContextMenus()
        {
            return sm_instance.m_contextMenu;
        }
        public static ICoreMenuCollections GetMenus()
        {
            return sm_instance.m_menus;
        }
        public static ICoreAction GetMenu(string menukey)
        {
            return sm_instance.m_menus[menukey];
        }
        public static ICoreAddInCollections GetAddIns()
        {
            return sm_instance.m_addins;
        }
        /// <summary>
        /// create a working object
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static ICoreWorkingObject CreateWorkingObject(string name)
        {
            if (IsCoreTransparantProxy ())
            {
                try
                {
                    Type vt = GetWorkingObjectType(name);
                    if (vt != null)
                        return CoreWorkingObjectCollections.CreateObject(vt) ?? GetDependencyObject (name);
                }
                catch
                {
                }
                return null;
            }
            if (Instance.m_wregobjects != null)
            {
                return Instance.m_wregobjects.CreateObject(name) ?? GetDependencyObject(name);
               
            }
            return null;
        }

        private static ICoreWorkingObject GetDependencyObject(string name)
        {          
            CoreDependencyInfo p = CoreDependencyObject.GetObjectByKey(name);
            if (p != null)
            {
                return new CoreDependencyLoader(p);
            }
            return null;
        }
        internal static void SaveSetting()
        {
            sm_instance.m_settings.SaveSetting();
        }
        internal static bool RegisterContextMenu(ContextMenu.CoreContextMenuAttribute v_attr, ContextMenu.CoreContextMenuBase childMenu)
        {
            return sm_instance.m_actions.Register(v_attr, childMenu);
        }
        /// <summary>
        /// get the current surface
        /// </summary>
        /// <returns></returns>
        public static ICoreWorkingSurface GetCurrentSurface()
        {
            ICoreWorkbench v_bench = GetWorkbench();
            if (v_bench == null) return null;
            return v_bench.CurrentSurface;
        }
        public static ICoreMenuAction[] GetRootMenus()
        {
            return sm_instance.m_menus.GetRootMenus();
        }
        public static ICoreContextMenuAction[] GetContextRootMenus()
        {
            return sm_instance.m_contextMenu.GetRootMenus();
        }
        /// <summary>
        /// get the settings
        /// </summary>
        /// <returns></returns>
        public static CoreSettingCollections GetSettings()
        {
            if (sm_instance !=null)
                return sm_instance.Settings;
            return null;
        }
        static CoreSystem __initServerInstance()
        {
            sm_instance =  new CoreSystem();
            return sm_instance;
        }

        public static bool InitWithEntryAssembly(Assembly asm)
        {
            if (sm_instance != null)
                return false;
            CoreSystemEnvironment.SetEntryAssembly(asm);
            //set the default thread culture for all thread
            CultureInfo.DefaultThreadCurrentCulture = CoreThreadManager.CultureInfo;
            CultureInfo.DefaultThreadCurrentUICulture = CoreThreadManager.CultureInfo;
            if (CoreApplicationManager.Application.RegisterServerSystem(__initServerInstance))
            {
                sm_instance._initInstance();

                CoreApplicationManager.Application.Register(sm_instance);                
                sm_instance.m_asmLoader = new CoreAssemblyLoader(Instance, CoreApplicationManager.StartupPath);
                sm_instance.m_asmLoader.Load();
                sm_instance.OnLoadingAssemblyComplete();
                return true;
            }
            else if (CoreApplicationManager.Application.RegisterClientSystem(__initServerInstance))
            {                
                if (asm.IsDynamic == false)
                {
                    //load AdditionalAssembly
                    string dir = Path.GetDirectoryName(asm.Location);
                    sm_instance.LoadAssemblyDir(dir);
                }
                return true;
            }
            return true;

        }
        public static bool InitWithEntryAssembly(Assembly assembly, bool createWorkbenchFromApplication)
        {
            bool r = InitWithEntryAssembly(assembly);
            if (r && createWorkbenchFromApplication)
            {
                if (IsCoreTransparantProxy() == false)
                {
                    var app = CoreApplicationManager.Application;
                    if (app != null)
                    {
                        sm_instance.Workbench = app.CreateNewWorkbench();
                        if (sm_instance.Workbench !=null)
                        sm_instance.Workbench.Init(sm_instance);
                    }
                }
            }
            return r;

        }
        /// <summary>
        /// Entry point of the core system
        /// </summary>
        /// <returns></returns>
        public static bool Init()
        {
            if (CoreSystemEnvironment.DesignMode)
            {
                sm_instance = new CoreSystem();
                sm_instance._initInstance();
                return false;
            }
            System.Diagnostics.StackTrace stackTrace = new System.Diagnostics.StackTrace();
            Assembly v_entryAsm = stackTrace.GetFrame(1).GetMethod().DeclaringType.Assembly;
            
            return InitWithEntryAssembly(v_entryAsm, true);
        }
        /// <summary>
        ///  Entry point of the core system
        /// </summary>
        /// <param name="startType">form type used as startup dialog</param>
        /// <param name="mainFormType">form type mainform </param>
        /// <returns></returns>
        public static bool Init(Type startType, Type mainFormType)
        {
            ICoreStartForm v_stform;
            ICoreMainForm v_Mform;
            if ((startType != null) && startType.IsClass && (startType.GetInterface(typeof(ICoreStartForm).FullName) != null))
            {
                CoreThreadManager.InitCurrentThread();
                InitWithEntryAssembly(mainFormType.Assembly, false);
                if (IsCoreTransparantProxy() == false)
                {

                    Thread v_sth = CoreThreadManager.CreateThread(() =>
                    {
                        using (v_stform = startType.Assembly.CreateInstance(startType.FullName) as ICoreStartForm)
                        {
                            v_stform.Load += (o, e) =>
                            {
                                Thread v_mth = CoreThreadManager.CreateThread(() =>
                                {
                                    if (IsCoreTransparantProxy() == false)
                                    {
                                        //create the mainform an get the current workbench

                                        using (v_Mform =
                                        mainFormType.Assembly.CreateInstance(mainFormType.FullName) as ICoreMainForm)
                                        {
                                            //get the workbench
                                            sm_instance.Workbench = v_Mform.Workbench;
                                            v_Mform.Shown += (op, ep) =>
                                            {
                                                v_stform.Invoke((CoreMethodHandler)v_stform.Close);
                                                v_Mform.Activate();
                                            };
                                            v_Mform.Workbench.Init(sm_instance);
                                            if (v_Mform is ICoreRunnableMainForm)
                                            {
                                                (v_Mform as ICoreRunnableMainForm).Run();
                                            }
                                            else
                                                v_Mform.ShowDialog();
                                        }
                                    }
                                }, "MainFormThread");
                                v_mth.SetApartmentState(ApartmentState.STA);
                                v_mth.IsBackground = false;
                                v_mth.Start();
                            };
                            v_stform.Run(mainFormType);
                        }
                    }, CoreConstant.STARTFORM_THREAD_NAME);

                    v_sth.SetApartmentState(ApartmentState.STA);
                    v_sth.IsBackground = false;
                    v_sth.Start();
                    return true;
                }
            }
            return false;
        }

     
        
        public override object InitializeLifetimeService()
        {//stop delete the object
            return null;// base.InitializeLifetimeService();
        }
        public void InitTread()
        {
            CoreThreadManager.InitCurrentThread();
        }
        //load Assembly directory
        public void LoadAssemblyDir(string dir)
        {
            if (this.m_asmLoader != null)
                this.m_asmLoader.LoadDir(dir);
        }
        public static ICoreAction GetAction(string actionName)
        {
            return sm_instance.m_actions[actionName];
        }
        public static ICoreAction[] GetActions()
        {
            return sm_instance.m_actions.GetActions();
        }
       
        public static void CloseApplication()
        {
            try
            {
                GetMainForm().Close();
            }
            catch
            {
            }
        }
        /// <summary>
        /// get if the system instance is a transparent proxy
        /// </summary>
        /// <returns></returns>
        public static bool IsCoreTransparantProxy()
        {
            return CoreApplicationManager.IsTransparantProxy(sm_instance);
        }
        /// <summary>
        /// register action
        /// </summary>
        /// <param name="attr"></param>
        /// <param name="menu"></param>
        /// <returns></returns>
        internal static bool  RegisterAction(CoreMenuAttribute  attr, ICoreMenuAction menu)
        {
            return sm_instance.m_actions.Register (attr, menu);
        }
        /// <summary>
        /// register In Global Action Collections
        /// </summary>
        /// <param name="v_attr"></param>
        /// <param name="v_menu"></param>
        /// <returns></returns>
        internal static bool RegisterAction(CoreContextMenuAttribute v_attr, ICoreContextMenuAction v_menu)
        {
            return sm_instance.m_actions.Register(v_attr, v_menu);
        }
        
        
        public static ICoreMenuAction GetMenuAction(string name) {
            return sm_instance.m_actions.GetMenuAction(name);
        }
        public static ICoreContextMenuAction GetContextMenuAction(string name)
        {
            return sm_instance.m_actions.GetContextMenuAction(name);
        }
        public static void ClearExtraDecoderList()
        {
            sm_instance.m_codecs.ClearExtraDecoder();
        }
        /// <summary>
        /// register exta decoder
        /// </summary>
        /// <param name="ext"></param>
        /// <param name="path"></param>
        public static void RegisterExtraDecoder(string ext, string path)
        {
            if (File.Exists (path ))
                sm_instance.m_codecs.RegisterDecoder(ext, path);
        }
        //public static ICoreTemplateCollections GetTemplates()
        //{
        //    return sm_instance.m_templates;
        //}

        /// <summary>
        /// raised when an action is performed
        /// </summary>
        public event EventHandler ActionPerformed;
        

        internal void PerformActionsEvent(object sender, EventArgs e)
        {
            this.ActionPerformed?.Invoke(sender, e);
        }
        /// <summary>
        /// create a working object by resolving the namespace hierarchi
        /// </summary>
        /// <param name="name">name to create </param>
        /// <param name="xreader">namespace hosted </param>
        /// <returns></returns>
        internal static ICoreWorkingObject CreateWorkingObject(string name, IXMLDeserializer xreader)
        {
            string[] t = name.Trim().Split(new String[]{":"}, StringSplitOptions.RemoveEmptyEntries);
            if (t.Length == 1)
            {
                return CreateWorkingObject(t[0]);
            }
            else {
                string prefix = xreader.LookupNamespace(t[0]);
                return CreateWorkingObject(prefix + "/" + t[1]);
            }
        }
        /// <summary>
        /// return the action name context
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        internal static object GetActionContext(string name)
        {
            if (sm_instance != null)
                return sm_instance.m_actionContext[name];
            return null;
        }
        /// <summary>
        /// register or replace the action context
        /// </summary>
        /// <param name="name"></param>
        /// <param name="?"></param>
        /// <returns></returns>
        public static bool RegisterActionContext(string name, ICoreActionContext service)
        {
            if (sm_instance != null)
            {
                return sm_instance.m_actionContext.Register(name, service);
            }
            return false ;
        }

        public static T GetWorkbench<T>()
        {
            var obj = GetWorkbench();
            if (obj is T)
                return (T)obj;
            return default(T);
        }
    }
}

