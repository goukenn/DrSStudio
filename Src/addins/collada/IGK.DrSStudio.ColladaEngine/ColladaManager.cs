using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IGK.DrSStudio.ColladaEngine.IO;
using System.Xml;
using IGK.ICore.Resources;
using IGK.ICore.IO;
using IGK.ICore;
using System.Reflection;
using System.IO;
using IGK.ICore.Xml;
using IGK.DrSStudio.ColladaEngine;
using IGK.ICore.Xml.XSD;

namespace IGK.DrSStudio.ColladaEngine
{
    class ColladaManager
    {
        //        static Dictionary<string, ColladaType> m_types;
        //        static Dictionary<string , ColladaTypeBase > m_items;
        //        static Dictionary<string,ColladaPathSegment> m_segments;

        //        class ColladaPathSegment {
        //            public ColladaTypeBase Item { get; internal set; }
        //            public string Name { get;set;}
        //            public ColladaPathSegment Parent { get; set; }
        //            public override string ToString()
        //            {
        //                return this.Name;
        //            }
        //        }

        //        class ColladaXmlLoaderListener :  ICoreXSDLoaderListener
        //        {
        //            private Stack<ColladaTypeBase> m_ElementStack;
        //            private bool flags;
        //            private ColladaPathSegment m_currentSegment;

        //            public string CurrentPrefix
        //            {
        //                get
        //                {
        //                    throw new NotImplementedException();
        //                }

        //                set
        //                {
        //                    throw new NotImplementedException();
        //                }
        //            }

        //            public ColladaXmlLoaderListener()
        //            {
        //                m_ElementStack = new Stack<ColladaTypeBase> ();
        //            }

        //            public void LoadComplexType(string n, XmlReader xmlReader)
        //            {
        //                if (m_ElementStack.Count == 0)
        //                {
        //                    ColladaType c = new ColladaType(n, enuXSDType.Complex).LoadDef(xmlReader);
        //                    m_types.Add(n, c);
        //                    this.m_ElementStack.Push (c);
        //                }
        //                else
        //                {
        //                }

        //            }

        //            public void LoadElement(string n, XmlReader xmlReader)
        //            {
        //                var ng = new ColladaTypeItem ();
        //                ng.Name = n;
        //                ng.Infos = xmlReader.GetAttributesDictionary ();

        //                if (this.m_ElementStack.Count > 0)
        //                {
        //                    var parent = this.m_ElementStack.Peek();
        //                    parent.addChild (ng);
        //                    this.m_ElementStack.Push(ng);
        //                    this.loadSegment(n, ng);

        //                }
        //                else
        //                {

        //                    if (m_items.Count == 0)
        //                    {
        //                        m_items.Add(n, ng);
        //                        this.m_ElementStack.Push(ng);
        //                        ColladaPathSegment segment = new ColladaPathSegment ();
        //                        segment.Name  = n;
        //                        segment.Item = ng;
        //                        m_segments.Add(n,segment);
        //                        m_currentSegment = segment;
        //                    }
        //                    else
        //                    {

        //                    }
        //                }



        //            }

        //            private void loadSegment(string n, ColladaTypeBase item)
        //            {

        //                if (this.m_currentSegment != null)
        //                {
        //                    ColladaPathSegment segment = new ColladaPathSegment();
        //                    segment.Name = Path.Combine(this.m_currentSegment.Name, n);
        //                    segment.Parent = this.m_currentSegment;
        //                    segment.Item = item;
        //                    m_segments.Add(segment.Name,segment);
        //                    this.m_currentSegment = segment;
        //                }
        //            }

        //            public void ExitElement() {
        //                if (!flags && this.m_ElementStack.Count > 0)
        //                {
        //                    var g = this.m_ElementStack.Pop();
        //                    if ((this.m_currentSegment != null)&&(this.m_currentSegment.Item == g))
        //                        this.m_currentSegment = this.m_currentSegment.Parent;

        //                }
        //                flags = false;
        //            }

