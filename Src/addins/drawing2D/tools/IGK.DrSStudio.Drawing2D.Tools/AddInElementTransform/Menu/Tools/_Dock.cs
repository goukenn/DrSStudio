

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: _Dock.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/

using IGK.ICore.WinCore;

using IGK.ICore;
using IGK.ICore.Menu;
using IGK.ICore.WinUI;
using IGK.ICore.Drawing2D;
using IGK.ICore.Drawing2D.Menu;
using IGK.ICore.Drawing2D.WinUI;
using IGK.DrSStudio;
using IGK.DrSStudio.Menu;
using IGK.DrSStudio.WinUI;
using IGK.DrSStudio.Drawing2D.Menu;
using IGK.DrSStudio.Drawing2D.WinUI;
/*
IGKDEV @ 2008-2016
author: C.A.D . BONDJE DOUE
file:_Dock.cs
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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace IGK.DrSStudio.ElementTransform.Menu.Tools
{
    using IGK.DrSStudio.Drawing2D;
    [DrSStudioMenu("Tools.Dock",
     21)]
    class _Dock : Core2DDrawingMenuBase 
    {
        public _Dock()
        {
            //register dock child 
            foreach (enuCore2DDockElement  item in Enum.GetValues(typeof(enuCore2DDockElement )))
            {
                CoreMenuAttribute v_attr = new CoreMenuAttribute(string.Format(this.Id + ".{0}", item.ToString()), (int)item);
                v_attr.ImageKey = string.Format("btn_2DDock_{0}", item.ToString());
                ChildMenu ch = new ChildMenu(item);
                ch.SetAttribute(v_attr);
                this.Childs.Add(ch);
                if (this.Register(v_attr, ch) == false )
                { 
#if DEBUG
                    CoreMessageBox.Show("Menu Not registrated : [" + v_attr.Name + "]");
#endif 
                    continue;
                }                
            }
        }
        sealed class ChildMenu : Core2DDrawingMenuBase
        {
            internal enuCore2DDockElement dock;
            internal ChildMenu(enuCore2DDockElement  dock )
            {
                this.dock = dock;
            }
            protected override bool PerformAction()
            {
                ICore2DDrawingLayeredElement[] elements = CurrentSurface.CurrentLayer.SelectedElements.ToArray();
                Rectanglef docBound = this.CurrentSurface.CurrentDocument.Bounds;
                foreach (Core2DDrawingLayeredElement item in elements)
                {
                    if (item == null) continue;   
                    item.Dock(dock);
                }
                this.CurrentSurface.RefreshScene();
                return false;
            }
         
            public override string ToString()
            {
                return "Dock : " + this.dock;
            }
        }
    }
}

