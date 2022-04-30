

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: IGKD2DImageElementToolBase.cs
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
file:IGKD2DImageElementToolBase.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace IGK.DrSStudio.Drawing2D.Tools
{
    using IGK.ICore.WinCore;
using IGK.ICore;
    using IGK.ICore.Drawing2D;
    using IGK.ICore.Drawing2D.Tools;
    using IGK.ICore.Drawing2D.WinUI;
    using IGK.ICore.WinUI;
    /// <summary>
    /// represent base abstart image element
    /// </summary>
    public abstract class IGKD2DImageElementToolBase : Core2DDrawingToolBase 
	{
        private ImageElement m_ImageElement;
        /// <summary>
        /// get or set the image element
        /// </summary>
        public ImageElement ImageElement
        {
            get { return m_ImageElement; }
            protected set
            {
                if (m_ImageElement != value)
                {
                    if (this.m_ImageElement !=null) UnregisterImageEvent();
                    m_ImageElement = value;
                    if (value!=null) RegisterImageEvent();
                }
            }
        }
        protected virtual  void RegisterImageEvent()
        {
            this.ImageElement.PropertyChanged += new CoreWorkingObjectPropertyChangedEventHandler(ImageElement_PropertyChanged);
        }
        void ImageElement_PropertyChanged(object o, CoreWorkingObjectPropertyChangedEventArgs e)
        {
            OnImagePropertyChanged(e);
        }
        protected virtual void OnImagePropertyChanged(CoreWorkingObjectPropertyChangedEventArgs e)
        {
        }
        protected virtual void UnregisterImageEvent()
        {
            this.ImageElement.PropertyChanged -= new CoreWorkingObjectPropertyChangedEventHandler(ImageElement_PropertyChanged);
        }
        protected override void OnCurrentSurfaceChanged(CoreWorkingElementChangedEventArgs<ICoreWorkingSurface> e)
        {
            if (e.OldElement  is ICore2DDrawingSurface)
                UnRegisterSurfaceEvent(e.OldElement as ICore2DDrawingSurface);
            else if (e.OldElement is ICoreWorkingConfigElementSurface)
                UnRegisterSurfaceEvent(e.OldElement as ICoreWorkingConfigElementSurface);
            if (e.NewElement  is ICore2DDrawingSurface)
                RegisterSurfaceEvent(e.NewElement as ICore2DDrawingSurface);
            else if (e.NewElement is ICoreWorkingConfigElementSurface)
                RegisterSurfaceEvent(e.NewElement as ICoreWorkingConfigElementSurface);
            else
                this.ImageElement = null;
        }
        private void RegisterSurfaceEvent(ICoreWorkingConfigElementSurface iCoreWorkingConfigElementSurface)
        {
            iCoreWorkingConfigElementSurface.ElementToConfigureChanged += new EventHandler(iCoreWorkingConfigElementSurface_ElementToConfigureChanged);
            this.ImageElement = iCoreWorkingConfigElementSurface.ElementToConfigure  as ImageElement ;
        }
        void iCoreWorkingConfigElementSurface_ElementToConfigureChanged(object sender, EventArgs e)
        {
            ICoreWorkingConfigElementSurface s = sender as ICoreWorkingConfigElementSurface;
            this.ImageElement = s.ElementToConfigure as ImageElement;
        }
        private void UnRegisterSurfaceEvent(ICoreWorkingConfigElementSurface iCoreWorkingConfigElementSurface)
        {
            iCoreWorkingConfigElementSurface.ElementToConfigureChanged -= new EventHandler(iCoreWorkingConfigElementSurface_ElementToConfigureChanged);
        }
	}
}

