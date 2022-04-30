

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: Menu_ConvertTextToCircleLine.cs
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
file:Menu_ConvertTextToCircleLine.cs
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
using System.Drawing;
using System.Drawing.Drawing2D;
namespace IGK.DrSStudio.CircleTextAddin.Menu
{
    using IGK.ICore;using IGK.DrSStudio;
    using IGK.DrSStudio.Menu ;
    using IGK.DrSStudio.Drawing2D;
    using IGK.DrSStudio.Drawing2D.Menu;
    [CoreMenu ("Edit.ConvertTo.ConvertTextToCircleLine",100)]
    public sealed class Menu_ConvertTextToCircleLine :  Core2DMenuBase  
    {       
        protected override bool PerformAction()
        {
            TextElement _text = (this.CurrentSurface.CurrentLayer.SelectedElements[0] as TextElement);
            if (_text != null)
            {
                Core2DDrawingMecanismBase v_mecanism = this.CurrentSurface.Mecanism as
                    Core2DDrawingMecanismBase;
                if (v_mecanism != null)
                {
                    CirclePathTextElement.Mecanism mecanism = new CirclePathTextElement.Mecanism(_text,
                        v_mecanism );
                    //this.CurrentSurface.Mecanism = mecanism;
                }
            }
            return false;
        }
        protected override bool IsEnabled()
        {    
            if ((this.CurrentSurface != null) &&(this.CurrentSurface.CurrentLayer.SelectedElements.Count == 1) &&
                (this.CurrentSurface.CurrentLayer.SelectedElements[0] is TextElement))             
            {
                Type t = this.CurrentSurface.CurrentTool;
             if (t == typeof (SelectionElement ))
                return true;
            }
            return false;
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
            this.SetupEnableAndVisibility();
        }
    }
}

