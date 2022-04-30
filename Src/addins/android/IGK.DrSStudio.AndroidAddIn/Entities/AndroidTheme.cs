
using IGK.DrSStudio.Android.Settings;
using IGK.ICore;
using IGK.ICore.IO;
using IGK.ICore.Xml;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.DrSStudio.Android.Entities
{
    /// <summary>
    /// represent an android theme entities
    /// </summary>
    public class AndroidTheme : AndroidEntities
    {
        public class AndroidThemeCollections :IEnumerable {
            private Dictionary<string, AndroidTheme> m_list;
            public AndroidTheme this[string name] {
                get {
                    if (!string.IsNullOrEmpty (name) && this.m_list.ContainsKey(name)) {
                        return this.m_list[name];
                    }
                    return null;
                }
            }
            internal AndroidThemeCollections()
            {
                this.m_list = new Dictionary < string, AndroidTheme > ();
            }
            public int Count=> m_list.Count;
            public  IEnumerator GetEnumerator()
            {
               return this.m_list.Values.OrderBy(i=>i.Name).GetEnumerator();
            }
            internal void Add(AndroidTheme theme) {
                this.m_list.Add (theme.Name, theme);
            }
            internal void Clear() =>  m_list.Clear();
            
        }


        private AndroidTheme m_Parent;
        private String m_Name;
        private AndroidThemeValuesCollections m_values;

        public override string ToString()
        {
            return this.m_Name;
        }
        /// <summary>
        /// get the android namde
        /// </summary>
        public String Name
        {
            get { return m_Name; }
            internal set
            {
                if (m_Name != value)
                {
                    m_Name = value;
                }
            }
        }
        /// <summary>
        /// Get the theme parent
        /// </summary>
        public AndroidTheme Parent
        {
            get { return m_Parent; }
            internal set {
                this.m_Parent = value;
            }
        }
        public override string EntityTypeName
        {
            get
            {
                return AndroidConstant.ANDROID_THEMES;
            }
        }
        /// <summary>
        /// set the properties
        /// </summary>
        /// <param name="name"></param>
        /// <param name="p"></param>
        public void SetProperty(string name, string p, string initial = null)
        {
            if (this.m_values.Contains(name))
            {
                this.m_values[name].Value = p;
            }
            else {
                this.m_values.Add(name, p, initial);
            }

        }
        /// <summary>
        /// get values
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public string GetValue(string name) {
            if (this.m_values.Contains(name))
            {
                return this.m_values[name].Value;
            }
            if (this.m_Parent != null)
                return this.m_Parent.GetValue(name);
            return null;
        }
        public AndroidTheme()
        {
            this.m_values = new AndroidThemeValuesCollections(this);
        }
        internal static AndroidTheme Create(CoreXmlElement i)
        {
            AndroidTheme v_theme = null;

            return v_theme;
        }

        /// <summary>
        /// retrieve all android theme attribute definitions
        /// </summary>
        /// <param name="plateFormsTarget"></param>
        /// <returns></returns>
        public static AndroidThemeAttributeCollections GetAndroidThemeAttrs(string plateFormsTarget) {

            
            //get public files 
            string pfile = PathUtils.GetPath(string.Format(AndroidSetting.Instance.PlatformSDK + "/platforms/{0}/data/res/values/public.xml",
               plateFormsTarget));
            CoreLog.WriteDebug("Loading public attribute : "+pfile);
            var pg = CoreXmlElement.LoadFile(pfile);
            if (pg == null)
            {
                return null;
            }
            Dictionary<string, int> pattrs = new Dictionary<string, int>();
            pg.Select("*").EachAll((i) => {
                if (i["type"] == "attr") {

                    string d = i["id"];
                    
                    pattrs.Add(i["name"], 0);

                }
            });


            string file = PathUtils.GetPath(string.Format(AndroidSetting.Instance.PlatformSDK + "/platforms/{0}/data/res/values/attrs.xml",
               plateFormsTarget));

            AndroidThemeAttributeCollections c = new AndroidThemeAttributeCollections();
            var g = CoreXmlElement.LoadFile(file);

            //get all editable attribute from the declared file string
            g.Select("*").EachAll((o) => {
                if ((o.TagName == "attr") && 
                     ((o["format"]?.Value !=null)||
                     (o.Childs.Count > 0)
                    ))
                {
                                        if (pattrs.ContainsKey(o["name"]))
                    {
                        c.Add(AndroidThemeAttribute.Load(o));
                    }
                    else {
                        CoreLog.WriteLine($"{o["name"]} is not a public member");
                    }
                }
            });


            //CoreXmlElement theme = null;
            //foreach (var i in g.getElementsByTagName("declare-styleable"))
            //{
            //    if (i["name"] == "Theme")
            //    {
            //        theme = i as CoreXmlElement;
            //        theme.Select(">>").eachAll((ii) =>
            //        {
            //            // theme = i as CoreXmlElement;
            //            if (ii.TagName == "attr")
            //                c.Add(AndroidThemeAttribute.Load(ii));
            //        });
            //    }
            //}
            //List<CoreXmlElement> v_glist = new List<CoreXmlElement>();
            //g.Select(">>").eachAll((o) => {                
            //    if (o.TagName == "attr")
            //    {
            //        c.Add(AndroidThemeAttribute.Load(o));                    
            //    }
            //});            
            return c;
        }


        /// <summary>
        /// attribute infor
        /// </summary>
        public class AndroidThemeAttribute {
            private string m_name;
            private string m_type;
            private AndroidEnumListCollection m_enumList;
            private bool m_isglobal;
            private CoreXmlAttributeValue m_groupKey;

            internal AndroidThemeAttribute()
            {

            }

            public string Name => m_name;
            public bool IsGlobal => m_isglobal;

            public string Type
            {
                get
                {
                    if (!string.IsNullOrEmpty (m_type))
                        return m_type;
                    if (this.m_enumList.Count > 0) {
                        return this.m_enumList.m_flags ? "flag":"enum";
                    }
                    return "unknow";
                }
            }

            public string GroupKey => m_groupKey;

            public override string ToString()
            {
                return $"{m_name},{Type}";
            }
            class AndroidEnumListCollection
            {
                internal bool m_flags;
                Dictionary<string, string> m_dics = new Dictionary<string, string>();

                public int Count => m_dics.Count;


                ///<summary>
                ///public .ctr
                ///</summary>
                public AndroidEnumListCollection()
                {
                }
                internal void Add(string name, string value)
                {
                    this.m_dics.Add(name, value);
                }

                internal string[] Keys()
                {
                    return this.m_dics.Keys.ToArray();
                }
            }

            internal static AndroidThemeAttribute Load(CoreXmlElement theme)
            {
                if (theme.TagName !="attr")
                    return null;
                AndroidThemeAttribute c = new AndroidThemeAttribute()
                {
                    m_name = theme["name"],
                    m_type = theme["format"],
                    m_isglobal = theme.Parent?.TagName == "resources",
                    
                };
                if (theme.Parent?.TagName == "declare-styleable")
                {
                    var m_groupKey = theme.Parent["name"];

                    c.m_groupKey = m_groupKey;

                }

                if (c.m_type == null) {
                    if (c.m_enumList == null)
                    {
                        c.m_enumList = new AndroidEnumListCollection();
                    }
                    theme.Select(">>").EachAll((i) => {
                        if (i.TagName == "enum")
                        {
                            c.m_enumList.Add(i["name"], i["value"]);
                        }
                        else if (i.TagName =="flag") {
                            c.m_enumList.m_flags = true;
                            c.m_enumList.Add (i["name"], i["value"]);
                        }

                    });
                }                
                return c;
            }
            /// <summary>
            /// get enum values
            /// </summary>
            /// <returns></returns>
            public string[]  GetEnumValues()
            {
                return this.m_enumList.Keys();
            }
        }

        /// <summary>
        /// android theme collections
        /// </summary>
        public class AndroidThemeAttributeCollections : IEnumerable {
            private List<AndroidThemeAttribute> m_list;
            private Dictionary<string, AndroidThemeAttribute> m_dics;
            private List<AndroidThemeAttribute> m_globals;
            private Dictionary<string, List<AndroidThemeAttribute>> m_groups = new Dictionary<string, List<AndroidThemeAttribute>>();

            ///<summary>
            ///public .ctr
            ///</summary>
            public AndroidThemeAttributeCollections()
            {
                this.m_list = new List<AndroidThemeAttribute>();
                this.m_dics = new Dictionary<string, AndroidThemeAttribute>();
                this.m_globals  = new List<AndroidThemeAttribute>();
            }

            public IEnumerator GetEnumerator()
            {
                return this.m_list.GetEnumerator();
            }

            internal void Add(AndroidThemeAttribute p)
            {
                if (p == null)
                    return;
                m_dics.Add(p.Name, p);
                this.m_list.Add(p);
                if (p.IsGlobal)
                {
                    m_globals.Add(p);
                }
                else if (!string.IsNullOrEmpty(p.GroupKey)) {
                    if (!this.m_groups.ContainsKey(p.GroupKey))
                        this.m_groups.Add(p.GroupKey, new List<AndroidThemeAttribute>());
                    this.m_groups[p.GroupKey].Add(p);
                }
            }

            /// <summary>
            /// get global properties definitions
            /// </summary>
            /// <returns></returns>
            public AndroidThemeAttribute[] GetGlobals()
            {
                return this.m_globals.ToArray();
            }

            /// <summary>
            /// return groups attribute
            /// </summary>
            /// <param name="key"></param>
            /// <returns></returns>
            public AndroidThemeAttribute[] GetGroups(string key)
            {
                if (this.m_groups.ContainsKey(key))
                    return this.m_groups[key].ToArray();
                return null;
            }

            public int Count => m_list.Count;
            public AndroidThemeAttribute this[int index] => m_list[index];
            public AndroidThemeAttribute this[string name] => m_dics.ContainsKey(name)?m_dics[name]:null;
        }

        /// <summary>
        /// retrieve android available theme
        /// </summary>
        /// <param name="plateFormsTarget"></param>
        /// <returns></returns>
        public static AndroidThemeCollections GetAndroidThemes(string plateFormsTarget)
        {

            string pf = PathUtils.GetPath(string.Format(AndroidSetting.Instance.PlatformSDK + "/platforms/{0}/data/res/values/public.xml",
                plateFormsTarget));


            if (!File.Exists(pf))
                return null;
            Dictionary<string, int> pattr = new Dictionary<string, int>();
            CoreXmlElement.LoadFile(pf).Select("public").EachAll((i) =>
            {
                if (i["type"] == "style") {

                    pattr.Add(i["name"], 0);
                }
            });




            AndroidThemeCollections v_themes = new AndroidThemeCollections();
            Dictionary<string, AndroidTheme> v_kthemes = new Dictionary<string, AndroidTheme>();

            string[] v_ftab = System.IO.Directory.GetFiles(PathUtils.GetPath(
                string.Format(AndroidSetting.Instance.PlatformSDK + "/platforms/{0}/data/res/values/",
                plateFormsTarget
                )), "*.xml");
            


            //string[] v_ftab = {
            //    "themes" ,
            //    "styles",
            //    "styles_material",
            //    "styles_micro",
            //    "theme_micro"
            //};

            CoreMethodInvoker del = null;
            for (int i = 0; i < v_ftab.Length; i++)
            {

                string f = v_ftab[i];
                    //PathUtils.GetPath(string.Format(AndroidSetting.Instance.PlatformSDK + "/platforms/{0}/data/res/values/"+v_ftab[i]+".xml",
                    //plateFormsTarget));
          
                if (File.Exists(f))
                {
                    //load file 
                    IGK.ICore.Xml.CoreXmlElement t = ICore.Xml.CoreXmlElement.CreateXmlNode("dummy");
                    t.LoadString(File.ReadAllText(f));

                    var styles = t.getElementsByTagName("style");
                    AndroidTheme v_theme = null;
                    AndroidTheme v_parent = null;
                    string v_name = null;
                    foreach (var item in styles)
                    {
                        v_name = item["name"];
                        if (!pattr.ContainsKey(v_name) || v_kthemes.ContainsKey(v_name))
                            continue;


                        v_parent = GetParent(item["parent"], v_kthemes);
                        v_theme = new AndroidTheme();
                        v_theme.Name = v_name;
                        v_theme.Parent = v_parent;
                        v_themes.Add(v_theme);

                        LoadSysThemeProperties(v_theme, item as IGK.ICore.Xml.CoreXmlElement);

                        if ((v_parent == null) && (v_name != "Theme"))
                        {
                            del += () =>
                            {
                                v_theme.Parent = GetParent(item["parent"], v_kthemes);
                            };
                        }

                        v_kthemes.Add(v_name, v_theme);
                    }
                }
            }
            if (del != null)
                del.Invoke();
            return v_themes;
        }
        /// <summary>
        /// get the theme in this theme list
        /// </summary>
        /// <param name="themes"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        internal static AndroidTheme GetAndroidTheme(AndroidTheme[] themes, string name)
        {
            foreach (var item in themes)
            {
                if (item.Name == name) {
                    return item;
                }
            }
            return null;
        }

        /// <summary>
        /// load theme properties
        /// </summary>
        /// <param name="v_theme">Theme to load</param>
        /// <param name="xmlElement">Node</param>
        private static void LoadThemeProperties(AndroidTheme v_theme, ICore.Xml.CoreXmlElement xmlElement)
        {
            if ((xmlElement == null) || (!xmlElement.HasChild))
                return;
            foreach (var c in xmlElement.getElementsByTagName("item"))
            {
                v_theme.SetProperty(c["name"].Value.ToString(), c.Value);
            }
        }

        /// <summary>
        /// load theme properties
        /// </summary>
        /// <param name="v_theme">Theme to load</param>
        /// <param name="xmlElement">Node</param>
        private static void LoadSysThemeProperties(AndroidTheme v_theme, ICore.Xml.CoreXmlElement xmlElement)
        {
            if ((xmlElement == null) || (!xmlElement.HasChild))
                return;
            foreach (var c in xmlElement.getElementsByTagName("item"))
            {
                v_theme.SetProperty(c["name"].Value.ToString(), GetAndroidValue( c.Value));
            }
        }

        private static string GetAndroidValue(string value)
        {
            if (value?.Length > 0) {
                if (value == "@null")
                    return value;
                if (value[0] == '@') {
                    value = "@android:" + value.Substring(1);
                }
            }
            return value;
        }

        private static AndroidTheme GetParent(CoreXmlAttributeValue xmlAttributeValue, Dictionary<string, AndroidTheme> v_kthemes)
        {
            if ((xmlAttributeValue == null) || (xmlAttributeValue.Value == null))
            {
                if (v_kthemes.ContainsKey("Theme"))
                {
                    return v_kthemes["Theme"];
                }
                else
                {
                    return null;
                }
            }
            string v = xmlAttributeValue.Value.ToString();
            if (v_kthemes.ContainsKey(v))
                return v_kthemes[v];
            return null;
        }
        public AndroidThemeValuesCollections Values =>  m_values;
        
        public class AndroidThemeValuesCollections : IEnumerable
        {
            private Dictionary<string, AndroidThemeValue> m_values;
            private AndroidTheme m_owner;
            public AndroidThemeValue this[string key] {
                get {
                    if (this.m_values.ContainsKey(key))
                        return this.m_values[key];
                    return null;
                }
                set { 
                }
            }
            
            public AndroidThemeValuesCollections(AndroidTheme theme)
            {
                this.m_owner = theme;
                this.m_values = new Dictionary<string, AndroidThemeValue>();
            }

            internal bool Contains(string name)
            {
                return !string.IsNullOrEmpty (name ) && this.m_values.ContainsKey(name);
            }
            /// <summary>
            /// get a key values Android Theme value enumerator
            /// </summary>
            /// <returns></returns>
            public IEnumerator GetEnumerator()
            {
                return this.m_values.GetEnumerator();
            }

            internal void Add(string name, string p, string @default=null)
            {
                this.m_values.Add(name, new AndroidThemeValue() {
                    Name = name ,
                    Value = p,
                    DefaultValue= @default ?? p
                });
            }
        }

        /// <summary>
        /// represent android theme value
        /// </summary>
        public class AndroidThemeValue 
        {
            public string Value { get;set;}
            public string Name { get;set;}
            public string Type { get;set;}
            public string DefaultValue { get;set;}

        }


       
    }
}
