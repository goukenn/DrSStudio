using IGK.DrSStudio.Android.WinUI.Dialog;
using IGK.ICore;
using IGK.ICore.Drawing2D;
using IGK.ICore.Drawing2D.Menu;
using IGK.ICore.Imaging;
using IGK.ICore.IO;
using IGK.ICore.Menu;
using IGK.ICore.WinCore;
using IGK.ICore.WinUI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.DrSStudio.Android.Drawing2D.AndroidIcon.Menu
{
    [CoreMenu("Tools.Android.ExportAsAndroidIcon", 0x03)]
    class ExportAsAndroidIconMenu : Core2DDrawingMenuBase
    {
        protected override bool PerformAction()
        {
            using (PickFolderNameDialog g = new PickFolderNameDialog())
            {
                g.OuputFolder = Environment.CurrentDirectory;
                g.FileName = CoreUtils.GetFileName(this.CurrentSurface);
                if (g.ShowDialog(this.Workbench, "title.ExportAsAndroidIcon".R()) == enuDialogResult.OK)
                {
                    this.SaveFiles(g.OuputFolder, g.FileName);
                    return true;
                }
            }
            return false;
        }

        private void SaveFiles(string OutputFolder, string FileName)
        {
            var v_doc = this.CurrentSurface.CurrentDocument;
            int x = v_doc.Width;
            int y = v_doc.Height;
            int w = 24;
            int h = 24;
            float[] v_scales = new float[] { 1.0f, 2f, 3f, 4f, 6f, 8f };
            string[] v_prefix = new string[] { "ldpi", "mdpi", "hdpi", "xhdpi", "xxhdpi", "xxxhdpi" };
           
            for (int i = 0; i < v_scales.Length; i++)
            {
                string n = Path.Combine(OutputFolder, "drawable-" + v_prefix[i], FileName + ".png");

                if (PathUtils.CreateDir(Path.GetDirectoryName(n)))
                {
                    using (WinCoreBitmap c = WinCoreBitmap.Create((int)Math.Ceiling(w * v_scales[i]),
                        (int)Math.Ceiling(h * v_scales[i]),
                         ICore.Drawing2D.enuPixelFormat.Format32bppArgb))
                    {
                        var device = c.CreateDevice();
                        v_doc.Draw(device, new Rectanglei(0, 0, c.Width, c.Height), true, enuFlipMode.None);
                        device.Dispose();

                        c.Save(n, CoreBitmapFormat.Png);
                    }
                }
            }


        }
    }
}
