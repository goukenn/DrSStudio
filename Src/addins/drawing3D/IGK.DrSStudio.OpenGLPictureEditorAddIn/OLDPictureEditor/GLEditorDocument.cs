

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: GLEditorDocument.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
/*
IGKDEV - [2008 - 2014]
author: C.A.D . BONDJE DOUE
file:GLEditorDocument.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace IGK.DrSStudio.GLPictureEditorAddIn
{
    public class GLEditorDocument
    {
        GLEditorLayerDocumentCollections m_layers;
        private GLEditorLayerDocument  m_CurrentLayer;
        /// <summary>
        /// get or set the current layer
        /// </summary>
        public GLEditorLayerDocument  CurrentLayer
        {
            get { return m_CurrentLayer; }
            set
            {
                if (m_CurrentLayer != value)
                {
                    m_CurrentLayer = value;
                }
            }
        }
        GLEditorLayerDocumentCollections Layers { get { return this.m_layers; } }
        public GLEditorDocument()
        {
            m_layers = CreateLayerCollections();
        }
        protected virtual GLEditorLayerDocumentCollections CreateLayerCollections()
        {
            return new GLEditorLayerDocumentCollections(this);
        }
        public class GLEditorLayerDocumentCollections : System .Collections .IEnumerable 
        {
            GLEditorDocument m_document;
            List<GLEditorLayerDocument> m_layers;
            public GLEditorLayerDocumentCollections(GLEditorDocument document)
            {
                this.m_document = document;
                this.m_layers = new List<GLEditorLayerDocument>();
                this.m_layers.Add(document.CreateNewLayer());
                this.m_document.m_CurrentLayer = this.m_layers[0];
            }
            public override string ToString()
            {
                return string.Format("Layers:[{0}]", this.Count);
            }
            public int Count { get { return this.m_layers.Count; } }
            #region IEnumerable Members
            public System.Collections.IEnumerator GetEnumerator()
            {
                return this.m_layers.GetEnumerator();
            }
            #endregion
        }
        internal void Render(IGK.OGLGame.Graphics.OGLGraphicsDevice Device)
        {
            foreach (GLEditorLayerDocument  layer in this.m_layers )
            {
                layer.Render(Device);
            }
        }
        protected virtual GLEditorLayerDocument CreateNewLayer()
        {
            return new GLEditorLayerDocument();
        }
        internal void FreeRessources(IGK.OGLGame.Graphics.OGLGraphicsDevice device)
        {
            foreach (GLEditorLayerDocument  layer in this.Layers)
            {
                layer.FreeResources(device);
            }
        }
    }
}

