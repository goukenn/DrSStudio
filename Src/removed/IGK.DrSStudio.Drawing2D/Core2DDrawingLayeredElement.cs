

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: Core2DDrawingLayeredElement.cs
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
file:Core2DDrawingLayeredElement.cs
*/
using IGK.ICore;using IGK.DrSStudio.ComponentModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
namespace IGK.DrSStudio.Drawing2D
{
    /// <summary>
    /// represent a base layered element
    /// </summary>
    public abstract class Core2DDrawingLayeredElement : 
        Core2DDrawingVisibleObject, 
        ICore2DDrawingLayeredElement,
        ICore2DDrawingGraphicsProperty
    {
        public Core2DDrawingLayeredElement()
        {
            this.m_CompositingMode = enuCompositingMode.Over;
            this.m_SmoothingMode = enuSmoothingMode.Antialiazed;
        }
        /// <summary>
        /// get the first top parent document
        /// </summary>
        [Browsable(false)]
        public ICore2DDrawingDocument ParentDocument
        {
            get {
                var q = this.Parent;
                while ((q != null))
                {
                    if (q is ICore2DDrawingDocument)
                        return q as ICore2DDrawingDocument;
                    q = q.Parent;
                }
                return null;
            }
        }
        /// <summary>
        /// get the first top parent layer
        /// </summary>
        /// <returns></returns>
        [Browsable(false)]
        public ICore2DDrawingLayer ParentLayer
        {
            get
            {
                var q = this.Parent;
                while ((q != null))
                {
                    if (q is ICore2DDrawingLayer)
                        return q as ICore2DDrawingLayer;
                    q = q.Parent;
                }
                return null;
            }
        }
        public void Align(enuCore2DAlignElement alignment, Rectanglef Bound)
        {
            throw new NotImplementedException();
        }
        [Browsable(false )]
        public virtual bool CanReSize
        {
            get { return true; }
        }
        [Browsable(false)]
        public virtual bool CanRotate
        {
            get { return true; }
        }
        [Browsable(false)]
        public virtual bool CanScale
        {
            get { return true; }
        }
        [Browsable(false)]
        public virtual bool CanTranslate
        {
            get { return true; }
        }
        public void ClearTransform()
        {
            throw new NotImplementedException();
        }
        public void Dock(enuCore2DDockElement dock, Rectanglef docBound)
        {
            throw new NotImplementedException();
        }
        public void FlipX()
        {
            throw new NotImplementedException();
        }
        public void FlipY()
        {
            throw new NotImplementedException();
        }
        public Matrix GetMatrix()
        {
            throw new NotImplementedException();
        }
        public void Inflate(float ex, float ey, enuMatrixOrder mat)
        {
            throw new NotImplementedException();
        }
        public void MultTransform(Matrix matrix, enuMatrixOrder order)
        {
            throw new NotImplementedException();
        }
        public void ResetTransform()
        {
            throw new NotImplementedException();
        }
        public void Rotate(float angle, Vector2f center, enuMatrixOrder mat)
        {
            throw new NotImplementedException();
        }
        public void Scale(float ex, float ey, enuMatrixOrder mat)
        {
            throw new NotImplementedException();
        }
        public void Transform(Matrix matrix)
        {
            throw new NotImplementedException();
        }
        public void Translate(float dx, float dy, enuMatrixOrder mat)
        {
            throw new NotImplementedException();
        }
        public ICoreWorkingPositionableObjectContainer Container
        {
            get { return this.Parent as ICoreWorkingPositionableObjectContainer; }
        }
        public void MoveAt(int index)
        {
            throw new NotImplementedException();
        }
        public void MoveBack()
        {
            throw new NotImplementedException();
        }
        public void MoveEnd()
        {
            throw new NotImplementedException();
        }
        public void MoveFront()
        {
            throw new NotImplementedException();
        }
        public void MoveStart()
        {
            throw new NotImplementedException();
        }
        public int ZIndex
        {
            get {
                var c = Container;
                if (c != null)
                    return c.IndexOf(this);
                return -1;
            }
        }
        private enuCompositingMode m_CompositingMode;
        private enuSmoothingMode  m_SmoothingMode;
        [CoreGraphicCategoryAttribute()]
        public enuSmoothingMode  SmoothingMode
        {
            get { return m_SmoothingMode; }
            set
            {
                if (m_SmoothingMode != value)
                {
                    m_SmoothingMode = value;
                    OnPropertyChanged(CoreWorkingObjectPropertyChangedEventArgs.Definition);
                }
            }
        }
        [CoreGraphicCategoryAttribute()]
        public enuCompositingMode CompositingMode
        {
            get { return m_CompositingMode; }
            set
            {
                if (m_CompositingMode != value)
                {
                    m_CompositingMode = value;
                    OnPropertyChanged(CoreWorkingObjectPropertyChangedEventArgs.Definition);
                }
            }
        }
    }
}

