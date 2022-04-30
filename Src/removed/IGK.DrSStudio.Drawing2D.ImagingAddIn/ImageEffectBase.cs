

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: ImageEffectBase.cs
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
file:ImageEffectBase.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace IGK.DrSStudio.Drawing2D
{
    using IGK.ICore;using IGK.DrSStudio.WinUI.Configuration;
    /// <summary>
    /// represent a basic image effect
    /// </summary>
    public abstract class ImageEffectBase : IImagingEffect , IDisposable 
    {
        private ImageElement m_ImageElement;
        private System.Drawing.Bitmap  m_OldBitmap;
        /// <summary>
        /// get the default bitmap data
        /// </summary>
        protected System.Drawing.Bitmap  OldBitmap
        {
            get { return m_OldBitmap; }
        }
        /// <summary>
        /// get the image element
        /// </summary>
        public ImageElement ImageElement
        {
            get { return m_ImageElement; }
            set
            {
                if (m_ImageElement != value)
                {
                    this.DisposeOldBitmap();
                    m_ImageElement = value;
                    if (value != null)
                    {
                        try
                        {
                            this.m_OldBitmap = CoreBitmapOperation.ConvertToCoreBitmap(value.Bitmap) as System.Drawing.Bitmap;
                           // this.ApplyEffect(true);
                        }
                        catch
                        {
                            this.m_OldBitmap = null;
                            CoreLog.WriteDebug("Unable to get bitmap");
                        }
                    }
                    OnImageElementChanged(EventArgs.Empty);
                }
            }
        }
        protected virtual void OnImageElementChanged(EventArgs eventArgs)
        {
        }
        #region IImagingEffect Members
        public abstract string EffectName
        {
            get;
        }
        #endregion
        #region ICoreWorkingConfigurableObject Members
        public virtual enuParamConfigType GetConfigType()
        {
            return enuParamConfigType.ParameterConfig;
        }
        public virtual  IGK.DrSStudio.WinUI.ICoreParameterConfigCollections GetParameters(IGK.DrSStudio.WinUI.ICoreParameterConfigCollections parameters)
        {
            return parameters ;
        }
        public System.Windows.Forms.Control GetConfigControl()
        {
            throw new NotImplementedException();
        }
        #endregion
        #region ICoreIdentifier Members
        string ICoreIdentifier .Id
        {
            get { return this.EffectName; }
        }
        #endregion
        public void ApplyEffect()
        {
            if (this.ImageElement != null)
                this.ApplyEffect(false);
        }
        /// <summary>
        /// apply effect to the image element
        /// </summary>
        protected abstract void ApplyEffect(bool temp);
        #region IDisposable Members
        public virtual void Dispose()
        {
        }
        private void DisposeOldBitmap()
        {
            if (this.m_OldBitmap != null)
            {
                this.m_OldBitmap.Dispose();
                this.m_OldBitmap = null;
            }
        }
        #endregion
        public event CoreWorkingObjectPropertyChangedEventHandler PropertyChanged;
        /// <summary>
        /// raise the property changed action 
        /// </summary>
        /// <param name="o"></param>
        /// <param name="e"></param>
        protected void OnPropertyChanged(CoreWorkingObjectPropertyChangedEventArgs e) {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, e);
            }
        }
    }
}

