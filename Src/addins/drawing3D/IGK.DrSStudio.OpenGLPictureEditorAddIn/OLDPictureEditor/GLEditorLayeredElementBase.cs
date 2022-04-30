

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: GLEditorLayeredElementBase.cs
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
file:GLEditorLayeredElementBase.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace IGK.DrSStudio.GLPictureEditorAddIn
{
    public abstract class GLEditorLayeredElementBase : IGLEditorLayerElement 
    {
        private bool m_Visible;
        public bool Visible
        {
            get { return m_Visible; }
            set
            {
                if (m_Visible != value)
                {
                    m_Visible = value;
                }
            }
        }
        public GLEditorLayeredElementBase()
        {
            this.m_Visible = true;
        }
        #region IGLEditorRenderer Members
        public abstract void Render(IGK.OGLGame.Graphics.OGLGraphicsDevice device);
        #endregion
        #region IDisposable Members
        public virtual void Dispose()
        {
        }
        #endregion
        #region IGLEditorLayerElement Members
        public virtual void FreeResources(IGK.OGLGame.Graphics.OGLGraphicsDevice device)
        {
        }
        #endregion
    }
}

