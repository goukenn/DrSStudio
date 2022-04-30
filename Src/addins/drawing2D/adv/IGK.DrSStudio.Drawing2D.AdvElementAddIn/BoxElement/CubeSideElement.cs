

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: CubeSideElement.cs
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
file:CubeSideElement.cs
*/
using System; 
using IGK.ICore; using IGK.ICore.Drawing2D;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
namespace IGK.DrSStudio.Drawing2D.BoxElement
{
    using IGK.ICore.Codec;
    using IGK.ICore.WinUI;
    [IGKD2DDrawingAdvancedElement("CubeSize", typeof(Mecanism)
       )]
    class CubeSideElement : RectangleElement 
    {
        private Vector2f  m_Offset;

        [CoreXMLAttribute ()]        
        public Vector2f  Offset
        {
            get { return m_Offset; }
            set
            {
                if (m_Offset != value)
                {
                    m_Offset = value;
                    OnPropertyChanged(CoreWorkingObjectPropertyChangedEventArgs.Definition);
                }
            }
        }
        public CubeSideElement()
        {
            this.m_Offset = new Vector2f(30, 0);
        }
       protected override void InitGraphicPath(CoreGraphicsPath path)
{
 	
            Rectanglef v_bound = this.Bounds;
            path.Reset();
            if (v_bound.IsEmpty)
            {
               
                return;
            }
            Vector2f offset = new Vector2f (this.m_Offset.X , 0.0f);
            Vector2f offsety = new Vector2f(0.0f, this.m_Offset.Y );
            path.SetMarkers();
            path.AddPolygon (new Vector2f []{
                v_bound.Location + offset ,
                v_bound.MiddleLeft + offsety ,
                v_bound.MiddleRight - (offset* 2.0f ) + offsety ,
                v_bound.TopRight - offset 
            });
            path.SetMarkers();
            path.AddPolygon(new Vector2f[] { 
                v_bound .MiddleLeft + offsety ,
                v_bound .BottomLeft +offset ,
                v_bound .BottomRight -offset,
                v_bound .MiddleRight - (offset * 2.0f ) + offsety 
            });
            if (offset != Vector2f.Zero)
            {
                path.SetMarkers();
                path.AddPolygon(new Vector2f[] { 
                v_bound.TopRight - offset ,
                v_bound.MiddleRight - (offset* 2.0f )+offsety ,
                v_bound .BottomRight -offset,
                v_bound .MiddleRight - offsety 
            });
            }
            path.CloseFigure();
            
        }

        /// <summary>
        /// represent a cube side mecanism element
        /// </summary>
        new class Mecanism : RectangleElement.Mecanism
        {
            const int SNIPPET_INDEX = 14;
            protected override void GenerateSnippets()
            {
                base.GenerateSnippets();
                this.AddSnippet(this.CurrentSurface.CreateSnippet (this, SNIPPET_INDEX, SNIPPET_INDEX));
                this.AddSnippet(this.CurrentSurface.CreateSnippet (this,SNIPPET_INDEX+1, SNIPPET_INDEX+1));
            }
            protected override void InitSnippetsLocation()
            {
                base.InitSnippetsLocation();
                CubeSideElement l = this.Element as CubeSideElement ;
                if (this.RegSnippets.Count > 0)
                {
                    if (this.RegSnippets.Contains(SNIPPET_INDEX))
                    {
                        this.RegSnippets[SNIPPET_INDEX].Location = this.CurrentSurface.GetScreenLocation(l.Bounds.Location + new Vector2f(l.Offset.X, 0));
                    }
                    this.RegSnippets[SNIPPET_INDEX + 1].Location = this.CurrentSurface.GetScreenLocation(l.Bounds.MiddleRight - new Vector2f(l.Offset.X * 2.0f, 0.0f) + new Vector2f(0, l.Offset.Y));
                }
            }
            protected override void UpdateSnippetEdit(CoreMouseEventArgs e)
            {
                CubeSideElement l = this.Element as CubeSideElement;
                switch (this.Snippet.Demand)
                {
                    case SNIPPET_INDEX:
                        {
                            this.Snippet.Location = e.Location;
                            Vector2f v_offset = Vector2f.Zero;
                            v_offset = this.CurrentSurface.GetFactorLocation(this.Snippet.Location) - l.Bounds.Location;
                            l.m_Offset = new Vector2f(Math.Abs(v_offset.X), l.Offset.Y );
                            l.InitElement();
                            this.Invalidate();
                            return;
                        }
                    case SNIPPET_INDEX + 1:
                        {
                            this.Snippet.Location = e.Location;
                            Vector2f v_offset = Vector2f.Zero;
                            Vector2f v_df = l.Bounds.MiddleRight - new Vector2f(2 * l.Offset.X, 0.0f);
                            v_offset = this.CurrentSurface.GetFactorLocation(this.Snippet.Location) - v_df;
                            float v_y = l.Bounds .Height /2.0f;
                            v_offset.Y = (v_offset .Y < -v_y)? -v_y : (v_offset .Y > v_y)? v_y : v_offset .Y ;
                            l.m_Offset = new Vector2f(l.Offset.X , v_offset.Y);
                            l.InitElement();
                            this.Invalidate();
                        }
                        return;
                }
                base.UpdateSnippetEdit(e);
            }
        }
    }
}

