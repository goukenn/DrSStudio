using IGK.ICore;
using IGK.ICore.Web;
using IGK.ICore.WinCore.WinUI.Controls;
using IGK.ICore.WinUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IGK.ICore.Xml;
using IGK.ICore.Resources;
using IGK.DrSStudio.Android.Entities;
using System.IO;
using IGK.ICore.IO;

namespace IGK.DrSStudio.Android.AndroidThemeBuilder.WinUI
{
    [CoreSurface("{85338003-01D5-4FDD-ADE2-BDFC18EC149C}")]
    public class AndroidThemeFileEditorSurface : IGKXWinCoreWorkingFileManagerSurface, 
        ICoreWorkingFilemanagerSurface,
        ICoreWebReloadDocumentListener
    {
        IGKXWebBrowserControl c_webControl;
        private Dictionary<string, AndroidTheme.AndroidThemeValue> m_properties;
        private AndroidTheme.AndroidThemeAttributeCollections m_themedefinition;

        public ATBThemeFile ThemeFile { get; private set; }
        public override string Title
        {
            get
            {
                return $"{ThemeFile.CurrentTheme.Name} - [Android Theme]";
            }
            protected set
            {
            }
        }

        public string SearchKey { get; internal set; }


        public string getSearchKey() {
            return this.SearchKey;
        }
        public override void SetParam(ICoreInitializatorParam p)
        {
            base.SetParam(p);

            this.ThemeFile = new ATBThemeFile();
            if (p.Contains("Parent")) {
                this.ThemeFile.CurrentTheme.Parent = p["Parent"]?.Trim();
            }
            if (p.Contains("FileName")) {
                this.ThemeFile.FileName = p["FileName"]?.Trim();
            }

            
            this.ThemeFile.DefaultPlateForm = AndroidSystemManager.GetAndroidTargetByName(p["Platform"])
                ?? AndroidSystemManager.GetAndroidTargetByName(IGK.DrSStudio.Android.Settings.AndroidSetting .Instance.DefaultPlatform)
                ;// as AndroidTargetInfo;

            if (this.ThemeFile.DefaultPlateForm == null)
                throw new Exception($"Can't initialize the surface {nameof(this.ThemeFile.DefaultPlateForm)} is null");
        }

        public AndroidThemeFileEditorSurface():base()
        {
            this.c_webControl = new IGKXWebBrowserControl()
            {
                Dock = System.Windows.Forms.DockStyle.Fill
            };
            this.ThemeFile = new ATBThemeFile();
            this.Controls.Add (c_webControl);
        }

        public override  ICoreSaveAsInfo GetSaveAsInfo()
        {
            return new CoreSaveAsInfo(
                "title.saveAsAdroidTheme".R(),
                "android xml theme resources | *.xml",
                this.FileName ?? this.ThemeFile.FileName );
        }
        public override void Save()
        {
            if (File.Exists(this.FileName))
            {
                this.ThemeFile.Save(this.FileName);
                this.NeedToSave = false;
                this.ThemeFile.FileName = this.FileName;
                OnSaved(EventArgs.Empty);
            }
            else {
                CoreSystem.Instance.Workbench.CallAction ( "File.SaveAs");                
            }
        }
        public override  void SaveAs(string filename)
        {
            this.ThemeFile.Save(filename);
            this.NeedToSave = false;
            this.FileName = filename ;
            OnSaved(EventArgs.Empty);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            IGK.ICore.Xml.CoreXmlWebDocument c =IGK.ICore.Xml.CoreXmlWebDocument.CreateICoreDocument();// CoreXmlWebDocument ();
            c.InitAndroidWebDocument();
            c.ForWebBrowserDocument = true;
            this.LoadDocument(c);
            this.c_webControl.AttachDocument (c);
            this.c_webControl.SetReloadDocumentListener(this);//.AttachDocument (c);
            this.c_webControl.ObjectForScripting = new AndroidThemeFileEditorScripting(this);
        }

        internal static AndroidThemeFileEditorSurface Create(ATBThemeFile f)
        {
            AndroidThemeFileEditorSurface v_o = new AndroidThemeFileEditorSurface()
            {
                ThemeFile = f
            };
            return v_o;
        }
        public static AndroidThemeFileEditorSurface CreateSurface(object param) {

            
            return null;
        }
        

        public void ReloadDocument(CoreXmlWebDocument document, bool attachDocument)
        {
            this.LoadDocument(document);
        }

        private void LoadDocument(CoreXmlWebDocument document)
        {

            document.Body.Clear();
            document.Body.LoadString (
                  IGK.ICore.Web.CoreWebUtils.EvalWebStringExpression(
#if DEBUG
                 CoreFileUtils.ReadAllFile(CoreConstant.DRS_SRC +@"\addins\android\IGK.DrSStudio.AndroidAddIn\AndroidThemeBuilder\Resources\FileEditor.xml")
                  ?? Encoding.UTF8.GetString(Properties.Resources.FileEditor)
#else
                  Encoding.UTF8.GetString(Properties.Resources.FileEditor)
#endif
                  , 
                  this));
        }


        /// <summary>
        /// build theme properties
        /// </summary>
        public void BuildPropertyInfo() {
            Dictionary<string, AndroidTheme.AndroidThemeValue> info = new Dictionary<string, AndroidTheme.AndroidThemeValue> ();
            string v_targetName =
                this.ThemeFile.DefaultPlateForm?.TargetName;

            var c = AndroidTheme.GetAndroidThemes(
                this.ThemeFile.DefaultPlateForm?.TargetName);
            const string ROOT = "Theme";
            AndroidTheme root = c[ROOT];
            AndroidTheme q = c[ThemeFile.CurrentTheme.Parent ?? ROOT];

            Dictionary<string,string> primaryThemes = new Dictionary<string, string>();

            foreach (var h in this.ThemeFile.Themes)
            {
                foreach (var gs in h.GetKeys())
                {
                    primaryThemes.Add(gs, h.GetValue(gs));
                }
            }

            //get public theme attributes
            m_themedefinition = IGK.DrSStudio.Android.Entities.AndroidTheme.GetAndroidThemeAttrs(v_targetName);



            //init global definition
            foreach (var i in m_themedefinition.GetGlobals()) {



                var o = new AndroidTheme.AndroidThemeValue()
                {
                    Name = i.Name,
                    DefaultValue = string.Empty, // cause we don't know the default value
                    Value = string.Empty ,
                    Type = "" //not recognise the default type, maybe by replacing we will get it
                };
                info.Add(i.Name, o);
                ThemeFile.CurrentTheme.Load(o );
            }

            foreach (var i in m_themedefinition.GetGroups("View")) {
                if (info.ContainsKey(i.Name))
                {
                    continue;
                }
                    var o = new AndroidTheme.AndroidThemeValue()
                {
                    Name = i.Name,
                    DefaultValue = string.Empty, // cause we don't know the default value
                    Value = string.Empty,
                    Type = "" //not recognise the default type, maybe by replacing we will get it
                };
                info.Add(i.Name, o);
                ThemeFile.CurrentTheme.Load(o);
            }

            bool replace = true;
            while (q != null) {
                foreach (KeyValuePair<string, AndroidTheme.AndroidThemeValue> s in q.Values) {
                    
                    if (m_themedefinition[s.Key] == null)
                        continue;
                    if (info.ContainsKey(s.Key))
                    {
                        if (!replace) {
                            continue;
                        }
                        info.Remove(s.Key);
                    }
                    ThemeFile.CurrentTheme.Load(s.Value);
                    info.Add(s.Key, s.Value);
                }
                q = q.Parent ?? ((q.Name != ROOT) ? root:null);
                replace = false;
            }
           m_properties = info;


            


            //update file values
            if (primaryThemes.Count > 0) {
                //setup primary theme;
                foreach (var item in primaryThemes)
                {
                    //filter properties
                    if (m_themedefinition[item.Key] == null)
                        continue;
                    this.ThemeFile.CurrentTheme.SetValue(item.Key, item.Value );

                    if (!m_properties.ContainsKey(item.Key)) {
                        AndroidTheme.AndroidThemeValue r = new AndroidTheme.AndroidThemeValue() {
                            Name = item.Key,
                            Type = null,
                            Value = item.Value 

                        };
                        m_properties.Add(item.Key, r);
                    }
                }
            }

            //update theme value

        }
        public string getThemePropertiesList() {

            if (m_properties == null)
                BuildPropertyInfo ();

           //var  v_g = null;
             //   AndroidTheme.GetAndroidTheme(AndroidTheme.GetAndroidThemes(
             //   this.ThemeFile.DefaultPlateForm.TargetName),
             //this.ThemeFile.CurrentTheme.Parent);
            var d = CoreXmlElement.CreateXmlNode("div");
            d.addDiv().setClass("fith overflow-y-a");
            //if (v_g != null)
            //{
            var ul = d.add("table").setClass("igk-table igk-table-toggle-row");// igk-table-striped");
            var tr = ul.add("tr");
            tr.add("th").Content = "Properties";
           // tr.add("th").Content = "Default";
            tr.add("th").Content = "Type";
            tr.add("th").Content = "Format";
            
            tr.add("th").Content = "Value";
            bool v_search = !string.IsNullOrEmpty(this.SearchKey);
            foreach (KeyValuePair<string, AndroidTheme.AndroidThemeValue> item in m_properties.OrderBy(i => i.Key))
            {

                if (v_search  && !item.Key.ToLower().Contains(this.SearchKey.ToLower()))
                {
                    continue;  

                } 
                bool v_changed = ThemeFile.CurrentTheme.IsValueChanged(item.Key);
           var v_v = v_changed ?
                    ThemeFile.CurrentTheme.GetValue (item.Key):
                    item.Value.Value ;

                var li = ul.Add("tr");
                BuildRow(li, item.Key);
                //// li.addInput("", "checkbox", item.Key);
                //li.add("td").add("span").Content = item.Key;
                //li.add("td").add("span").Content = v_changed ? "user": "default";
                
                //var gi = m_themedefinition[item.Key];
                //li.add("td").add("span").Content = gi.Type;

                //li.add("td").add("span").Content = item.Value.Type;// "type";
                //li.add("td").add("span").addInput(item.Key, "text", v_v).setClass ("igk-form-control");
            }

            

            return d.RenderXML (null);
            //StringBuilder sb = new StringBuilder();
            //return sb.ToString();
        }
        public string getThemeInfo() {
            var d = CoreXmlElement.CreateXmlNode ("div");
            d.setAttribute("style", "margin-top: 32px");
            var ul= d.add ("ul");
            var li = ul.add("li");            
                li.add("label")
                .setAttribute("style", "margin-right:4px")
                .setContent("lb.name".R());
            li.add("span").setContent(":");
            li.add("span").setContent (this.ThemeFile.CurrentTheme.Name );

            li = ul.add("li");
                li.add("label")
                .setAttribute("style", "margin-right:4px")
                .setContent ("lb.parent".R());
            li.add("span").setContent(":");
            li.add("span").setContent(this.ThemeFile.CurrentTheme.Parent);

            //platform 
            li = ul.add("li");
            li.add("label")
            .setAttribute("style", "margin-right:4px")
            .setContent("lb.android.platform".R());
            li.add("span").setContent(":");
            li.add("span").setContent(this.ThemeFile.DefaultPlateForm.TargetName);

            return d.RenderXML (null);
        }

        internal string UpdateTheme(string id, string value)
        {
            this.ThemeFile.CurrentTheme.SetValue(id, value);          
            var li = CoreXmlElement.CreateXmlNode("tr");
            BuildRow(li, id);
            return li.RenderXML( new CoreXmlHtmlOptions()
            {

            });

        }

        private void BuildRow(CoreXmlElement li, string id)
        {
            var item = m_properties[id];
            bool v_changed = ThemeFile.CurrentTheme.IsValueChanged(id);
            var go = ThemeFile.CurrentTheme.GetValue(id);
            // li.addInput("", "checkbox", item.Key);
            li.add("td").add("span").Content = id;
            li.add("td").add("span").Content = v_changed ? "user" : "default";
            var gi = m_themedefinition[id];
            var ti = gi?.Type ?? string.Empty;
            li.add("td").add("span").Content = gi?.Type;
            li.add("td").add("span").Content = item.Type;// "type";

            if (gi == null) {
                CoreLog.WriteDebug("Not found " + id);

            }

            switch (gi?.Type) {
                case "enum":
                    {
                        var ls = li.add("td").add("span").add("select").setId(id).setClass("igk-form-control");
                        foreach (var i in gi.GetEnumValues()) {
                            var opt = ls.Add("option");

                            opt.Content  = i;
                            opt["value"] = i;
                            if (i == go) {
                                opt["selected"] = "true";
                            }
                        }
                    }
                    break;
                case "flag":
                    {
                        var td = li.add("td");
                        var ls = td.add("span").addInput(id, "text", go).setClass("igk-form-control");
                        string g =
                        string.Join("| ", gi.GetEnumValues());

                        td.addDiv().setClass("igk-tooltip").setAttribute("data", g).Content = g;
                    }
                    break;
                default:
                    {
                        var td = li.add("td");
                            td.add("span").addInput(id, "text", go).setClass("igk-form-control");

                        if (ti.Contains("color") && go.StartsWith ("#")) {
                            td.addDiv().setClass("color").SetAttribute("style", "background-color:" + go);
                        }
                    }
                    break;
            }
            
        }
    }
}
