
using IGK.ICore;using IGK.ICore.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.DrSStudio.Android.AndroidThemeBuilder.Tools
{

    using IGK.ICore.Xml;
    using IGK.ICore.Web;

    using System.Runtime.InteropServices;
    using IGK.ICore.WinUI;
    using IGK.ICore.Web.WinUI;
    using Entities;
    using IGK.ICore.WinUI.Common;

    [CoreTools("AndroidThemEditorManagerTool")]
    class AndroidThemeEditorManagerTool : CoreToolBase
    {
        private static AndroidThemeEditorManagerTool sm_instance;
        private AndroidThemeEditorManagerTool()
        {
        }

        public static AndroidThemeEditorManagerTool Instance
        {
            get
            {
                return sm_instance;
            }
        }
        static AndroidThemeEditorManagerTool()
        {
            sm_instance = new AndroidThemeEditorManagerTool();

        }

        internal bool AddNewItemStyleTo(ATBTheme androidTheme)
        {
            var m = new AddItemStyle(this, androidTheme);
            m.Document = GetThemeEditorDocument(androidTheme);
            var t =
            Workbench.CreateWebBrowserDialog(
               m
                );
            m.dialog = t;
            return t.ShowDialog() == enuDialogResult.OK;
        }

        private ICore.Xml.CoreXmlWebDocument GetThemeEditorDocument(ATBTheme androidTheme)
        {
            var r = CoreXmlWebDocument.CreateICoreDocument();
            var frm = r.Body.addForm();
            var ul = frm.addDiv().Add("ul");
            ul["class"] = "igk-ul-selection";
          //var  v_g = AndroidTheme.GetAndroidTheme (AndroidTheme.GetAndroidThemes( androidTheme.File?.DefaultPlateForm.TargetName ),
          //     androidTheme.Parent );
          //  if (v_g != null)
          //  {   
          //      foreach (KeyValuePair<string,AndroidTheme.AndroidThemeValue> item in v_g.Values)
          //      {
          //          var li = ul.Add("li").addDiv();
          //          li.addInput("", "checkbox", item.Key);
          //          li.add("span").Content = item.Key;
          //      }
          //  }
            ///get items properties list
         //   ATBTheme 
            //var li = ul.Add("li").addDiv();
            //li.addInput("",  "checkbox","backColor");
            //li.add("span").Content = "backColor";

            frm.addDiv ().setContent ("&nbsp;");
            var btn = frm.addInput("btn_ok", "submit", "btn.ok".R());
            btn["class"]="igk-btn fitw";
            btn["onclick"] = "javascript:window.external.create_new();";
            return r;
        }

    }
}
