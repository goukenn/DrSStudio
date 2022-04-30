

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: InsertAudioFile.cs
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
file:InsertAudioFile.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
namespace IGK.DrSStudio.Drawing2D
{
    using IGK.ICore;using IGK.DrSStudio.Resources;
    using IGK.DrSStudio.Codec;
    //[IGK.DrSStudio.Menu.CoreMenu("Drawing2D.Insert.AudioFile", 200)]
    public sealed class InsertAudioFile : IGK.DrSStudio.Drawing2D.Menu .Core2DMenuBase 
    {
        protected override bool PerformAction()
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "audio files | *.mp3; *.wav";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    //IGK.DrSStudio.Codec.CoreResourceProjectItem rs = this.CurrentSurface.ProjectInfo["Resources"] as
                    //    IGK.DrSStudio.Codec.CoreResourceProjectItem;
                    //if (rs == null)
                    //{
                    //    rs = IGK.DrSStudio.Codec.CoreProjectItemBase.CreateItem("Resources")
                    //        as IGK.DrSStudio.Codec.CoreResourceProjectItem;
                    //    this.CurrentSurface.ProjectInfo.Add("Resources", rs);
                    //}
                    //AudioFileItemProject fproj = new AudioFileItemProject(ofd.FileName);
                    //rs.Add("ItemFile", ofd.FileName);
                }
            }
            return false;
        }
    }
}

