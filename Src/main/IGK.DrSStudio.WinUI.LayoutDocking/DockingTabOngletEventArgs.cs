

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: DockingTabOngletEventArgs.cs
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
file:DockingTabOngletEventArgs.cs
*/
/* 
-------------------------------------------------------------------
Company: IGK-DEV
Author : C.A.D. BONDJE DOUE
SITE : http://www.igkdev.be
Application : DrSStudio
powered by IGK - DEV &copy; 2008-2012
THIS FILE IS A PART OF THE DRSSTUDIO APPLICATION. SEE "License.txt"
FOR MORE INFORMATION ABOUT THE LICENSE
------------------------------------------------------------------- 
*/
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using IGK.ICore.WinCore;
using IGK.ICore;using IGK.ICore.GraphicModels;
namespace IGK.DrSStudio.WinUI
{
        /// <summary>
        /// draw docking tab onglet event args
        /// </summary>
        public class DockingDrawTabOngletEventArgs : EventArgs
        {
            private ICoreGraphics g;
            private Rectanglef m_rc;
            private IDockingPage m_page;
            /// <summary>
            /// get the graphic to draw
            /// </summary>
            public ICoreGraphics Graphics
            {
                get
                {
                    return g;
                }
            }
            /// <summary>
            /// get the display rectangle
            /// </summary>
            public Rectanglef Rectangle
            {
                get
                {
                    return this.m_rc;
                }
            }
            /// <summary>
            /// get the docking page
            /// </summary>
            public IDockingPage Page
            {
                get
                {
                    return this.m_page;
                }
            }
            public DockingDrawTabOngletEventArgs(ICoreGraphics g, Rectanglef rc, IDockingPage page)
            {
                this.g = g;
                this.m_rc = rc;
                this.m_page = page;
            }
        }
    }

