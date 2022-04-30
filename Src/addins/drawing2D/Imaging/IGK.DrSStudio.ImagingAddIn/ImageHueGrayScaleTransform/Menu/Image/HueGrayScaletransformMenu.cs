using IGK.DrSStudio.Drawing2D.Menu;
using IGK.DrSStudio.ImageAddIn.GDIAttribute.WinUI;

using IGK.DrSStudio.Imaging.ImageHueGrayScaleTransform.WinUI;
using IGK.ICore;
using IGK.ICore.Drawing2D;
using IGK.ICore.Menu;
using IGK.ICore.WinCore;
using IGK.ICore.WinUI;
using IGK.ICore.WinUI.Common;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace IGK.DrSStudio.Imaging.ImageHueGrayScaleTransform.Menu.Image
{

    [CoreMenuAttribute("Image.HueGrayScaleTransform", 12)]
    class HueGrayScaletransformMenu : ImageMenuBase
    {
        protected override bool PerformAction()
        {
            //var v_bmp = this.ImageElement.Bitmap.Clone() as ICoreBitmap;

            //var vdata = v_bmp.ToData();

            ////HueGrayTranform(ref vdata,v_bmp.Width, v_bmp.Height, 200, 210);
            //HueGrayTranform(ref vdata, v_bmp.Width, v_bmp.Height, 20, 90);
            //Bitmap bmp = new Bitmap(v_bmp.Width, v_bmp.Height, PixelFormat.Format32bppArgb);
            //var data = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);

            //Marshal.Copy(vdata, 0, data.Scan0, vdata.Length);

            //bmp.UnlockBits(data);

            //v_bmp.Dispose();

            //var obmp = WinCoreBitmap.Create(bmp);
            //this.ImageElement.SetBitmap(obmp, false);
            //this.CurrentSurface.Invalidate();
            //return false;


            using (XHueConfigControl attr = new XHueConfigControl())
            {
                attr.ImageElement = this.ImageElement;
                attr.Surface = this.CurrentSurface;
                using (ICoreDialogForm frm = Workbench.CreateNewDialog(attr))
                {
                    frm.Title = "title.HueColorAttribute.Caption".R();
                    frm.ShowDialog();
                }
            }
            return base.PerformAction();
        }

      }
}
