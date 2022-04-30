

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: CoreApplicationManager.cs
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
file:CoreApplicationManager.cs
*/
using IGK.ICore;
using IGK.ICore.Resources;
using IGK.ICore.WinUI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
namespace IGK.ICore
{
    
    [Serializable()]
    /// <summary>:
    /// represent a core application access
    /// </summary>
    /// <remarks>
    /// A valid application must be marked width CoreApplicationAttribute and implement ICoreApplication interface
    /// </remarks>
    public static class CoreApplicationManager  
    {
        static ICoreApplication sm_application;
        /// <summary>
        /// Get the application instance
        /// </summary>
        public static ICoreApplication Application {
            get {
                return sm_application;
            }
        }
        /// <summary>
        /// .static Factory
        /// </summary>
        static CoreApplicationManager()
        {
          ///start CoreApplication
          if (CoreSystemEnvironment.DesignMode )
              return;
            Assembly v_asm = CoreSystemEnvironment.GetEntryAssembly();
            if (v_asm != null)
            {
                Type t = v_asm.FindAttribute<CoreApplicationAttribute>();
                if (t == null)
                {
                    //found in other assembly
                    CoreLog.WriteDebug("A type marked with CoreApplicationAttribute is required to load CoreApplication data");
                    Environment.Exit(-100);
                    //found in other assembly
                    throw new ArgumentException("No CoreApplicationManager child found to initialize the core system : " + v_asm.FullName);
                }
                RegisterApplication(t);
            }
        }
        /// <summary>
        /// used to register manually a type for Application instance
        /// </summary>
        /// <param name="appType">ICoreApplication Type implement</param>
        public static void RegisterApplication(Type appType)
        {
            ICoreApplication v_application = appType.Assembly.CreateInstance(appType.FullName) as ICoreApplication;
            System.Diagnostics.Debug.Assert(v_application != null, "ICore can't create instance from "+
                appType.FullName+
                " must implement IGK.ICore.ICoreApplication ");
            v_application.Initialize();
            sm_application = v_application;
        }
        /// <summary>
        /// bind application as icore application.
        /// </summary>
        /// <param name="application"></param>
        internal static void BindApplication(ICoreApplication application,CoreBindApplicationListener Listener)
        {
            if (application == null) {
                return;
            }
            if (sm_application == null)
            {
                CoreApplicationAttribute c = application.GetType().GetCustomAttribute(typeof(CoreApplicationAttribute)) as CoreApplicationAttribute;
                if (c != null)
                {
                    ///register this core application
                    sm_application = application;
                    Listener?.Invoke(c);
                }
            }
        }
        public static  void Close() {
            sm_application.Close();
        }       
        public static event EventHandler ApplicationExit {
            add {
                if (sm_application != null)
                {
                    sm_application.ApplicationExit += value;
                }
            }
            remove {
                if (sm_application != null)
                {
                    sm_application.ApplicationExit -= value;
                }
            }
        }
        public static string CurrentWorkingPath
        {
            get {
                return sm_application.CurrentWorkingPath;
            }
        }
        public static enuKeys ModifierKeys { get { return sm_application.ControlManager.ModifierKeys; } }
        public static IXForm ActiveForm { get { return sm_application.ControlManager.ActiveForm;  } }
        public static string StartupPath { get { return sm_application.StartupPath; } }
        public static string UserAppDataPath { get 
        { 
           
            return sm_application.UserAppDataPath;
        } 
        }
        internal static ICoreResourceItem OpenFile(string filename)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// check if this is a transparent proxy
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static bool IsTransparantProxy(object obj)
        {
            if (sm_application != null)
            {
                return sm_application.IsTransparentProxy(obj);
            }
            return false;
        }

        public static string PrivateFontsDirectory { get {
            if (sm_application !=null)
            return sm_application.PrivateFontsDirectory;
            return null;
        } }

        public static string SourceDir { get
            {
#if !NOSOURCEDIR
                return CoreConstant.DRS_SRC;
#else 
            return null;
#endif

        } }
    }
}

