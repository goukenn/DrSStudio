

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: _SaveDisplayedImage.cs
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
file:_SaveDisplayedImage.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
namespace IGK.DrSStudio.VideoEditionTableAddIn.Menu.Actions
{
    [VideoMenu(VideoConstant.MENU_SAVE_DISPLAYED_PLAYERPIC, 3)]
    sealed class _SaveDisplayedImage :   VideoMenuBase
    {
        protected override bool PerformAction()
        {
            System.Drawing.Bitmap bmp = this.CurrentSurface.GetPlayerDisplayedImage();
            if (bmp == null)
                return false;
            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Filter = "PictureFiles | *.bmp";
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    ///save displayed picture
                    try
                    {
                        bmp.Save(sfd.FileName);
                    }
                    catch {
                        CoreLog.WriteDebug ("Error Append When saving the file");
                        return false;
                    }
                    return true;
                }
            }
            return false;
        }
    }
}

