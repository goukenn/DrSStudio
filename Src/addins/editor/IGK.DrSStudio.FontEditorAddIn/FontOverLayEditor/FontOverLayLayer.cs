using IGK.ICore;
using IGK.ICore.Drawing2D;
using IGK.ICore.Drawing2D.WinUI;
using IGK.ICore.GraphicModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.DrSStudio.Editor.FontEditor.FontOverLayEditor
{
    /// <summary>
    /// represent a layer overlay for font edition
    /// </summary>
    sealed class FontOverLayLayer : ICore2DDrawingFrameRenderer
    {
        
        private ICore2DDrawingMultiFrameSurface m_surface;

        public FontOverLayLayer(ICore2DDrawingMultiFrameSurface s)
        {
            this.m_surface = s;
        }

        public void Render(ICoreGraphics g) {
            var o = g.Save();

            var c = this.m_surface.GetScreenLocation(new Vector2f(10, 0));
            var d = this.m_surface.GetScreenLocation(new Vector2f(0, 10));
            float H = this.m_surface.Height;
            float W = this.m_surface.Width;
            var p = CoreBrushRegisterManager.GetPen(Colorf.Black);
            //var p = CoreApplicationManager.Application.BrushRegister.GetPen<CorePen>(Colorf.Black);
            p.DashStyle = enuDashStyle.DashDotDot;
            //top - x
            g.DrawLine(p, c.X , 0, c.X, H);
            //top - y
            g.DrawLine(p, 0, d.Y , W, d.Y);

            //botom -  x
            c = this.m_surface.GetScreenLocation(new Vector2f(90, 0));
            g.DrawLine(p, c.X, 0, c.X, H);


            d = this.m_surface.GetScreenLocation(new Vector2f(0, 90));
            g.DrawLine(p, 0, d.Y, W, d.Y);

            p.DashStyle = enuDashStyle.Solid;

            g.Restore(o);
        }
    }
}
