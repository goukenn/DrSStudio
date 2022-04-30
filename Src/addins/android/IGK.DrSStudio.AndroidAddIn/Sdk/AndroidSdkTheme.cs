using IGK.ICore.Xml;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.DrSStudio.Android.Sdk
{
    public class AndroidSdkTheme : AndroidSdkItemBase
    {
        Dictionary<string, string> attrdef;
        private string m_parent;

        public AndroidSdkTheme(string name, AndroidTargetInfo target):base(target)
        {
            this.Name = name;
            this.attrdef = new Dictionary<string, string>();
        }

        internal void loadAttributes(CoreXmlElementBase i)
        {
            this.m_parent = i["parent"];
            foreach (var h in i.getElementsByTagName("item"))
            {
                if (attrdef .ContainsKey (h["name"]))                    
                    attrdef[h["name"]] = h.Value;   
                else
                    attrdef.Add (h["name"], h.Value);   
            }
        }

        internal IEnumerable GetProperties()
        {
            return this.attrdef;
        }

        public string Parent { get { return this.m_parent; } }
    }
}
