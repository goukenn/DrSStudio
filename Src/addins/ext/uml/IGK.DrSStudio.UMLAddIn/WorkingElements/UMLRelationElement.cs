

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: UMLRelationElement.cs
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
file:UMLRelationElement.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace IGK.DrSStudio.UMLAddIn.WorkingElements
{
    
using IGK.ICore;
    using IGK.ICore.Drawing2D;
    [UMLGroupElement ("Relation", typeof (UMLMecanismSelection))]
    /// <summary>
    /// represent a relation element
    /// </summary>
    public class UMLRelationElement : 
        Core2DDrawingLayeredElement ,
        ICore2DDrawingVisitable 
    {
        private Vector2f  m_StartPoint;
        private Vector2f  m_EndPoint;
        private ICorePen m_StrokeBrush;

        public UMLRelationElement()
        {
            this.m_StrokeBrush = new CorePen(this);
            this.m_StrokeBrush.BrushDefinitionChanged += m_StrokeBrush_BrushDefinitionChanged;
        }
        public override void Dispose()
        {
            if (this.m_StrokeBrush != null)
                this.m_StrokeBrush.Dispose();
            this.m_StrokeBrush = null;
            base.Dispose();
        }
        void m_StrokeBrush_BrushDefinitionChanged(object sender, EventArgs e)
        {
            OnPropertyChanged(Core2DDrawingChangement.Definition);
        }
        public ICorePen StrokeBrush
        {
            get { return m_StrokeBrush; }
        }
        public Vector2f  EndPoint
        {
            get { return m_EndPoint; }
            set
            {
                if (m_EndPoint != value)
                {
                    m_EndPoint = value;
                }
            }
        }
        public Vector2f  StartPoint
        {
            get { return m_StartPoint; }
            set
            {
                if (m_StartPoint != value)
                {
                    m_StartPoint = value;
                }
            }
        }
        
        
        public override enuBrushSupport BrushSupport
        {
            get { return enuBrushSupport.StrokeOnly; }
        }
        public override ICoreBrush GetBrush(enuBrushMode mode)
        {
            if (mode == enuBrushMode.Stroke )
            return this.m_StrokeBrush;
            return null;
        }
        protected override void InitGraphicPath(CoreGraphicsPath path)
        {
            path.Reset();
            path.AddLine(this.StartPoint, this.EndPoint);
        }

        public bool Accept(ICore2DDrawingVisitor visitor)
        {
            return (visitor != null);
                
        }

        public void Visit(ICore2DDrawingVisitor visitor)
        {
            visitor.DrawLine( this.StrokeBrush,
          this.StartPoint,
          this.EndPoint);
        }
    }
}

