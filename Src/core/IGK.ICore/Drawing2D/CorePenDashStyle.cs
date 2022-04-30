

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: CorePenDashStyle.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
/*
IGKDEV @ 2008-2016
author: C.A.D . BONDJE DOUE
file:CorePenDashStyle.cs
*/
using IGK.ICore;
using IGK.ICore.Settings;
using IGK.ICore.WinUI;
using IGK.ICore.WinUI.Configuration;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace IGK.ICore.Drawing2D
{
    public class CorePenDashStyle : 
        ICoreLineStyle , 
        ICoreWorkingConfigurableObject ,
        ICoreWorkingDefinitionObject
    {

        class CorePenDashTypeConverter : TypeConverter {
            public override object ConvertTo(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, Type destinationType)
            {
                if (destinationType == typeof(string))
                {
                    return GetUnitToString (value as CoreUnit[]);
                }
                return base.ConvertTo(context, culture, value, destinationType);
            }
            public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
            {
                if (destinationType == typeof(string))
                    return true;
                return base.CanConvertTo(context, destinationType);
            }
            public string GetUnitToString(CoreUnit[] unit)
            {
                StringBuilder sb = new StringBuilder();
                bool c = false;
                foreach (CoreUnit item in unit)
                {
                    if (c) sb.Append(";");
                    sb.Append(item.ToString());
                    c = true;
                }
                return sb.ToString();
            }
        }
        private string m_Name;
        private CoreUnit[] m_Units;
        private enuDashStyle m_Style;
        private float m_dashOffset;
        /// <summary>
        /// Get the style of the pen
        /// </summary>
        public enuDashStyle Style
        {
            get { return m_Style; }
        }

        [System.ComponentModel.TypeConverter (typeof(CorePenDashTypeConverter))]
        /// <summary>
        /// get the units
        /// </summary>
        public CoreUnit[] Units
        {
            get { return m_Units; }
            set { m_Units = value; }
        }
        static CorePenDashStyle()
        {
            LoadStyles();
        }
        #region ICoreWorkingConfigurableObject Members
        public enuParamConfigType GetConfigType()
        {
            return enuParamConfigType.ParameterConfig;
        }
        public ICoreParameterConfigCollections GetParameters(ICoreParameterConfigCollections parameters)
        {
            string group = "Default";
            parameters.AddItem("DisplayName", "lb.Name.caption", group, enuParameterType.Text, unitChanged);
            parameters.AddItem(GetType().GetProperty("DashOffset"), "lb.offset.caption", group);
            parameters.AddItem("Units", "lb.units.caption", group, enuParameterType.Text, unitChanged);
            return parameters;
        }
        public string GetUnitToString()
        {
            StringBuilder sb = new StringBuilder();
            bool c = false;
            foreach (CoreUnit item in this.Units)
            {
                if (c) sb.Append(";");
                sb.Append(item.ToString());
                c = true;
            }
            return sb.ToString();
        }
        void unitChanged(object sender, IGK.ICore.WinUI.Configuration.CoreParameterChangedEventArgs e)
        {
            switch (e.Item.Name)
            {
                case "Units":
                    string[] str = e.Value.ToString().Split(';');
                    List<CoreUnit> unit = new List<CoreUnit>();
                    CoreUnit c = null;
                    foreach (string item in str)
                    {
                        c = item;
                        if (c != null)
                            unit.Add(c);
                    }
                    this.m_Units = unit.ToArray();
                    break;
                default:
                    if (e.Value.ToString().Length > 0)
                    {
                        this.m_Name = e.Value.ToString();
                    }
                    break;
            }
        }
        public ICoreControl GetConfigControl()
        {
            throw new NotImplementedException();
        }
        #endregion
        #region ICoreIdentifier Members
        public string Id
        {
            get { return this.m_Name; }
        }
        #endregion
        /// <summary>
        /// create a new line style
        /// </summary>
        /// <param name="dashStyle"></param>
        /// <returns></returns>
        public static ICoreLineStyle GetLineStyle(enuDashStyle dashStyle)
        {
            if (dashStyle == enuDashStyle.Custom)
                return null;
            CorePenDashStyle c = new CorePenDashStyle();
            c.m_Name = dashStyle.ToString();
            c.m_Style = dashStyle;
            return c;
        }
        /// <summary>
        /// Create a new Line style
        /// </summary>
        /// <param name="name"></param>
        /// <param name="units"></param>
        /// <returns></returns>
        public static ICoreLineStyle GetLineStyle(string name, CoreUnit[] units)
        {
            CorePenDashStyle c = new CorePenDashStyle();
            c.m_Name = name;
            c.m_Units = units;
            c.m_Style = enuDashStyle.Custom;
            return c;
        }
        static Dictionary<string, CorePenDashStyle> sm_PensLineStyle = new Dictionary<string, CorePenDashStyle>();
        public static CorePenDashStyle[] LoadStyles()
        {
            if (sm_PensLineStyle.Count > 0)
            {
                return sm_PensLineStyle.Values.ToArray<CorePenDashStyle>();
            }
            List<CorePenDashStyle> v_m = new List<CorePenDashStyle>();
            foreach (enuDashStyle item in Enum.GetValues(typeof(enuDashStyle)))
            {
                if (item != enuDashStyle.Custom)
                {
                    v_m.Add((CorePenDashStyle)GetLineStyle(item));
                }
            }
            string dir = IGK.ICore.IO.PathUtils.GetPath(DashStyleFolder);//.GetStartupFullPath("DashStyle");
            IGK.ICore.Codec.IXMLDeserializer deseri = null;
            if (System.IO.Directory.Exists(dir))
            {
                foreach (string str in System.IO.Directory.GetFiles(dir, "*.gkls"))//line style
                {
                    using (System.IO.Stream stream = System.IO.File.Open(str, System.IO.FileMode.Open))
                    {
                        deseri = IGK.ICore.Codec.CoreXMLDeserializer.Create(stream);
                        if (deseri.ReadToDescendant("Style"))
                        {
                            CorePenDashStyle obj = new CorePenDashStyle();
                            obj.m_Style = enuDashStyle.Custom;
                            obj.Deserialize(deseri);
                            v_m.Add(obj);
                        }
                    }
                }
            }
            foreach (CorePenDashStyle item in v_m)
            {
                sm_PensLineStyle.Add(item.DisplayName, item);
            }
            return v_m.ToArray();
        }
        /// <summary>
        /// get the dash style folder
        /// </summary>
        public static string DashStyleFolder
        {
            get
            {
                if (CoreApplicationSetting.Instance.Contains("DashStyleFolder") == false)
                {
                    CoreApplicationSetting.Instance["DashStyleFolder"].Value = "%startup%/DashStyles";
                }
                return (string)CoreApplicationSetting.Instance["DashStyleFolder"].Value;
            }
            set
            {
                CoreApplicationSetting.Instance["DashStyleFolder"].Value = value;
            }
        }
        public bool Save()
        {
            if (this.Style != enuDashStyle.Custom)
                return false;
            IGK.ICore.Codec.IXMLSerializer deseri = null;
            string dir = IO.PathUtils.GetPath(DashStyleFolder);
            if (!IO.PathUtils.CreateDir(dir))
            {
                CoreLog.WriteDebug(string.Format("Can't create folder {0}", dir));
                return false;
            }
            string file = IO.PathUtils.GetPath(string.Format(dir + "/{0}.gkls", this.m_Name));
            deseri = IGK.ICore.Codec.CoreXMLSerializer.Create(file);
            deseri.WriteStartElement("Style");
            this.Serialize(deseri);
            deseri.WriteEndElement();
            deseri.Close();
            return true;
        }
        public static void Reload()
        {
            sm_PensLineStyle.Clear();
            LoadStyles();
        }
        #region ICoreLineStyle Members
        public string DisplayName
        {
            get { return this.m_Name; }
        }
        public  float[] GetUnit()
        {
            if ((this.m_Units == null) || (this.m_Units.Length == 0))
                return null;
            float[] t = new float[this.m_Units.Length];
            for (int i = 0; i < t.Length; i++)
            {
                t[i] = ((ICoreUnitPixel)this.m_Units[i]).Value;
            }
            return t;
        }
        #endregion
        #region ICoreLineStyle Members
        /// <summary>
        /// get or set the dash offset
        /// </summary>
        public float DashOffset
        {
            get
            {
                return m_dashOffset;
            }
            set
            {
                m_dashOffset = value;
            }
        }
        #endregion
        public override string ToString()
        {
            return "DashStyle [" + this.Style + "]";
        }
        #region ICoreSerializerService Members
        public void Serialize(IGK.ICore.Codec.IXMLSerializer xwriter)
        {
            xwriter.WriteElementString("Name", this.m_Name);
            xwriter.WriteElementString("Offset", this.DashOffset.ToString());
            xwriter.WriteElementString("Units", GetUnitToString());
        }
        public void Deserialize(IGK.ICore.Codec.IXMLDeserializer xreader)
        {
            while (xreader.Read())
            {
                switch (xreader.NodeType)
                {
                    case System.Xml.XmlNodeType.Element:
                        switch (xreader.Name.ToLower())
                        {
                            case "name": this.m_Name = xreader.ReadElementContentAsString(); break;
                            case "offset": this.m_dashOffset = float.Parse(xreader.ReadElementContentAsString()); break;
                            case "units":
                                string[] str = xreader.ReadElementContentAsString().Split(';');
                                List<CoreUnit> unit = new List<CoreUnit>();
                                CoreUnit c = null;
                                foreach (string item in str)
                                {
                                    c = item;
                                    if (c != null)
                                        unit.Add(c);
                                }
                                this.m_Units = unit.ToArray();
                                break;
                        }
                        break;
                }
            }
        }
        #endregion
        public static ICoreLineStyle GetLineStyle(string name)
        {
            if (name.StartsWith("#"))
            {
                name = name.Substring(1);
            }
            if (sm_PensLineStyle.ContainsKey(name))
                return sm_PensLineStyle[name];
            return sm_PensLineStyle[enuDashStyle.Solid.ToString()];
        }
        #region ICoreWorkingDefinitionObject Members
        public string GetDefinition(IGK.ICore.Codec.IXMLSerializer seri)
        {
            if (seri == null)
            {
            }
            return GetDefinition();
        }
        /// <summary>
        /// get the definition of this core pen definition
        /// </summary>
        /// <returns></returns>
        public string GetDefinition()
        {
            StringBuilder sb = new StringBuilder();
            if (this.Style != enuDashStyle.Custom)
                sb.Append(this.Style);
            else
            {
                sb.Append("#" + this.DisplayName);
            }
            return sb.ToString();
        }
        public void CopyDefinition(string str)
        {
            //copy defition
        }
        #endregion
        public static bool AddStyle(CorePenDashStyle style)
        {
            if ((style == null) || (sm_PensLineStyle.ContainsKey(style.DisplayName)))
                return false;
            sm_PensLineStyle.Add(style.DisplayName, style);
            return true;
        }
    }
}

