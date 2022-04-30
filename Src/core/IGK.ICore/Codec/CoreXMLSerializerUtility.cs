

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: CoreXMLSerializerUtility.cs
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
file:CoreXMLSerializerUtility.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Runtime.InteropServices;
using System.ComponentModel;
using System.Xml;
using System.Text.RegularExpressions;

namespace IGK.ICore.Codec
{
    using IGK.ICore;
    using IGK.ICore.Drawing2D;
    using IGK.ICore.WorkingObjects;
    using IGK.ICore.ComponentModel;
    using IGK.ICore.Dependency;
    using IGK.ICore.Xml;


    /// <summary>
    /// Core Xml loader utility
    ///  </summary>
    public static class CoreXMLSerializerUtility
    {
        public static void WriteAttributes(ICoreWorkingObject item, IXMLSerializer seri, Action<string, object> SetAttribute=null)
        {
            if (item == null)
                return;
            CoreXMLAttributeAttribute v_attr = null;
            CoreXMLDefaultAttributeValueAttribute v_default = null;
            object v_obj = null;
            string v_def = null;
            System.ComponentModel.TypeConverter v_conv = null;
            //to store writed properties
            List<string> v_pjr = new List<string>();
            PropertyInfo[] v_prInfos = item.GetType().GetProperties();
            foreach (PropertyInfo pr in v_prInfos)
            {
                
                if (v_pjr.Contains(pr.Name))
                    continue; // already save the property
               

                if (!pr.CanRead) continue;
                v_attr = CoreXMLAttributeAttribute.GetCustomAttribute(pr);
                if (v_attr == null) continue;
                v_obj = pr.GetValue(item, null);
                if (v_obj == null) continue;
                v_default = Attribute.GetCustomAttribute(pr, typeof(CoreXMLDefaultAttributeValueAttribute))
                    as CoreXMLDefaultAttributeValueAttribute;
                if ((v_default != null) && (v_obj.Equals(v_default.Value))) continue;

              
                if ((v_attr != null) && (v_attr.IsAttribute))
                {
                    if (v_obj is ICoreWorkingDefinitionObject)
                    {
                        v_def = (v_obj as ICoreWorkingDefinitionObject).GetDefinition();
                        if (!string.IsNullOrEmpty(v_def))
                            seri.WriteAttributeString(pr.Name, v_def);
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

                        
                        var vattr = Attribute.GetCustomAttribute(pr, typeof(CoreXMLNameAttribute)) as CoreXMLNameAttribute;
                        string v_n  = vattr?.name ?? pr.Name;


                        seri.WriteAttributeString(v_n, v_conv.ConvertToString(v_obj));
                        v_pjr.Add(pr.Name);
                    }
                }
            }
            CoreDependencyObject s = item as CoreDependencyObject ;
            if (s != null) 
            {
                //store attached property
                var p = new { Parent = default(CoreWorkingObjectBase), Type = default(Type) };
                var c = CoreDependencyObject.GetDenpendencyValues(s);
                if (c != null)
                {
                    foreach (KeyValuePair<CoreDependencyProperty, object> v in c)
                    {
                        v_conv = CoreTypeDescriptor.GetConverter(v.Key.PropertyType);
                        seri.WriteAttributeString(v.Key.GetFullName(), v_conv.ConvertToString(v.Value));
                    }
                }
            }
        }
        public static void WriteBrushDefinition(IXMLSerializer xwriter, string name, ICoreBrush brush)
        {
            TypeConverter tp = null;
            tp = CoreTypeDescriptor.GetConverter(brush);
            xwriter.WriteAttributeString(name, tp.ConvertTo(brush, typeof(string)).ToString());
        }
        public static void ReadBrushDefinition(IXMLDeserializer xreader, string name, ICoreBrush brush)
        {
            string str = xreader.GetAttribute(name);
            if (!string.IsNullOrEmpty(str))
                brush.CopyDefinition(str);
        }
        public static void WriteElements(ICoreWorkingObject item, IXMLSerializer seri)
        {
            CoreXMLElementAttribute v_attr = null;
            CoreXMLDefaultAttributeValueAttribute v_default = null;
            object v_obj = null;
            string v_def = null;
            System.ComponentModel.TypeConverter v_conv = null;
            PropertyInfo[] v_tpr = item.GetType().GetProperties();
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
                if ((v_default != null) && v_default.IsDefaultValue (v_obj)) continue;
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
                            vd.WriteStartElement(GetName(pr));
                            //CoreLog.DEBUGING = true;
                            (v_obj as ICoreSerializerService).Serialize(vd);

                            vd.WriteEndElement();
                            vd.Flush();
                        }
                        catch(Exception ex)
                        {
                            CoreLog.WriteDebug("Exception " + ex.Message);
                        }
                        System.Xml.XmlDocument d = new System.Xml.XmlDocument();
                        d.LoadXml(sb.ToString());
                        System.Xml.XmlNode node = d.ChildNodes [0];
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
        }
        /// <summary>
        /// get the core display name of this property
        /// </summary>
        /// <param name="pr"></param>
        /// <returns></returns>
        public  static string GetName(PropertyInfo pr)
        {
            CoreXMLDisplayNameAttribute v_attr =
                CoreXMLDisplayNameAttribute.GetCustomAttribute(pr, typeof(CoreXMLDisplayNameAttribute)) as CoreXMLDisplayNameAttribute;
            if (v_attr != null)
                return v_attr.Name;
            return pr.Name;
        }
        public static void WriteStartElement(ICoreWorkingObject item, IXMLSerializer xwriter)
        {
            CoreWorkingObjectAttribute t =
            Attribute.GetCustomAttribute(item.GetType(), typeof(CoreWorkingObjectAttribute)) as CoreWorkingObjectAttribute;
            if (t != null)
            {
                xwriter.WriteStartElement(t.Name);
            }
            else {
                xwriter.WriteStartElement(item.GetType().FullName);
            }
        }
        public static void ReadElements(ICoreWorkingObject item, IXMLDeserializer xreader)
        {
            ReadElements(item, xreader, null);
        }
        /// <summary>
        /// public read xml 
        /// </summary>
        /// <param name="item"></param>
        /// <param name="xreader"></param>
        public static void ReadElements(ICoreWorkingObject item, IXMLDeserializer xreader, 
            CoreReadAdditionalElementPROC proc)
        {
            if (xreader.IsEmptyElement) 
                return;
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
                                SetElementProperty(xreader.ReadSubtree(), v_pr[v_name], item, proc);
                            }
                            else
                            {
                                ICoreWorkingObject
                                    obj = CoreSystem.CreateWorkingObject(xreader.Name, xreader)
                                    as ICoreWorkingObject;
                                if (obj != null)
                                {
                                    if (obj is IGK.ICore.Dependency.CoreDependencyLoader)
                                    {
                                        (obj as IGK.ICore.Dependency.CoreDependencyLoader).LoadValue(item,
                                            xreader.ReadInnerXml ());
                                    }
                                    continue;
                                }
                                //read element child
                                bool v_read = true;
                                if (proc != null)
                                {
                                    proc(xreader);
                                }
                                else
                                {
                                    v_read = false;
                                    if (item is ICoreSerializerAdditionalPropertyService)
                                    {
                                        CoreReadAdditionalElementPROC v_proc = (item as ICoreSerializerAdditionalPropertyService).GetProc();
                                        if (v_proc != null)
                                        {
                                            v_read = true;
                                            v_proc(xreader);
                                        }
                                    }
                                }
                                if (!v_read)
                                {
                                    if (!xreader.IsEmptyElement)
                                    {
                                        xreader.Skip();
                                        CoreLog.WriteLine("CoreXmlSerializerUtility Element Skipped : " + v_name);
                                    }
                                }
                            }
                            break;
                    }
                }            
        }
        /// <summary>
        /// read attribute
        /// </summary>
        /// <param name="item"></param>
        /// <param name="xreader"></param>
        public static void ReadAttributes(ICoreWorkingObject item, IXMLDeserializer xreader, Action<string, string> SetAttribute)
        {
          
            if (!xreader.HasAttributes)
                return;
            int v_count = xreader.AttributeCount;
            string v_name = string.Empty;
            if (xreader.MoveToFirstAttribute())
            {
                //read attribute to move speed
               
                Type v_t = item.GetType();
                PropertyInfo v_pr = null;
                TypeConverter v_conv = null;
                for (int i = 0; i < v_count; i++)
                {
                    v_name = xreader.Name;
                    v_pr = v_t.GetProperty(v_name);
                    if (v_name == "Matrix") {


                    }
                    //if (xreader.Name == "FillBrush")
                    //{
                    //    System.Diagnostics.Debug.WriteLine (xreader.Value);
                    //}
                    //if (xreader.Value == "#F444")
                    //{

                    //}

                    if ((v_pr != null) && CoreXMLAttributeAttribute.IsDefined(item, v_pr))
                    {
                        if (v_pr.CanWrite)
                        {
                            object[] v_tb = v_pr.GetCustomAttributes(typeof(TypeConverterAttribute), false);
                            if (v_tb.Length == 1)
                            {
                                Type v_tconv = Type.GetType((v_tb[0] as TypeConverterAttribute).ConverterTypeName);
                                v_conv = v_tconv.Assembly.CreateInstance(v_tconv.FullName) as TypeConverter;
                            }
                            else
                            {
                                v_conv = CoreTypeDescriptor.GetConverter(v_pr.PropertyType);
                            }
                            if (v_conv.CanConvertFrom(typeof(string)))
                                v_pr.SetValue(item, v_conv.ConvertFromString(xreader.Value), null);
                            else
                            {
                                CoreLog.WriteLine("Can't convert from string ...." + v_name);
                            }
                        }
                        else
                        {
                         
                            if (SetAttribute == null)
                                ReadAttributeValue(item, v_name, xreader.Value);
                            else
                                SetAttribute(v_name, xreader.Value);
                        }
                    }
                    else
                    {
                        //attached properties . attached properties 

                        var b = CoreSystem.CreateWorkingObject(v_name) as IGK.ICore.Dependency.CoreDependencyLoader;
                        if (b != null)
                        {
                            b.LoadValue(item, xreader.Value);
                            xreader.MoveToNextAttribute();
                            continue;
                        }                  
                    }
                    xreader.MoveToNextAttribute();
                }
            }
        }
        /// <summary>
        /// override this method if this object can't be writed directly
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        public static void ReadAttributeValue(ICoreWorkingObject obj, string name, string value)
        {
            if (obj == null)
                return;
            object v_obj = obj.GetType().GetProperty(name).GetValue(obj, null);
            ICoreWorkingDefinitionObject v_dev = v_obj as ICoreWorkingDefinitionObject;
            if (v_dev != null)
            {
                v_dev.CopyDefinition(value);
            }
        }
        /// <summary>
        /// deserialize working object
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public static ICoreWorkingObject[] GetAllObjects(string expression)
        {
            System.IO.StringReader sr = new System.IO.StringReader(expression);
            XmlReaderSettings v_setting = new XmlReaderSettings();
            v_setting.IgnoreComments = true;
            v_setting.IgnoreProcessingInstructions = true;
            v_setting.ValidationType = ValidationType.None;
            v_setting.ConformanceLevel = ConformanceLevel.Fragment;
            CoreXMLDeserializer deserir = CoreXMLDeserializer.Create(System.Xml.XmlReader.Create(sr, v_setting));
            var s =  GetAllObjects(deserir);
            deserir.Close();
            return s;
        }
        /// <summary>
        /// get all object in this node
        /// </summary>
        /// <param name="xreader"></param>
        /// <returns></returns>
        public static ICoreWorkingObject[] GetAllObjects(IXMLDeserializer xreader)
        {
            List<ICoreWorkingObject> v_objects = new List<ICoreWorkingObject>();

            bool skipped = false;
            while (skipped  || xreader.Read())
            {
                skipped = false;
                switch (xreader.NodeType)
                {
                    case System.Xml.XmlNodeType.Element:
                        //or icoreworking object or dependency object that can be attached
                        ICoreWorkingObject
                                   obj = CoreSystem.CreateWorkingObject(xreader.Name)
                                   as ICoreWorkingObject;
                        if (obj != null)
                        {
                            v_objects.Add(obj);
                            if (obj is IGK.ICore.Codec.ICoreSerializerService)
                                (obj as IGK.ICore.Codec.ICoreSerializerService).Deserialize(xreader.ReadSubtree());
                        }
                        else
                        {
                            CoreLog.WriteDebug("Item : " + xreader.Name + " Not Created");
                            if (!xreader.IsEmptyElement)
                            {
                                xreader.MoveToContent();
                                xreader.Skip();
                                skipped = true;
                            }
                        }
                        break;
                }
            }
            return v_objects.ToArray();
        }
        /// <summary>
        /// call this method to set up the element property
        /// </summary>
        /// <param name="xreader"></param>
        /// <param name="propertyInfo"></param>
        /// <param name="item"></param>
        public static void SetElementProperty(IXMLDeserializer xreader, 
            PropertyInfo propertyInfo, 
            ICoreWorkingObject item,
            CoreReadAdditionalElementPROC proc)
        {
            if ((xreader == null) || (propertyInfo == null) || (item == null))
                return;

            if (xreader.NodeType == System.Xml.XmlNodeType.None)
            {
                xreader.Read();
            }
            TypeConverter v_conv = null;

            CoreXMLResourceElementAttribute attr = propertyInfo.GetCustomAttribute<CoreXMLResourceElementAttribute>();
            if (attr != null)
            {
                SetResourceElementProperty(xreader, propertyInfo, item, proc);               
                return;
            }

            if (propertyInfo.CanWrite)
            {
                object[] attribs = propertyInfo.GetCustomAttributes(typeof(TypeConverterAttribute), false);
                if (attribs.Length == 1)
                {
                    Type t = Type.GetType((attribs[0] as TypeConverterAttribute).ConverterTypeName);
                    v_conv = t.Assembly.CreateInstance(t.FullName) as TypeConverter;
                }
                else
                    v_conv = CoreTypeDescriptor.GetConverter(propertyInfo.PropertyType);
                
                if (v_conv.CanConvertFrom(typeof(string)))
                {
                    string vstr = xreader.ReadElementContentAsString().Trim();
                    
                    try
                    {
                        object vobj = null;
                        vobj = v_conv.ConvertFromString(
                               vstr
                            );
                        propertyInfo.SetValue(item, vobj, null);
                    }
                    catch { 

                    }
                }
                else {

                    if (xreader.HasValue)
                    {
                        string s = xreader.ReadInnerXml();
                        if (!string.IsNullOrEmpty(s))
                        {
                            StringBuilder sb = new StringBuilder(s);
                            CoreXMLDeserializer deseri = CoreXMLDeserializer.Create(sb);
                            Object[] d = GetAllObjects(deseri);
                            if (d.Length == 1)
                            {
                                propertyInfo.SetValue(item, d[0], null);
                            }
                        }
                    }
                }
                
            }
            else
            {
                object cobj = propertyInfo.GetValue(item, null);
                if (cobj != null)
                {
                    if (cobj.GetType().GetInterface(typeof(ICoreDeserializable).Name) != null)
                    {
                        ICoreDeserializable v_obj = cobj as ICoreDeserializable;
                        v_obj.Deserialize(xreader);
                    }
                    else if (cobj.GetType().IsAssignableFrom(typeof(ICoreWorkingDefinitionObject)))
                    {
                        ICoreWorkingDefinitionObject v_obj = cobj as ICoreWorkingDefinitionObject;
                        v_obj.CopyDefinition(xreader.ReadElementString());
                    }
                }
                else if (proc != null)
                    proc(xreader);
            }
        }

        internal static void SetResourceElementProperty(IXMLDeserializer xreader, PropertyInfo propertyInfo, ICoreWorkingObject item, CoreReadAdditionalElementPROC proc)
        {
            //bind resources element
            if (!xreader.HasValue)
            {
                //consider id 
                string v_id = xreader.GetAttribute("Id");
                if (!string.IsNullOrEmpty(v_id))
                {
                    string rg = CoreConstant.REGEX_RES_TARGET;
                    if (Regex.IsMatch(v_id, rg, RegexOptions.IgnoreCase))
                    {
                        Match m = Regex.Match(v_id, rg, RegexOptions.IgnoreCase);
                        string cat = m.Groups["cat"].ToString();
                        string v_resid = m.Groups["id"].ToString();
                        xreader.LoadingComplete += (o, e) =>
                        {
                            ICoreLoadingContext deseri = o as ICoreLoadingContext;
                            switch (cat)
                            {
                                case "resources":
                                    ResourceElement v = deseri.GetElementByTagName(CoreConstant.TAG_RESOURCES)
                                         as ResourceElement ;
                                    if (v != null)
                                    {
                                        object obj = v.GetElementById(v_resid);
                                        if (obj != null)
                                        {
                                            propertyInfo.SetValue(item, obj, null);
                                        }
                                    }
                                    break;
                                default:
                                    break;
                            }
                        };
                    }
                }
            }
        }
        /// <summary>
        /// get object
        /// </summary>
        /// <param name="xreader"></param>
        /// <returns></returns>
        public static ICoreWorkingObject GetObject(IXMLDeserializer xreader)
        {
            if (xreader.NodeType == System.Xml.XmlNodeType.Element)
            {
                ICoreWorkingObject
                                obj = CoreSystem.CreateWorkingObject(xreader.Name, xreader)
                                as ICoreWorkingObject;
                if (obj != null)
                {
                    if (obj is IGK.ICore.Codec.ICoreSerializerService)
                        (obj as IGK.ICore.Codec.ICoreSerializerService).Deserialize(xreader.ReadSubtree());
                }
                else
                {
                    CoreLog.WriteDebug("Item : " + xreader.Name + " Not Created");
                    if (!xreader.IsEmptyElement)
                        xreader.Skip();
                }
                return obj;
            }
            return null;
        }
     
        internal static void ReadNode(ImageElement imageElement, IXMLDeserializer xreader)
        {
            throw new NotImplementedException();
        }

        internal static void WriteValue(CoreXMLSerializer seri, CoreXmlElement q)
        {
            var o = q.Content;
            if (o is ICoreTextValueElement m)
            {
                seri.WriteValue(m.Text);
            }
            else {
                seri.WriteValue(o);
            }
        }
    }
}

