using System;

namespace IGK.DrSStudio.Balafon.Xml
{
    public class BalafonProjectItemBase : BalafonCoreXmlElement,
        IBalafonProjectItem
    {
        IBalafonProjectItem IBalafonProjectItem.Parent
        {
            get;
            set;
        }
        public BalafonProjectItemBase(string tagname):base(tagname)
        {

        }
    }
}