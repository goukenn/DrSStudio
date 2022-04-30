

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: WebPDFEncoderVisitor.cs
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
using IGK.DrSStudio.WinUI;
using IGK.DrSStudio.Drawing2D;
using IGK.DrSStudio.Drawing2D.WinUI;
﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.DrSStudio.WebAddIn.IGKWebAddIn.HtmlPdf
{
    using IGK.ICore;

    class WebPDFEncoderVisitor : CoreEncoderVisitor
    {
        private StringBuilder m_sb;
        private float m_alpha;
        private Colorf m_fillColor;
        private Colorf m_strokeColor;
        private float m_lineWidth;
        private enuCompositingMode m_compositingMode;
        private int m_pageCount;
        private string m_currentUnit;
        public override void Flush()
        {
        }
        public void SetAlpha(float alpha)
        {
            if (alpha!=1.0f) {
                this.m_alpha = alpha;
                this.m_sb.AppendLine(string.Format("$pdf->globalAlpha={0};", alpha));
            }
        }
        public void SetFillColor(Colorf cl)
        {
            if (this.m_fillColor != cl)
            {
                this.m_fillColor = cl;
                this.m_sb.AppendLine(string.Format("$pdf->setfColorf({0},{1},{2});",
               cl.R, cl.G, cl.B));
            }
        }
        public WebPDFEncoderVisitor()
            : base()
        {
            m_sb = new StringBuilder();
            this.m_currentUnit = "mm";
            m_sb.AppendLine("<?php ");
        }
        internal void SaveTo(string filename)
        {
            this.m_sb.Append("?>");
            File.WriteAllText(filename, this.m_sb.ToString());
            this.Clear();
        }
        public void Clear()
        {
            m_sb.Clear();
        }
        public void Visit(ICore2DDrawingDocument document)
        {

            string mode = "L"; //"P";
            if (m_pageCount > 0)
            {
                m_sb.AppendLine(string.Format("$pdf->addCPage(\"" + mode + "\",array({0},{1}));",
                ((CoreUnit)document.Width).GetValue(enuUnitType.mm),
                ((CoreUnit)document.Height).GetValue(enuUnitType.mm)));
            }
            else {
                m_sb.AppendLine(string.Format("$pdf = new IGKPDF(\""+mode+"\",\"mm\",array({0},{1}));",
                ((CoreUnit)document.Width).GetValue(enuUnitType.mm),
                ((CoreUnit)document.Height).GetValue(enuUnitType.mm)));
            }

           // m_sb.AppendLine("$igk(canva).setCss({width:\""+2*document.Width+"px\", height:\""+4*document.Height +"px\"})");
            foreach (ICore2DDrawingLayer layer in document.Layers)
            {
                this.Visit(layer);
            }
            m_pageCount++;
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
            this.m_sb.AppendLine("//no rendering " + element.Id);
        }
     

        private bool bindStrokeBrush(ICoreBrushOwner owner)
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

        private bool bindFillBrush(ICoreBrushOwner owner)
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
                    break;
                case enuBrushType.PathGradient:
                    break;
                case enuBrushType.Solid:
                    this.SetStrokeColor(p.Colors[0]);
                    this.m_sb.AppendLine(string.Format("$pdf->strokeStyle = \"{0}\";",
                      p.Colors[0].ToString(true)));
                    this.SetAlpha(p.Colors[0].A);
                    break;
                case enuBrushType.Texture:
                    break;
                default:
                    break;
            }
            this.SetLineWidth(p.Width);            
            //init data style
            this.m_sb.AppendLine(string.Format("$pdf->lineCap = \"butt\";",""));
            this.m_sb.AppendLine(string.Format("$pdf->lineJoin = \"miter\";",""));

        }

        private void SetLineWidth(float p)
        {
            if (p!=1.0f)
            {
                this.m_lineWidth = p;
                this.m_sb.AppendLine(string.Format("$pdf->lineWidth={0};", p));
            }
        }

        private void SetStrokeColor(Colorf cl)
        {
            if (this.m_strokeColor != cl)
            {
                this.m_strokeColor = cl;

                this.m_sb.AppendLine(string.Format("$pdf->setdColorf({0},{1},{2});",
                    cl.R,cl.G, cl.B ));
            }
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
        private  float GetValue(float v)
        {
            switch (this.m_currentUnit)
            { 
                case "mm":
                    return v.ToMm();
                    
            }
            return v;
        }
        public void Visit(RectangleElement rc)
        {
           Rectanglef v_rc = rc.Bounds;
           this.m_sb.AppendLine(string.Format("$pdf->save();"));
           this.bindMatrix(rc.GetMatrix());
           this.bindGraphicsProperty(rc);
           renderShadow(rc);
        
           if (this.bindFillBrush(rc))
           {
               this.m_sb.AppendLine(string.Format("$pdf->fillRect({0},{1}, {2}, {3});",
                  v_rc.X.ToMm(),
                  v_rc.Y.ToMm(),
                  v_rc.Width.ToMm(),
                  v_rc.Height.ToMm()));
           }
           if (this.bindStrokeBrush(rc))
           {
               this.m_sb.AppendLine(string.Format("$pdf->drawRect({0},{1}, {2}, {3});",  GetValue(v_rc.X),      GetValue(v_rc.Y),     GetValue( v_rc.Width),      GetValue(v_rc.Height)));
           }
           this.m_sb.AppendLine(string.Format("$pdf->restore();"));
        }

        private void renderShadow(Core2DDrawingLayeredElement rc)
        {
            if (rc.CanRenderShadow && rc.AllowShadow)
            {
                this.m_sb.AppendLine(string.Format("$pdf->shadowColor =\"{0}\";",
                    rc.ShadowProperty.Brush.Colors[0].ToString(true)));
                this.m_sb.AppendLine(string.Format("$pdf->shadowOffsetX={0};", rc.ShadowProperty.Offset.X));
                this.m_sb.AppendLine(string.Format("$pdf->shadowOffsetY={0};", rc.ShadowProperty.Offset.Y));
                if (rc.ShadowProperty.Blur)
                {
                    this.m_sb.AppendLine(string.Format("$pdf->shadowBlur ={0};", rc.ShadowProperty.BlurRadius));
                }
            }
        }

        private void bindMatrix(Matrix matrix)
        {
            if ((matrix == null)||(matrix.IsIdentity ))
                return;
            var v_Tab = matrix.Elements;
            this.m_sb.Append(string.Format("$pdf->setTransform({0},{1},", v_Tab[0], v_Tab[1]));
            this.m_sb.Append(string.Format("{0},{1},", v_Tab[4], v_Tab[5]));
            this.m_sb.AppendLine(string.Format("{0},{1});",
                GetValue(matrix.OffsetX),
                GetValue(matrix.OffsetY)));
        }
        private void bindGraphicsProperty(ICore2DDrawingGraphicsProperty obj)
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
            if (v!= enuCompositingMode.Over)
            {
                this.m_compositingMode = v;
                switch (v)
                {
                    case enuCompositingMode.Copy:
                        this.m_sb.AppendLine(string.Format("$pdf->globalCompositeOperation = \"source-copy\";"));
                        break;
                    case enuCompositingMode.Over:
                        break;
                    default:
                        break;
                }
            }
        }

        public void Visit(LineElement ln)
        {
            this.m_sb.AppendLine("/*render line element : " + ln.Id + "*/");
            this.m_sb.AppendLine(string.Format("$pdf->save();"));
            this.bindMatrix(ln.GetMatrix());
            this.bindGraphicsProperty(ln);
            if (this.bindStrokeBrush(ln))
            {
      
                this.m_sb.AppendLine(string.Format("$pdf->beginPath();"));
                this.m_sb.AppendLine(string.Format("$pdf->moveTo({0},{1});",
                    ln.StartPoint.X,
                    ln.StartPoint.Y));
                this.m_sb.AppendLine(string.Format("$pdf->moveTo({0},{1});",
               ln.EndPoint.X,
               ln.EndPoint.Y));
                this.m_sb.AppendLine(string.Format("$pdf->stroke();"));
                this.m_sb.AppendLine(string.Format("$pdf->closePath();"));
            }
            this.m_sb.AppendLine(string.Format("$pdf->restore();"));
        }
        public void Visit(TextElement text)
        {
            this.Visit(text as Core2DDrawingLayeredElement);
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
            this.m_sb.AppendLine(string.Format("$pdf->save();"));
            //this.bindMatrix(item.GetMatrix());
            this.bindGraphicsProperty(item);
            //string v_mode = "M";
            StringBuilder sb = m_sb;
            this.m_sb.AppendLine(string.Format("$pdf->beginPath();"));
            List<Vector2f> v_bezierPoint = new List<Vector2f>();
            //bool v_newfig = false;
            for (int i = 0; i < v_pts.Length; i++)
            {
                switch ((enuGdiGraphicPathType)v_types[i])
                {
                    case enuGdiGraphicPathType.StartFigure:
                        this.m_sb.AppendLine(string.Format("$pdf->moveTo({0},{1});",
               v_pts[i].X,
               v_pts[i].Y));
                        break;
                    case enuGdiGraphicPathType.ControlPoint:
                        if (v_bezierPoint.Count == 3)
                        {
                            this.m_sb.AppendLine(string.Format("$pdf->bezierCurveTo({0});",
                                string.Join(",", v_bezierPoint.ToArray()).Replace (";",",")));
                            v_bezierPoint.Clear();
                        }
                        v_bezierPoint.Add(v_pts[i]);
               //         this.m_sb.AppendLine(string.Format("$pdf->bezierCurveTo({0},{1});",
               //v_pts[i].X,
               //v_pts[i].Y));
                        break;
                    case enuGdiGraphicPathType.LinePoint:
                        if (v_bezierPoint.Count > 0)
                        {
                            switch (v_bezierPoint.Count)                            
                            { 
                                case 3:
                                    this.m_sb.AppendLine(string.Format("$pdf->bezierCurveTo({0});",
                                    string.Join(",", v_bezierPoint.ToArray()).Replace(";", ",")));
                                v_bezierPoint.Clear();
                                    break;
                                default :
                                    break;
                            }
                        }
                      this.m_sb.AppendLine(string.Format("$pdf->lineTo({0},{1});",
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
                            switch(v_bezierPoint.Count)
                            {
                                case 2:
                                v_bezierPoint.Add(v_pts[i]);
                                this.m_sb.AppendLine(string.Format("$pdf->bezierCurveTo({0});",
                                    string.Join(",", v_bezierPoint.ToArray()).Replace(";", ",")));
                                v_bezierPoint.Clear();
                                    break;
                                case 0 :
                                    this.m_sb.AppendLine(string.Format("$pdf->lineTo({0},{1});",
                                      v_pts[i].X,
                                      v_pts[i].Y));
                                    break;
                                default :
                                    break;
                            }
                        }
                        if (i < (v_pts.Length - 1))
                        {//start new figure
                            this.m_sb.AppendLine(string.Format("$pdf->closePath();"));
               //             this.m_sb.AppendLine(string.Format("$pdf->moveTo({0},{1});",
               //v_pts[i].X,
               //v_pts[i].Y));
                        }
                        break;
                }
            }


            this.m_sb.AppendLine(string.Format("$pdf->closePath();"));
            if (this.bindFillBrush(item))
            {
                this.m_sb.AppendLine(string.Format("$pdf->fill();"));
            }
            if (this.bindStrokeBrush(item))
            {
                this.m_sb.AppendLine(string.Format("$pdf->stroke();"));
            }
            this.m_sb.AppendLine(string.Format("$pdf->restore();"));
        }
        public void Visit(ImageElement image)
        { 
        }
    }
}
