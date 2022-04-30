using IGK.ICore;
using IGK.ICore.Drawing2D.WinUI;
using IGK.ICore.Menu;
using IGK.ICore.WinUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IGK.DrSStudio.SVGAddIn.Menu.File
{
    /// TODO : EXPORT SVG AS WOFF 
    //[CoreMenu("File.ExportTo.SvgExportAsWoffFileMenu", 0xc2)]
    class ExportAsWoffFileMenu: CoreApplicationSurfaceMenuBase
    {
        protected override bool PerformAction()
        {
            try
            {

                throw new NotImplementedException();
            }
            catch (Exception ex) {
                CoreMessageBox.Show(ex);
            }
            return false;

            //if (this.CurrentSurface is ICore2DDrawingSurface s)
            //{
            //    using (SaveFileDialog sfd = new SaveFileDialog())
            //    {
            //        sfd.Filter = "wOFF file | *.woff";
            //        if (sfd.ShowDialog() == DialogResult.OK)
            //        {

            //            //WOFFFileManager m = new WOFFFileManager();
            //            //setting = s.GetParam<WoFFFileFontSetting>() ?? new WoFFFileFontSetting();// GetParam(
            //            //foreach (ICore2DDrawingLayeredDocument item in s.Documents)
            //            //{
            //            //    m.AddGlyf();
            //            //}
            //            //m.Save(sfd.FileName, setting);
            //            //SVGUtils.ExportToSVGFont(sfd.FileName, s.Documents);
            //        }
            //    }

            //}
            //return false;
        }
        protected override bool IsEnabled()
        {
            return this.CurrentSurface is ICore2DDrawingSurface;
        }
        protected override bool IsVisible()
        {
            return this.CurrentSurface is ICore2DDrawingSurface;
        }
        protected override void OnCurrentSurfaceChanged(CoreWorkingElementChangedEventArgs<ICoreWorkingSurface> e)
        {
            base.OnCurrentSurfaceChanged(e);
            this.SetupEnableAndVisibility();
        }
    }
}
