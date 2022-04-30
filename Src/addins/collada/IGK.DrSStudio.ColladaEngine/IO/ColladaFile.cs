using IGK.ICore.Extensions;
using IGK.ICore.Xml;
using IGK.ICore.Xml.XSD;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.DrSStudio.ColladaEngine.IO
{
    public class ColladaFile: 
        CoreXSDFileBase 
            {        
        /// <summary>
        /// get type info
        /// </summary>
        /// <param name="typeName"></param>
        /// <returns></returns>
        public override ICoreXSDDeclaringType GetTypeInfo(string typeName)
        {
            return ColladaManager.GetTypeInfo(typeName);
        }
        public override object CreateItem(ICoreXSDType coreXSDType)
        {
            return CoreXSDItem.Create (this, coreXSDType);
        }
        
        public string Version { get; set; }

        
        /// <summary>
        /// .ctr
        /// </summary>
        internal ColladaFile():base() {
            SetManagerItemListener(new ColladaFileManagerListener(this));
        }

       

        public override bool Save(string outfile)
        {
            var e = CoreXmlElement.CreateXmlNode(ColladaConstants.ROOT_NODE);
            var s = ColladaManager.GetItem(ColladaConstants.ROOT_NODE);
            //build data to export
            e["version"] = this.Version;
            this.storeEntries(e, s);
            global::System.IO.File.WriteAllText(outfile, e.RenderXML(null));
            return true;
        }

      
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public  override CoreXSDItem CreateItem(string name)
        {
            return this.CreateNewItem (ColladaConstants.ROOT_NODE, name );
            
            //string fname = Path.Combine(ColladaConstants.ROOT_NODE, name);
            //ColladaItemBase  s =null;
            //CoreXSDItem v_r = null;
            //var v_entries = this.Entries;//Entries();

            //if (v_entries.ContainsKey(fname))
            //{
            //    s = v_entries[fname].Item as ColladaItemBase;

            //    if (s is ICoreXSDItemArray) {
            //        var g = (s as ICoreXSDItemArray );

            //        if (g.CanAdd){
            //            v_r = g.CreateNew();
            //            return v_r;
            //        }
            //    } else {
            //        if (s.MaxOccurs == 1)
            //        {
            //            return v_entries[fname].ValueItems[0];//s as CoreXSDItem;
            //        }
            //        else
            //        {
            //            //change to ICoreXSDItemArray

            //            ColladaItemArray array = ColladaItemArray.CreateFrom(s);
                   
            //            //array.AddItem(s); 
            //            v_r = array.CreateNew();
            //            array.AddItem( v_entries[fname].ValueItems[0]);

            //            v_entries[fname] = new ColladaFileEntry(this, 
            //                array);
            //        array.AddItem (v_r);

            //            return v_r;
            //        }
            //    }
            //}
            //else
            //{
            //    s = ColladaManager.GetItem(fname);
            //    if (s == null)
            //        return null;
            //    CoreXSDItem v_obj=null;
            //    if (s.TypeName != null)
            //    {

            //        v_obj = ColladaManager.CreateItem(s.TypeName);// new CoreXSDItem ();
            //    }
            //    else {
            //         v_obj = CoreXSDItem.Create(this, s as ICoreXSDType);

            //    }
            //    if  (v_obj ==null)
            //        throw new Exception ($"{nameof(v_obj)} is null");
            //    v_entries.Add (fname, new ColladaFileEntry (this, s).AddData(v_obj));

            //    return v_obj;
            //}
            //return null;
        }

        protected override ICoreXSDFileEntry CreateFileEntry(ICoreXSDObjectType itype)
        {
            if (itype is ColladaTypeBase)
                 return new ColladaFileEntry(this, itype as ColladaTypeBase);
            return base.CreateFileEntry (itype);
        }

        public static ColladaFile CreateNew() {
            ColladaFile c = ColladaManager.CreateNewFile();
            return c;
        }

        public override ICoreXSDType GetItemType(string typeName)
        {
            return ColladaManager.GetItemType(typeName);
        }
        public override bool ContainsType(string typeName)
        {
            return ColladaManager.ContainsType(typeName);
        }

        public override bool Open(string filename)
        {
            return false;
        }

        /// <summary>
        /// represent a collada file entry
        /// </summary>
        class ColladaFileEntry : CoreXSDFileEntry, ICoreXSDFileEntry 
        {
            private ColladaFile m_owner;
            private List<CoreXSDItem> m_items;


            /// <summary>
            /// get list of value item hosted by this item
            /// </summary>
            public override CoreXSDItem[] ValueItems => m_items.ToArray();
            /// <summary>
            /// get the source type of this file entry
            /// </summary>
           public new ColladaTypeBase Item { get; set; }


            ICoreXSDObjectType ICoreXSDFileEntry.Item{
                get{
                return this.Item; }}

            //public override ICoreXSDType Item
            //{
            //    get;
            //    set;
            //}

            public ColladaFileEntry(ColladaFile owner, ColladaTypeBase s)
            {
                this.m_owner = owner;
                this.Item = s;
                this.m_items = new List<CoreXSDItem>();
            }

            public override ICoreXSDFileEntry AddData(CoreXSDItem v_obj)
            {
                this.m_items.Add(v_obj);
                return this;
            }
         

            /// <summary>
            /// clear the value item
            /// </summary>
            public void Clear() { this.m_items.Clear(); }

            public override  void SaveData(CoreXmlElement e)
            {
                    foreach (var item in m_items)
                    {
                        CoreXSDItem.StoreItem(item, e);
                    }
            }

          
        }
        /// <summary>
        /// represnet a collada file manager
        /// </summary>
        private class ColladaFileManagerListener : ICoreXSDManagerListener
        {
            private ColladaFile colladaFile;

            public ColladaFileManagerListener(ColladaFile colladaFile)
            {
                this.colladaFile = colladaFile;
            }

            public CoreXSDItem CreateItem(string typeName)
            {
                return ColladaManager.CreateItem (typeName);
            }

            public ICoreXSDType GetItem(string fname)
            {
                return ColladaManager.GetItem (fname );
            }
        }
    }
}
