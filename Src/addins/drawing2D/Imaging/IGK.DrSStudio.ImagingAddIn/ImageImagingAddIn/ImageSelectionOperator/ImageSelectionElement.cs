

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: ImageSelectionElement.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
using IGK.ICore.WinCore;
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
/*
IGKDEV - [2008 - 2014]
author: C.A.D . BONDJE DOUE
file:ImageSelectionElement.cs
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
﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing;
using System.Xml;
using System.Runtime.InteropServices ;
namespace IGK.DrSStudio.Drawing2D.ImageSelection
{
    using IGK.DrSStudio.Drawing2D.ContextMenu;
    using IGK.DrSStudio.Drawing2D.ImageSelection.ContextMenu;
    using IGK.DrSStudio.ContextMenu;
    using Tools;
    using IGK.DrSStudio.WinUI.Configuration;
using IGK.DrSStudio.Imaging;
    using IGK.DrSStudio.MecanismActions;
    /// <summary>
    /// Represent la selction sur image
    /// </summary>
    [Core2DDrawingImageItemAttribute("ImageSelection",
        typeof(Mecanism),
        ImageKey ="DE_OPE")]
    public sealed class ImageSelectionElement : RectangleElement  
    {
        static Bitmap sm_odlBitmap;
        static Bitmap sm_tempBitmap;
        Mecanism m_mecanism;
        public const int SOP_INVERT = 0;
        public const int SOP_MAKETRANSPARENT = 1;
        public const int SOP_R2G = 2;
        public const int SOP_R2B = 3;
        public const int SOP_G2B = 4;
        public const int SOP_REPLACE = 5;
        public const int SOP_ADD = 6;
        public const int SOP_ADDWAlpha = 10;
        public const int SOP_SUBSRC = 7;
        public const int SOP_SUBSRCWAlpha = 11;
        public const int SOP_SUBDEST = 8;
        public const int SOP_SUBDESTWAlpha = 12;
        public const string CONTEXTMENU_COPYSELECTION = "ImageSelection.CopySelection";
        public const string CONTEXTMENU_SETSELECTIONMODE = "ImageSelection.SetSelectionMode";
        public const string CONTEXTMENU_SELECTREGION = "ImageSelection.SelectRegion";
        internal ImageSelectionElement(Mecanism mecanism)
        {
            this.m_mecanism = mecanism;
        }
        protected override void InitGraphicPath(CoreGraphicsPath p)
        {
            base.InitGraphicPath(p);
        }
        static void ApplyRegionOperation(ImageElement image, Region region, int operation, bool temp)
        {
            if (sm_tempBitmap == null) return;
            //clone old image
            sm_odlBitmap = sm_tempBitmap.Clone() as Bitmap;
            BitmapData data = sm_odlBitmap.LockBits(new Rectangle(Point.Empty, sm_odlBitmap.Size),
                ImageLockMode.ReadWrite, PixelFormat.Format32bppPArgb);
            Byte[] dat = new byte[data.Stride * data.Height];
            Marshal.Copy(data.Scan0, dat, 0, dat.Length);
            Bitmap v_maskbmp = new Bitmap(image.Width, image.Height, PixelFormat.Format32bppPArgb);
            Graphics v_g = Graphics.FromImage(v_maskbmp);
            v_g.InterpolationMode = InterpolationMode.NearestNeighbor;
            v_g.PixelOffsetMode = PixelOffsetMode.HighSpeed;
            v_g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.Default;
            v_g.Clear(Color.White);
            v_g.FillRegion(Brushes.Black, region); 
            v_g.Flush();
            v_g.Dispose();
            BitmapData v_maskBitmapData = v_maskbmp.LockBits(new Rectangle(Point.Empty, sm_odlBitmap.Size),
                ImageLockMode.ReadWrite, PixelFormat.Format32bppPArgb);
            Byte[] v_maskData = new Byte[v_maskBitmapData.Stride * v_maskBitmapData.Height];
            Marshal.Copy(v_maskBitmapData.Scan0, v_maskData, 0, v_maskData.Length);
            //get only black color and apply opertion to this
            int r, g, b;
            for (int i = 0; i < v_maskData.Length; ++i)
            {
                r = v_maskData[i];
                g = v_maskData[i + 1];
                b = v_maskData[i + 2];
                if ((r == g) && (g == b) && (b == 0))
                {
                    switch (operation)
                    {
                        case SOP_INVERT: //invert Color
                            dat[i] = (byte)(255 - dat[i]);
                            dat[i + 1] = (byte)(255 - dat[i + 1]);
                            dat[i + 2] = (byte)(255 - dat[i + 2]);
                            break;
                        case SOP_MAKETRANSPARENT: //make transparent:
                            dat[i + 3] = 0;
                            break;
                        case SOP_R2G:
                            r = dat[i];
                            g = dat[i + 1];
                            b = dat[i + 2];
                            dat[i] = (byte)g;
                            dat[i + 1] = (byte)r;
                            dat[i + 2] = (byte)b;
                            break;
                        case SOP_R2B:
                            r = dat[i];
                            g = dat[i + 1];
                            b = dat[i + 2];
                            dat[i] = (byte)b;
                            dat[i + 1] = (byte)g;
                            dat[i + 2] = (byte)r;
                            break;
                        case SOP_G2B:
                            r = dat[i];
                            g = dat[i + 1];
                            b = dat[i + 2];
                            dat[i] = (byte)r;
                            dat[i + 1] = (byte)b;
                            dat[i + 2] = (byte)g;
                            break;
                    }
                }
                i = i + 3;
            }
            Marshal.Copy(dat, 0, data.Scan0, dat.Length);
            //unloack bit
            sm_odlBitmap.UnlockBits(data);
            v_maskbmp.UnlockBits(v_maskBitmapData);
            v_maskbmp.Dispose();
            //apply bitmap
            image.SetBitmap(sm_odlBitmap, temp);
        }
        static void ApplyRegionOperation(ImageElement image, Brush br, Region region, int operation, bool temp)
        {
            if (sm_tempBitmap == null) return;
            //clone old image
            sm_odlBitmap = sm_tempBitmap.Clone() as Bitmap;
            BitmapData data = sm_odlBitmap.LockBits(new Rectangle(Point.Empty, sm_odlBitmap.Size),
                ImageLockMode.ReadWrite, PixelFormat.Format32bppPArgb);
            Byte[] dat = new byte[data.Stride * data.Height];
            Marshal.Copy(data.Scan0, dat, 0, dat.Length);
            Bitmap v_maskbmp = new Bitmap(image.Width, image.Height, PixelFormat.Format32bppPArgb);
            Bitmap v_maskbmp2 = new Bitmap(image.Width, image.Height, PixelFormat.Format32bppPArgb);
            Graphics v_g = Graphics.FromImage(v_maskbmp);
            v_g.InterpolationMode = InterpolationMode.NearestNeighbor;
            v_g.PixelOffsetMode = PixelOffsetMode.HighSpeed;
            v_g.SmoothingMode = System.Drawing.Drawing2D .SmoothingMode.HighQuality;
            v_g.Clear(Color.White);
            v_g.FillRegion(Brushes.Black, region);
            v_g.Flush();
            v_g.Dispose();
            v_g = Graphics.FromImage(v_maskbmp2);
            v_g.InterpolationMode = InterpolationMode.NearestNeighbor;
            v_g.PixelOffsetMode = PixelOffsetMode.HighSpeed;
            v_g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            v_g.Clear(Color.White);
            v_g.CompositingMode = System.Drawing.Drawing2D.CompositingMode.SourceCopy;
            v_g.FillRegion(br, region);
            v_g.Flush();
            v_g.Dispose();
            Rectangle rc = new Rectangle(Point.Empty, sm_odlBitmap.Size);
            BitmapData v_maskBitmapData = v_maskbmp.LockBits(rc,
                ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb );
            BitmapData v_maskBitmapData2 = v_maskbmp2.LockBits(rc,
                ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
            Byte[] v_maskData = new Byte[v_maskBitmapData.Stride * v_maskBitmapData.Height];
            Marshal.Copy(v_maskBitmapData.Scan0, v_maskData, 0, v_maskData.Length);
            //filled brush
            Byte[] v_maskData2 = new Byte[v_maskBitmapData2.Stride * v_maskBitmapData2.Height];
            Marshal.Copy(v_maskBitmapData2.Scan0, v_maskData2, 0, v_maskData2.Length);
            //get only black color and apply opertion to this
            int r, g, b;
            for (int i = 0; i < v_maskData.Length; i += 4)
            {
                r = v_maskData[i];
                g = v_maskData[i + 1];
                b = v_maskData[i + 2];
                if ((r == g) && (g == b) && (b == 0))
                {
                    switch (operation)
                    {
                        case SOP_ADD:
                        case SOP_ADDWAlpha :
                            r = dat[i] + v_maskData2[i];
                            g = dat[i + 1] + v_maskData2[i + 1];
                            b = dat[i + 2] + v_maskData2[i + 2];
                            dat[i] = (byte)(r < 0 ? 0 : r > 255 ? 255 : r);
                            dat[i + 1] = (byte)(g < 0 ? 0 : g > 255 ? 255 : g);
                            dat[i + 2] = (byte)(b < 0 ? 0 : b > 255 ? 255 : b);
                            if (operation == SOP_ADDWAlpha)
                            {
                                dat[i + 3] = v_maskData2[i + 3];
                            }
                            break;
                        case SOP_SUBSRC:
                        case SOP_SUBSRCWAlpha:
                            r = dat[i] - v_maskData2[i];
                            g = dat[i + 1] - v_maskData2[i + 1];
                            b = dat[i + 2] - v_maskData2[i + 2];
                            dat[i] = (byte)((r < 0) ? 0 : (r > 255) ? 255 : r);
                            dat[i + 1] = (byte)((g < 0) ? 0 : (g > 255) ? 255 : g);
                            dat[i + 2] = (byte)((b < 0) ? 0 : (b > 255) ? 255 : b);
                            if (operation == SOP_SUBSRCWAlpha)
                            {
                                dat[i + 3] = v_maskData2[i + 3];
                            }
                            break;
                        case SOP_SUBDEST:
                        case SOP_SUBDESTWAlpha :
                            r = v_maskData2[i] - dat[i];
                            g = v_maskData2[i + 1] - dat[i + 1];
                            b = v_maskData2[i + 2] - dat[i + 2];
                            dat[i] = (byte)(r < 0 ? 0 : r > 255 ? 255 : r);
                            dat[i + 1] = (byte)(g < 0 ? 0 : g > 255 ? 255 : g);
                            dat[i + 2] = (byte)(b < 0 ? 0 : b > 255 ? 255 : b);
                            if (operation == SOP_SUBDESTWAlpha)
                            {
                                dat[i + 3] = v_maskData2[i + 3];
                            }
                            break;
                        case SOP_REPLACE:
                            dat[i] = v_maskData2[i];
                            dat[i + 1] = v_maskData2[i + 1];
                            dat[i + 2] = v_maskData2[i + 2];
                            break;
                    }
                }
            }
            Marshal.Copy(dat, 0, data.Scan0, dat.Length);
            //unloack bit
            sm_odlBitmap.UnlockBits(data);
            v_maskbmp.UnlockBits(v_maskBitmapData);
            v_maskbmp2.UnlockBits(v_maskBitmapData2);
            v_maskbmp.Dispose();
            v_maskbmp2.Dispose();
            //apply bitmap
            image.SetBitmap(sm_odlBitmap, temp);
        }
        static Bitmap CopyRegionSelection(ImageElement image, Region region)
        {
            //clone old image
            sm_odlBitmap = sm_tempBitmap.Clone() as Bitmap;
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
            RectangleF rc = region.GetBounds(v_g);
            v_g.Flush();
            v_g.Dispose();
            BitmapData v_maskBitmapData = v_maskbmp.LockBits(new Rectangle(Point.Empty, sm_odlBitmap.Size),
                ImageLockMode.ReadWrite, PixelFormat.Format32bppPArgb);
            Byte[] v_maskData = new Byte[v_maskBitmapData.Stride * v_maskBitmapData.Height];
            Marshal.Copy(v_maskBitmapData.Scan0, v_maskData, 0, v_maskData.Length);
            //get only black color and apply opertion to this
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
            v_g.TranslateTransform(-rc.X, -rc.Y, MatrixOrder.Append);
            v_g.DrawImage(v_maskbmp, Point.Empty);
            v_g.Flush();
            v_g.Dispose();
            v_maskbmp.Dispose();
            sm_odlBitmap.Dispose();
            return newBitmap;
        }
        protected override void OnPropertyChanged(CoreWorkingObjectPropertyChangedEventArgs e)
        {
            if (( (IGK.DrSStudio.Drawing2D.enu2DPropertyChangedType) e.ID ) 
                == enu2DPropertyChangedType.BrushChanged )
            {
                 this.m_mecanism.UpdateSelectionProperty();
            }
            base.OnPropertyChanged(e);
        }
        /// <summary>
        /// represent the mecanism associate to this selection
        /// </summary>
        public new sealed class Mecanism :
            IGK.DrSStudio.Drawing2D.Mecanism.Core2DDrawingRectangleMecanismBase<ImageSelectionElement>,
            ICoreWorkingConfigurableObject 
            //,ICoreDrawingImageOperation
        {
            private enuCoreSelectionForm m_selectionForm;
            //private enuCoreRegionOperation m_regionOperation;
            private bool m_draw;
            private List<Vector2f> m_points;
            private Region m_region;
            private RectangleF m_invalidZoomRectangle;
            private Timer m_tim;
            private ImageSelectionElement m_imgSelection;
            //3 allowed context menu
            private _ImageSelectionContextMenuBase m_menuCopySelection;
            private _ImageSelectionContextMenuBase m_menuRegionSelection;
            private _ImageSelectionContextMenuBase m_menuSetSelection;
            /// <summary>
            /// get or set the region operation
            /// </summary>
            public enuCoreRegionOperation RegionOperation
            {
                get { return Tools.SelectionRegionOperationTools .Instance.RegionOperation ; }
                set
                {
                    Tools.SelectionRegionOperationTools.Instance.RegionOperation = value;
                }
            }
            public enuSelectionOperationType RegionOperationType {
                get {
                    return Tools.SelectionRegionOperationTools.Instance.RegionSelectionOperationType;
                }
                set {
                    Tools.SelectionRegionOperationTools.Instance.RegionSelectionOperationType =value;
                }
            }
            public enuCoreSelectionForm SelectionForm
            {
                get { return m_selectionForm; }
                set
                {
                    if (m_selectionForm != value)
                    {
                        m_selectionForm = value;
                    }
                }
            }
            protected override void GenerateActions()
            {
                base.GenerateActions();
                //repalace action
                this.Actions[Keys.Control | Keys.E] = new EditMecanismProperties(); 
            }
            protected override void RegisterDocumentEvent(ICore2DDrawingDocument document)
            {
                base.RegisterDocumentEvent(document);
                document.CurrentLayerChanged += new Core2DDrawingLayerChangedEventHandler(document_CurrentLayerChanged);
            }
            protected override void UnregisterDocumentEvent(ICore2DDrawingDocument document)
            {
                document.CurrentLayerChanged -= new Core2DDrawingLayerChangedEventHandler(document_CurrentLayerChanged);
                base.UnregisterDocumentEvent(document);
            }
            void document_CurrentLayerChanged(object sender, Core2DDrawingLayerChangedEventArgs  e)
            {
                this.Reset();
            }
            private void Reset()
            {
                this.DisposeRegion();
                this.DisposeTempBitmap();
            }
            public override void EndEdition()
            {
                if (this.Element != null)
                {
                    if (this.m_region != null)
                    {
                        this.ApplySelectOperation(false);
                        this.DisposeRegion();
                    }
                    this.Element.Invalidate(true);
                    if (this.Element != null)
                    {
                        this.Element.PropertyChanged -= 
                            (imgElement_PropertyChanged);
                    }
                    base.EndEdition();
                }
                else {
                    if (this.m_region != null)
                    {
                        this.DisposeRegion();
                    }
                    this.CurrentSurface.Invalidate();
                    base.EndEdition();
                }
            }
            public override void Dispose()
            {
                this.DisposeRegion();
                if (m_points != null)
                {
                    m_points.Clear();
                }
                //dispose 
                if (m_imgSelection != null)
                {
                    m_imgSelection.Dispose();
                    m_imgSelection = null;
                }
                //dispose timer
                if (this.m_tim != null)
                {
                    this.m_tim.Dispose();
                    this.m_tim = null;
                }
                DisposeTempBitmap();
                base.Dispose();
            }
            private void DisposeTempBitmap()
            {
                if (sm_tempBitmap != null)
                {
                    sm_tempBitmap.Dispose();
                    sm_tempBitmap = null;
                }
            }
            public Mecanism()
            {
                m_points = new List<PointF>();
                m_selectionForm = enuCoreSelectionForm.Rectangle;
            }
            //protected override string GetHelpMessage()
            //{
            //    return "Image Selection: Keys: A, R, C, E, F, T, Enter to validate Region, Escape to cancel region; " +
            //        string.Format("Mode = " + m_selectionForm + "; RegionMode=" +
            //        this.RegionOperation  + "; SelectionOperationType=" + 
            //        this.RegionOperationType );
            //}
            public override bool Register(ICore2DDrawingSurface t)
            {
                bool v =  base.Register(t);
                
                m_menuCopySelection = CoreSystem.GetAction (CONTEXTMENU_COPYSELECTION)
                    as _ImageSelectionContextMenuBase;
                m_menuCopySelection.Mecanism = this;
                m_menuSetSelection = CoreSystem.GetAction (CONTEXTMENU_SETSELECTIONMODE) as _ImageSelectionContextMenuBase;
                m_menuSetSelection.Mecanism = this;
                m_menuRegionSelection = CoreSystem.GetAction (CONTEXTMENU_SELECTREGION) as _ImageSelectionContextMenuBase;
                m_menuRegionSelection.Mecanism = this;
                Tools.SelectionRegionOperationTools.Instance.RegionOperationChanged += new EventHandler(Instance_RegionOperationChanged);
                Tools.SelectionRegionOperationTools.Instance.RegionSelectionOperationTypeChanged += new EventHandler(Instance_RegionSelectionOperationTypeChanged);
                return v;
            }
            public override bool UnRegister()
            {
                
                Tools.SelectionRegionOperationTools.Instance.RegionOperationChanged -= new EventHandler(Instance_RegionOperationChanged);
                Tools.SelectionRegionOperationTools.Instance.RegionSelectionOperationTypeChanged -= new EventHandler(Instance_RegionSelectionOperationTypeChanged);
                if (m_menuCopySelection != null)
                    m_menuCopySelection.Mecanism = null;
                if (m_menuRegionSelection != null)
                    m_menuRegionSelection.Mecanism = null;
                if (m_menuSetSelection != null)
                    m_menuSetSelection.Mecanism = null;
                return base.UnRegister();
            }
            void Instance_RegionSelectionOperationTypeChanged(object sender, EventArgs e)
            {
                this.ApplySelectOperation(true);
            }
            void Instance_RegionOperationChanged(object sender, EventArgs e)
            {
                this.ApplySelectOperation(true);
            }
            public void SetElementToConfigure(ICoreWorkingObject  element)
            {
                if (element is ImageElement)
                {
                    ImageElement imgElement = element as ImageElement;
                    sm_tempBitmap = imgElement.Bitmap.Clone() as Bitmap;
                    base.Element = imgElement ;
                    this.CurrentSurface.CurrentLayer.Select(this.Element);                    
                    m_imgSelection = new ImageSelectionElement(this);
                    m_imgSelection.Bound = new IGK.Rectanglef(0, 0,
                        imgElement.Width, imgElement.Height);
                    m_imgSelection.InitElement();
                    m_imgSelection.Transform(imgElement.GetMatrix().Clone() as Matrix);
                    //for threading in operation
                    this.m_tim = new Timer();
                    this.m_tim.Enabled = false;
                    this.m_tim.Interval = 1000;
                    this.m_tim.Tick += new EventHandler(m_tim_Tick);
                    //property change for element
                    imgElement.PropertyChanged += imgElement_PropertyChanged;
                    this.CurrentSurface.ElementToConfigure = m_imgSelection;    
                }
            }
            void m_tim_Tick(object sender, EventArgs e)
            {
                this.ApplySelectOperation(true);
                m_tim.Enabled = false;
            }
            void imgElement_PropertyChanged(object o,CoreWorkingObjectPropertyChangedEventArgs  e)
            {
                if ((enu2DPropertyChangedType)e.ID == enu2DPropertyChangedType.BitmapChanged )
                {
                    DisposeTempBitmap();
                    sm_tempBitmap = (o as ImageElement).Bitmap.Clone() as Bitmap;
                }
            }
            internal void UpdateSelectionProperty()
            {                
                    if (this.m_tim != null)
                        this.m_tim.Enabled = true;                
            }
            private void ApplySelectOperation(bool temp)
            {
                if ((this.m_imgSelection!=null)&&(m_region != null) && (this.Element is ImageElement))
                {
                    Region v_region = m_region.Clone() as Region;
                    Matrix m = new Matrix();
                    Matrix m2 = this.Element.GetMatrix().Clone() as Matrix;
                    if (!m2.IsIdentity && m2.IsInvertible)
                    {
                        m2.Invert();
                        m.Multiply(m2, MatrixOrder.Append);
                    }
                    v_region.Transform(m);
                    m.Dispose();
                    m2.Dispose();
                    switch ((int)this.RegionOperationType )
                    {
                        case SOP_MAKETRANSPARENT:
                        case SOP_INVERT:
                        case SOP_G2B:
                        case SOP_R2G:
                        case SOP_R2B:
                            ApplyRegionOperation(this.Element as ImageElement, v_region, 
                                (int)this.RegionOperationType, temp);
                            break;
                        case SOP_REPLACE:
                        case SOP_ADD:
                        case SOP_SUBSRC:
                        case SOP_SUBDEST:
                        case SOP_SUBDESTWAlpha :
                        case SOP_SUBSRCWAlpha :
                        case SOP_ADDWAlpha :
                            Brush br = this.m_imgSelection.FillBrush.GetBrush();
                            ApplyRegionOperation(this.Element as ImageElement, 
                                br, 
                                v_region,
                                (int)this.RegionOperationType, 
                                temp);
                            break;
                    }
                    v_region.Dispose();
                    CurrentSurface.Invalidate(this.Element);
                    if (temp == false)
                    {
                        if (sm_tempBitmap != null)
                        {
                            sm_tempBitmap.Dispose();
                            sm_tempBitmap = null;
                        }
                        sm_tempBitmap = (this.Element as ImageElement).Bitmap.Clone() as Bitmap;
                    }
                }
            }
            protected override void OnMouseDown(CoreMouseEventArgs e)
            {
                switch (e.Button)
                {
                    case MouseButtons.Left:
                        this.StartPoint = e.FactorPoint;
                        this.EndPoint = e.FactorPoint;
                        if (this.Element == null)
                        {
                            //check first image element
                            ICore2DDrawingLayer l = this.CurrentSurface.CurrentLayer ;
                            for (int i = l.Elements.Count - 1; i >= 0; --i)
                            {
                                if (l.Elements[i].Contains (e.FactorPoint )&& (l.Elements[i] is ImageElement))
                                {
                                    this.Element = l.Elements[i] as ImageElement;
                                    //selected image element
                                    l.Select(this.Element);
                                    this.SetElementToConfigure(this.Element as Core2DDrawingLayeredElement );
                                    OnMouseDown(e);
                                    return;
                                }
                            }
                        }
                        else
                        {
                            State = ST_CONFIGURING;
                            BeginSelection(e);
                        }
                        break;
                    case MouseButtons .Right :
                        this.AllowContextMenu = false;
                        break;
                }
            }
            protected override void OnMouseMove(CoreMouseEventArgs e)
            {
                switch (e.Button)
                {
                    case MouseButtons.Left:
                        UpdateSelection(e);
                        break;
                }
            }
            protected override void OnMouseUp(CoreMouseEventArgs e)
            {
                switch (e.Button)
                {
                    case MouseButtons.Right:
                        //end Selection
                        if (this.m_region != null)
                        {
                            this.CurrentSurface.Invalidate();
                        }
                        break;
                    case MouseButtons.Left:
                        if (this.Element == null)
                        {
                            //check for selected element
                            ICore2DDrawingLayer l = this.CurrentSurface.CurrentLayer;
                            for (int i = 0; i < this.CurrentSurface.CurrentLayer.Elements.Count; i++)
                            {
                                if ((l.Elements[i] is ImageElement) && (l.Elements[i].Contains(e.FactorPoint)))
                                {
                                    this.Element = l.Elements[i] as ImageElement;
                                    l.Select(this.Element);
                                    this.SetElementToConfigure(Element);
                                }
                            }
                            return;
                        }
                        EndSelection(e);
                        if (this.Element == null)
                            return;
                        //Matrix m = this.Element.GetMatrix().Clone () as Matrix ;
                        PointF[] vtb = new PointF[] { this.StartPoint, this.EndPoint };
                        PointF screenEnd = this.EndPoint;// Surface.GetScreenLocation(vtb[1]);
                        PointF screenStart = this.StartPoint;// Surface.GetScreenLocation(vtb[0]);
                        GraphicsPath vp = GetGraphicsPath(screenEnd, screenStart);
                        if ((vp != null) && (vp.PointCount > 0))
                        {
                            if (m_region == null)
                            {
                                m_region = new Region(vp);
                                if (sm_tempBitmap != null)
                                {
                                    sm_tempBitmap.Dispose();
                                    sm_tempBitmap = null;
                                }
                                //copy drawing image
                                sm_tempBitmap = (this.Element as ImageElement).Bitmap.Clone() as Bitmap;
                            }
                            else
                            {
                                BuildRegion(m_region,this.RegionOperation , vp);
                            }
                            vp.Dispose();
                        }
                        ApplySelectOperation(true);
                        State = ST_EDITING;
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
            private GraphicsPath GetGraphicsPath(Vector2f screenEnd, Vector2f screenStart)
            {
                GraphicsPath vp = new GraphicsPath();
                switch (m_selectionForm)
                {
                    case enuCoreSelectionForm.Freehand:
                        if (m_points.Count > 2)
                        {
                            vp.AddClosedCurve(m_points.ToArray());
                        }
                        else
                        {
                            vp.Dispose();
                            vp = null;
                        }
                        break;
                    case enuCoreSelectionForm.Circle:
                        float radius = CoreMathOperation.GetDistance(screenEnd, screenStart);
                        PointF center = screenStart;
                        PointF[] pts = new PointF[360];
                        float v_step = (float)(Math.PI / 180.0f);
                        for (int i = 0; i < 360; ++i)
                        {
                            pts[i] = new PointF(
                                (float)(center.X + radius * Math.Cos(i * v_step)),
                                (float)(center.Y + radius * Math.Sin(i * v_step))
                                );
                        }
                        vp.AddPolygon(pts);//.AddEllipse(CoreMathOperation.GetBounds(center, radius));
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
            private void DisposeRegion()
            {
                if (this.m_region != null)
                {
                    this.m_region.Dispose();
                    this.m_region = null;
                    this.CurrentSurface.Invalidate();
                }
            }
            //protected override void OnKeyPress(KeyPressEventArgs e)
            //{
            //    Keys c = (Keys )char.ToUpper (e.KeyChar );
            //    switch (c)
            //    {
            //        case Keys.T:
            //            if (IsControlKey == false)
            //            {
            //                //select all image region
            //                this.DisposeRegion();
            //                RectangleF rc = new RectangleF(Point.Empty, (this.Element as ImageElement).Bitmap.Size);
            //                rc = CoreMathOperation.ApplyMatrix(rc, this.Element.GetMatrix());
            //                this.m_region = new Region(rc);
            //                this.ApplySelectOperation(true);
            //                e.Handled = true;
            //            }
            //            break;
            //        case Keys.U:
            //            if (IsControlKey  == false)
            //            {
            //                //unselect all image region
            //                this.DisposeRegion();
            //                this.ApplySelectOperation(true);
            //                e.Handled = true;
            //            }
            //            break;
            //        case Keys.A: //add all image region
            //            if (IsControlKey == false)
            //            {
            //                if (this.m_region == null)
            //                {
            //                    goto case Keys.T;
            //                }
            //                RectangleF rc = new RectangleF(Point.Empty, (this.Element as ImageElement).Bitmap.Size);
            //                rc = CoreMathOperation.ApplyMatrix(rc, this.Element.GetMatrix());
            //                GraphicsPath vp = new GraphicsPath();
            //                vp.AddRectangle(rc);
            //                BuildRegion(m_region, RegionOperation, vp);
            //                vp.Dispose();
            //                this.ApplySelectOperation(true);
            //                e.Handled = true;
            //            }
            //            break;
            //        case Keys.R:
            //            this.m_selectionForm = enuCoreSelectionForm.Rectangle;
            //            e.Handled = true;
            //            break;
            //        case Keys.E:
            //            this.m_selectionForm = enuCoreSelectionForm.Ellipse;
            //            e.Handled = true;
            //            break;
            //        case Keys.C:
            //            this.m_selectionForm = enuCoreSelectionForm.Circle;
            //            e.Handled = true;
            //            break;
            //        case Keys.F:
            //            this.m_selectionForm = enuCoreSelectionForm.Freehand;
            //            e.Handled = true;
            //            break;
            //    }
            //    if (e.Handled)
            //        this.SendHelpMessage();
            //    base.OnKeyPress(e);
            //}
            //protected override void OnKeyUp(KeyEventArgs e)
            //{
            //    switch (e.KeyCode)
            //    {
            //        case Keys.Escape:
            //            if (this.State == ST_EDITING)
            //            {
            //                if ((sm_tempBitmap != null) && (this.Element != null))
            //                {
            //                    (this.Element as ImageElement).SetBitmap(sm_tempBitmap.Clone() as Bitmap, false);
            //                    DisposeRegion();
            //                    this.m_draw = false;
            //                    this.CurrentSurface.Invalidate(this.Element);
            //                }
            //                e.Handled = true;
            //                this.State = ST_NONE;
            //            }
            //            break;
            //        case Keys.Enter:
            //            if ((this.State == ST_EDITING)&& (this.m_region !=null))
            //            {
            //                this.ApplySelectOperation(false);
            //                DisposeRegion();
            //                e.Handled = true;
            //                this.State = ST_NONE;
            //                this.m_draw = false;
            //                this.Element.Invalidate(true);
            //            }
            //            break;
            //        default:
            //            char ch = (char)e.KeyValue;
            //            if (char.IsNumber(ch))
            //            {
            //                e.Handled = true;
            //                this.RegionOperationType = (enuSelectionOperationType ) (e.KeyValue - 48);
            //            }
            //            break;
            //    }
            //    if (e.Handled)
            //    {
            //        this.SendHelpMessage();
            //    }
            //    base.OnKeyUp(e);
            //}
          
            
            void BeginSelection(CoreMouseEventArgs e)
            {
                m_points.Clear();
                this.StartPoint = e.FactorPoint;
                this.EndPoint = e.FactorPoint;
                if (this.m_selectionForm == enuCoreSelectionForm.Freehand)
                {
                    m_points.Add(e.FactorPoint);
                }
                this.m_draw = true;
            }
            void EndSelection(CoreMouseEventArgs e)
            {
                this.UpdateSelection(e);
                this.m_draw = false;
                this.CurrentSurface.Invalidate();
            }
            void UpdateSelection(CoreMouseEventArgs e)
            {
                this.m_draw = false;
                switch (m_selectionForm)
                {
                    case enuCoreSelectionForm.Rectangle:
                    case enuCoreSelectionForm.Ellipse:
                        this.EndPoint = e.FactorPoint;
                        this.m_invalidZoomRectangle = CoreMathOperation.GetBounds(this.StartPoint, this.EndPoint);
                        break;
                    case enuCoreSelectionForm.Freehand:
                        RectangleF rc = new RectangleF(this.EndPoint, SizeF.Empty);
                        rc.Inflate(2, 2);
                        if (!rc.Contains(e.FactorPoint))
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
                        PointF c = this.StartPoint;
                        float radius = CoreMathOperation.GetDistance(this.EndPoint, this.StartPoint);
                        this.EndPoint = e.FactorPoint;
                        this.m_draw = true;
                        radius = CoreMathOperation.GetDistance(this.EndPoint, this.StartPoint);
                        this.m_invalidZoomRectangle = CoreMathOperation.GetBounds(c, radius);
                        break;
                }
                this.m_draw = true;
                this.CurrentSurface.Invalidate();
            }
            protected override void OnPaint(PaintEventArgs e)
            {
                GraphicsState s = e.Graphics.Save();
                e.Graphics.ResetTransform();
                e.Graphics.ScaleTransform(CurrentSurface.ZoomX, CurrentSurface.ZoomY, MatrixOrder.Append);
                e.Graphics.TranslateTransform(CurrentSurface.PosX, CurrentSurface.PosY, MatrixOrder.Append);
                e.Graphics.InterpolationMode = InterpolationMode.NearestNeighbor;
                e.Graphics.PixelOffsetMode = PixelOffsetMode.HighSpeed;
                Brush br = CoreBrushRegister.GetBrush(
                    HatchStyle .Percent50 ,
                    Color.FromArgb(25, Color.DarkBlue),
                    Color.Transparent 
                    );
                if (this.m_region != null)
                {
                    e.Graphics.FillRegion(br, m_region);
                }
                e.Graphics.Restore(s);
                //draw selection
                if (m_draw == false) return;
                s = e.Graphics.Save();
                PointF screenStart = CurrentSurface.GetScreenLocation(this.StartPoint);
                PointF screenEnd = CurrentSurface.GetScreenLocation(this.EndPoint);
                RectangleF rc = RectangleF.Empty;
                Pen v_pen = CoreBrushRegister.GetPen(Color.Black);
                v_pen.DashStyle = DashStyle.Dash;
                switch (m_selectionForm)
                {
                    case enuCoreSelectionForm.Freehand:
                        GraphicsState v_bs = e.Graphics.Save();
                        e.Graphics.ResetTransform();
                        e.Graphics.ScaleTransform(CurrentSurface.ZoomX, CurrentSurface.ZoomY, MatrixOrder.Append);
                        e.Graphics.TranslateTransform(CurrentSurface.PosX, CurrentSurface.PosY, MatrixOrder.Append);
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
                        PointF center = screenStart;
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
                e.Graphics.Restore(s);
            }
            internal void CopySelection()
            {
                if (m_region != null)
                {
                    Region v_region = m_region.Clone() as Region;
                    Matrix m = new Matrix();
                    Matrix m2 = this.Element.GetMatrix().Clone() as Matrix;
                    if (!m2.IsIdentity && m2.IsInvertible)
                    {
                        m2.Invert();
                        m.Multiply(m2, enuMatrixOrder.Append);
                    }
                    v_region.Transform(m);
                    m.Dispose();
                    m2.Dispose();
                    ImageElement img = ImageElement.FromImage(CopyRegionSelection(
                        this.Element as ImageElement, v_region));
                    if (img != null)
                    {
                        this.CurrentSurface.CurrentLayer.Elements.Add(img);
                        this.Invalidate();
                    }
                    v_region.Dispose();
                }
            }
            sealed class EditMecanismProperties :
                CoreMecanismActionBase
            {
               
                protected override bool PerformAction()
                {
                    CoreSystem.Instance.Workbench.ConfigureWorkingObject(this.Mecanism);
                    return false;
                }
            }
            #region ICoreWorkingConfigurableObject Members
            public IGK.DrSStudio.WinUI.Configuration.enuParamConfigType GetConfigType()
            {
                return enuParamConfigType.ParameterConfig;
            }
            public ICoreParameterConfigCollections GetParameters(IGK.DrSStudio.WinUI.ICoreParameterConfigCollections parameters)
            {
                ICoreParameterGroup group = parameters.AddGroup ("MecanismProperties");
                //group.AddItem("RegionOp", "lb.region.op.caption", enuParameterType.EnumType , new CoreParameterChangedEventHandler(rgChanged));
                group.AddItem(GetType().GetProperty("SelectionForm"));
                group.AddItem(GetType().GetProperty("RegionOperation"));
                group.AddItem(GetType().GetProperty("RegionOperationType" ));
                //group = parameters.AddGroup("Actions");
                //group.AddActions("btnCopySelection", "lb.copy.selection", this.m_menuCopySelection);
                return parameters;
            }
            public ICoreControl GetConfigControl()
            {
                return null;
            }
            #endregion
            #region ICoreIdentifier Members
            public string Id
            {
                get { return this.GetHashCode().ToString(); }
            }
            #endregion
        }
    }
}

