

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: SinPathBrushStyle.cs
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
file:SinPathBrushStyle.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D ;
namespace IGK.DrSStudio.PathBrushEditorAddIn
{
    using IGK.ICore.WinCore;
using IGK.ICore;using IGK.DrSStudio;
    public  class SinPathBrushStyle : IGK.DrSStudio.Drawing2D.CorePathBrushStyleBase 
    {
        static SinPathBrushStyle() {
            IGK.DrSStudio.Drawing2D.CorePathBrushStyleBase.Register(PathBrushConstant.SIN_STYLE,
                typeof(SinPathBrushStyle));
        }
        public override string Id
        {
            get { return PathBrushConstant .SIN_STYLE; }
        }
        private float m_Amplitude;
        private float m_Period;
        public float Period
        {
            get { return m_Period; }
            set
            {
                if (m_Period != value)
                {
                    m_Period = value;
                    OnPropertyChanged(EventArgs.Empty);
                }
            }
        }
        public float Amplitude
        {
            get { return m_Amplitude; }
            set
            {
                if (m_Amplitude != value)
                {
                    m_Amplitude = value;
                    OnPropertyChanged(EventArgs.Empty);
                }
            }
        }
        public SinPathBrushStyle()
        {
            this.m_Amplitude = 10.0f;
            this.m_Period = 2.0f;
        }
        public override IGK.DrSStudio.WinUI.ICoreParameterConfigCollections GetParameters(IGK.DrSStudio.WinUI.ICoreParameterConfigCollections parameters)
        {
            parameters = base.GetParameters(parameters);
            var group = parameters.AddGroup(CoreConstant.PARAM_DEFINITION);
            group.AddItem(GetType().GetProperty("Amplitude"));
            group.AddItem(GetType().GetProperty("Period"));
            return parameters;
        }
        public override string GetDefinition()
        {
            StringBuilder sb = new StringBuilder ( base.GetDefinition());
            sb.Append(string.Format("Amplitude:{0};", this.Amplitude));
            sb.Append(string.Format("Period:{0};", this.Period));
            return sb.ToString();
        }
        public override  void Generate(System.Drawing.Drawing2D.GraphicsPath path)
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
                    (float)(c[i].Y - (this.Amplitude * Math.Sin(i * this.Period  * Math.PI / (c.Length - 1)))),
                    c[i].X,
                    (float)(c[i].Y + (this.Amplitude * Math.Sin(i * this.Period  * Math.PI / (c.Length - 1)))));
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
            path.AddPath(path1,false );
        }
    }
}

