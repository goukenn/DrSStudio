using IGK.ICore.Extensions;
using IGK.ICore.Xml.XSD;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Collections;

namespace IGK.ICore.Xml.XSD
{
    /// <summary>
    /// represent a sxd item
    /// </summary>
    public class CoreXSDItem : ICoreXSDItem
    {
        public object Value { get; set; }
        private ICoreXSDType _type;
        private ICoreXSDItemChain _chain;
        private ICoreXSDItemChainValues _chainvalues;
        private ICoreXSDSourceType _source;
        private Dictionary<string, string> m_attributes;

        public ICoreXSDType Type => _type;
        public ICoreXSDSourceType SourceType => _source;

        private CoreXSDItem()
        {
            this._chainvalues = CreateChainValueCollection();
            m_attributes = new Dictionary<string, string> ();
        }

     
        protected virtual ICoreXSDItemChainValues CreateChainValueCollection()
        {
            return new ChainValueCollections(this);
        }
        /// <summary>
        /// create an item from type
        /// </summary>
        /// <param name="source"></param>
        /// <param name="g"></param>
        /// <returns></returns>
        public static CoreXSDItem Create(ICoreXSDSourceType source, ICoreXSDType g)
        {
            if (g == null)
                return null;
            CoreXSDItem t = new CoreXSDItem();
            t._type = g;
            t._source = source;
            //build or init chain of xsd type
            t._chain = g.GetChain(source);
            return t;
        }
        private void SetAttribute(string attrName, string value) {
            if (m_attributes.ContainsKey(attrName))
            {
                if (string.IsNullOrEmpty (value))
                    m_attributes.Remove(attrName );
                else 
                    m_attributes[attrName] = value;

            }
            else if (!string.IsNullOrEmpty (value)){
                m_attributes.Add(attrName , value );
            }
        }

        public void SetAttribute(string name, string attrName, string value)
        {
            if (name == null) {
                //for current attribute
                if (this.Type.HasAttribute) {
                   var gttr =  this.Type.GetAttribute (attrName);
                   
                    if (gttr != null && _CheckResctricition(gttr, value)) {
                        this.SetAttribute (attrName, value );
                    }
                } 
                //set attribute of this item;
                return ;
            }
            if (this._chain.ContainKey(name))
            {
                this._chainvalues.SetAttributeValue(name, attrName, value);
            }
            else if (
                name.Contains("/"))
            {
                //build to chain and set the value
                string[] r = name.Split('/');
                for (int i = 0; i < r.Length; i++)
                {
                    if (this._chain.ContainKey(r[i]))
                    {
                        string g = string.Join("/", r.Slice(i + 1, r.Length - i - 1));

                        //string[] rt = new string[r.Length - i - 1];
                        //Array.Copy(r, i + 1, rt, 0, rt.Length);
                        //string g = string.Join("/", rt);//r.Slice
                        if (_chain[r[i]].TypeName == null)
                            return;

                        var m = (this._chainvalues.GetValue(r[i])) as ICoreXSDItemValue;

                        m.SetAttribute(g, attrName, value);
                        //m
                        //     as CoreXSDItem).SetAttribute(g, attrName, value);

                        break;
                        // _chain[r[i]]. .setValue(g, value );

                    }
                }
            }
        }



