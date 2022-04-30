using IGK.ICore.Resources;
using IGK.ICore.Web;
using IGK.ICore.WinUI;
using IGK.ICore.WinUI.Common;
using IGK.ICore.Xml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace IGK.ICore.WinCore
{

    [ComVisible(true)]
    [CoreService("DesignerService")]
    public class WinCoreDesigner : ICoreDesignerService
    {
        private CoreXmlElement sl;

        public Type GetEditionTool(Type type)
        {

            var b = CoreSystem.GetWorkingObjects().GetEditionTools(type);

            if ((b!=null) && (b.Length > 0)) {
                var g = CoreWorkingObjectAttribute.GetCustomAttribute(type, typeof(CoreWorkingObjectAttribute)) as CoreWorkingObjectAttribute;
                Type gt = null;
                Dictionary<string, Type> v_tools = new Dictionary<string, Type>();

                var p = CoreSystem.Instance.Settings["DefaultDesignerTool"];
                var s  = p?[g.Name]?.Value;
                if (s != null) {
                    //find the type of name

                }

                //CoreMessageBox.NotifyMessage("Please select a edition tools","Here some Tool for edition " + b.Length);

                if (gt == null) {

                    string[] v_name = new string[b.Length + 1];
                    v_name[0] = g.Name;

                    var div = CoreXmlElement.CreateXmlNode("div");
                    sl = CoreXmlElement.CreateXmlNode("select");
                    sl.Add("option").SetAttribute("value", v_name[0]).Content = v_name[0];
                    v_tools.Add(v_name[0], type);
                    for (int i = 0; i < v_name.Length-1; i++)
                    {

                        v_name[i + 1] = CoreWorkingObjectAttribute.GetObjectName(b[i]);// (CoreWorkingObjectAttribute.GetCustomAttribute(type, typeof(CoreWorkingObjectAttribute)) as CoreWorkingObjectAttribute).Name;
                        v_tools.Add(v_name[i+1], b[i]);
                        sl.Add("option").SetAttribute("value", v_name[i + 1]).Content = ("DE_"+v_name[i + 1]).R();
                    }

                   // return v_tools["BezierPenTool"];

                    var row = div.Add("div").SetAttribute("class", "igk-row");
                    row.Add("div");
                    div.AddChild(sl);
                    div.Add("button").SetAttribute("class", "igk-btn igk-btn-default igk-winui-btn-action")
                        .SetAttribute("value", "btn.ok".R()).Content = "SD";

                    var source = string.Empty;
                    
                    source = Encoding.UTF8.GetString(Properties.Resources.mecanism_designer_content);

                    var dic =  CoreCommonDialogUtility.BuildWebDialog(CoreSystem.GetWorkbench(),
                        "Select Designer Tool",
                        CoreWebUtils.EvalWebStringExpression(source,
                        this)
                        );
                    if (dic != null) {
                        var dics = new IGK.ICore.JSon.CoreJSon().ToDictionary(dic.ToString());
                        type = v_tools[dics["tools"].ToString()];
                        if (p!=null)
                        p[g.Name].Value = dics["tools"].ToString();
                        if (dics.ContainsKey("remindselection")) {                            
                            if (p != null)
                                p["AlwayShowDialog"].Value = true;
                        }
                     }

                }

                //select a tool
            }
            return type;
        }
        public string getToolsOptions() {
            return sl.RenderInnerHTML(null);
        }
    }
}
