using IGK.ICore.WinCore;
using IGK.DrSStudio.WinUI;
using IGK.ICore.Xml;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AndroidAddInSln
{
    using IGK;
    using IGK.ICore;
    using IGK.DrSStudio;
    using IGK.DrSStudio.Android;
    using IGK.ICore.IO;
    using IGK.DrSStudio.Android.Settings;
    using System.IO;
    using IGK.DrSStudio.Android.Sdk;
    using IGK.ICore.WinCore.WinUI.Controls;

    public partial class AttributeViewDemoForm : Form, IAttributeEditorLoader, IAttributeEditorStoreListener
    {
        private IGKXAttributeEditor c_de;
        private List<XmlElementBase> declare_styleables;
        private Dictionary<string, AndroidAttributeItem> m_list;
        private Dictionary<string, List<AndroidAttributeItem>> m_groups;
        public AttributeViewDemoForm()
        {
            InitializeComponent();
        }

        private string m_PlatForms;

        public string PlatForms
        {
            get { return m_PlatForms; }
            set
            {
                if (m_PlatForms != value)
                {
                    m_PlatForms = value;
                    OnPlatFormChanged(EventArgs.Empty);
                }
            }
        }
        public event EventHandler PlatFormChanged;
        ///<summary>
        ///raise the PlatFormChanged 
        ///</summary>
        protected virtual void OnPlatFormChanged(EventArgs e)
        {
            if (PlatFormChanged != null)
                PlatFormChanged(this, e);
        }


        private void _Load(object sender, EventArgs e)
        {
            this.PlatForms = "android-20";
            this.InitAndroidPlatFormAttributes(this.PlatForms);
            
            c_de = new IGKXAttributeEditor();
            c_de.Dock = DockStyle.Fill;

            c_de.Title = "title.FrameLayoutEditor".R();
            c_de.Description = null;
            c_de.AddRootNode("resources");
            c_de.SetAttributeLoaderListener (this);
            c_de.SetStoreAttributeListener (this);

            
            this.Controls.Add(c_de);
            this.Controls.SetChildIndex(c_de,0);
            this.LoadAttributeCategoryType();


            c_de.LoadNode(XmlElement.CreateXmlNode("FrameLayout"));


        }

        private void LoadAttributeCategoryType()
        {
            string path = Path.GetFullPath ( Path.Combine(PathUtils.GetPath(AndroidSetting.Instance.PlatformSDK),
                "platforms/"+this.PlatForms+"/data/res/values/attrs.xml"));

            if (!File.Exists(path))
                return;
            XmlElement l = XmlElement.LoadFile(path);
            string category = string.Empty;   
                //Edit Theme only
            this.editToolStripMenuItem.MergeAction = MergeAction.Append;
            declare_styleables = new List<XmlElementBase>();
            declare_styleables.AddRange(l.getElementsByTagName("declare-styleable"));

            declare_styleables.Sort(new Comparison<XmlElementBase>((x, y) =>
            {
                string s = x["name"].Value.ToString();
                string u = y["name"].Value.ToString();
                return s.CompareTo(u);
            }));
            foreach (var s in declare_styleables)
            {
                ToolStripItem g = this.editToolStripMenuItem.DropDownItems.Add (s["name"]);
                g.Name = s["name"];
                g.Tag = s;
                g.Click += (o, e)=>{
                    this.c_de.Add(s["name"]);                    
                };
            }
        
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var v_res = c_de.RootNode;
            if (v_res == null)
                return;

            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Title = "Store resources";
                sfd.Filter = "xml files | *.xml; *.axml";
                sfd.FileName = "resources.xml";
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    this.c_de.Save(sfd.FileName);
                }
            }
        }

        private void addStyleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.c_de.AddStyle("MyStyle", null);
        }

        //init attribute 
        private  void InitAndroidPlatFormAttributes(string platform)
        {
            //string platform = "android-20";
            string path = Path.GetFullPath(Path.Combine(PathUtils.GetPath(AndroidSetting.Instance.PlatformSDK),
                "platforms/" + platform + "/data/res/values/attrs.xml"));

            string wpath = Path.GetFullPath(Path.Combine(PathUtils.GetPath(AndroidSetting.Instance.PlatformSDK),
                "platforms/" + platform + "/data/widgets.txt"));
            AndroidWidgets c = AndroidWidgets.LoadWidgets(wpath);

            if (!File.Exists(path))
                return;

            XmlElement l = XmlElement.LoadFile(path);
            string v_category = string.Empty;
            string v_name = string.Empty;
            AndroidAttributeItem attr = null;
            Dictionary<string, List<AndroidAttributeItem>> v_groups = new Dictionary<string, List<AndroidAttributeItem>>();
            Dictionary<string, AndroidAttributeItem> v_list = new Dictionary<string, AndroidAttributeItem>();
            foreach (XmlElement  s in l.getElementsByTagName("attr"))
            {
                if ((s.Parent != null) && (s.Parent.TagName != "resources"))
                    v_category = s.Parent["name"];
                else
                    v_category = "Theme";
                v_name = s["name"];

                if (!v_list.ContainsKey(v_name))
                {
                    attr = AndroidAttributeItem.CreateAttributeItem(s);
                    attr.Value = null;
                    v_list.Add(v_name, attr);
                }
                else
                {
                    attr = v_list[v_name] as AndroidAttributeItem;
                    //update values
                    if (string.IsNullOrEmpty(attr.Type)) attr.Type = s["format"];
                    attr.AddCategory(v_category);
                }

                if (v_groups.ContainsKey(v_category))
                    v_groups[v_category].Add(attr);
                else
                    v_groups.Add(v_category, new List<AndroidAttributeItem>() { attr });
            }
            this.m_groups = v_groups;
            this.m_list = v_list;
        }

        public void LoadAttribute(IGKXAttributeEditor editor, XmlElement nodeName)
        {
            if (nodeName == null)
            {
                editor.SuspendLayout();
                editor.Attributes.Clear();
                editor.ResumeLayout ();
                return;
            }
            string platform = "android-20";
            string path = Path.GetFullPath ( Path.Combine(PathUtils.GetPath(AndroidSetting.Instance.PlatformSDK),
                "platforms/" + platform + "/data/res/values/attrs.xml"));

            string wpath = Path.GetFullPath(Path.Combine(PathUtils.GetPath(AndroidSetting.Instance.PlatformSDK),
                "platforms/" + platform + "/data/widgets.txt"));
            AndroidWidgets c = AndroidWidgets.LoadWidgets(wpath);

            var widget = c!=null ?c.GetWidget(nodeName.TagName):null;

            if (!File.Exists(path))
                return;



            editor.Cursor = Cursors.WaitCursor;
            string tagName = nodeName != null ? nodeName.TagName : null;
            XmlElement l = XmlElement.LoadFile(path);
            editor.SuspendLayout();
            editor.Attributes.Clear();
            editor.Title = "Theme Editor ";
            editor.ItemDefaultHeight = (int)"5mm".ToPixel();
            string v_category = string.Empty;
            switch(tagName)
            {
                case "style":
            {
                //Edit Theme only
                //load all theme theme style
                string v_name = string.Empty;
                foreach (var s in this.m_list.Values)
                {
                     AndroidAttributeItem attr = s.Clone() as AndroidAttributeItem ;
                    attr.Value = nodeName != null ? nodeName[attr.Name] : null;
                     editor.Attributes.Add(attr);
                }
                
                //
                /*foreach (var s in l.getElementsByTagName("declare-styleable"))
                {
                    if (s["name"] == "Theme")
                    {
                        category = "Theme";
                        v_name = string.Empty;
                        foreach (var b in s.getElementsByTagName("attr"))
                        {
                            
                            AndroidAttributeItem attr = AndroidAttributeItem.CreateAttributeItem(b);
                            attr.Value = nodeName != null ? nodeName[attr.Name] : null;
                            editor.Attributes.Add(attr);
                        }
                        break;
                    }
                }*/
            }
            break;
                default:
                    var v_tab = l.getElementsByTagName("declare-styleable");
            if (widget != null)
            {
                //is a widget
                while (widget != null)
                {
                    _loadAttr(editor, widget, nodeName, v_tab);
                    widget = widget.Parent;
                }
                //append styles properties

                foreach (var s in this.m_list.Values)
                {
                    if (editor.Attributes.Contains(s.Name))
                        continue;
                    AndroidAttributeItem attr = s.Clone() as AndroidAttributeItem;
                    string b = nodeName[attr.Name] ?? nodeName["android:" + attr.Name];
                    if (b != null)
                    {
                        attr.Value = b;
                    }
                    editor.Attributes.Add(attr);
                }
            }
            else
            {
                //not a widget
                foreach (XmlElement  s in v_tab)
                {
                    if (s["name"] == nodeName.TagName)
                    {
                        v_category = nodeName.TagName;
                        foreach (XmlElement b in s.getElementsByTagName("attr"))
                        {

                            AndroidAttributeItem attr = AndroidAttributeItem.CreateAttributeItem(b);
                            attr.Value =  nodeName[attr.Name] ?? nodeName["android:" + attr.Name];
                            editor.Attributes.Add(attr);
                        }
                        break;
                    }
                }
            }
            break;
            }

            editor.Sort();
            editor.ResumeLayout(true );
            editor.Cursor = Cursors.Default;
        }

        private void _loadAttr(IGKXAttributeEditor editor, AndroidWidgets.AndroidWidget widget, XmlElement nodeName, XmlElementBase[] v_tab)
        {
            string category = string.Empty;
            foreach (var s in v_tab)
            {
                if (widget.IsMatch(s["name"]))
                {
                    category = s["name"];
                    foreach (XmlElement b in s.getElementsByTagName("attr"))
                    {

                        AndroidAttributeItem attr = AndroidAttributeItem.CreateAttributeItem(b);
                        attr.Value = nodeName != null ? nodeName[attr.Name] ?? nodeName["android:"+attr.Name] : null;
                        editor.Attributes.Add(attr);
                    }
                    break;
                }
            }
        }

        public void StoreAttribute(IGKXAttributeEditor editor, string filename)
        {
            if (editor.RootNode == null)
                return;
            string tagName = editor.RootNode.TagName;
            var res = XmlElement.CreateXmlNode(tagName);
            ////res.AddChild(m_l);
            if (tagName !="resources")
                res["xmlns:android"] = AndroidConstant.ANDROID_NAMESPACE;
            //copy root attributes
            foreach (KeyValuePair<string, XmlAttributeValue> t in editor.RootNode.Attributes)
            {
                switch (t.Key)
                {
                    case "xmlns:android":
                    continue;
                }
                res["android:" + (t.Key.StartsWith ("android:")?
                    t.Key.Substring (8): t.Key )] = t.Value;
            }
            foreach (XmlElement t in editor.RootNode.Childs)
            {
                if (t == null)
                    continue;

                if (t.TagName == "style")
                {
                    var style = res.Add("style");
                    style["name"] = t.GetProperty("name");
                    style["parent"] = t.GetProperty("parent");
                    foreach (KeyValuePair<string, XmlAttributeValue> item in t.Attributes)
                    {

                        var v_i = style.Add("item");
                        v_i["name"] = "android:" + item.Key;
                        v_i.Content = item.Value.GetValue();
                    }
                }
                else
                {
                    res.LoadString(t.Render(null));
                }
            }
            IGK.DrSStudio.Android.AndroidResourceBuilder.Store(filename, res.Render(null));
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "Xml file | *.xml; *.axml";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    this.c_de.LoadFile(ofd.FileName);
                }
            }
        }

        private void addResourcesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.c_de.ClearNode();
            this.c_de.AddRootNode("resources");
        }

        private void quitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

       
    }
}
