using IGK.ICore.Xml.XSD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IGK.ICore.Xml;
using System.Collections;

namespace IGK.DrSStudio.ColladaEngine
{
    /// <summary>
    /// represent an array of colladation 
    /// </summary>
    public class ColladaItemArray : ColladaTypeBase, ICoreXSDItemArray
    {
        List<CoreXSDItem> m_list;
        private int m_maxItem;
        private int m_minItem;
        private ColladaTypeBase m_type;

        public bool CanAdd => true || (m_maxItem == ColladaConstants.UNBUNDED) || (m_list.Count < m_maxItem);

        public ColladaItemArray(int min, int max) : this(){
            m_maxItem = max;
            m_minItem = min;
        }
        public ColladaItemArray()
        {
            this.m_list   = new  List<CoreXSDItem>();
            this.m_maxItem = -1; //unbunded
            this.m_minItem = 0;
        }
        public int Count { get { return this.m_list.Count ;} }

        /// <summary>
        /// represent the maximum item allowed to this collada array
        /// </summary>
        public int MaxItem { get { return this.m_maxItem; } }
        /// <summary>
        /// represent the minimum item allowed 
        /// </summary>
        public int MinItem { get { return this.m_maxItem; } }

        public ICoreXSDType Type
        {
            get
            {
                return (this.m_list.Count  > 0)? this.m_list[0].Type : null;
            }
        }
        
        public static ColladaItemArray CreateFrom(ColladaTypeBase s)
        {
            if (s==null)
                return null;
            ColladaItemArray r = new ColladaItemArray (s.MinOccurs, s.MaxOccurs);
            r.m_type = s;
            //r.m_type = s.T.T.SourceTypeName = s.TypeName;
            //r.AddItem (s);

            return r;
        }

        
        public void AddItem(CoreXSDItem s)
        {

            if (!this.m_list.Contains(s) && (this.CanAdd))
            {
                this.m_list.Add(s);
            }
        }

        public CoreXSDItem CreateNew()
        {
               return  ColladaManager.CreateItem(this.m_type.TypeName );
        }

        public IEnumerator GetEnumerator()
        {
            return this.m_list.GetEnumerator ();
        }
    }
}
