using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IGK.ICore.Resources
{
    public class FileResourceItem : CoreResourceItemBase 
    {
        private string m_FileName;

        public override enuCoreResourceType ResourceType
        {
            get { return enuCoreResourceType.File; }
        }
        public string FileName
        {
            get { return m_FileName; }
        }
        protected internal override void SetValue(string readvalue)
        {
            this.m_FileName = readvalue;
        }
        public override string GetDefinition()
        {
            return this.FileName;
        }
        public override object GetData()
        {
            return this.FileName;
        }
    }
}
