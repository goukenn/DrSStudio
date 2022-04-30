using IGK.ICore.WinCore;
using IGK.ICore;using IGK.ICore.Codec;
using IGK.ICore.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IGK.DrSStudio.Menu.Insert
{
    class RSCAudioResourceItem : CoreResourceItemBase, ICoreResourceItem
    {
        private string m_fileName;

        public string FileName { get { return this.m_fileName; } }

        public RSCAudioResourceItem(string filename)
        {
            this.m_fileName = filename;
        }
        public override enuCoreResourceType ResourceType
        {
            get {
                return enuCoreResourceType.SoundFile;
                }
        }

        public override object GetData()
        {
            return null;
        }

        public override string GetDefinition()
        {
            return this.FileName;
        }

        protected  override void SetValue(string value)
        {
            this.m_fileName = value;
        }
        protected override void Serialize(IXMLSerializer xwriter)
        {
            xwriter.WriteStartElement(this.ResourceType.ToString());
            xwriter.WriteAttributeString("Id", this.Id);
            xwriter.WriteValue(this.FileName);
            xwriter.WriteEndElement();
        }
    }
}
