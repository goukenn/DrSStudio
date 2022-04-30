

using IGK.ICore.WinCore;
using IGK.DrSStudio;
using IGK.DrSStudio.WinUI;
/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: GCSXSingleBrushColorSelector.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
﻿using IGK.ICore;
using IGK.ICore.Drawing2D;
using IGK.ICore.Resources;
using IGK.ICore.Tools;
using IGK.ICore.WinUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IGK.ICore.WinCore.WinUI.Controls;
using IGK.ICore.WinCore.Controls;

namespace IGK.ICore.WinUI
{

    [CoreRegistrableControl(IsRegistrable=true, Name= CoreControlConstant.SINGLECOLORSELECTOR)]
    /// <summary>
    /// register public controller. on core namespace
    /// </summary>
    public class GCSXSingleColorSelector : 
        IGKXToolHostedControl,
        IColorSelector ,
        ICoreEditSingleBrushControl
    {
          private Matrix m_matrix;
        public event CoreWorkingObjectPropertyChangedEventHandler PropertyChanged;
        private bool m_configuring; //flag setted for configuring purpose

        private ICoreBrush m_FillBrush; //represent the default fill brush
        private ICorePen m_StrokeBrush; //represent the default storke brush

        private ICoreBrush m_internalFillBrush;
        private ICorePen m_internalStrokeBrush;
        private enuBrushSupport m_brushSupport; //store the brush support

        public event EventHandler ElementToConfigureChanged;
        private RectangleElement m_rectangleElement;
        private ICoreBrush m_brush;


        private GCSColorNoteBook c_singleNoteBook;
        private IGKXUserControl c_userControl;
        private IGKXPanel igkxPanel1;
        private IGKXTextBox c_txb_ColorCode;
        private GCSRGBColorSelector c_rgb_ColorSelector;


     

    
        public ICoreBrush BrushToConfigure
        {
            get { return this.Brush; }
        }
        /// <summary>
        /// get the brush to configure
        /// </summary>
        internal  ICoreBrush ConfigFillBrush {
            get {
                return m_internalFillBrush;
            }
            //set {
            //    if (this.m_internalFillBrush != value)
            //    {
            //        this.m_internalFillBrush = value;
            //    }
            //}
        }
        /// <summary>
        /// get the pen to configure
        /// </summary>
        internal  ICorePen ConfigStrokeBrush
        {
            get
            {
                return m_internalStrokeBrush;
            }
            //set
            //{
            //    if (this.m_internalStrokeBrush != value)
            //    {
            //        this.m_internalStrokeBrush = value;
            //    }
            //}
        }

      
        /// <summary>
        /// get the stroke brush to edit
        /// </summary>
        public ICorePen StrokeBrush
        {
            get { 
                return m_StrokeBrush; 
            }
            set {
                this.m_StrokeBrush = value;
            }
        }
        /// <summary>
        /// get the fillbrush to edit
        /// </summary>
        public ICoreBrush FillBrush
        {
            get { return m_FillBrush; }
            set {
                this.m_FillBrush = value;
            }
        
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.m_rectangleElement.Dispose();
            }
            this.DisposeMatrix();
            base.Dispose(disposing);
        }

        private void DisposeMatrix()
        {
            if (this.m_matrix != null)
                this.m_matrix.Dispose();
            this.m_matrix = null;
        }
        public GCSXSingleColorSelector()
        {            
            this.InitializeComponent();
            this.InitControl();
        }

        public override System.Windows.Forms.DockStyle Dock
        {
            get
            {
                return base.Dock;
            }
            set
            {
                base.Dock = value;  
            }
        }
        private void InitControl()
        {
            m_rectangleElement = new RectangleElement();
            m_internalFillBrush = m_rectangleElement.FillBrush;
            m_internalStrokeBrush = m_rectangleElement.StrokeBrush;

            this.ElementToConfigureChanged += _ElementToConfigureChanged;
        }

        void _ElementToConfigureChanged(object sender, EventArgs e)
        {
            this.FillBrush = this.Brush; 
            this.StrokeBrush = this.Brush  as CorePen;    

          
            OnBrushSupportChanged(EventArgs.Empty);
        }


