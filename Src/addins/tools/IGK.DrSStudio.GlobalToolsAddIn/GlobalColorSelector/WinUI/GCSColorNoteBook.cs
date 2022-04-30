

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: GCSColorNoteBook.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
﻿using IGK.ICore.WinCore;
using IGK.ICore;using IGK.ICore.Drawing2D;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.DrSStudio.WinUI
{
    class GCSColorNoteBook : IGKXNoteBook 
    {
        private GCSXSolidBrushSelector c_solidBrushSelector;
        private GdiStrokePropertySelector c_gdiStrokeBrushSelector;
        private GCSXLinearBrushSelector c_linearBrushSelector;
        private GCSXPathBrushSelector c_pathBrushSelector;
        private TextureBrushSelector c_textureBrushSelector;
        private HatchBrushSelector c_hatchBrushSelector;
        private IColorSelector m_owner;
        private enuBrushType  m_BrushType;

        public enuBrushType  BrushType
        {
            get { return m_BrushType; }
            set
            {
                if (m_BrushType != value)
                {
                    m_BrushType = value;
                    var p = GetPage(value);
                    if (this.SelectedPage != p)
                    {
                        this.SelectedPage = p;
                    }
                    OnBrushTypeChanged(EventArgs.Empty);
                }
            }
        }
        public event EventHandler BrushTypeChanged;
        ///<summary>
        ///raise the BrushTypeChanged 
        ///</summary>
        protected virtual void OnBrushTypeChanged(EventArgs e)
        {
            if (BrushTypeChanged != null)
                BrushTypeChanged(this, e);
        }

        
        public GCSColorNoteBook():base()
        {

            c_gdiStrokeBrushSelector = new GdiStrokePropertySelector();
            c_gdiStrokeBrushSelector.CaptionKey = "title.StrokeProperty";
            c_hatchBrushSelector = new HatchBrushSelector();
            c_hatchBrushSelector.CaptionKey = "title.HatchBrush";
            c_linearBrushSelector = new GCSXLinearBrushSelector();
            c_linearBrushSelector.CaptionKey = "title.LinearGradient";
            c_pathBrushSelector = new GCSXPathBrushSelector();
            c_pathBrushSelector.CaptionKey = "title.PathGradientBrush";
            c_solidBrushSelector = new GCSXSolidBrushSelector();
            c_solidBrushSelector.CaptionKey = "title.SolidBrushSelector";
            c_textureBrushSelector = new TextureBrushSelector();
            c_textureBrushSelector.CaptionKey = "title.Texture";


            this.TabPages.Add(c_solidBrushSelector);
            this.TabPages.Add(c_gdiStrokeBrushSelector);
            this.TabPages.Add(c_hatchBrushSelector);
            this.TabPages.Add(c_linearBrushSelector);
            this.TabPages.Add(c_pathBrushSelector);
            this.TabPages.Add(c_textureBrushSelector);

            c_solidBrushSelector.Tag = enuBrushType.Solid;
            c_linearBrushSelector.Tag = enuBrushType.LinearGradient;
            c_textureBrushSelector.Tag = enuBrushType.Texture;
            c_pathBrushSelector.Tag = enuBrushType.PathGradient;
            c_hatchBrushSelector.Tag = enuBrushType.Hatch ;
            this.SelectedPageChanged += _SelectedPageChanged;
            c_solidBrushSelector.ColorChanged += c_solidBrushSelector_ColorChanged;
        }

        void c_solidBrushSelector_ColorChanged(object sender, EventArgs e)
        {
            this.m_owner.SetColor(this.c_solidBrushSelector.Color);
        }

        public GCSColorNoteBook(IColorSelector owner)
            : this()
        {            
            this.m_owner = owner;
            this.m_owner.BrushSupportChanged += m_owner_BrushSupportChanged;
            this.m_owner.ElementToConfigureChanged += m_owner_ElementToConfigureChanged;
            this.updateBrushSupport();
        }

        void m_owner_ElementToConfigureChanged(object sender, EventArgs e)
        {
            ICoreBrush br = this.m_owner.BrushToConfigure;
            if (br != null)
            {
                var p = this.GetPage(br.BrushType);
                switch (br.BrushType)
                {
                    case enuBrushType.Solid:
                        if ((this.SelectedPage is GdiStrokePropertySelector) == false)
                        {
                            this.SelectedPage = p;
                        }
                        else {
                            GdiStrokePropertySelector r = this.SelectedPage as GdiStrokePropertySelector;
                            r.Enabled = false;
                            r.BrushToConfigure = (br as CorePen);
                        }
                        break;
                    default:
                        if (this.SelectedPage != p)
                        {
                            this.SelectedPage = p;

                        }
                        else {
                            var s = (this.SelectedPage as GCSXBrushConfigureBase).BrushToConfigure;

                           
                        }
                        
                        break;
                }
            }
        }

        private void updateBrushSupport()
        {
            enuBrushSupport v_osupport = this.m_owner .BrushSupport;            
            this.c_gdiStrokeBrushSelector.Enabled = (v_osupport & enuBrushSupport.GdiStroke) == enuBrushSupport.GdiStroke;
            this.c_solidBrushSelector.Enabled = (v_osupport & enuBrushSupport.Solid ) == enuBrushSupport.Solid;
            this.c_textureBrushSelector.Enabled = (v_osupport & enuBrushSupport.Texture ) == enuBrushSupport.Texture ;
            this.c_pathBrushSelector .Enabled = (v_osupport & enuBrushSupport.PathGradient ) == enuBrushSupport.PathGradient ;
            this.c_linearBrushSelector.Enabled = (v_osupport & enuBrushSupport.LinearGradient ) == enuBrushSupport.LinearGradient ;
            this.c_hatchBrushSelector.Enabled = (v_osupport & enuBrushSupport.Hatch ) == enuBrushSupport.Hatch ;

        }

        void m_owner_BrushSupportChanged(object sender, EventArgs e)
        {
            updateBrushSupport();
        }

        void _SelectedPageChanged(object sender, EventArgs e)
        {
            enuBrushType b =  GetBrushType();
            this.BrushType = b;
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // GCSColorNoteBook
            // 
            this.Name = "GCSColorNoteBook";
            this.Size = new System.Drawing.Size(471, 294);
            this.ResumeLayout(false);

        }

        internal IGKXNoteBookPage GetPage(enuBrushType brushType)
        {
            switch (brushType)
            {
                case enuBrushType.Custom:
                    break;
                case enuBrushType.Hatch:
                    return c_hatchBrushSelector;
                    
                case enuBrushType.LinearGradient:
                    return c_linearBrushSelector;
                case enuBrushType.PathGradient:
                    return c_pathBrushSelector;                
                case enuBrushType.Texture:
                    return c_textureBrushSelector;
                default:
                case enuBrushType.Solid:
                    return c_solidBrushSelector;
            }
            return null;
        }

        private enuBrushType GetBrushType()
        {
            if (this.SelectedPage.Tag!=null)
                return (enuBrushType)this.SelectedPage.Tag;
            return this.m_BrushType;
            
        }
    }
}
