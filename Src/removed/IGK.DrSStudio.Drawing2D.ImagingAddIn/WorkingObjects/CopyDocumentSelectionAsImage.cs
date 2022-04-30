

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: CopyDocumentSelectionAsImage.cs
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
file:CopyDocumentSelectionAsImage.cs
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
/* 
-------------------------------------------------------------
This file is part of iGK-DEV-DrawingStudio
-------------------------------------------------------------
-------------------------------------------------------------
-------------------------------------------------------------
view license file in Documentation folder to get more info
Copyright (c) 2008-2010 
Author  : Charles A.D. BONDJE DOUE 
mail : bondje.doue@hotmail.com
-------------------------------------------------------------
-------------------------------------------------------------
*/
using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D ;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Windows.Forms;
namespace IGK.DrSStudio.CopySelectionAsImage
{
    using IGK.ICore;using IGK.DrSStudio.WinUI;
    using IGK.DrSStudio;
    using IGK.DrSStudio.Drawing2D;
    using IGK.DrSStudio.Drawing2D.WinUI;
    /// <summary>
    /// Represent pot render mecanism
    /// </summary>
    [Core2DDrawingImageItemAttribute("CopyDocSelection", typeof(Mecanism),
        ImageKey="DE_SelectCopy")]
    sealed class CoreDrawingElement_copySelectionAsImage : Core2DDrawingLayeredDualBrushElement
    {
        protected override void GeneratePath()
        {
            this.SetPath(null);
        }
        private CoreDrawingElement_copySelectionAsImage()
        {
            //used internally
            throw new CoreException( enuExceptionType.OperationNotValid ,"constructor");
        }
        static Bitmap sm_odlBitmap;
        /// <summary>
        /// copy the image region
        /// </summary>
        /// <param name="image">image to copy</param>
        /// <param name="region">region</param>
        /// <returns></returns>
        static Bitmap CopyRegionSelection(Bitmap image, Region region)
        {
            DateTime v_time = DateTime.Now;
            //CoreSystem.Instance.JobStart();
            //clone old image
            sm_odlBitmap = image.Clone() as Bitmap;
            BitmapData data = sm_odlBitmap.LockBits(new Rectangle(Point.Empty, sm_odlBitmap.Size),
                ImageLockMode.ReadWrite, PixelFormat.Format32bppPArgb);
            Byte[] dat = new byte[data.Stride * data.Height];
            Marshal.Copy(data.Scan0, dat, 0, dat.Length);
            Bitmap v_maskbmp = new Bitmap(image.Width, image.Height, PixelFormat.Format32bppPArgb);
            Graphics v_g = Graphics.FromImage(v_maskbmp);
            v_g.InterpolationMode = InterpolationMode.NearestNeighbor;
            v_g.PixelOffsetMode = PixelOffsetMode.HighSpeed;
            v_g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.None;
            v_g.Clear(Color.White);
            v_g.FillRegion(Brushes.Black, region);
            Rectanglef rc = region.GetBounds(v_g);
            v_g.Flush();
            v_g.Dispose();
            if ((rc.Width == 0) || (rc.Height == 0)) return null;
            BitmapData v_maskBitmapData = v_maskbmp.LockBits(new Rectangle(Point.Empty, sm_odlBitmap.Size),
                ImageLockMode.ReadWrite, PixelFormat.Format32bppPArgb);
            Byte[] v_maskData = new Byte[v_maskBitmapData.Stride * v_maskBitmapData.Height];
            Marshal.Copy(v_maskBitmapData.Scan0, v_maskData, 0, v_maskData.Length);
            //get only black color and apply operation to this
            //CoreSystem.Instance.JobProgress(0.25f);
            int r, g, b;
            for (int i = 0; i < v_maskData.Length; ++i)
            {
                r = v_maskData[i];
                g = v_maskData[i + 1];
                b = v_maskData[i + 2];
                if ((r == g) && (g == b) && (b == 0))
                {
                    v_maskData[i] = dat[i];
                    v_maskData[i + 1] = dat[i + 1];
                    v_maskData[i + 2] = dat[i + 2];
                    v_maskData[i + 3] = dat[i + 3];
                }
                else
                {
                    v_maskData[i] = 0;
                    v_maskData[i + 1] = 0;
                    v_maskData[i + 2] = 0;
                    v_maskData[i + 3] = 0;
                }
                i = i + 3;
            }
            Marshal.Copy(v_maskData, 0, v_maskBitmapData.Scan0, v_maskData.Length);
            //unloack bit
            sm_odlBitmap.UnlockBits(data);
            v_maskbmp.UnlockBits(v_maskBitmapData);
            //apply bitmap
            Bitmap newBitmap = new Bitmap((int)rc.Width, (int)rc.Height);
            v_g = Graphics.FromImage(newBitmap);
            v_g.TranslateTransform(-rc.X, -rc.Y, enuMatrixOrder.Append);
            v_g.DrawImage(v_maskbmp, Point.Empty);
            v_g.Flush();
            v_g.Dispose();
            v_maskbmp.Dispose();
            sm_odlBitmap.Dispose();
            //CoreSystem.Instance.JobProgress(1.0f);
            //CoreSystem.Instance.JobComplete(DateTime.Now - v_time);
            return newBitmap;
        }
        new sealed class Mecanism : Core2DDrawingMecanismBase 
        {
            private Region m_region; //region to build for copy
            private bool m_drawRegion;
            private bool m_drawSelection;
            private enuCoreRegionOperation m_regionOperation;
            private enuCoreSelectionForm m_selectionForm;
            private List<Vector2f> m_points;
            Vector2f screenEnd;
            Vector2f screenStart;
            Graphics m_surfaceGraphics;
            private Rectanglef m_invalidZoomRectangle;
            public Mecanism() {
                m_points = new List<Vector2f>();
                this.m_selectionForm = enuCoreSelectionForm.Rectangle;
                this.m_regionOperation = enuCoreRegionOperation.Union;
            }
            public override void EndEdition()
            {
                this.m_drawRegion = false;
                this.m_drawSelection = false;
                base.EndEdition();
                this.CurrentSurface.Invalidate();
            }
            public override void Dispose()
            {
                DisposeRegion();
                DisposeGraphics();
                base.Dispose();   
            }
            private void DisposeGraphics()
            {
                if (this.m_surfaceGraphics != null)
                {
                    this.m_surfaceGraphics.Dispose();
                    this.m_surfaceGraphics = null;
                }
            }
            private void DisposeRegion()
            {
                if (this.m_region != null)
                {
                    this.m_region.Dispose();
                    this.m_region = null;
                }
            }
            protected override void RegisterSurface(IGK.DrSStudio.Drawing2D.WinUI.ICore2DDrawingSurface surface)
            {
                base.RegisterSurface(surface);
                if (this.m_surfaceGraphics != null)
                    this.m_surfaceGraphics.Dispose();
                this.m_surfaceGraphics = surface.CreateGraphics();
            }
            protected override void OnPaint(System.Windows.Forms.PaintEventArgs e)
            {
                base.OnPaint(e);
                if (this.m_drawRegion== false) 
                    return;
                GraphicsState s = e.Graphics.Save();
                Brush br = CoreBrushRegister.GetBrush(Color.FromArgb(25, Color.DarkBlue));
                if (this.m_region != null)
                {
                    e.Graphics.FillRegion(br, m_region);
                }
                if (this.m_drawSelection)
                {
                    //draw temp Selection
                    screenStart = CurrentSurface.GetScreenLocation(this.StartPoint);
                    screenEnd = CurrentSurface.GetScreenLocation(this.EndPoint);
                    Rectanglef rc = Rectanglef.Empty;
                    Pen v_pen = CoreBrushRegister.GetPen(Color.Black);
                    v_pen.DashStyle = DashStyle.Dash;
                    switch (m_selectionForm)
                    {
                        case enuCoreSelectionForm.Freehand:
                            GraphicsState v_bs = e.Graphics.Save();
                            e.Graphics.ResetTransform();
                            e.Graphics.ScaleTransform(CurrentSurface.ZoomX, CurrentSurface.ZoomY, enuMatrixOrder.Append);
                            e.Graphics.TranslateTransform(CurrentSurface.PosX, CurrentSurface.PosY, enuMatrixOrder.Append);
                            if (m_points.Count > 2)
                            {
                                e.Graphics.FillClosedCurve(br, m_points.ToArray());
                                e.Graphics.DrawClosedCurve(Pens.White, m_points.ToArray());
                                e.Graphics.DrawClosedCurve(v_pen, m_points.ToArray());
                            }
                            e.Graphics.Restore(v_bs);
                            break;
                        case enuCoreSelectionForm.Circle:
                            float radius = CoreMathOperation.GetDistance(screenEnd, screenStart);
                            Vector2f center = screenStart;
                            rc = CoreMathOperation.GetBounds(center, radius);
                            e.Graphics.FillEllipse(br, rc);
                            e.Graphics.DrawEllipse(Pens.White, rc.X, rc.Y, rc.Width, rc.Height);
                            e.Graphics.DrawEllipse(v_pen, rc.X, rc.Y, rc.Width, rc.Height);
                            break;
                        case enuCoreSelectionForm.Ellipse:
                            rc = CoreMathOperation.GetBounds(screenStart, screenEnd);
                            e.Graphics.FillEllipse(br, rc);
                            e.Graphics.DrawEllipse(Pens.White, rc.X, rc.Y, rc.Width, rc.Height);
                            e.Graphics.DrawEllipse(v_pen, rc.X, rc.Y, rc.Width, rc.Height);
                            break;
                        case enuCoreSelectionForm.Rectangle:
                            rc = CoreMathOperation.GetBounds(screenStart, screenEnd);
                            e.Graphics.FillRectangle(br, rc);
                            e.Graphics.DrawRectangle(Pens.White, rc.X, rc.Y, rc.Width, rc.Height);
                            e.Graphics.DrawRectangle(v_pen, rc.X, rc.Y, rc.Width, rc.Height);
                            break;
                    }
                    v_pen.DashStyle = DashStyle.Solid;
                }
                e.Graphics.Restore(s);
            }
            protected override void OnMouseDown(IGK.DrSStudio.Drawing2D.WinUI.CoreMouseEventArgs e)
            {
                switch (e.Button)
                {
                    case MouseButtons.Left:
                        State = ST_CONFIGURING;
                        BeginSelection(e);
                        break;
                }
            }
            protected override void OnMouseMove(IGK.DrSStudio.Drawing2D.WinUI.CoreMouseEventArgs e)
            {
                switch (e.Button)
                {
                    case MouseButtons.Left:
                        this.EndPoint = e.FactorPoint;
                        UpdateSelection(e);
                        break;
                }
            }
            private GraphicsPath GetGraphicsPath( Vector2f screenEnd, Vector2f screenStart)
            {
                GraphicsPath vp = new GraphicsPath();
                switch (m_selectionForm)
                {
                    case enuCoreSelectionForm.Freehand:
                        if (m_points.Count > 2)
                        {
                            Vector2f[] tb = new Vector2f[m_points.Count];
                            for (int i = 0; i < tb.Length; i++)
                            {
                                tb[i] = CurrentSurface.GetScreenLocation(m_points[i]);
                            }
                            vp.AddClosedCurve(tb);
                        }
                        else
                        {
                            vp.Dispose();
                            vp = null;
                        }
                        break;
                    case enuCoreSelectionForm.Circle:
                        float radius = CoreMathOperation.GetDistance(screenEnd, screenStart);
                        Vector2f center = screenStart;
                        Vector2f[] pts = new Vector2f[360];
                        float v_step = (float)(Math.PI / 180.0f);
                        for (int i = 0; i < 360; ++i)
                        {
                            pts[i] = new Vector2f(
                                (float)(center.X + radius * Math.Cos(i * v_step)),
                                (float)(center.Y + radius * Math.Sin(i * v_step))
                                );
                        }
                        vp.AddPolygon(pts);
                        break;
                    case enuCoreSelectionForm.Ellipse:
                        vp.AddEllipse(CoreMathOperation.GetBounds(screenStart, screenEnd));
                        break;
                    case enuCoreSelectionForm.Rectangle:
                        vp.AddRectangle(CoreMathOperation.GetBounds(screenStart, screenEnd));
                        break;
                }
                return vp;
            }
            protected override void OnMouseUp(IGK.DrSStudio.Drawing2D.WinUI.CoreMouseEventArgs e)
            {
                if (this.m_surfaceGraphics == null) return;
                switch (e.Button)
                {
                    case MouseButtons.Right:
                        //cancel selection
                        //end Selection
                        //--------------
                        if (this.m_region != null)
                        {
                            this.DisposeRegion();
                            this.m_drawRegion = false;
                            this.CurrentSurface.Invalidate();
                        }
                        break;
                    case MouseButtons.Left:
                        EndSelection(e);
                        //Matrix m = this.Element.GetMatrix().Clone () as Matrix ;
                        Vector2f[] vtb = new Vector2f[] { this.StartPoint, this.EndPoint };
                        screenEnd =  CurrentSurface.GetScreenLocation(vtb[1]);
                        screenStart = CurrentSurface.GetScreenLocation(vtb[0]);
                        GraphicsPath vp = GetGraphicsPath(screenEnd, screenStart);
                        this.m_drawRegion = false;
                        if ((vp != null)&&(vp.PointCount > 0))
                        {
                            if (m_region == null)
                            {
                                m_region = new Region(vp);                                
                                //copy drawing image                                
                            }
                            else
                            {
                                BuildRegion(this.m_region, this.m_regionOperation, vp);
                            }
                            vp.Dispose();
                            State = ST_EDITING;
                            this.m_drawRegion = true;
                            this.m_drawSelection = false;
                            this.CurrentSurface.Invalidate();
                        }
                         break;
                }
            }
            private static void BuildRegion(Region rg, enuCoreRegionOperation rgOperation, GraphicsPath vp)
            {
                if (rg == null) return;
                switch (rgOperation)
                {
                    case enuCoreRegionOperation.Union:
                        rg.Union(vp);
                        break;
                    case enuCoreRegionOperation.Intersect:
                        rg.Intersect(vp);
                        break;
                    case enuCoreRegionOperation.Xor:
                        rg.Xor(vp);
                        break;
                    case enuCoreRegionOperation.Exclude:
                        rg.Exclude(vp);
                        break;
                    case enuCoreRegionOperation.Complement:
                        rg.Complement(vp);
                        break;
                }
            }
            void BeginSelection(CoreMouseEventArgs e)
            {
                m_points.Clear();
                this.StartPoint = e.FactorPoint;
                this.EndPoint = e.FactorPoint;
                this.m_drawSelection = true;
                if (this.m_selectionForm == enuCoreSelectionForm.Freehand)
                {
                    m_points.Add(e.FactorPoint);
                }
                this.UpdateSelection(e);
            }
            void EndSelection(CoreMouseEventArgs e)
            {
                this.UpdateSelection(e);
            }
            void UpdateSelection(CoreMouseEventArgs e)
            {
                this.m_drawRegion = false;
               // this.CurrentSurface.InvalidZoomRectangle(m_invalidZoomRectangle, 1.0f);
                switch (m_selectionForm)
                {
                    case enuCoreSelectionForm.Rectangle:
                    case enuCoreSelectionForm.Ellipse:
                        this.EndPoint = e.FactorPoint;
                        this.m_invalidZoomRectangle = CoreMathOperation.GetBounds(this.StartPoint, this.EndPoint);
                        break;
                    case enuCoreSelectionForm.Freehand:
                        if (this.m_points.Count >= 2)
                            this.CurrentSurface.Invalidate(Rectangle.Round (CoreMathOperation.GetBounds(this.m_points.ToArray())));
                        Rectanglef rc = new Rectanglef(
                            this.m_points[this.m_points.Count - 1],
                            SizeF.Empty);
                        rc.Inflate(1, 1);
                        if (!rc.Contains(e.FactorPoint ))
                        {
                            m_points.Add(e.FactorPoint);
                        }
                        this.EndPoint = e.FactorPoint;
                        if (this.m_points.Count >= 2)
                        {
                            this.m_invalidZoomRectangle = CoreMathOperation.GetBounds(this.m_points.ToArray());
                        }
                        break;
                    case enuCoreSelectionForm.Circle:
                        Vector2f c = this.StartPoint;
                        float radius = CoreMathOperation.GetDistance(this.EndPoint, this.StartPoint);
                        this.EndPoint = e.FactorPoint;
                        this.m_drawRegion = true;
                        radius = CoreMathOperation.GetDistance(this.EndPoint, this.StartPoint);
                        this.m_invalidZoomRectangle = CoreMathOperation.GetBounds(c, radius);
                        break;
                }
                this.m_drawRegion = true;
                this.CurrentSurface.Invalidate ();//.InvalidZoomRectangle(m_invalidZoomRectangle, 1.0f);
            }
            protected override void OnKeyUp(System.Windows.Forms.KeyEventArgs e)
            {              
                switch (e.KeyCode)
                {
                    case Keys.Escape :
                        if (m_region != null)
                        {
                            e.Handled = true;
                            Graphics g = this.CurrentSurface.CreateGraphics();
                            Rectanglef v_rc = m_region.GetBounds(g);
                            g.Dispose();
                            m_region.Dispose();
                            m_region = null;
                            this.CurrentSurface.Invalidate(Rectangle.Round (v_rc));
                        }
                        else {
                            //
                            //go to default mecanism type
                            //
                            if (this.GetType() != this.CurrentSurface.DefaultTool )
                            {
                                //catch the handle of the suface                                
                                this.EndEdition();
                                this.CurrentSurface.CurrentTool = this.CurrentSurface.DefaultTool;
                                e.Handled = true;
                            }
                        }
                        break;
                    case System.Windows.Forms.Keys.Enter:
                        //validate selection
                        if (m_region != null)
                        {
                            if (this.IsShiftKey)
                            {
                                CutSelectionAsImage();
                            }
                            else
                                CopySelectionAsImage();
                            e.Handled = true;
                        }
                        break;
                    case Keys.R:
                        this.m_selectionForm = enuCoreSelectionForm.Rectangle;
                        e.Handled = true;
                        break;
                    case Keys.E:
                        this.m_selectionForm = enuCoreSelectionForm.Ellipse;
                        e.Handled = true;
                        break;
                    case Keys.C:
                        this.m_selectionForm = enuCoreSelectionForm.Circle;
                        e.Handled = true;
                        break;
                    case Keys.F:
                        this.m_selectionForm = enuCoreSelectionForm.Freehand;
                        e.Handled = true;
                        break;
                }
                base.OnKeyUp(e);
            }
            private void CopySelectionAsImage()
            {
                //region is a scree region so need to transform to the bitmap region
                Matrix m = new Matrix();
                m.Scale(this.CurrentSurface.ZoomX, this.CurrentSurface.ZoomY, enuMatrixOrder.Append);
                m.Translate(this.CurrentSurface.PosX, this.CurrentSurface.PosY, enuMatrixOrder.Append);
                m.Invert();
                Rectanglef rc = this.m_region.GetBounds(this.m_surfaceGraphics);
                rc = CoreMathOperation.ApplyMatrix(rc, m);
                Bitmap bmp = new Bitmap((int)rc.Width, (int)rc.Height, PixelFormat.Format32bppPArgb);
                Graphics g = Graphics.FromImage(bmp);
                g.TranslateTransform(-rc.X, -rc.Y, enuMatrixOrder.Append);              
                this.CurrentSurface.CurrentDocument.Draw(g);
                g.Flush();
                g.Dispose();
                m.Translate(-rc.X, -rc.Y, enuMatrixOrder.Append);
                m_region.Transform(m);
                m.Dispose();
                ImageElement img = ImageElement.FromImage(CopyRegionSelection(bmp, m_region));
                bmp.Dispose();
                if (img != null)
                {
                    img.Translate(rc.X, rc.Y, enuMatrixOrder.Append);
                    this.DisposeRegion();
                    this.CurrentSurface.CurrentLayer.Elements.Add(img);
                    this.CurrentSurface.Invalidate(img);
                }
            }
            private void CutSelectionAsImage()
            {
            }
        }
    }
}