        private void clearColorClick(object sender, EventArgs e)
        {
            this.c_rgb_ColorSelector.Color = Colorf.Transparent;

        }

        private IBrushSelector  m_CurrentSelector;

        public IBrushSelector  CurrentSelector
        {
            get { return m_CurrentSelector; }
            set {
                if (this.m_CurrentSelector != value)
                {
                    if (this.m_CurrentSelector != null)
                    {
                        this.m_CurrentSelector.BrushToConfigure = null;
                        this.m_CurrentSelector.ColorOwner = null;
                    }
                    this.m_CurrentSelector = value;
                    if (this.m_CurrentSelector != null)
                    {
                        this.m_CurrentSelector.BrushToConfigure = this.Brush ;
                        this.m_CurrentSelector.ColorOwner = this;
                        this.UpdateDualBrushSelector();
                    }
                }
            }
        }

        void _SelectedPageChanged(object sender, EventArgs e)
        {
            IBrushSelector selector = this.c_singleNoteBook.SelectedPage as IBrushSelector;
            this.CurrentSelector = selector;
        }

        void _ColorChanged(object sender, EventArgs e)
        {
            if (this.m_configuring)
                return;
            this.m_configuring = true;
            Colorf v_cl = this.c_rgb_ColorSelector.Color;
            this.UpdateColorProperty(v_cl);         
            this.c_txb_ColorCode.Text = v_cl.ToString(false);
            this.m_configuring = false;
        }
        private bool IsEditionAvailable()
        { 
                return (((this.BrushType == enuBrushType.LinearGradient)
                    && ((this.BrushSupport & enuBrushSupport.LinearGradient) == enuBrushSupport.LinearGradient)) ||
                    ((this.BrushType == enuBrushType.PathGradient)
                    && ((this.BrushSupport & enuBrushSupport.PathGradient) == enuBrushSupport.PathGradient)) ||
                    ((this.BrushType == enuBrushType.Solid)
                    && ((this.BrushSupport & enuBrushSupport.Solid) == enuBrushSupport.Solid)) ||
                    ((this.BrushType == enuBrushType.Texture)
                    && ((this.BrushSupport & enuBrushSupport.Texture) == enuBrushSupport.Texture)) ||
                             ((this.BrushType == enuBrushType.Hatch)
                    && ((this.BrushSupport & enuBrushSupport.Hatch) == enuBrushSupport.Hatch))
                    );
        }
        /// <summary>
        /// update brush property
        /// </summary>
        /// <param name="color"></param>
        private void UpdateColorProperty(Colorf color)
        {
                 if (this.IsEditionAvailable ())
                 {
                  
                        IBrushSelector selector = this.c_singleNoteBook.SelectedPage as IBrushSelector;
                        if (selector != null) selector.SetColor(color);
                }
        }

        void m_StrokeBrush_BrushDefinitionChanged(object sender, EventArgs e)
        {            
            OnPropertyChanged(Core2DDrawingChangement.Brush);
          
        }

        void m_FillBrush_BrushDefinitionChanged(object sender, EventArgs e)
        {
            OnPropertyChanged(Core2DDrawingChangement.Brush);
          
        }

