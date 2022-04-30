using IGK.ICore.Drawing2D.WinUI;
using IGK.ICore.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.DrSStudio.Editor.FontEditor.FontOverLayEditor.Menu
{
    [CoreMenu("Edit.Add.FontOverlayLayer", 0x050)]
    class AddFontOverLayLayerMenu : CoreApplicationMenu
    {
        protected override bool PerformAction()
        {
            if (this.CurrentSurface is ICore2DDrawingMultiFrameSurface s ){

                if (!s.OverlayFrames.Contains<FontOverLayLayer>())
                {
                    s.OverlayFrames.Add(new FontOverLayLayer(s));
                }
                s.RefreshScene(true);//.Invalidate();
            }
            return base.PerformAction();
        }
    }
}
