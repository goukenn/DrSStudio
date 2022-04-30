using IGK.ICore.ComponentModel;
using IGK.ICore.Settings;
using IGK.ICore.WinUI;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.ICore.WinCore.Tools
{
    class WinCoreRegistryRendererSaveTool
    {
        private static WinCoreRegistryRendererSaveTool sm_instance;
        private WinCoreRegistryRendererSaveTool()
        {
        }

        public static WinCoreRegistryRendererSaveTool Instance
        {
            get
            {
                return sm_instance;
            }
        }
        static WinCoreRegistryRendererSaveTool()
        {
            sm_instance = new WinCoreRegistryRendererSaveTool();
            System.Windows.Forms.Application.ApplicationExit += _ApplicationExit;
        }

        static void  _ApplicationExit(object sender, EventArgs e)
        {
            if (CoreApplicationSetting.Instance.AllowSaveToRegistry)
            {
                SaveSkinToReg();
            }
        }


        /// <summary>
        /// stave all rendring setting to system user registery
        /// </summary>
        private static void SaveSkinToReg()
        {
            if (Environment.OSVersion.Version.Major < 5)
                return;
            RegistryKey Reg = System.Windows.Forms.Application.UserAppDataRegistry;            
            RegistryKey v_skin = null;
            v_skin = Reg.OpenSubKey(CoreConstant.REG_SKINS,true );
            if (v_skin == null)
              v_skin =  Reg.CreateSubKey(CoreConstant.REG_SKINS, RegistryKeyPermissionCheck.ReadWriteSubTree );
            TypeConverter v_conv = null;
            var e = CoreRenderer.GetRendereringEnumerator();
            
            foreach (KeyValuePair<string, ICoreRendererSetting> k in e)
            {
                v_conv = CoreTypeDescriptor.GetConverter(k.Value);
                try
                {
                    v_skin.SetValue(k.Key, string.Format("{0},{1}",
                        k.Value.Type.ToString(),
                        v_conv.ConvertToString(
                        k.Value.Value)),
                        RegistryValueKind.String);
                }
                catch {
                    CoreLog.WriteDebug("Cant write to registry");
                }
            }
            v_skin.Flush();
            v_skin.Close();
        }
        private static void LoadFormReg()
        {
            RegistryKey Reg = System.Windows.Forms.Application.UserAppDataRegistry;
            RegistryKey v_skin = null;
            v_skin = Reg.OpenSubKey(CoreConstant.REG_SKINS);
            if (v_skin == null)
                return;
            object v_value= null;
            RegistryValueKind v_kind = RegistryValueKind.String;
            //sm_renderingAttributes.Clear();
            try
            {
                List<CoreRendererSetting> v_setting = new List<CoreRendererSetting> ();
                foreach (string c in v_skin.GetValueNames())
                {
                    v_kind = v_skin.GetValueKind(c);
                    if (v_kind == RegistryValueKind.String)
                    {
                        try
                        {
                            v_value = v_skin.GetValue(c);
                            string[] t = v_value.ToString().Split(',');
                            if (t.Length == 2)
                            {
                               v_setting.Add(new CoreRendererSetting(c,
                                         (enuRendererSettingType)Enum.Parse(typeof(enuRendererSettingType), t[0]),
                                         t[1]));
                            }
                        }
                        catch
                        {
                        }
                    }
                }
                CoreRenderer.LoadSettings(v_setting.ToArray ());
            }
            catch
            {
            }
            finally
            {
                v_skin.Close();
            }
        }
    }
}
