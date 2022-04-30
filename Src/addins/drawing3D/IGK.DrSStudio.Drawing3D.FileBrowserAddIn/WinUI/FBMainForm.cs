

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: FileBrowserMainForm.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IGK.DrSStudio.Drawing3D.FileBrowser
{
    /// <summary>
    /// represent a file browser main form
    /// </summary>
    class FileBrowserMainForm : Form
    {
        private bool m_FullScreen;

        public bool FullScreen
        {
            get { return m_FullScreen; }
            set
            {
                if (m_FullScreen != value)
                {
                    m_FullScreen = value;
                }
            }
        }
        public FileBrowserMainForm()
        {

        }
    }
}
