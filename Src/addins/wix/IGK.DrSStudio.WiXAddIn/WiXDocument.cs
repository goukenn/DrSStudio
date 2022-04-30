

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: WiXDocument.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
using IGK.ICore.WinCore;
using IGK.ICore;
using IGK.ICore.Codec;
using IGK.ICore.Menu;
using IGK.ICore.WinUI;
using IGK.ICore.Drawing2D;
using IGK.ICore.Drawing2D.Menu;
using IGK.ICore.Drawing2D.WinUI;
using IGK.DrSStudio;
using IGK.DrSStudio.Menu;
using IGK.DrSStudio.WinUI;
using IGK.DrSStudio.Drawing2D;
using IGK.DrSStudio.Drawing2D.WinUI;
using IGK.DrSStudio.WiXAddIn.WinUI;
/*
IGKDEV @ 2008-2016
author: C.A.D . BONDJE DOUE
file:WiXDocument.cs
*/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace IGK.DrSStudio.WiXAddIn
{
    public class WiXDocument : WiXEntry, ICoreWorkingDocument
    {
        public virtual bool IsValid
        {
            get { return true; }
        }
        private WiXDocumentFeatureCollections m_Features;
        [Browsable(false)]
        /// <summary>
        /// get or set the output tempory directory of the document
        /// </summary>
        public string SourceTempDir { get; set; }//change to sourcedir
        [WiXElement()]
        public WiXDocumentFeatureCollections Features
        {
            get
            {
                return m_Features;
            }
        }
        public WiXDocument()
        {
            this.m_Features = new WiXDocumentFeatureCollections(this);
        }
        internal protected WiXFeatureEntry GetFeature(int p)
        {
            WiXFeatureEntry[] t = GetFeatures();
            if ((p >= 0) && (p < t.Length))
                return t[p];
            return null;
        }
        internal protected WiXFeatureEntry[] GetFeatures()
        {
            List<WiXFeatureEntry> t = new List<WiXFeatureEntry>();
            foreach (var item in this.m_Features)
            {
                if (item is WiXFeatureEntry)
                {
                    t.Add(item as WiXFeatureEntry);
                }
            }
            return t.ToArray();
        }
        public class WiXDocumentFeatureCollections : CoreWorkingObjectCollections<WiXFeature>
        {
            WiXDocument owner;
            public WiXDocumentFeatureCollections(WiXDocument owner)
            {
                this.owner = owner;
            }
            public override void Add(WiXFeature element)
            {
                base.Add(element);
                element.Parent = owner;
            }
            public override void Remove(WiXFeature element)
            {
                base.Remove(element);
                element = null;
            }
            public WiXFeature GetElementById(string p)
            {
                foreach (WiXFeature item in this)
                {
                    if (item.Id == p)
                        return item;
                }
                return null;
            }
        }
      
        public void Deserialize(IXMLDeserializer xreader)
        {
            
        }
        public void Serialize(IXMLSerializer xwriter)
        {
        }

        public virtual Type DefaultSurfaceType
        {
            get { return typeof(WiXSurface); }
        }
    }
}

