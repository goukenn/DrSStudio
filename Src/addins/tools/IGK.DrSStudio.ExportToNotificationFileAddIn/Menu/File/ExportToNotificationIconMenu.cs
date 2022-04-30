

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: ExportToNotificationIconMenu.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
﻿using IGK.ICore.WinCore;
using IGK.ICore;using IGK.DrSStudio.Drawing2D.Menu;
using IGK.ICore.Drawing2D;
using IGK.ICore.Drawing2D.Menu;
using IGK.ICore.Imaging;
using IGK.ICore.WinUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IGK.ICore.WinUI.Common;

namespace IGK.DrSStudio.Menu.File
{
    [DrSStudioMenu(
        "File.ExportTo.NoticiationIconModel",0x20,
        Description="export current drawing document to notification file size icon")]
    class ExportToNotificationIconMenu : Core2DDrawingMenuBase
    {
        static Vector2i[] sizes = new Vector2i[] { 
            new Vector2i(512,512),
            new Vector2i(256,256),
            new Vector2i(128,128),
            new Vector2i(64,64),
            new Vector2i(48,48),
            new Vector2i(24,24)
        };
        protected override bool IsEnabled()
        {
            return (this.CurrentSurface !=null);
        }
        protected override bool IsVisible()
        {
            return this.IsEnabled();
        }
        protected override bool PerformAction()
        {
            if (this.CurrentSurface != null)
            {
                var v_doc = this.CurrentSurface.CurrentDocument;
                using (IGK.ICore.WinUI.Common.FileNamePicker
                    picker = Workbench.CreateCommonDialog < IGK.ICore.WinUI.Common.FileNamePicker>(
                    CoreCommonDialogs.FileNamePicker))
                    {
                        picker.Title = "title.pickfilename".R();
                    if ((picker.ShowDialog () == enuDialogResult.OK)
                        && !string.IsNullOrEmpty (picker.FileName ))
                    {
                        for (int i = 0; i < sizes.Length; i++)
                        {
                            using (WinCoreBitmap bmp = WinCoreBitmap.Create(sizes[i].X, sizes[i].Y))
                            {
                                var device = bmp.CreateDevice();
                                v_doc.Draw(device, new Rectanglei(0, 0, bmp.Width, bmp.Height),
                                    true, enuFlipMode.None);
                                device.Flush();
                                device.Dispose();
                                string fname = System.IO.Path.GetFullPath (string.Format("{0}_{1}x{2}.png", picker.FileName
                                    , bmp.Width, bmp.Height));
                                bmp.Save(fname, CoreBitmapFormat.Png);
                            }
                        }
                }
                }
            }
            return base.PerformAction();
        }
    }
}
