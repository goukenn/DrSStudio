

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: PathBrushItem.cs
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
file:PathBrushItem.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
namespace IGK.DrSStudio.PathBrushEditorAddIn.WinUI
{
    using IGK.ICore.WinCore;
using IGK.ICore;using IGK.DrSStudio.WinUI;
    using IGK.DrSStudio;
    using IGK;
    /// <summary>
    /// represent a path brush item
    /// </summary>
    public class PathBrushItem : XControl
    {
        private IGK.DrSStudio.Drawing2D.ICore2DPathBrushStyle m_Style;
        GraphicsPath m_path;
        public IGK.DrSStudio.Drawing2D.ICore2DPathBrushStyle Style
        {
            get { return m_Style; }
            set
            {
                if (m_Style != value)
                {
                    m_Style = value;
                }
            }
        }
        public PathBrushItem(IGK.DrSStudio.Drawing2D.ICore2DPathBrushStyle Style)
        {
            this.m_Style = Style;
            this.Paint += new System.Windows.Forms.PaintEventHandler(_Paint);
            this.SizeChanged += new EventHandler(PathBrushItem_SizeChanged);
        }
        void PathBrushItem_SizeChanged(object sender, EventArgs e)
        {
            if (this.m_path == null)
                this.m_path = new GraphicsPath();
            m_path.Reset();
            float h = this.Height / 2.0f;
            float w = this.Width - 10;
            float v_step = w / 100.0f;
            if (v_step > 0.0f)
            {
                Vector2f d = new Vector2f(5, h);
                Vector2f[] t = new Vector2f[100];
                for (int i = 0; i < t.Length; i++)
                {
                    t[i] = d;
                    d.X += v_step;
                }
                m_path.AddCurve(t);
                this.Style.Generate(m_path);
            }
        }
        void _Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {
            if (m_path != null)
            {
                e.Graphics.SmoothingMode = SmoothingMode.HighQuality;
                e.Graphics.FillPath (Brushes.Black, m_path);
                e.Graphics.DrawPath(Pens.Black, m_path);
            }
        }
    }
}

