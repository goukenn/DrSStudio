

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: WebRenderingEventArgs.cs
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
/*
IGKDEV @ 2008-2016
author: C.A.D . BONDJE DOUE
file:WebRenderingEventArgs.cs
*/
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace IGK.DrSStudio.WebProjectAddIn
{
    public class WebRenderingEventArgs : EventArgs 
    {
        private Rectanglei m_Rectangle;
        private Graphics m_Graphics;
        public Graphics Graphics
        {
            get { return m_Graphics; }
            set
            {
                if (m_Graphics != value)
                {
                    m_Graphics = value;
                }
            }
        }
        public Rectanglei Rectangle
        {
            get { return m_Rectangle; }
            set
            {
                if (m_Rectangle != value)
                {
                    m_Rectangle = value;
                }
            }
        }
        public WebRenderingEventArgs(Graphics g , Rectanglei bound)
        {
            this.m_Graphics = g;
            this.m_Rectangle = bound;
        }
    }
}

