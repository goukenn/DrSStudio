

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: IGKD3DOGLWorkingDrawableAttribute.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/

using IGK.ICore;
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

namespace IGK.DrSStudio.Drawing3D.OpenGL
{
    /// <summary>
    /// represent a drawable 3d object
    /// </summary>
    public class IGKD3DOGLWorkingDrawableAttribute : IGKD3DOGLWorkingObjectAttribute
    {
        private System.Type m_Mecanism;

        public System.Type Mecanism
        {
            get { return m_Mecanism; }
            set
            {
                if (m_Mecanism != value)
                {
                    m_Mecanism = value;
                }
            }
        }
        public IGKD3DOGLWorkingDrawableAttribute(string name, Type mecanism):base(name )
        {
            this.Mecanism = mecanism;
        }
    }
}