        //            public void LoadGroup( XmlReader xmlReader)
        //            {
        //                string n = xmlReader.GetAttribute ("name");
        //                if (!string.IsNullOrEmpty(n))
        //                {
        //                    var g = new ColladaType(n, enuXSDType.Group).LoadDef(xmlReader);


        //                    g .Infos  = xmlReader.GetAttributesDictionary ();


        //                    m_types.Add(n, g);
        //                    this.PushItem(g);
        //                }
        //                else
        //                {
        //                    n = xmlReader.GetAttribute("ref");
        //                    var g = new ColladaType($"ref:/{n}", enuXSDType.Group).LoadDef(xmlReader);
        //                    m_ElementStack.Peek().addChild (g);
        //                    flags = true ;
        //                }



        //            }

        //            public void LoadSimpleType(string n, XmlReader xmlReader)
        //            {
        //                if (this.m_ElementStack.Count == 0)
        //                {
        //                    var h = new ColladaType(n, enuXSDType.Simple).LoadDef(xmlReader);
        //                    this.PushItem (h);
        //                    m_types.Add(n, h);
        //                }
        //                else {

        //                }

        //            }

        //            public void setAnnotation(string documentation, string appInfo)
        //            {
        //               var r = (this.m_ElementStack.Count >0)? this.m_ElementStack.Peek ():null;
        //                if (r==null)
        //                    return ;
        //                r.Annotation.Description = documentation;
        //                r.Annotation.AppInfo = appInfo ;
        //            }

        //            public void LoadSequence(XmlReader _xr)
        //            {
        //                ColladaTypeSequence seq = new ColladaTypeSequence ();
        //               var t =  this.m_ElementStack.Peek ();
        //                t.addChild(seq);// as ColladaItem ).Childs.Add (seq);

        //                //this.loadSegment("sequence");
        //                this.PushItem(seq);
        //            }

        //            public void LoadChoice(XmlReader _xr)
        //            {
        //                if (this.m_ElementStack.Count > 0)
        //                {
        //                    ColladaTypeChoice choice = new ColladaTypeChoice();
        //                    m_ElementStack.Peek().addChild(choice);
        //                    this.PushItem(choice);
        //                    // this.loadSegment ("choice");

        //                    choice.Infos = _xr.GetAttributesDictionary();
        //                }
        //                else {
        //                    System.Diagnostics.Debug.WriteLine("Can't add choice to nothing");
        //                }
        //            }

        //            private void PushItem(ColladaTypeBase item)
        //            {
        //                this.m_ElementStack.Push(item);
        //            }

        //            public void LoadRestriction(XmlReader _xr)
        //            {
        //                Dictionary<string, string > g = _xr.GetAttributesDictionary();
        //                string v_base = g["base"];//?..GetValue<string,string>("base");

        //                Dictionary<string, object > r = new Dictionary<string, object> ();

        //                while (_xr.Read()) {
        //                    switch (_xr.NodeType) {
        //                        case XmlNodeType.Element:
        //                            string n = _xr.Name ;
        //                            if (r.ContainsKey(n))
        //                            {
        //                                if (r[n] is List<string>)
        //                                {
        //                                    (r[n] as List<string>).Add(_xr.GetAttribute("value"));
        //                                }
        //                                else {
        //List <string> gg = new List<string> ();
        //                                    gg.Add (r[n] as string);
        //                                    gg.Add (_xr.GetAttribute("value"));
        //                                    r[n] = gg;

        //                                }

        //                            }
        //                            else {
        //                                r.Add (n , _xr.GetAttribute ("value"));
        //                            }

        //                            break;
        //                    }
        //                }
        //                if (r.Count > 0) {

        //                    this.m_ElementStack.Peek().SetRestriction(v_base, r);//.m_currentSegment .LoadRestriction(r);
        //                }
        //                flags=true;
        //            }

        //            public void LoadAttribute(string v, XmlReader _xr)
        //            {

