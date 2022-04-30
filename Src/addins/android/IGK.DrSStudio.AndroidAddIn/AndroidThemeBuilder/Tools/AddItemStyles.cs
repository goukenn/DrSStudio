using IGK.ICore.Web;
using IGK.ICore.Web.WinUI;
using IGK.ICore.WinUI;
using IGK.ICore.Xml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace IGK.DrSStudio.Android.AndroidThemeBuilder.Tools
{

    [ComVisible(true)]
    public class AddItemStyle : CoreWebScriptObjectBase, ICoreWebDialogProvider
    {
        private AndroidThemeEditorManagerTool androidThemeEditorManagerTool;
        private ATBTheme androidTheme;
        public ICore.WinUI.ICoreDialogForm dialog;

        public void create_new()
        {
            dialog.DialogResult = enuDialogResult.OK;
        }
        internal AddItemStyle(AndroidThemeEditorManagerTool tool, ATBTheme androidTheme)
        {
            this.androidThemeEditorManagerTool = tool;
            this.androidTheme = androidTheme;
        }


        public ICoreWebScriptObject OjectForScripting
        {
            get { return this; }
        }


    }
  
}
