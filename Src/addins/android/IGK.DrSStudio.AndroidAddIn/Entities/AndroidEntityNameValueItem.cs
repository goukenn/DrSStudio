
using IGK.ICore;
using IGK.ICore.Codec;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IGK.DrSStudio.Android.Entities
{
    public class AndroidEntityNameValueItem : ICoreSerializable
    {
        private string m_Name;
        private string m_Value;

        public string Value
        {
            get { return m_Value; }
            set
            {
                if (m_Value != value)
                {
                    m_Value = value;
                }
            }
        }
        public string Name
        {
            get { return m_Name; }
            set
            {
                if (m_Name != value)
                {
                    m_Name = value;
                }
            }
        }
        void ICoreSerializable.Serialize(IXMLSerializer seri)
        {
            this.Serialize(seri);
        }
        protected virtual  void Serialize(IXMLSerializer seri)
        {
            seri.WriteStartElement(AndroidConstant.ITEM_TAG);
            seri.WriteAttributeString("name", this.Name );
            seri.WriteValue(this.Value);
            seri.WriteEndElement();
        }
    }
}
