

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: IGKOGL2DDrawingDeviceVisitor.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/

using IGK.ICore;
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
﻿using IGK.ICore.GraphicModels;
using IGK.OGLGame.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using IGK.DrSStudio.Drawing3D.OpenGL.AddInDrawing2D;

namespace IGK.DrSStudio.Drawing2D.OpenGL
{
    using IGK.GLLib;
    using System.Runtime.InteropServices;
    using System.Drawing;
    using IGK.ICore.WinCore;
    
    /// <summary>
    /// used as gl drawing 2D visitor
    /// </summary>
    public class IGKOGL2DDrawingDeviceVisitor : ICoreGraphics, IIGKOGL2DDrawingDeviceVisitor
    {
        private OGLGraphicsDevice m_device;
        private enuSmoothingMode m_smoothingMode;
        private enuCompositingMode m_compositingMode;
        private ICore2DDrawingVisitable m_currentVisitableElement;
        private RectangleElement m_resourceOwner = new RectangleElement();
        public OGLGraphicsDevice Device { get { return this.m_device; } }
        private Matrix m_matrix = new Matrix();

        internal static IGKOGL2DDrawingDeviceVisitor Create(OGLGraphicsDevice device)
        {
            IGKOGL2DDrawingDeviceVisitor dev = new IGKOGL2DDrawingDeviceVisitor();
            dev.m_device = device;
            return dev;
            
        }

        public bool Accept(ICoreWorkingObject obj)
        {
            if (obj !=null)
                return true;
            return false;
        }

        public void Clear(Colorf color)
        {
            this.m_device.Clear(color);
        }

        public Rectanglef ClipBounds
        {
            get { return Rectanglef.Empty; }
        }

        public enuCompositingMode CompositingMode
        {
            get
            {
                return m_compositingMode;
            }
            set
            {
                this.m_compositingMode = value;
            }
        }

       

        public void Draw(ICore2DDrawingObject obj)
        {
            throw new NotImplementedException();
        }


        public void DrawString(string text, ICoreFont font, Colorf color, float x, float y)
        {
            if (string.IsNullOrEmpty(text) || (font == null))
                return;

            SpriteFont ft = IGKOGL2DDrawingExtensions.ToSpriteFont(font, this.m_device); 
                this.m_device.DrawString(ft, text, new Vector2f(x, y), color);
            
        }

        public void DrawEllipse(ICorePen pen, float x, float y, float width, float height)
        {
            throw new NotImplementedException();
        }

        public void DrawEllipse(Colorf color, float x, float y, float width, float height)
        {
            throw new NotImplementedException();
        }

        public void DrawLine(Colorf color, Vector2f start, Vector2f end)
        {
            this.Device.DrawLine(
                color, start, end);
        }

        public void DrawLine(ICorePen color, Vector2f start, Vector2f end)
        {
            Colorf cl = color.Colors[0];
            this.Device.DrawLine(
                    cl, start, end);
        }

        public void DrawLine(ICorePen color, float x1, float y1, float x2, float y2)
        {
            DrawLine(color.Colors[0], color.Width, color.DashStyle, x1, y1, x2, y2);
        }

        public void DrawLine(Colorf color, float penwidth, enuDashStyle style, float x1, float y1, float x2, float y2)
        {
            if (style != enuDashStyle.Solid)
            {   
                this.Device.RenderState.SetLineStipple (1, 0xAAAA);
                this.Device.Capabilities.LineStipple = true;
            }
            this.Device.DrawLine(
          color, new Vector2f(x1, y1), new Vector2f(x2, y2));
            this.Device.Capabilities.LineStipple = false;
        }

        public void DrawLine(Colorf color, float x1, float y1, float x2, float y2)
        {
            this.DrawLine(color, 1.0F, enuDashStyle.Solid, x1, y1, x2, y2);
        }

        public void DrawPath(ICorePen pen, CoreGraphicsPath p)
        {
            throw new NotImplementedException();
        }

        public void DrawPath(Colorf colorf, CoreGraphicsPath p)
        {
            Vector2f[] tab = null;
            byte[] types = null;
            p.GetAllDefinition(out tab, out types);
            if (tab.Length == 0)
            {
                return;
            }
            IGKOGL2DPathRenderer r = new IGKOGL2DPathRenderer(tab, types);
            this.Device.SetColor(colorf);
            r.Render(this);

        }

