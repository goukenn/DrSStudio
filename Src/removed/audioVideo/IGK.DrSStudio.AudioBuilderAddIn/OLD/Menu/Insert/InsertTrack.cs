

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: InsertTrack.cs
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
file:InsertTrack.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
namespace IGK.DrSStudio.AudioBuilder.Menu.Insert
{
    [IGK.DrSStudio.Menu.CoreMenu("AUDIOBuilderInsert.AudioTrack", 0)]
    sealed class InsertTrack : AudioMenuBase
    {
        protected override bool PerformAction()
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "Audio file | *.wav; *.mp3; *.wma;";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    this.CurrentSurface.InsertAudioTrack(ofd.FileName);
                }
            }
            return false;
        }
    }
}