        public static void StoreItem(CoreXSDItem item, CoreXmlElement e)
        {
            List<string> list = new List<string>();

            if (item.Type.HasAttribute)
            {
                foreach (KeyValuePair<string, string> r in item.m_attributes)
                {
                    e.SetAttribute(r.Key, r.Value);
                }
            }           
            if (item.CheckChain(list, e.TagName))
            {

                //store attributes
                string v_tagname = string.Empty;
                foreach (KeyValuePair<string, ICoreXSDItemValue> k in item.ChainValues())
                {
                    v_tagname = k.Value.Name;
                    var n = e.Add(v_tagname);
                    __storeValue(k.Value, n);

                    if (k.Value is CoreXSDItemValue)
                    {

                        var v_v = k.Value as CoreXSDItemValue;
                        if ((v_v.ChainValueCount > 0) && (v_v.ChainInfo.IsInSequence))
                        {
                            //  CoreXSDDummyItemChainValue dummy = new CoreXSDDummyItemChainValue (item);
                            //store chain
                            for (int i = 0; i < v_v.ChainValueCount; i++)
                            {
                                var r = v_v.GetItemValue(i);
                                // list.Clear();
                                //dummy.SetValue(r);


                                if ((r.Value as CoreXSDItem)?.CheckChain(list, v_tagname) == true)
                                {
                                    //if ((r.ChainInfo as CoreXSDItem).CheckChain(list))
                                    //{
                                    n = e.Add(v_tagname);
                                    __storeValue(r, n);
                                    //}
                                }
                            }

                        }
                        continue;

                    }
                }
            }
            if (list.Count > 0) {
                throw new Exception(string.Join("\n", list.ToArray()));
            }
        }

        private static void __storeValue(ICoreXSDItemValue value, CoreXmlElement n)
        {

            if (value.HasAttribute)
            {
                foreach (KeyValuePair<string, string> r in value.GetAttributes())
                {
                    n.SetAttribute(r.Key, r.Value);
                }

            }
            if (value is Array)
            {
            }
            else
            {

                if (value.Value is CoreXSDItem)
                {
                    StoreItem(value.Value as CoreXSDItem, n);
                }
                else
                    n.setContent(value.Value);
            }


        }

        public IEnumerable<KeyValuePair<string, ICoreXSDItemValue>> GetAttributes()
        {
            return new Dictionary<string, ICoreXSDItemValue>();
        }

