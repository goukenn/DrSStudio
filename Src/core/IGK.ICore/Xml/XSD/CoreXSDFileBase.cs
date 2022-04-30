using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.ICore.Xml.XSD
{
    /// <summary>
    /// represent the base class to open/manage a xsd file
    /// </summary>
    public abstract class CoreXSDFileBase : ICoreXSDSourceType
    {
        /// <summary>
        /// represent a global collada files entrie
        /// </summary>
        private Dictionary<string, ICoreXSDFileEntry> m_entries;
        private ICoreXSDManagerListener m_fileManager;
    //    private bool m_throwOnFirstError;

        

        public void SetManagerItemListener(ICoreXSDManagerListener fileManager)
        {
            m_fileManager = fileManager;
        }
        /// <summary>
        /// open an xsd file
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public abstract bool Open (string filename);
        /// <summary>
        /// used to save the data
        /// </summary>
        /// <param name="filename"></param>
        public abstract bool Save(string filename);

        public abstract bool ContainsType(string typeName);

        public abstract object CreateItem(ICoreXSDType coreXSDType);

        public abstract ICoreXSDDeclaringType GetTypeInfo(string typeName);


        public CoreXSDFileBase() {
            this.m_entries = new Dictionary<string, ICoreXSDFileEntry> ();
            //this.m_throwOnFirstError = true;
        }

        protected Dictionary<string, ICoreXSDFileEntry> Entries=> m_entries;


        public Dictionary<ICoreXSDType, int> _getXSDContainerInfoIndex(string v_rootNode, ICoreXSDType s)
        {
            Dictionary<ICoreXSDType, int> v_rtab = new Dictionary<ICoreXSDType, int>();

            Queue<CoreXSDFileEntryInfo> ee = new Queue<CoreXSDFileEntryInfo>();

            ee.Enqueue(new CoreXSDFileEntryInfo(s, 0));

            string v_key = string.Empty;
            ICoreXSDType[] g = null;
            CoreXSDFileEntryInfo v_e;
            int v_index = -1;
            while (ee.Count > 0)
            {
                v_index++;
                 v_e = ee.Dequeue();
                g = CoreXSDUtils.GetXSDTypeItems(this, v_e.Item);

                if (g == null) continue;
                for (int i = v_e.Index; i < g.Length; i++)
                {
                    var item = g[i];
                    if (item is ICoreXSDItemContainer)
                    {
                        v_rtab.Add(item, v_index);
                        ee.Enqueue(new CoreXSDFileEntryInfo(item, 0));//0 for start index
                        ee.Enqueue(new CoreXSDFileEntryInfo(v_e.Item, i + 1)); // i+1 for next index
                        break;
                    }
                    v_index++;


                    v_key = Path.Combine(v_rootNode, item.Name);
                    if (this.m_entries.ContainsKey(v_key))
                    {
                        //gdata.Add(item);
                        //var v_gg = this.m_entries[v_key];
                        //if (v_gg.Item is ICoreXSDItemArray)
                        //{
                        //    foreach (CoreXSDItem v_mm in (v_gg.Item as ICoreXSDItemArray))
                        //    {
                        //        CoreXSDItem.StoreItem(v_mm, e.Add(item.Name));
                        //    }
                        //}
                        //else
                        //{
                        //    v_gg.SaveData(e.Add(item.Name));
                        //}

                    }
                }
            }
            return v_rtab;
        }

        public virtual ICoreXSDType GetItemDefinition(string v)
        {
            return null;
        }

        public  ICoreXSDType[] _getXSDContainer(string v_rootNode, ICoreXSDType s) {
            List<ICoreXSDType> v_rtab = new List<ICoreXSDType> ();

            Queue<CoreXSDFileEntryInfo> ee = new Queue<CoreXSDFileEntryInfo>();

            ee.Enqueue(new CoreXSDFileEntryInfo(s, 0));

            string v_key = string.Empty;
            ICoreXSDType[] g = null;
            CoreXSDFileEntryInfo v_e;
            while (ee.Count > 0)
            {
                v_e = ee.Dequeue();
                g = CoreXSDUtils.GetXSDTypeItems(this, v_e.Item);

                if (g == null) continue;
                for (int i = v_e.Index; i < g.Length; i++)
                {
                    var item = g[i];
                    if (item is ICoreXSDItemContainer)
                    {
                        v_rtab.Add (item);
                        ee.Enqueue(new CoreXSDFileEntryInfo(item, 0));
                        ee.Enqueue(new CoreXSDFileEntryInfo(v_e.Item, i + 1));
                        break;
                    }



                    v_key = Path.Combine(v_rootNode, item.Name);
                    if (this.m_entries.ContainsKey(v_key))
                    {
                        //gdata.Add(item);
                        //var v_gg = this.m_entries[v_key];
                        //if (v_gg.Item is ICoreXSDItemArray)
                        //{
                        //    foreach (CoreXSDItem v_mm in (v_gg.Item as ICoreXSDItemArray))
                        //    {
                        //        CoreXSDItem.StoreItem(v_mm, e.Add(item.Name));
                        //    }
                        //}
                        //else
                        //{
                        //    v_gg.SaveData(e.Add(item.Name));
                        //}

                    }
                }
            }
            return v_rtab.ToArray ();
        }
        public ICoreXSDItemInfo GetItemInfo(string v)
        {
            var m = this.GetItemDefinition(v);
            ICoreXSDType s = null;
            ICoreXSDItemInfo g = null;
            if (m.TypeName != null)
            {
                s = this.GetItemType(m.TypeName);
            }
            if (s != null)
            {
                g = global::IGK.ICore.Xml.XSD.CoreXSDItemInfo.Create(this, v, s);
            }
            return g;
        }
        protected void storeEntries(CoreXmlElement e, ICoreXSDType s)
        {
            if (s == null || (listItems==null))
                return;
            //  ICoreXSDType item = null;
            CoreXSDFileEntryInfo v_e = CoreXSDFileEntryInfo.Empty;
            string gName = string.Empty;
            string v_rootNode = e.TagName;


            Queue<CoreXSDFileEntryInfo> ee = new Queue<CoreXSDFileEntryInfo>();

            ee.Enqueue(new CoreXSDFileEntryInfo(s, 0));

            string v_key = string.Empty;
            ICoreXSDType[] g = null;
            var v_level = 0;
            List<CoreXSDOutSortItem> o_dic = new List<CoreXSDOutSortItem> ();
            int v_l = 0;

            Dictionary<ICoreXSDType, int> rd = new Dictionary<ICoreXSDType, int>();
            string levelPath = string.Empty;
            while (ee.Count > 0)
            {
                v_e = ee.Dequeue();
                g = CoreXSDUtils.GetXSDTypeItems(this, v_e.Item);

                if (g == null) continue;
                for (int i = v_e.Index; i < g.Length; i++)
                {
                    var item = g[i];
                    if (item is ICoreXSDItemContainer)
                    {
                        if (v_level > 0) {
                            //add level path
                          //  Console.WriteLine ("levelPath ");
                            levelPath += (levelPath==""?"":"/")+ item.Index  + $"level{v_level-1}";
                           /// Console.WriteLine("="+levelPath);
                        }
                        ee.Enqueue(new CoreXSDFileEntryInfo(item, 0));
                        ee.Enqueue(new CoreXSDFileEntryInfo(v_e.Item, i + 1));
                      
                        if (listItems.ContainsKey(item))
                        {
                            loadToLevel(v_level, o_dic,                               
                                item is ICoreXSDChoice,                             
                                levelPath,
                                listItems[item].ToArray());

                        }
                        v_level++;
                        v_l = 1;
                        break;
                    }
                    //if (listItems.ContainsKey(item)){
                    //    loadToLevel(v_level, o_dic, listItems[item].ToArray ());

                    //}



                }
                if ((v_l == 0) && (v_level > 0))
                {
                    v_level--;
                   levelPath = string.IsNullOrEmpty (levelPath)?"": Path.GetDirectoryName (levelPath );
                }
                v_l = 0;
            }


            o_dic.Sort();
            foreach (var item in o_dic)
            {
                CoreXSDItem.StoreItem(item.coreXSDItemInfo.Item, e.Add(item.coreXSDItemInfo.Name ));
            }

        }

        private void loadToLevel(int level, 
            List<CoreXSDOutSortItem> o_dic, 
            //store the order in array
            bool storeIndex , 
            string levelPath,
            params CoreXSDItemInfo[] coreXSDItemInfo)
        {
            int v_index = 0;
            for (int i = 0; i < coreXSDItemInfo.Length; i++)
            {
                v_index = storeIndex ? i :
                    coreXSDItemInfo[i].SourceType.Index;

                //Console.WriteLine ($"{v_index}level{level}");

                o_dic .Add (new CoreXSDOutSortItem (
                    level, 
                    v_index 
                    //IndexOf(coreXSDItemInfo[i],
                    //gtab, rd)
                    , coreXSDItemInfo[i], levelPath));
            }
        }

        class CoreXSDOutSortItem : IComparable
        {
            public CoreXSDItemInfo coreXSDItemInfo;
           // public int v_level;
            public int Index;
            public string m_levelpath;

            public CoreXSDOutSortItem(int level, int index, CoreXSDItemInfo coreXSDItemInfo,
                string levelPath)
            {
               // this.v_level = v_level;
                this.Index = index;
                this.coreXSDItemInfo = coreXSDItemInfo;
                this.m_levelpath  = (string.IsNullOrEmpty (levelPath)? "": levelPath +"/")+
                                 $"{Index}level{level}";
            }

            public override string ToString()
            {
                return "OutSort : "+this.m_levelpath + " : "+this.coreXSDItemInfo.Name;// base.ToString();
            }

            public int CompareTo(object obj)
            {
                CoreXSDOutSortItem c = (CoreXSDOutSortItem)obj;

                return this.m_levelpath.CompareTo (c.m_levelpath );

                //if (c.v_level == this.v_level) {
                //   return this.Index.CompareTo(c.Index);              
                //}
                //return -1;
            }
        }
        protected void storeEntries_bck(CoreXmlElement e, ICoreXSDType s)
        {
            if (s==null)
                return;
          //  ICoreXSDType item = null;
            CoreXSDFileEntryInfo v_e =  CoreXSDFileEntryInfo.Empty;           
            string gName = string.Empty;
            string v_rootNode = e.TagName ;
           // var tab = _getXSDTypeItems(s);
            var vtab2 = _getXSDContainer(v_rootNode, s);
            var rtab3 = _getXSDContainerInfoIndex(v_rootNode, s);
            var finfo = new CoreXSDFileEntryInfo(s, 0);
           // Array.Sort(tab, new SequenceComparer(

            foreach (var k in vtab2) {
                if (!listItems.ContainsKey(k))
                    continue;
                var m = listItems[k];

                if (k is ICoreXSDChoice)
                {
                    foreach (var v_ss in m){
                        CoreXSDItem.StoreItem(v_ss.Item, e.Add(v_ss.Name));
                    }
                }
                else {
                    var gtab = k.Items;
                     Dictionary<ICoreXSDType, int > rd  = new Dictionary<ICoreXSDType, int> (); 

                    m.Sort ((a,b)=> {
                        int ia = IndexOf(a, gtab,rd);
                        int ib = IndexOf(b, gtab,rd);
                       
                        return ia.CompareTo(ib);
                    });
                    foreach (var v_ss in m)
                    {
                        CoreXSDItem.StoreItem(v_ss.Item, e.Add(v_ss.Name));
                    }
                }

             }
            //return ;

            //algotithm for entry info


            //foreach( KeyValuePair<string, ICoreXSDFileEntry> ss in this.m_entries)
            //{
            //    //gdata.Add(ss.Val);
            //    var v_gg = ss.Value;//this.m_entries[v_key];
            //    item = null;//ss.Value.Item.T  as ICoreXSDType;
            //    if (v_gg.Item is ICoreXSDItemArray)
            //    {
            //        item = (v_gg.Item as ICoreXSDItemArray).Type;
            //        foreach (CoreXSDItem v_mm in (v_gg.Item as ICoreXSDItemArray))
            //        {
            //            CoreXSDItem.StoreItem(v_mm, e.Add(item.Name));
            //        }
            //    }
            //    else
            //    {
            //        v_gg.SaveData(e.Add(item.Name));
            //    }

            //}
            //return ;


            ////algorithm for sequence info type
            //List<ICoreXSDType> gdata = new List<ICoreXSDType>();
            //Queue<CoreXSDFileInfo> ee = new Queue<CoreXSDFileInfo>();

            //ee.Enqueue(new CoreXSDFileInfo(s, 0));
          
            //string v_key = string.Empty;
            //ICoreXSDType[] g = null;
            //while (ee.Count > 0)
            //{
            //    v_e = ee.Dequeue();
            //    g = _getXSDTypeItems(v_e.Item);
               
            //    if  (g==null)continue;
            //    for (int i = v_e.Index; i < g.Length; i++)
            //    {
            //        item = g[i];

            //        if (item is ICoreXSDItemContainer)
            //        {

            //            ee.Enqueue(new CoreXSDFileInfo(item, 0));
            //            ee.Enqueue(new CoreXSDFileInfo(v_e.Item, i + 1));
            //            break;
            //        }



            //        v_key = Path.Combine(v_rootNode, item.Name);
            //        if (this.m_entries.ContainsKey(v_key))
            //        {
            //            gdata.Add(item);
            //            var v_gg = this.m_entries[v_key];
            //            if (v_gg.Item is ICoreXSDItemArray)
            //            {
            //                foreach (CoreXSDItem v_mm in (v_gg.Item as ICoreXSDItemArray))
            //                {
            //                    CoreXSDItem.StoreItem(v_mm, e.Add(item.Name));
            //                }
            //            }
            //            else
            //            {
            //                v_gg.SaveData(e.Add(item.Name));
            //            }

            //        }
            //    }
            //}

        }

        private int IndexOf(CoreXSDItemInfo a, ICoreXSDType[] gtab, Dictionary<ICoreXSDType, int> rd)
        {
            if (rd.ContainsKey(a.Item.Type))
                return rd[a.Item.Type];

            for (int i = 0; i < gtab.Length; i++)
            {
                var t = _getXSDRealType(gtab[i]);


                if (t == a.Item.Type) {
                    rd[a.Item.Type] = i;
                    return i;
                }
                //if (gtab[i] is ICoreXSDChoice) {
                //    //search in choice
                //   var q =  a.Item.Type.Parent;
                //    while ((q != null) && (q != gtab[i])) {
                //        q = q.Parent;
                //    }
                //    if (q != null) {
                //        rd[a.Item.Type] = i;
                //        return i;
                //    }
                //}

            }
            return -1;
        }

        private ICoreXSDType _getXSDRealType(ICoreXSDType coreXSDType)
        {
            if (!string.IsNullOrEmpty(coreXSDType.TypeName)) {
               return  this.GetItemType(coreXSDType.TypeName);
            } 
            return coreXSDType ;
        }

        //private void _storeSequence(ICoreXSDType k, List<CoreXSDItemInfo> m, CoreXmlElement e)
        //{

        //    ICoreXSDType item = null;
        //    CoreXSDFileInfo v_e = CoreXSDFileInfo.Empty;
        //    string gName = string.Empty;
        //    string v_rootNode = e.TagName;
        //    List<ICoreXSDType> gdata = new List<ICoreXSDType>();
        //    Queue<CoreXSDFileInfo> ee = new Queue<CoreXSDFileInfo>();

        //    ee.Enqueue(new CoreXSDFileInfo(m, 0));

        //    string v_key = string.Empty;
        //    ICoreXSDType[] g = null;
        //    while (ee.Count > 0)
        //    {
        //        v_e = ee.Dequeue();
        //        g = _getXSDTypeItems(v_e.Item);

        //        if (g == null) continue;
        //        for (int i = v_e.Index; i < g.Length; i++)
        //        {
        //            item = g[i];

        //            if (item is ICoreXSDItemContainer)
        //            {

        //                ee.Enqueue(new CoreXSDFileInfo(item, 0));
        //                ee.Enqueue(new CoreXSDFileInfo(v_e.Item, i + 1));
        //                break;
        //            }



        //            v_key = Path.Combine(v_rootNode, item.Name);
        //            if (this.m_entries.ContainsKey(v_key))
        //            {
        //                gdata.Add(item);
        //                var v_gg = this.m_entries[v_key];
        //                if (v_gg.Item is ICoreXSDItemArray)
        //                {
        //                    foreach (CoreXSDItem v_mm in (v_gg.Item as ICoreXSDItemArray))
        //                    {
        //                        CoreXSDItem.StoreItem(v_mm, e.Add(item.Name));
        //                    }
        //                }
        //                else
        //                {
        //                    v_gg.SaveData(e.Add(item.Name));
        //                }

        //            }
        //        }
        //    }
        //}

     


        /// <summary>
        /// override this method to create root file
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public virtual CoreXSDItem CreateItem(string name)
        {
            if (this.m_fileManager!=null) {

                var i =  this.m_fileManager.CreateItem(name);
                if (this.Entries.ContainsKey(name))
                {
                }
                else
                {
                    this.Entries.Add(name,
                        this.CreateFileEntry(i.Type).AddData(i));
                }
                return i;
            }
            return null;
        }

        /// <summary>
        /// crreate new or return the last created element
        /// </summary>
        /// <param name="rootNode">root name of the node</param>
        /// <param name="name">requested name to root node</param>
        /// <returns></returns>
        protected CoreXSDItem CreateNewItem(string rootNode, string name) {

            string fname = rootNode==null? name:Path.Combine(rootNode, name);
            ICoreXSDType s = null;
            CoreXSDItem v_r = null;
            var v_entries = this.Entries;//Entries();
            ICoreXSDObjectType v_o = null;
            if (v_entries.ContainsKey(fname))
            {
                v_o = v_entries[fname].Item;
                if (v_o is ICoreXSDItemArray)
                {
                    var g = (s as ICoreXSDItemArray);
                    if (g.CanAdd)
                    {
                        v_r = g.CreateNew();
                        return v_r;
                    }
                }
                else
                {
                    s = v_o as ICoreXSDType;
                    if ((s.MaxOccurs == 1)&& !(s.IsInChoice && ( s.Parent.MaxOccurs == CoreXSDConstant.UNBOUNDED)  ))
                    {
                        return v_entries[fname].ValueItems[0];//s as CoreXSDItem;
                    }
                    else
                    {
                        //change to ICoreXSDItemArray
                        var array = this.CreateRootItemArray();
                        //ColladaItemArray array = ColladaItemArray.CreateFrom(s);

                        if (array == null)
                            throw new Exception("Can't create a root item array");
                        //array.AddItem(s); 
                        array.AddItem(v_entries[fname].ValueItems[0]);
                     
                     
                        v_entries[fname] = this.CreateFileEntry(array);
                        v_r = array.CreateNew();
                        array.AddItem(v_r);

                        _loadListItem(s, v_r, name );
                        return v_r;
                    }
                }
            }
            else
            {
                s = this.m_fileManager.GetItem(fname );
                if (s == null)
                    return null;
                CoreXSDItem v_obj = null;
                if (s.TypeName != null)
                {

                    v_obj = this.m_fileManager.CreateItem(s.TypeName);// new CoreXSDItem ();
                }
                else
                {
                    v_obj = CoreXSDItem.Create(this, s as ICoreXSDType);

                }
                if (v_obj == null)
                    throw new Exception($"{nameof(v_obj)} is null");

              

                var n_finfo = this.CreateFileEntry(s);
                n_finfo.AddData(v_obj);

                v_entries.Add(fname, n_finfo);//new ColladaFileEntry(this, s).addData(v_obj));

                _loadListItem(s, v_obj, name );


                return v_obj;
            }
            return null;

        }

        private void _loadListItem(ICoreXSDType s, CoreXSDItem v_obj, string name)
        {
            if (listItems == null)
                listItems = new Dictionary<ICoreXSDType, List<CoreXSDItemInfo>>();
            if (s.Parent != null)
            {
                var v_p = s.Parent;
                if (!listItems.ContainsKey(v_p))
                {
                    listItems.Add(v_p, new List<CoreXSDItemInfo>());
                }

                var rb = new CoreXSDItemInfo(name, v_obj, s);
                if (!listItems[v_p].Contains(rb))
                    listItems[v_p].Add(rb);
            }
            else {


            }
        }

        struct CoreXSDItemInfo {
            private string name;
            private CoreXSDItem v_obj;
            private ICoreXSDType s;

            /// <summary>
            /// name used for that entry
            /// </summary>
            public string Name=>name;
            /// <summary>
            /// store the entry item
            /// </summary>
            public CoreXSDItem Item=>v_obj;
            /// <summary>
            /// get the source type used to create this info
            /// </summary>
            public ICoreXSDType SourceType=>s;
           

            public CoreXSDItemInfo(string name, CoreXSDItem v_obj, ICoreXSDType s) 
            {
                this.name = name;
                this.v_obj = v_obj;
                this.s = s;
            }
        }

        Dictionary<ICoreXSDType , List<CoreXSDItemInfo>> listItems;
       
        protected virtual ICoreXSDFileEntry CreateFileEntry(ICoreXSDObjectType itype)
        {
            return new CoreXSDFileBase.CoreXSDFileEntry(this, itype);
        }

        protected virtual ICoreXSDItemArray CreateRootItemArray()
        {
           return new CoreXSDItemArray(this);
        }

        public virtual  ICoreXSDType GetItemType(string typeName)
        {
            return null;
        }

        public virtual string GetTypeName(string typeName)
        {
            if (typeName.Contains(":"))
            {
                string[] tt = typeName.Split(':');
                return tt[tt.Length - 1];
            }
            return typeName;
        }
    
   private class CoreXSDFileEntry : XSD.CoreXSDFileEntry, ICoreXSDFileEntry
        {
            private CoreXSDFileBase m_owner;
            private List<CoreXSDItem> m_items;
            private ICoreXSDObjectType m_item;

            public override ICoreXSDObjectType Item =>
                    m_item;

            public override CoreXSDItem[] ValueItems => m_items.ToArray();

            public CoreXSDFileEntry(CoreXSDFileBase coreXSDFileBase,
                ICoreXSDObjectType itype)
            {
                if (itype == null)
                    throw new ArgumentNullException ($"{nameof(itype)}");
                this.m_owner = coreXSDFileBase;
                this.m_item = itype;
                this.m_items = new List<CoreXSDItem> ();
            }

            public override ICoreXSDFileEntry AddData(CoreXSDItem item)
            {
                this.m_items.Add (item);
                return this;
            }

            public override void SaveData(CoreXmlElement e)
            {
                foreach (var item in m_items)
                {
                    CoreXSDItem.StoreItem(item, e);
                }
            }
        }


        public class CoreXSDItemArray : ICoreXSDItemArray
        {
            private List<CoreXSDItem> m_items;
            private CoreXSDFileBase m_owner;

            public CoreXSDItemArray(CoreXSDFileBase coreXSDFileBase)
            {
                this.m_owner = coreXSDFileBase;
                m_items = new List<CoreXSDItem> ();
            }

            public bool CanAdd => 
                (this.Type.MaxOccurs == CoreXSDConstant.UNBOUNDED) || 
                this.Count  < this.Type.MaxOccurs;
            public int Count => m_items.Count;

            public ICoreXSDType Type => m_items.Count>0? m_items[0].Type : null;

            public void AddItem(CoreXSDItem item)
            {
                if (this.m_items.Contains (item ) ||item==null)
                    return ;
                this.m_items.Add(item);
            }

            public CoreXSDItem CreateNew()
            {
                return this.m_owner.CreateItem (this.Type ) as CoreXSDItem;
            }

            public IEnumerator GetEnumerator()
            {return this.m_items.GetEnumerator ();
            }
        }


    }
}
