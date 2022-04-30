

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: PolygonFace.cs
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
file:PolygonFace.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
namespace IGK.DrSStudio.Drawing2D
{
    using IGK.ICore;using IGK.DrSStudio.Drawing3D;
    public class PolygonFace : 
        Core2DDrawingLayeredDualBrushElement  ,
        ICorePolygonFace 
    {
        ICoreCameraOwner element;
        private Vector3f[] m_Points;
        public Vector3f[] Points
        {
            get { return m_Points; }
        }
        public PolygonFace(ICoreCameraOwner element, 
            Vector3f[] points)
        {
            this.element = element;
            this.m_Points  = points;            
        }
        #region IComparer Members
        public float GetCullFactor()
        {
            float a = 0.0f;
            Vector3f[] screen = new Vector3f[m_Points.Length];
            for (int i = 0; i < m_Points.Length; i++)
            {
                screen[i] = Matrix4x4f.GluProject(m_Points[i],
                    this.element.Camera.ModelView,
                    this.element.Camera.Projection,
                    this.element.Camera.Viewport);
            }
            int m = 0;
            for (int i = 0; i < m_Points.Length; i++)
            {
                m = (i + 1) % m_Points.Length;
                a += screen[i].X * screen[m].Y - (screen[m].X * screen[i].Y);
            }
            a /= 0.5f;
            return a;
        }
        public override void Draw(Graphics g)
        {
            //GraphicsPath p = new GraphicsPath();
            Vector2f[] t = new Vector2f[4];
            t[0] =(Vector2f ) this.element.GetWindowCoord(m_Points[0]);
            t[1] = (Vector2f)this.element.GetWindowCoord(m_Points[1]);
            t[2] = (Vector2f)this.element.GetWindowCoord(m_Points[2]);
            t[3] = (Vector2f)this.element.GetWindowCoord(m_Points[3]);
            //p.AddPolygon(t);
            Brush br = this.FillBrush.GetBrush();
            Pen p = this.StrokeBrush.GetPen();
            if (p!= null)
            g.FillPolygon(br, t);
            if (p!=null)
            g.DrawPolygon(p, t);
        }
        #endregion
        public class PolygonFaceComparer : 
            System.Collections.IComparer,
            System.Collections.Generic.IComparer<PolygonFace >
        {
            public int Compare(object x, object y)
            {
                PolygonFace c1 = (PolygonFace)x;
                PolygonFace c2 = (PolygonFace)y;
                return c1.GetCullFactor().CompareTo(c2.GetCullFactor());
            }
            #region IComparer<PolygonFace> Members
            public int Compare(PolygonFace x, PolygonFace y)
            {
                return y.GetCullFactor().CompareTo(x.GetCullFactor());
            }
            #endregion
        }
        protected override void GeneratePath()
        {
            this.SetPath(null);
        }
    }
}

