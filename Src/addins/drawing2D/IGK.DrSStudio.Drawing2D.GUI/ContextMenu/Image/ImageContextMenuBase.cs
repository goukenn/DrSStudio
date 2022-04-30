

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: ImageContextMenuBase.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
using IGK.ICore.WinCore;
using IGK.ICore;using IGK.ICore.Drawing2D;
/*
IGKDEV @ 2008-2016
author: C.A.D . BONDJE DOUE
file:ImageContextMenuBase.cs
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
namespace IGK.DrSStudio.Drawing2D.ContextMenu.Image
{
    /// <summary>
    /// represent the base class of the context menu image element
    /// </summary>
    public abstract class ImageContextMenuBase : IGKD2DDrawingContextMenuBase 
    {
        private ImageElement m_ImageElement;
        public ImageElement ImageElement
        {
            get { return m_ImageElement; }
            set
            {
                if (m_ImageElement != value)
                {
                    m_ImageElement = value;
                    OnImageElementChanged(EventArgs.Empty);
                }
            }
        }
        public event EventHandler ImageElementChanged;
        protected virtual void OnImageElementChanged(EventArgs eventArgs)
        {
            if (this.ImageElementChanged != null)
                this.ImageElementChanged(this, eventArgs);
        }
        protected override void RegisterLayerEvent(ICore2DDrawingLayer layer)
        {
            base.RegisterLayerEvent(layer);
            layer.SelectedElementChanged += new EventHandler(layer_SelectedElementChanged);
        }
        protected override void UnRegisterLayerEvent(ICore2DDrawingLayer layer)
        {
            layer.SelectedElementChanged -= new EventHandler(layer_SelectedElementChanged);
            base.UnRegisterLayerEvent(layer);
        }
        void layer_SelectedElementChanged(object sender, EventArgs e)
        {
            ICore2DDrawingLayer l = sender as ICore2DDrawingLayer;
            if ((l.SelectedElements.Count == 1) && (l.SelectedElements[0] is ImageElement))
            {
                this.ImageElement = l.SelectedElements[0] as ImageElement;
            }
            else
                this.ImageElement = null;
        }
        protected override void OnOpening(EventArgs e)
        {
            this.Visible = 
                (this.OwnerContext.SourceControl == this.CurrentSurface)
                && (this.ImageElement != null)
                && (this.AllowContextMenu );
            this.Enabled = this.Visible;
        }
    }
}

