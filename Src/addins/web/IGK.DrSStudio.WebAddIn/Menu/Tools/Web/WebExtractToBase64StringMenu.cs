

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: _ExtractToBase64String.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/

using IGK.ICore;
using IGK.ICore.Codec;
using IGK.ICore.Menu;
using IGK.ICore.WinUI;
using IGK.ICore.Drawing2D;
using IGK.ICore.Drawing2D.Menu;
using IGK.ICore.Drawing2D.WinUI;
using IGK.DrSStudio;
using IGK.DrSStudio.Menu;
using IGK.DrSStudio.WinUI;
using IGK.DrSStudio.Drawing2D;
using IGK.DrSStudio.Drawing2D.WinUI;
/*
IGKDEV @ 2008-2016
author: C.A.D . BONDJE DOUE
file:_ExtractToBase64String.cs
*/
/* 
-------------------------------------------------------------------
Company: IGK-DEV
Author : C.A.D. BONDJE DOUE
SITE : http://www.igkdev.be
Application : DrSStudio
powered by IGK - DEV &copy; 2008-2012
THIS FILE IS A PART OF THE DRSSTUDIO APPLICATION. SEE "License.txt"
FOR MORE INFORMATION ABOUT THE LICENSE
------------------------------------------------------------------- 
*/
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IGK.DrSStudio.WebAddIn.Menu.Tools.Web;

namespace IGK.DrSStudio.WebAddIn.Menu.Tools
{
    using System.Drawing;
    using IGK.DrSStudio.WebAddIn.WinUI;
    using IGK.ICore.WinCore;
    using IGK.ICore.Imaging;
    using System.IO;

    [DrSStudioMenu("Tools.Web.ExtractToBase64String",0x201)]
    class _ExtractToBase64String :
        WebDrawing2DMenuBase 
    {
        protected override bool PerformAction()
        {
            XWebBase64String d = new XWebBase64String ();
           string vc = this.GetBase64String (1);
            WinCoreClipBoard.CopyToClipBoard(vc);
            CoreMessageBox.NotifyMessage("title.notify".R(),
                  "msg.webtools.imagehavebeencopied_1".R("clipboard"));
            //CoreClipb
            //d.ModeChanged += new EventHandler(d_ModeChanged);      
            //using (ICoreDialogForm frm = Workbench.CreateNewDialog(d))
            //{
            //    frm.Title = "dlg.ExtractToBase64String".R();
            //    frm.ShowDialog();
            //}
            return false;
        }
        void d_ModeChanged(object sender, EventArgs e)
        {
            XWebBase64String d = sender as XWebBase64String;
            d.Value = this.GetBase64String(d.Mode);
        }
        private string GetBase64String(int mode)
        {


            ICoreBitmap v_bmp =
                     WinCoreBitmapOperation.GetBitmap(
                     this.CurrentSurface.CurrentDocument,
                     CoreScreen.DpiX,
                     CoreScreen.DpiY);

            using (ICoreBitmap bmp = this.CurrentSurface.CurrentDocument.ToBitmap())
            {
                string v_temp = Path.GetTempFileName();
                bmp.Save(v_temp, CoreBitmapFormat.Png);
                string outf = Path.Combine(Path.GetDirectoryName(v_temp),
                    Path.GetFileNameWithoutExtension(v_temp) + ".png");
                try
                {
                    System.IO.File.Move(v_temp, outf);
                    StringBuilder sb = new StringBuilder();

                    return Convert.ToBase64String(File.ReadAllBytes(outf));
                }
                finally {
                    File.Delete(outf);
                }
            }




            //    string i = string.Empty;
            //using (Bitmap bmp = v_bmp.ToGdiBitmap())
            //{
            //    i = WinCoreBitmapOperation.BitmapToBase64String(v_bmp.ToGdiBitmap(), mode);
            //}
            //v_bmp.Dispose();
            //return i;
        }
    }
}

