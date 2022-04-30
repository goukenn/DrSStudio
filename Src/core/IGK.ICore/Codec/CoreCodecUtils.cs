

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: CoreCodecUtils.cs
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
file:CoreCodecUtils.cs
*/
using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.ComponentModel;
using IGK.ICore;using IGK.ICore.ComponentModel;
namespace IGK.ICore.Codec
{
    /// <summary>
    /// represent a delegate for additional element proc
    /// </summary>
    /// <param name="deseri"></param>
    /// <returns></returns>
    public delegate bool CoreReadAdditionalElementPROC(IXMLDeserializer deseri);
    public static class CoreCodecUtils
    {
        /// <summary>
        /// get string extensions
        /// </summary>
        /// <param name="v_filter"></param>
        /// <returns></returns>
        public static string GetExtensions(string v_filter)
        {
            string[] t = v_filter.Split('|');
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < t.Length - 1; i += 2)
            {
                sb.Append(t[i + 1]);
            }
            return sb.ToString();
        }
        public static void WriteAttributes(ICoreWorkingObject obj, IXMLSerializer xwriter)
        {
            CoreXMLAttributeAttribute v_attr = null;
            CoreXMLDefaultAttributeValueAttribute v_default = null;
            object v_obj = null;
            string v_def = null;
            System.ComponentModel.TypeConverter v_conv = null;
            foreach (PropertyInfo pr in obj.GetType().GetProperties())
            {
                if (!pr.CanRead) continue;
                v_obj = pr.GetValue(obj, null);
                if (v_obj == null) continue;
                v_attr = CoreXMLAttributeAttribute.GetCustomAttribute(pr);
                if (v_attr == null) continue;
                v_default = Attribute.GetCustomAttribute(pr, typeof(CoreXMLDefaultAttributeValueAttribute))
                    as CoreXMLDefaultAttributeValueAttribute;
                if ((v_default != null) && v_default.IsDefaultValue(v_obj)) continue;
                if ((v_attr != null) && (v_attr.IsAttribute))
                {
                    if (v_obj is ICoreWorkingDefinitionObject)
                    {
                        v_def = (v_obj as ICoreWorkingDefinitionObject).GetDefinition();
                        if (!string.IsNullOrEmpty(v_def))
                            xwriter.WriteAttributeString(pr.Name, v_def);
                    }
                    else
                    {
                        //check if custom attribute is defined
                        object[] v_t = pr.GetCustomAttributes(typeof(TypeConverterAttribute), false);
                        if (v_t.Length == 1)
                        {
                            //conains a type conveter
                            Type v_convtype = Type.GetType((v_t[0] as TypeConverterAttribute).ConverterTypeName);
                            v_conv = v_convtype.Assembly.CreateInstance(v_convtype.FullName) as TypeConverter;
                        }
                        else
                            v_conv = CoreTypeDescriptor.GetConverter(pr.PropertyType);
                        xwriter.WriteAttributeString(pr.Name, v_conv.ConvertToString(v_obj));
                    }
                }
            }
        }
        public static void WriteElements(ICoreWorkingObject obj, IXMLSerializer xwriter)
        {
            CoreXMLElementAttribute v_attr = null;
            CoreXMLDefaultAttributeValueAttribute v_default = null;
            object v_obj = null;
            string v_def = null;
            System.ComponentModel.TypeConverter v_conv = null;
            foreach (PropertyInfo pr in obj.GetType().GetProperties())
            {
                if (!pr.CanRead) continue;
                v_obj = pr.GetValue(obj, null);
                if (v_obj == null) continue;
                v_attr = CoreXMLElementAttribute.GetCustomAttribute(pr);
                v_default = Attribute.GetCustomAttribute(pr, typeof(CoreXMLDefaultAttributeValueAttribute))
                    as CoreXMLDefaultAttributeValueAttribute;
                if ((v_default != null) && (v_obj.Equals(v_default.Value))) continue;
                if ((v_attr != null) && (v_attr.IsElement))
                {
                    if (v_obj is ICoreWorkingDefinitionObject)
                    {
                        v_def = (v_obj as ICoreWorkingDefinitionObject).GetDefinition();
                        if (!string.IsNullOrEmpty(v_def))
                        {
                            xwriter.WriteStartElement(pr.Name);
                            xwriter.WriteValue(v_def);
                            xwriter.WriteEndElement();
                        }
                    }
                    if (v_obj is ICoreSerializerService)
                    {
                        StringBuilder sb = new StringBuilder();
                        CoreXMLSerializer vd = CoreXMLSerializer.Create(sb, xwriter);
                        (v_obj as ICoreSerializerService).Serialize(vd);
                        vd.Flush();
                        if (!string.IsNullOrEmpty(sb.ToString()))
                        {
                            xwriter.WriteStartElement(pr.Name);
                            xwriter.WriteRaw(sb.ToString());
                            xwriter.WriteEndElement();
                        }
                    }
                    else
                    {
                        v_conv = CoreTypeDescriptor.GetConverter(pr.PropertyType);
                        string v_str = v_conv.ConvertToString(v_obj);
                        if (!string.IsNullOrEmpty(v_str))
                        {
                            xwriter.WriteStartElement(pr.Name);
                            xwriter.WriteValue(v_str);
                            xwriter.WriteEndElement();
                        }
                    }
                }
            }
        }
        public static void ReadElements(ICoreWorkingObject obj, IXMLDeserializer xreader)
        {
            ReadElements(obj, xreader, null);
        }
        /// <summary>
        /// public read xml 
        /// </summary>
        /// <param name="item"></param>
        /// <param name="xreader"></param>
        public static void ReadElements(ICoreWorkingObject item, IXMLDeserializer xreader, CoreReadAdditionalElementPROC proc)
        {
            if (xreader.IsEmptyElement) return;
            //element property info
            //capture element property
            Dictionary<string, PropertyInfo> v_pr = new Dictionary<string, PropertyInfo>();
            CoreXMLElementAttribute v_attr = null;
            foreach (PropertyInfo v in item.GetType().GetProperties())
            {
                v_attr = CoreXMLElementAttribute.GetCustomAttribute(v);
                if (v_attr != null)
                {
                    v_pr.Add(v.Name, v);
                }
            }
            xreader.MoveToElement();
            while (xreader.Read())
            {
                switch (xreader.NodeType)
                {
                    case System.Xml.XmlNodeType.Element:
                        string v_name = xreader.Name;
                        if (v_pr.ContainsKey(v_name))
                        {
                            SetElementProperty(xreader.ReadSubtree(), v_pr[v_name], item);
                        }
                        else
                        {
                            //read element child
                            if (proc != null)
                            {
                                proc(xreader);
                            }
                            else
                            {
                                xreader.Skip();
                                CoreLog.WriteLine("element skip : " + v_name);
                            }
                        }
                        break;
                }
            }
        }
        /// <summary>
        /// call this method to set up the element property
        /// </summary>
        /// <param name="xreader"></param>
        /// <param name="propertyInfo"></param>
        /// <param name="item"></param>
        public static void SetElementProperty(IXMLDeserializer xreader, PropertyInfo propertyInfo, ICoreWorkingObject item)
        {
            if ((xreader == null) || (propertyInfo == null) || (item == null))
                return;
            if (xreader.NodeType == System.Xml.XmlNodeType.None)
            {
                xreader.Read();
            }
            TypeConverter v_conv = null;
            if (propertyInfo.CanWrite)
            {
                v_conv = CoreTypeDescriptor.GetConverter(propertyInfo.PropertyType);
                string vstr = xreader.ReadElementContentAsString();
                propertyInfo.SetValue(item, v_conv.ConvertFromString(vstr), null);
            }
            else
            {
                if (propertyInfo.PropertyType.GetInterface(typeof(ICoreSerializerService).Name) != null)
                {
                    ICoreSerializerService v_obj = propertyInfo.GetValue(item, null) as ICoreSerializerService;
                    v_obj.Deserialize(xreader);
                }
                else if (propertyInfo.PropertyType.IsAssignableFrom(typeof(ICoreWorkingDefinitionObject)))
                {
                    ICoreWorkingDefinitionObject v_obj = propertyInfo.GetValue(item, null) as ICoreWorkingDefinitionObject;
                    v_obj.CopyDefinition(xreader.ReadElementString());
                }
            }
        }
        public static void ReadAttributes(ICoreWorkingObject obj, IXMLDeserializer xreader)
        {
                 if (!xreader.HasAttributes) 
                return;            
            int v_count = xreader.AttributeCount;
            xreader.MoveToFirstAttribute();
            Type v_t = obj.GetType ();
            PropertyInfo v_pr = null;
            TypeConverter v_conv = null;
            Object v_obj = null;
            for (int i = 0; i <   v_count ; i++)
            {
                v_pr = v_t.GetProperty(xreader.Name );
                if ((v_pr != null) && 
                    CoreXMLAttributeAttribute.IsDefined(obj, v_pr))
                {                    
                    if (v_pr.CanWrite)
                    {
                        object[] v_tb = v_pr.GetCustomAttributes(typeof(TypeConverterAttribute), false);
                        if (v_tb.Length == 1)
                        {
                            Type v_tconv = Type.GetType((v_tb[0] as TypeConverterAttribute).ConverterTypeName);
                            v_conv = v_tconv.Assembly.CreateInstance(v_tconv.FullName) as TypeConverter;
                        }
                        else {
                            v_conv = CoreTypeDescriptor.GetConverter(v_pr.PropertyType);
                        }
                            //if ((v_conv != null) && (v_conv.CanConvertFrom(typeof(string))))
                            //{
                                v_pr.SetValue(obj, v_conv.ConvertFromString(xreader.Value), null);
                            //}
                    }
                    else
                    {
                        v_obj = v_pr.GetValue(obj, null);
                        if ((v_obj != null) && (v_obj is ICoreWorkingDefinitionObject))
                        {
                            (v_obj as ICoreWorkingDefinitionObject).CopyDefinition(xreader.Value);
                        }
                    }
                }
                xreader.MoveToNextAttribute();
            }
        }
    }
}

