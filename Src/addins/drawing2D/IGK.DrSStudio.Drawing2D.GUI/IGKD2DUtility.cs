

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: IGKD2DUtility.cs
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
file:IGKD2DUtility.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
namespace IGK.DrSStudio.Drawing2D
{
    using IGK.ICore.WinCore;
using IGK.ICore;using IGK.ICore.WinUI ;
    using IGK.ICore.Drawing2D.WinUI ;
    /// <summary>
    /// represent a utility class
    /// </summary>
    static class IGKD2DUtility
    {
        /// <summary>
        /// split path element
        /// </summary>
        /// <param name="element"></param>
        /// <param name="surface"></param>
        /// <returns></returns>
        //internal static bool PathSplitPath(PathElement element, ICore2DDrawingSurface surface)
        //{
        //    CoreGraphicsPath v_gpath = element.GetPath();
        //    List<GraphicsPath> c_paths = new List<GraphicsPath>();
        //    List<Vector2f> c_pts = new List<Vector2f>();
        //    List<byte> c_type = new List<byte>();
        //    //Vector2f[] v_pt = v_gpath.GetAllDefinition(.PathPoints;
        //    //byte[] v_type = v_gpath.PathTypes;
        //    GraphicsPath c_path = null;
        //    ICoreBrush cbrush = element.GetBrush(enuBrushMode.Stroke);
        //    ICoreBrush fcbrush = element.GetBrush(enuBrushMode.Stroke);
        //    for (int i = 0; i < v_pt.Length; i++)
        //    {
        //        if ((i != 0) && (v_type[i] == 0))
        //        {
        //            c_path = new GraphicsPath(c_pts.ToArray(), c_type.ToArray());
        //            c_paths.Add(c_path);
        //            c_pts.Clear();
        //            c_type.Clear();
        //        }
        //        c_type.Add(v_type[i]);
        //        c_pts.Add(v_pt[i]);
        //    }
        //    if (c_type.Count > 1)
        //    {
        //        c_path = new GraphicsPath(c_pts.ToArray(), c_type.ToArray());
        //        c_paths.Add(c_path);
        //        c_pts.Clear();
        //        c_type.Clear();
        //    }
        //    if (c_paths.Count > 1)
        //    {
        //        surface.CurrentLayer.Select(null);
        //        surface.CurrentLayer.Elements.Remove(element);
        //        for (int i = 0; i < c_paths.Count; i++)
        //        {
        //            PathElement v_npath = PathElement.Create(c_paths[i]);
        //            if (v_npath != null)
        //            {
        //                v_npath.StrokeBrush.Copy(cbrush);
        //                v_npath.FillBrush.Copy(fcbrush);
        //                surface.CurrentLayer.Elements.Add(v_npath);
        //            }
        //        }
        //        surface.Invalidate();
        //        return true;
        //    }
        //    return false;
        //}
    }
}

