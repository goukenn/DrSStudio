using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IGK.DrSStudio.Balafon.Xml
{
    public class BalafonProjecPropertyGroup : BalafonCoreXmlElement
    {
        private BalafonProject balafonProject;

        internal BalafonProjecPropertyGroup(BalafonProject balafonProject):
            base(BalafonConstant.PROJECT_PROPERTY_GROUP_TAG)
        {
            this.balafonProject = balafonProject;
        }

        public string AppLogin { get { return this.GetElementProperty("AppLogin"); } set { this.SetElementProperty("AppLogin", value ); } }
        public string AppLoginPwd { get { return this.GetElementProperty("AppLoginPwd"); } set { this.SetElementProperty("AppLoginPwd", value); } }
    }
}
