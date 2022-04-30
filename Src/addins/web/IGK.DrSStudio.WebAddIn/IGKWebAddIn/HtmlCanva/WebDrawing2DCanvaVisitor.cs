

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: WebDrawing2DCanvaVisitor.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/

using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
﻿using System;
using System.Collections.Generic;

namespace IGK.DrSStudio.WebAddIn.HtmlCanva
{
    using IGK.ICore;
    using IGK.ICore.Web;
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
    /// <summary>
    /// generate canvas base code
    /// </summary>
    class WebDrawing2DCanvaVisitor : CoreEncoderVisitor
    {
        private StringBuilder m_sb;
        private float m_alpha;
        private Colorf m_fillColor;
        private Colorf m_strokeColor;
        private float m_lineWidth;
        private string m_lineJoin;
        private string m_lineCap;

        private enuCompositingMode m_compositingMode;
        
        private bool m_imageRes = true;
        private List<string> m_images;
        private List<string> m_vars; //declared variable


        public string LineFeed { get; set; }

        void Append(string msg) {
            this.m_sb.Append(msg + this.LineFeed);
        }

        public void SetAlpha(float alpha)
        {
            if (this.m_alpha != alpha)
            {
                this.m_alpha = alpha;
                Append(string.Format("v_ctx.globalAlpha={0};", alpha));
            }
        }
        public void SetFillColor(Colorf cl)
        {
            if (!this.m_fillColor.Equals (cl))
            {
                this.m_fillColor = cl;     
            }
            Append(string.Format("v_ctx.fillStyle = \"{0}\";", cl.ToString(true)));
        }
        public WebDrawing2DCanvaVisitor():base()
        {
            m_vars = new List<string>();
            m_sb = new StringBuilder();
            m_alpha = 1.0f;
            m_images = new List<string>();
            m_imageRes = false;
            LineFeed = Environment.NewLine;
            m_lineJoin = "round";
            m_lineCap = "round";
        }
        public override void Flush()
        {
        }
        internal void SaveTo(string filename)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(_header());
            if (this.m_imageRes)
            {
                sb.Append(
@"var v_imgs = [];
var v_loaded = 0;");
                sb.Append(@"function imgLoadedComplete(){ v_loaded++;};");
                sb.Append(@"function initimages(){");
                sb.Append(@"var img=null;");
                foreach (string data in this.m_images)
                {

                    sb.Append(@"img = document.createElement(""img""); img.onload = imgLoadedComplete; v_imgs.push(img);");

                    sb.Append(string.Format(@"img[""src""] = ""{0}""", data));
                }
                sb.Append(@"};");
                sb.Append(@"initimages();");
                sb.Append(@"function waitforloading(){
	if (v_loaded<v_imgs.length){
	setTimeout(waitforloading,100);
	}
	else{
	render();
	}
}
waitforloading();
function render(){");
                sb.Append(this.m_sb.ToString());
                sb.Append("}");
                
            }
            else
            {
                sb.Append(this.m_sb.ToString());
            }
            File.WriteAllText(filename, sb.ToString());
        }

        private string _header()
        {
            StringBuilder sb = new StringBuilder ();
            sb.Append("/*canvas expression generate with drsstudio "+CoreConstant.VERSION+" */" + this.LineFeed );
            sb.Append("/*author : C.A.D. BONDJE DOUE */" + this.LineFeed);
            sb.Append("if (!canva) throw 'no canva ';"+this.LineFeed);  
            sb.Append("if (!v_ctx) throw 'no canva context';"+this.LineFeed);            
            return sb.ToString();
        }
        public void Clear()
        {
            m_sb.Clear();
        }
        public void Visit(ICore2DDrawingDocument document)
        {

            Append($"/* Document : {document.Id}*/");
            Append("canva.width = " + document.Width + ";");
            Append("canva.height = " + document.Height + ";");
            Append($"v_ctx.lineJoin=\"{m_lineJoin}\";");
            Append($"v_ctx.lineCap=\"{m_lineCap}\";");

            foreach (ICore2DDrawingLayer layer in document.Layers)
            {
                Append($"/* Layer : {layer.Id}*/");

                Append("v_ctx.save();");
                if (layer.Opacity < 1) {
                    Append($"v_ctx.globalAlpha={layer.Opacity};");
                }
                
                this.Visit(layer);
                Append("v_ctx.restore();");
            }
        }
        public void Visit(ICore2DDrawingLayer layer)
        {
            foreach (ICore2DDrawingLayeredElement item in layer.Elements)
            {
                if (item.View)
                    this.Visit(item as object );
            }
        }
        public void Visit(ICore2DDrawingLayeredElement element)
        {
            Append("/*no rendering " + element.Id+"*/");
        }
     

        private bool BindStrokeBrush(ICoreBrushOwner owner)
        {
            if (owner == null)
                return false;
            if ((owner.BrushSupport & enuBrushSupport.Stroke) == enuBrushSupport.Stroke)
            {
                var p = (ICorePen)owner.GetBrush(enuBrushMode.Stroke);
                BuildStrokeStyle(p);
                return true;
            }
            return false;
        }

        private bool BindFillBrush(ICoreBrushOwner owner)
        {
            if (owner == null)
                return false ;
            if ((owner.BrushSupport & enuBrushSupport.Fill) == enuBrushSupport.Fill)
            {
                var s = owner.GetBrush(enuBrushMode.Fill);
                if (s != null)
                {
                    BuildFillStyle(s);
                    return true;
                }
            }
            return false;
        }

        private void BuildStrokeStyle(ICorePen p)
        {
            switch (p.BrushType)
            {
                case enuBrushType.Custom:
                    break;
                case enuBrushType.Hatch:
                    break;
                case enuBrushType.LinearGradient:
                    Append(GetLinearDefinition(p));
                    Append("v_ctx.strokeStyle=grd;");
                    break;
                case enuBrushType.PathGradient:
                    break;
                case enuBrushType.Solid:
                    this.SetStrokeColor(p.Colors[0]);              
                    this.SetAlpha(p.Colors[0].A);
                    break;
                case enuBrushType.Texture:
                    break;
                default:
                    break;
            }
            this.SetLineWidth(p.Width);

            string cap = "butt";
            switch (p.DashCap)
            {
                case enuDashCap.Round:
                    cap = "round";
                    break;
                case enuDashCap.Flat:
                    cap = "square";
                    break;
            }

            string miter = "miter";
            switch (p.LineJoin) {
                case enuLineJoin.Bevel:
                    miter = "bevel";
                    break;
                case enuLineJoin.Round:
                    miter = "round";
                    break;
            }


            //init data style
            if (cap != m_lineCap)
            Append(string.Format($"v_ctx.lineCap = \"{cap}\";",""));
            if (miter!= m_lineJoin)
            Append(string.Format($"v_ctx.lineJoin = \"{miter}\";",""));

        }

        private void SetLineWidth(float p)
        {
            if (p!=1.0f)
            {
                this.m_lineWidth = p;
                Append(string.Format("v_ctx.lineWidth={0};", p));
            }
        }

        private void SetStrokeColor(Colorf cl)
        {
            if (!this.m_strokeColor.Equals (cl))
            {
                this.m_strokeColor = cl;

                Append(string.Format("v_ctx.strokeStyle = \"{0}\";",
                    cl.ToString(true)));
            }
        }
        private static string GetPathLinearDefinition(ICoreBrush brush)
        {
            Colorf[] cl = brush.Colors;
            enuPathBrushMode m = brush.PathBrushMode;
            StringBuilder sb = new StringBuilder();
            switch (m)
            {
                case enuPathBrushMode.CustomRectangle:
                    break;
                case enuPathBrushMode.InnerEllipse:
                    break;
                case enuPathBrushMode.OuterEllipse:
                    break;
                case enuPathBrushMode.Path:
                    break;
                case enuPathBrushMode.Rectangle:
                    break;
                default:
                    break;
            }
            return sb.ToString();
        }

        private string GetLinearDefinition(ICoreBrush brush)
        { 
            Colorf[] cl = brush.Colors ;
            enuPathBrushMode m = brush.PathBrushMode;
            StringBuilder sb = new StringBuilder();
            Rectanglef v_rc = CoreMathOperation.GetBounds(Vector2f.Zero, new Vector2f(0, 1));
            Matrix v_m = null;
            CoreGraphicsPath v_p = null;
            if (brush.Owner != null)
            {

                if (brush.AutoSize)
                {
                    CoreGraphicsPath cp = brush.Owner.GetPath();
                    v_rc = cp != null ? cp.GetBounds() : new Rectanglef(0, 0, 1, 1);
                }
                else
                {
                    v_rc = Rectanglef.Round(
             new Rectanglef(brush.Bounds.X,
             brush.Bounds.Y,
             Math.Max(1.0f, brush.Bounds.Width),
             Math.Max(1.0f, brush.Bounds.Height)));
                }
            }
            else
            {
                if (brush.AutoSize)
                {
                    if (brush.Owner != null)
                    {
                        v_m = brush.Owner.GetMatrix();
                        v_p = brush.Owner.GetPath();
                        if (v_p != null)
                        {
                            v_p = v_p.Clone() as CoreGraphicsPath;
                            v_rc = Rectanglei.Round((Rectanglef)v_p.GetBounds());
                        }
                        else
                        {
                            v_rc = Rectanglei.Round(v_p.GetBounds());
                        }
                    }
                }
                else
                {
                    v_rc = Rectanglef.Round(
                        new Rectanglef(brush.Bounds.X,
                        brush.Bounds.Y,
                        Math.Max(1.0f, brush.Bounds.Width),
                        Math.Max(1.0f, brush.Bounds.Height)));
                }
            }



            if (!m_vars.Contains("grd"))
            {
                sb.Append(@"var grd = null;");
            }
            //get de definition point
            Vector2f pt1 = new Vector2f(0, 0);
            Vector2f pt2 = new Vector2f(v_rc.Right - v_rc.X, v_rc.Bottom - v_rc.Top );
            sb.Append(string.Format (@"grd = v_ctx.createLinearGradient(0, 0, {2}, {3});", 
                pt1.X ,
                pt1.Y,
                pt2.X,
                pt2.Y));

            for (int i = 0; i < cl.Length; i++)
			{
                sb.Append(string.Format (@"grd.addColorStop({0}, ""{1}""); ", i,cl[i].ToString (true)));
			}

            return sb.ToString();
        }
        private void BuildFillStyle(ICoreBrush s)
        {
            switch (s.BrushType)
            {
                case enuBrushType.Custom:
                    break;
                case enuBrushType.Hatch:
                    break;
                case enuBrushType.LinearGradient:
                    Append(GetLinearDefinition(s));
                    Append("v_ctx.fillStyle=grd;");
                    break;
                case enuBrushType.PathGradient:
                    break;
                case enuBrushType.Solid:
                    this.SetFillColor(s.Colors[0]);
                    this.SetAlpha(s.Colors[0].A);                    
                    break;
                case enuBrushType.Texture:
                    break;
                default:
                    break;
            }
        }
        public void Visit(RectangleElement rc)
        {
           Rectanglei v_rc = Rectanglei.Round (rc.Bounds);
           Append(string.Format("v_ctx.save();"));
           this.BindMatrix(rc.GetMatrix());
           this.BindGraphicsProperty(rc);
           RenderShadow(rc);
         
           if (this.BindFillBrush(rc))
           {
               Append(string.Format("v_ctx.fillRect({0},{1}, {2}, {3});",
                  v_rc.X,
                  v_rc.Y,
                  v_rc.Width,
                  v_rc.Height));
           }
           if (this.BindStrokeBrush(rc))
           {
               Append(string.Format("v_ctx.strokeRect({0},{1}, {2}, {3});",
      v_rc.X,
      v_rc.Y,
      v_rc.Width,
      v_rc.Height));
           }
           Append(string.Format("v_ctx.restore();"));
        }

        public void Visit(RoundRectangleElement rc) {
            Rectanglei v_rc = Rectanglei.Round(rc.Bounds);
            Append(string.Format("v_ctx.save();"));
            this.BindMatrix(rc.GetMatrix());
            this.BindGraphicsProperty(rc);
            RenderShadow(rc);

            float vtl_dx = Math.Max(rc.TopLeft.X * 2, 0.1f);
            float vtl_dy = Math.Max(rc.TopLeft.Y * 2, 0.1f);
            float vtr_dx = Math.Max(rc.TopRight.X * 2, 0.1f);
            float vtr_dy = Math.Max(rc.TopRight.Y * 2, 0.1f);
            float vbr_dx = Math.Max(rc.BottomRight.X * 2, 0.1f);
            float vbr_dy = Math.Max(rc.BottomRight.Y * 2, 0.1f);
            float vbl_dx = Math.Max(rc.BottomLeft.X * 2, 0.1f);
            float vbl_dy = Math.Max(rc.BottomLeft.Y * 2, 0.1f);


            if (this.BindFillBrush(rc))
            {
                Append(string.Format("igk.html.canva.fillRoundRecGlobal(v_ctx,{0},{1});",
                  string.Format("{0},{1},{2},{3}",
                 v_rc.X,
                 v_rc.Y,
                 v_rc.Width,
                 v_rc.Height),
                 string.Format("{0},{1},{2},{3}",
                 rc.TopLeft .X+ " , "+rc.TopLeft .Y,
                 v_rc.Y,
                 v_rc.Width,
                 v_rc.Height))

                 );

                switch (rc.RoundStyle )
                {
                  
                    //case enuRoundStyle.Inner:
                    //    break;
                    //case enuRoundStyle.Flat:
                    //    break;
                    //case enuRoundStyle.Plus:
                    //    break;
                    //case enuRoundStyle.Outer:
                    //    break;
                    case enuRoundStyle.Standard:
                    default:
            //             pSegment.AddArc(new Rectanglef(v_rect.X, v_rect.Y, vtl_dx, vtl_dy), 180.0f, 90.0f);
            //pSegment.AddArc(new Rectanglef(
            //    v_rect.X + v_rect.Width - vtr_dx, v_rect.Y,
            //    vtr_dx, vtr_dy), -90.0f, 90.0f);
            //pSegment.AddArc(new Rectanglef(
            //    v_rect.X + v_rect.Width - vbr_dx, v_rect.Y + v_rect.Height - vbr_dy,
            //    vbr_dx, vbr_dy), 0.0f, 90.0f);
            //pSegment.AddArc(
            //    new Rectanglef(
            //        v_rect.X, v_rect.Y + v_rect.Height - vbl_dy,
            //        vbl_dx, vbl_dy), 90.0f, 90.0f);
            //pSegment.CloseFigure();
            //path.AddSegment(pSegment);

                        Append(string.Format("v_ctx.arc({0}, {1}, {2},0, 2*Math.PI, true);",
               vtl_dx,
               vtl_dy,
              0.0)); 
                        break;
                }
              
            }
            if (this.BindStrokeBrush(rc))
            {
                Append(string.Format("v_ctx.strokeRect({0},{1}, {2}, {3});",
       v_rc.X,
       v_rc.Y,
       v_rc.Width,
       v_rc.Height));
            }
            Append(string.Format("v_ctx.restore();"));
        }
        
        
        private void RenderShadow(Core2DDrawingLayeredElement rc)
        {
            if (rc.CanRenderShadow && rc.AllowShadow)
            {
                Append(string.Format("v_ctx.shadowColor =\"{0}\";",
                    rc.ShadowProperty.Brush.Colors[0].ToString(true)));
                Append(string.Format("v_ctx.shadowOffsetX={0};", rc.ShadowProperty.Offset.X));
                Append(string.Format("v_ctx.shadowOffsetY={0};", rc.ShadowProperty.Offset.Y));
                if (rc.ShadowProperty.Blur)
                {
                    Append(string.Format("v_ctx.shadowBlur ={0};", rc.ShadowProperty.BlurRadius));
                }
            }
        }
        private void BindMatrix(Matrix matrix)
        {
            if ((matrix == null)||(matrix.IsIdentity ))
                return;
            var v_Tab = matrix.Elements;
            Append(string.Format("v_ctx.setTransform({0},{1},", v_Tab[0], v_Tab[1]));
            Append(string.Format("{0},{1},", v_Tab[4], v_Tab[5]));
            Append(string.Format("{0},{1});", matrix.OffsetX, matrix.OffsetY));
        }
        private void BindGraphicsProperty(ICore2DDrawingGraphicsProperty obj)
        {
            this.SetCompositingMode(obj.CompositingMode);
            switch (obj.SmoothingMode)
            {
                case enuSmoothingMode.AntiAliazed:
                    break;
                case enuSmoothingMode.None:
                    break;
                default:
                    break;
            }
        }

        private void SetCompositingMode(enuCompositingMode v)
        {
            if (v != this.m_compositingMode)
            {
                this.m_compositingMode = v;
                switch (v)
                {
                    case enuCompositingMode.Copy:
                        Append(string.Format("v_ctx.globalCompositeOperation = \"source-copy\";"));
                        break;
                    case enuCompositingMode.Over:
                        Append(string.Format("v_ctx.globalCompositeOperation = \"source-over\";"));
                        break;
                    default:
                        break;
                }
            }
        }
        
        public void Visit(LineElement ln)
        {
            Append("/*render line element : " + ln.Id + "*/");
            Append(string.Format("v_ctx.save();" ));
            this.BindMatrix(ln.GetMatrix());
            this.BindGraphicsProperty(ln);
            if (this.BindStrokeBrush(ln))
            {
      
                Append(string.Format("v_ctx.beginPath();") );
                Append(string.Format("v_ctx.moveTo({0},{1});",
                    ln.StartPoint.X,
                    ln.StartPoint.Y));
                Append(string.Format("v_ctx.moveTo({0},{1});",
               ln.EndPoint.X,
               ln.EndPoint.Y) );
                Append(string.Format("v_ctx.stroke();") );
                Append(string.Format("v_ctx.closePath();") );
            }
            Append(string.Format("v_ctx.restore();") );
        }
        public void Visit(TextElement text)
        {
            this.Visit(text as Core2DDrawingLayeredElement);
        }


        public void Visit(CircleElement circle)
        {
            this.saveState();
            this.BindMatrix(circle.GetMatrix());
            Append(string.Format("v_ctx.beginPath();"));
            for (var i = 0; i < circle.Radius.Length; i++)
            {
                if (i > 0)
                { 
                    Append(string.Format("v_ctx.moveTo({0}, {1});",
                            circle.Center.X + circle.Radius[i] ,
                            circle.Center.Y
                        ));
                }
                Append(string.Format("v_ctx.arc({0}, {1}, {2},0, 2*Math.PI, true);",
                    circle.Center.X,
                    circle.Center.Y,
                    circle.Radius[i]));           
            }
            Append(string.Format("v_ctx.closePath();"));
            this.renderBrush(circle, circle.FillMode);
            this.restore();
        }
        private void saveState()
        {
            Append(string.Format("v_ctx.save();"));
        }
        private void restore()
        {
            Append(string.Format("v_ctx.restore();"));
        }
        private void renderBrush(Core2DDrawingLayeredElement item, enuFillMode mode )
        {
            string options = "";
            if (this.BindFillBrush(item))
            {
                if (mode == enuFillMode.Alternate)
                {
                    options = "\"evenodd\"";
                }
                Append(string.Format("v_ctx.fill({0});", options));
            }
            if (this.BindStrokeBrush(item))
            {
                Append(string.Format("v_ctx.stroke();"));
            }
        }
        public void Visit(Core2DDrawingLayeredElement item)
        {
            if (item == null)
                return;
            CoreGraphicsPath p = item.GetPath();
            Vector2f[] v_pts = null;
            byte[] v_types = null;

            p.GetAllDefinition(out v_pts, out v_types);

            if ((v_pts == null) || (v_pts .Length == 0))
                return ;
            Append(string.Format("v_ctx.save();"));
            //this.bindMatrix(item.GetMatrix());
            this.BindGraphicsProperty(item);
            //string v_mode = "M";
            StringBuilder sb = m_sb;
            Append("/*path items #"+item.Id +"*/");
            Append(string.Format("v_ctx.beginPath();"));
            List<Vector2f> v_bezierPoint = new List<Vector2f>();
            //bool v_newfig = false;
            for (int i = 0; i < v_pts.Length; i++)
            {
                switch ((enuGdiGraphicPathType)v_types[i])
                {
                    case enuGdiGraphicPathType.StartFigure:
                        Append(string.Format("v_ctx.moveTo({0},{1});",
               v_pts[i].X,
               v_pts[i].Y));
                        break;
                    case enuGdiGraphicPathType.ControlPoint:
                        if (v_bezierPoint.Count == 3)
                        {
                            Append(string.Format("v_ctx.bezierCurveTo({0});",
                                string.Join(",", v_bezierPoint.ToArray()).Replace (";",",")));
                            v_bezierPoint.Clear();
                        }
                        v_bezierPoint.Add(v_pts[i]);
               //         Append(string.Format("v_ctx.bezierCurveTo({0},{1});",
               //v_pts[i].X,
               //v_pts[i].Y));
                        break;
                    case enuGdiGraphicPathType.LinePoint:
                        if (v_bezierPoint.Count > 0)
                        {
                            switch (v_bezierPoint.Count)                            
                            { 
                                case 3:
                                    Append(string.Format("v_ctx.bezierCurveTo({0});",
                                    string.Join(",", v_bezierPoint.ToArray()).Replace(";", ",")));
                                v_bezierPoint.Clear();
                                    break;
                                default :
                                    break;
                            }
                        }
                      Append(string.Format("v_ctx.lineTo({0},{1});",
               v_pts[i].X,
               v_pts[i].Y));
                        break;
                    case enuGdiGraphicPathType.Marker | enuGdiGraphicPathType.ControlPoint:
                        //sb.Append(string.Join(",", v_pts[i].X, v_pts[i].Y) + " ");
                        //  sb.Append(" z ");
                        break;
                    case enuGdiGraphicPathType.Marker:
                        break;
                    case enuGdiGraphicPathType.Mask:
                        break;
                    case enuGdiGraphicPathType.EndPoint:
                        break;
                    default:
                        if (((enuGdiGraphicPathType)v_types[i] & enuGdiGraphicPathType.EndPoint)
                            == enuGdiGraphicPathType.EndPoint)
                        {
                            int b = v_types[i];
                            int sdb = 0x80;
                            var s = b - sdb;
                            switch(v_bezierPoint.Count)
                            {
                                case 2:
                                v_bezierPoint.Add(v_pts[i]);
                                Append(string.Format("v_ctx.bezierCurveTo({0});",
                                    string.Join(",", v_bezierPoint.ToArray()).Replace(";", ",")));
                                v_bezierPoint.Clear();
                                    break;
                                case 0 :
                                    Append(string.Format("v_ctx.lineTo({0},{1});",
                                      v_pts[i].X,
                                      v_pts[i].Y));
                                    break;
                                case 3:
                                    Append(string.Format("v_ctx.bezierCurveTo({0});",
                               string.Join(",", v_bezierPoint.ToArray()).Replace(";", ",")));
                                    v_bezierPoint.Clear();
                                    if ((s & 1) == 1)
                                    {
                                        Append(string.Format("v_ctx.lineTo({0},{1});",
                                          v_pts[i].X,
                                          v_pts[i].Y));
                                    }
                                    break;
                                default :
                                    break;
                            }
                        //}
                        //if (i < (v_pts.Length - 1))
                        //{
                            //start new figure
                            Append(string.Format("v_ctx.closePath();"));               
                        }
                        break;
                }

            }

            if (v_bezierPoint.Count == 3)
            {//render bezier point count
                Append(string.Format("v_ctx.bezierCurveTo({0});",
                    string.Join(",", v_bezierPoint.ToArray()).Replace(";", ",")));
                v_bezierPoint.Clear();
            }
            //Append(string.Format("v_ctx.closePath();"));
            this.renderBrush(item, p.FillMode);
          
            Append(string.Format("v_ctx.restore();"));
        }

      
        private string imageResDefinition() {
            StringBuilder sb = new StringBuilder();
            foreach (string data in this.m_images)
            {
                sb.Append(string.Format("img[\"src\"] = \"{0}\";", data));    
            }
            return sb.ToString();
        }
        public void Visit(ImageElement image)
        {
            string data = image.ToBase64Data();
            this.m_imageRes = true;
            int index  = this.m_images.Count;
            this.m_images.Add(data);


            Rectanglei bound = new Rectanglei(0, 0, image.Width, image.Height);// Rectanglei.Round(image.GetBound());

            StringBuilder sb = new StringBuilder();
            sb.Append(string.Format("v_ctx.drawImage(v_imgs["+index+"], {0},{1},{2},{3});", bound.X, bound.Y, bound.Width,bound.Height ));


            Append(string.Format("v_ctx.save();"));
            this.BindMatrix(image.GetMatrix());
            this.BindGraphicsProperty(image);
            Append(sb.ToString());
            this.restore();
        }

       
        
    }
}
