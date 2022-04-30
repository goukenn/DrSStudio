using IGK.ICore.Xml;
using IGK.ICore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IGK.DrSStudio.Android.Xamarin.Xml
{
    public class XamarinProjectReferenceItemFile : XamarinProjectItemFile, IXamarinProjectFile, IXamarinProjectItemGroupChild
    {
        private CoreXmlElement m_HintPath;

        public String HintPath
        {
            get { return m_HintPath != null ?
                (m_HintPath.Content ==null? string.Empty:  m_HintPath.Content.ToString ()) : 
                string.Empty; }
            set
            {
                if (m_HintPath != null)
                {
                    if (value == null)
                        this.RemoveChild(this.m_HintPath);
                    else
                        this.m_HintPath.Content = value;                    
                }
                else if (value != null)
                {
                    this.m_HintPath = this.Add("HintPath");
                    this.m_HintPath.Content = value;
                }
            }
        }
        public XamarinProjectReferenceItemFile()
            : base(enuXamarinBuildMode.Reference.ToString())
        {

        }
       
    }
}