        /// <summary>
        /// Add a value definition file
        /// </summary>
        /// <param name="name">path name to add</param>
        /// <param name="value"></param>
        /// <returns></returns>
        public ICoreXSDItemValue AddValue(string name, string value = null) {
            ICoreXSDItemValue outf = null;
            this._addValue(name, value, true,ref outf);
            // ICoreXSDItemValue r =  this..GetValue(name);
          // var r =  this._chainvalues.GetValue (name);
            return outf;
            //  return null;
        }
        private void _addValue(string name, string value, bool append)
        {
        }
        private void _addValue(string name, string value, bool append, ref ICoreXSDItemValue outf )
        {
            var q = this;
            string v_n = name ;
            bool v_set = true;
            if (name.Contains("/"))
            {
                //build to chain and set the value
                string[] r = name.Split('/');
                v_n = r[r.Length - 1];//get the last
                for (int i = 0; i < r.Length - 1; i++)
                {
                    if (q._chain.ContainKey(r[i]))
                    {
                        //string[] rt = new string[r.Length - i - 1];
                        //Array.Copy(r, i + 1, rt, 0, rt.Length);
                        //string g = string.Join("/", rt);//r.Slice
                        //if (_chain[r[i]].TypeName == null)
                        //{
                        //   (this._chainvalues.GetValue(r[i])
                        //   as ICoreXSDItemValue).SetValue(g, value);
                        //   // return;
                        //}

                        ICoreXSDItemValue rtt = q._chainvalues.GetValue(r[i]) as ICoreXSDItemValue;
                        q = rtt.Value as CoreXSDItem;
                        //rtt

                        //     (q._chainvalues.GetValue(r[i])
                        //    as ICoreXSDItemValue).SetValue(g, value);
                    }                       // _chain[r[i]]. .setValue(g, value );
                    else
                    {
                        v_set = false;
                        break;
                    }
                }
            }
            if (v_set && (q is CoreXSDItem) && q._chain.ContainKey(v_n))
            {
                if (CheckValue(q._chain[v_n], this._source, value))
                {
                    q._chainvalues.SetValue(v_n, value, append);
                    outf = q._chainvalues.GetValue(v_n) as ICoreXSDItemValue;
                }
#if DEBUG
                else
                    System.Diagnostics.Debug.WriteLine($"element property \"{name}\" does not support that value : {value}");
#endif
            }


//            if (this._chain.ContainKey(name))
//            {
//                if (CheckValue(this._chain[name], this._source, value))
//                {
//                    this._chainvalues.SetValue(name, value, append);
//                    outf = this._chainvalues.GetValue(name) as ICoreXSDItemValue;
//                }
//#if DEBUG
//                else
//                    System.Diagnostics.Debug.WriteLine($"element property \"{name}\" does not support that value : {value}");
//#endif
//            }
//            else if (
//                name.Contains("/"))
//            {
//                //build to chain and set the value
//                string[] r = name.Split('/');
//                var q  = this;
//                bool v_set = true;
//                for (int i = 0; i < r.Length-1; i++)
//                {
//                    if (q._chain.ContainKey(r[i]))
//                    {
//                        //string[] rt = new string[r.Length - i - 1];
//                        //Array.Copy(r, i + 1, rt, 0, rt.Length);
//                        //string g = string.Join("/", rt);//r.Slice
//                        //if (_chain[r[i]].TypeName == null)
//                        //{
//                        //   (this._chainvalues.GetValue(r[i])
//                        //   as ICoreXSDItemValue).SetValue(g, value);
//                        //   // return;
//                        //}

//                        ICoreXSDItemValue rtt = q._chainvalues.GetValue(r[i]) as ICoreXSDItemValue;
//                        q = rtt.Value as CoreXSDItem;
//                        //rtt

//                        //     (q._chainvalues.GetValue(r[i])
//                        //    as ICoreXSDItemValue).SetValue(g, value);
//                    }                       // _chain[r[i]]. .setValue(g, value );
//                    else {
//                       v_set = false;
//                        break ;
//                    }
//                }
//                if (v_set && (q is CoreXSDItem)) {

//                    //  (q as CoreXSDItem).SetValue (r[r.Length], value);
//                    string n = r[r.Length-1];
//                    if (q._chain.ContainKey(n))
//                    {
//                        if (CheckValue(q._chain[n], this._source, value))
//                        {
//                            q._chainvalues.SetValue(n, value, append);
//                            outf = q._chainvalues.GetValue(n) as ICoreXSDItemValue;
//                        }
//                    }

//                    //this._chainvalues.SetValue(name, value, append);
//                    //outf = this._chainvalues.GetValue(name) as ICoreXSDItemValue;


//                }

//                //for (int i = 0; i < r.Length; i++)
//                //{
//                //    if (this._chain.ContainKey(r[i]))
//                //    {
//                //        string[] rt = new string[r.Length - i - 1];
//                //        Array.Copy(r, i + 1, rt, 0, rt.Length);
//                //        string g = string.Join("/", rt);//r.Slice
//                //        //if (_chain[r[i]].TypeName == null)
//                //        //{
//                //        //   (this._chainvalues.GetValue(r[i])
//                //        //   as ICoreXSDItemValue).SetValue(g, value);
//                //        //   // return;
//                //        //}
//                //        (this._chainvalues.GetValue(r[i])
//                //            as ICoreXSDItemValue).SetValue(g, value);
//                //    }
//                //    break;                        // _chain[r[i]]. .setValue(g, value );
//                //}
//            }
          
        }
        /// <summary>
        /// set only value
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        public void SetValue(string name, string value)
        {
            ICoreXSDItemValue outf=null;
             this._addValue(name, value, false, ref outf);
        }

        public static ICoreXSDRestrictionInfo CreateRestrictionInfo(string baseType, IEnumerable<KeyValuePair<string, object>> r)
        {
            return new CoreXSDRestrictionInfo(baseType, r);
        }

