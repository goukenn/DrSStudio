

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: WinCoreBitmapDeviceVisitor.cs
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
file:WinCoreBitmapDevice.cs
*/

using IGK.ICore.GraphicModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IGK.ICore.WinCore
{
    using IGK.ICore;
    using IGK.ICore.Drawing2D;
    using IGK.ICore.Drawing2D.Imaging;
    using IGK.ICore.Drawing2D.WorkingObjects.Standard;
    using IGK.ICore.GraphicModels;
    /// <summary>
    /// Represent the base wincore visitor. to render igk working object on a surface
    /// </summary>
    public class WinCoreBitmapDeviceVisitor :
        Core2DDrawingDeviceVisitorBase,
        ICoreGraphics,
        ICore2DDrawingVisitor 
    {
        private Graphics m_device;
        private GraphicsPath m_currentPath;
        private object syncobj = new object();
        private ICore2DDrawingVisitable m_currentVisiting;//object that is currently visited
        private Bitmap m_sourceBitmap;

        public Graphics Device { get { return m_device; } }

        /// <summary>
        /// get the brush used to fill the entired shape
        /// </summary>
        public ICoreBrush FillBrush { get;set;} 
        public ICorePen StrokeBrush { get;set;}
        static WinCoreBitmapDeviceVisitor() {         
        }

        protected WinCoreBitmapDeviceVisitor()
        {
            this.m_currentPath = new GraphicsPath();
            this.FillBrush = null;
            this.StrokeBrush = null;
        }
        public bool Accept(ICoreWorkingObject obj)
        {
            return (obj !=null);// MethodInfo.GetCurrentMethod().DeclaringType.GetMethod("Visit", new Type[] { typeof(ICoreWorkingObject) }).Accept(this, obj);            
        }
        protected  virtual void Visit(DocumentBlockElement document)
        {
            object obj = Save();
            this.SetupGraphicsDevice(document);
            this.MultiplyTransform(document.Matrix, enuMatrixOrder.Prepend);
            document.Document.Draw(this);      
            Restore(obj);
        }

        public void Visit(ICore2DDrawingDocument document)
        {
            this.Visit(document, false, new Rectanglei(0, 0, document.Width, document.Height), enuFlipMode.None);
        }
        public void Visit(ICore2DDrawingVisitable item)
        {
            if (item == null)
                return;
            var v_old = this.m_currentVisiting;
            this.m_currentVisiting = item;
            if (item.Accept (this))
            {
                item.Visit(this);
            }
            this.m_currentVisiting = v_old;
        }
        public void  Visit(GroupElement group)
        {
            object obj = Save();
            this.SetupGraphicsDevice(group);
            foreach (ICoreWorkingObject item in group.Elements)
            {
                item.Draw(this);
            }
            Restore(obj);
        }

      
        public override void Visit(ICoreWorkingObject obj, Type requestedType)
        {
            if (obj == null)
                return;

            if (obj is ICore2DDrawingShadowElement)
            {
                ICore2DDrawingShadowElement r = obj as ICore2DDrawingShadowElement;
                if (r.AllowShadow && r.CanRenderShadow)
                {
                    RenderShadow(r);
                }
            }

            if ((this.m_currentVisiting != obj) && (obj is ICore2DDrawingVisitable))
            {
                Visit((obj as ICore2DDrawingVisitable));
            }
            else
            {
                MethodInfo.GetCurrentMethod().Visit(this, new Type[]{
                    requestedType,
                } , obj );
            }
        }
        
        public void Flush()
        {
            this.m_device.Flush();
        }
        public void Dispose()
        {
            this.m_device.Dispose();
            if (this.m_currentPath != null)
            {
                this.m_currentPath.Dispose();
                this.m_currentPath = null;
            }
            this.m_device = null;
        }
        /// <summary>
        /// create a visitor
        /// </summary>
        /// <param name="g"></param>
        /// <returns></returns>
        public  static WinCoreBitmapDeviceVisitor Create(Graphics g, Bitmap sourceBitmap = null)
        {
            if (g == null)
                return null;
            WinCoreBitmapDeviceVisitor winCoreBitmapDeviceVisitor = new WinCoreBitmapDeviceVisitor();
            WinCoreBitmapDeviceVisitor dev = winCoreBitmapDeviceVisitor;
            dev.m_device = g;
            dev.m_sourceBitmap = sourceBitmap;
            return dev;
        }
        public void Visit(WinCoreBitmap bmp)
        {
            var s = bmp.Bitmap.PhysicalDimension;
            var r = bmp.Bitmap.HorizontalResolution;
            var h = bmp.Bitmap.VerticalResolution ;
            var px = this.m_device.DpiX;
            var py = this.m_device.DpiY;
            try
            {
                if ((px == r) && (py == h))
                {
                    this.m_device.DrawImage(bmp.Bitmap, Point.Empty);
                }
                else
                {
                    //disable dpi transformatio
                    Rectangle rc = new Rectangle(0, 0, (int)s.Width, (int)s.Height);
                    this.m_device.DrawImage(bmp.Bitmap, rc, rc, GraphicsUnit.Pixel);
                }
            }
            catch (Exception ex) {
                CoreLog.WriteLine(ex.Message);
            }

        }
        public void Visit(ImageElement image)
        {
            this.Visit(image.Bitmap);
        }
        public void Visit(Core2DDrawingLayeredElement element)
        {

            //if (element.GetObjectName().ToLower() == "linesegments")
            //{ 

            //}
            CoreGraphicsPath v_p = element.GetPath ();
            GraphicsPath v_gdipath = v_p.WinCoreToGdiGraphicsPath();
            if (v_gdipath != null)
            {
                lock (v_gdipath)
                {
                    GraphicsState v_state = this.m_device.Save();
                    //setup graphics device
                    this.SetupGraphicsDevice(element);


                    Pen v_pen = GetStrokeBrush(v_p);
                    Brush v_br = GetBrush(v_p);
                    if (v_br != null)
                        this.m_device.FillPath(v_br, v_gdipath);
                    if (v_pen != null)
                        this.m_device.DrawPath(v_pen, v_gdipath);
                    this.m_device.Restore(v_state);
                    v_gdipath.Dispose();
                }
            }
        }

        public void Visit(TextElement text)
        {//visit a text drawing element
            if (text == null)
                return;
            switch(text.RenderMode)
            {
                case enuTextElementMode.Text :
                    var v_state = this.Save();                    
                    this.SetupGraphicsDevice(text);
                    this.MultiplyTransform(text.GetMatrix(), enuMatrixOrder.Append);
                    this.DrawString(text.Text, text.Font,
                        text.FillBrush,
                        text.Bounds);
            this.Restore(v_state);
                    break ;
                case enuTextElementMode.Path :
                    this.Visit(text as Core2DDrawingLayeredElement);
                    break ;
            }


        }


        public void Visit(LayerObjectElement element) {

            object obj = this.Save();
            foreach (var l in element.Layers) {
                this.Visit(l);
            }
            this.ResetClip();
            Restore(obj);

        }

        
        /// <summary>
        /// render shadow element
        /// </summary>
        /// <param name="element">element to render shadow</param>
        /// <param name="gdiPath">path for shadow rendering</param>
        /// <param name="shadowProperty">shadow property</param>
        protected virtual void RenderShadow(ICore2DDrawingShadowElement element)
        {
            if ((element == null) || (!element.CanRenderShadow))
                return;
            MethodInfo v_meth =
                element.GetType().GetMethod("VisitShadow", BindingFlags.Public | BindingFlags.Instance);
         
            ICore2DDrawingBlurFilter filter = CoreFilterManager.GetFilter("Blur") as ICore2DDrawingBlurFilter;
            GraphicsPath v_gdipath = element.GetPath().WinCoreToGdiGraphicsPath();
            var shadowProperty = element.ShadowProperty;
            if (shadowProperty == null)
                return;
            Brush v_br = CoreApplicationManager.Application.BrushRegister.GetBrush<Brush>(shadowProperty.Brush);
            if ((v_gdipath == null)|| (v_br ==null))
                return;
            RectangleF rc = v_gdipath.GetBounds();
            Rectangle v_rci = Rectangle.Ceiling(rc);
            if (shadowProperty.Blur)
            {
                int radius = shadowProperty.BlurRadius;
                filter.Radius = radius;
                filter.Edge = shadowProperty.BlurEdge;
                v_rci.Inflate(radius, radius);
            
                if ((v_rci.Width > 0) && (v_rci.Height > 0))
                {
                    using (Bitmap bmp = new Bitmap(v_rci.Width, v_rci.Height, PixelFormat.Format32bppArgb))
                    {
                        Graphics g = Graphics.FromImage(bmp);
                        g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;                       
                        g.TranslateTransform(-v_rci.X, -v_rci.Y, MatrixOrder.Append);
                        if (v_meth != null)
                        {
                            
                            v_meth.Invoke(element, new object[] { WinCoreBitmapDeviceVisitor.Create(g)});
                        }
                        else
                            g.FillPath(v_br, v_gdipath);
                        g.Flush();

                        //applying the blur effect
                        WinCoreBitmap ct = WinCoreBitmap.Create(bmp);
                        filter.Apply(ct, new Rectanglei(0, 0, bmp.Width, bmp.Height));

                        var v_state = this.m_device.Save();
                        this.SetupGraphicsDevice(element);
                        // this.MultiplyTransform(element.GetMatrix(), enuMatrixOrder.Append);
                        this.m_device.TranslateTransform(rc.X, rc.Y, MatrixOrder.Prepend);
                        this.m_device.TranslateTransform(shadowProperty.Offset.X - radius, shadowProperty.Offset.Y - radius, MatrixOrder.Prepend);
                        //this.Draw(ct, new Rectanglei(0 , 0, bmp.Width, bmp.Height));
                        this.m_device.DrawImage(ct.Bitmap, Point.Empty);
                        this.m_device.ResetTransform();
                        this.m_device.Restore(v_state);
                        ct.Dispose();


                    }
                }
            }
            else
            {
                //not blur
                var v_state = this.m_device.Save();
                this.SetupGraphicsDevice(element);
                if (shadowProperty.IsClipped)
                {
                    ///because WinCoreToGdiGraphicsPath create new gdi graphics path we can easily operate on it
                    GraphicsPath v_cp = element.GetPath().WinCoreToGdiGraphicsPath();
                    if (v_cp != null)
                    {
                        System.Drawing.Drawing2D.Matrix m = new System.Drawing.Drawing2D.Matrix();
                        m.Translate(shadowProperty.Offset.X, shadowProperty.Offset.Y, MatrixOrder.Append);
                        v_cp.Transform(m);
                        m.Dispose();

                        Region rg = new Region(v_cp);
                        rg.Exclude(v_gdipath);
                        this.m_device.Clip = rg;

                        v_cp.Dispose();
                    }
                }
                this.m_device.TranslateTransform(shadowProperty.Offset.X, shadowProperty.Offset.Y, MatrixOrder.Prepend);
                if (v_meth != null)
                    v_meth.Invoke(element, new object[] { this });
                else
                    this.m_device.FillPath(v_br, v_gdipath);
                this.m_device.Restore(v_state);
            }
        }
        private Pen GetStrokeBrush(CoreGraphicsPath v_p)
        {
            if (this.StrokeBrush == null)
            {
                if (v_p.StrokeBrush != null)
                {
                    return CoreApplicationManager.Application.BrushRegister.GetPen<Pen>(v_p.StrokeBrush);
                }
                return null;
            }
            return CoreApplicationManager.Application.BrushRegister.GetPen<Pen>(this.StrokeBrush);
        }
        private Brush GetBrush(CoreGraphicsPath v_p)
        {
            if (FillBrush == null)
            {
                if (v_p.FillBrush != null)
                {
                    return CoreApplicationManager.Application.BrushRegister.GetBrush<Brush>(v_p.FillBrush) as Brush;
                }
                return null;
            }
            return CoreApplicationManager.Application.BrushRegister.GetBrush<Brush>(FillBrush);
        }
        public void SetupGraphicsDevice(ICoreWorkingObject obj)
        {
            ICore2DDrawingGraphicsProperty p = obj as ICore2DDrawingGraphicsProperty;
            this.SetupGraphicsDevice(p);
        }
        private void SetupGraphicsDevice(ICore2DDrawingGraphicsProperty p)
        {
            if (p == null) return;
            this.m_device.CompositingMode =(CompositingMode) p.CompositingMode;
            this.m_device.SmoothingMode =(SmoothingMode) p.SmoothingMode ;
        }
        public void Draw(ICoreWorkingObject obj, bool proportional, Rectanglei rectangle, enuFlipMode flipMode)
        {
            MethodInfo.GetCurrentMethod().Visit(this, obj, proportional, rectangle, flipMode );
        }
        public void Draw(ICoreBitmap  obj, bool proportional, Rectanglei rectangle, enuFlipMode flipMode)
        {
            this.Visit(obj, proportional, rectangle, flipMode);
        }
        public virtual void Draw(ICore2DDrawingLayer layer, bool proportional, Rectanglei rectangle, enuFlipMode flipMode)
        {
            if (this.m_device == null)
                return;
            var c = layer.ParentDocument as ICore2DDrawingDocument;

            Rectanglef v_rc = Rectanglef.Empty ;


            if (c == null)
            {
                v_rc = rectangle;
            }
            else
            {
                v_rc = new Rectanglef(0, 0, c.Width, c.Height)
                {
                    X = 0,
                    Y = 0
                };
            }            
            object v_state = this.Save();            
            //-----------------------------------------------------------------------------------
            //setup matrix
            //-----------------------------------------------------------------------------------
            System.Drawing.Drawing2D.Matrix m = this.SetupMatrix(v_rc, rectangle, proportional, flipMode);
            this.m_device.MultiplyTransform(m, MatrixOrder.Prepend);
            m.Dispose();
            this.Visit(layer);
            this.Restore(v_state);
        }
        public virtual void Draw(ICore2DDrawingLayeredElement element, bool proportional, Rectanglei rectangle, enuFlipMode flipMode)
        {
            if (this.m_device == null)
                return;
            object v_state = this.Save();
            Rectanglef v_rc = element.GetBound();
            v_rc.X = 0;
            v_rc.Y = 0;
            //-----------------------------------------------------------------------------------
            //setup matrix
            //-----------------------------------------------------------------------------------
            System.Drawing.Drawing2D.Matrix m = this.SetupMatrix(v_rc, rectangle, proportional, flipMode);
            this.m_device.MultiplyTransform(m, MatrixOrder.Prepend);      
            m.Dispose();
            this.Visit(element);
            this.Restore(v_state);
        }
        public virtual void Draw(ICore2DDrawingDocument document, bool proportional, Rectanglei rectangle, enuFlipMode flipMode)
        {
            if (this.m_device == null)
                return;
            object v_state = this.Save();
            this.m_device.TextContrast = document.TextContrast;
            this.m_device.TextRenderingHint = (TextRenderingHint)document.TextRenderingMode;
            this.m_device.PixelOffsetMode = (PixelOffsetMode)document.PixelOffset;
            this.m_device.InterpolationMode = (InterpolationMode)document.InterpolationMode;
            this.SetupGraphicsDevice(document);
            Rectanglei v_rc = new Rectanglei(0, 0, document.Width, document.Height);
            //-----------------------------------------------------------------------------------
            //setup matrix
            //-----------------------------------------------------------------------------------
            System.Drawing.Drawing2D.Matrix m = this.SetupMatrix(v_rc, rectangle, proportional, flipMode);
            this.m_device.MultiplyTransform(m, MatrixOrder.Prepend );
            m.Dispose();
            if (document.ClipView)
            {
                Region rg = new Region(new RectangleF(0, 0, document.Width, document.Height));
                m_device.Clip = rg;
            }

            if (document.IsClipped)
            {
                this.SetClip(document.GetClippedRegion());
            }
            RenderBackground(document);

            //render document
            RenderDocument(document, v_rc);

         
            this.Restore(v_state);
        }

        private void RenderDocument(ICore2DDrawingDocument document, Rectanglei v_rc)
        {
            var graphics = this.m_device;
            switch (document.RenderingMode)
            {
                case enuRenderingMode.Pixel:
                    //create a offscreen bmp
                    using (Bitmap v_img = new Bitmap(v_rc.Width, v_rc.Height))
                    {
                        Graphics v_g = Graphics.FromImage(v_img);
                        v_g.TextContrast = document.TextContrast;
                        v_g.TextRenderingHint = (TextRenderingHint)document.TextRenderingMode;
                        v_g.PixelOffsetMode = (PixelOffsetMode)document.PixelOffset;
                        v_g.InterpolationMode = (InterpolationMode)document.InterpolationMode;
                        //this.SetupGraphicsDevice(document);
                        using (WinCoreBitmapDeviceVisitor dev = WinCoreBitmapDeviceVisitor.Create(v_g))
                        {
                            RenderBackground(document);
                            if (document.IsClipped)
                            {
                                //v_g.Clip = GetRion( obj..GetClippedRegion();
                            }
                            Rectangle v_trc = Rectangle.Round(new RectangleF(0, 0, v_rc.Width, v_rc.Height));
                            foreach (ICore2DDrawingLayer layer in document.Layers)
                            {
                                v_g.Clear(Color.Empty);
                                if (layer.View)
                                {
                                    layer.Draw(dev);
                                    this.m_device.DrawImage(v_img,
                                       v_trc,
                                        0,
                                        0,
                                        v_rc.Width,
                                        v_rc.Height,
                                        GraphicsUnit.Pixel,
                                        GetImageAttributes(layer));
                                }
                                else
                                {
                                    graphics.DrawImage(v_img,
                                        0, 0, v_rc.Width, v_rc.Height);
                                }
                            }
                        }
                        v_g.Dispose();
                    }
                    break;
                case enuRenderingMode.Vector:
                    foreach (ICore2DDrawingLayer l in document.Layers)
                    {
                        if (l.View && Accept(l))
                        {
                            Visit(l);
                        }
                    }
                    break;
            }
        }

	
        public virtual void Visit(ICore2DDrawingDocument document, bool proportional, Rectanglei rectangle, enuFlipMode flipMode)
        {

            if (m_device == null)
                return;
            GraphicsState v_state = this.m_device.Save();
            //setup graphics
            Graphics graphics = this.m_device;
            graphics.TextContrast = document.TextContrast;
            graphics.TextRenderingHint = (TextRenderingHint)document.TextRenderingMode;
            graphics.PixelOffsetMode = (PixelOffsetMode)document.PixelOffset;
            graphics.InterpolationMode = (InterpolationMode)document.InterpolationMode;
            this.SetupGraphicsDevice(document);

            Rectanglei v_rc = new Rectanglei(0, 0, document.Width, document.Height);

            //-----------------------------------------------------------------------------------
            //setup matrix
            //-----------------------------------------------------------------------------------

            System.Drawing.Drawing2D.Matrix m =
                this.SetupMatrix(v_rc, rectangle, proportional, flipMode);

     
            this.m_device.MultiplyTransform(m, MatrixOrder.Prepend);

            if (document.ClipView)
            {
                Region rg = new Region(new Rectangle(0, 0, document.Width, document.Height));
                graphics.IntersectClip(rg);
            }
            RenderDocument (document , v_rc );
           
            this.m_device.Restore(v_state);
        }

        public void Visit(ICoreBitmap bitmap, bool proportional, Rectanglei rectangle, enuFlipMode flipMode)
        {
            if ((bitmap  == null) || (this.m_device ==null))
                return;
            System.Drawing.Drawing2D.Matrix m = SetupMatrix(new Rectanglef(0, 0, bitmap.Width, bitmap.Height), rectangle, proportional, flipMode);
            this.m_device.MultiplyTransform(m, MatrixOrder.Append);
            bitmap.Draw(this);            
            m.Dispose();
        }
        public void Visit(ICore2DDrawingLayer layer)
        {
            this.Visit(layer, false);
        }
        public virtual void Visit(ICore2DDrawingLayer layer, bool forceview)
        {

            if (!forceview && !layer.View)
                return;
            object obj = this.Save();
            bool v_isClipped = layer.IsClipped;
            var m = layer.Matrix;
            if (!m.IsIdentity)
                this.MultiplyTransform(m, enuMatrixOrder.Append);
            m.Dispose();
            if (v_isClipped)
            {
                this.SetClip(layer.ClippedElement);
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
            this.Restore(obj);
            if (v_isClipped)
            {
                //this.ResetClip();
                if ((layer.ClippedElement.View) && Accept(layer.ClippedElement))
                {
                    Visit(layer.ClippedElement);
                }
            }
          
        }

        
        protected virtual void RenderBackground(ICore2DDrawingDocument document)
        {
            if ((document.BackgroundTransparent == false)&&  (document.FillBrush != null))
            {
                {
                    Brush c =
                        CoreBrushRegisterManager.GetBrush<Brush>(document.FillBrush);
                    if (c!=null){
                    this.m_device.FillRectangle(
                        c,
                        new Rectangle(Point.Empty, new Size(document.Width, document.Height)));
                    }
                }
            }
        }
        private new System.Drawing.Drawing2D.Matrix SetupMatrix(Rectanglef src, Rectanglef dest, bool proportional, enuFlipMode flipMode)
        {

            System.Drawing.Drawing2D.Matrix m = new System.Drawing.Drawing2D.Matrix();
            if (src.IsEmpty)
                return m;

            float fx = dest.Width / (float)src.Width;
            float fy = dest.Height / (float)src.Height;


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
                m.Scale(fx, fy, MatrixOrder.Append);
                m.Translate(dest.X, dest.Y, MatrixOrder.Append);


                switch (flipMode)
                {
                    case enuFlipMode.None:
                        break;
                    case enuFlipMode.FlipVertical:
                        m.Translate(0, dest.Height, MatrixOrder.Append);
                        break;
                    case enuFlipMode.FlipHorizontal:
                        m.Translate(dest.Width, 0, MatrixOrder.Append);
                        break;
                    default:
                        break;
                }

            }
            else
            {
                fx = Math.Min(fx, fy);
                fy = fx;
                m.Scale(fx, fy, MatrixOrder.Append);
                int posx = (int)(((-src.Width * fx) / 2.0f) + (dest.Width / 2.0f));
                int posy = (int)(((-src.Height * fx) / 2.0f) + (dest.Height / 2.0f));
                m.Translate(posx + dest.X, posy + dest.Y, MatrixOrder.Append);


                switch (flipMode)
                {
                    case enuFlipMode.None:
                        break;
                    case enuFlipMode.FlipVertical:
                        m.Translate(0, dest.Height, MatrixOrder.Append);
                        break;
                    case enuFlipMode.FlipHorizontal:
                        m.Translate(dest.Width, 0, MatrixOrder.Append);
                        break;
                    default:
                        break;
                }
            }
            return m;
        }
     
        private void SetClip(ICoreGraphicsPath coreGraphicsPath)
        {
            GraphicsPath p = coreGraphicsPath.WinCoreToGdiGraphicsPath();
            this.m_device.SetClip(p, CombineMode.Intersect);
            p.Dispose();
        }
      
        /// <summary>
        /// method visitor for drawing 2d layer
        /// </summary>
        /// <param name="layer"></param>
        /// <param name="bound"></param>
        /// <param name="proportional"></param>
        /// <param name="flipmode"></param>
        public void Visit(ICore2DDrawingLayer layer, bool proportional, Rectanglei bound, enuFlipMode flipmode)
        {
            int w = 0;
            int h = 0;
            if (layer.ParentDocument is ICore2DDrawingDocument v_doc)
            {
                w = v_doc.Width;
                h = v_doc.Height;
            }
            else
            {
                w = bound.Width;
                h = bound.Height;
            }
            System.Drawing.Drawing2D.Matrix m = SetupMatrix(new Rectanglef(0, 0, w, h ),
                bound, proportional, flipmode);
            object obj = this.Save();
            this.m_device.MultiplyTransform(m, MatrixOrder.Append);            
            Visit(layer, true );
            this.Restore(obj);
        }
        
        /// <summary>
        /// set clip for element
        /// </summary>
        /// <param name="element"></param>
        private void SetClip(ICore2DDrawingLayeredElement element)
        {   
            GraphicsPath p = element.GetPath().WinCoreToGdiGraphicsPath ();
            if (p != null)
            {  
                this.m_device.SetClip(p, CombineMode.Intersect);                
                p.Dispose();
            }
        }
        private ImageAttributes GetImageAttributes(ICore2DDrawingLayer layer)
        {
            ImageAttributes imgAttr = new ImageAttributes();

            return imgAttr ;
        }
        public void FillRectangle(ICoreBrush brush, float x, float y, float width, float height)
        {
            Brush br = CoreApplicationManager.Application.BrushRegister.GetBrush<Brush>(brush);
            if (br != null)
            {
                this.m_device.FillRectangle(br, x, y, width, height);
            }else{
                CoreLog.WriteLine("there is some error  brush is null");
            }
        }
        public void DrawRectangle(ICorePen pen, float x, float y, float width, float height)
        {
            Pen br = CoreApplicationManager.Application.BrushRegister.GetPen<Pen>(pen);
            this.m_device.DrawRectangle(br, x, y, width, height);
        }
        public object Save()
        {
            return this.m_device.Save();
        }
        public void Restore(object state)
        {
            if (state is GraphicsState s)
                this.m_device.Restore(s);
        }
        public void TranslateTransform(float dx, float dy, enuMatrixOrder order)
        {
            this.m_device.TranslateTransform (dx, dy, (MatrixOrder)order);
        }
        public void ScaleTransform(float ex, float ey, enuMatrixOrder order)
        {
            this.m_device.ScaleTransform(ex, ey, (MatrixOrder)order);
        }
        public void Clear(Colorf color)
        {
             this.m_device.Clear(color.CoreConvertTo<Color>());
        }
        public void FillRectangle(Colorf color, float x, float y, float width, float height)
        {
            this.Device.FillRectangle(CoreBrushRegisterManager.GetBrush<Brush>(color),
                x, y, width, height);
        }
        public void DrawRectangle(Colorf color, float x, float y, float width, float height)
        {
            this.DrawRectangle(color, 1.0F, enuDashStyle.Solid,
                x, y, width, height);
        }
        public void FillEllipse(ICoreBrush brush, float x, float y, float width, float height)
        {
            this.Device.FillEllipse(GetBrush (brush),
            x, y, width, height);
        }
        public void FillEllipse(Colorf color, float x, float y, float width, float height)
        {
            this.Device.FillEllipse(CoreBrushRegisterManager.GetBrush<Brush>(color),
             x, y, width, height);
        }
        public void DrawEllipse(Colorf color, float x, float y, float width, float height)
        {
            this.Device.DrawEllipse(CoreBrushRegisterManager.GetPen<Pen>(color),
            x, y, width, height);
        }
        public void DrawEllipse(ICorePen pen, float x, float y, float width, float height)
        {
            this.Device.DrawEllipse(GetPen (pen),
x, y, width, height);
        }
        static Brush GetBrush(ICoreBrush brush)
        {
            Brush br = CoreApplicationManager.Application.BrushRegister.GetBrush<Brush>(brush);
            return br;
        }
        static Pen GetPen(ICorePen brush)
        {
            Pen br = CoreApplicationManager.Application.BrushRegister.GetPen<Pen>(brush);
            return br;
        }
        /// <summary>
        /// draw rectangle
        /// </summary>
        /// <param name="color"></param>
        /// <param name="penwidth"></param>
        /// <param name="style"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        public void DrawRectangle(Colorf color, float penwidth, enuDashStyle style, float x, float y, float width, float height)
        {
            Pen p = CoreBrushRegisterManager.GetPen<Pen>(color);
            p.Width = penwidth;
            p.DashStyle = (DashStyle)style;
           
            this.m_device.DrawRectangle(p,(int)Math.Round(x), (int)Math.Round(y), width, height);
            p.Width = 1.0f;
            p.DashStyle = DashStyle.Solid;
        }
        public void Release()
        {
            this.m_currentPath.Dispose();
            this.m_currentPath = null;
            this.m_device = null;
        }
        public enuSmoothingMode SmoothingMode
        {
            get
            {
                return (enuSmoothingMode)m_device.SmoothingMode;
            }
            set
            {
                m_device.SmoothingMode = (SmoothingMode)value;
            }
        }
        public enuInterpolationMode InterpolationMode
        {
            get
            {
                return (enuInterpolationMode) m_device.InterpolationMode;
            }
            set
            {
                m_device.InterpolationMode = (global::System.Drawing.Drawing2D.InterpolationMode)value;
            }
        }
        public enuCompositingMode CompositingMode
        {
            get
            {
                return (enuCompositingMode)this.m_device.CompositingMode;
            }
            set
            {
                this.m_device.CompositingMode = (CompositingMode)value;
            }
        }
        public enuTextRenderingMode TextRenderingMode
        {
            get
            {
                return (enuTextRenderingMode)this.m_device.TextRenderingHint;
            }
            set
            {
                this.m_device.TextRenderingHint = (TextRenderingHint) value; 
            }
        }
        public enuPixelOffset PixelOffsetMode
        {
            get
            {
                return (enuPixelOffset )m_device .PixelOffsetMode;
            }
            set
            {
                m_device .PixelOffsetMode =(PixelOffsetMode ) value ;
            }
        }
        public void FillPath(Colorf colorf, CoreGraphicsPath p)
        {
            using (GraphicsPath v_gp = p.WinCoreToGdiGraphicsPath())
            {
                this.m_device.FillPath(
                    WinCoreBrushRegister.GetBrush(colorf),
                    v_gp);
            }
        }
        public void DrawPath(Colorf colorf, CoreGraphicsPath p)
        {
            if ((p ==null) || (p.PathPoints.Length == 0))
                return;
            using (GraphicsPath v_gp = p.WinCoreToGdiGraphicsPath())
            {
                this.m_device.DrawPath(
                    WinCoreBrushRegister.GetPen (colorf),
                    v_gp);
            }
        }
        public void FillPath(ICoreBrush br, CoreGraphicsPath p)
        {
            var b = GetBrush(br);
            if (b == null) return;
            using (GraphicsPath v_gp = p.WinCoreToGdiGraphicsPath())
            {
                if (v_gp != null)
                {
                    this.m_device.FillPath(
                       b,
                        v_gp);
                }
            }
        }
        public void DrawPath(ICorePen pen, CoreGraphicsPath p)
        {
            using (GraphicsPath v_gp = p.WinCoreToGdiGraphicsPath())
            {
                if (v_gp != null)
                {
                    this.m_device.DrawPath(
                        GetPen(pen),
                        v_gp);
                }
            }
        }


        public void ResetTransform()
        {
            this.m_device.ResetTransform();
        }

        public void RotateTransform(float angle, enuMatrixOrder order)
        {
            this.m_device.RotateTransform(angle, (MatrixOrder)order);
        }

        public Rectanglef ClipBounds { get {
            RectangleF r = this.m_device.ClipBounds;
            return new Rectanglef(r.X, r.Y, r.Width, r.Height);
        } }

        public void SetClip(Rectanglef rectangle)
        {
            lock (this.syncobj)
            {
                this.m_device.Clip = new Region(new RectangleF(rectangle.X, rectangle.Y,
                    rectangle.Width, rectangle.Height));
            }
        }
        public void ResetClip()
        {
            lock (this.syncobj)
            {
                this.m_device.ResetClip ();
            }
        }


        public void MultiplyTransform(Matrix matrix, enuMatrixOrder order)
        {
            if ((matrix ==null) ||matrix.IsDisposed )
                return;
            lock (this.syncobj)
            {
                using (global::System.Drawing.Drawing2D.Matrix m = matrix.ToGdiMatrix())
                { 
                    this.m_device.MultiplyTransform(m, (MatrixOrder ) order);
                }
            }
            
        }
        public void DrawString(string text, ICoreFont font, Colorf color, float x, float y)
        {
            if (string.IsNullOrEmpty(text) || (font == null))
                return;
            using (Font v_ft = font.ToGdiFont())
            {
                this.m_device.DrawString(text,
                        v_ft,
                        CoreBrushRegisterManager.GetBrush<Brush>(color),
                        x, y);
            }
        }
        public void DrawString(string text, CoreFont font, Colorf color, Rectanglef bounds)
        {
            if (string.IsNullOrEmpty(text) || (font == null))
                return;
            using (Font v_ft = font.ToGdiFont())
            {
                var s = font.ToStringFormat();
                this.m_device.DrawString(text,
                        v_ft,
                        CoreBrushRegisterManager.GetBrush<Brush>(color), 
                        new RectangleF(bounds.X, bounds.Y, bounds.Width, bounds.Height ),
                        s
                        );
                s.Dispose();
            }
        }

        public void DrawString(string text, ICoreFont font, ICoreBrush coreBrush, Rectanglef bounds)
        {
            this.DrawString(text, font, coreBrush , bounds, enuTextTrimming.None, false);
        }
        public void DrawString(string text, ICoreFont coreFont, ICoreBrush coreBrush, float x, float y)
        {
            if (string.IsNullOrEmpty(text) ||(coreFont ==null) ||(coreBrush == null))
                return ;
            using (Font v_ft = coreFont.ToGdiFont()){
                this.m_device.DrawString(text,
                        v_ft,
                        GetBrush(coreBrush),
                        x, y);
            }

        }
        public void DrawString(string text, ICoreFont font, ICoreBrush Brush, Rectanglef bound, enuTextTrimming trimming, bool isMnemocic)
        {
            if (string.IsNullOrEmpty(text) || (font == null) || (Brush == null))
                return;
            using (StringFormat sf = font.ToStringFormat())
            {

                Font v_ft = font.ToGdiFont();//.FontFamily(), font.FontSize, (FontStyle)font.FontStyle);                
                if (isMnemocic)
                    sf.HotkeyPrefix = HotkeyPrefix.Show;
                else {
                    sf.HotkeyPrefix = HotkeyPrefix.None;
                }
                switch (trimming)
                {
                    case enuTextTrimming.None:
                        break;
                    case enuTextTrimming.Char:
                        sf.Trimming = StringTrimming.Character;
                        break;
                    case enuTextTrimming.Word:
                        sf.Trimming = StringTrimming.EllipsisWord;
                        break;
                    default:
                        break;
                }
                this.m_device.DrawString(text,
                    v_ft,
                    GetBrush(Brush),
                    bound, sf);

                v_ft.Dispose();

            } 
        }
        public void DrawString(string text, CoreFont font, ICoreBrush Brush, Rectanglef bound)
        {
            DrawString(text, font, Brush, bound, enuTextTrimming.None, false);
        }


        public void DrawLine(Colorf color, float x1, float y1, float x2, float y2)
        {
            Pen p = WinCoreBrushRegister.GetPen(color);
            
            this.m_device.DrawLine(p,
                x1, y1, x2, y2);
        }

        public void DrawLine(Colorf color, float penwidth, enuDashStyle style, float x1, float y1, float x2, float y2)
        {
            Pen p = WinCoreBrushRegister.GetPen(color);
            if (penwidth > 0)
                p.Width = penwidth;
            p.DashStyle = (DashStyle)style;
            this.m_device.DrawLine(p,
                x1, y1, x2, y2);
            p.DashStyle = DashStyle.Solid;
        }

        public void DrawLine(ICorePen pen, float x1, float y1, float x2, float y2)
        {
            Pen p = GetPen(pen);

             this.m_device.DrawLine(p,
                x1, y1, x2, y2);
        }


        public void Draw(ICore2DDrawingObject obj)
        {
            this.Visit(obj as ICoreWorkingObject);
        }
        public void Draw(ICoreBitmap inBmp, Rectanglei rectanglei)
        {
            this.Visit(inBmp, false,  rectanglei , enuFlipMode.None );
        }


        public void DrawLine(ICorePen pen, Vector2f start, Vector2f end)
        {
            this.DrawLine(pen, start.X, start.Y, end.X, end.Y);
        }

        public void DrawLine(Colorf color, Vector2f start, Vector2f end)
        {
            this.DrawLine(color, start.X, start.Y, end.X, end.Y);
        }


        public void IntersectClip(Rectanglef rectangle)
        {
            lock (this.syncobj)
            {
                this.m_device.IntersectClip(new RectangleF(rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height));
            }
        }

        public void ExcludeClip(Rectanglef rectangle)
        {
            lock (this.syncobj)
            {
                this.m_device.ExcludeClip(new Region(new RectangleF(rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height)));
            }
        }


        public void FillRectangle(Colorf color, Rectanglef rectangle)
        {
            this.FillRectangle(color, rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height);
        }

        public void TransformViewBox(Rectanglef rectangle, Rectanglef bound, enuFlipMode flipMode, bool proportional)
        {
            //-----------------------------------------------------------------------------------
            //setup matrix
            //-----------------------------------------------------------------------------------

            global::System.Drawing.Drawing2D.Matrix m = new global::System.Drawing.Drawing2D.Matrix();

            float fx = rectangle.Width / (float)bound.Width;
            float fy = rectangle.Height / (float)bound.Height;


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
                m.Scale(fx, fy, MatrixOrder.Append);
                m.Translate(rectangle.X, rectangle.Y, MatrixOrder.Append);


                switch (flipMode)
                {
                    case enuFlipMode.None:
                        break;
                    case enuFlipMode.FlipVertical:
                        m.Translate(0, rectangle.Height, MatrixOrder.Append);
                        break;
                    case enuFlipMode.FlipHorizontal:
                        m.Translate(rectangle.Width, 0, MatrixOrder.Append);
                        break;
                    default:
                        break;
                }

            }
            else
            {
                fx = Math.Min(fx, fy);
                fy = fx;
                m.Scale(fx, fy, MatrixOrder.Append);
                int posx = (int)(((-bound.Width * fx) / 2.0f) + (rectangle.Width / 2.0f));
                int posy = (int)(((-bound.Height * fx) / 2.0f) + (rectangle.Height / 2.0f));
                m.Translate(posx + rectangle.X, posy + rectangle.Y, MatrixOrder.Append);


                switch (flipMode)
                {
                    case enuFlipMode.None:
                        break;
                    case enuFlipMode.FlipVertical:
                        m.Translate(0, rectangle.Height, MatrixOrder.Append);
                        break;
                    case enuFlipMode.FlipHorizontal:
                        m.Translate(rectangle.Width, 0, MatrixOrder.Append);
                        break;
                    default:
                        break;
                }
            }
            this.m_device.MultiplyTransform(m, MatrixOrder.Append);
        }


        public Matrix GetCurrentTransform()
        {
            float [] t = this.m_device.Transform.Elements;
            return new IGK.ICore.Matrix(t[0], t[1], t[2], t[3], t[4], t[5]);
        }
        public void FillPolygon(Colorf colorf, Vector2f[] t)
        {
            if (t==null)
                return ;
            Brush br = CoreBrushRegisterManager.GetBrush<Brush>(colorf);
            PointF[] r = new PointF[t.Length];
            for (int i = 0; i < t.Length; i++)
			{
                r[i] = new PointF (t[i].X , t[i].Y );
			}
            this.m_device.FillPolygon(br, r);
        }

        /// <summary>
        /// copy this bits and return a new ICoreBitmap data
        /// </summary>
        /// <param name="rc"></param>
        /// <returns></returns>
        public ICoreBitmap Copy(Rectanglei rc)
        {

            IntPtr srchdc = this.m_device.GetHdc();// WinCoreBitmapOperation.GetDC(IntPtr.Zero);
            IntPtr hbitmp = IntPtr.Zero;
            IntPtr holdobj = IntPtr.Zero;
            if (this.m_sourceBitmap != null)
            {
                hbitmp = this.m_sourceBitmap.GetHbitmap();
                holdobj = WinCoreBitmapOperation.SelectObject(srchdc, hbitmp);
            }
            //solution !!!!
            //Graphics g = Graphics.FromHdc(srchdc);
            //g.FillRectangle(Brushes.Black, new Rectangle(0, 0, 100, 100));
            //Bitmap r = new Bitmap(CoreConstant.DEBUG_TEMP_FOLDER+"\\r.jpg");
            //g.DrawImage(r, 0, 0);
            //r.Dispose();
            //g.Flush();
            //g.Dispose();

            IntPtr desthdc = IntPtr.Zero;

            var s = WinCoreBitmap.Create (rc.Width, rc.Height);
            var d = s.CreateDevice() as WinCoreBitmapDeviceVisitor ;
          
            desthdc = d.m_device.GetHdc();
            bool m = WinCoreBitmapOperation.CopyBitBlt(srchdc, desthdc, rc.X, rc.Y, rc.Width, rc.Height, 0, 0,
                (uint)enuCopyMode.SrcCopy);
            d.m_device.ReleaseHdc(desthdc);
            
            if (hbitmp != IntPtr.Zero)
            {
                WinCoreBitmapOperation.SelectObject(srchdc, holdobj);
                WinCoreBitmapOperation.DeleteObject(hbitmp);
            }
            this.m_device.ReleaseHdc(srchdc);
                d.Dispose();

            return s;
        }
      

        public ICoreBitmap CopyFromScreen(Rectanglei rc, enuCopyMode mode)
        {
            IntPtr srchdc = WinCoreBitmapOperation.GetDC(IntPtr.Zero);//screen handle
            IntPtr desthdc = IntPtr.Zero;

            var s = WinCoreBitmap.Create(rc.Width, rc.Height);
            var d = s.CreateDevice() as WinCoreBitmapDeviceVisitor;

            desthdc = d.m_device.GetHdc();
            bool m = WinCoreBitmapOperation.CopyBitBlt(srchdc, desthdc, rc.X, rc.Y, rc.Width, rc.Height, 0, 0, (uint)mode);
            d.m_device.ReleaseHdc(desthdc);
            bool k = WinCoreBitmapOperation.ReleaseHDC(IntPtr.Zero, srchdc);
            d.Dispose();
            return s;
        }


        public void DrawRectangle(Colorf colorf, Rectanglef brc)
        {
            this.DrawRectangle(colorf, brc.X, brc.Y, brc.Width, brc.Height);
        }
        

        public void DrawCurve(Colorf color, IEnumerable<IVector2f> points)
        {
            using (GraphicsPath pt = new GraphicsPath())
            {

                List<PointF> cp = new List<PointF>();
                foreach (var item in points)
                {

                    cp.Add(new PointF(item.X, item.Y));

                }
                if (cp.Count > 1)
                {
                    pt.AddCurve(cp.ToArray());
                    
                    this.m_device.DrawPath(
                    WinCoreBrushRegister.GetPen(color),
                    pt);

                }
            }
        }
    }
}

