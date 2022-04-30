

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: WiXEntry.cs
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
file:WiXEntry.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace IGK.DrSStudio.WiXAddIn
{
    public class WiXEntry : ICoreWorkingObject 
    {
        private string m_Id;
        private WiXDocument  m_wixDocument;
        private WiXEntry m_parent;
        private WiXEntryCollections m_children; 
        public WiXEntry Parent { 
            get{
                return this.m_parent; 
            } 
            internal protected set{
                m_parent = value;
            } 
        }
        [WiXAttribute()]
        public virtual  string Id
        {
            get { return m_Id; }
            set
            {
                if (m_Id != value)
                {
                    m_Id = value;
                }
            }
        }
        public WiXEntry()
        {
            this.m_Id = WiXDisplayNameAttribte.GetName (this.GetType ()) +"_" + this.GetHashCode();
            this.m_children = new WiXEntryCollections(this);
        }
        protected virtual bool Support(WiXEntry e)
        {
            return true;
        }
        public WiXDocument GetDocument()
        {
            if (this.m_parent != null)
            {
                if (this.m_parent is WiXDocument)
                    return this.m_parent as WiXDocument;
                return this.m_parent.GetDocument();
            }
            return null;
        }
        internal void SetWiXDocument(WiXDocument document)
        {
            this.m_wixDocument = document;
        }
        [WiXElement ()]
        public WiXEntryCollections Children {
            get {
                return this.m_children;
            }
        }
        public class WiXEntryCollections : CoreWorkingObjectCollections<WiXEntry>
        {
            private WiXEntry wiXEntry;
            public WiXEntryCollections(WiXEntry wiXEntry):base()
            {                
                this.wiXEntry = wiXEntry;
                
            }
            public override void Add(WiXEntry element)
            {
                if (wiXEntry.Support(element))
                {
                    if (!this.Contains (element ))
                    {
                    base.Add(element);
                    element.m_parent = this.wiXEntry;
                    }
                }
            }
            public override void Remove(WiXEntry element)
            {
                base.Remove(element);
                element.m_parent = null;
            }
        }
    }
}