        private static bool CheckValue(ICoreXSDType t, ICoreXSDSourceType _source, string value)
        {
            if (t.TypeName == null)
                return true;

            if (_source.ContainsType(t.TypeName))
            {
                var v_info = _source.GetTypeInfo(t.TypeName);
                return Support(v_info, value);
            }
            return true;
        }

        /// <summary>
        /// check restriction on attribute value
        /// </summary>
        /// <param name="e"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        private static bool _CheckResctricition(ICoreXSDAttribute e, string value)
        {
            if (e != null)
            {
                if (!Support(CoreXSDRestrictionInfo.GetRestricData(e.Type), value))
                {
#if DEBUG

                    CoreLog.WriteDebug($"attribute <{e.Name}> does't support that <{value}> , {e.Type} required");
#endif
                    return false;
                }

            }
            return true;
        }

        private static bool Support(ICoreXSDRestrictType v_info, string value)
        {
            //var e =  ( v_info as ICoreXSDDeclaringType)?.XSDType;
            if (v_info?.HasRestriction == true) {
                var ee = v_info.GetRestriction();
                bool r = true;
                foreach (KeyValuePair<string, object> item in ee)
                {
                    switch (item.Key) {

                        case CoreXSDConstant.ENUM_XS_PATTERN:                           
                            r =  r && Regex.IsMatch(value, item.Value as string);
                            break;
                        case CoreXSDConstant.ENUM_XS_ENUMERATION:
                            r = r && (item.Value as List<string>).Contains(value);//!=null;
                            break;
                        case CoreXSDConstant.ENUM_XS_MINMAXLENGTH:
                            r = r && (item.Value as CoreXSDMinMaxEvaluator).Match(value);
                            break;
                        case CoreXSDConstant.ENUM_XS_MINMAXINCLUSIVE:
                        case CoreXSDConstant.ENUM_XS_MINMAXEXCLUSIVE:
                            r = r && (item.Value as ICoreXSDRestrictionEvaluator).Match(value);
                            break ;

                    }
                    if (!r)
                        break;

                }
                return r;
                //if (e != null)
                //{
                //    switch (e)
                //    {
                //        case enuXSDType.Simple:
                //            v_info.
                //            break;
                //        default:
                //            break;
                //    }
                //}
            }
            // v_info.i
            return true;
        }

        public bool CheckChain(List<string> list, string name)
        {
            return _chain.Check(list, this._chainvalues, _source, name);

        }

        public IEnumerable<KeyValuePair<string, ICoreXSDItemValue>> ChainValues()
        {
            ///order chain keys
            Dictionary<string, ICoreXSDItemValue> v_dic = new Dictionary<string, ICoreXSDItemValue>();
            foreach (string k in this._chain.ChainKeys) {
                if (this._chainvalues.Contains(k)) {
                    v_dic.Add(k, this._chainvalues.GetValue(k) as ICoreXSDItemValue);
                }
            }

            return v_dic;//this._chainvalues.GetEnumerator();
        }

        class ChainValueCollections : ICoreXSDItemChainValues
        {
            private CoreXSDItem m_owner;
            Dictionary<string, CoreXSDItemValue> m_values;
            public ChainValueCollections(CoreXSDItem owner)
            {
                this.m_owner = owner;
                this.m_values = new Dictionary<string, CoreXSDItemValue>();
            }

            public bool Contains(string key)
            {
                return this.m_values.ContainsKey(key);
            }

            public IEnumerable<KeyValuePair<string, CoreXSDItemValue>> GetEnumerator()
            {
                return this.m_values;
            }

            public object GetValue(string v)
            {
                if (this.m_values.ContainsKey(v))
                    return m_values[v];

                

                var s = this.m_owner._source.CreateItem(this.m_owner._chain[v]) as CoreXSDItem;
                CoreXSDItemValue sr = new CoreXSDItemValue(v, m_owner, s);
                // sr.Value = s;
                this.m_values.Add(v, sr);
                return sr;
            }