        public void DrawRectangle(Colorf color, float penwidth, enuDashStyle style, float x, float y, float width, float height)
        {
            var g = this.Device.RenderState;
            g.LineWidth = penwidth;
            g.SetPolygonMode(PolygonCullFace.FrontAndBack, PolygonFaceMode.Line);
            if (style != enuDashStyle.Solid)
            {
                g.LineStippleFactor = 1;
                g.LineStipplePattern = 0xF0F0;
                this.Device.Capabilities.LineStipple = true;
            }
            this.Device.DrawRectangle(color, x, y, width, height);
            this.Device.Capabilities.LineStipple = false;
            g.SetPolygonMode(PolygonCullFace.FrontAndBack, PolygonFaceMode.Fill );
        }

        public void DrawRectangle(Colorf color, float x, float y, float width, float height)
        {
            this.DrawRectangle(color, 1.0f, enuDashStyle.Solid, x, y, width, height);
        }

        public void DrawRectangle(ICorePen pen, float x, float y, float width, float height)
        {
            if (pen.DashStyle != enuDashStyle.Solid)
            {
                this.Device.Capabilities.LineStipple = true;
                this.Device.RenderState.LineStipplePattern = 0xF0F0;
                this.DrawRectangle(pen.Colors[0], 1.0f, enuDashStyle.Solid, x, y, width, height);
                this.Device.Capabilities.LineStipple = false;
            }
            else
            {
                this.DrawRectangle(pen.Colors[0], 1.0f, enuDashStyle.Solid, x, y, width, height);
            }

        }

        public void ExcludeClip(Rectanglef rectangle)
        {
            //no exculing clip
        }

        public void FillEllipse(Colorf color, float x, float y, float width, float height)
        {
            this.Device.Begin(enuGraphicsPrimitives.LineLoop);
            float rx = width / 2.0f;
            float ry = height / 2.0f;
            float cx = x + rx;
            float cy = y + ry;
            
            for (int i = 0; i < 360; i++)
            {
                this.Device.SetVertex(new Vector2f(
                    cx + rx * Math.Cos(i * CoreMathOperation.ConvDgToRadian ),
                    cy + ry * Math.Sin(i * CoreMathOperation.ConvDgToRadian)
                    ));
            }
            this.Device.End();
        }

        public void FillEllipse(ICoreBrush brush, float x, float y, float width, float height)
        {
            throw new NotImplementedException();
        }

        public void FillPath(ICoreBrush br, CoreGraphicsPath p)
        {
            throw new NotImplementedException();
        }

        public void FillPath(Colorf colorf, CoreGraphicsPath p)
        {
            throw new NotImplementedException();
        }

        public void FillRectangle(Colorf color, float x, float y, float width, float height)
        {
            
            this.Device.RenderState.SetPolygonMode(PolygonCullFace.FrontAndBack, PolygonFaceMode.Fill);
            this.Device.DrawRectangle(color, x, y, width, height);
        }

        public void FillRectangle(Colorf color, Rectanglef rectangle)
        {
            this.FillRectangle(color, rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height);
        }

        public void FillRectangle(ICoreBrush brush, float x, float y, float width, float height)
        {
            
        }

        public void Flush()
        {
            this.m_device.Flush();
        }

        public enuInterpolationMode InterpolationMode
        {
            get
            {
                return enuInterpolationMode.Hight;
            }
            set
            {
                
            }
        }

        public void IntersectClip(Rectanglef rectangle)
        {
            
        }

        public void MultiplyTransform(Matrix matrix, enuMatrixOrder order)
        {
            this.m_matrix.Multiply(matrix, order);
            this.Device.Projection.LoadMatrix(m_matrix);
        }

        public enuPixelOffset PixelOffsetMode
        {
            get
            {
                return enuPixelOffset.Half;
            }
            set
            {
            }
        }

        public void Release()
        {
            //not supported what...
            this.m_device.FreeResources();
        }

        public void ResetClip()
        {
            //not yet supported
        }


        public object Save()
        {
            object obj = new object();
            this.Device.PushAttrib(enuAttribBit.All);
            this.Device.Projection.PushMatrix();
            return obj;
        }
        public void Restore(object state)
        {
            this.Device.PopAttrib();
            this.Device.Projection.PopMatrix();
            Matrix p = this.Device.Projection.GetModelViewMatrix();

            if (!p.Equals (this.m_matrix))
            {
                for (int i = 0; i < this.m_matrix.Elements.Length; i++)
                {
                    this.m_matrix.Elements[i] = p.Elements[i];
                }
            }
            p.Dispose();
        }

        

       

