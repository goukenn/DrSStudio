using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace IGK.ICore.WinCore
{
    /// <summary>
    /// represent extension for gdi plus
    /// </summary>
    public static class WinCoreGdiPlusExtensions
    {
        public static void Clear(this Graphics g, Colorf cl)
        {
            g.Clear(Color.FromArgb(
                (int)(cl.A * 255),
                (int)(cl.R * 255),
                (int)(cl.G * 255),
                (int)(cl.B * 255)));
        }
        public static void FillRectangle(this Graphics graphics,
            Colorf color,
            Rectanglef rc)
        {
            
            if (graphics == null)
                return;
            graphics.FillRectangle(
                CoreBrushRegisterManager.GetBrush<Brush>(color),
                rc.X, rc.Y ,
                rc.Width, rc.Height );

        }
    }
}