            public void SetAttributeValue(string name, string attrName, string value)
            {
                CoreXSDItemValue g = null;               
                if (this.m_values.ContainsKey(name))
                {
                    g = this.m_values[name];
                    //[attrName] = value;
                }
                else
                {
                    //create a value 
                    g = this.GetValue(name) as CoreXSDItemValue;
                }

                var e = (g.Value as CoreXSDItem)?.Type.GetAttribute(attrName);

                if (_CheckResctricition(e, value))
                {
                    g[attrName] = value;
                }
             
                //this.m_values.Add (name, g);           
        }

         
        public void SetValue(string key, object value, bool append)
        {
            if (this.m_values.ContainsKey(key))
            {
                if (!append) {
                    this.m_values[key].Value = value;
                    return;
                }

                var _ggg = this.m_values[key];
                if (_ggg.CanAdd)
                {
                    CoreXSDItemValue g = new CoreXSDItemValue(key, this.m_owner, value);
                    _ggg.AddValueInChain(g);
                    if (g.ChainInfo.IsInChoice) {
                        this.m_values.Add($"{key}{g.ChainValueCount}", g);

                    }
                }
                //if support multiple value

                //if not 

            }
            else {
                CoreXSDItemValue g = new CoreXSDItemValue(key, this.m_owner, value);
                // g.Value = value ;
                this.m_values.Add(key, g);
            }
        }

        IEnumerable<KeyValuePair<string, ICoreXSDItemValue>> ICoreXSDItemChainValues.GetEnumerator()
        {
            Dictionary<string, ICoreXSDItemValue> g = new Dictionary<string, ICoreXSDItemValue>();
            foreach (var item in this.m_values)
            {
                g.Add(item.Key, item.Value);
            }
            return g;
        }
    }


    struct CoreXSDChainInfo
    {
        public ICoreXSDType Item;
        public int Index;

        public CoreXSDChainInfo(ICoreXSDType s, int v)
        {
            this.Item = s;
            this.Index = v;
        }
    }

    public static ICoreXSDItemChain BuildChain(ICoreXSDType e, 
        ICoreXSDSourceType sourceType,
        CoreXSDItemLoadCallback callback = null)
    {
        if (e == null)
            throw new ArgumentNullException($" argument is null {nameof(e)}");

        ICoreXSDItemChain chain = null;

        var s = e;
        Queue<CoreXSDChainInfo> ee = new Queue<CoreXSDChainInfo>();

        ee.Enqueue(new CoreXSDChainInfo(s, 0));
        ICoreXSDType item = null;
        string v_key = string.Empty;
        Dictionary<string, ICoreXSDType> gdata = new Dictionary<string, ICoreXSDType>();
            ICoreXSDType[] g = null;
        while (ee.Count > 0)
        {
            var v_e = ee.Dequeue();

                if (v_e.Item.TypeName == null)
                {
                    g = v_e.Item.Items;
                }
                else {
                    string v_ = sourceType.GetTypeName(v_e.Item.TypeName);
                    if (sourceType.ContainsType(v_))
                    {
                       g = sourceType.GetItemType(v_).Items;
                    }
                    else {
                        CoreLog.WriteDebug ("Does not containt type");
                        continue;
                    }
                }
                    for (int i = v_e.Index; i < g.Length; i++)
                    {
                        item = g[i];

                        if (item is ICoreXSDItemContainer)
                        {

                            ee.Enqueue(new CoreXSDChainInfo(item, 0));
                            ee.Enqueue(new CoreXSDChainInfo(v_e.Item, i + 1));
                            break;
                        }

                        if (callback != null)
                        {

                            callback(item);
                        }
                        gdata.Add(item.Name, item);

                        //v_key = Path.Combine(ColladaConstants.ROOT_NODE, item.Name);
                        //if (this.m_entries.ContainsKey(v_key))
                        //{
                        //    gdata.Add(item);
                        //    this.m_entries[v_key].saveData(e.Add(item.Name));

                        //}
                    }
               
        }


        chain = new CoreXSDChain(e, gdata);
        return chain;
    }

