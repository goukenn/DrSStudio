

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: WinCoreGraphics.cs
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
file:WinCoreGraphics.cs
*/
using IGK.ICore;using IGK.DrSStudio.Drawing2D;
using IGK.DrSStudio.Rendering;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
namespace IGK.DrSStudio.WinLauncher
{
    class WinCoreGraphics : ICoreGraphics
    {
        Graphics m_graphics;
        private WinCoreGraphics()
        {
        }
        internal static ICoreGraphics Create(Graphics g)
        {
            if (g == null)
                return null;
            WinCoreGraphics c = new WinCoreGraphics();
            c.m_graphics = g;
            return c;
        }
        public bool Accept(ICoreWorkingObject obj)
        {
            if (obj is ICore2DDrawingObject)
            {
                return true;
            }
            return false;
        }
        public void Flush()
        {
            this.m_graphics.Flush();
        }
        public void Visit(ICoreWorkingObject obj)
        {
            MethodInfo.GetCurrentMethod().Visit(this, obj);
        }
        public void Dispose()
        {
            this.m_graphics.Dispose();
        }
    }
}

