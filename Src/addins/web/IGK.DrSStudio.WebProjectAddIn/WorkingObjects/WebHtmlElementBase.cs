

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: WebHtmlElementBase.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/

using IGK.ICore;
using IGK.ICore.Codec;
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
/*
IGKDEV @ 2008-2016
author: C.A.D . BONDJE DOUE
file:WebHtmlElementBase.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace IGK.DrSStudio.WebProjectAddIn.WorkingObjects
{
    using IGK.ICore.Xml ;
    using IGK.ICore.WinUI.Configuration;
    /// <summary>
    /// represent a html element base
    /// </summary>
    public abstract class WebHtmlElementBase : 
        RectangleElement ,
        IWebElementView,
        ICoreBrushOwner 
    {
        private WebRectangle m_Bound;
       private CoreXmlElementBase  m_Node;
        /// <summary>
        /// get the html element parent
        /// </summary>
       public new WebHtmlElementBase Parent {
           get {
               return base.Parent as WebHtmlElementBase;
           }
       }
       private HtmlStyleProperty m_Style;
       public Rectanglei GetRealBound()
       {
           return this.Bound.GetRect(this.GetParentBound());
       }
       public Rectanglei GetParentBound()
       {
           if (this.Parent != null)
               return this.Parent.GetRealBound();
           else {
               return (this.ParentDocument != null) ? this.ParentDocument.Bounds : Rectanglei.Empty;
           }
       }
       public HtmlStyleProperty Style
       {
           get { return m_Style; }
       }
       public CoreXmlElementBase  Node
       {
           get { return m_Node; }
       }
        [CoreXMLAttribute ()]
        /// <summary>
        /// get the bound of this element
        /// </summary>
        public WebRectangle  Bound
        {
            get { return m_Bound; }
            set
            {
                if (!m_Bound.Equals ( value))
                {
                    m_Bound = value;
                    OnPropertyChanged(HtmlWebPropertyChangedEventArgs.BoundChanged );
                }
            }
        }
        protected virtual int DefaultHeight {
            get {
                return 24;
            }
        }
        public override bool CanRenderShadow
        {
            get
            {
                return false;
            }
        }
        protected virtual int DefaultWidth {
            get {
                return 150;
            }
        }
        public override bool CanScale
        {
            get
            {
                return false;
            }
        }
        //public override bool CanResize
        //{
        //    get
        //    {
        //        return false;
        //    }
        //}
        public override bool CanRotate
        {
            get
            {
                return base.CanRotate;
            }
        }
        protected override void BuildBeforeResetTransform()
        {
            base.BuildBeforeResetTransform();
        }
        protected override void OnPropertyChanged(CoreWorkingObjectPropertyChangedEventArgs e)
        {
            HtmlWebPropertyChangedEventArgs v_e = e as HtmlWebPropertyChangedEventArgs;
            if (v_e != null)
            {
                switch ((int)v_e.ID)
                {
                    case HtmlWebPropertyChangedEventArgs.BaseEventPos:
                        InitElement();
                        break ;
                }
            }
            base.OnPropertyChanged(e);
        }
        /// <summary>
        /// .ctr
        /// </summary>
        public WebHtmlElementBase()
        {
            this.m_Node = CreateNode();
            this.m_Bound = new Rectanglei(0, 0, this.DefaultHeight, this.DefaultHeight);
            this.m_Style = new HtmlStyleProperty(this);
            this.m_Style.StyleChanged += new EventHandler(m_Style_StyleChanged);
            this.m_Node["id"] = this.Id;
            this.m_Node["style"] = this.m_Style.Attribute;
            this.m_Anchor = enuWebAnchor.Left | enuWebAnchor.Top;
            this.PropertyChanged += new CoreWorkingObjectPropertyChangedEventHandler(HtmlElementBase_PropertyChanged);
        }
        void HtmlElementBase_PropertyChanged(object o, CoreWorkingObjectPropertyChangedEventArgs e)
        {
            if (e == CoreWorkingObjectPropertyChangedEventArgs.Id)
            {
                this.m_Node["id"] = this.Id;
            }
        }
        void m_Style_StyleChanged(object sender, EventArgs e)
        {
            OnPropertyChanged(new Core2DDrawingElementPropertyChangeEventArgs(enu2DPropertyChangedType.BrushChanged));
        }
        protected abstract CoreXmlElementBase CreateNode();
        //protected override void GeneratePath()
        //{
        //    Graphics v_path = new Graphics();
        //    Rectanglei rc = this.GetRealBound();
        //    v_path.AddRectangle( 
        //        rc
        //        );
        //    this.SetPath(v_path);
        //}
        public override enuParamConfigType GetConfigType()
        {
            return base.GetConfigType();
        }
        public override ICoreParameterConfigCollections GetParameters(ICoreParameterConfigCollections parameters)
        {
            parameters = base.GetParameters(parameters);
            return parameters;
        }
        public override   enuBrushSupport BrushSupport
        {
            get { return enuBrushSupport.All; }
        }
        public override  ICoreBrush GetBrush(enuBrushMode mode)
        {
            switch ((enuHtmlBrushType)mode)
            {
                case enuHtmlBrushType.Fill:
                    return this.Style.FillBrush;
                case enuHtmlBrushType.Stroke:
                    return this.Style.StrokeBrush;
                case enuHtmlBrushType.BorderLeft:
                    break;
                case enuHtmlBrushType.BorderTop:
                    break;
                case enuHtmlBrushType.BorderRight:
                    break;
                case enuHtmlBrushType.BorderBottom:
                    break;
                default:
                    break;
            }
            return null;
        }
        internal protected new class Mecanism : HtmlDesignMecanism<WebHtmlElementBase >
        {
            public WebHtmlLayoutLayer LayoutLayer {
                get {
                    return this.CurrentLayer as WebHtmlLayoutLayer;
                }
            }
            protected void SuspendLayout()
            {
                if (LayoutLayer == null) return;
                LayoutLayer.SuspendLayout();
            }
            protected void ResumeLayout()
            {
                if (LayoutLayer == null) return;
                LayoutLayer.ResumeLayout ();
            }
            protected void PerformLayout()
            {
                if (LayoutLayer == null) return;
                LayoutLayer.PerformLayout();
            }
                public new WebHtmlElementBase Element
                {
                    get { return base.Element as WebHtmlElementBase; }
                    set { base.Element = value; }
                }
                public Mecanism()
                {
                }
                protected override void OnMouseDown(CoreMouseEventArgs e)
                {
                    switch (e.Button)
                    {
                        case enuMouseButtons.Left:
                            switch (this.State)
                            {
                                case ST_NONE:
                                case ST_CREATING:
                                    this.CreateAndAddElement(e);
                                    break;
                                case ST_EDITING:
                                    if (this.Snippet != null)
                                    {
                                    }
                                    else
                                    {
                                        if ((this.Element != null) && (this.Element.Contains(e.FactorPoint)))
                                        {
                                            this.BeginMove(e);
                                        }
                                        else
                                        {
                                            if (this.Snippet == null)
                                            {
                                                this.State = ST_NONE;
                                                this.CreateAndAddElement(e);
                                            }
                                        }
                                    }
                                    break;
                            }
                            break;
                    }
                }
                protected override void InitNewCreatedElement(WebHtmlElementBase element, Vector2f location)
                {
                    base.InitNewCreatedElement(element, location);
                }
                private void CreateAndAddElement(CoreMouseEventArgs e)
                {
                    this.Element = this.CreateNewElement() as WebHtmlElementBase;
                    if (this.Element != null)
                    {
                        InitNewCreatedElement(this.Element, e.FactorPoint );
                        this.StartPoint = e.FactorPoint;
                        this.EndPoint = e.FactorPoint;
                        this.SuspendLayout();
                        this.CurrentSurface.CurrentDocument.CurrentLayer.Elements.Add(this.Element);
                        this.CurrentSurface.CurrentDocument.CurrentLayer.Select(this.Element);
                        this.Element.InitElement();
                        this.State = ST_CREATING;
                    }
                }
                protected override void OnMouseMove(CoreMouseEventArgs e)
                {
                    switch (e.Button)
                    {
                        case enuMouseButtons.Left:
                            switch (this.State)
                            {
                                case ST_NONE:
                                    break;
                                case ST_CREATING:
                                    UpdateDrawing(e);
                                    break;
                                case ST_EDITING:
                                    if (this.Snippet != null)
                                        UpdateSnippetEdit(e);
                                    break;
                                case ST_MOVING:
                                    if (this.Element != null)
                                    {
                                        this.UpdateMove(e);
                                    }
                                    else
                                        this.State = ST_NONE;
                                    break;
                            }
                            break;
                    }
                }
                protected override void EndDrawing(CoreMouseEventArgs e)
                {
                    UpdateDrawing(e);
                    this.State = ST_EDITING;
                    this.ResumeLayout();
                    this.PerformLayout();
                    this.Element.OnPropertyChanged(CoreWorkingObjectPropertyChangedEventArgs.Definition);
                    this.GenerateSnippets();
                    this.InitSnippetsLocation();
                }
                protected override void OnMouseUp(CoreMouseEventArgs e)
                {
                    switch (e.Button)
                    {
                        case enuMouseButtons.Left:
                            switch (this.State)
                            {
                                case ST_NONE:
                                    break;
                                case ST_CREATING:
                                    EndDrawing(e);
                                    break;
                                case ST_EDITING:
                                    if (this.Snippet != null)
                                    {
                                        UpdateSnippetEdit(e);
                                        this.Element.OnPropertyChanged(CoreWorkingObjectPropertyChangedEventArgs.Definition);
                                        InitSnippetsLocation();
                                    }
                                    break;
                                case ST_MOVING:
                                    if (this.Element != null)
                                    {
                                        EndMove(e);
                                    }
                                    break;
                            }
                            break;
                    }
                }
                protected override void UpdateDrawing(CoreMouseEventArgs e)
                {
                    this.EndPoint = e.FactorPoint;
                    this.Element.m_Bound =
                        Rectanglei.Round (CoreMathOperation.GetBounds(this.StartPoint, this.EndPoint));
                    this.Element.InitElement();
                    this.CurrentSurface.Invalidate();
                }
                protected override void UpdateSnippetEdit(CoreMouseEventArgs e)
                {
                    Rectanglei v_rc = this.Element.GetRealBound();
                    this.Snippet.Location = e.Location;
                    switch (this.Snippet.Index)
                    {
                        case 3:
                            if (e.FactorPoint.X < v_rc.Right)
                            {
                                v_rc.Width =(int)Math.Ceiling (  Math.Abs(v_rc.Right - e.FactorPoint.X));
                                v_rc.X =(int)Math.Ceiling (  e.FactorPoint.X);
                            }
                            break;
                        case 0:
                            if (e.FactorPoint.Y < v_rc.Bottom)
                            {
                                v_rc.Height =(int)Math.Ceiling (  Math.Abs(v_rc.Bottom - e.FactorPoint.Y));
                                v_rc.Y =(int)Math.Ceiling (  e.FactorPoint.Y);
                            }
                            break;
                        case 2:
                            if (e.FactorPoint.Y > v_rc.Top)
                            {
                                v_rc.Height =(int)Math.Ceiling (  Math.Abs(e.FactorPoint.Y - v_rc.Top));
                            }
                            break;
                        case 1:
                            if (e.FactorPoint.X > v_rc.Left)
                            {
                                v_rc.Width =(int)Math.Ceiling ( Math.Abs(e.FactorPoint.X - v_rc.Left));
                                //v_rc.X = e.FactorPoint.X;
                            }
                            break;
                    }
                    this.Element.m_Bound = v_rc;
                    this.Element.InitElement();
                    this.CurrentSurface.Invalidate();
                }
                protected override void GenerateSnippets()
                {
                    base.GenerateSnippets();
                    AddSnippet(this.CurrentSurface.CreateSnippet(this, 0, 0));
                    AddSnippet(this.CurrentSurface.CreateSnippet(this, 1, 1));
                    AddSnippet(this.CurrentSurface.CreateSnippet(this, 2, 2));
                    AddSnippet(this.CurrentSurface.CreateSnippet(this, 3, 3));
                }
                protected override void InitSnippetsLocation()
                {
                    if (this.Element == null) return;
                    Vector2f[] t = CoreMathOperation.GetResizePoints(this.Element.Bound.GetRect (this.CurrentDocument.Bounds));
                    this.RegSnippets[0].Location = CurrentSurface.GetScreenLocation(t[1]);
                    this.RegSnippets[1].Location = CurrentSurface.GetScreenLocation(t[3]);
                    this.RegSnippets[2].Location = CurrentSurface.GetScreenLocation(t[5]);
                    this.RegSnippets[3].Location = CurrentSurface.GetScreenLocation(t[7]);
                }
            }
        internal protected class Macanism<T> : Mecanism 
            where T :  WebHtmlElementBase
        {
            public new T Element {
                get {
                    return (T) base.Element;
                }
            }
        }
        //public override void Draw(Graphics g)
        //{
        //    WebRenderingEventArgs e = new WebRenderingEventArgs(g, this.GetRealBound());
        //    HtmlRenderer.RenderBackground(this,e);
        //    HtmlRenderer.RenderBorder(this, e);
        //    HtmlRenderer.RenderContent(this, e);
        //}
        enuWebAnchor m_Anchor;
        public enuWebAnchor Anchor
        {
            get
            {
                return this.m_Anchor;
            }
            set
            {
                this.m_Anchor = value;
            }
        }
        /// <summary>
        /// return the preferred size of this element
        /// </summary>
        /// <param name="v_parentDocument"></param>
        /// <returns></returns>
        public Size2i GetPreferredSize(Rectanglei v_parentDocument)
        {
            return new Size2i((int)this.Bound.Width.GetValue (v_parentDocument .Width, enuWebUnitType .px  ),
                (int) this.Bound.Height.GetValue (v_parentDocument.Height , enuWebUnitType .px ));
        }
        IWebElementView IWebElementView.Parent
        {
            get { return this.Parent; }
        }
        protected override void InitGraphicPath(CoreGraphicsPath path)
        {
            path.Reset();
            path.AddRectangle(this.Bounds);
        }
    }
}

