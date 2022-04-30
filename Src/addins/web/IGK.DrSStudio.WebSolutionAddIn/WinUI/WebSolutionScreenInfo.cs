

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: WebSolutionScreenInfo.cs
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
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.DrSStudio.Web.WinUI
{
    /// <summary>
    /// web solution screen info
    /// </summary>
    public struct  WebSolutionScreenInfo
    {
        private int m_Width;
        private int m_Height;
        private string m_name;

        public int Height
        {
            get { return m_Height; }
        }
        public int Width
        {
            get { return m_Width; }          
        }
        public WebSolutionScreenInfo(string name, int width, int height)
        {
            this.m_name = name;
            this.m_Width = width;
            this.m_Height = height;
        }
        public override string ToString()
        {
            return string.Format("WebScreen_{0}x{1}", this.Width, this.Height);
        }
    }
}
