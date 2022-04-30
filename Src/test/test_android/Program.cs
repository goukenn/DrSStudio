using IGK.DrSStudio.Android;
using IGK.DrSStudio.Android.AndroidThemeBuilder.WinUI;
using IGK.ICore;
using IGK.ICore.Actions;
using IGK.ICore.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace test_android
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            try
            {
               CoreSystem.Init();

              var v_p =   CoreWorkingProjectItemTemplateAttribute.GetProject(AndroidConstant.PROJECT_NAME + ".Theme");
                if (v_p == null) {
                    throw new Exception("can't create android theme project");
                }
                //  v_p.Params = "g:ico";
                var gt = CoreSettings.GetSetting("Android.AndroidGeneral.PlatformSDK");//?.setValue("d");//.ToString();// Value == "";
                gt.Value =  "d://android";


                CoreSystem.GetAction("File.New.Android.ThemeFileEditor").DoAction();

                string sdk = (string)CoreSettings.GetSettingValue("Android.AndroidGeneral.PlatformSDK");

                var s = CoreWorkingProjectUtils.CreateAndInit(v_p);


                using (AndroidTemplateForm frm = new test_android.AndroidTemplateForm())
                {                  
                    frm.RunSurface(s);
                    Application.Run(frm);
                }
            }
            catch (Exception ex){
                CoreLog.WriteDebug("Exception : " + ex.Message);
            }
        }
    }
}
