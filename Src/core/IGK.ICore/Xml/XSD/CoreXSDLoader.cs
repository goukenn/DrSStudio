using IGK.ICore.IO;
using IGK.ICore.Resources;
using IGK.ICore.Xml.XSD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace IGK.ICore.Xml
{
    public class CoreXSDLoader
    {
         delegate bool SkipHandler();

        public static void Load(XmlReader xmlReader, 
            ICoreXSDLoaderListener listener)
        {
            bool r_r = false;
            var _xr = xmlReader;
            Stack<XmlReader > v_stackReader=new Stack<XmlReader> ();
            string v_n = string.Empty ;
            var d = new SkipHandler( ()=> {
                if (v_stackReader.Count > 0)
                {
                    listener.ExitElement();
                    return (_xr = v_stackReader.Pop()).Read();
                }
                return false ;
            });
            var _skipd = (CoreMethodInvoker) delegate (){
                v_stackReader.Push(_xr);
                _xr = _xr.ReadSubtree();
                _xr.Read();
            };

            if (listener == null) {

                listener = new CoreXSDFileLoaderListener();
            }
            while (_xr.Read() || d())
            {

                switch (_xr.NodeType)
                {
                    case XmlNodeType.Element:

                        string v_nodeName = GetName(_xr);
                        if (!r_r)
                        {
                            if (v_nodeName == "xs:schema")
                            {

                                var v_dic = _xr.GetAttributesDictionary();
                                if (!v_dic.ContainsKey("xmlns:xml")) {
                                    v_dic.Add("xmlns:xml",CoreXSDConstant.XML_XSD);
                                }
                            
                                listener.LoadSchemaInfo(v_dic.GetEnumerator ());

                                r_r = true;
                            }
                            continue;
                        }
                        if ((v_stackReader.Count ==0) && (_xr.Depth > 1))
                            continue;
                        switch (v_nodeName)
                        {
                            case "xs:restriction":
                                _skipd();
                                listener.LoadRestriction(_xr);
                                break;
                            case "xs:import":
                                _skipd();
                                var di = _xr.GetAttributesDictionary();
                                var v_ns= di.GetValue<string,string>("namespace", "");
                                var v_nl = di.GetValue<string,string>("schemalocation", "");

                                listener.ResolvImport(v_ns,v_nl);
                                break;
                            case "xs:simpleType":
                                {
                                    v_n = _xr.GetAttribute("name");
                                    if (!string.IsNullOrEmpty(v_n))
                                    {
                                        _skipd();
                                        listener.LoadSimpleType(v_n, _xr);
                                    }
                                }
                                break;
                            case "xs:attributeGroup":
                                v_n = _xr.GetAttribute("name");
                                if (!string.IsNullOrEmpty(v_n))
                                {
                                    _skipd();
                                    listener.LoadAttributeGroup(v_n, _xr);
                                }
                                else {
                                    v_n = GetName(_xr.GetAttribute("ref"));
                                    if (!string.IsNullOrEmpty(v_n))
                                    {
                                        _skipd ();
                                        listener.LoadAttributeGroup("ref:/" + v_n, _xr);
                                    }
                                }
                                break;
                            case "xs:simpleContent":
                                _skipd();
                                listener.LoadSimpleContent(_xr);
                                break;
                            case "xs:complexContent":
                                _skipd();
                                listener.LoadComplexContent(_xr);
                                break;
                            //case "xs:extension":
                            //    break;
                            case "xs:list":
                                _skipd();
                                listener.LoadList(_xr);

                                break;
                            //case "xs:item":
                            //    break;
                            //case "xs:pattern":
                            //    break;
                            //case "xs:enumeration":
                            //    break;
                            //case "xs:minLength":
                            //    break;
                            //case "xs:maxLength":
                            //    break;
                            //case "xs:maxInclusive":
                            //    break;
                            //case "xs:minInclusive":
                            //    break;
                            case "xs:union":
                                var tab = _xr.GetAttribute("memberTypes").Split(' ');
                                _skipd();
                                listener.LoadUnion(tab, _xr);
                                break;
                            //case "xs:complexContent":
                            //    break;
                            //case "xs:maxExclusive":
                            //    break;
                            //case "xs:minExclusive":
                            //    break;
                            //case "xs:any":
                            //break;
                            case "xs:attribute":
                                _skipd();
                                v_n = _xr.GetAttribute("name");
                                _xr.MoveToElement();
                                listener.LoadAttribute(v_n, _xr);
                                break;
                            case "xs:sequence":
                                _skipd();
                                listener.LoadSequence(_xr);
                                break;
                            case "xs:choice":
                                _skipd();
                                listener.LoadChoice(_xr);
                                break;
                            case "xs:complexType":
                                {
                                    v_n = _xr.GetAttribute("name");
                                    if (!string.IsNullOrEmpty(v_n))
                                    {
                                        _skipd();
                                        listener.LoadComplexType(v_n, _xr);
                                        //m_types.Add(n, new ColladaType(n, enuColladaType.Complex)
                                        //    .LoadDef(_xr.ReadSubtree()));
                                    }
                                }
                                break;
                            case "xs:group":
                                {
                                  
                                        _skipd();
                                        listener.LoadGroup( _xr);

                                  
                                }
                                break;
                            case "xs:element":
                                {
                                    v_n = _xr.GetAttribute("name");
                                    if (!string.IsNullOrEmpty(v_n))
                                    {
                                        _skipd();
                                        listener.LoadElement(v_n, _xr);
                                    }
                                    else {
                                        v_n = GetName(_xr.GetAttribute ("ref")?.ToString());
                                        //reference a 
                                        if (!string.IsNullOrEmpty(v_n))
                                        {
                                            _skipd();
                                            listener.LoadElement(v_n, _xr);
                                        }

                                    }
                                }
                                break;
                            case "xs:annotation":
                                _skipd();
                                CoreXSDPathLoader defs = new CoreXSDPathLoader();
                                if (_xr.MoveToFirstAttribute())
                                {
                                    do
                                    {
                                        defs[GetName(_xr)] = _xr.Value;
                                    }
                                    while (_xr.MoveToNextAttribute());
                                    _xr.MoveToElement();
                                }
                                while (_xr.Read())
                                {
                                    switch (_xr.NodeType
                                        )
                                    {
                                        case XmlNodeType.Element:
                                            string p = GetName( _xr);//.Name;
                                            defs.AddElement(p,
                                            _xr.ReadElementContentAsString());
                                            break;
                                        default:
                                            break;
                                    }
                                }
                                _xr = v_stackReader.Pop();
                                listener.setAnnotation(
                                    defs["/xs:documentation"],
                                   defs["/xs:appinfo"]);

                                break;
                            default:
                                //render not handle element
                                Console.WriteLine(v_nodeName);
                                break;
                        }
                        break;
                    default:
                        break;
                }

            }
        }

        private static string GetName(XmlReader _xr)
        {
            if (_xr.NamespaceURI == CoreXSDConstant.NAMESPACEURI) {
                return $"xs:{_xr.LocalName}";
            }
            return _xr.LocalName;
        }
        private static string GetName(string r)
        {
            if (r.Contains(":"))
            {
                return r.Split (':').Last();
            }
            return r;
        }
        public static void LoadXSDResource(Assembly assembly, 
            string name,
            ICoreXSDLoaderListener listener)
        {
            var _t = CoreResources.GetResource(assembly, name)?.ToMemoryStream();
            System.Diagnostics.Debug.Assert((_t != null),
                "Must load xsd schema");

            //m_items = new Dictionary<string, ColladaItemBase>();
            //m_types = new Dictionary<string, ColladaType>();
            //m_segments = new Dictionary<string, ColladaPathSegment>();
            XmlReaderSettings setting = new  XmlReaderSettings();
            setting.DtdProcessing= DtdProcessing.Ignore;

            CoreXSDLoader.Load(XmlReader.Create(_t, setting),
                listener);

            _t.Dispose();
        }

        class CoreXSDPathLoader
        {
            Dictionary<string, string> m_value;

            public string this[string key]
            {
                get {
                    if (this.m_value.ContainsKey(key))

                    return this.m_value[key];
                return null;}
                set
                {
                    this.m_value[key] = value;
                }
            }

            public CoreXSDPathLoader()
            {
                    m_value = new Dictionary<string, string> ();
            }

            internal void AddElement(string p, string v)
            {
                string k = $"/{p}";
                if (this.m_value.ContainsKey(k))
                {
                    this.m_value[k]+= "\r\n\r\n##\r\n\r\n" + v;
                }
                else 
                this.m_value.Add (k, v.Replace ("\t", ""));
            }
        }
        }
    
}

