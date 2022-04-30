

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: IGKD2DDrawingInsertMethod.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
using IGK.ICore.WinCore;
using IGK.ICore;
using IGK.ICore.Drawing2D;
/*
IGKDEV @ 2008-2016
author: C.A.D . BONDJE DOUE
file:IGKD2DDrawingInsertMethod.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
namespace IGK.ICore.Drawing2D.WinUI
{
    /// <summary>
    /// used to insert object or object on IGK2DDrawingSurface
    /// </summary>
    class IGKD2DDrawingInsertMethod
    {
        private IGKD2DDrawingSurfaceBase m_currentSurface;
        public IGKD2DDrawingInsertMethod(IGKD2DDrawingSurfaceBase iGKD2DDrawingSurfaceBase)
        {
            this.m_currentSurface = iGKD2DDrawingSurfaceBase;
        }
        internal void Insert(ICoreWorkingObject obj)
        {
            MethodBase.GetCurrentMethod().Visit(this, obj);
        }
        internal void Remove(ICoreWorkingObject obj)
        {
            MethodBase.GetCurrentMethod().Visit(this, obj);
        }
        public void Insert(Core2DDrawingLayeredElement e)
        {
            if (e!=null)
            this.m_currentSurface.CurrentLayer.Elements.Add(e);
        }
        public void Remove(Core2DDrawingLayeredElement e)
        {
            if (e != null)
                this.m_currentSurface.CurrentLayer.Elements.Remove(e);
        }
    }
}

