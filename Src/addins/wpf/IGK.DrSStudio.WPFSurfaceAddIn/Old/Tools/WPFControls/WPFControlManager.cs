

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: WPFControlManager.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
/*
IGKDEV - [2008 - 2014]
author: C.A.D . BONDJE DOUE
file:WPFControlManager.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using IGK.ICore.WinCore;
using IGK.ICore;using IGK.DrSStudio.WPFSurfaceAddIn;
using System.Reflection.Emit;
namespace IGK.DrSStudio.WPFSurfaceAddIn.Tools.WPFControls
{
    using IGK.DrSStudio.WPFSurfaceAddIn.WorkingObjects;
    using IGK.DrSStudio.WinUI;
    [CoreTools ("Tool.WPFTools")]
    sealed class WPFControlManager : WPFToolBase
    {
        private static WPFControlManager sm_instance;
        private WPFControlManager()
        {
        }
        public static WPFControlManager Instance
        {
            get
            {
                return sm_instance;
            }
        }
        static WPFControlManager()
        {
            sm_instance = new WPFControlManager();
            sm_instance.LoadTools();
        }
        protected override void GenerateHostedControl()
        {
            this.HostedControl = new UIXWPControl();// base.GenerateHostedControl();
        }
        void LoadTools()
        {
            ConstructorInfo attribCtrInfo  = typeof (WPFControlElementAttribte ).GetConstructor (
                new Type[]{typeof(string), typeof (Type )});
            PropertyInfo v_imgkeyPr = typeof(WPFControlElementAttribte).GetProperty("ImageKey");
            Type v_controlType = typeof(System.Windows.Controls.Control);
            Type mecanism = typeof(WPFControlElement.ControlMecanismBase);
            //create assembly name
            AssemblyName v_asmName = new AssemblyName(WPFConstant.CONTROL_ASSEMBLY);
            //create assembly builder            
            AssemblyBuilder asmbuilder = AppDomain.CurrentDomain.DefineDynamicAssembly(v_asmName ,
                 AssemblyBuilderAccess.Run );
            //set asm attribute
            CustomAttributeBuilder v_cattribB = new CustomAttributeBuilder (
                typeof (CoreAddInAttribute ).GetConstructor(Type.EmptyTypes ),
                new object[0]);            
            asmbuilder.SetCustomAttribute(v_cattribB);
            //create module
            ModuleBuilder v_modb =  asmbuilder.DefineDynamicModule(
                v_asmName.Name);
            TypeBuilder v_tb = null;
            TypeBuilder v_ntb = null;
            //Assembly asm = Assembly.LoadFile("Microsoft.Windows.Design.dll");
            Type v_rsType =
            Type.GetType("MS.Internal.Controls.PropertyEditing.EditModeSwitchButtonKeyboardFix", false);
            //);
            //get resources
            Dictionary<string, string> v_dic = new Dictionary<string, string>();
            if (v_rsType != null)
            {
                string[] rs = //asm
                    Assembly.GetAssembly(
                v_rsType
                )
                .GetManifestResourceNames();
                foreach (string item in rs)
                {
                    v_dic.Add(item, item);
                }
            }
            foreach (Assembly item in AppDomain.CurrentDomain.GetAssemblies())
            {
                try
                {
                    foreach (Type c in item.GetTypes())
                    {
                        if (c.IsSubclassOf(v_controlType))
                        {
                            if (c.IsAbstract) continue;
                            if (c.IsNotPublic) continue;
                            if (c.IsNested) continue;
                            //v_attrib = new WPFElementAttribute(c.Name, mecanism );
                            try
                            {
                                v_tb = v_modb.DefineType(WPFConstant.CONTROL_ASSEMBLY + "." + c.Name,
                                    TypeAttributes.Class |
                                     TypeAttributes.AutoClass |
                                     TypeAttributes.Sealed |
                                     TypeAttributes.AutoLayout |
                                     TypeAttributes.BeforeFieldInit,
                                     typeof(WPFControlElement));
                                v_ntb = v_tb.DefineNestedType("Mecanism", TypeAttributes.Public | TypeAttributes.NestedPublic | TypeAttributes.Sealed,
                                    typeof(WPFControlElement.ControlMecanismBase));
                                //-------------------------------------
                                //override  the control type
                                //-------------------------------------
                                MethodBuilder bm = v_tb.DefineMethod("ControlType",
                                     MethodAttributes.Public | MethodAttributes.ReuseSlot |
                    MethodAttributes.Virtual | MethodAttributes.HideBySig, typeof(Type),
                    Type.EmptyTypes);
                                ILGenerator r = bm.GetILGenerator();
                                r.Emit(OpCodes.Ldtoken, c);
                                r.Emit(OpCodes.Call, typeof(Type).GetMethod("GetTypeFromHandle"));
                                r.Emit(OpCodes.Ret);
                                Type v_stb = v_tb.CreateType();
                                Type v_mecaType = v_ntb.CreateType();
                                string v_rsname = "MS.Internal.Resources.ToolboxBitmaps." + c.Name + ".bmp";
                                //string[] tr = c.Assembly.GetManifestResourceNames();
                                //System.Drawing.Bitmap bmp = new System.Drawing.Bitmap(
                                //    c, "MS.Internal.Resources.ToolboxBitmaps"+c.Name+".bmp" );
                                if (v_dic.ContainsKey(v_rsname))
                                {
                                    CoreResourcesCollections.Register(
                                        v_rsname,
                                    Assembly.GetAssembly(v_rsType).GetManifestResourceStream(v_rsname));
                                }
                                v_tb.SetCustomAttribute(
                                    new CustomAttributeBuilder(
                                        attribCtrInfo,
                                        new object[]{
                                    c.Name ,
                                    v_mecaType 
                                }, new PropertyInfo[]{
                                    v_imgkeyPr
                                },
                                    new object[]{
                                    v_rsname 
                                }));
                                CoreSystem.Instance.WorkingObjects.RegisterType(v_stb,
                                    v_stb.GetCustomAttributes(false)[0] as CoreWorkingObjectAttribute);
                            }
                            catch (Exception ex)
                            {
                                CoreLog.WriteDebug(ex.Message);
                            }
                            //CoreSystem.Instance.WorkingObjects .RegisterType(typeof (ControlMecanism ),
                            //    v_attrib);
                        }
                    }
                }
                catch (Exception ex)
                {
                    CoreLog.WriteDebug(ex.Message + "\n" + ex.StackTrace);
#if DEBUG
                    System.Windows.Forms.MessageBox.Show(ex.Message+"\n"+ex.StackTrace , "Error in WPFControl Manager");
#endif
                }
            }
        }
        class UIXWPControl : IGKXToolConfigControlBase 
        {
            IGKXListBox lsb = new IGKXListBox();
            public UIXWPControl():base(Instance )
            {
                this.Controls.Add(lsb);
                lsb.Dock = System.Windows.Forms.DockStyle.Fill;
            }
            protected override void OnLoad(EventArgs e)
            {
                base.OnLoad(e);
            }
        }
    }
    }

