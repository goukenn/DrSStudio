
using IGK.ICore;using IGK.ICore.Xml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.DrSStudio.Android.AndroidMenuBuilder
{
    /// <summary>
    /// android menu
    /// </summary>
    public class AndroidMenu : CoreXmlElement 
    {
        internal AndroidMenu():base("menu")
        {            
        }
        internal static AndroidMenu Load(CoreXmlElementBase element)
        {
            CoreXmlElement e=  element as CoreXmlElement ;
            AndroidMenu v_b = null;
            v_b = new AndroidMenu();
            //copy definition
            v_b.CopyAttributes(e);
            
            return v_b;
        }

        protected override void CopyAttributes(CoreXmlElement element)
        {
            if ((element ==null) && !element.HasAttributes)
                return;

            foreach (CoreXmlAttributeValue  item in element.Attributes)
            {
             //   this[item] = item.GetValue();
            }
        }
    }
}
