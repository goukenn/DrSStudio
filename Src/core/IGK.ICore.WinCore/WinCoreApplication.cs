/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: WinCoreApplication.cs
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
file:WinCoreApplication.cs
*/
using System;

namespace IGK.ICore.WinCore
{
    using IGK.ICore;
    using IGK.ICore.ComponentModel;
    using IGK.ICore.Drawing2D;
    using IGK.ICore.IO;
    using IGK.ICore.WinCore.ComponentModel;
    using IGK.ICore.WinUI;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Drawing;
    using System.Runtime.Remoting;
    using System.Windows.Forms;
    /// <summary>
    /// Basic windows Application for Windows Application Textext
    /// </summary>
    public abstract class WinCoreApplication : CoreApplicationBase, 
        ICoreApplication,
        ICoreMessageFilterApplication
    {
       
        private ICoreBrushRegister m_brushRegister;
        private ICoreResourceManager m_WinCoreResourceManager;
        private ICoreControlManager m_controlManager;
        private ICoreD2DPathUtils m_graphicPathUtils;



        /// <summary>
        /// check update update if require. restart application.
        /// </summary>
        protected  virtual void CheckForUpdate()
        {

        }
        public override ICoreResourceManager ResourcesManager
        {
            get
            {
                if (m_WinCoreResourceManager == null)
                {
                    m_WinCoreResourceManager = CreateResourceManager();
                    System.Diagnostics.Debug.Assert(this.m_WinCoreResourceManager != null, "Resource Manager Not Created");
                }
                return m_WinCoreResourceManager;
            }
        }
        public override ICoreBrushRegister BrushRegister
        {
            get
            {
                if (this.m_brushRegister == null)
                {
                    this.m_brushRegister = CreateBrushRegister();
                    System.Diagnostics.Debug.Assert(this.m_brushRegister != null, "Brush register not created");
                }
                return this.m_brushRegister;
            }
        }
        public override ICoreControlManager ControlManager
        {
            get
            {
                if (this.m_controlManager == null)
                {
                    this.m_controlManager = CreateControlManager();
                    Debug.Assert(this.m_controlManager != null, "Control Manager Not Created");
                }
                return this.m_controlManager;
            }
        }
        public override ICoreD2DPathUtils GraphicsPathUtils
        {
            get
            {
                if (this.m_graphicPathUtils == null)
                {
                    this.m_graphicPathUtils = CreatePathUtils();
                    Debug.Assert(this.m_graphicPathUtils != null, "GraphicPath Utils not created!!!!");
                }
                return m_graphicPathUtils;
            }
        }



     
        public  override void Initialize()
        {
            base.Initialize();
            //CoreTypeDescriptor.AddAttributes(typeof(byte[]), new TypeConverterAttribute(typeof(ByteArrayConverter)));
            ////register descriptor
            //CoreTypeDescriptor.AddAttributes(typeof(Size2f), new TypeConverterAttribute(typeof(Size2fConverter)));
            //CoreTypeDescriptor.AddAttributes(typeof(Size2i), new TypeConverterAttribute(typeof(Size2fConverter)));
            //CoreTypeDescriptor.AddAttributes(typeof(Size2d), new TypeConverterAttribute(typeof(Size2fConverter)));
            //CoreTypeDescriptor.AddAttributes(typeof(float[]), new TypeConverterAttribute(typeof(CoreFloatArrayTypeConverter)));
            //CoreTypeDescriptor.AddAttributes(typeof(bool), new TypeConverterAttribute(typeof(CoreBooleanTypeConverter)));
            //wincore component model
            CoreTypeDescriptor.AddAttributes(typeof(Vector2f), new TypeConverterAttribute(typeof(WinCoreVector2fTypeConverter)));
            CoreTypeDescriptor.AddAttributes(typeof(Vector2f[]), new TypeConverterAttribute(typeof(WinCoreVector2fArrayTypeConverter)));
            CoreTypeDescriptor.AddAttributes(typeof(Colorf), new EditorAttribute(typeof(ColorfTypeEditor), typeof(System.Drawing.Design.UITypeEditor)));
            CoreTypeDescriptor.AddAttributes(typeof(Colorf), new TypeConverterAttribute(typeof(WinCoreColorfTypeConverter)));
            CoreTypeDescriptor.AddAttributes(typeof(Rectanglef), new TypeConverterAttribute(typeof(WinCoreRectanglefTypeConverter)));
            CoreTypeDescriptor.AddAttributes(typeof(Rectanglei), new TypeConverterAttribute(typeof(WinCoreRectanglefTypeConverter)));
            CoreTypeDescriptor.AddAttributes(typeof(CoreFont), new TypeConverterAttribute(typeof(WinCoreFontTypeConverter)));
            CoreTypeDescriptor.AddAttributes(typeof(Matrix), new TypeConverterAttribute(typeof(WinCoreMatrixTypeConverter)));
            CoreTypeDescriptor.AddAttributes(typeof(int), new TypeConverterAttribute(typeof(WinCoreInt32TypeConverter)));
            CoreTypeDescriptor.AddAttributes(typeof(float), new TypeConverterAttribute(typeof(WinCoreSingleTypeConverter)));        
            //register installed font
            System.Drawing.Text.InstalledFontCollection c = new System.Drawing.Text.InstalledFontCollection();
            foreach (FontFamily ftfam in c.Families)
            {
                CoreFont.RegisterFonts(this, ftfam.Name);
            }
        }

        /// <summary>
        /// used to register message filter. to windows system
        /// </summary>
        /// <param name="messageFilter"></param>
        public virtual void AddMessageFilter(ICoreMessageFilter messageFilter) {
            if (messageFilter is IMessageFilter)
                Application.AddMessageFilter(messageFilter as IMessageFilter);
            else {
                var f =  WinCoreMessageFilter.CreateFilter(messageFilter);
                Application.AddMessageFilter(f);
            }
        }
        /// <summary>
        /// unregister message filter
        /// </summary>
        /// <param name="messageFilter"></param>
        public virtual void RemoveMessageFilter(ICoreMessageFilter messageFilter) {
            if (messageFilter is IMessageFilter)
            {
                Application.RemoveMessageFilter(messageFilter as IMessageFilter );
            }
            else {
                var f = WinCoreMessageFilter.GetFilter(messageFilter);
                Application.RemoveMessageFilter(f);
                WinCoreMessageFilter.Remove(messageFilter);
            }
        }
        public override bool RegisterClientSystem(Func<CoreSystem> __initInstance)
        {
            return false;
        }
        public override bool RegisterServerSystem(Func<CoreSystem> __initInstance)
        {
            __initInstance();
            return true;
        }

        public override void Close()
        {
            Application.Exit();
        }
        public override event EventHandler ApplicationExit
        {
            add
            {
                Application.ApplicationExit += value;
            }
            remove
            {
                Application.ApplicationExit -= value;
            }
        }
        public override string CurrentWorkingPath
        {
            get { return Environment.CurrentDirectory; }
        }
        public override string StartupPath
        {
            get { return Application.StartupPath; }
        }
      
        public virtual void Run(ICoreMainForm coreMainForm)
        {
            if (coreMainForm is ICoreRunnableMainForm)
                (coreMainForm as ICoreRunnableMainForm).Run();
            else
            {
                coreMainForm.ShowDialog();
            }
        }
        public override string UserAppDataPath
        {
            get
            {
                return System.Windows.Forms.Application.UserAppDataPath;
            }
        }
        public bool IsTransparentProxy(CoreSystem instance)
        {
            return RemotingServices.IsTransparentProxy(instance);
        }

        protected virtual  ICoreControlManager CreateControlManager()
        {
            return new WinCoreControlManager();
        }
        protected virtual ICoreBrushRegister CreateBrushRegister()
        {
            return new WinCoreBrushRegister();
        }
        protected virtual  ICoreResourceManager CreateResourceManager()
        {
            return new WinCoreResourceManager();
        }
        protected virtual ICoreD2DPathUtils CreatePathUtils()
        {
            return new WinCoreGraphicsPathUtils(this);
        }
        public override string PrivateFontsDirectory
        {
            get
            {
                return PathUtils.GetPath("%startup%/Fonts");
            }
        }
        public override  ICoreScreenInfo GetScreenInfo()
        {
            return WinCoreScreenInfo.Instance;
        }
        public override bool IsTransparentProxy(object obj)
        {
            return RemotingServices.IsTransparentProxy(obj);
        }
        
     
    }
}