    class CoreXSDChain : ICoreXSDItemChain
    {
        private Dictionary<string, ICoreXSDType> m_dic;
        private ICoreXSDType m_item;

        public CoreXSDChain(ICoreXSDType item, Dictionary<string, ICoreXSDType> gdata)
        {
            this.m_item = item;
            this.m_dic = gdata;
        }

        public ICoreXSDType this[string n]
        {
            get
            {
                if (this.ContainKey(n))
                    return this.m_dic[n];
                return null;
            }
        }

        public string[] ChainKeys
        {
            get
            {
                return this.m_dic.Keys.ToArray();
            }
        }

        public bool Check(List<string> list, ICoreXSDItemChainValues _chainvalues, 
            ICoreXSDSourceType sourceType,
            string parentName)
        {

            bool v_ctn;
            foreach (var item in this.m_dic)
            {
                v_ctn = _chainvalues.Contains(item.Key);
                if (item.Value.MinOccurs > 0) {

                    if (!v_ctn) {
                        if (!((item.Value.IsInChoice) && (item.Value.Parent.MinOccurs == 0)))
                        {
                            list.Add($"\"{parentName}\" must save at least one item of \"{item.Key}\"");
                            return false;
                        }

                    }
                }

                //check attributes
                if (v_ctn) {
                    ICoreXSDType g = null;
                    if ((item.Value.TypeName != null) && (sourceType.ContainsType(item.Value.TypeName)))
                    {
                        g = sourceType.GetTypeInfo(item.Value.TypeName) as ICoreXSDType;
                    }
                    else
                        g = item.Value;

                    if (g?.HasAttribute == true)
                    {
                        foreach (ICoreXSDAttribute attr in g.GetAttributes())
                        {

                            if (attr.IsRequired)
                            {
                                var gg = _chainvalues.GetValue(item.Key) as CoreXSDItemValue;
                                if (!gg.ContainAttribute(attr.Name))
                                {

                                    list.Add($"you must first set the attribute <{attr.Name}> of <{item.Key}>");
                                    return false;
                                }
                            }
                        }
                    }


                }
            }
            return true;
        }

        public bool ContainKey(string name)
        {
            return this.m_dic.ContainsKey(name);
        }
    }


    public sealed class CoreXSDItemValue : ICoreXSDItemValue {
        Dictionary<string, string> m_attributes;
        private CoreXSDItem m_owner;
        private object m_value;
        private bool _xsd_value;
        private string m_name;

        public string Name => m_name;
        public CoreXSDItem Owner => m_owner;
        public object Value { get {
                return this.m_value;
            } set {
                if (_xsd_value) {
                    return;
                }
                this.m_value = value;
            } }

        public bool HasAttribute => this.m_attributes.Count > 0;

        ICoreXSDItem ICoreXSDItemValue.Owner
        {
            get
            {
                return this.m_owner;
            }
        }

        public string this[string k] {
            get {
                return this.m_attributes[k];
            }
            set {
                if (this.m_attributes.ContainsKey(k))
                {
                    if (value == null)
                        this.m_attributes.Remove(k);
                    else
                        this.m_attributes[k] = value;
                } else
                    this.m_attributes.Add(k, value);

            }
        }

        CoreXSDItemValue _parent;
        private List<CoreXSDItemValue> m_valueChains;
        internal int ChainValueCount => m_valueChains.Count;

        public ICoreXSDType ChainInfo => this.m_owner._chain[this.Name];

        public bool CanAdd {
            get {
                if ((_parent == null))
                {
                    var einfo = ChainInfo;

                    if (einfo.MaxOccurs == -1)
                        return true;
                    return (1 + m_valueChains.Count) < einfo.MaxOccurs;
                }
                return false;

            }

        }
        internal void AddValueInChain(CoreXSDItemValue v) {
            if (!this.m_valueChains.Contains(v) && this.CanAdd)
            {
                this.m_valueChains.Add(v);
                v._parent = this;
            }
        }
        internal CoreXSDItemValue GetItemValue(int i)
        {
            return this.m_valueChains[i];
        }

