using IGK.DrSStudio.Android.Drawing2D.Tools;
using IGK.ICore;
using IGK.ICore.Drawing2D;
using IGK.ICore.Drawing2D.WinUI;
using IGK.ICore.Menu;
using IGK.ICore.WinUI;
using IGK.ICore.WinUI.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.DrSStudio.Android.Drawing2D.Menu
{
    [CoreMenu("File.SaveAsNinePatch", 0x152)]
    class SaveFileAsNinePatchMenu : CoreApplicationMenu
    {
        protected override bool IsVisible()
        {
            return (this.CurrentSurface !=null);
        }
        protected override bool IsEnabled()
        {
            var s = this.CurrentSurface;
            if (s == null) return false;
            var p = this.CurrentSurface.GetProjectElement();
            if (p != null) 
            {
                var m =  p.GetAttribute(AndroidConstant.ANDROID_DRAWING2D_NINEPATCHATTRIBUTE);
                return m!= null; 
            }
            return false;
        }
        protected override void InitMenu()
        {
            base.InitMenu();
            NinePathManagerTool.Instance.MustUpdate += Instance_MustUpdate;
        }

        void Instance_MustUpdate(object sender, EventArgs e)
        {
            this.SetupEnableAndVisibility();
        }
        protected override bool PerformAction()
        {
            using (var d = Workbench.CreateCommonDialog<IXCommonSaveFileAsDialog>(CoreCommonDialogs.SaveFileAsDialog))
            {
                d.FileName = "image.9.png";
                d.Filter ="Nine patch file | *.9.png";
                if (d.ShowDialog() == enuDialogResult.OK)
                {
                    var s = this.CurrentSurface as ICore2DDrawingSurface;
                    SaveDocument(d.FileName, s.CurrentDocument);
                }
            }            
            return false;
        }

        private void SaveDocument(string filename, ICore2DDrawingDocument document)
        {
            document.ToBitmap().Save (filename , ICore.Imaging.CoreBitmapFormat.Png );
        }
    }
}
