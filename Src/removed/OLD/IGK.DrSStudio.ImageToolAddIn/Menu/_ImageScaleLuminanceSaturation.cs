

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: _ImageScaleLuminanceSaturation.cs
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
file:_ImageScaleLuminanceSaturation.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace IGK.DrSStudio.Drawing2D.Menu
{
    using IGK.ICore;using IGK.DrSStudio.WinUI;
    using IGK.DrSStudio.WinUI.Configuration;
    [IGK.DrSStudio.Menu.CoreMenu("Image.Tools.SLS", 4)]
    class _ImageScaleLuminanceSaturation : ImageMenuBase, ICoreWorkingConfigurableObject 
    {
        private System.Drawing.Bitmap m_OldBitmap;
        public System.Drawing.Bitmap OldBitmap
        {
            get { return m_OldBitmap; }
        }
        private float m_Luminosity;
        private float m_Scale;
        private float m_Saturation;
        public float Saturation
        {
            get { return m_Saturation; }
            set
            {
                if (m_Saturation != value)
                {
                    m_Saturation = value;
                }
            }
        }
        public float Scale
        {
            get { return m_Scale; }
            set
            {
                if (m_Scale != value)
                {
                    m_Scale = value;
                }
            }
        }
        public float Luminosity
        {
            get { return m_Luminosity; }
            set
            {
                if (m_Luminosity != value)
                {
                    m_Luminosity = value;
                }
            }
        }
        protected override bool PerformAction()
        {
            this.m_OldBitmap = this.ImageElement.Bitmap.Clone() as System.Drawing.Bitmap;
            if (Workbench.ConfigureWorkingObject(this) == enuDialogResult.OK)
            {
                this.Update(false);
            }
            else {
                //restore the old bitmap
                this.ImageElement.SetBitmap(this.OldBitmap, true);
            }
            return base.PerformAction();
        }
        #region ICoreWorkingConfigurableObject Members
        public IGK.DrSStudio.WinUI.Configuration.enuParamConfigType GetConfigType()
        {
            return enuParamConfigType.ParameterConfig;
        }
        public IGK.DrSStudio.WinUI.ICoreParameterConfigCollections GetParameters(IGK.DrSStudio.WinUI.ICoreParameterConfigCollections parameters)
        {
            var group = parameters.AddGroup("Default");
            group.AddTrackbar("Scale", "lb.Scale", 0, 100, 100, propChanged);
            group.AddTrackbar("Luminance", "lb.Luminance", 0, 100, 100, propChanged);
            group.AddTrackbar("Saturation", "lb.Saturation", 0, 100, 100, propChanged);
            return parameters;
        }
        void propChanged(object sender, IGK.DrSStudio.WinUI.Configuration.CoreParameterChangedEventArgs e)
        {
            switch (e.Item.Name.ToLower())
            {
                case "scale": this.m_Scale = Convert.ToSingle (e.Value ) / 100.0f;
                    break;
                case "luminance": this.Luminosity = Convert.ToSingle(e.Value) / 100.0f;
                    break;
                case "saturation": this.m_Saturation = Convert.ToSingle(e.Value) / 100.0f;
                    break;
            }
            this.Update(true);
        }
        private void Update(bool temp)
        {
            //this.ImageElement.Bitmap.Transform(
            //this.ImageElement.SetBitmap(
            //    WinCoreBitmapOperation.ApplyColorMatrix(this.OldBitmap,
            //    WinCoreBitmapOperation.GetAdjustmentMatrix(this.Scale, this.Luminosity, this.Saturation)), temp);
            this.ImageElement.Invalidate(true);
        }
        public ICoreControl GetConfigControl()
        {
            return null;
        }
        #endregion
    }
}

