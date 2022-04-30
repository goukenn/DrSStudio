

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: WebMapStaticPolygon.cs
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
file:WebMapStaticPolygon.cs
*/
using IGK.ICore.WinUI.Configuration;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace IGK.DrSStudio.Web.WorkingObject
{
    [WebMapElementStandardAttribute("MapStaticPolyArea", typeof(Mecanism)
        ,Keys = enuKeys.P)]
    class WebMapStaticPolygon : WebMapCircle
    {
        private int m_Count;
        private float m_Angle;
        public override enuWebMapAreaType Type
        {
            get
            {
                return enuWebMapAreaType.Poly;
            }
        }
        [CoreXMLAttribute()]
        [CoreXMLDefaultAttributeValue(3)]
        public float Angle
        {
            get { return m_Angle; }
            set
            {
                if (m_Angle != value)
                {
                    m_Angle = value;
                    OnPropertyChanged(CoreWorkingObjectPropertyChangedEventArgs.Definition);
                }
            }
        }
        [CoreXMLAttribute()]
        [CoreXMLDefaultAttributeValue(3)]
        public int Count
        {
            get { return m_Count; }
            set
            {
                if (m_Count != value)
                {
                    m_Count = value;
                    OnPropertyChanged(CoreWorkingObjectPropertyChangedEventArgs.Definition);
                }
            }
        }
        public WebMapStaticPolygon()
        {
            this.m_Count = 3;
            this.m_Angle = 0;
        }
        protected override string GetCoords()
        {
            StringBuilder sb = new StringBuilder();
            Vector2f[] t = GetPoints();
            Vector2f c = Vector2f.Zero;
            for (int i = 0; i < t.Length; i++)
            {
                c = t[i];
                if (i > 0)
                    sb.Append(",");
                sb.Append(string.Format("{0},{1}", (int) c.X,(int) c.Y));
            }
            return sb.ToString();
        }
        protected override void InitGraphicPath(CoreGraphicsPath path)
        {

            path.Reset();
            path.AddPolygon(GetPoints());
            
        }
        protected virtual Vector2f[] GetPoints()
        {
            Vector2f[] vtab = new Vector2f[this.m_Count];
            float step = (float)((360 / (float)m_Count) * (Math.PI / 180.0f));
            float v_angle = (float)(m_Angle * CoreMathOperation.ConvDgToRadian);
            float r = this.Radius;
            Vector2i c = this.Center;
            for (int i = 0; i < this.m_Count; i++)
            {
                vtab[i] = new Vector2f(
                    (float)(c.X + this.Radius * Math.Cos(i * step + v_angle)),
                    (float)(c.Y + this.Radius * Math.Sin(i * step + v_angle)));
            }
            return vtab;
        }
        public override ICoreParameterConfigCollections GetParameters(ICoreParameterConfigCollections defaultParam)
        {
            defaultParam = base.GetParameters(defaultParam);
            Type t = this.GetType();
            var g = defaultParam.AddGroup("Default");
            g.AddItem(t.GetProperty("Angle"));
            g.AddItem(t.GetProperty("Count"));
            return defaultParam;
        }
        new  class Mecanism : IGK.DrSStudio.Drawing2D.Mecanism.CircleMecanism
        {
            protected override void UpdateDrawing(CoreMouseEventArgs e)
            {
                WebMapStaticPolygon c = this.Element as WebMapStaticPolygon;
                Vector2f v1 = new Vector2f(c.Center.X, c.Center.Y);
                Vector2f v2 = new Vector2f(e.FactorPoint.X, e.FactorPoint.Y);
                c.m_Angle = CoreMathOperation.GetAngle(v1, v2) * CoreMathOperation.ConvRdToDEGREE ;
                c.Radius = (int)CoreMathOperation.GetDistance(
                    v1,
                    v2
                    );
                this.Invalidate();
            }
            protected override void InitSnippetsLocation()
            {
                WebMapStaticPolygon c = this.Element as WebMapStaticPolygon;
                float v_angle = (c.Angle ) * CoreMathOperation.ConvDgToRadian ;
                this.RegSnippets[1].Location = CurrentSurface.GetScreenLocation(
                  new Vector2f(c.Center.X + (int)(this.Element.Radius * Math.Cos(v_angle)),
                      c.Center.Y + (int)(this.Element.Radius * Math.Sin (v_angle))
                      ));
                //center point
                RegSnippets[0].Location = CurrentSurface.GetScreenLocation(this.Element.Center);
            }
            protected override void UpdateSnippetEdit(CoreMouseEventArgs e)
            {
                if (this.Snippet != null)
                {
                    WebMapStaticPolygon c = this.Element as WebMapStaticPolygon;
                    switch (this.Snippet.Index)
                    {
                        case 0:
                            c.Center = Vector2i.Round(e.FactorPoint);
                            break;
                        case 1:
                Vector2f v1 = new Vector2f(c.Center.X, c.Center.Y);
                Vector2f v2 = new Vector2f(e.FactorPoint.X, e.FactorPoint.Y);
                           c.m_Angle =  CoreMathOperation.GetAngle(
                                               this.Element.Center, e.FactorPoint) * CoreMathOperation.ConvRdToDEGREE;
                           c.Radius = (int)CoreMathOperation.GetDistance(
                 v1,
                 v2
                 );
                           break;
                        default:
                            break;
                    }
                }
                this.CurrentSurface.RefreshScene();
            }
        }
    }
}

