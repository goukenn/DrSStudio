

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: Core2DDrawingObject.cs
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
file:Core2DDrawingObject.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace IGK.DrSStudio.Drawing2D
{
    /// <summary>
    /// represent the base class of the drawing 2D element
    /// </summary>
    public abstract class Core2DDrawingObject : Core2DDrawingComponentObjectBase,
        ICoreWorkingObject ,
        ICoreWorkingRenderableObject ,
        ICore2DDrawingObject
    {
        private Core2DDrawingObject m_Parent;
        public Core2DDrawingObject Parent
        {
            get { return m_Parent; }
            protected internal set
            {
                if (m_Parent != value)
                {
                    m_Parent = value;
                }
            }
        }
        ICore2DDrawingObject ICore2DDrawingObject.Parent
        {
            get {
                return this.Parent;
            }
        }
        public virtual object  Clone() {
            return MemberwiseClone();
        }
        public virtual void Accept(ICoreWorkingRenderer renderer)
        {
            if (renderer != null)
            {
                renderer.Render(this);
            }
        }
    }
}

