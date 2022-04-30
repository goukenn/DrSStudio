

/*
IGKDEV @ 2008 - 2014
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
IGKDEV - [2008 - 2014]
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
namespace IGK.DrSStudio.Drawing2D.Menu
{
    [IGK.DrSStudio.Menu.CoreMenu("Edit.ConvertTo.OutlinePath", 20)]
    class _ConvertToOutLinePath :  Core2DMenuBase 
    {
        // Declaration required for interop
        [DllImport(@"gdiplus.dll")]
        public static extern int GdipWindingModeOutline(HandleRef path, IntPtr matrix, float flatness);
        protected override bool PerformAction()
        {
            if (this.CurrentSurface.CurrentLayer.SelectedElements.Count < 1)
                return false ;
            GraphicsPath v_path = new GraphicsPath ();
            foreach(ICore2DDrawingLayeredElement l in  this.CurrentSurface.CurrentLayer .SelectedElements)
            {
                v_path.AddPath (l.GetPath (), false );
            }
            System.Drawing.Drawing2D.GraphicsPath path = GetOutlinePath (v_path);
            PathElement vp = PathElement.Create(path);
            this.CurrentSurface.CurrentLayer.Elements.Add(vp);
            return false ;
        }
        public static GraphicsPath GetOutlinePath(GraphicsPath path)
        {
            HandleRef handle = new HandleRef(path, (IntPtr)path.GetType().GetField("nativePath", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(path));
            // Change path so it only contains the outline
            GdipWindingModeOutline(handle, IntPtr.Zero, 0.25F);
            return path;
        }
    }
}

