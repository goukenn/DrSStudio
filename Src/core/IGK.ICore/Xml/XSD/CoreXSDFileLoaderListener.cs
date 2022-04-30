using IGK.ICore.Xml.XSD;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System;

namespace IGK.ICore.Xml
{
    public class CoreXSDFileLoaderListener : 
        ICoreXSDLoaderListener,
        ICoreXSDSourceType
    {
        private Stack<CoreXSDTypeBase> m_ElementStack;
        private bool flags;
        private CoreXSDPathSegment m_currentSegment;
  


       private Dictionary<string, CoreXSDTypeBase> m_types;
       private Dictionary<string, CoreXSDTypeBase> m_items;
       private Dictionary<string, CoreXSDPathSegment> m_segments;
       private Dictionary<string, CoreXSDAttributeGroup> m_attrGroups;

        Dictionary<string, CoreXSDNamespaceInfo> m_nameSpace;
        private ICoreXSDFileImportResolver m_resolver;
        private Dictionary<string, CoreXSDItemAttribute> m_globalAttribs;

        /// <summary>
        /// get the target namespace
        /// </summary>
        public string TargetNamespace { get; protected set; }
        public string CurrentPrefix { get; set; }

      

        class CoreXSDPathSegment
        {
            public ICoreXSDType Item { get; internal set; }
            public string Name { get; internal set; }
            public CoreXSDPathSegment Parent { get; internal set; }
        }

        public void SetResolvImportListener(ICoreXSDFileImportResolver fileResolver)
        {
            this.m_resolver = fileResolver;
        }

        public CoreXSDFileLoaderListener()
        {
            m_types = new Dictionary<string, CoreXSDTypeBase> ();
            m_items = new Dictionary<string, CoreXSDTypeBase> ();
            m_segments = new Dictionary<string, CoreXSDPathSegment> ();
            m_ElementStack = new Stack<CoreXSDTypeBase> ();
            m_attrGroups = new Dictionary<string, CoreXSDAttributeGroup> ();
            m_nameSpace = new Dictionary<string, CoreXSDNamespaceInfo> ();
            m_globalAttribs = new Dictionary<string, CoreXSDItemAttribute> ();
        }

        public ICoreXSDType GetItem(string node)
        {
            if (node.Contains(Path.DirectorySeparatorChar.ToString())) {
                bool r = true;

                var rt = node.Split (Path.DirectorySeparatorChar);
                var i = 0;
                ICoreXSDType q = null;
                string v_k = string.Empty; 
                ICoreXSDItemChain v_chain = null;
                while (r && (i<rt.Length)) {
                    v_k = rt[i];
                    if (m_items.ContainsKey(v_k))
                    {
                        if ((q == null) ||  (v_chain = q.GetChain(this)).ContainKey(v_k))
                        {
                            //get items 
                            q = m_items[rt[i]];
                        }
                        else
                            r = false;

                    }
                    else {
                        r = false;
                    }
                    i++;
                }
               return r ?(v_chain !=null? this.CreateChainReference(v_chain[v_k], q) : q):null;
               
            }

            return  m_items.ContainsKey(node)?m_items[node]:null;
        }

        protected virtual ICoreXSDType CreateChainReference(ICoreXSDType chainType, ICoreXSDType source)
        {
            return new CoreXSDChainReference(chainType , source);
        }

        /// <summary>
        /// create a item info from type
        /// </summary>
        /// <param name="name">Type name</param>
        /// <returns></returns>
        public CoreXSDItem CreateItem(string name)
        {
            var g = this.m_types.ContainsKey(name)? this.m_types[name]:this.GetItem(name);
            if (g != null) {
                
                return  CoreXSDItem.Create(this, g);
            }
           
            //if (m_types.ContainsKey(name))
            //{
            //    var g = m_types[name];
            //    return CoreXSDItem.Create(this, g);
            //}
            return null;
        }

        public void LoadComplexType(string n, XmlReader xmlReader)
        {
            if (m_ElementStack.Count == 0)
            {//load root complex type type
                CoreXSDType c = new CoreXSDType(n, enuXSDType.Complex).LoadDef(xmlReader);
                this._addType(n,c);             
                this.m_ElementStack.Push(c);
            }
            else
            {
            }

        }

        private void _addType(string n, CoreXSDType c)
        {
            m_types.Add(__GetName(n), c);
        }

        public void LoadElement(string n, XmlReader xmlReader)
        {
            var ng = new CoreXSDNode();
            ng.Name = n;
            ng.Infos = xmlReader.GetAttributesDictionary();

            if (this.m_ElementStack.Count > 0)
            {
                var parent = this.m_ElementStack.Peek();
                parent.addChild(ng);
                this.m_ElementStack.Push(ng);
                this.loadSegment(n, ng);

            }
            else
            {
                if (this.m_ElementStack.Count == 0)
                {
                    string pn = __GetName(n); //prefix name
                    m_items.Add(pn, ng);
                    this.m_ElementStack.Push(ng);

                    //init basic segment
                    CoreXSDPathSegment segment = new CoreXSDPathSegment();
                    segment.Name = n;
                    segment.Item = ng;
                    m_segments.Add(pn, segment);
                    m_currentSegment = segment;
                }
                else
                {
                    //m_items.Add(n, ng);
                    //this.m_ElementStack.Push(ng);
                    //CoreXSDPathSegment segment = new CoreXSDPathSegment();
                    //segment.Name = n;
                    //segment.Item = ng;
                    //m_segments.Add(n, segment);
                    //m_currentSegment = segment;
                }
            }



        }
        /// <summary>
        /// resolv name for item
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        protected string __GetName(string n)
        {
            if (!string.IsNullOrEmpty (this.CurrentPrefix ))
                return $"{this.CurrentPrefix}:{n}";
            return n;
        }

        private void loadSegment(string n, ICoreXSDType item)
        {

            if (this.m_currentSegment != null)
            {
                CoreXSDPathSegment segment = new CoreXSDPathSegment();
                segment.Name = Path.Combine(this.m_currentSegment.Name, n);
                segment.Parent = this.m_currentSegment;
                segment.Item = item;
                m_segments.Add(segment.Name, segment);
                this.m_currentSegment = segment;
            }
        }

        public void ExitElement()
        {
            if (!flags && this.m_ElementStack.Count > 0)
            {
                var g = this.m_ElementStack.Pop();
                if ((this.m_currentSegment != null) && (this.m_currentSegment.Item == g))
                    this.m_currentSegment = this.m_currentSegment.Parent;

            }
            flags = false;
        }

        public void LoadGroup(XmlReader xmlReader)
        {
            string n = xmlReader.GetAttribute("name");
            if (!string.IsNullOrEmpty(n))
            {
                var g = new CoreXSDType(n, enuXSDType.Group).LoadDef(xmlReader);
                g.Infos = xmlReader.GetAttributesDictionary();
                this._addType(n, g);
                this.PushItem(g);
            }
            else
            {
                n = xmlReader.GetAttribute("ref");
                var g = new CoreXSDType($"ref:/{n}", enuXSDType.Group).LoadDef(xmlReader);
                g.Infos = xmlReader.GetAttributesDictionary();
                m_ElementStack.Peek().addChild(g);
                flags = true;
            }



        }

        public void LoadSimpleType(string n, XmlReader xmlReader)
        {
            if (this.m_ElementStack.Count == 0)
            {
                var h = new CoreXSDType(n, enuXSDType.Simple).LoadDef(xmlReader);
                this.PushItem(h);
                this._addType(n, h);
            }
            else
            {

            }

        }

        public void setAnnotation(string documentation, string appInfo)
        {
            var r = (this.m_ElementStack.Count > 0) ? this.m_ElementStack.Peek() : null;
            if (r == null)
                return;
            r.Annotation.Description = documentation;
            r.Annotation.AppInfo = appInfo;
        }

        public void LoadSequence(XmlReader _xr)
        {
            CoreXSDItemSequence seq = new CoreXSDItemSequence();
            var t = this.m_ElementStack.Peek();
            t.addChild(seq);// as CoreXSDItem ).Childs.Add (seq);

            //this.loadSegment("sequence");
            this.PushItem(seq);
        }

        public void LoadChoice(XmlReader _xr)
        {
            if (this.m_ElementStack.Count > 0)
            {
                CoreXSDItemChoice choice = new CoreXSDItemChoice();
                m_ElementStack.Peek().addChild(choice);
                this.PushItem(choice);
                // this.loadSegment ("choice");

                choice.Infos = _xr.GetAttributesDictionary();
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("Can't add choice to nothing");
            }
        }

        private void PushItem(CoreXSDTypeBase item)
        {
            this.m_ElementStack.Push(item);
        }

        public void LoadRestriction(XmlReader _xr)
        {
            Dictionary<string, string> g = _xr.GetAttributesDictionary();
            string v_base = g["base"];//?..GetValue<string,string>("base");

            Dictionary<string, object> r = new Dictionary<string, object>();

            while (_xr.Read())
            {
                switch (_xr.NodeType)
                {
                    case XmlNodeType.Element:
                        string n = _xr.Name;
                        if (r.ContainsKey(n))
                        {
                            if (r[n] is List<string>)
                            {
                                (r[n] as List<string>).Add(_xr.GetAttribute("value"));
                            }
                            else
                            {
                                List<string> gg = new List<string>();
                                gg.Add(r[n] as string);
                                gg.Add(_xr.GetAttribute("value"));
                                r[n] = gg;

                            }

                        }
                        else
                        {
                            r.Add(n, _xr.GetAttribute("value"));
                        }

                        break;
                }
            }
            if (r.Count > 0)
            {

                this.m_ElementStack.Peek().SetRestriction(v_base, r);//.m_currentSegment .LoadRestriction(r);
            }
            flags = true;
        }

        public void LoadAttribute(string v, XmlReader _xr)
        {
            if (v == "type") {

            }
            CoreXSDItemAttribute attr = new CoreXSDItemAttribute();
            attr.Name = v==null?v:__GetName(v);
            attr.Default = _xr.GetAttribute("default");
            attr.Fixed = _xr.GetAttribute("fixed");
            attr.Form = _xr.GetAttribute("form");
            attr.Id = _xr.GetAttribute("id");
            //attr. = _xr.GetAttribute ("name");
            attr.Ref = _xr.GetAttribute("ref");
            attr.Type = _xr.GetAttribute("type");
            attr.Use = _xr.GetAttribute("use");

            //                fixed= string
            //form = qualified | unqualified
            //id = ID
            //name = NCName
            //ref= QName
            //type = QName
            //use = optional | prohibited | required


            if (this.m_ElementStack.Count > 0)
            {
                var r = this.m_ElementStack.Peek();
                r?.addAttribute(attr);
                this.m_ElementStack.Push(attr);
            }
            else
            {
#if DEBUG
                //testing 
                if (string.IsNullOrEmpty(v)) {
                }
#endif
                this.m_globalAttribs.Add(__GetName(v),attr);
                this.m_ElementStack.Push(attr);
            }
        }

        public void LoadList(XmlReader _xr)
        {
            var d = _xr.GetAttributesDictionary();
            if (_xr.IsEmptyElement)
                flags = true;
            //skip list
            while (_xr.Read())
            {

            }
        }

        public void LoadComplexContent(XmlReader _xr)
        {
            //ignore complex content
            _xr.Skip();
            flags = true;
        }

        public void LoadSimpleContent(XmlReader _xr)
        {
            //ignore simple content
            _xr.Skip();
            flags = true;
        }

        public void LoadUnion(string[] tab, XmlReader _xr)
        {
            CoreXSDItemUnion u = new CoreXSDItemUnion();
            u.Data = tab;
            this.m_ElementStack.Peek().addChild(u);
            if (_xr.IsEmptyElement)
                flags = true;
            else
            {
                this.PushItem(u);
            }
        }

        public virtual object CreateItem(ICoreXSDType coreXSDType)
        {
            throw new NotImplementedException();
        }

        public virtual bool ContainsType(string typeName)
        {
            return this.m_types.ContainsKey (typeName);
        }

        public virtual ICoreXSDDeclaringType GetTypeInfo(string typeName)
        {
           return  this.m_types[typeName] as ICoreXSDDeclaringType; throw new NotImplementedException();
        }

        public ICoreXSDType GetItemType(string typeName)
        {
           return this.m_types.ContainsKey(typeName)?this.m_types[typeName]:null;
        }

        public virtual string GetTypeName(string typeName)
        {
            if (typeName.Contains(":")) {
                string[] tt = typeName.Split (':');
                return tt[tt.Length - 1];
            }
            return typeName;
        }

        public void LoadAttributeGroup(string v_n, XmlReader _xr)
        {

            if (this.m_ElementStack.Count == 0)
            {
                CoreXSDAttributeGroup v_group = new CoreXSDAttributeGroup(v_n);

                PushItem(v_group);
                this.m_attrGroups.Add(v_n, v_group);
                 
                     

            }
            else
            {
                flags  = true;
                //load attribute ref to element stack
                if (v_n.StartsWith("ref:/"))
                {
                    var rs = this.m_ElementStack.Peek();
                    string gid = v_n.Substring(5);
                    if (this.m_attrGroups.ContainsKey(gid))
                    {
                        var i = this.m_attrGroups[gid].GetAttributes();
                        foreach (CoreXSDItemAttribute item in i)
                        {
                            if (item.Name == null)
                            {
                                //  rs.addAttribute (new CoreXSDItemAttribute (item.Ref));
                                if (item.Ref != null) {
                                    if (this.m_globalAttribs.ContainsKey(item.Ref))
                                    {
                                        rs.addAttribute(this.m_globalAttribs[item.Ref]);
                                    }
                                    else
                                    {
                                        //attribute not founded
                                        var gi = m_types[item.Ref];
                                    }
                                }    

                                continue;
                            }
                            rs.addAttribute(item);
                        }
                    }
                    else
                    {

                    }

                    //  this.m_ElementStack.Peek();

                }
                else
                { //not a reference

                }




            }
        }
#if DEBUG
        public void CheckEnv()
        {
           
        }
#endif

        public ICoreXSDType GetSegmentItem(string node)
        {
            return m_segments.ContainsKey(node) ? m_segments[node].Item : null;
        }

        public void LoadSchemaInfo(Dictionary<string, string>.Enumerator enumerator)
        {
            while (enumerator.MoveNext()) {
                Console.WriteLine (enumerator.Current.Key  + " : "+enumerator.Current.Value );
                string key = enumerator.Current.Key;
                switch (enumerator.Current.Key.ToLower())
                {
                    case "targetnamespace":
                        this.TargetNamespace = enumerator.Current.Value;
                        break ;
                    default:
                        if (key.Contains(":") && key.StartsWith("xmlns"))
                            
                        {
                            var ns = enumerator.Current.Value;
                            var prefix = key.Split(':').Last ();
                            if (!this.m_nameSpace.ContainsKey(ns))
                            {
                                CoreXSDNamespaceInfo inf = new CoreXSDNamespaceInfo();
                                inf.Location = null;
                                inf.NameSpace = ns;
                                inf.Prefix = prefix;
                                this.m_nameSpace.Add(inf.NameSpace, inf);
                            }
                        }

                        break;
                }
            }
            
        }

        /// <summary>
        /// resolv 
        /// </summary>
        /// <param name="namespace"></param>
        /// <param name="location"></param>
        public void ResolvImport(string @namespace, string location)
        {
            if (this.m_resolver !=null)
                this.m_resolver.Resolv(this, @namespace,location);
          
        }
        /// <summary>
        /// get loaded prefix
        /// </summary>
        /// <param name="namespace"></param>
        /// <returns></returns>
        public  string GetPrefix(string @namespace)
        {
            if (this.m_nameSpace.ContainsKey(@namespace)) {
                return this.m_nameSpace[@namespace].Prefix;
            }
            return null;
        }
        
    }
}