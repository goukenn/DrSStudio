

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: WebSolutionFile.cs
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
﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace IGK.DrSStudio.Web
{
    /// <summary>
    /// represet a web solution file
    /// </summary>
    public class WebSolutionFile : WebSolutionItem
    {

        private string m_FileName;

        public WebSolutionFile()
        {

        }
        public override string ImageKey
        {
            get
            {
                if (string.IsNullOrEmpty(this.FileName))
                    return "img_dash";
                string ext = Path.GetExtension(this.FileName).Replace(".", "");
                return string.Format("img_{0}", ext);
            }
        }
        public WebSolutionFile(string name, string filename)
        {
            this.Name = name;
            this.FileName = filename;
        }

        public WebSolutionFile(string filename):this(Path.GetFileName (filename), filename)
        {                        
        }

        public string FileName
        {
            get { return m_FileName; }
            set
            {
                if (m_FileName != value)
                {
                    m_FileName = value;

                }
            }
        }
    }
}
