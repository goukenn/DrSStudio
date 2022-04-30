

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: IGKD2DDrawingLayerPropertyMenu.cs
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
file:IGKD2DDrawingLayerPropertyMenu.cs
*/
using IGK.ICore.WinCore;
using IGK.ICore.WinUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IGK.DrSStudio.Drawing2D.AddIn2DLayer.Menu.Layer;
using IGK.DrSStudio.Drawing2D.Actions;
using IGK.ICore.Drawing2D;

namespace IGK.DrSStudio.Drawing2D.Menu.Layer
{
    using IGK.ICore;
    [IGKD2DDrawingLayerMenuAttribute("Properties", 0xFFFF,
       SeparatorBefore = true,
       IsShortcutMenuChild = true,
       Shortcut = enuKeys.P)]
    class IGKD2DAddNewLayerPropertyMenu : IGKD2DDrawingLayerMenuBase, ICoreEditLayerPropertiesAction
    {
        private ICore2DDrawingLayer m_selectedLayer;
        protected override bool IsEnabled()
        {
            return (this.m_selectedLayer != null) || base.IsEnabled();
        }

        protected override bool PerformAction()
        {
            var s = new Size2i(420, 290);
            if (this.m_selectedLayer != null)
            {
                Workbench.ConfigureWorkingObject(this.m_selectedLayer, "title.editLayerProperty_1".R(
                    this.m_selectedLayer.Id
                    ), false, s);
            }
            else
                Workbench.ConfigureWorkingObject(this.CurrentLayer,"title.editLayerProperty_1".R(
                    this.CurrentLayer.Id), false, s);
            return false;
        }

        public ICore2DDrawingLayer Layer
        {
            get
            {
                return this.m_selectedLayer;
            }
            set
            {
                this.m_selectedLayer = value;
            }
        }
    }
}

