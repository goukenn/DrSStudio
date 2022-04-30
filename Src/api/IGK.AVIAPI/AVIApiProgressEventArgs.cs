

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: AVIApiProgressEventArgs.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
/*
IGKDEV @ 2008-2016
author: C.A.D . BONDJE DOUE
file:ProgressEventArgs.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IGK.AVIApi
{
    public delegate void AVIApiProgressEventHandler(object sender, AVIApiProgressEventArgs e);

    /// <summary>
    /// progression event args
    /// </summary>
    public class AVIApiProgressEventArgs : EventArgs 
    {
        private int m_Progress;
        public int Progress
        {
            get { return m_Progress; }
        }
        public AVIApiProgressEventArgs(int progress)
        {
            this.m_Progress = progress;
        }
    }
}

