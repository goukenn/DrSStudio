using IGK.ICore.Drawing2D;
using IGK.ICore.Xml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IGK.DrSStudio.Android.Resources
{
    /// <summary>
    /// represent a base android menu items
    /// </summary>
    public abstract class AndroidResourceItemBase : CoreXmlElement
    {
        public const string ATTRIBUTENAME = "android:AttrName";

        public GroupElement Host { get; set; }

        public AndroidResourceItemBase(string tagname):base(tagname )
        {
        }

        /// <summary>
        /// define your autorisation namespace definition. 
        /// </summary>
        /// <example>
        /// return type xmlns:android="..."
        /// </example>
        /// <returns></returns>
        protected override string getLoadStringNamespace()
        {
            return string.Format ("{0} {1} ",base.getLoadStringNamespace(),
                string.Format ("xmlns:android=\"{0}\"", AndroidConstant.NAME_SPACE)
                );
        }
        public override string ToString()
        {
            return string.Format("{0}[id:{1}]", this.TagName, GetAttributeValue<string>("android:id"));
        }

        public override CoreXmlElement CreateChildNode(string tagName)
        {
            Type t = Type.GetType(string.Format("{0}.Android{1}Resource",
                this.GetType().Namespace,
                tagName), false, true);
            if (t!=null)
            {
                return t.Assembly.CreateInstance (t.FullName ) as AndroidResourceItemBase;
            }
            return base.CreateChildNode(tagName);
        }
    }
}
