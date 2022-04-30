using IGK.ICore.Xml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IGK.DrSStudio.Android.Xamarin.Xml
{
    /// <summary>
    /// samaring property element
    /// </summary>
    public sealed class XamarinSingleValueXmlElement : CoreXmlElement
    {
        public XamarinSingleValueXmlElement(string name):base(name )
        {
        }
        public override string ToString()
        {
            return string.Format("XamarinSingleValueXmlElement[{0}:{1}]", this.TagName, this.Value);
        }
    }
}
