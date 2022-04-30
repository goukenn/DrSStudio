
using IGK.ICore;using IGK.ICore.Xml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.DrSStudio.Android.AndroidMenuBuilder
{
    class AndroidMenuBuilder
    {
        private CoreXmlElement m_node;

        public class AndroidMenuItem
        {
            private CoreXmlElement m_item; 

            internal  AndroidMenuItem(CoreXmlElement i)
            {
                this.m_item = i;
            }
        }
        public AndroidMenuBuilder()
        {
            m_node = CoreXmlElement.CreateXmlNode("menu");
        }
        public void BuildMenu(string filename) { 
            
            StringBuilder sb = new StringBuilder ();
            sb.Append(this.m_node.RenderXML(null));
            AndroidResourceBuilder.Store(filename, sb.ToString());
            
        }
        public AndroidMenuItem AddItem(string Name) {
            var i = m_node.Add("item");
            i["android:id"] = "@+id/"+Name;
            AndroidMenuItem ii = new AndroidMenuItem(i);
            return ii;
        }
    }
}
