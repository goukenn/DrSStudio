using IGK.ICore;
using IGK.ICore.Drawing2D.WinUI;
using IGK.ICore.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using IGK.ICore.WinUI;

namespace IGK.DrSStudio.SVGAddIn.Menu.File
{
    [CoreMenu("File.ExportTo.SvgFontMenu", 0xc1)]
    class ExportAsSvgFontMenu : CoreApplicationSurfaceMenuBase 
    {
        protected override void InitMenu()
        {
            base.InitMenu();
        }
        protected override bool PerformAction()
        {
            if (this.CurrentSurface is ICore2DDrawingSurface s) {
                using (SaveFileDialog sfd = new SaveFileDialog())
                {
                    sfd.Filter = "Svg Font | *.svg";
                    if (sfd.ShowDialog() == DialogResult.OK)
                    {
                        SVGUtils.ExportToSVGFont(sfd.FileName, s.Documents);
                    }
                }
                
            }
            return false;
        }
        protected override void OnCurrentSurfaceChanged(CoreWorkingElementChangedEventArgs<ICoreWorkingSurface> e)
        {
            base.OnCurrentSurfaceChanged(e);        
            this.SetupEnableAndVisibility();
        }

        protected override bool IsEnabled()
        {
            return this.CurrentSurface is ICore2DDrawingSurface;
        }
        protected override bool IsVisible()
        {
            return this.CurrentSurface is ICore2DDrawingSurface;
        }

        
    }
}