        internal CoreXSDItemValue(string name, CoreXSDItem m_owner, object value)
        {
            this.m_name = name;
            this.m_owner = m_owner;
            this.m_value = value;
            this.m_attributes = new Dictionary<string, string>();
            this._xsd_value = value is CoreXSDItem;
            this.m_valueChains = new List<CoreXSDItemValue>();
        }

        public ICoreXSDItemValue SetValue(string name, string value)
        {
            CoreXSDItem v_i = __getXSDItemValue();
            v_i?.SetValue(name, value);
            //if (this.m_value is CoreXSDItem)
            //    (this.m_value as CoreXSDItem).SetValue(name, value);
            //else
            //{
            //    if (!_xsd_value) {

            //       // var _newdata = new CoreXSDItemValue(this.Name, this.m_owner,

            //    }
            //    var s = this.m_owner._source.CreateItem(this.m_owner._chain[this.Name]) as CoreXSDItem;
            //    s.Value = this.Value;
            //    this.Value = s;
            //    s.SetValue (name, value );

            //    //CoreXSDItemValue sr = new CoreXSDItemValue(v, m_owner, s);
            //    //var i =  this.m_owner._chainvalues.GetValue(this.Name ); //.SetValue(name, value);
            //}
            return this;
        }

        private CoreXSDItem __getXSDItemValue()
        {

            if (this.m_value is CoreXSDItem)
                return (this.m_value as CoreXSDItem);
            else
            {
                if (!_xsd_value)
                {

                    // var _newdata = new CoreXSDItemValue(this.Name, this.m_owner,

                }
                if (string.IsNullOrEmpty (this.Name))
                        return null;
                var s = this.m_owner._source.CreateItem(this.m_owner._chain[this.Name]) as CoreXSDItem;
                s.Value = this.Value;
                this.Value = s;
                return s;
                //   s.SetValue(name, value);

                //CoreXSDItemValue sr = new CoreXSDItemValue(v, m_owner, s);
                //var i =  this.m_owner._chainvalues.GetValue(this.Name ); //.SetValue(name, value);
            }
        }

        public ICoreXSDItemValue SetAttribute(string name, string attrName, string value)
        {
            __getXSDItemValue()?.SetAttribute(name, attrName, value);
                return this;
            //if (this.m_value is CoreXSDItem)
            //(this.m_value as CoreXSDItem).SetAttribute(name, attrName, value);
        }

        public IEnumerable<KeyValuePair<string, string>> GetAttributes()
        {
            return this.m_attributes;
        }

        internal bool ContainAttribute(string name)
        {
            return this.m_attributes.ContainsKey(name);
        }


    }

    private class CoreXSDRestrictionInfo : IEnumerable<KeyValuePair<string, object>>, ICoreXSDRestrictionInfo
    {
        private static Dictionary<string, ICoreXSDRestricAttributeData> sm_restricdata;
        private string baseType;
        private IEnumerable<KeyValuePair<string, object>> r;

