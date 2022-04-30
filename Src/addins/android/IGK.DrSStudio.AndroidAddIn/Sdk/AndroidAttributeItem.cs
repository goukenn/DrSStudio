
using IGK.ICore;
using IGK.ICore.Xml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IGK.ICore.WinCore.WinUI.Controls;

namespace IGK.DrSStudio.Android.Sdk
{
    public class AndroidAttributeItem : IGKXAttributeEditor.AttributeItem 
    {
        public override string ToString()
        {
            return this.Name;
        }
        public override string DisplayText
        {
            get
            {
                return base.Name;
            }
        }
        private AndroidAttributeItem():base()
        {
        }
        /// <summary>
        /// create attribute item factory
        /// </summary>
        /// <param name="name"></param>
        /// <param name="format"></param>
        /// <param name="description"></param>
        /// <returns></returns>
        public static AndroidAttributeItem CreateAttributeItem(string name,
            string format,
            string category,
            string description)
        {
            AndroidAttributeItem c = new AndroidAttributeItem();

            
            c.Name = name;
            c.Type = format;
            c.Category = category;
            
            return c;
        }
        private bool m_IsAndroid;

        public bool IsAndroid
        {
            get { return m_IsAndroid; }
            set
            {
                if (m_IsAndroid != value)
                {
                    m_IsAndroid = value;
                }
            }
        }
        public override string Name
        {
            get
            {
                return this.IsAndroid? "android:"+base.Name : base.Name;
            }
            set
            {
                base.Name = value;
            }
        }
        public override string[] GetSupportedValues()
        {
            List<string> data = new List<string>();
            if (!string.IsNullOrEmpty(this.Type))
            {

                if (this.Type.ToLower().Contains("boolean"))
                    data.AddRange (new string[] { "true", "false" });
            }
            if (m_flagvalues != null)
                data.AddRange(m_flagvalues.ToArray());
            if (m_enumvalues != null)
                data.AddRange(m_enumvalues.ToArray());
            return data.ToArray();
        }
        List<string> m_flagvalues;
        List<string> m_enumvalues;
        private ICore.Xml.CoreXmlElement m_source;
        internal void AddFlag(string name, string value)
        {
            if (m_flagvalues == null)
                m_flagvalues = new List<string>();
            m_flagvalues.Add(name);
        }

        internal void AddEnum(string name, string value)
        {
            if (m_enumvalues == null)
                m_enumvalues = new List<string>();
            m_enumvalues.Add(name);
        }


        public override object Clone()
        {
            if (this.m_source != null) {
                this.m_source["format"] = this.Type;
                return CreateAttributeItem(this.m_source.Clone() as CoreXmlElement);
            }
            return base.Clone();
        }

        public  static AndroidAttributeItem CreateAttributeItem(CoreXmlElement b)
        {
            if (b == null)
                return null;
            string category = string.Empty;
             if( (b.Parent!=null) && (b.Parent.TagName == "declare-styleable"))
             {
                 category = b.Parent["name"];
             }
              var t = CreateAttributeItem(b["name"],
                            b["format"],
                            category ,
                            b["format"]);
              t.m_IsAndroid = true;
              t.m_source = b;

              var f = (b.getElementsByTagName("flag"));
              if ((f != null) && (f.Length > 0))
              {
                  foreach (var flag in f)
                  {
                      t.AddFlag(flag["name"], flag["value"]);
                  }
              }
              f = (b.getElementsByTagName("enum"));
              if ((f != null) && (f.Length > 0))
              {
                  foreach (var flag in f)
                  {
                      t.AddEnum(flag["name"], flag["value"]);
                  }
              }
              return t;
        }

        public void AddCategory(string v_category)
        {
            if (string.IsNullOrEmpty(this.Category))
                this.Category = v_category;
            else
                this.Category += "|" + v_category;
            this.m_source.SetProperty ("category", this.Category);
        }
    }
}