        public void ScaleTransform(float ex, float ey, enuMatrixOrder order)
        {
            this.m_matrix.Scale(ex, ey, order);
            this.Device.Projection.LoadMatrix(this.m_matrix);
            //Matrix c = this.Device.Projection.GetModelViewMatrix();
            //this.Device.Projection.Scale(ex, ey, 1.0f);
            //c = this.Device.Projection.GetModelViewMatrix();
        }
        public void TranslateTransform(float dx, float dy, enuMatrixOrder order)
        {
            this.m_matrix.Translate(dx, dy, order);
            //this.Device.Projection.Translate(dx, dy, 0.0f);
            this.Device.Projection.LoadMatrix(this.m_matrix);
         //   Matrix c = this.Device.Projection.GetModelViewMatrix();
            
        }
        public void RotateTransform(float angle, enuMatrixOrder order)
        {
            this.m_matrix.Rotate(angle, Vector2f.Zero, order);
            this.Device.Projection.LoadMatrix(this.m_matrix);
        }
        public void ResetTransform()
        {
            this.m_matrix.Reset();
        }

        public void SetClip(Rectanglef rectangle)
        {
            
        }

        public void SetupGraphicsDevice(ICoreWorkingObject obj)
        {
            ICore2DDrawingGraphicsProperty p = obj as ICore2DDrawingGraphicsProperty;
            this.SetupGraphicsDevice(p);
        }
        private void SetupGraphicsDevice(ICore2DDrawingGraphicsProperty p)
        {
            if (p == null) return;
            if (p.CompositingMode == enuCompositingMode.Over)
            { }
            bool v = (p.SmoothingMode == enuSmoothingMode.AntiAliazed);
            this.m_device.Capabilities.PolygonSmooth = v;
            this.m_device.Capabilities.LineSmooth = v;
            this.m_device.Capabilities.PointSmooth = v;
            
            //hint meaning

        }

        public enuSmoothingMode SmoothingMode
        {
            get
            {
                return this.m_smoothingMode;
            }
            set
            {
                this.m_smoothingMode = value;
            }
        }

        public enuTextRenderingMode TextRenderingMode
        {
            get
            {
                return enuTextRenderingMode.AntiAliazed;
            }
            set
            {
                throw new NotImplementedException();
            }
        }

     

