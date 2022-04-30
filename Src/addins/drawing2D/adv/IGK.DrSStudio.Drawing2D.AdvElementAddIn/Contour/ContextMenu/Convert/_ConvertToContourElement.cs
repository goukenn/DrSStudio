

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: _ConvertToContourElement.cs
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
file:_ConvertToContourElement.cs
*/

using IGK.ICore.ContextMenu;
using IGK.DrSStudio.Drawing2D.Contour;
using IGK.ICore.Menu;
using System; using IGK.ICore; using IGK.ICore.Drawing2D;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace IGK.DrSStudio.Drawing2D.ContextMenu.Convert
{
    using IGK.DrSStudio.Drawing2D.ContextMenu;
    using IGK.ICore.WinUI;

    [IGKD2DConvertToContextMenuAttribute("ContourElement", 0xC001)]
    class _ConvertToContourElement : IGKD2DChildContextMenuBase 
    {
        public _ConvertToContourElement()
        {
            this.IsRootMenu = false;
        }
        protected override void InitContextMenu()
        {
            base.InitContextMenu();        
        }
        protected override bool IsEnabled()
        {
            return base.IsEnabled();
        }
        protected override bool IsVisible()
        {
            return base.IsVisible();
        }
        protected override bool PerformAction()
        {
            if (this.CurrentSurface.CurrentLayer.SelectedElements.Count == 1)
            {
                Core2DDrawingLayeredElement l = this.CurrentSurface.CurrentLayer.SelectedElements[0] as Core2DDrawingLayeredElement ;
                if ((l is ContourElement) == false)
                {
                    ContourElement c  =ContourElement.CreateElement(l);
                    if (c != null)
                    {
                        this.CurrentSurface.CurrentLayer.Elements.Remove(l);
                        this.CurrentSurface.CurrentLayer.Elements.Add(c);
                        //change the current to mecanism element
                        ICoreWorkingToolManagerSurface v_t = this.CurrentSurface as ICoreWorkingToolManagerSurface;
                        if (v_t !=null)
                            v_t.CurrentTool = c.GetType();
                        this.CurrentSurface.RefreshScene();
                        return true;
                    }
                }
            }
            return false;
        }
    }
}

