

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: CoreApplicationBase.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
﻿using IGK.ICore;
using IGK.ICore.ComponentModel;
using IGK.ICore.Drawing2D;
using IGK.ICore.WinUI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.ICore
{
    /// <summary>
    /// Represent the base ICoreApplication base class implements
    /// </summary>
    public abstract class CoreApplicationBase : ICoreApplication , ICoreApplicationServices
    {
        private  CoreSystem m_coreSystem;
        private string m_appName;
        private string m_author;
        private string m_title;
        private string m_copyright;

        public CoreApplicationBase()
        {
        }
        protected void BindApplication()
        {
            CoreApplicationManager.BindApplication(this, (attr)=> {
                //init from attribute
                this.m_author = attr.Author;
                this.m_copyright = attr.CopyRight;
                this.m_title = attr.Title;
                this.m_appName = attr.Name;
            });
        }

        
        /// <summary>
        /// initialize the core application. this method is used by CoreApplicationManager
        /// </summary>
        public virtual void Initialize()
        {
            this.BindApplication();
            CoreTypeDescriptor.AddAttributes(typeof(byte[]), new TypeConverterAttribute(typeof(ByteArrayConverter)));
            //register descriptor
            CoreTypeDescriptor.AddAttributes(typeof(Size2f), new TypeConverterAttribute(typeof(Size2fConverter)));
            CoreTypeDescriptor.AddAttributes(typeof(Size2i), new TypeConverterAttribute(typeof(Size2fConverter)));
            CoreTypeDescriptor.AddAttributes(typeof(Size2d), new TypeConverterAttribute(typeof(Size2fConverter)));
            CoreTypeDescriptor.AddAttributes(typeof(float[]), new TypeConverterAttribute(typeof(CoreFloatArrayTypeConverter)));
            CoreTypeDescriptor.AddAttributes(typeof(bool), new TypeConverterAttribute(typeof(CoreBooleanTypeConverter)));
            //wincore component model
            CoreTypeDescriptor.AddAttributes(typeof(Vector2f), new TypeConverterAttribute(typeof(CoreVector2fTypeConverter)));
            CoreTypeDescriptor.AddAttributes(typeof(Vector2f[]), new TypeConverterAttribute(typeof(CoreVector2fArrayTypeConverter)));
            //CoreTypeDescriptor.AddAttributes(typeof(Colorf), new EditorAttribute(typeof(ColorfTypeEditor), typeof(System.Drawing.Design.UITypeEditor)));
            //CoreTypeDescriptor.AddAttributes(typeof(Colorf), new TypeConverterAttribute(typeof(WinCoreColorfTypeConverter)));
            //CoreTypeDescriptor.AddAttributes(typeof(Rectanglef), new TypeConverterAttribute(typeof(WinCoreRectanglefTypeConverter)));
            //CoreTypeDescriptor.AddAttributes(typeof(Rectanglei), new TypeConverterAttribute(typeof(WinCoreRectanglefTypeConverter)));
            //CoreTypeDescriptor.AddAttributes(typeof(CoreFont), new TypeConverterAttribute(typeof(WinCoreFontTypeConverter)));
            //CoreTypeDescriptor.AddAttributes(typeof(Matrix), new TypeConverterAttribute(typeof(WinCoreMatrixTypeConverter)));
            //CoreTypeDescriptor.AddAttributes(typeof(int), new TypeConverterAttribute(typeof(WinCoreInt32TypeConverter)));
            //CoreTypeDescriptor.AddAttributes(typeof(float), new TypeConverterAttribute(typeof(WinCoreSingleTypeConverter)));     
        }

        public abstract void Close();

        public abstract bool RegisterServerSystem(Func<CoreSystem> __initInstance);

        public abstract bool RegisterClientSystem(Func<CoreSystem> __initInstance);

        public virtual ICoreSystem GetSystem() {
            return m_coreSystem;
        }

        public abstract event EventHandler ApplicationExit;

        public abstract ICoreD2DPathUtils GraphicsPathUtils
        {
            get ;
        }

        public abstract ICoreControlManager ControlManager
        {
            get;
        }

        public abstract ICoreResourceManager ResourcesManager
        {
            get;
        }

        public abstract ICoreBrushRegister BrushRegister
        {
            get ;
        }

        public abstract string StartupPath
        {
            get ;
        }

        public string AddInFolderPath => System.IO.Path.Combine (StartupPath, "AddIn");

        public abstract  string CurrentWorkingPath
        {
            get;
        }

        public abstract  string UserAppDataPath
        {
            get;
        }

        public abstract  bool IsTransparentProxy(object obj);
        /// <summary>
        /// override this method to return a screen info.
        /// </summary>
        /// <returns></returns>
        public abstract ICoreScreenInfo GetScreenInfo();

        public virtual string Title
        {
            get { 
                return "title.app".R();
            }
        }

        public virtual  void Register(CoreSystem instance)
        {
            this.m_coreSystem = instance;
        }

        public abstract  string PrivateFontsDirectory
        {
            get;
        }

        public virtual string AppName
        {
            get { return this.m_appName;  }
        }

        public virtual  string Copyright
        {
            get { return this.m_copyright; }
        }

        public virtual  string AppAuthor
        {
            get { return this.m_author; }
        }

        /// <summary>
        /// override this to create a new workbench for your application
        /// </summary>
        /// <returns></returns>
        public virtual ICoreSystemWorkbench CreateNewWorkbench() {
            return null;
        }


        /// <summary>
        /// filter the list of assemblies provided by CoreAddIn
        /// </summary>
        /// <param name="m"></param>
        public virtual void OnPrefilterAssemblyList(List<string> m)
        {               
         
        }



        public virtual ICoreApplicationService GetService(string name)
        {
            return null;
        }

        public virtual T GetService<T>() where T : ICoreApplicationService
        {
            return default(T);
        }
    }
}
