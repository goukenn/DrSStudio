
using IGK.DrSStudio.Android.AndroidMenuBuilder.WinUI;
using IGK.ICore.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.DrSStudio.Android.AndroidMenuBuilder.Menu.File
{
    
    using IGK.ICore;
    using IGK.ICore.WinUI;
    using IGK.ICore.Xml;
    using IGK.ICore.Web;
    using System.Runtime.InteropServices;
    using IGK.ICore.Web.WinUI;
    using IGK.ICore.WinUI.Common;


    /// <summary>
    /// create a new android menu resource
    /// </summary>
    [CoreMenu("File.New.Android.Menu", 0x10)]
    public sealed class NewAndroidMenu : CoreApplicationMenu
    {
        [ComVisible(true)]
        public class ScriptObject : CoreWebScriptObjectBase, ICoreWebDialogProvider
        {
            public ICoreWebScriptObject OjectForScripting
            {
                get { return this; }
            }

        

            public ScriptObject() {

            }
            public void create_menu(string name, string outFolder)
            {
                Menu.m_Name = name;
                Menu.m_OutFolder = outFolder;
                this.DialogResult = enuDialogResult.OK;
            }
            public string getOutputFolder()
            {
                return Environment.CurrentDirectory;
            }
            public string pickFolder(string value) {           
                return IGK.ICore.WinUI.Common.CoreCommonDialogUtility.PickFolder(this.Menu.Workbench, null, value );
                //return CoreCommonDialogUtility.PickFolder(this.Menu.Workbench, null, value );
            }
            public override void Reload()
            {
                this.Document.Body.Clear();
                this.Menu.Render(this.Document);
            }
            public NewAndroidMenu Menu { get; set; }
        }
        protected override bool PerformAction()
        {
            CoreXmlWebDocument v_document = CoreXmlWebDocument.CreateICoreDocument();
            v_document.InitAndroidWebDocument();
            Render(v_document);
            using (var dial = Workbench.CreateWebBrowserDialog(new ScriptObject() { 
                Menu=this,
                Document = v_document 
                
            }))
            {
                dial.Title = "title.android.newmenuresources".R();
                dial.Size = new Size2i(400, 360);
                if (dial.ShowDialog() == enuDialogResult.OK)
                {
                   Workbench.AddSurface (
                       new AndroidMenuBuilderSurface() { 
                        FileName = string.Format ("{0}/{1}.xml",
                        this.m_OutFolder,
                        this.m_Name )
                    }, true);
                }
            }
            return true;
        }

        private void Render(CoreXmlWebDocument v_document)
        {
            v_document.Body.LoadString(Encoding.UTF8.GetString (Properties.Resources.android_new_android_menu_form).CoreEvalWebStringExpression ());
        }

        private string m_Name ;
        private string m_OutFolder;
    }
}
