

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: CornerLineElement.cs
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
file:CornerLineElement.cs
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
using System; 
using IGK.ICore;  using IGK.ICore.Drawing2D;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace IGK.DrSStudio.Drawing2D
{
    using IGK.ICore;
using IGK.ICore.Codec;
using IGK.ICore.Drawing2D.Codec;
using IGK.ICore.Drawing2D.Mecanism;
using IGK.ICore.MecanismActions;
using IGK.ICore.WinUI;
    using IGK.ICore.WinUI.Configuration;
    

    [LineCorner ("CornerLine", typeof (Mecanism))]
    class CornerLineElement : Core2DDrawingLayeredElement, ICore2DDrawingVisitable 
    {
        private ICorePen m_StrokeBrush;
        private CoreUnit m_Position;
        private enuCornerLineDirection m_Direction;
        public override bool CanRenderShadow
        {
            get
            {
                return false;
            }
        }
        public override CoreGraphicsPath  GetShadowPath()
        {
            return null;
        }
        [CoreXMLAttribute()]
        [CoreXMLDefaultAttributeValue (enuCornerLineDirection.Vertical )]
        public enuCornerLineDirection Direction
        {
            get { return m_Direction; }
            set
            {
                if (m_Direction != value)
                {
                    m_Direction = value;
                    OnPropertyChanged(CoreWorkingObjectPropertyChangedEventArgs.Definition);        
                }
            }
        }
        [CoreXMLAttribute ()]
        public CoreUnit Position
        {
            get{ return m_Position;}
            set{ 
            if (m_Position !=value)
            {
            m_Position =value;
            OnPropertyChanged(CoreWorkingObjectPropertyChangedEventArgs.Definition);
            }
            }
        }
        public override ICoreParameterConfigCollections GetParameters(ICoreParameterConfigCollections parameters)
        {
            parameters = base.GetParameters(parameters);
            parameters.AddGroup(CoreConstant.PARAM_DEFINITION).AddItem(GetType().GetProperty("Direction"));
            return parameters ;
        }
        public CornerLineElement(){ 
            m_StrokeBrush = new CorePen (this);
            m_StrokeBrush.SetSolidColor(Colorf.Black);
            this.m_Direction = enuCornerLineDirection.Vertical;
            this.m_Position = "50%";
            this.StrokeBrush.BrushDefinitionChanged += new EventHandler(StrokeBrush_BrushDefinitionChanged);
        }
        void StrokeBrush_BrushDefinitionChanged(object sender, EventArgs e)
        {
            OnPropertyChanged(new CoreWorkingObjectPropertyChangedEventArgs(
                (enuPropertyChanged)enu2DPropertyChangedType.BrushChanged));
        }
        public override  enuBrushSupport BrushSupport
        {
            get
            {
                return enuBrushSupport.StrokeOnly;
            }
        }
        public override void Dispose()
        {
            base.Dispose();
            if (this.m_StrokeBrush != null)
            {
                this.m_StrokeBrush.Dispose();
                this.m_StrokeBrush = null;
            }
        }
        [CoreXMLAttribute()]
       // [CoreXMLDefaultStrokeAttribute()]
        public ICorePen StrokeBrush
        {
            get { return m_StrokeBrush; }
        }
        public override bool CanReSize
        {
            get
            {
                return false;
            }
        }
        public override bool CanRotate
        {
            get
            {
                return false;
            }
        }
        public override bool CanScale
        {
            get
            {
                return false;
            }
        }
        public override bool CanTranslate
        {
            get
            {
                return false;
            }
        }
      
        public override bool  Contains(Vector2f position)
        {
            float f = ((ICoreUnitPixel)this.Position ).Value;
            Rectanglef rc = Rectanglef.Empty;
            float pwith = this.StrokeBrush.Width;
            switch (this.Direction )
            {
                case enuCornerLineDirection .Vertical:
                    rc.X = f;
                    rc.Inflate(pwith, pwith);
                    return rc.Contains(new Vector2f(position .X, 0));
                case enuCornerLineDirection .Horizontal:
                    rc.Y = f;
                    rc.Inflate(pwith, pwith);
                    bool g = rc.Contains(new Vector2f(0, position.Y));
                    return g;
                default:
                    break;
            }
 	         return base.Contains(position);
        }
        public void Visit(ICore2DDrawingVisitor visitor)
        {

            ICore2DDrawingDocument doc = GetParentDocument(this);
            if (doc == null) return;
            float x = 0.0f;
            float y = 0.0f;
            float w = doc.Width;
            float h = doc.Height;
            switch (this.Position.UnitType)
            {
                case enuUnitType.percent:
                    switch (this.Direction)
                    {
                        case enuCornerLineDirection.Horizontal:
                            y = h * this.Position.Value / 100.0f;
                            //g.DrawLine(v_p, 0, y, w, y);
                            break;
                        case enuCornerLineDirection.Vertical:
                            x = w * this.Position.Value / 100.0f;
                            break;
                        default:
                            break;
                    }
                    break;
                default:
                    switch (this.Direction)
                    {
                        case enuCornerLineDirection.Horizontal:
                            y = ((ICoreUnitPixel)this.Position).Value;
                            break;
                        case enuCornerLineDirection.Vertical:
                            x = ((ICoreUnitPixel)this.Position).Value;
                            break;
                        default:
                            break;
                    }
                    break;
            }
            switch (this.Direction)
            {
                case enuCornerLineDirection.Horizontal:
                    y = y < 0 ? 0 : y > h ? h : y;
                    visitor.DrawLine(this.StrokeBrush , 0, y, w, y);
                    break;
                case enuCornerLineDirection.Vertical:
                    x = x < 0 ? 0 : x > w ? w : x;
                    visitor.DrawLine(this.StrokeBrush, x, 0, x, h);
                    break;
                default:
                    break;
            }
        }
        class Mecanism :   Core2DDrawingSurfaceMecanismBase<CornerLineElement>
        {
            protected override void GenerateSnippets()
            {
                base.GenerateSnippets();
                AddSnippet(this.CurrentSurface.CreateSnippet(this, 0, 0));
                AddSnippet(this.CurrentSurface.CreateSnippet(this, 1, 1));
                AddSnippet(this.CurrentSurface.CreateSnippet(this, 2, 2));
            }
            protected override void InitSnippetsLocation()
            {
                base.InitSnippetsLocation();
                CornerLineElement v_l = this.Element;
                float f = 0.0f;
                float h = this.CurrentSurface.CurrentDocument.Height;
                float w = this.CurrentSurface.CurrentDocument.Width;
                if (v_l.Position.UnitType == enuUnitType.percent)
                {
                    switch (this.Element.Direction)
                    {
                        case enuCornerLineDirection.Horizontal:
                            f = h * v_l.Position.Value / 100.0f;
                            break;
                        default:
                            f = w * v_l.Position.Value / 100.0f;
                            break;
                    }
                }
                else 
                    f = ((ICoreUnitPixel)this.Element.Position).Value;
                switch (this.Element.Direction)
                {
                    case enuCornerLineDirection.Horizontal:
                        this.RegSnippets[0].Location = (CurrentSurface.GetScreenLocation(new Vector2f(0, f)));
                        this.RegSnippets[1].Location = (CurrentSurface.GetScreenLocation(new Vector2f(w / 2.0f, f)));
                        this.RegSnippets[2].Location = (CurrentSurface.GetScreenLocation(new Vector2f(w, f)));
                        break;
                    case enuCornerLineDirection.Vertical:
                        this.RegSnippets[0].Location = (CurrentSurface.GetScreenLocation(new Vector2f(f, 0)));
                        this.RegSnippets[1].Location = (CurrentSurface.GetScreenLocation(new Vector2f(f, h / 2.0f)));
                        this.RegSnippets[2].Location = (CurrentSurface.GetScreenLocation(new Vector2f(f, h)));
                        break;
                    default:
                        break;
                }
            }
            protected override void UpdateSnippetEdit(CoreMouseEventArgs e)
            {
                switch (this.Snippet.Demand)
                {
                    case 0:
                    case 1:
                    case 2:
                        UpdateDrawing(e);
                        break;
                }
                this.Snippet.Location = e.Location;
            }
            protected override void UpdateDrawing(CoreMouseEventArgs e)
            {
                float pos = 0.0f;
                switch (this.Element.Direction)
	            {
		            case enuCornerLineDirection.Horizontal:
                        pos = (e.FactorPoint.Y < 0) ? 0 : ((e.FactorPoint.Y > this.CurrentDocument.Height ) ? this.CurrentDocument.Height : e.FactorPoint.Y);
                        this.Element.m_Position = string.Format("{0}{1}", pos, "px");                         
                     break;
                    case enuCornerLineDirection.Vertical:
                        pos = (e.FactorPoint.X <0)? 0: ((e.FactorPoint.X>this.CurrentDocument.Width) ? this.CurrentDocument.Width : e.FactorPoint.X) ;
                     this.Element.m_Position = string.Format("{0}{1}", pos, "px"); 
                     break;
                    default:
                     break;
	            }
                this.Element.InitElement();
                this.Invalidate();
            }
            protected override void EndSnippetEdit(CoreMouseEventArgs e)
            {
                this.UpdateSnippetEdit(e);
                this.InitSnippetsLocation();
                this.Invalidate();
                
            }

            class ToggleAction : CoreMecanismActionBase
            {
                protected override bool PerformAction()
                {
                    Mecanism c =this.Mecanism as Mecanism;
                    if (c.Element != null)
                    {
                        c.Element.m_Position = "50%";
                        if (c.Element.Direction == enuCornerLineDirection.Vertical)
                            c.Element.Direction = enuCornerLineDirection.Horizontal;
                        else
                            c.Element.Direction = enuCornerLineDirection.Vertical;
                        c.Element.InitElement();
                        c.InitSnippetsLocation();
                        c.Invalidate();
                        return true;
                    }
                    return false;
                }
            }
            
        }

        protected override void InitGraphicPath(CoreGraphicsPath path)
        {
            path.Reset();
        }
        
        public override ICoreBrush GetBrush(enuBrushMode mode)
        {
            if (mode == enuBrushMode.Stroke)
                return this.StrokeBrush;
            return null;
        }

        public bool Accept(ICore2DDrawingVisitor visitor)
        {
            return (visitor != null);
        }

      
    }
}

