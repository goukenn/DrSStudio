

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: WinCoreDesignerApplication.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
using IGK.ICore;
using IGK.ICore.ComponentModel;
using IGK.ICore.Drawing2D;
using IGK.ICore.WinUI;
/*
IGKDEV @ 2008-2016
author: C.A.D . BONDJE DOUE
file:WinCoreDesignerApplication.cs
*/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Runtime.Remoting;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace IGK.DrSStudio.WinUI.Design
{
    /// <summary>
    /// represent a application designer
    /// </summary>
    [CoreApplication()]
    class WinCoreDesignerApplication : DrSStudioWinCoreApp
    {
        public WinCoreDesignerApplication()
        {
        }
        /// <summary>
        /// override this method to initialize custum dictionnary collection type for exemple
        /// </summary>
        /// <param name="instance"></param>
        public override void Register(CoreSystem instance)
        {
        }
        private Assembly FindAssembly(string name)
        {
            foreach (Assembly asm in AppDomain.CurrentDomain.GetAssemblies())
            {
                if (asm.FullName.Split(',')[0] == name)
                {
                    return asm;
                }
            }
            return null;
        }
        public override void Initialize()
        {
            base.Initialize();
            Assembly asm = FindAssembly("IGK.DrSStudio.WinCore");
            if (asm != null)
            {
                Type vector2fConv = asm.GetType ("IGK.DrSStudio.ComponentModel.Vector2fTypeConverter",false );
                Type colorfEditor = asm.GetType ("IGK.DrSStudio.ComponentModel.ColorfTypeEditor", false );
                Type colorfConv = asm.GetType("IGK.DrSStudio.ComponentModel.ColorfTypeConverter", false);
                

                TypeDescriptor.AddAttributes(typeof(Vector2f),new TypeConverterAttribute(vector2fConv) );
                TypeDescriptor.AddAttributes(typeof(Vector2f[]), new TypeConverterAttribute( asm.GetType ("IGK.DrSStudio.ComponentModel.Vector2fArrayTypeConverter")));
                TypeDescriptor.AddAttributes(typeof(Colorf), new EditorAttribute(colorfEditor, typeof(System.Drawing.Design.UITypeEditor)));
                TypeDescriptor.AddAttributes(typeof(Colorf), new TypeConverterAttribute(colorfConv));
                TypeDescriptor.AddAttributes(typeof(Size2f), new TypeConverterAttribute(asm.GetType ("IGK.DrSStudio.ComponentModel.Size2fConverter")));
                TypeDescriptor.AddAttributes(typeof(Size2i), new TypeConverterAttribute(asm.GetType ("IGK.DrSStudio.ComponentModel.Size2fConverter")));
                TypeDescriptor.AddAttributes(typeof(Size2d), new TypeConverterAttribute(asm.GetType ("IGK.DrSStudio.ComponentModel.Size2fConverter")));
                //TypeDescriptor.AddAttributes(typeof(Vector4f), new TypeConverterAttribute(typeof(Vector4Converter)));
                //TypeDescriptor.AddAttributes(typeof(Vector4i), new TypeConverterAttribute(typeof(Vector4Converter)));
                //TypeDescriptor.AddAttributes(typeof(Vector4d), new TypeConverterAttribute(typeof(Vector4Converter)));
                //TypeDescriptor.AddAttributes(typeof(Vector2f[]), new TypeConverterAttribute(typeof(Vector4Converter)));
                TypeDescriptor.AddAttributes(typeof(float[]), new TypeConverterAttribute(asm.GetType ("IGK.DrSStudio.ComponentModel.WinCoreFloatArrayConverter")));
                TypeDescriptor.AddAttributes(typeof(Rectanglef), new TypeConverterAttribute(asm.GetType ("IGK.DrSStudio.ComponentModel.RectanglefTypeConverter")));
                TypeDescriptor.AddAttributes(typeof(CoreFont), new TypeConverterAttribute(asm.GetType ("IGK.DrSStudio.ComponentModel.WinCoreFontTypeConverter")));
                TypeDescriptor.AddAttributes(typeof(Matrix), new TypeConverterAttribute(asm.GetType ("IGK.DrSStudio.ComponentModel.WinCoreMatrixTypeConverter")));
            }
            Debug.WriteLine("Load fonts ....");            
            System.Drawing.Text.InstalledFontCollection c = new System.Drawing.Text.InstalledFontCollection();
            foreach (FontFamily ftfam in c.Families)
            {
                CoreFont.RegisterFonts(this, ftfam.Name);
            }
        }
        public override void Close()
        {
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
      
        protected override ICoreBrushRegister CreateBrushRegister()
        {
            return new WinCoreDesignBrushRegister();
        }
        protected override ICoreResourceManager CreateResourceManager()
        {
            return new WinCoreDesignResourceManager();
        }
        public override void AddMessageFilter(ICoreMessageFilter messageFilter)
        {
        }
        public override void RemoveMessageFilter(ICoreMessageFilter messageFilter)
        {
        }
        public override void Run(ICoreMainForm coreMainForm)
        {
            if (coreMainForm is ICoreRunnableMainForm)
            (coreMainForm as ICoreRunnableMainForm).Run();
        }
        public override string UserAppDataPath
        {
            get
            {
                return System.Windows.Forms.Application.UserAppDataPath;
            }
        }

        public override string AppName
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public override string Copyright
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public override string AppAuthor
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public override bool RegisterClientSystem(Func<CoreSystem> __initInstance)
        {
            __initInstance();
            return false;
        }
        public override bool RegisterServerSystem(Func<CoreSystem> __initInstance)
        {
            __initInstance();
            return true;
        }
     
        public override ICoreScreenInfo GetScreenInfo()
        {
            return null;
        }
        public override bool IsTransparentProxy(object obj)
        {
            return RemotingServices.IsTransparentProxy(obj);
        }
        protected override ICoreD2DPathUtils CreatePathUtils()
        {
            return null;
        }
        protected override ICoreControlManager CreateControlManager()
        {
            return new WinCoreDesignControlManager();
        }
    }


}

