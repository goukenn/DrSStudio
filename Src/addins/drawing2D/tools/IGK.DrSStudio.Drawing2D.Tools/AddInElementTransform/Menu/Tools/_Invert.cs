

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: _Invert.cs
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
file:_Invert.cs
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
    [DrSStudioMenu("Tools.Invert",
     21)]
    class _Invert : Core2DDrawingMenuBase 
    {
        public _Invert()
        {
        }
        [DrSStudioMenu("Tools.Invert.X",
 0,
 ImageKey = CoreImageKeys.BTN_2DALIGN_INVERTX_GKDS)]
        sealed class ChilMenuInvertX : Core2DDrawingMenuBase
        {
            protected override bool PerformAction()
            {
                ICore2DDrawingLayeredElement[] elements = CurrentSurface.CurrentLayer.SelectedElements.ToArray();
                Rectanglef docBound = this.CurrentSurface.CurrentDocument.Bounds;
                for (int i = 0; i < elements.Length; i++)
                {
                    elements[i].FlipX();
                }
                this.CurrentSurface.RefreshScene();
                return false;
            }
        }
        [DrSStudioMenu("Tools.Invert.Y",
0,
ImageKey=CoreImageKeys.BTN_2DALIGN_INVERTY_GKDS)]
        sealed class ChilMenuInvertY : Core2DDrawingMenuBase
        {
            protected override bool PerformAction()
            {
                ICore2DDrawingLayeredElement[] elements = CurrentSurface.CurrentLayer.SelectedElements.ToArray();
                Rectanglef docBound = this.CurrentSurface.CurrentDocument.Bounds;
                for (int i = 0; i < elements.Length; i++)
                {
                    elements[i].FlipY();
                }
                this.CurrentSurface.RefreshScene();
                return false;
            }
        }
    }
}
