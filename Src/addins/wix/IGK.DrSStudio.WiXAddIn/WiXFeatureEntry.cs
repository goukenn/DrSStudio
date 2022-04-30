

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: WiXFeatureEntry.cs
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
/*
IGKDEV @ 2008-2016
author: C.A.D . BONDJE DOUE
file:WiXFeatureEntry.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace IGK.DrSStudio.WiXAddIn
{
    /// <summary>
    /// represent a feature entry
    /// </summary>
    public class WiXFeatureEntry : WiXFeature 
    {
        private string m_Level;
        private string m_Title;
        private WiXFeatureCollections m_Entries;
        [WiXElement ()]
        public WiXFeatureCollections Entries
        {
            get {
                return this.m_Entries;
            }
        }
        [WiXAttribute()]
        public string Title
        {
            get { return m_Title; }
            set
            {
                if (m_Title != value)
                {
                    m_Title = value;
                }
            }
        }
        [WiXAttribute ()]
        public string Level
        {
            get { return m_Level; }
            set
            {
                if (m_Level != value)
                {
                    m_Level = value;
                }
            }
        }
        public WiXFeatureEntry()
        {
            this.Level = "1";
            this.Id = "FeatureId_"+this.GetHashCode ();
            this.m_Title = "IGKDEV Feature";
            this.m_Entries = new WiXFeatureCollections(this);
        }
        internal void Add(WiXDirectoryComponent comp)
        {
            if (comp == null)
                return;
            WiXComponentRef v_ref = new WiXComponentRef();
            v_ref.Component = comp;
            this.m_Entries.Add(v_ref);
        }

        public class WiXFeatureCollections : CoreWorkingObjectCollections<WiXComponentRef>
        {
            private WiXFeatureEntry wiXFeatureEntry;

            public override string ToString()
            {
                return "WiXFeatureCollection [ " + this.Count + "]";
            }
            public WiXFeatureCollections(WiXFeatureEntry wiXFeatureEntry):base()
            {                
                this.wiXFeatureEntry = wiXFeatureEntry;
            } 
        }
    }
}

