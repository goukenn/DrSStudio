

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: CopyDocumentEMF.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
using IGK.ICore.WinCore;
using IGK.ICore.WinUI;
/*
IGKDEV @ 2008-2016
author: C.A.D . BONDJE DOUE
file:CopyDocumentEMF.cs
*/
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace IGK.DrSStudio.Drawing2D.Menu.Document.Copy
{
    using IGK.ICore;

    [IGKD2DDrawingCopyDocumentMenuAttribute("EMF", 0)]
    class CopyDocumentEMF : IGKD2DDrawingDocumentMenuBase
    {
        protected override bool PerformAction()
        {
            using (System.Drawing.Bitmap bmp = new System.Drawing.Bitmap(
             this.CurrentSurface.CurrentDocument.Width,
             this.CurrentSurface.CurrentDocument.Height))
            {
                bmp.SetResolution(CoreScreen.DpiX,
                     CoreScreen.DpiY);


                Graphics g = Graphics.FromImage(bmp);
                IntPtr hdc = g.GetHdc();
                MemoryStream mem = new MemoryStream();
                Metafile mfile = new Metafile(mem, hdc);
                Graphics bf = Graphics.FromImage(mfile);

                bf.Clip = new Region(new Rectangle(Point.Empty, bmp.Size));
                WinCoreBitmapDeviceVisitor device = WinCoreBitmapDeviceVisitor.Create(bf);//g);
                this.CurrentDocument.Draw(device);

                g.ReleaseHdc(hdc);
                g.Dispose();
                bf.Flush();
                bf.Dispose();
                mem.Close();
                bool c = WinCoreClipBoard.PutEnhMetafileOnClipboard(
                MainForm.Handle, mfile);
                mfile.Dispose();

                if (!c)
                {
                    CoreMessageBox.NotifyMessage("title.DocumentEMFError", "Impossible de copier en EMF.");
                }
                else {
                    
                }
            }
            return base.PerformAction();
        }
    }
}