        public CoreXSDRestrictionInfo(string baseType, IEnumerable<KeyValuePair<string, object>> r)
        {
            this.baseType = baseType;

                Dictionary<string, object > g = new Dictionary<string, object> ();

                foreach (var item in r)
                {
                    try
                    {
                        switch (item.Key)
                        {
                            case CoreXSDConstant.ENUM_XS_MININCLUSIVE:
                                if (!g.ContainsKey(CoreXSDConstant.ENUM_XS_MINMAXINCLUSIVE))
                                {
                                    CoreXSDMinMaxInclusiveEvaluator m = new CoreXSDMinMaxInclusiveEvaluator();
                                    g.Add(CoreXSDConstant.ENUM_XS_MINMAXINCLUSIVE, m);
                                }
                                (g[CoreXSDConstant.ENUM_XS_MINMAXINCLUSIVE] as CoreXSDMinMaxInclusiveEvaluator).MinInclude = float.Parse(item.Value.ToString());

                                break;
                            case CoreXSDConstant.ENUM_XS_MAXINCLUSIVE:
                                if (!g.ContainsKey(CoreXSDConstant.ENUM_XS_MINMAXINCLUSIVE))
                                {
                                    CoreXSDMinMaxInclusiveEvaluator m = new CoreXSDMinMaxInclusiveEvaluator();
                                    g.Add(CoreXSDConstant.ENUM_XS_MINMAXINCLUSIVE, m);
                                }
                                (g[CoreXSDConstant.ENUM_XS_MINMAXINCLUSIVE] as CoreXSDMinMaxInclusiveEvaluator).MaxInclude = float.Parse(item.Value.ToString());

                                break;
                            case CoreXSDConstant.ENUM_XS_MAXEXCLUSIVE:
                                if (!g.ContainsKey(CoreXSDConstant.ENUM_XS_MINMAXEXCLUSIVE))
                                {
                                    CoreXSDMinMaxExclusiveEvaluator m = new CoreXSDMinMaxExclusiveEvaluator();
                                    g.Add(CoreXSDConstant.ENUM_XS_MINMAXEXCLUSIVE, m);
                                }
                                (g[CoreXSDConstant.ENUM_XS_MINMAXEXCLUSIVE] as CoreXSDMinMaxExclusiveEvaluator).MaxExclude = float.Parse(item.Value.ToString());

                                break;
                            case "xs:minLength":
                                if (g.ContainsKey(CoreXSDConstant.ENUM_XS_MINMAXLENGTH))
                                {
                                    (g[CoreXSDConstant.ENUM_XS_MINMAXLENGTH] as CoreXSDMinMaxEvaluator).Min = Int32.Parse(item.Value.ToString());
                                }
                                else
                                {
                                    CoreXSDMinMaxEvaluator m = new CoreXSDMinMaxEvaluator();
                                    m.Min = Int32.Parse(item.Value.ToString());
                                    g.Add(CoreXSDConstant.ENUM_XS_MINMAXLENGTH, m);
                                }
                                break;
                            case "xs:maxLength":
                                if (!g.ContainsKey(CoreXSDConstant.ENUM_XS_MINMAXLENGTH))
                                {
                                    CoreXSDMinMaxEvaluator m = new CoreXSDMinMaxEvaluator();
                                    g.Add(CoreXSDConstant.ENUM_XS_MINMAXLENGTH, m);
                                }
                                (g[CoreXSDConstant.ENUM_XS_MINMAXLENGTH] as CoreXSDMinMaxEvaluator).Max = Int32.Parse(item.Value.ToString());

                                break;
                            default:
                              //  Console.WriteLine("Pattern " + item.Key);
                                g.Add(item.Key, item.Value);
                                break;
                        }
                    }
                    catch (Exception ex) {

                        Console.WriteLine (ex.Message );
                    }
                }
           
            this.r = g;
        }

        public string BaseType => baseType;

            /// <summary>
            /// get global restriction data
            /// </summary>
            /// <param name="type"></param>
            /// <returns></returns>
        internal static ICoreXSDRestrictType GetRestricData(string type)
        {
            if (sm_restricdata == null) {
                sm_restricdata = new Dictionary<string, ICoreXSDRestricAttributeData>();
                sm_restricdata["xs:boolean"] = new CoreXSDRescrition().SetPattern("(true|false)");

            }
            return sm_restricdata.ContainsKey(type) ? sm_restricdata[type] : null;
        }

        public IEnumerator<KeyValuePair<string, object>> GetEnumerator()
        {
            return r.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.r.GetEnumerator();
        }
    }
}
}


