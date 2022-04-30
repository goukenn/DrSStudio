

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: XLayerBlock.cs
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
file:XLayerBlock.cs
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
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D ;
using System.Drawing.Imaging;
using IGK.ICore;using IGK.DrSStudio.WinUI;
namespace IGK.DrSStudio.Drawing2D.WinUI
{
    using IGK.DrSStudio.Drawing2D;
    using IGK.DrSStudio.Menu;
    using IGK.DrSStudio.Tools;
    using IGK.DrSStudio.Core;
    using IGK.DrSStudio.Core.Layers;
    class XLayerBlock : XControl, ILayerBlock 
    {
        private ICore2DDrawingLayer m_layer;
        private XLayerPanel m_owner;
        //image display
        private const int IMG_HEIGHT = 27;
        private const int IMG_WIDTH = 48;
        private const int Min_HEIGHT = IMG_HEIGHT + 4;
        internal const int DEFAULT_HEIGHT = IMG_HEIGHT+4;
        /// <summary>
        /// timer for enabled update selection
        /// </summary>
        private Timer m_timer;
        private static Bitmap m_dashBackgroundImage;
        public int Index {
            get {
             return    m_layer.ZIndex;
            }
        }
        static Bitmap DashBackgroundImage
        {
            get
            {
                if (m_dashBackgroundImage == null)
                    m_dashBackgroundImage = CoreResources.GetDocumentImage("dash", 16, 16) as Bitmap;
                return m_dashBackgroundImage;
            }
        }
        public XLayerBlock(XLayerPanel owner, Core2DDrawingLayer layer)
        {
            if (layer == null)
                throw new CoreException(enuExceptionType.ArgumentIsNull, "layer");
            if (owner == null)
                throw new CoreException(enuExceptionType.ArgumentIsNull, "owner");
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer , true);
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            m_layer = layer;
            m_owner = owner;
            m_timer = new Timer();
            m_timer.Tick += new EventHandler(m_timer_Tick);
            m_timer.Enabled = false;
            this.Height = Min_HEIGHT;
            this.Width = 100;
            RegisterEvent();
            this.DoubleClick += new EventHandler(_DoubleClick);
            this.MouseEnter += new EventHandler(_MouseEnter);
            this.MouseLeave += new EventHandler(_MouseLeave);
            this.Paint += new PaintEventHandler(_Paint);
         }
        //paint the layer block
        void _Paint(object sender, PaintEventArgs e)
        {
            Color v_fcl = LayerBlockRenderer.XLayerBlockForeColor;
            bool v_checked = (this.Index == this.m_owner.SelectedIndex);
            bool v_isOver = this.ClientRectangle.Contains((this.PointToClient(Control.MousePosition)));
            LinearGradientBrush ln = null;
            int w = 0;
            int h = 0;
            float v_posx = 0;
            float v_posy = 0;
            //draw bulle selection document
            e.Graphics.CompositingMode = CompositingMode.SourceCopy;
            if (this.Index == this.m_owner.SelectedIndex)
            {
                RenderSelected(e);
                //render selected index
                Color v_cl1 = LayerBlockRenderer.XLayerBlockSelectedBackgroundBegin;
                Color v_cl2 = LayerBlockRenderer.XLayerBlockSelectedBackgroundEnd;
                if (v_isOver)
                {
                    v_cl1 = LayerBlockRenderer.XLayerBlockSelectedOverBackgroundBegin;
                    v_cl2 = LayerBlockRenderer.XLayerBlockSelectedOverBackgroundEnd;
                }
                using (ln = new LinearGradientBrush(this.ClientRectangle,
                        v_cl1,
                        v_cl2,
                        90.0f))
                {
                    e.Graphics.FillRectangle(ln, this.ClientRectangle);
                }
            }
            else
            {//render nn selecetd index
                RenderNonSelected(e);
                if (!v_isOver)
                {
                    e.Graphics.FillRectangle(CoreBrushRegister.GetBrush(
                                                   LayerBlockRenderer.XLayerBlockBackground), this.ClientRectangle);
                }
                else
                {
                    e.Graphics.FillRectangle(CoreBrushRegister.GetBrush(
                                LayerBlockRenderer.XLayerBlockOverBackground), this.ClientRectangle);
                }
            }
            e.Graphics.CompositingMode = CompositingMode.SourceOver;
            if (v_checked)
            {
                v_fcl = LayerBlockRenderer.XLayerBlockSelectedForeColor;
            }
            //draw image
            //get the layer information
            w = Layer.Parent.Width;
            h = Layer.Parent.Height;
            float zoomX = IMG_WIDTH / (float)w;
            float zoomY = IMG_HEIGHT / (float)h;
            zoomX = Math.Min(zoomX, zoomY);
            zoomY = zoomX;
            int xx = Math.Max (1, (int)Math.Round(zoomX * w));
            int yy = Math.Max (1, (int)Math.Round(zoomY * h));
            using (Bitmap bmp = new Bitmap(xx, yy))
            {
                if (bmp == null)
                    return;
                Graphics v_g = Graphics.FromImage(bmp);
                v_g.ScaleTransform(zoomX, zoomY, MatrixOrder.Append);
                this.m_layer.Draw(v_g);
                v_g.Dispose();
                v_posx = (IMG_WIDTH / 2.0f) - (bmp.Width) / 2.0f;
                v_posy = 2 + (IMG_HEIGHT / 2.0f) - (bmp.Height) / 2.0f;
                GraphicsState s = e.Graphics.Save();
                e.Graphics.InterpolationMode = InterpolationMode.High;
                Rectangle rc = new Rectangle((int)(3 + v_posx), (int)v_posy, bmp.Width, bmp.Height);
                //fill background width black color
                if ((DashBackgroundImage != null) && (DashBackgroundImage.PixelFormat != System.Drawing.Imaging.PixelFormat.Undefined))
                {//fill dash
                    Brush br = CoreBrushRegister.GetBrush(DashBackgroundImage);
                    e.Graphics.FillRectangle(br, rc);
                }
                ControlPaint.DrawBorder(e.Graphics, rc, Color.Black, ButtonBorderStyle.Solid);
                e.Graphics.DrawImage(bmp, 3 + v_posx, v_posy, bmp.Width, bmp.Height);
                v_posx += IMG_WIDTH  +2;
                if (this.Layer.IsClipped)
                {
                    using (Bitmap cbmp =
                        CoreBitmapOperation.GetMask(bmp, 0, false, false))
                    {
                        e.Graphics.DrawImage(cbmp, v_posx, v_posy, bmp.Width, bmp.Height);
                    }
                    //Draw border ;
                    ControlPaint.DrawBorder(e.Graphics, Rectangle.Round(new RectangleF(v_posx, v_posy, bmp.Width, bmp.Height)), Color.Black, ButtonBorderStyle.Solid);
                    v_posx += IMG_WIDTH + 3;
                }
                e.Graphics.Restore(s);
                //draw bar
                RectangleF prc = new RectangleF(new PointF(v_posx, 0), new Size(4, this.Height));
                using (LinearGradientBrush br = new LinearGradientBrush(prc, Color.DarkGray , Color.Gray , 90))
                {
                    e.Graphics.FillRectangle(br, prc);
                }
                Color cl = Color.Black;
                if (this.Index == this.m_owner.SelectedIndex)
                    cl = Color.Magenta;
                ControlPaint.DrawBorder(e.Graphics,
                    new Rectangle(3, (int)v_posy, IMG_WIDTH, IMG_HEIGHT), cl, ButtonBorderStyle.Solid);
            }
            //draw layer id
            StringFormat sf = new StringFormat();
            sf.FormatFlags = StringFormatFlags.NoWrap;
            sf.Trimming = StringTrimming.EllipsisPath;
            sf.Alignment = StringAlignment.Center;
            sf.LineAlignment = StringAlignment.Center;
            int v_w = this.m_layer.IsClipped ? IMG_WIDTH : 0;
            RectangleF v_txtrc = new RectangleF(
                    v_posx + v_w,
                    0
                    , (this.Width - (v_posx + v_w)),
                    this.Height);
            RectangleF v_rc = CoreMathOperation.Align(
                new RectangleF(PointF.Empty,
                    e.Graphics.MeasureString(Layer.Id, this.Font, new SizeF(short.MaxValue, short.MinValue),
                    sf)),
                    v_txtrc,
                    enuCore2DAlignElement.Center);
            //if (v_txtrc.X <= v_rc.X)
            //{
                e.Graphics.DrawString(Layer.Id, this.Font, CoreBrushRegister.GetBrush(v_fcl),
                    v_txtrc, sf);
            //}
            sf.Dispose();
            //
            //draw global border
            //
            ControlPaint.DrawBorder(e.Graphics, this.ClientRectangle,
                LayerBlockRenderer.XLayerBlockCheckedBorderColor,
                ButtonBorderStyle.Solid);
        }
        private void RenderNonSelected(PaintEventArgs e)
        {            
        }
        private void RenderSelected(PaintEventArgs e)
        {            
        }
        void _MouseLeave(object sender, EventArgs e)
        {
            this.Invalidate();
        }
        void _MouseEnter(object sender, EventArgs e)
        {
            this.Invalidate();
        }
        void _DoubleClick(object sender, EventArgs e)
        {
            this.m_owner.OwnerControl.MainForm.Workbench.CallAction("Layer.Property");        
        }
        void m_timer_Tick(object sender, EventArgs e)
        {
            this.Refresh();
            this.m_timer .Enabled = false;
        }
        void ParentDocument_CurrentLayerChanged(object sender,Core2DDrawingLayerChangedEventArgs  e)
        {
            if (this.m_layer == e.NewLayer)
            {
                this.m_owner.SelectedIndex = this.Index;
            }
        }
        void ParentDocument_LayerRemoved(object o, Core2DDrawingLayerEventArgs  e)
        {
            if (e.Layer == this.m_layer)
            {
                this.Dispose();
                this.m_owner.Layers.Remove(this);
            }
        }
        ~XLayerBlock()
        {
            Dispose();
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                UnregisterEvent();
                if (this.m_timer != null)
                {
                    this.m_timer.Dispose();
                    this.m_timer = null;
                }
                m_layer = null;
            }
            base.Dispose(disposing);
        }
        private void RegisterEvent()
        {
            this.m_layer.Parent.LayerRemoved+= new Core2DDrawingLayerEventHandler(ParentDocument_LayerRemoved);
            this.m_layer.Parent.CurrentLayerChanged += new Core2DDrawingLayerChangedEventHandler(ParentDocument_CurrentLayerChanged);
            this.m_layer.Parent.LayerZIndexChanged += new CoreWorkingObjectZIndexChangedHandler (ParentDocument_LayerZIndexChanged);
            this.m_layer.PropertyChanged += new CoreWorkingObjectPropertyChangedEventHandler(m_layer_PropertyChanged);
            this.m_owner.SelectedIndexChanged += new EventHandler(m_owner_SelectedIndexChanged);
            this.m_layer.ClippedChanged += new EventHandler(m_layer_ClippedChanged);
            this.Click += new EventHandler(_Click);
        }
        void m_layer_ClippedChanged(object sender, EventArgs e)
        {
            if (this.Visible)
            {
                this.Invalidate();
            }
        }
        void Parent_LayerRemoved(object o, EventArgs e)
        {
        }
        void _Click(object sender, EventArgs e)
        {
            this.m_owner.SelectedIndex = this.Layer.ZIndex;           
        }
        void ParentDocument_LayerZIndexChanged(object document, CoreWorkingObjectZIndexChangedArgs  e)
        {
            if (e.Item == this.m_layer)
            {
                this.m_owner.Controls.SetChildIndex(this, e.CurrentIndex );
                this.m_owner.Layers.Insert(e.CurrentIndex , this);
                this.m_owner.SelectedIndex = e.CurrentIndex;
            }
        }
        void m_owner_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Invalidate();
        }
        private void UnregisterEvent()
        {
            if ((m_layer != null)&&( m_layer.Parent !=null))
            {
                this.m_layer.Parent.LayerRemoved -= new Core2DDrawingLayerEventHandler(ParentDocument_LayerRemoved);
                this.m_layer.Parent.CurrentLayerChanged -= new Core2DDrawingLayerChangedEventHandler(ParentDocument_CurrentLayerChanged);                      
            }
            this.m_layer.ClippedChanged -= new EventHandler(this.m_layer_ClippedChanged);
            this.m_layer.PropertyChanged -= new CoreWorkingObjectPropertyChangedEventHandler(m_layer_PropertyChanged);               
            this.m_owner.SelectedIndexChanged -= new EventHandler(m_owner_SelectedIndexChanged);
        }
        void m_layer_PropertyChanged(object o, CoreWorkingObjectPropertyChangedEventArgs e)
        {
            if (m_layer == o)
            {
                switch ((enu2DPropertyChangedType ) e.ID )
                {
                    case enu2DPropertyChangedType.DefinitionChanged:
                    //case enuCorePropertyChangedType.elementChanged:
                    case enu2DPropertyChangedType.MatrixChanged :
                        m_timer.Enabled = true;
                        break;
                }
            }
        }
        #region ILayerBlock Members
        ILayerPanel ILayerBlock.Owner {
            get {
                return m_owner;
            }
        }
        public ICore2DDrawingLayer Layer
        {
            get { return m_layer; }
        }
        #endregion
        #region ILayerBlock Members
        public int X
        {
            get
            {
                return Location.X;
            }
            set
            {
                Point pt = this.Location;
                pt.X = value;
                this.Location = pt;
            }
        }
        public int Y
        {
            get
            {
                return Location.Y;
            }
            set
            {
                Point pt = this.Location;
                pt.Y = value;
                this.Location = pt;
            }
        }
        #endregion
    }
}

