

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: Core2DDrawingDualBrushElement.cs
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
file:Core2DDrawingDualBrushElement.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace IGK.DrSStudio.Drawing2D
{
    /// <summary>
    /// represent the core brush element base
    /// </summary>
    public abstract class Core2DDrawingDualBrushElement : 
        Core2DDrawingLayeredElement, 
        ICore2DDrawingDualBrushElement ,
        ICore2DBrushOwner,
        ICore2DDrawingBoundElement 
    {
        private ICoreBrush m_FillBrush;
        private ICorePen m_StrokeBrush;
        private Rectanglef m_Bound;
        public Rectanglef Bound
        {
            get { return m_Bound; }
            set
            {
                if (!m_Bound.Equals(value))
                {
                    m_Bound = value;
                    OnPropertyChanged(CoreWorkingObjectPropertyChangedEventArgs.Definition);
                }
            }
        }
        public Core2DDrawingDualBrushElement()
        {
            this.m_FillBrush = new CoreBrush(this);
            this.m_StrokeBrush = new CorePen(this);
        }
        public ICoreBrush FillBrush
        {
            get { return this.m_FillBrush; }
        }
        public ICorePen StrokeBrush
        {
            get { return this.m_StrokeBrush; }
        }
        public ICoreBrush GetBrush(enuBrushMode mode)
        {
            switch (mode)
            {
                case enuBrushMode.Fill:
                    return this.m_FillBrush ;
                case enuBrushMode.Stroke:
                    return this.m_StrokeBrush;
                default:
                    break;
            }
            return this.m_FillBrush;
        }
        /// <summary>
        /// get all brush
        /// </summary>
        /// <returns></returns>
        public virtual ICoreBrush[] GetBrushes()
        {
            return new ICoreBrush[] { 
                m_FillBrush ,
                m_StrokeBrush 
            };
        }
        public enuBrushSupport BrushSupport
        {
            get { return enuBrushSupport.All; }
        }
    }
}

