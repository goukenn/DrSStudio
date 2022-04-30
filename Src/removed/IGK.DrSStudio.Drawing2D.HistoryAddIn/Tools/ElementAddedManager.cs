

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: ElementAddedManager.cs
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
file:ElementAddedManager.cs
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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace IGK.DrSStudio.Drawing2D.Tools
{
    using IGK.ICore;using IGK.DrSStudio.Tools;
    using IGK.DrSStudio.Drawing2D.Tools;
    using IGK.DrSStudio.Drawing2D;
    class ElementAddedManager : 
        HistoryToolManagerBase
    {
        internal ElementAddedManager(HistorySurfaceManager tool):base(tool)
        {
        }
        protected override void RegisterLayerEvent(ICore2DDrawingLayer layer)
        {            
            base.RegisterLayerEvent(layer);
            layer.ElementAdded += new Core2DDrawingElementEventHandler (layer_ElementAdded);
            layer.ElementRemoved += new Core2DDrawingElementEventHandler(layer_ElementRemoved);
        }
        protected override void UnRegisterLayerEvent(ICore2DDrawingLayer layer)
        {            
            base.UnRegisterLayerEvent(layer);
            layer.ElementAdded -= new Core2DDrawingElementEventHandler(layer_ElementAdded);
            layer.ElementRemoved -= new Core2DDrawingElementEventHandler(layer_ElementRemoved);
        }
        void layer_ElementRemoved(object o, Core2DDrawingElementEventArgs e)
        {
            if (Tools.HistorySurfaceManager.Instance.CanAdd)
            {
                Core2DDrawingLayeredElement[] t = new Core2DDrawingLayeredElement[]{e.Element as
                Core2DDrawingLayeredElement};
                Tools.HistorySurfaceManager.Instance.Add(new HistoryActions._ItemRemoved(
                    o as Core2DDrawingLayer,
                    t));
            }
        }
        void layer_ElementAdded(object o, Core2DDrawingElementEventArgs e)
        {
            if (Tools.HistorySurfaceManager.Instance.CanAdd)
            {
                Tools.HistorySurfaceManager.Instance.Add(new HistoryActions._ItemAdded(
                    o as Core2DDrawingLayer, e.Element as Core2DDrawingLayeredElement 
                    ));
            }
        }
    }
}

