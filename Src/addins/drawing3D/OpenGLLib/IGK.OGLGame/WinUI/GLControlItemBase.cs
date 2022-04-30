

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: GLControlItemBase.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
/*
IGKDEV @ 2008-2016
author: C.A.D . BONDJE DOUE
file:GLControlItemBase.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace IGK.OGLGame.WinUI.GLControls
{
    
using IGK.ICore;
    /// <summary>
    /// represent the gl item control base
    /// </summary>
    public class GLControlItemBase : IGLControlItem 
    {
        internal int m_id;
        protected bool m_isVisible;
        protected bool m_isEnabled;
        protected bool m_isDefaultControl;
        protected bool m_hasFocus;
        protected bool m_MouseHover;
        protected string m_name;
        protected Vector2i m_location;
        protected Size2i m_size;
        private IGLControl m_parent;
        public int Id { get { return this.m_id; } }
        public Size2i Size { get { return m_size; } set { this.m_size = value; } }
        public string Name { get { return this.m_name; } set { this.m_name = value; } }
        public bool Visible { get { return this.m_isVisible; } set { this.m_isVisible = value; } }
        #region IGLControlItem Members
        public IGK.OGLGame.Graphics.OGLGraphicsDevice GraphicsDevice
        {
            get {
                if (m_parent !=null)
                    return m_parent.GraphicsDevice;
                return null;
            }
        }
        public IGLControl Parent
        {
            get { return m_parent; }
            internal set { this.m_parent = value; }
        }
        #endregion
        public GLControlItemBase()
        {
            this.m_isVisible = true;
            this.m_isEnabled = true;
            this.m_location = Vector2i.Zero;
        }
        /// <summary>
        /// get or set the bound
        /// </summary>
        public virtual Rectanglei Bounds { get { return new Rectanglei(this.m_location, m_size); } 
            set { 
                this.m_location = value.Location; 
                this.m_size = value.Size; }
        }
        /// <summary>
        /// check if the control contains the location
        /// </summary>
        /// <param name="location"></param>
        /// <returns></returns>
        public virtual bool Contains(Vector2i location) { return this.Bounds.Contains(location); }
        internal protected virtual void Render(GLControlTime ControlTime)
        {
        }
        internal protected virtual void Initialize()
        {
        }
        internal protected virtual void LoadContent()
        {
        }
        internal protected virtual void UnloadContent()
        {
        }      
        internal protected virtual void OnMouseHover() { this.m_MouseHover = true; }
        internal protected virtual void OnMouseLeave() { this.m_MouseHover = false; }
    }
}