        //                ColladaTypeAttribute attr = new ColladaTypeAttribute ();
        //                attr.Name = v;
        //                attr.Default = _xr.GetAttribute ("default");
        //                attr.Fixed = _xr.GetAttribute ("fixed");
        //                attr.Form = _xr.GetAttribute ("form");
        //                attr.Id = _xr.GetAttribute ("id");
        //                //attr. = _xr.GetAttribute ("name");
        //                attr.Ref = _xr.GetAttribute ("ref");
        //                attr.Type = _xr.GetAttribute ("type");
        //                attr.Use = _xr.GetAttribute ("use");

        ////                fixed= string
        ////form = qualified | unqualified
        ////id = ID
        ////name = NCName
        ////ref= QName
        ////type = QName
        ////use = optional | prohibited | required


        //                if (this.m_ElementStack.Count > 0) {
        //                    var r = this.m_ElementStack.Peek ();
        //                    r?.addAttribute(attr);
        //                    this.m_ElementStack.Push (attr);
        //                }
        //                else {
        //                }
        //            }

        //            public void LoadList(XmlReader _xr)
        //            {
        //                var d = _xr.GetAttributesDictionary ();
        //                if (_xr.IsEmptyElement )
        //                    flags = true ;

        //                while (_xr.Read()) {

        //                }
        //            }

        //            public void LoadComplexContent(XmlReader _xr)
        //            {
        //                //ignore complex content
        //                _xr.Skip ();
        //                flags = true ;
        //            }

        //            public void LoadSimpleContent(XmlReader _xr)
        //            {
        //                //ignore simple content
        //                _xr.Skip();
        //                flags = true;
        //            }

        //            public void LoadUnion(string[] tab, XmlReader _xr)
        //            {
        //                ColladaTypeUnion u = new ColladaTypeUnion ();
        //                u.Data = tab;
        //                this.m_ElementStack.Peek().addChild (u);
        //                if (_xr.IsEmptyElement)
        //                    flags = true;
        //                else
        //                {
        //                    this.PushItem(u);
        //                }
        //            }

        //            public void LoadAttributeGroup(string v_n, XmlReader _xr)
        //            {
        //                _xr.Skip();
        //                flags = true;
        //            }

        //            public void LoadSchemaInfo(Dictionary<string, string>.Enumerator enumerator)
        //            {

        //            }

        //            public void ResolvImport(string @namespace, string location)
        //            {
        //                //collada not required value for importation
        //            }

        //            public string GetPrefix(string @namespace)
        //            {
        //                throw new NotImplementedException();
        //            }
        //        }


        private static ColladaManager sm_instance;
        private CoreXSDFileLoaderListener m_listener;
        private ICoreXSDSourceType sm_basicSource;

        private ColladaManager()
        {
        }

        public static ColladaManager Instance
        {
            get
            {
                return sm_instance;
            }
        }
        


        internal static ICoreXSDType GetItemType(string typeName)
        {
            return Instance.m_listener.GetItemType (typeName);// m_types[typeName];
        }

        internal static bool ContainsType(string typeName)
        {
            return Instance.m_listener.ContainsType(typeName );// m_types.ContainsKey(typeName);
        }



        internal static ICoreXSDType GetItem(string node)
        {
            return Instance.m_listener.GetSegmentItem(node);// m_segments.ContainsKey(node) ? m_segments[node].Item : null;
        }