        private void InitializeComponent()
        {
            this.c_userControl = new IGKXUserControl();
            this.igkxPanel1 = new IGKXPanel();
            this.c_txb_ColorCode = new IGKXTextBox();
            this.c_rgb_ColorSelector = new IGK.DrSStudio.GCSRGBColorSelector();
            this.igkxPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // c_userControl
            // 
            this.c_userControl.CaptionKey = null;
            this.c_userControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.c_userControl.Location = new System.Drawing.Point(0, 200);
            this.c_userControl.Name = "c_userControl";
            this.c_userControl.Size = new System.Drawing.Size(360, 202);
            this.c_userControl.TabIndex = 1;
            // 
            // igkxPanel1
            // 
            this.igkxPanel1.CaptionKey = null;
            this.igkxPanel1.Controls.Add(this.c_txb_ColorCode);
            this.igkxPanel1.Controls.Add(this.c_rgb_ColorSelector);
            this.igkxPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.igkxPanel1.Location = new System.Drawing.Point(0, 0);
            this.igkxPanel1.Name = "igkxPanel1";
            this.igkxPanel1.Size = new System.Drawing.Size(360, 200);
            this.igkxPanel1.TabIndex = 2;
            // 
            // c_txb_ColorCode
            // 
            this.c_txb_ColorCode.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.c_txb_ColorCode.Font = new System.Drawing.Font("Consolas", 12.0f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.c_txb_ColorCode.Location = new System.Drawing.Point(150, 150);
            this.c_txb_ColorCode.Name = "c_txb_ColorCode";
            this.c_txb_ColorCode.Size = new System.Drawing.Size(120, 32);
            this.c_txb_ColorCode.TabIndex = 16;
            this.c_txb_ColorCode.Text = "#FFFFFF";
            this.c_txb_ColorCode.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.c_txb_ColorCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.c_txb_ColorCode.MaxLength = 20;
            this.c_txb_ColorCode.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.c_txb_ColorCode.TextChanged += c_txb_ColorCode_TextChanged;
            // 
            // c_lb_currentMode
            // 
            // 
            // c_btn_saveCurrentBrush
            // 
            // 
            // c_rgb_ColorSelector
            // 
            this.c_rgb_ColorSelector.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.c_rgb_ColorSelector.CaptionKey = null;
            this.c_rgb_ColorSelector.Color = Colorf.FromFloat(0F, 0F, 0F, 0F);
            this.c_rgb_ColorSelector.Location = new System.Drawing.Point(3, 3);
            this.c_rgb_ColorSelector.Name = "c_rgb_ColorSelector";
            this.c_rgb_ColorSelector.Size = new System.Drawing.Size(354, 141);
            this.c_rgb_ColorSelector.TabIndex = 9;
            // 
            // GCSXDualBrushColorSelector
            // 
            this.Controls.Add(this.c_userControl);
            this.Controls.Add(this.igkxPanel1);
            this.MinimumSize = new System.Drawing.Size(360, 320);
            this.Name = "GCSXDualBrushColorSelector";
            this.Size = new System.Drawing.Size(360, 402);
            this.Load += new System.EventHandler(this._Load);
            this.igkxPanel1.ResumeLayout(false);
            this.igkxPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        void c_txb_ColorCode_TextChanged(object sender, EventArgs e)
        {
            if (this.m_configuring)
                return;
            Colorf c = Colorf.FromString (this.c_txb_ColorCode.Text );
            this.m_configuring = true;
            this.c_rgb_ColorSelector.Color = c;
            this.UpdateColorProperty(c);
            this.m_configuring = false;
        }

        public ICoreBrush GetBrush(enuBrushMode enuBrushMode)
        {
            switch (enuBrushMode)
            {
                case enuBrushMode.Stroke:
                    return this.m_StrokeBrush; 
            }
            return this.m_FillBrush;
        }

        public Matrix GetMatrix()
        {
            return m_matrix;
        }
        CoreGraphicsPath m_graphicsPath= null;

        public CoreGraphicsPath GetPath()
        {
            if (m_graphicsPath == null)
            {
                m_graphicsPath = new CoreGraphicsPath();
                m_graphicsPath.AddRectangle(new Rectanglef(0, 0, 32, 32));
            }
            return m_graphicsPath;
        }

        
        ///<summary>
        ///raise the PropertyChanged 
        ///</summary>
        protected virtual void OnPropertyChanged(CoreWorkingObjectPropertyChangedEventArgs e)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, e);
        }

