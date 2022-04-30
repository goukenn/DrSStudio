using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace IGK.DrSStudio.Balafon.Xml
{
    public class BalafonProjectItemFile : BalafonProjectItemBase  
    {
        public string Include
        {
            get { return this["Include"]; }
            set
            {
                this["Include"] = value;

            }
        }
        public BalafonProjectItemFile(string tagname):base(tagname)
        {

        }

        internal static BalafonProjectItemFile CreateFile(enuBalafonItemType mode, string filepath)
        {
            Type t = Type.GetType(string.Format("{0}.BafafonProject{1}Item",
               MethodInfo.GetCurrentMethod().DeclaringType.Namespace,
               mode.ToString()), false, true);
            BalafonProjectItemFile g = null;
            if ((t != null) && !t.IsAbstract)
                g = (BalafonProjectItemFile)t.Assembly.CreateInstance(t.FullName);
            else
                g = new BalafonProjectItemFile(mode.ToString());
            g.Include = filepath;
            return g;
        }
    }
}