        static ColladaManager() {
            //init Engines
            sm_instance = new ColladaManager();
            sm_instance.sm_basicSource =  new ColladaSourceManager();
            //    var _t = CoreResources.GetResource(Assembly.GetExecutingAssembly(), "collada_ref")?.ToMemoryStream();
            //System.Diagnostics.Debug.Assert((_t!=null),
            //    "Must load xsd schema");

            //m_items = new Dictionary<string, ColladaTypeBase> ();
            //m_types = new Dictionary<string, ColladaType>();
            //m_segments = new Dictionary<string,ColladaPathSegment> ();
            sm_instance.m_listener = new CoreXSDFileLoaderListener ();
            CoreXSDLoader.LoadXSDResource(Assembly.GetExecutingAssembly(), 
                "collada_ref",
                  sm_instance.m_listener
                );

#if DEBUG
            sm_instance.m_listener .CheckEnv();
#endif

            //CoreXSDLoader.Load(XmlReader.Create(_t), 


            //    new ColladaXmlLoaderListener());

            //   _t.Dispose();

            /* XmlReader _xr = XmlReader.Create (_t);
             m_types = new Dictionary<string, ColladaType> ();
             bool r_r = false ;
             while (_xr.Read()){

                 switch (_xr.NodeType)
                 {
                     case XmlNodeType.Element:


                         if (!r_r)
                         {
                             if (_xr.Name == "xs:schema") {
                                 r_r = true;
                             }
                             continue;
                         }
                         //if (_xr.Depth>1)
                         //    continue;
                         switch (_xr.Name)
                         {

                             case "xs:simpleType":
                                 {
                                     var n = _xr.GetAttribute("name");
                                     if (!string.IsNullOrEmpty(n))
                                     {
                                         m_types.Add(n, new ColladaType(n, enuColladaType.Simple).LoadDef(_xr.ReadSubtree()));
                                     }
                                 }
                                 break;
                             case "xs:complexType":
                                 {
                                     var n = _xr.GetAttribute("name");
                                     if (!string.IsNullOrEmpty(n))
                                     {
                                         m_types.Add(n, new ColladaType(n, enuColladaType.Complex)
                                             .LoadDef(_xr.ReadSubtree()));
                                     }
                                 }
                                 break;
                             case "xs:group":
                                 {
                                     var n = _xr.GetAttribute("name");
                                     if (!string.IsNullOrEmpty(n))
                                     {
                                         m_types.Add(n, new ColladaType(n, enuColladaType.Group)
                                             .LoadDef(_xr.ReadSubtree()));
                                     }
                                 }
                                 break;
                             case "xs:element":
                                 {
                                     var n = _xr.GetAttribute("name");
                                     if (!string.IsNullOrEmpty(n)) {
                                         _xr.ReadSubtree ();

                                     }
                                 }
                                 break;

                             default:
                                 Console.WriteLine(_xr.Name);
                                 break;
                         }
                         break ;
                     default:
                         break;
                 }

             }

           // XmlElement c = XmlReader.Create (();
           */

        }
        internal static ColladaFile CreateNewFile()
        {
            return new ColladaFile ();
        }

      

        /// <summary>
        /// represent a type name
        /// </summary>
        /// <param name="typeName"></param>
        /// <returns></returns>
        public static CoreXSDItem CreateItem(string typeName) {
           var g =  Instance.m_listener.GetItemType(typeName);
            if (g != null)
            {
                //var g = m_types[typeName];
                return CoreXSDItem.Create(Instance.sm_basicSource, g);
            }
            return null;
        }

        internal static ICoreXSDDeclaringType GetTypeInfo(string typeName)
        {
            return Instance.m_listener.GetItemType (typeName) as ICoreXSDDeclaringType;// null;//m_types[typeName];
        }

        private class ColladaSourceManager : ICoreXSDSourceType
        {
            public bool ContainsType(string typeName)
            {
                return Instance.m_listener.ContainsType(typeName);// false ;//ColladaManager.m_types.ContainsKey(typeName );
            }
            public ICoreXSDType GetItemType(string typeName)
            {
                return null;// ColladaManager.m_types[typeName];
            }

            public object CreateItem(ICoreXSDType coreXSDType)
            {
                if (coreXSDType.TypeName == null)
                {
                    return CoreXSDItem.Create(this, coreXSDType);
                }
                else {
                    return ColladaManager.CreateItem(coreXSDType.TypeName );
                }
            }

           

            public ICoreXSDDeclaringType GetTypeInfo(string typeName)
            {
               return ColladaManager.GetTypeInfo(typeName );
            }

            public string GetTypeName(string typeName)
            {
                if (typeName.Contains(":"))
                {
                    string[] tt = typeName.Split(':');
                    return tt[tt.Length - 1];
                }
                return typeName;

            }
        }

        //internal static object GetType(string typeName)
        //{
        //    return m_items[typeName];
        //}
    }
}
