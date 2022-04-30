

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: GLEditorLayerDocument.cs
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
file:GLEditorLayerDocument.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace IGK.DrSStudio.GLPictureEditorAddIn
{
    public class GLEditorLayerDocument : IGLEditorRenderer
    {
        private GLEditorLayaredElementCollections  m_Elements;
        /// <summary>
        /// get the element collection
        /// </summary>
        public GLEditorLayaredElementCollections  Elements
        {
            get { return m_Elements; }
        }
        public GLEditorLayerDocument()
        {
            m_Elements = new GLEditorLayaredElementCollections(this);
        }
        public void Render(IGK.OGLGame.Graphics.OGLGraphicsDevice Device)
        {
            //bind layer effect 
            Device.PushAttrib(OGLGame.Graphics.enuAttribBit.All);
            foreach (GLEditorLayeredElementBase  item in this.m_Elements )
            {
                if (item.Visible)
                item.Render(Device);
            }
            Device.PopAttrib();
        }
        public class GLEditorLayaredElementCollections : System.Collections.IEnumerable
        {
            GLEditorLayerDocument m_layer;
                List<GLEditorLayeredElementBase> m_elements;
            public GLEditorLayaredElementCollections(GLEditorLayerDocument layer)
            {
                this.m_elements = new List<GLEditorLayeredElementBase>();
                this.m_layer = layer;
            }
            public int Count { get { return this.m_elements.Count; } }
            public void Add(GLEditorLayeredElementBase element)
            {
                this.m_elements.Add(element);
            }
            public void Remove(GLEditorLayeredElementBase element)
            {
                this.m_elements.Remove(element);
            }
            #region IEnumerable Members
            public System.Collections.IEnumerator GetEnumerator()
            {
                return this.m_elements.GetEnumerator();
            }
            #endregion
        }
        public void FreeResources(IGK.OGLGame.Graphics.OGLGraphicsDevice device)
        {   
            foreach (GLEditorLayeredElementBase  item in this.m_Elements )
            {
                item.FreeResources(device);
            }
        }
    }
}

