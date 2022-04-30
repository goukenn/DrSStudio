

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: _InsertImage.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
/*
IGKDEV - [2008 - 2014]
author: C.A.D . BONDJE DOUE
file:_InsertImage.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
namespace IGK.DrSStudio.WPFSurfaceAddIn.Menu.Insert
{
    using IGK.ICore.WinCore;
using IGK.ICore;using IGK.DrSStudio.Drawing2D;
    using IGK.DrSStudio.WinUI;
    [WPFMenu ("Insert.Image", 0)]
    public class _InsertImage : WPFMenuBase 
    {
        protected override bool PerformAction()
        {
            Bitmap v_bmp = null ;
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "Pictures | *.bmp; *.jpg;*.png;*.jpeg;";
                if (ofd.ShowDialog() == DialogResult.OK)                
                {
                    try
                    {
                        v_bmp
                            = new Bitmap(ofd.FileName);
                        System.Windows.Media.ImageSource v_source =  Utils.BitmapUtils.GetSourceFromBitmap(v_bmp );
                        if (v_source != null)
                        {
                            WorkingObjects.ImageElement v_imge = WorkingObjects.ImageElement.CreateElement(
                                v_source);
                            this.CurrentSurface.CurrentDocument.CurrentLayer.Elements.Add(v_imge);
                        }
                    }
                    catch {
                        CoreServices.ShowError(CoreSystem.GetString ("ERR.UnableToLoadPicture"));
                    }
                }
            }
            return base.PerformAction();
        }
    }
}

