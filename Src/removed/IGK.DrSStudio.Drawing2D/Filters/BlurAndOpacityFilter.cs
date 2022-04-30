

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: BlurAndOpacityFilter.cs
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
file:BlurAndOpacityFilter.cs
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
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;
namespace IGK.DrSStudio.Drawing2D.Filters
{
    [Core2DDrawingFilterAttribute()]
    public sealed class BlurAndOpacityFilter : Core2DDrawingFilterBase 
    {
        private float m_Opacity;
        private float m_Smooth;
        private enuBlurFilterType  m_FilterType;
        private bool m_Expended;
        private InterpolationMode m_InputInterpolation;
        /// <summary>
        /// Get or set the input interpolation
        /// </summary>
        public InterpolationMode InputInterpolation
        {
            get { return m_InputInterpolation; }
            set
            {
                if (m_InputInterpolation != value)
                {
                    m_InputInterpolation = value;
                    OnFilterPropertyChanged(EventArgs.Empty);
                }
            }
        }
        /// <summary>
        /// get or set if set is exempended
        /// </summary>
        public bool Expended
        {
            get { return m_Expended; }
            set
            {
                if (m_Expended != value)
                {
                    m_Expended = value;
                    OnFilterPropertyChanged(EventArgs.Empty);
                }
            }
        }
        public enuBlurFilterType FilterType
        {
            get { return m_FilterType; }
            set
            {
                if (m_FilterType != value)
                {
                    m_FilterType = value;
                    OnFilterPropertyChanged(EventArgs.Empty);
                }
            }
        }
        public float Smooth
        {
            get { return m_Smooth; }
            set
            {
                if ((m_Smooth != value) && AuthorizeSmooth(value ))
                {
                    m_Smooth = value;
                    OnFilterPropertyChanged(EventArgs.Empty);
                }
            }
        }
        private bool AuthorizeSmooth(float value)
        {
            switch ((int)value)
            {
                case 0:
                case 2:
                case 4:
                case 8:
                case 16:
                case 32:
                case 64:
                case 100:
                    return true;
            }
            return false;
        }
        public float Opacity
        {
            get { return m_Opacity; }
            set
            {
                if ((m_Opacity != value) && (value >= 0.0f) && (value <=1.0f))
                {
                    m_Opacity = value;
                    OnFilterPropertyChanged(EventArgs.Empty);
                }
            }
        }
        public override bool Activated
        {
            get {
                return ((this.Opacity != 1.0f) || (this.Smooth != 0.0f));
            }
        }
        public BlurAndOpacityFilter()
        {
            this.m_Opacity = 1.0f;
            this.m_Smooth = 0.0f;
            this.m_InputInterpolation = InterpolationMode.Default;
        }
        //public bool ApplyFilter(ICore2DDrawingLayeredElement element, System.Drawing.Bitmap bitmap)
        //{
        //    if (!this.Activated )
        //        return false ;          
        //    return true ;
        //}
        public override bool ApplyFilter(ref Bitmap bmp)
        {
            if (!this.Activated)
                return false;
            try
            {
                Bitmap cbmp = bmp;
                bmp = CoreBitmapOperation.ApplyColorMatrix(bmp,
                     new System.Drawing.Imaging.ColorMatrix(new float[][]{
                     new float []{1, 0, 0, 0, 0},
                     new float []{0, 1, 0, 0, 0},
                     new float []{0, 0, 1, 0, 0},
                     new float []{0, 0, 0, this.Opacity , 0},
                     new float []{0, 0, 0, 0, 1},
                 }));
                cbmp.Dispose();
                cbmp = bmp;
                if (this.m_Smooth > 0)
                {
                    switch (this.FilterType)
                    {
                        case enuBlurFilterType.Smooth:
                            bmp = CoreBitmapOperation.SmoothBitmap(bmp,
                                this.m_Smooth,
                                this.InputInterpolation ,
                                CompositingQuality.HighQuality,
                                InterpolationMode.HighQualityBilinear,
                                CompositingQuality.HighQuality,
                                this.Expended);
                            break;
                        case enuBlurFilterType.Pixelise :
                            bmp = CoreBitmapOperation.SmoothBitmap(bmp,
                                this.m_Smooth,
                                this.InputInterpolation,
                                CompositingQuality.HighQuality,
                                InterpolationMode.NearestNeighbor ,
                                CompositingQuality.HighQuality,
                                this.Expended );
                            break;
                    }
                    if (bmp !=cbmp )
                    cbmp.Dispose();
                }
            }
            catch(Exception ex)
            {
                CoreLog.WriteDebug(ex.Message);
            }
            return true;
        }
        public override string Name
        {
            get { return CoreConstant.FILTER_SMOOTHING; }
        }
        protected override void SaveAttributes(IGK.DrSStudio.Codec.IXMLSerializer xwriter)
        {
            base.SaveAttributes(xwriter);
            xwriter.WriteAttributeString("Opacity", this.Opacity .ToString());
            xwriter.WriteAttributeString("SmoothType", this.FilterType.ToString());
            xwriter.WriteAttributeString("Smooth", this.Smooth.ToString());
            xwriter.WriteAttributeString("Expended", this.Expended.ToString());
            xwriter.WriteAttributeString("InputInterpolation", this.InputInterpolation.ToString());
        }
        protected override void ReadAttributes(IGK.DrSStudio.Codec.IXMLDeserializer xreader)
        {
            base.ReadAttributes(xreader);
            this.Smooth = Convert.ToSingle (xreader.GetAttribute("Smooth"));
            this.Opacity = Convert.ToSingle(xreader.GetAttribute("Opacity"));
            string v_str = xreader.GetAttribute("InputInterpolation");
            if (!string.IsNullOrEmpty(v_str))
            {
                this.InputInterpolation = (InterpolationMode)Enum.Parse(typeof(InterpolationMode), xreader.GetAttribute("InputInterpolation"));
            }
            string s = xreader.GetAttribute("SmoothType");
            if (!string.IsNullOrEmpty(s))
            {
                this.FilterType = (enuBlurFilterType)Enum.Parse (typeof(enuBlurFilterType), s);
            }
            this.m_Expended = Convert.ToBoolean(xreader.GetAttribute("Expended"));           
        }
    }
}

