
using IGK.ICore;using IGK.DrSStudio.Android.Settings;
using IGK.ICore.IO;
using IGK.ICore.Xml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.DrSStudio.Android.Entities
{
    public class AndroidPlatformEntitiesAttribute : AndroidEntities 
    {
        private Dictionary<string, CoreXmlElementBase> m_items;
        public AndroidPlatformEntitiesAttribute()
        {
            this.m_items = new Dictionary<string, CoreXmlElementBase>();
        }
        public override string ToString()
        {
            return this.Name + "[" + this.m_AttributeType + ":"+m_items.Count+"]";
        }
        public override string EntityTypeName
        {
            get
            {
                return AndroidConstant.ANDROID_ATTRIBUTES;
            }
        }
        private string m_Name;
        private enuAndroidAttributeType m_AttributeType;

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
        public static AndroidPlatformEntitiesAttribute[] GetAttributes(string plateform)
        {
            string f = PathUtils.GetPath(string.Format(AndroidSetting.Instance.PlatformSDK + "/platforms/{0}/data/res/values/attrs.xml",
          plateform));

            List<AndroidPlatformEntitiesAttribute> v_tattr = new List<AndroidPlatformEntitiesAttribute> ();
            if (File.Exists(f))
            {
                CoreXmlElement x = CoreXmlElement.CreateXmlNode("dummy");
                x.LoadString(File.ReadAllText(f));
                //get declaring stylable
                foreach (var item in x.getElementsByTagName("declare-styleable"))
                {
                    AndroidPlatformEntitiesAttribute attr = new AndroidPlatformEntitiesAttribute();
                    attr.Name = item["name"];
                    v_tattr.Add(attr);
                    attr.Load(item);
                }
                List<string> v_lattr = new List<string>();
                //get attributes 
                foreach (var item in x.getElementsByTagName("attr"))
                {
                   //attribute supported value
                    Console.WriteLine("attr" + item["name"]);
                    v_lattr.Add(item["name"]);
                }
            }
            return v_tattr.ToArray();
        }
        public string[] GetAttributeNames() {
            return this.m_items.Keys.ToArray<string>();
        }
        public string GetAttributeFormat(string name){
            if (this.m_items.ContainsKey(name))
                return this.m_items[name]["format"];
            return null;
        }
        public string[] GetSupportedValue(string name)
        {
            if (!this.m_items.ContainsKey(name))
                return null;
            List<string> m_tlist = new List<string>();

            CoreXmlElement l = this.m_items[name] as CoreXmlElement;
            foreach (CoreXmlElement i in l.Childs)
            {
                m_tlist.Add(i["name"]);
            }

            return m_tlist.ToArray ();
        }
        private void Load(CoreXmlElementBase item)
        {
            var s = item.getElementsByTagName("attr");
            if (s.Length > 0)
            {
                this.m_AttributeType = enuAndroidAttributeType.Attribute;
                foreach (var i in s)
                {
                    this.m_items.Add(i["name"], i);

                    new AndroidAttributeItem()
                    {
                        Name = i["name"],
                        Format = i["format"]
                    };
                }
            }

        }

        class AndroidAttributeItem
        {
            private string m_Name;
            private string m_Format;

            public string Format
            {
                get { return m_Format; }
                set
                {
                    if (m_Format != value)
                    {
                        m_Format = value;
                    }
                }
            }
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
        }
    }
}
