

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: GCSXDualBrushColorSelector.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
﻿using IGK.ICore.WinCore;
using IGK.ICore;
using IGK.ICore.Drawing2D;
using IGK.ICore.Resources;
using IGK.ICore.WinUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IGK.ICore.WinCore.WinUI.Controls;
using IGK.ICore.WinCore.Controls;

namespace IGK.DrSStudio.WinUI
{

    /// <summary>
    /// allow to modify to color of items
    /// </summary>
    public class GCSXDualBrushColorSelector : 
        IGKXToolHostedControl ,
        ICoreBrushOwner ,
        IColorSelector 
    {
        private Matrix m_matrix;
        public event CoreWorkingObjectPropertyChangedEventHandler PropertyChanged;
        private bool m_configuring; //flag setted for configuring purpose

        private ICoreBrush m_FillBrush; //represent the default fill brush
        private ICorePen m_StrokeBrush; //represent the default storke brush

        private ICoreBrush m_internalFillBrush;
        private ICorePen m_internalStrokeBrush;
        private enuBrushSupport m_brushSupport; //store the brush support


        private GCSColorNoteBook c_singleNoteBook;
        private IGKXUserControl c_userControl;
        private IGKXPanel igkxPanel1;
        private IGKXTextBox c_txb_ColorCode;
        private IGKXLabel c_lb_currentMode;
        private IGKXButton c_btn_saveCurrentBrush;
        private IGKXButton c_btn_toogleBrush;
        private IGKXButton c_btn_clearColor;
        private IGKXButton c_btn_pickColor;
        private GCSXDualBrushModeSelector c_gcs_dualModeBrushSelector;
        private GCSRGBColorSelector c_rgb_ColorSelector;


     

        private bool EnabledStrokeBrush {
            get {
                return this.c_gcs_dualModeBrushSelector.EnabledStrokeBrush;
            }
        }
        private bool EnabledFillBrush {
            get {
                return this.c_gcs_dualModeBrushSelector.EnabledFillBrush;
            }
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
        /// get the pick color Button
        /// </summary>
        public IGKXButton PickColorButton {
            get {
                return c_btn_pickColor;
            }
        }
        public IGKXButton SaveBrushButton { get { return this.c_btn_saveCurrentBrush; } }

        /// <summary>
        /// get the brush mode
        /// </summary>
        public enuBrushMode BrushMode
        {
            get { return this.c_gcs_dualModeBrushSelector.BrushMode; }         
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
                if (this.m_StrokeBrush != value)
                {
                    if (this.m_StrokeBrush != null)
                        this.m_StrokeBrush.BrushDefinitionChanged -= _BrushChanged;
                    this.m_StrokeBrush = value;
                    if (this.m_StrokeBrush != null)
                        this.m_StrokeBrush.BrushDefinitionChanged += _BrushChanged;
                }
            }
        }
        /// <summary>
        /// get the fillbrush to edit
        /// </summary>
        public ICoreBrush FillBrush
        {
            get { return m_FillBrush; }
            set {
                if (this.m_FillBrush != value)
                {
                    if (this.m_FillBrush != null)
                        this.m_FillBrush.BrushDefinitionChanged -= _BrushChanged;
                    this.m_FillBrush = value;
                    if (this.m_FillBrush != null)
                        this.m_FillBrush.BrushDefinitionChanged += _BrushChanged;
                }
            }
        
        }
        protected override void Dispose(bool disposing)
        {
            
            if (disposing)
            {
                if (this.m_rectangleElement != null)
                {
                    this.m_rectangleElement.Dispose();
                    this.m_rectangleElement = null;
                }
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
        public GCSXDualBrushColorSelector()
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
            //rectangle element for brush definition

            this.m_FillBrush = new CoreBrush(this);
            this.m_StrokeBrush = new CorePen(this);
            this.m_matrix = new Matrix();
            this.m_FillBrush.BrushDefinitionChanged += m_FillBrush_BrushDefinitionChanged;
            this.m_StrokeBrush.BrushDefinitionChanged += m_StrokeBrush_BrushDefinitionChanged;
            this.c_rgb_ColorSelector.ColorChanged += _ColorChanged;

            this.c_singleNoteBook = new GCSColorNoteBook(this);
            this.c_singleNoteBook.Dock = System.Windows.Forms.DockStyle.Fill;
            this.c_singleNoteBook.SelectedPageChanged += _SelectedPageChanged;
            this.c_singleNoteBook.SelectedPage = this.c_singleNoteBook.GetPage(enuBrushType.Solid);
            this.CurrentSelector = this.c_singleNoteBook.SelectedPage as IBrushSelector;
            
            this.c_userControl.Controls.Add(c_singleNoteBook);

            m_rectangleElement = new RectangleElement();
            m_rectangleElement.Id = "DualBrushColorSelector_InternalRectangle";
            m_internalFillBrush = m_rectangleElement.FillBrush;
            m_internalStrokeBrush = m_rectangleElement.StrokeBrush;

            this.c_gcs_dualModeBrushSelector.BrushModeChanged += _BrushModeChanged;
            this.c_btn_pickColor.ButtonDocument = CoreButtonDocument.CreateFromRes (CoreImageKeys.BTN_PICKCOLOR_GKDS);//.Create(CoreResources.GetAllDocuments("btn_PickColor"));
            this.c_btn_clearColor.ButtonDocument = CoreButtonDocument.CreateFromRes(CoreImageKeys.BTN_CLEARCOLOR_GKDS);//.Create(CoreResources.GetAllDocuments("btn_clearColor"));
            this.c_btn_toogleBrush.Click += this.c_toogleBrush_Click;
            this.c_btn_clearColor.Click += clearColorClick;
            this.ElementToConfigureChanged += _ElementToConfigureChanged;
            this.Load += _Load; 
        }
        

        void _ElementToConfigureChanged(object sender, EventArgs e)
        {
          
        }

        private void updateElementConfiguration()
        {
            if (this.m_configuring)
                return;
            this.m_configuring = true;

            if (this.ElementToConfigure != null)
            {
                this.m_brushSupport = this.ElementToConfigure.BrushSupport;
                this.FillBrush = this.ElementToConfigure.GetBrush(enuBrushMode.Fill);
                this.StrokeBrush = this.ElementToConfigure.GetBrush(enuBrushMode.Stroke) as CorePen;
                this.c_gcs_dualModeBrushSelector.EnabledFillBrush = (enuBrushSupport.Fill) == (this.m_brushSupport & enuBrushSupport.Fill);
                this.c_gcs_dualModeBrushSelector.EnabledStrokeBrush = (enuBrushSupport.Stroke) == (this.m_brushSupport & enuBrushSupport.Stroke);
                //update clolor
                this.updateSelectorDefinition();
            }
            else
            {
                this.c_gcs_dualModeBrushSelector.EnabledFillBrush = true;
                this.c_gcs_dualModeBrushSelector.EnabledStrokeBrush = true;
                this.FillBrush = this.m_internalFillBrush;
                this.StrokeBrush = this.m_internalStrokeBrush;
                this.m_brushSupport = enuBrushSupport.All;
            }
            OnBrushSupportChanged(EventArgs.Empty);
            this.m_configuring = false;
        }

        private void updateSelectorDefinition()
        {
            if (!m_configuring)
                return;

            m_configuring = true;
            
            this.c_gcs_dualModeBrushSelector.UpdateBrush(enuBrushMode.Stroke, this.StrokeBrush);
            switch (this.BrushMode)
            {
                case enuBrushMode.Stroke:
                    this.c_rgb_ColorSelector.Color = this.StrokeBrush != null ? this.StrokeBrush.Colors[0] : this.m_internalStrokeBrush.Colors[0];
                    break;
                case enuBrushMode.Fill:
                    this.c_rgb_ColorSelector.Color = this.FillBrush != null ? this.FillBrush.Colors[0] : this.m_internalFillBrush.Colors[0];
                    break;
            }
            this.c_gcs_dualModeBrushSelector.UpdateBrush(enuBrushMode.Fill, this.FillBrush);
            this.c_txb_ColorCode.Text = this.c_rgb_ColorSelector.Color.ToString(false);
            m_configuring = false;
        }


        private void clearColorClick(object sender, EventArgs e)
        {
            var v_br = this.BrushToConfigure;
            if (v_br != null)
            {
                this.SetColor(Colorf.Transparent);
                //v_br.SetSolidColor(Colorf.Transparent);
               // this.c_singleNoteBook.BrushType = this.BrushToConfigure.BrushType ;
            }
            else
            {
                this.c_rgb_ColorSelector.Color = Colorf.Transparent;
            }

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
                        this.m_CurrentSelector.ColorOwner = this;
                        this.m_CurrentSelector.BrushToConfigure = this.GetBrush(this.BrushMode);
                        
                        this.UpdateDualBrushSelector();
                    }
                }
            }
        }

        void _SelectedPageChanged(object sender, EventArgs e)
        {
            GCSColorNoteBook nbook = sender as GCSColorNoteBook;
            var page = nbook.SelectedPage;
            IBrushSelector selector = page as IBrushSelector;
            this.CurrentSelector = selector;
        }

        void _ColorChanged(object sender, EventArgs e)
        {
            if (this.m_configuring)
                return;
            this.m_configuring = true;
            Colorf v_cl = this.c_rgb_ColorSelector.Color;
            var selector = this.CurrentSelector;

            this.UpdateColorProperty(v_cl);         
            this.c_txb_ColorCode.Text = v_cl.ToString(false);
            this.c_txb_ColorCode.Refresh();
           // this.UpdateDualBrushSelector();
            //if (this.CurrentSelector != null) 
            //    this.CurrentSelector.SetColor(v_cl);
            //this.updateSelectorDefinition();
            this.m_configuring = false;
        }
        private bool IsEditionAvailable()
        { 
            if  ((((this.BrushMode == enuBrushMode.Fill) &&
                (this.BrushSupport & enuBrushSupport.Fill) == enuBrushSupport.Fill)) ||
            ((this.BrushMode == enuBrushMode.Stroke) &&
                ((this.BrushSupport & enuBrushSupport.Stroke) == enuBrushSupport.Stroke)))
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
            return false ;
        }
        /// <summary>
        /// update brush property
        /// </summary>
        /// <param name="color"></param>
        private void UpdateColorProperty(Colorf color)
        {
             if (this.IsEditionAvailable ())
             {
                    if (this.BrushType == enuBrushType.Solid)
                    {
                        ICoreBrush  br = this.GetBrush(this.BrushMode);
                        if (br!=null)
                        br.SetSolidColor(color);
                    }
                    else
                    {
                        IBrushSelector selector = this.c_singleNoteBook.SelectedPage as IBrushSelector;
                        if (selector != null) 
                            selector.SetColor(color);
                    }
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
            this.c_lb_currentMode = new IGKXLabel();
            this.c_btn_saveCurrentBrush = new IGKXButton();
            this.c_btn_toogleBrush = new IGKXButton();
            this.c_btn_clearColor = new IGKXButton();
            this.c_btn_pickColor = new IGKXButton();
            this.c_gcs_dualModeBrushSelector = new GCSXDualBrushModeSelector();
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
            this.igkxPanel1.Controls.Add(this.c_lb_currentMode);
            this.igkxPanel1.Controls.Add(this.c_btn_saveCurrentBrush);
            this.igkxPanel1.Controls.Add(this.c_btn_toogleBrush);
            this.igkxPanel1.Controls.Add(this.c_btn_clearColor);
            this.igkxPanel1.Controls.Add(this.c_btn_pickColor);
            this.igkxPanel1.Controls.Add(this.c_gcs_dualModeBrushSelector);
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
            this.c_lb_currentMode.CaptionKey = null;
            this.c_lb_currentMode.AutoSize = true;
            this.c_lb_currentMode.BackColor = System.Drawing.Color.Transparent;            
            this.c_lb_currentMode.Location = new System.Drawing.Point(69, 153);
            this.c_lb_currentMode.Name = "c_lb_currentMode";
            this.c_lb_currentMode.TabIndex = 0;
            this.c_lb_currentMode.TabStop = false;
            // 
            // c_btn_saveCurrentBrush
            // 
            this.c_btn_saveCurrentBrush.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.c_btn_saveCurrentBrush.CaptionKey = null;
            this.c_btn_saveCurrentBrush.Checked = false;
            this.c_btn_saveCurrentBrush.DialogResult = enuDialogResult.None;
            this.c_btn_saveCurrentBrush.Location = new System.Drawing.Point(16, 176);
            this.c_btn_saveCurrentBrush.Name = "c_btn_saveCurrentBrush";
            this.c_btn_saveCurrentBrush.ShowButtonImage = false;
            this.c_btn_saveCurrentBrush.Size = new System.Drawing.Size(16, 16);
            this.c_btn_saveCurrentBrush.State = enuButtonState.Normal;
            this.c_btn_saveCurrentBrush.TabIndex = 14;
            // 
            // c_toogleBrush
            // 
            this.c_btn_toogleBrush.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.c_btn_toogleBrush.CaptionKey = null;
            this.c_btn_toogleBrush.Checked = false;
            this.c_btn_toogleBrush.DialogResult = enuDialogResult.None;
            this.c_btn_toogleBrush.Location = new System.Drawing.Point(51, 140);
            this.c_btn_toogleBrush.Name = "c_toogleBrush";
            this.c_btn_toogleBrush.ShowButtonImage = false;
            this.c_btn_toogleBrush.Size = new System.Drawing.Size(16, 16);
            this.c_btn_toogleBrush.State = enuButtonState.Normal;
            this.c_btn_toogleBrush.TabIndex = 13;
            // 
            // c_btn_clearColor
            // 
            this.c_btn_clearColor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.c_btn_clearColor.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.c_btn_clearColor.CaptionKey = null;
            this.c_btn_clearColor.Checked = false;
            this.c_btn_clearColor.DialogResult = enuDialogResult.None;
            this.c_btn_clearColor.Location = new System.Drawing.Point(287, 148);
            this.c_btn_clearColor.Name = "c_btn_clearColor";
            this.c_btn_clearColor.ShowButtonImage = false;
            this.c_btn_clearColor.State = enuButtonState.Normal;
            this.c_btn_clearColor.TabIndex = 11;
            // 
            // c_btn_pickColor
            // 
            this.c_btn_pickColor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.c_btn_pickColor.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.c_btn_pickColor.CaptionKey = null;
            this.c_btn_pickColor.Checked = false;
            this.c_btn_pickColor.DialogResult = enuDialogResult.None;
            this.c_btn_pickColor.Location = new System.Drawing.Point(325, 148);
            this.c_btn_pickColor.Name = "c_btn_pickColor";
            this.c_btn_pickColor.ShowButtonImage = false;
            this.c_btn_pickColor.State = enuButtonState.Normal;
            this.c_btn_pickColor.TabIndex = 12;
            // 
            // c_gcs_dualModeBrushSelector
            // 
            this.c_gcs_dualModeBrushSelector.BackColor = System.Drawing.Color.Transparent;
            this.c_gcs_dualModeBrushSelector.BrushMode = enuBrushMode.Stroke;
            this.c_gcs_dualModeBrushSelector.CaptionKey = null;
            this.c_gcs_dualModeBrushSelector.Location = new System.Drawing.Point(12, 136);
            this.c_gcs_dualModeBrushSelector.Name = "c_gcs_dualModeBrushSelector";
            this.c_gcs_dualModeBrushSelector.Size = new System.Drawing.Size(50, 50);
            this.c_gcs_dualModeBrushSelector.TabIndex = 10;
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
            this.updateElementConfiguration();
            //update size
            this.c_lb_currentMode.CaptionKey = "";
            this.c_lb_currentMode.CaptionKey = this.BrushMode.ToString();
            this.c_btn_saveCurrentBrush.AutoButtonSize = false;
            this.c_btn_saveCurrentBrush.ButtonDocument = 
                CoreButtonDocument.CreateFromRes(CoreImageKeys.BTN_SAVE_BRUSH_GKDS );//(CoreResources.GetAllDocuments(CoreConstant.IMG_BTN_SAVE_BRUSH));

            this.c_btn_toogleBrush.AutoButtonSize = false;
            this.c_btn_toogleBrush.ButtonDocument = 
                CoreButtonDocument.CreateFromRes(CoreImageKeys.BTN_TOOGLE_BRUSH_GKDS );//.Create(CoreResources.GetAllDocuments(CoreConstant.IMG_BTN_TOGGLE_BRUSH));
        }

        void _BrushChanged(object sender, EventArgs e)
        {
            this.UpdateDualBrushSelector();
            //this.c_gcs_dualModeBrushSelector.UpdateBrush(this.BrushMode, this.GetBrush(this.BrushMode));
            if (this.c_singleNoteBook != null)
            {
                if (this.BrushType != this.BrushToConfigure.BrushType)
                {
                    this.c_singleNoteBook.BrushType = this.BrushToConfigure.BrushType;

                }
            }
        }
        public enuBrushType BrushType {
            get {
                if (this.c_singleNoteBook != null)
                {
                    return c_singleNoteBook.BrushType;
                }
                return enuBrushType.Solid;
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
            if (this.m_configuring)
                return;
            this.m_configuring = true;
            this.c_lb_currentMode.CaptionKey = this.BrushMode.ToString();

            
            ICoreBrush brush = this.GetBrush(this.BrushMode);
            if (brush != null)
            {
                this.c_rgb_ColorSelector.Color = brush.Colors[0];
                this.c_txb_ColorCode.Text = brush.Colors[0].ToString(false);
                if (this.m_CurrentSelector != null)
                {
                    if (this.BrushType == brush.BrushType)
                    {
                        this.m_CurrentSelector.ColorOwner = this;
                        this.m_CurrentSelector.BrushToConfigure = brush;
                    }
                    else
                    {
                        this.c_singleNoteBook.BrushType = brush.BrushType;
                        this.m_CurrentSelector.ColorOwner = this;
                        this.m_CurrentSelector.BrushToConfigure = brush;
                    }

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
            this.c_gcs_dualModeBrushSelector.EnabledFillBrush = (this.BrushSupport & enuBrushSupport.Fill) == enuBrushSupport.Fill;
            this.c_gcs_dualModeBrushSelector.EnabledStrokeBrush  = (this.BrushSupport  & enuBrushSupport.Stroke) == enuBrushSupport.Stroke ;
            UpdateDualBrushSelector();
        
        }

        private void UpdateDualBrushSelector()
        {
            if (this.c_gcs_dualModeBrushSelector.EnabledFillBrush)
            {
                this.c_gcs_dualModeBrushSelector.UpdateBrush(enuBrushMode.Fill, this.FillBrush);
            }
            if (this.c_gcs_dualModeBrushSelector.EnabledStrokeBrush)
            {
                this.c_gcs_dualModeBrushSelector.UpdateBrush(enuBrushMode.Stroke, this.StrokeBrush);
            }
        }

      

      

        private ICoreBrush GetCurrentBrush()
        {
            if (this.BrushMode == enuBrushMode.Fill)
                return this.ConfigFillBrush;
            return this.ConfigStrokeBrush ;

            
        }

      

        private ICoreBrushOwner   m_ElementToConfigure;

        /// <summary>
        /// get or set the element to configure
        /// </summary>
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
        public event EventHandler ElementToConfigureChanged;
        private RectangleElement m_rectangleElement;


        private void OnElementToConfigureChanged(EventArgs eventArgs)
        {
            this.updateElementConfiguration();
            if (ElementToConfigureChanged != null)
            {
                this.ElementToConfigureChanged(this, eventArgs);
            }
            ICoreBrush brush = this.GetBrush(this.BrushMode);

            this.m_CurrentSelector.BrushToConfigure = brush;
        }


        public ICoreBrush BrushToConfigure
        {
            get { return this.GetBrush(this.BrushMode); }
        }

        internal void CopyBrushDefinition(ICoreBrushOwner coreBrushOwner)
        {
            if (this.m_configuring)
                return;
            this.m_configuring = true;
            this.m_internalFillBrush.Copy(coreBrushOwner.GetBrush(enuBrushMode.Fill));
            this.m_internalStrokeBrush.Copy(coreBrushOwner.GetBrush(enuBrushMode.Stroke));
            this.updateSelectorDefinition();
            this.m_configuring = false;
            //refresh configure config
            //this.OnElementToConfigureChanged(EventArgs.Empty);
        }
    }
}
