
using IGK.DrSStudio.Android.Entities;
using IGK.ICore;
using IGK.ICore.Codec;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.DrSStudio.Android.AndroidThemeBuilder
{
    public class ATBTheme : ICoreSerializable
    {

        private string m_Name; //name of this theme
        private string m_Parent;//parent . by default theme if not Theme
        private string m_DeclrationFile; //store where the theme is declared

        //public class AndroidThemeItem : AndroidEntityNameValueItem
        //{
        //}
             

        public ATBThemeFile File { get;internal set; }
        public string DeclarationFile {
            get { return m_DeclrationFile; }
            internal set {
                m_DeclrationFile = value;
            }
        }

        /// <summary>
        /// get or set the parent name
        /// </summary>
        public string Parent
        {
            get { return m_Parent; }
            set
            {
                if (m_Parent != value)
                {
                    m_Parent = value;
                }
            }
        }
        /// <summary>
        /// get or set the name
        /// </summary>
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

        

        /// <summary>
        /// .ctr
        /// </summary>
        public ATBTheme(){
            this.m_DeclrationFile = string.Empty ;
        }
    

        public void Serialize(IXMLSerializer seri)
        {
            seri.WriteStartElement(AndroidConstant.STYLE_TAG);
            seri.WriteAttributeString ("name",this.Name );
            if (!string.IsNullOrEmpty (this.Parent))
            seri.WriteAttributeString ("parent", $"@android:style/{this.Parent}" );
            //foreach (ICoreSerializable item in this.m_items)
            //{
            //    if (item == null) 
            //        continue;
            //    item.Serialize(seri);
            //}
            seri.WriteComment("primary styles");
            foreach (var item in this.m_properties)
            {
                
                AndroidThemeDefine d = item.Value;

                //if (d.Name == "actionBarPopupTheme")
                //{

                //}
                if (d.Value != d.Default) {//value changed
                    seri.WriteStartElement ("item");
                    seri.WriteAttributeString ("name",  $"android:{d.Name}");
                    seri.WriteValue(d.Value);

                    seri.WriteEndElement ();
                }
            }
            seri.WriteComment("Custom style properties");
            seri.WriteEndElement();
        }
       private readonly Dictionary<string, AndroidThemeDefine> m_properties = new Dictionary<string, AndroidThemeDefine> ();

        internal void SetValue(string id, string value)
        {
            if (this.m_properties.ContainsKey(id)) {
                AndroidThemeDefine g = this.m_properties[id];
                g.Value = value;
                this.m_properties[id] = g;
            }
            else {
                AndroidThemeDefine g = new AndroidThemeDefine()
                {
                    Name = id,
                    Default = null,
                    Value = value
                };
                this.m_properties.Add(id, g);
            }
        }
        internal string GetValue(string key)
        {
            if (this.m_properties.ContainsKey(key)) {
                return m_properties [key].Value;
            }
            return null;
        }
        internal bool IsValueChanged(string key)
        {
            if (this.m_properties.ContainsKey(key))
            {
                return m_properties[key].Value != m_properties[key].Default;
            }
            return false;
        }
        /// <summary>
        /// used to load global theme
        /// </summary>
        /// <param name="value"></param>
        internal void Load(AndroidTheme.AndroidThemeValue value)
        {
            this.m_properties[value.Name] = new AndroidThemeDefine() {
                Name = value.Name ,
                Default = value.DefaultValue ,
                Value = value.Value 
            };
        }
   

        struct AndroidThemeDefine
        {
            public string Default { get; internal set; }
            public string Name { get; internal set; }
            public string Value { get; internal set; }
        }
       
        /// <summary>
        /// get enumeration of keys
        /// </summary>
        /// <returns></returns>
        internal IEnumerable<string> GetKeys()
        {
            return this.m_properties.Keys.AsEnumerable<string>();
        }
    }
}
