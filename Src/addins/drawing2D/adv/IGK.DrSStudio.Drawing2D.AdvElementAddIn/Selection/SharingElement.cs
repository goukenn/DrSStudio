

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: SharingElement.cs
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
file:SharingElement.cs
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
using IGK.ICore.GraphicModels;
using IGK.ICore.WinUI;
using IGK.ICore.Drawing2D.Mecanism;

namespace IGK.DrSStudio.Drawing2D.Selection
{
#if DEBUG
    [Core2DDrawingSelection("SharingElement", typeof(Mecanism), 
        IsVisible = true,
        ImageKey=CoreImageKeys.DE_SELECTANDSHARE_GKDS)]
#endif
    public class SharingElement : Core2DDrawingObjectBase
    {
        private SharingElement()
        {
        }
        class Mecanism : Core2DDrawingSurfaceMecanismBase<Core2DDrawingLayeredElement>  
        {
            float m_shearingX;
            float m_shearingY;
            private Rectanglef m_bounds;
            private Matrix m_bckMatrix;
            public Mecanism():base()
            {
                this.DesignMode = false;
            }
            public override ICoreWorkingSurface Surface
            {
                get
                {
                    return base.Surface;
                }
            }
            public override void Render(ICoreGraphics e)
            {
                base.Render(e);
                if (this.Element != null)
                {
                    object  s = e.Save();
                    this.ApplyCurrentSurfaceTransform(e);
                    Matrix m = new Matrix();
                    m.Translate(this.m_bounds.X, this.m_bounds.Y);
                    m.Shear(this.m_shearingX, this.m_shearingY  );                    
                    e.MultiplyTransform(m, enuMatrixOrder.Prepend  );
                    m.Dispose ();
                    //e.Graphics.MultiplyTransform (m);
                    e.DrawRectangle(
                        Colorf.Blue,                   
                        0,0,
                        this.m_bounds.Width , 
                        this.m_bounds .Height );

                    CoreGraphicsPath p = new CoreGraphicsPath();
                    p.AddRectangle(new Rectanglef(0, 0, this.m_bounds.Width, this.m_bounds.Height));
                    m = new Matrix ();
                    m.Shear (this.m_shearingX , this.m_shearingY );
                    p.Transform(m);
                    e.DrawPath(Colorf.Yellow, p);
                    e.Restore(s);
                }
            }
            protected override void OnMouseDown(CoreMouseEventArgs e)
            {
                switch (e.Button)
                {
                    case enuMouseButtons.Left :
                        if (this.Element == null)
                        {
                            this.SelectOne(e);
                        }
                        break;
                }
            }
            protected override void OnElementChanged(CoreWorkingElementChangedEventArgs<Core2DDrawingLayeredElement> e)
            {
                
                base.OnElementChanged(e);
                if (this.Element != null)
                {
                    this.m_bounds = this.Element.GetPath().GetBounds();
                    this.m_shearingX = 0;
                    this.m_shearingY = 0;
                    this.m_bckMatrix = this.Element.GetMatrix().Clone () as Matrix ;
                    this.GenerateSnippets();
                    this.InitSnippetsLocation();
                    this.Invalidate();
                }
            }
            private void SelectOne(CoreMouseEventArgs e)
            {
                ICore2DDrawingLayeredElement[] t = this.CurrentLayer.Elements.ToArray();
                if (t == null)
                    return;
                for (int i = t.Length - 1; i >= 0; i--)
                {
                    if (t[i].Contains(e.FactorPoint))
                    {
                        this.Element = t[i] as Core2DDrawingLayeredElement ;
                        break;
                    }
                }
            }
            protected override void OnMouseMove(CoreMouseEventArgs e)
            {
                switch (e.Button)
                {
                    case enuMouseButtons.Left:
                        if (this.Element != null)
                        {
                            UpdateSnippetEdit(e);
                        }
                        break;
                }
            }
            protected override void OnMouseUp(CoreMouseEventArgs e)
            {
            }
            protected override void GenerateSnippets()
            {
                base.GenerateSnippets();
                //2 snippet pour la definition
                this.AddSnippet (this.CurrentSurface.CreateSnippet (this, 0,0));
                this.AddSnippet(this.CurrentSurface.CreateSnippet(this, 1,1));
            }
            protected override void InitSnippetsLocation()
            {
                base.InitSnippetsLocation();
                if (this.Element != null)
                { 
                    Vector2f  [] pts = CoreMathOperation .GetResizePoints (this.m_bounds );
                    RegSnippets[0].Location = CurrentSurface.GetScreenLocation(pts[3]);
                    RegSnippets[1].Location = CurrentSurface.GetScreenLocation(pts[5]);
                }
            }
            protected override void UpdateSnippetEdit(CoreMouseEventArgs e)
            {
                Core2DDrawingLayeredElement v_el = this.Element;
                if ((this.Element != null) && (this.Snippet !=null))
                {
                    switch (this.Snippet.Index)
                    {
                        case 0:
                            //sharing x
                            this.m_shearingX = (e.FactorPoint.X - this.m_bounds.Right)
                                /
                                this.m_bounds.Width ;
                                //this.m_bounds.Height;
                            break;
                        case 1:
                            //sharing y
                            this.m_shearingY = (e.FactorPoint.Y - this.m_bounds.Bottom)/
                                //this.m_bounds.Width ;
                            this.m_bounds.Height ;
                            break;
                    } 
                    this.Snippet.Location = e.Location;
                    Matrix m = this.m_bckMatrix.Clone() as Matrix ;
                    enuMatrixOrder order = enuMatrixOrder.Append;
                    m.Translate(-m_bounds.X, -m_bounds.Y, order);
                    m.Shear(this.m_shearingX, this.m_shearingY, order);
                    m.Translate(m_bounds.X, m_bounds.Y, order);
                   
                    this.Element.ClearTransform();
                    this.Element.Transform(m);
                    
                    this.Invalidate();
                }
            }
        }
    }
}

