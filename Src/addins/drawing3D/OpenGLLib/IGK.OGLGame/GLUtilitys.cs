

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: GLUtilitys.cs
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
file:GLUtilitys.cs
*/
using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Collections;
namespace IGK.OGLGame
{
    
using IGK.ICore.Drawing2D;
    using System.Drawing.Drawing2D;
    using IGK.ICore;

    public static class GLUtilitys
    {
        public static Vector2d[][] BuildFontOutLine(char ch, FontFamily font, FontStyle style)
        {
            if (char.IsControl(ch))
                return null;
            GraphicsPath g = new GraphicsPath();
            StringFormat sf = new StringFormat();
            g.AddString(ch.ToString(), font, (int)style, 1.0f, Point.Empty, sf);
            g.CloseFigure();
            Vector2d[][] t = BuildGraphicsOutLine(g);
            g.Dispose();
            return t;
        }
        public static Vector2d[][] BuildFontOutLine(string ch, FontFamily font, FontStyle style)
        {
            GraphicsPath g = new GraphicsPath();
            StringFormat sf = new StringFormat();
            g.AddString(ch.ToString(), font, (int)style, 1.0f, Point.Empty, sf);
            g.CloseFigure();
            global::System.Drawing.Drawing2D.Matrix m = new global::System.Drawing.Drawing2D.Matrix();
            RectangleF v_rc = g.GetBounds();
            //translate to middle Y
            float dy = v_rc.Height / 2.0f;
            m.Translate(0, dy, MatrixOrder.Append);
            m.Scale(1, -1, MatrixOrder.Append);
            m.Translate(0, -dy, MatrixOrder.Append);
            g.Transform(m);
            m.Dispose();
            m = null;
            Vector2d[][] t = BuildGraphicsOutLine(g);
            g.Dispose();
            return t;
        }
        /// <summary>
        /// build graphics font outlines
        /// </summary>
        /// <param name="g"></param>
        /// <returns></returns>
        public static Vector2d[][] BuildGraphicsOutLine(this GraphicsPath g)
        {
            if (g.PointCount == 0)
                return null;
            List<Vector2d[]> v_list = new List<Vector2d[]>();
            List<Vector2d> v_tlist = new List<Vector2d>();
            Vector2f[] point = g.PathPoints.CoreConvertFrom<Vector2f[]>();
            Byte[] type = g.PathTypes;
            for (int i = 0; i < g.PointCount; i++)
            {
                switch ((enuGdiGraphicPathType)type[i])
                {
                    case enuGdiGraphicPathType.StartFigure:
                        if (i != 0)
                        {
                            v_list.Add(v_tlist.ToArray());
                            v_tlist = new List<Vector2d>();
                        }
                        break;
                    default:
                        break;
                }
                v_tlist.Add(new Vector2d()
                {
                    X = point[i].X,
                    Y = point[i].Y
                });
            }
            if (v_tlist.Count > 0)
                v_list.Add(v_tlist.ToArray());
            return v_list.ToArray();
        }
    }
}