        private void _Load(object sender, EventArgs e)
        {
            this.m_matrix = new Matrix();
            this.c_rgb_ColorSelector.ColorChanged += _ColorChanged;
            this.c_singleNoteBook = new GCSColorNoteBook(this);
            this.c_singleNoteBook.Dock = System.Windows.Forms.DockStyle.Fill;
            this.c_singleNoteBook.SelectedPageChanged += _SelectedPageChanged;
            this.c_singleNoteBook.SelectedPage = this.c_singleNoteBook.GetPage(enuBrushType.Solid);
            this.CurrentSelector = this.c_singleNoteBook.SelectedPage as IBrushSelector ;
            this.c_userControl.Controls.Add(c_singleNoteBook);
            this.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right | System.Windows.Forms.AnchorStyles.Top;
        }
        public enuBrushType BrushType {
            get {
                return c_singleNoteBook.BrushType;
            }
        }
        public event EventHandler BrushTypeChanged {
            add {
                this.c_singleNoteBook.BrushTypeChanged += value;
            }
            remove {
                this.c_singleNoteBook.BrushTypeChanged -= value;
            }
        }
     
        /// <summary>
        /// set the color brush owner
        /// </summary>
        /// <param name="color"></param>
        public void SetColor(Colorf color)
        {
            if (this.m_configuring)
                return;
            
            this.c_rgb_ColorSelector.Color = color;            
        }

        private void _BrushModeChanged(object sender, EventArgs e)
        {
            this.m_configuring = true;
            ICoreBrush brush = this.GetBrush(this.BrushMode);
            this.c_rgb_ColorSelector.Color = brush.Colors[0];
            this.c_txb_ColorCode.Text = brush.Colors[0].ToString(false);
            if (this.m_CurrentSelector != null)
            {
                if (this.BrushType == brush.BrushType)
                {
                    this.m_CurrentSelector.ColorOwner = this;
                    this.m_CurrentSelector.BrushToConfigure = brush;
                }
                else {
                    this.c_singleNoteBook.BrushType = brush.BrushType;
                    this.m_CurrentSelector.ColorOwner = this;
                    this.m_CurrentSelector.BrushToConfigure = brush;
                }
                
            }
            this.m_configuring = false ;
        }
        /// <summary>
        /// get the brush support
        /// </summary>
        public enuBrushSupport BrushSupport
        {
            get {
                return m_brushSupport;
            }
            set {
                if (this.m_brushSupport != value)
                {
                    this.m_brushSupport = value;
                    OnBrushSupportChanged(EventArgs.Empty);
                }
            }
        }
        public event EventHandler BrushSupportChanged;

        protected void OnBrushSupportChanged(EventArgs eventArgs)
        {
            if (BrushSupportChanged != null)
            {
                BrushSupportChanged(this, eventArgs);
            }
        }

        private void c_toogleBrush_Click(object sender, EventArgs e)
        {
            enuBrushSupport v_c = enuBrushSupport.Fill | enuBrushSupport.Stroke ;
            if ((this.BrushSupport & v_c) == v_c)
            {
                ICoreBrush v_fb = this.FillBrush;
                string v_def = v_fb.GetDefinition();
                this.FillBrush.Copy(this.StrokeBrush);
                this.StrokeBrush.CopyDefinition(v_def);
                this.UpdateBrushModeDefinition();
            }
        }
        /// <summary>
        /// update the brush mode definition. 
        /// </summary>
        private void UpdateBrushModeDefinition()
        {
            UpdateDualBrushSelector();
        
        }

        private void UpdateDualBrushSelector()
        {
            
        }

      

      

        private ICoreBrush GetCurrentBrush()
        {
            return this.m_brush;
        }

      

        private ICoreBrushOwner   m_ElementToConfigure;

        public ICoreBrushOwner   ElementToConfigure
        {
            get { return m_ElementToConfigure; }
            set
            {
                if (m_ElementToConfigure != value)
                {
                    m_ElementToConfigure = value;
                    OnElementToConfigureChanged(EventArgs.Empty);
                }
            }
        }

        private void OnElementToConfigureChanged(EventArgs eventArgs)
        {
            if (ElementToConfigureChanged != null)
            {
                this.ElementToConfigureChanged(this, eventArgs);
            }
        }

        public ICoreBrush Brush
        {
            get
            {
                return this.m_brush;
            }
            set
            {
                if (this.m_brush != value)
                {
                    this.m_brush = value;
                    OnElementToConfigureChanged(EventArgs.Empty);
                }
            }
        }


        public enuBrushMode BrushMode
        {
            get { return enuBrushMode.Fill; }
        }
    }
    
}