        public void Draw(ICoreWorkingObject d, bool proportional, Rectanglei rectangle, enuFlipMode flipMode)
        {
            MethodInfo.GetCurrentMethod().Visit(this, d, proportional, rectangle, flipMode);
        }
        public virtual void Visit(ICore2DDrawingDocument document, bool proportional, Rectanglei rectangle, enuFlipMode flipMode)
        {
            object  v_state = this.Save();
            //setup graphics
           // Graphics graphics = this.m_device;
           // graphics.TextContrast = document.TextContrast;
            //graphics.TextRenderingHint = (TextRenderingHint)document.TextRenderingMode;
            //graphics.PixelOffsetMode = (PixelOffsetMode)document.PixelOffset;
            //graphics.InterpolationMode = (InterpolationMode)document.InterpolationMode;
            this.SetupGraphicsDevice(document);
            Rectanglei v_rc = new Rectanglei(0, 0, document.Width, document.Height);

            //-----------------------------------------------------------------------------------
            //setup matrix
            //-----------------------------------------------------------------------------------

            Matrix m = new Matrix();

            float fx = rectangle.Width / (float)document.Width;
            float fy = rectangle.Height / (float)document.Height;


            switch (flipMode)
            {
                case enuFlipMode.None:
                    break;
                case enuFlipMode.FlipVertical:
                    fy *= -1;
                    break;
                case enuFlipMode.FlipHorizontal:
                    fx *= -1;
                    break;
                default:
                    break;
            }


            if (proportional == false)
            {
                m.Scale(fx, fy, enuMatrixOrder.Append);
                m.Translate(rectangle.X, rectangle.Y, enuMatrixOrder.Append);


                switch (flipMode)
                {
                    case enuFlipMode.None:
                        break;
                    case enuFlipMode.FlipVertical:
                        m.Translate(0, rectangle.Height, enuMatrixOrder.Append);
                        break;
                    case enuFlipMode.FlipHorizontal:
                        m.Translate(rectangle.Width, 0, enuMatrixOrder.Append);
                        break;
                    default:
                        break;
                }

            }
            else
            {
                fx = Math.Min(fx, fy);
                fy = fx;
                m.Scale(fx, fy, enuMatrixOrder.Append);
                int posx = (int)(((-document.Width * fx) / 2.0f) + (rectangle.Width / 2.0f));
                int posy = (int)(((-document.Height * fx) / 2.0f) + (rectangle.Height / 2.0f));
                m.Translate(posx + rectangle.X, posy + rectangle.Y, enuMatrixOrder.Append);


                switch (flipMode)
                {
                    case enuFlipMode.None:
                        break;
                    case enuFlipMode.FlipVertical:
                        m.Translate(0, rectangle.Height, enuMatrixOrder.Append);
                        break;
                    case enuFlipMode.FlipHorizontal:
                        m.Translate(rectangle.Width, 0, enuMatrixOrder.Append);
                        break;
                    default:
                        break;
                }
            }
            Matrix c = this.Device.Projection.GetModelViewMatrix();

            this.Device.Projection.MultMatrix (m, enuMatrixOrder.Append);

            Matrix v = this.Device.Projection.GetModelViewMatrix();

            //if (document.ClipView)
            //{
            //    Region rg = new Region(new Rectangle(0, 0, document.Width, document.Height));
            //    graphics.Clip = rg;
            //}
            switch (document.RenderingMode)
            {
                case enuRenderingMode.Pixel:
                    ////create a offscreen bmp
                    //Bitmap v_img = new Bitmap(v_rc.Width, v_rc.Height);
                    //Graphics v_g = Graphics.FromImage(v_img);
                    //if (document.IsClipped)
                    //{
                    //    //v_g.Clip = GetRion( obj..GetClippedRegion();
                    //}
                    //foreach (ICore2DDrawingLayer layer in document.Layers)
                    //{
                    //    v_g.Clear(Color.Empty);
                    //    if (layer.View)
                    //    {
                    //        //layer.Draw(v_g);
                    //        //graphics.DrawImage(v_img,
                    //        //    v_rc,
                    //        //    0,
                    //        //    0,
                    //        //    v_rc.Width,
                    //        //    v_rc.Height,
                    //        //    GraphicsUnit.Pixel,
                    //        //    GetImageAttributes( layer));
                    //    }
                    //    else
                    //    {
                    //        graphics.DrawImage(v_img,
                    //            0, 0, v_rc.Width, v_rc.Height);
                    //    }
                    //}
                    //v_g.Dispose();
                    //v_img.Dispose();
                    break;
                case enuRenderingMode.Vector:
                default:
                    if (document.IsClipped)
                    {
                       // this.SetClip(document.GetClippedRegion());
                    }
                    RenderBackground(document);
                    foreach (ICore2DDrawingLayer l in document.Layers)
                    {
                        if (l.View && Accept(l))
                        {
                            Visit(l);
                        }
                    }
                    break;
            }
            this.Restore(v_state);
        }

        private void RenderBackground(ICore2DDrawingDocument document)
        {
            if ((document.BackgroundTransparent == false) && (document.FillBrush != null))
            {
                this.Device.DrawRectangle(document.FillBrush.Colors[0],
                    0,0,
                    document.Width, document.Height);
            } 
        }
        /// <summary>
        /// dispose the device
        /// </summary>
        public void Dispose()
        {
            if (this.m_matrix != null)
                this.m_matrix.Dispose();
            if (m_resourceOwner != null)
            {
                this.m_resourceOwner.Dispose();
                this.m_resourceOwner = null;
            }
            this.m_device.Dispose();
        }

        public void Visit(ICoreWorkingObject obj)
        {
            if (obj != null)
            {
                this.Visit(obj, obj.GetType());
            }
        }

