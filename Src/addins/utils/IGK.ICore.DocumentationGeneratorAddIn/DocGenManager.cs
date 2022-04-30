
using IGK.ICore;using IGK.ICore.ContextMenu;
using IGK.ICore.IO;
using IGK.ICore.Menu;
using IGK.ICore.Tools;
using IGK.ICore.Xml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.ICore
{
    public class DocGenManager
    {
        string[] m_langs;
        private DocGenManager()
        {
            m_langs = new string[] { "fr", "en" };
        }
        internal static void ExtractDocumentationTo(string folder, bool overwriteExisting)
        {
            DocGenManager doc = new DocGenManager();
            doc.OutputFolder = folder;
            doc.OverWriteExiting = overwriteExisting;
            doc.Generate();
        }

        private void Generate()
        {
            var doc = IGK.ICore.Xml.CoreXmlElement.CreateXmlNode ("html");
            var body = doc.Add("body");

            BuildAssembly(body);
            BuildActions(body);
            BuildTools(body);
            BuildMenu(body);
            BuildContextMenu(body);

            BuildWorkingObjects(body);            
            

            File.WriteAllText(
                Path.Combine(this.OutputFolder, "index.html"),
                "<?xml version=\"1.0\" encoding=\"utf-8\"?>\r\n" + doc.RenderXML(null));
        }

        private void BuildTools(CoreXmlElement body)
        {
            var v_t = body.Add("Tools");
            foreach (KeyValuePair<string, ICoreTool> tool in CoreSystem.GetTools())
            {
                var i = v_t.Add("Item");
                i["id"] = tool.Value.Id;
                i["igk-icore-addin"] = this.GetAddInName(tool.Value.GetType ().Assembly.FullName);
                
                this.BuildDesc(i, tool.Key, "tools");
            }

            
        }

        private void BuildActions(CoreXmlElement body)
        {
            var v_t = body.Add("Actions");
            foreach (var ack in CoreSystem.GetActions())
            {
                var v_a = v_t.Add("Action");
                v_a["id"] = ack.Id;
                v_a["igk-icore-action-type"] = ack.ActionType.ToString();
                v_a["igk-icore-addin"] = this.GetAddInName(ack.GetType().Assembly.FullName);
                BuildDesc(v_a, ack.Id, "actions");
            }
        }

        private CoreXmlAttributeValue GetAddInName(string p)
        {
            return p;
        }

        private void BuildMenu(CoreXmlElement body)
        {
            var v_t = body.Add("Menus");
            foreach (KeyValuePair<string,ICoreMenuAction> ack in CoreSystem.GetMenus())
            {
                var v_a = v_t.Add("Menu");
                v_a["id"] = ack.Value.Id;
                v_a["igk-icore-action-type"] = ack.Value.ActionType.ToString();
                v_a["igk-icore-addin"] = this.GetAddInName(ack.GetType().Assembly.FullName);
                
            }
        }

        private void BuildContextMenu(CoreXmlElement body)
        {
            var v_t = body.Add("ContextMenus");
            foreach (KeyValuePair<string,ICoreContextMenuAction> ack in CoreSystem.GetContextMenus())
            {
                var v_a = v_t.Add("ContextMenu");
                v_a["id"] = ack.Value.Id;
                v_a["igk-icore-action-type"] = ack.Value.ActionType.ToString();
                v_a["igk-icore-addin"] = this.GetAddInName(ack.Value.GetType().Assembly.FullName);
                //BuildDesc(ack.Id, "contextmenus");
            }
        }

        private void BuildWorkingObjects(CoreXmlElement body)
        {
            var v_t = body.Add("WorkingObjects");
            foreach (KeyValuePair<string, ICoreWorkingObjectInfo> ack in CoreSystem.GetWorkingObjects ())
            {
                var v_a = v_t.Add("Item");
                v_a["id"] = ack.Key;
                v_a["igk-icore-addin"] = this.GetAddInName(ack.Value.Type.Assembly.FullName);
                v_a["igk-icore-xmlns"] = ack.Value.Attribute.NameSpace;
                string[] k = ack.Value.Attribute.NameSpace.Split(
                    new string[] { CoreConstant.WEBSITE+  (CoreConstant.WEBSITE.EndsWith("/")?"":"/")}, StringSplitOptions.RemoveEmptyEntries);
                
                BuildDesc(v_a, Path.Combine (k[0],ack.Key), "workingobjects");
            }
        }
       

        private void BuildAssembly(CoreXmlElement body)
        {
            //this.m_assemblies = new Dictionary<string, ICoreAddIn >();
            var v_asms = body.Add("assemblies");
            CoreAddInAttribute v_attr = null;
            foreach (ICoreAddIn  dd in CoreSystem.GetAddIns())
            {
                var v_asm = v_asms.Add("assembly");
                v_asm["id"] = dd.Assembly.FullName;
                v_attr = CoreAddInAttribute.GetAttribute(dd.Assembly);
                v_asm.Add("author").Content = v_attr.AuthorName;
                v_asm.Add("author-contact").Content = v_attr.AuthorContact;
                
                //build asm info

            }
        }
        private void BuildDesc(CoreXmlElement item, string id, string name)
        {
            string file = "";

            foreach (string l in this.m_langs)
            {
                file = id + "." + l + ".html";
                string ofile = Path.Combine(this.OutputFolder,
                    "desc", name, file);
                if (IGK.ICore.IO.PathUtils.CreateDir(PathUtils.GetDirectoryName(ofile)))
                {
                    if (this.OverWriteExiting || !File.Exists(ofile))
                        File.WriteAllText(ofile, GetDefaultDescription(item, id, name));
                }
            }
        }

        private string GetDefaultDescription(CoreXmlElement item, string id, string name)
        {
            var div = CoreXmlWebElement.CreateXmlNode("div");
            div["class"] = "igk-doc-item";

            var v_desc_title = div.Add("div");
            v_desc_title["class"] = "igk-doc-title";
            v_desc_title.Content = string.Format("[lang:{0}]", id);
            
            
            var v_desc_desc = div.Add("div");
            v_desc_desc["class"] = "igk-doc-description";




            return div.RenderXML(null);
        }

        public string OutputFolder { get; set; }
        public bool OverWriteExiting { get; set; }
    }
}
