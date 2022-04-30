

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: CosPathBrushStyle.cs
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
file:CosPathBrushStyle.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Drawing;
namespace IGK.DrSStudio.PathBrushEditorAddIn
{
    public class CosPathBrushStyle : SinPathBrushStyle 
    {
        static CosPathBrushStyle()
        {
            IGK.DrSStudio.Drawing2D.CorePathBrushStyleBase.Register(PathBrushConstant.SIN_STYLE,
                typeof(CosPathBrushStyle));
        }
        public override void Generate(System.Drawing.Drawing2D.GraphicsPath path)
        {
            path.Flatten();
            Vector2f[] c = path.PathPoints;
            Rectanglef v_rc = Rectangle.Empty;
            GraphicsPath path1 = new GraphicsPath();
            GraphicsPath path2 = new GraphicsPath();
            GraphicsPath mp = new GraphicsPath();
            List<Vector2f> m = new List<Vector2f>();
            List<Vector2f> m2 = new List<Vector2f>();
            float scale = 1.0f / (float)c.Length;
            float angle = 0.0f;
            Matrix mat = new Matrix();
            for (int i = 0; i < c.Length; i++)
            {
                v_rc = new Rectanglef(c[i], Size.Empty);
                v_rc.Inflate(2, 2);
                mat.Reset();
                if ((i + 1) < c.Length)
                {
                    angle = IGK.DrSStudio.CoreMathOperation.GetAngle(c[i], c[i + 1]) * CoreMathOperation.ConvRdToDEGREE;
                    mat.RotateAt(angle, c[i], enuMatrixOrder.Append);
                }
                else
                {
                    mat.RotateAt(angle, c[i], enuMatrixOrder.Append);
                }
                //draw 
                mp.AddLine(c[i].X,
                    (float)(c[i].Y - (this.Amplitude * Math.Cos(i * this.Period * Math.PI / (c.Length - 1)))),
                    c[i].X,
                    (float)(c[i].Y + (this.Amplitude * Math.Cos(i * this.Period * Math.PI / (c.Length - 1)))));
                mp.Transform(mat);
                m.Add(mp.PathPoints[0]);
                m2.Add(mp.PathPoints[1]);
                mp.Reset();
            }
            path1.AddCurve(m.ToArray());
            path2.AddCurve(m2.ToArray());
            path2.Reverse();
            path1.AddPath(path2, true);
            path1.CloseAllFigures();
            path1.FillMode = FillMode.Winding;
            path.Reset();
            path.AddPath(path1, false);
        }
    }
}

