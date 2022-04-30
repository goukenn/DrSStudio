

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: PathOperator.cs
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
file:PathOperator.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing ;
using System.Drawing .Drawing2D ;
namespace IGK.DrSStudio.Drawing2D
{
    /// <summary>
    /// represent a class operator
    /// </summary>
    public static class PathOperator
    {
        public static void  AddReversePoint(GraphicsPath path, bool connected)
        {
            GraphicsPath v_path = new GraphicsPath(path.PathPoints , path.PathTypes);
            v_path.Reverse();
            path.AddPath(v_path, connected);
            v_path.Dispose();
        }
        public static GraphicsPath MergePath(params GraphicsPath[] path)
        {
            if (path.Length == 0)
                return null ;
            GraphicsPath v_path = null;
            List<Vector2f> v_points = new List<Vector2f>();
            List<Byte> v_types = new List<byte>();
            bool v_t = false;
            int v_index = 0;
            foreach (GraphicsPath  item in path)
            {
                if (!v_t)
                {
                    v_t = true;
                    v_points.AddRange(item.PathPoints);
                    v_types.AddRange(item.PathTypes);
                }
                else {
                    v_types[v_index] =(byte)( enuGdiGraphicPathType .StartFigure -v_types[v_index]);
                    v_points.AddRange(item.PathPoints);
                    v_types.AddRange(item.PathTypes);
                }
                v_index = v_points.Count-1;
            }
            v_path = new GraphicsPath(v_points.ToArray (), v_types.ToArray());
            return v_path;
        }
    }
}

