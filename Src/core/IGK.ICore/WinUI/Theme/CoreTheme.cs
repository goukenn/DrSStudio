using IGK.ICore.IO;
using IGK.ICore.Settings;
using IGK.ICore.Xml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.ICore.WinUI.Theme
{

    /// <summary>
    /// represent a theme value.
    /// </summary>
    public class CoreTheme
    {
        private string m_Name;
        private CoreTheme m_Parent;
        private static CoreTheme sm_defaultTheme;
        static Dictionary<string, CoreTheme> sm_regTheme;

        private Dictionary<string, CoreRendererSetting> m_settings;
        static CoreTheme() {
            sm_regTheme = new Dictionary<string, CoreTheme>();
            sm_defaultTheme = new CoreTheme();
            //init default theme
            var f  = PathUtils.GetPath(Path.Combine (CoreApplicationSetting.Instance.SkinsFolder, "default.theme"));
            if (File.Exists(f))
            {
                sm_defaultTheme.__loadTheme(f);
            }
            else
            {
                foreach (KeyValuePair<string, ICoreRendererSetting> s in CoreRenderer.GetRendereringEnumerator())
                {
                    sm_defaultTheme.Add(s.Value as CoreRendererSetting );
                }
            }
            sm_regTheme .Add ("Default", sm_defaultTheme );
            //load themes
            var dir = PathUtils.GetPath(CoreApplicationSetting.Instance.SkinsFolder);
            if (Directory.Exists(dir))
            {
                foreach (string file in Directory.GetFiles (dir, "*.theme"))
                {
                    if (Path.GetFileNameWithoutExtension(file).ToLower() == "default")
                        continue;
                    CoreTheme th = CoreTheme.LoadTheme(file);
                    if (f != null)
                    {
                        if (!sm_regTheme.ContainsKey(th.Name))
                        {
                            sm_regTheme.Add (th.Name, th );
                        }
                    }
                }
            }
        }

        private void Add(CoreRendererSetting setting)
        {
            if ((setting == null) || this.m_settings.ContainsKey(setting.Name))
                return;
            this.m_settings.Add(setting.Name, setting);
        }

        public static CoreTheme DefaultTheme {
            get {
                return sm_defaultTheme;
            }
        }
        private  CoreTheme()
        {
            this.m_settings = new Dictionary<string, CoreRendererSetting>();
            this.m_Name = "Default";
        }

        /// <summary>
        /// get or set the theme parent
        /// </summary>
        public CoreTheme Parent
        {
            get { return m_Parent; }
            set
            {
                if (m_Parent != value)
                {
                    m_Parent = value;
                }
            }
        }
        /// <summary>
        /// get or set the name of the theme
        /// </summary>
        public string Name
        {
            get { return m_Name; }
            set
            {
                if (m_Name != value)
                {
                    m_Name = value;
                }
            }
        }

        public void save(string filename)
        {
            CoreXmlElement l = CoreXmlElement.CreateXmlNode("igk-theme");
            l["name"] = this.Name;
            foreach (var item in this.m_settings)
            {
                var b = l.Add("item");
                b["name"] = item.Value.Name;
                b.Content = item.Value.Value;

                b["format"] = item.Value.Type.ToString();
            }

            File.WriteAllText(filename, l.RenderXML(null));
        }

        public static CoreTheme LoadTheme(string filename)
        {
            if (File.Exists(filename) == false)
                return null;
            CoreTheme th = new CoreTheme();
            th.m_Name = Path.GetFileNameWithoutExtension(filename);
            th.m_Parent = DefaultTheme;
            th.__loadTheme(filename);
          
            return th;
        }

        private  void __loadTheme(string filename)
        {
             CoreXmlElement l = CoreXmlElement.LoadFile(filename);
            if (l.TagName == "igk-theme")
            {//good files
                var r = l.GetAttributeValue<string>("name", null);
                if (string.IsNullOrEmpty(r) == false) {
                    this.m_Parent = GetParent(r);
                }

                foreach (CoreXmlElement child in l.Childs)
                {
                    if (child == null) continue;

                    if (child.TagName == "item")
                    {
                        string n = child.GetAttributeValue<string>("name", null);
                        if (string.IsNullOrEmpty(n))
                        {
                            continue;
                        }
                        this.Add(new CoreRendererSetting(n,
                            child.GetAttributeValue<enuRendererSettingType>("format", 
                            enuRendererSettingType.String), child.Content));
                    }
                }
}
        }

        private static CoreTheme GetParent(string name)
        {
            if (sm_regTheme.ContainsKey(name))
            {
                return sm_regTheme[name];
            }
            return sm_defaultTheme;
        }

        /// <summary>
        /// load the current theme to data
        /// </summary>
        public void LoadTheme() {
            if (this == sm_defaultTheme)
            {
                //load the default theme
                this.__LoadTheme();
                return;
            }
            //load the parent theme
            if (this.Parent != null)
            {
                this.Parent.LoadTheme();
            }
            this.__LoadTheme();
            
        }

        private void __LoadTheme()
        {
            foreach (var item in this.m_settings)
            {
                CoreRenderer.SetValue (item.Key, item.Value.Value);            
            }
        }

        public static IEnumerable<CoreTheme> GetThemes()
        {
            return sm_regTheme.Values.ToArray();
        }
    }
}
