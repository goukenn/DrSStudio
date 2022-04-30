

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: XMLDocumentLine.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/

using IGK.ICore;
using IGK.ICore.Menu;
using IGK.ICore.WinUI;
using IGK.ICore.Drawing2D;
using IGK.ICore.Drawing2D.Menu;
using IGK.ICore.Drawing2D.WinUI;
using IGK.DrSStudio;
using IGK.DrSStudio.Menu;
using IGK.DrSStudio.WinUI;
using IGK.DrSStudio.Drawing2D.WinUI;
/*
IGKDEV @ 2008-2016
author: C.A.D . BONDJE DOUE
file:XMLDocumentLine.cs
*/
/* 
-------------------------------------------------------------------
Company: IGK-DEV
Author : C.A.D. BONDJE DOUE
SITE : http://www.igkdev.be
Application : DrSStudio
powered by IGK - DEV &copy; 2008-2012
THIS FILE IS A PART OF THE DRSSTUDIO APPLICATION. SEE "License.txt"
FOR MORE INFORMATION ABOUT THE LICENSE
------------------------------------------------------------------- 
*/
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace IGK.DrSStudio.XMLEditorAddIn
{
    public class XMLDocumentLine :IXMLDocumentLine 
    {
        IXMLDocumentLine m_parent;
        private IXMLDocumentSegmentCollections m_segments;
        #region IXMLDocumentLine Members
        public IXMLDocumentLine Parent
        {
            get { return m_parent; }
            set { m_parent = value; }
        }
        public IXMLDocumentSegmentCollections Segments
        {
            get {
                if (this.m_segments == null)
                    this.m_segments = new SegmentCollections();
                return this.m_segments; }
        }
        public int Index
        {
            get { return -1; }
        } 
        public int Draw(System.Drawing.Graphics g)
        {
            throw new NotImplementedException();
        }
        public virtual int Draw(XMLRendereringEventArgs e)
        {
            foreach (IXMLDocumentSegment  s in this.Segments  )
            {
                s.Draw(e );
            }
            e.OffsetX = 0;
            return e.LineHeight;
        }
        #endregion
        public string  Value
        {
            get {
                StringBuilder sb = new StringBuilder();
                foreach (IXMLDocumentSegment  item in this.m_segments)
                {
                    sb.Append(item.Value);
                }
                return sb.ToString(); }
        }
        public override string ToString()
        {
            return this.Value;
        }
        class SegmentCollections : IXMLDocumentSegmentCollections
        {
            List<IXMLDocumentSegment> m_segments;
            public SegmentCollections()
            {
                m_segments = new List<IXMLDocumentSegment>();
            }
            #region IXMLDocumentSegmentCollections Members
            public int Count
            {
                get { return m_segments.Count; }
            }
            public void Add(IXMLDocumentSegment segment)
            {
                this.m_segments.Add(segment);
            }
            public void Remove(IXMLDocumentSegment segment)
            {
                this.m_segments.Remove(segment);
            }
            public void Insert(IXMLDocumentSegment segment, int index)
            {
                this.m_segments.Insert(index, segment);
            }
            #endregion
            #region IEnumerable Members
            public System.Collections.IEnumerator GetEnumerator()
            {
                return this.m_segments.GetEnumerator();
            }
            #endregion
            #region IXMLDocumentSegmentCollections Members
            public void AddSpace()
            {
            }
            public void AddAttribute(string name, string value)
            {
                throw new NotImplementedException();
            }
            #endregion
        }
    }
}