        public virtual void Visit(ICoreWorkingObject obj, Type requestType)
        {
            if ((obj is ICore2DDrawingVisitable) && (this.m_currentVisitableElement != obj))
            {
                ICore2DDrawingVisitable c = obj as ICore2DDrawingVisitable;
                this.m_currentVisitableElement = c;
                if (c.Accept(this))
                {
                    c.Visit(this);
                }
                this.m_currentVisitableElement = null;
                return;
            }
            MethodInfo.GetCurrentMethod().Visit(this, new Type[]{requestType}, obj);
        }
        public void Visit(ICore2DDrawingDocument document)
        {
            this.Visit(document, false, new Rectanglei(0, 0, document.Width, document.Height), enuFlipMode.None);

        }
        public virtual void Visit(ICore2DDrawingLayer layer)
        {

            if (!layer.View)
                return;
            object obj = this.Save();
            bool v_isClipped = layer.IsClipped;
            if (v_isClipped)
            {
                // this.SetClip(layer.ClippedElement);
            }
            foreach (ICore2DDrawingLayeredElement item in layer.Elements)
            {
                if (v_isClipped && (item == layer.ClippedElement))
                    continue;
                if (item.View && Accept(item))
                    Visit(item);
                else
                {
                    CoreLog.WriteLine("can't render : " + item);
                }
            }
            if (v_isClipped)
            {
                this.ResetClip();
                if ((layer.ClippedElement.View) && Accept(layer.ClippedElement))
                {
                    Visit(layer.ClippedElement);
                }
            }
            this.Restore(obj);
        }
        public void Visit(RectangleElement rectangle)        
        {
            var p = rectangle.GetPath();
            object obj = this.Save ();
            this.SetupGraphicsDevice(rectangle);
            this.MultiplyTransform(rectangle.GetMatrix(), enuMatrixOrder.Prepend );
            GL.glEnable(GL.GL_TEXTURE_2D);
            Rectanglef v_rc = rectangle.Bounds ;
            Texture2D rd = Texture2D.Create(this.Device, (int)v_rc.Width, (int)v_rc.Height);
            if (rd != null)
            {
                System.Drawing.Bitmap bmp = new System.Drawing.Bitmap((int)v_rc.Width, (int)v_rc.Height);
                using (System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(bmp))
                {
                    g.Clear(System.Drawing.Color.Transparent );
                    g.Flush();
                    var br = WinCoreBrushRegister.GetBrush(rectangle.FillBrush );
                    if (br != null)
                    {
                        g.TranslateTransform(-v_rc.X, -v_rc.Y, global::System.Drawing.Drawing2D.MatrixOrder.Append );
                        g.FillRectangle(br, v_rc);
                        g.TranslateTransform(v_rc.X, v_rc.Y, global::System.Drawing.Drawing2D.MatrixOrder.Append);
                    }
                    
                    g.Flush();
                }
                rd.Bind();
                rd.ReplaceTexture(bmp);
          
            }

           // GL.glEnable(GL.GL_TEXTURE_GEN_S);
           // GL.glEnable(GL.GL_TEXTURE_GEN_T);
            float[] currentCoeff = new float[] { 1.0f, 0.0f, 0.0f, 0.0f };
            uint currentGenMode = GL.GL_OBJECT_LINEAR;// GL_EYE_LINEAR;
            uint currentPlane = 
            //    GL.GL_OBJECT_PLANE 
             GL.GL_EYE_PLANE
            ;
            GL.glTexGeni(GL.GL_S, GL.GL_TEXTURE_GEN_MODE, (int)currentGenMode);
            GL.glTexGeni(GL.GL_T, GL.GL_TEXTURE_GEN_MODE, (int)currentGenMode);
            GL.glTexGenfv(GL.GL_S, currentPlane, currentCoeff );
            GL.glTexGenfv(GL.GL_T, currentPlane, currentCoeff);
    //        this.FillRectangle(
    //rectangle.FillBrush.Colors[0],
    //rectangle.Bounds.X,
    //rectangle.Bounds.Y,
    //rectangle.Bounds.Width,
    //rectangle.Bounds.Height);

            this.Device.RenderState.LineWidth =
                rectangle.StrokeBrush.Width;

            this.Device.SetColor(Colorf.White);
         
            this.Device.Begin(enuGraphicsPrimitives.Quads);
            this.Device.SetVertex(rectangle.Bounds.X,
                rectangle.Bounds.Y);
            this.Device.SetVertex(rectangle.Bounds.X + rectangle.Bounds.Width ,
                rectangle.Bounds.Y );
            this.Device.SetVertex(rectangle.Bounds.X + rectangle.Bounds.Width,
                rectangle.Bounds.Y + rectangle.Bounds.Height);
            this.Device.SetVertex(rectangle.Bounds.X ,
                rectangle.Bounds.Y+ rectangle.Bounds.Height);
            this.Device.End();
            //this.DrawRectangle(
            //    rectangle.StrokeBrush.Colors[0],
            //    rectangle.StrokeBrush.Width ,
            //    rectangle.StrokeBrush.DashStyle,
            //    rectangle.Bounds.X,
            //    rectangle.Bounds.Y,
            //    rectangle.Bounds.Width,
            //    rectangle.Bounds.Height);
            GL.glDisable(GL.GL_TEXTURE_GEN_S);
            GL.glDisable(GL.GL_TEXTURE_GEN_T);
            if (rd != null)
            {
                this.Device.Draw(rd, rectangle.Bounds.Location ,rectangle.Bounds.Size , Colorf.White);
                rd.Dispose();
            }

            

            GL.glDisable(GL.GL_TEXTURE_2D);
            this.Restore (obj);
        }



