using IGK.ICore.Codec;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;
using System.Linq;
using System.Text;

namespace IGK.DrSStudio.WiXAddIn
{
    [TypeConverter(typeof(Converter))]
    public class WiXProjectVariables : ICoreDeserializable
    {
        private WiXProject wiXProject;
        private string m_WixUIBannerBmp;

        [Description("top banner : 493 × 58")]
        [Editor(typeof(BitmapFileEditor), typeof(UITypeEditor))]
        /// <summary>
        /// bitmap header
        /// </summary>
        public string WixUIBannerBmp
        {
            get { return m_WixUIBannerBmp; }
            set
            {
                if (m_WixUIBannerBmp != value)
                {
                    m_WixUIBannerBmp = value;
                }
            }
        }
        
        private string m_WixUIDialogBmp;

        [Description("Background bitmap used on the welcome and completion dialogsr : 493 × 312")]
        [Editor(typeof(BitmapFileEditor), typeof(UITypeEditor))]
        public string WixUIDialogBmp
        {
            get { return m_WixUIDialogBmp; }
            set
            {
                if (m_WixUIDialogBmp != value)
                {
                    m_WixUIDialogBmp = value;
                }
            }
        }
        public WiXProjectVariables(WiXProject wiXProject)
        {            
            this.wiXProject = wiXProject;
        }
        class Converter : TypeConverter
        {
            public override bool GetPropertiesSupported(ITypeDescriptorContext context)
            {
                return true;
            }
            public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributes)
            {
                List<PropertyDescriptor> pd = new List<PropertyDescriptor> ();
                return TypeDescriptor.GetProperties (value);
                //PropertyDescriptorCollection p = new PropertyDescriptorCollection (
                //return base.GetProperties(context, value, attributes);
            }
        }
        class BitmapFileEditor : System.Windows.Forms.Design.FileNameEditor
        { 
        }

        void ICoreDeserializable.Deserialize(IXMLDeserializer xreader)
        {
            while (xreader.Read())
            {
                switch (xreader.NodeType)
                {
                    case System.Xml.XmlNodeType.Element:
                        var p = this.GetType().GetProperty(xreader.Name);
                        if (p != null && p.CanWrite ) {
                            p.SetValue(this, xreader.ReadElementContentAsString());
                        }
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
