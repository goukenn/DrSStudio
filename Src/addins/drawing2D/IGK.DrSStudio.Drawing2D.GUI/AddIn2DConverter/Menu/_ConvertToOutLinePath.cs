

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: _ConvertToOutLinePath.cs
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
file:_ConvertToOutLinePath.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Reflection;
using System.Drawing;
using System.Drawing.Drawing2D ;
using IGK.ICore.WinCore;
using IGK.ICore.GraphicModels;
namespace IGK.DrSStudio.Drawing2D.Menu
{
    using IGK.ICore.WinUI;
    using IGK.DrSStudio.Menu;
    using IGK.ICore.Drawing2D;
    using IGK.ICore;
    using IGK.ICore.Drawing2D.Menu;
    [DrSStudioMenu(ConverterConstant.MENU_CONVERTTO_OUTLINEPATH, 20)]
    class _ConvertToOutLinePath : 
        Core2DDrawingMenuBase 
    {
        // Declaration required for interop
        [DllImport(@"gdiplus.dll")]
        public static extern int GdipWindingModeOutline(HandleRef path, IntPtr matrix, float flatness);
        protected override bool PerformAction()
        {
            if (this.CurrentSurface.CurrentLayer.SelectedElements.Count < 1)
                return false ;
            CoreGraphicsPath v_path = new CoreGraphicsPath();
            foreach(ICore2DDrawingLayeredElement l in  this.CurrentSurface.CurrentLayer .SelectedElements)
            {
                ICoreGraphicsPath v_c = l.GetPath();
                if (v_c == null) continue;
                v_path.Add(v_c);
            }
            CoreGraphicsPath path = GetOutlinePath (v_path);
            PathElement vp = PathElement.CreateElement(path);
            this.CurrentSurface.CurrentLayer.Elements.Add(vp);
            return false ;
        }
        public static CoreGraphicsPath GetOutlinePath(CoreGraphicsPath path)
        {
            GraphicsPath c = path.WinCoreToGdiGraphicsPath();

            HandleRef handle = new HandleRef(c, (IntPtr)c.GetType().GetField("nativePath", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(c));
            // Change path so it only contains the outline
            GdipWindingModeOutline(handle, IntPtr.Zero, 0.25F);
            byte[] t = c.PathTypes;
            Vector2f[] vt = c.PathPoints.CoreConvertFrom<Vector2f[]>();
            c.Dispose();

            CoreGraphicsPath h = new CoreGraphicsPath();
            h.AddDefinition(vt, t);
            return h;
        }
    }
}