        public void Visit(WinCoreBitmap bitmap)
        {

            Texture2D v_text = 
                IGKOGL2DDrawingResourcesManager.GetTexture(
                this.m_device,
                this.m_resourceOwner,
                bitmap);
            if (v_text != null)
            {
                object obj = this.Save();
                //this.MultiplyTransform(image.GetMatrix(), enuMatrixOrder.Prepend);
                this.m_device.Draw(v_text, Vector2f.Zero, new Size2f(bitmap.Width, bitmap.Height), Colorf.White);
                this.Restore(obj);
            }
        }
        public void Visit(ImageElement image)
        { 
            WinCoreBitmap bmp = image.Bitmap as WinCoreBitmap;
            if (bmp==null){
                return;
            }

            //render image a bitmap matrix
            //position
            /*float w = image.Width;
            float h = image.Height;
            float ex =  this.m_matrix.Elements[0];
            float ey = -this.m_matrix.Elements[5];
            var g = CoreMathOperation.ApplyMatrix(image.GetBound(), image.GetMatrix());
            
            this.m_device.PushAttrib( enuAttribBit.All);
            this.m_device.SetRasterPos(0, 0 );
            this.m_device.RenderState.ZoomX = ex;
            this.m_device.RenderState.ZoomY = ey;
                m_device.DrawBitmap(bmp.Bitmap);
                this.m_device.PopAttrib();
            */

                Texture2D v_text = IGKOGL2DDrawingResourcesManager.GetTexture(this.m_device, image, bmp);
                if (v_text != null)
                {
                    object obj = this.Save();
                    this.MultiplyTransform(image.GetMatrix(), enuMatrixOrder.Prepend);
                    this.m_device.Draw(v_text, Vector2f.Zero, new Size2f(bmp.Width,bmp.Height), Colorf.White);
                    this.Restore(obj);
                }
        }

        public void Draw(ICoreBitmap inBmp, Rectanglei rectanglei)
        {
            //draw in pixel
            Texture2D v_text = IGKOGL2DDrawingResourcesManager.GetTexture(this.m_device, inBmp);
            if (v_text != null)
            {
                object obj = this.Save();
                this.m_device.Draw(v_text, rectanglei.Location , new Size2f(rectanglei.Width, rectanglei.Width), Colorf.White);
                this.Restore(obj);
            }
        }



        public void DrawString(string text, ICoreFont font, ICoreBrush Brush, Rectanglef bound, enuTextTrimming trimming, bool isMnemocic)
        {
            using (SpriteFont ft = SpriteFont.Create(this.m_device, font.FontName, font.FontSize, font.FontStyle, (int)bound.Width, (int)bound.Height))
            {
                m_device.DrawString(ft, text, bound.Location, Colorf.Black);
            }
            

        }

        public void DrawString(string text, CoreFont font, ICoreBrush Brush, Rectanglef bound)
        {
            
        }

        public void DrawString(string text, ICoreFont font, ICoreBrush coreBrush, Rectanglef rectangle)
        {
            
        }

        public void DrawString(string text, CoreFont font, Colorf color, Rectanglef bounds)
        {

        }

        public void DrawString(string text, ICoreFont coreFont, ICoreBrush coreBrush, float x, float y)
        {
            using (SpriteFont ft = SpriteFont.Create(this.m_device, coreFont.FontName,
                coreFont.FontSize, coreFont.FontStyle, 12, (int)coreFont.FontSize ))
            {
                m_device.DrawString(ft, text, new Vector2f (x,y), Colorf.Black);
            }
        }


        public void DrawBezierCurve(Colorf color, Vector2f[] points)
        {
            m_device.SetColor(Colorf.Red);
            drawCurve(points, false);
        }

