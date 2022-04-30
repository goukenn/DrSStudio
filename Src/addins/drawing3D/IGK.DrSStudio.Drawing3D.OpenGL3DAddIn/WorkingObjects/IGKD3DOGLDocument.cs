

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: IGKD3DOGLDocument.cs
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
using IGK.DrSStudio.Drawing2D;
using IGK.DrSStudio.Drawing2D.WinUI;
﻿using IGK.ICore.Codec;
using IGK.DrSStudio.Drawing3D.OpenGL.WinUI;
using IGK.OGLGame.Graphics;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.DrSStudio.Drawing3D.OpenGL
{
    [IGKD3DOGLWorkingObjectAttribute("OGLSceneDocument")]
    public class IGKD3DOGLDocument : IGKD3DOGLElementBase , ICoreWorkingDocument 
    {
        private IGKD3DOGLDocumentElementCollection m_elements;
        private Rectanglei m_Viewport;
        /// <summary>
        /// get or set the view port
        /// </summary>
        public Rectanglei Viewport
        {
            get { return m_Viewport; }
            set
            {
                if (m_Viewport != value)
                {
                    m_Viewport = value;
                }
            }
        }

        /// <summary>
        /// get element container in this document
        /// </summary>
        public IGKD3DOGLDocumentElementCollection Elements {
            get {
                return this.m_elements;
            }
        }
        protected override void ReadAttributes(IXMLDeserializer xreader)
        {
            base.ReadAttributes(xreader);
        }
   
        protected override void WriteAttributes(IXMLSerializer xwriter)
        {
            base.WriteAttributes(xwriter);
        }
        protected override void WriteElements(IXMLSerializer xwriter)
        {
            base.WriteElements(xwriter);
        }
        public IGKD3DOGLDocument()
        {
            this.m_elements = new IGKD3DOGLDocumentElementCollection(this);
        }

        /// <summary>
        /// represent a document collections
        /// </summary>
        public class IGKD3DOGLDocumentElementCollection : IEnumerable
        {
            private IGKD3DOGLDocument m_document;
            private List<IGKD3DOGLElement> m_elements;
            public IGKD3DOGLDocumentElementCollection(IGKD3DOGLDocument document)
            {

                this.m_document = document;
                this.m_elements = new List<IGKD3DOGLElement>();
            }
            /// <summary>
            /// get the number of element in this collection
            /// </summary>
            public int Count { get { return this.m_elements.Count; } }
            public void Add(IGKD3DOGLElement element)
            {
                if ((element == null) || this.m_elements.Contains(element))
                    return;
                this.m_elements.Add(element);
                this.m_document.OnElementAdded(new CoreWorkingObjectEventArgs<IGKD3DOGLElement>(element));
            }
            public void Remove(IGKD3DOGLElement element)
            {
                if (this.m_elements.Contains(element))
                {
                    this.m_elements.Remove(element);
                    this.m_document.OnElementRemoved(new CoreWorkingObjectEventArgs<IGKD3DOGLElement>(element));
                }
            }
            public IEnumerator GetEnumerator()
            {
                return this.m_elements.GetEnumerator();
            }
        }
        /// <summary>
        /// render on device
        /// </summary>
        /// <param name="device"></param>
        public virtual void Render(OGLGraphicsDevice device)
        {
            
            foreach (IGKD3DOGLElement item in this.m_elements)
            {
                item.Render(device);
            }
        }

        protected virtual void OnElementAdded(CoreWorkingObjectEventArgs<IGKD3DOGLElement> e)
        {
            if (this.ElementAdded != null)
                this.ElementAdded(this, e);
        }

        protected virtual void OnElementRemoved(CoreWorkingObjectEventArgs<IGKD3DOGLElement> e)
        {
            if (this.ElementRemoved != null)
                this.ElementRemoved(this, e);
        }

        public event EventHandler<CoreWorkingObjectEventArgs<IGKD3DOGLElement>> ElementAdded;
        public event EventHandler<CoreWorkingObjectEventArgs<IGKD3DOGLElement>> ElementRemoved;

        /// <summary>
        /// get the default surface type
        /// </summary>
        public virtual Type DefaultSurfaceType
        {
            get {
                return typeof(IGKD3DOGLSurface);
            }
        }
    }
}
