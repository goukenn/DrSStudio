

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: ConvertToAnchorItem.cs
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
file:ConvertToAnchorItem.cs
*/
using IGK.ICore;using IGK.DrSStudio.Drawing2D.Disposition.WorkingObjects;
using IGK.DrSStudio.Drawing2D.Menu;
using IGK.DrSStudio.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace IGK.DrSStudio.Drawing2D.Disposition.Menu.Edit
{
    [CoreMenu("Edit.ConvertTo.AnchorItem", 0x4400)]
    class ConvertToAnchorItem : Core2DDrawingMenuBase
    {
        protected override bool PerformAction()
        {
            Core2DDrawingLayeredElement[] v_t= this.CurrentSurface.CurrentLayer.SelectedElements.ToArray().ConvertTo<Core2DDrawingLayeredElement>();
            if ((v_t != null) && v_t.Length > 0)
            {
                var v_m = v_t.Where( (i, b) => !(i is AnchorElement ) );
                if (v_m.Count() > 0)
                {
                    this.CurrentSurface.CurrentLayer.Select(null);
                    foreach (var item in v_m)
                    {
                        this.CurrentSurface.CurrentLayer.Elements.Remove(item);
                    }
                    AnchorElement h = AnchorElement.CreateElement(v_m.ToArray());
                    if (h!=null)
                    this.CurrentSurface.CurrentLayer.Elements.Add(h);
                }
            }
            return base.PerformAction();
        }
    }
}