        internal void drawCurve(Vector2f[] points, bool closed)
        {
            float[] tab = new float[points.Length * 3];
            int offset = 0;
            for (int i = 0; i < points.Length; i++)
            {
                offset = 3 * i;
                tab[offset] = points[i].X;
                tab[offset + 1] = points[i].Y;
            }

            float[] rt = new float[4*3];//3 "is number of vector point. 4 is the number of point
            int seek = rt.Length-3;
            GL.glEnable(GL.GL_MAP1_VERTEX_3);
            
            for (int i = 0; i < tab.Length ; i += seek)
            {
                
                if ((i + 12) < tab.Length)
                {
                    Array.Copy(tab, i, rt, 0, rt.Length);
                }
                else {                    
                    rt = new float[4 * 3]; 
                      Array.Copy(tab, i, rt, 0, tab.Length-i);
                     i = tab.Length;                    
                }
                GL.glMap1f(GL.GL_MAP1_VERTEX_3,
                    0.0f,
                    1.0f,
                    3, 4,
                    rt
                    );

                this.m_device.Begin(enuGraphicsPrimitives.LineStrip);
                for (int j = 0; j <= 30; j++)
                {
                    GL.glEvalCoord1f(j / 30.0f);
                }
                this.m_device.End();
                if (closed)
                {
                    this.m_device.Begin(enuGraphicsPrimitives.LineStrip);
                    this.m_device.SetVertex(points[points.Length-1]);
                    this.m_device.SetVertex(points[2]);
                    //this.m_device.SetVertex(points[0]);
                    this.m_device.End();
                }

                //this.m_device.Begin(enuGraphicsPrimitives.Points);
                //this.m_device.RenderState.PointSize = 5;
                //for (int j = 0; j <  rt.Length; j+=3)
                //{
                //     GL.glVertex2f(rt[j], rt[j + 1]);
                //}
                //this.m_device.RenderState.PointSize = 1;
                //this.m_device.End();
            }

            GL.glDisable(GL.GL_MAP1_VERTEX_3);
        }

        public void drawLine(Vector2f start, Vector2f end)
        {
            this.m_device.Begin(enuGraphicsPrimitives.Lines);
            this.m_device.SetVertex(start);
            this.m_device.SetVertex(end);
            this.m_device.End();
        }
        public void drawPolygon(Vector2f[] points) {
            this.m_device.Begin(enuGraphicsPrimitives.Polygon);
            for (int i = 0; i < points.Length; i++)
            {
                this.m_device.SetVertex(points[i]);    
            }
            this.m_device.End();
        }
        //public void Visit(PolygonElement element)
        //{
        //    Vector2f[] tab = null;
        //    Byte[] bytes = null;
        //    element.GetPath().GetAllDefinition(out tab, out bytes);
        //    object obj = this.Save();
        //    this.SetupGraphicsDevice(element);
        //    this.m_device.SetColor(element.FillBrush.Colors[0]);
        //    this.drawPolygon(tab);
        //    this.m_device.SetColor(element.StrokeBrush.Colors[0]);
        //    IGKOGL2DPathRenderer r = new IGKOGL2DPathRenderer(tab, bytes);
        //    r.Render(this);


        //    this.Restore(obj);
        //}
        /// <summary>
        /// 
        /// </summary>
        /// <param name="element"></param>
        /// <exception cref="NullArgumentException"></exception>
        public void Visit(Core2DDrawingLayeredElement element)
        {
            if (element == null)
                return;

            Vector2f[] tab = null;
            Byte[] bytes = null;
            element.GetPath().GetAllDefinition (out tab, out bytes);

            if (tab.Length == 0)
                return;

            IGKOGL2DPathRenderer r = new IGKOGL2DPathRenderer(tab,bytes);
            object obj = this.Save();
            this.SetupGraphicsDevice(element);
            //no fill graphics path
            //ICoreBrush br = element.GetBrush(enuBrushMode.Fill);
            //if (br != null)
            //{
            //    this.m_device.SetColor(br.Colors[0]);
            //    IGKOGL2DTessalationRenderer.RenderPolygon(this.m_device,element, tab, bytes);
            //}

            ICoreBrush br = element.GetBrush(enuBrushMode.Stroke);
            if (br != null)
                this.m_device.SetColor(br.Colors[0]);
            r.Render(this);
            this.Restore(obj);

            
        }
        public void Visit(BezierElement bezier)
        {
            object obj = this.Save();
            this.SetupGraphicsDevice(bezier);
            this.MultiplyTransform(bezier.GetMatrix(), enuMatrixOrder.Prepend);
            this.m_device.RenderState.LineWidth = bezier.StrokeBrush.Width;
            this.m_device.Capabilities.LineSmooth = true;
            DrawBezierCurve(Colorf.Black, bezier.Points);
            this.m_device.Capabilities.LineSmooth = false;
            this.Restore (obj);

        }

