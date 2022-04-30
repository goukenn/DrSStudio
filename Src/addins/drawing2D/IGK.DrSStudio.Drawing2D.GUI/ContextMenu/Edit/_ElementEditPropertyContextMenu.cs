

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: _ElementEditPropertyContextMenu.cs
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
file:_ElementProperty.cs
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
namespace IGK.DrSStudio.Drawing2D.ContextMenu.Edit
{
    using IGK.ICore.WinCore;
using IGK.DrSStudio.ContextMenu;
    using IGK.ICore;
    using IGK.ICore.Drawing2D;
    using IGK.ICore.WinUI;
    using IGK.ICore.WinUI.Configuration;

    /*
     * 
     * 
     * configure a property of an element. you must be over that element to edit it
     * 
     * */
    /// <summary>
    /// 
    /// </summary>
    [DrSStudioContextMenu("Drawing2D.ElementProperty", 
        0xFFFF, 
        ShortCut = enuKeys.Control | enuKeys.E,
        SeparatorBefore=true
        )]
    sealed class _ElementEditPropertyContextMenu : IGKD2DDrawingContextMenuBase
    {
        ICoreWorkingConfigurableObject m_element;
        ICoreWorkingConfigurableObject Element {
            get {
                return m_element;
            }
            set {
                this.m_element = value;
            }
        }
        public _ElementEditPropertyContextMenu()
        {

        }
        

        protected override bool IsVisible()
        {
            return base.IsVisible() && (this.Element != null) &&  this.CheckOverSingleElement();
        }
        protected override bool IsEnabled()
        {
            return this.IsVisible();
        }
      
        protected override bool PerformAction()
        {
            if (this.Element != null)            
            {
                (this.Element as ICore2DDrawingLayeredElement).PropertyChanged += new CoreWorkingObjectPropertyChangedEventHandler(_ElementProperty_PropertyChanged);
                this.Workbench.ConfigureWorkingObject(this.Element, "title.editElementProperty".R(), false, 
                    new Size2i(415,620));
                (this.Element as ICore2DDrawingLayeredElement).PropertyChanged -= new CoreWorkingObjectPropertyChangedEventHandler(_ElementProperty_PropertyChanged);
            }
            return false;
        }
        void _ElementProperty_PropertyChanged(object o, CoreWorkingObjectPropertyChangedEventArgs e)
        {
            this.CurrentSurface.RefreshScene();
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
            if (l.SelectedElements.Count == 1)
            {
                this.Element   =l.SelectedElements[0] as ICoreWorkingConfigurableObject;
            }
            else 
                this.Element = null;
        }
        protected override void InitContextMenu()
        {
            base.InitContextMenu();
        }
    }
}

