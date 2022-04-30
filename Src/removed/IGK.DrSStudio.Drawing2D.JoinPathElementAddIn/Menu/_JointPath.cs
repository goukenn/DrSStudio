

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: _JointPath.cs
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
file:_JointPath.cs
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
using System.Drawing.Drawing2D;
namespace IGK.DrSStudio.Drawing2D.Menu
{
    /// <summary>
    /// joint multiple path
    /// </summary>
    [IGK.DrSStudio.Menu.CoreMenu ("Tools.Drawing2DPath.Joint", 4, ImageKey="Menu_Joint")]
    class _JointPath : Core2DMenuBase 
    {
        protected override bool PerformAction()
        {
            if (this.CurrentSurface.CurrentDocument.CurrentLayer.SelectedElements.Count < 2)
                return false ;
            List<GraphicsPath> v_path = new List<GraphicsPath>();
            int i = 0;
            foreach (ICore2DDrawingLayeredElement  l in this.CurrentSurface .CurrentDocument.CurrentLayer.SelectedElements )
            {
                    v_path.Add(l.GetPath().Clone() as GraphicsPath);
                    if (i == 1)
                    {
                        v_path[v_path.Count-1].Reverse();
                        i = 0;
                    }                    
                    i = 1;            
            }
            if (v_path.Count > 1)
            {
                GraphicsPath v_p = EnterJoindMode.Joint(v_path.ToArray());
                this.CurrentSurface.CurrentDocument.CurrentLayer.Elements .Add (
                    PathElement.Create(v_p)
                    );
            }
            return base.PerformAction();
        }
        protected override bool IsEnabled()
        {
            return base.IsEnabled () && (this.CurrentSurface.CurrentDocument.CurrentLayer.SelectedElements.Count >= 2);                
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