        public void Visit(RoundRectangleElement rcElement)
        {
            if (rcElement == null)
                return;

            object obj = this.Save();
            this.SetupGraphicsDevice(rcElement);
            this.MultiplyTransform(rcElement.GetMatrix(), enuMatrixOrder.Prepend);

            Vector3f radiusTopLeft = rcElement.TopLeft;
            Vector3f radiusTopRight = rcElement.TopRight;
            Vector3f radiusBottomRight = rcElement.BottomRight ;
            Vector3f radiusBottomLeft = rcElement.BottomLeft;

            

            Rectanglef rc = rcElement.Bounds;
            var device = this.Device;
            float x = rc.X;
            float y = rc.Y;
            float w = rc.Width;
            float h = rc.Height;
            const float M_PI = (float)Math.PI;

            device.Hint.PointSmooth = true;
            device.Hint.PolygonSmooth = true;
            device.Hint.PolygonSmoothHint = Hint.Nicest;
            device.Hint.LineSmooth = true;
            device.Hint.LineSmoothHint = Hint.Nicest;
            device.SetColor(rcElement.FillBrush.Colors[0]);
            device.Begin(enuGraphicsPrimitives.Polygon);
            for (float i = (float)M_PI; i <= 1.5f * M_PI; i += 0.1f)
                device.SetVertex((float)(radiusTopLeft.X * Math.Cos(i)) + x + radiusTopLeft.X, (float)(radiusTopLeft.Y * Math.Sin(i)) + y + radiusTopLeft.Y);
            for (float i = 1.5f * (float)M_PI; i <= 2 * M_PI; i += 0.1f)
                device.SetVertex((float)(radiusTopRight.X * Math.Cos(i)) + x + w - radiusTopRight.X, (float)(radiusTopRight.Y * Math.Sin(i)) + y + radiusTopRight.Y);
            for (float i = 0; i <= 0.5f * M_PI; i += 0.1f)
                device.SetVertex((float)(radiusBottomRight.X * Math.Cos(i)) + x + w - radiusBottomRight.X, (float)(radiusBottomRight.Y * Math.Sin(i)) + y + h - radiusBottomRight.Y);
            for (float i = 0.5f * (float)M_PI; i <= M_PI; i += 0.1f)
                device.SetVertex((float)(radiusBottomLeft.X * Math.Cos(i)) + x + radiusBottomLeft.X, (float)(radiusBottomLeft.Y * Math.Sin(i)) + y + h - radiusBottomLeft.Y);
            device.SetVertex(x, y + radiusTopLeft .Y );
            device.End();

            this.Restore(obj);
        

            
        }

        internal void drawLines(Vector2f[] points, bool closed)
        {
            this.m_device.Begin((closed &&
                points.Length >2)?enuGraphicsPrimitives.LineLoop: enuGraphicsPrimitives.LineStrip);
            for (int i = 0; i < points.Length; i++)
            {
                this.m_device.SetVertex(points[i]);
            }
            this.m_device.End();
        }


     
        public void DrawRtf(string text, Rectanglef bounds)
        {
            
        }


        public void DrawCube(Vector3f location, float size)
        {
            this.DrawCube(location, new Vector3f(size));
        }

        public void DrawCube(Vector3f location, Vector3f size)
        {
            this.Device.DrawWiredCube(Vector2f.Zero, size);
        }


        public global::IGK.ICore.Matrix GetCurrentTransform()
        {
            return this.m_matrix.Clone() as global::IGK.ICore.Matrix;
        }


        public Rectanglei TransformToViewportBound(Rectanglei rc)
        {
            rc.Y = this.Device.Viewport.Height - rc.Height - rc.Y;
            return rc;
        }
        public void FillPolygon(Colorf colorf, Vector2f[] t)
        {
            if (t == null)
                return;
            Brush br = CoreBrushRegisterManager.GetBrush<Brush>(colorf);
            PointF[] r = new PointF[t.Length];
            for (int i = 0; i < t.Length; i++)
            {
                r[i] = new PointF(t[i].X, t[i].Y);
            }
            //this.m_device.DrawUserIndexPrimitive<Vector2fColor>(enuGraphicsPrimitives.Polygon ,.FillPolygon(br, r);
        }

        public ICoreBitmap Copy(Rectanglei rectanglei)
        {
            return null;
        }

        public ICoreBitmap CopyFromScreen(Rectanglei rc, enuCopyMode mode)
        {
            return null;
        }


        public void DrawRectangle(Colorf colorf, Rectanglef brc)
        {
            throw new NotImplementedException();
        }

        public void DrawCurve(Colorf color, IEnumerable<IVector2f> points)
        {
            throw new NotImplementedException();
        }
    }
}
