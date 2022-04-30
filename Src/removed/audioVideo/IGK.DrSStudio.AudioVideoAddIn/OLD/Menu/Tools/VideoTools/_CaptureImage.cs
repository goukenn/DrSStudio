

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: _CaptureImage.cs
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
file:_CaptureImage.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
namespace IGK.DrSStudio.AudioVideo.Menu.VideoTools
{
    [IGK.DrSStudio.Menu.CoreMenu("Tools.Video.CapturePicture", 3)]
    class _CaptureImage : VideoMenuBase
    {
        protected override bool PerformAction()
        {
                using (System.Windows.Forms.SaveFileDialog sfd = new System.Windows.Forms.SaveFileDialog())
                {
                    sfd.Filter = "Pictures | *.bmp";
                    if (sfd.ShowDialog() == DialogResult.OK)
                    {
                        this.CurrentSurface.CaptureImageTo(sfd.FileName);
                    }
                }
            return true;
        }
    }
}

