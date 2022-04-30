using IGK.ICore.Codec;
using IGK.ICore.ComponentModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;

namespace IGK.ICore.Xml
{
    public class CoreXmlUtility
    {
        public static string GetStringValue(object obj)
        {
            if (obj == null)
                return null;
            if (obj is ICoreXmlGetValueMethod)
            {
                return (obj as ICoreXmlGetValueMethod).GetValue();
            }
            return obj.ToString();
        }
        public static string WriteElements(ICoreWorkingObject item, IXmlOptions options)
        {
            if (item == null)
                return string.Empty;
            CoreXmlElement v_dt = CoreXmlElement.CreateXmlNode("dummy");
            var h = new StringBuilder();
            CoreXMLSerializer seri = CoreXMLSerializer.Create(h);
            CoreXMLElementAttribute v_attr = null;
            CoreXMLDefaultAttributeValueAttribute v_default = null;
            object v_obj = null;
            string v_def = null;
            System.ComponentModel.TypeConverter v_conv = null;
            PropertyInfo[] v_tpr = item.GetType().GetProperties();
            seri.WriteStartElement("root");
            foreach (PropertyInfo pr in v_tpr)
            {
                if (!pr.CanRead)
                    continue;
                v_attr = CoreXMLElementAttribute.GetCustomAttribute(pr);
                if (v_attr == null)
                    continue;
                v_obj = pr.GetValue(item, null);
                if (v_obj == null)
                    continue;
                v_default = Attribute.GetCustomAttribute(pr, typeof(CoreXMLDefaultAttributeValueAttribute))
                    as CoreXMLDefaultAttributeValueAttribute;
                if ((v_default != null) && v_default.IsDefaultValue(v_obj)) continue;
                if (v_attr.IsElement)
                {
                    if (v_obj is ICoreWorkingDefinitionObject)
                    {
                        v_def = (v_obj as ICoreWorkingDefinitionObject).GetDefinition();
                        if (!string.IsNullOrEmpty(v_def))
                        {
                            seri.WriteStartElement(pr.Name);
                            seri.WriteValue(v_def);
                            seri.WriteEndElement();                         
                            continue;
                        }
                    }
                    if (v_obj is ICoreSerializerService)
                    {
                        StringBuilder sb = new StringBuilder();
                        CoreXMLSerializer vd = CoreXMLSerializer.Create(sb, seri);

                        try
                        {
                            vd.WriteStartElement(CoreXMLSerializerUtility.GetName(pr));
                            //CoreLog.DEBUGING = true;
                            (v_obj as ICoreSerializerService).Serialize(vd);

                            vd.WriteEndElement();
                            vd.Flush();
                        }
                        catch (Exception ex)
                        {
                            CoreLog.WriteDebug("Exception " + ex.Message);
                        }
                        System.Xml.XmlDocument d = new System.Xml.XmlDocument();
                        d.LoadXml(sb.ToString());
                        System.Xml.XmlNode node = d.ChildNodes[0];
                        if (((node.Attributes != null) && (node.Attributes.Count > 0)) || (node.ChildNodes.Count > 0))
                        {
                            seri.WriteRaw(sb.ToString());

                        }
                        continue;
                    }
                    if (v_obj is ICoreSerializable)
                    {
                        (v_obj as ICoreSerializable).Serialize(seri);
                    }
                    else
                    {
                        TypeConverterAttribute c = null;
                        object[] bc =
                            pr.GetCustomAttributes(typeof(TypeConverterAttribute), false);//[0] as TypeConverterAttribute ;
                        if (bc.Length == 1)
                            c = bc[0] as TypeConverterAttribute;
                        if (c != null)
                        {
                            Type t = Type.GetType(c.ConverterTypeName);
                            v_conv = t.Assembly.CreateInstance(t.FullName) as TypeConverter;
                        }
                        else
                            v_conv = CoreTypeDescriptor.GetConverter(pr.PropertyType);
                        string v_str = v_conv.ConvertToString(v_obj);
                        if (!string.IsNullOrEmpty(v_str))
                        {
                            seri.WriteStartElement(pr.Name);
                            seri.WriteValue(v_str);
                            seri.WriteEndElement();
                        }
                    }
                }
            }
            seri.WriteEndElement();
            
            seri.Close();
            string v_hstr = h.ToString();
                if (v_hstr.ToLower() == "<root />")
                return string.Empty;

            v_dt.LoadString(v_hstr);
            var p = (v_dt.Childs[0] as CoreXmlElement);
            p.Parent.Remove(p);
            if (p.HasChild)
                return p.RenderInnerHTML(options);
            return string.Empty;
        }


        public static string TreatAttributeValue(string input)
        {
            input = Regex.Replace(input, "(\\\\)*[\"]", (o) => {
                if (!o.Value.StartsWith("\\"))
                {
                    return "&quot;";
                }
                return o.Value;
            });
            return input;
        }

        public static bool ValidName(string newTableName)
        {
            return XmlReader.IsName(newTableName);
        }
    }
}
