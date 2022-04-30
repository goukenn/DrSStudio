

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: WinCoreDesignBrushRegister.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
﻿using IGK.ICore;
using IGK.ICore.Drawing2D;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace IGK.DrSStudio.WinUI.Design
{
    class WinCoreDesignBrushRegister : ICoreBrushRegister
    {
        Dictionary<Colorf, Brush> m_brushes = new Dictionary<Colorf,Brush> ();
        Dictionary<Colorf, Pen> m_pens = new Dictionary<Colorf, Pen>(); 
        public void Unregister(ICoreBrush coreBrush)
        {
            
        }

        public void Register(ICoreBrush coreBrush)
        {
            
        }

        public T GetPen<T>(Colorf color) where T : class, IDisposable
        {
            if (m_pens.ContainsKey(color))
                return m_pens[color] as T;
            var b = new Pen(color.CoreConvertTo<Color>(), 1.0f);
            m_pens.Add(color, b);
            return b as T;
        }

        public T GetBrush<T>(Colorf color) where T : class, IDisposable
        {
            if (m_brushes.ContainsKey(color))
                return m_brushes[color] as T;
            var b = new SolidBrush(color.CoreConvertTo<Color>());
            m_brushes.Add(color, b);
            return b as T;
        }

        public T GetBrush<T>(enuHatchStyle style, Colorf cl1, Colorf cl2) where T : class, IDisposable
        {
            return default(T);
        }

        public T GetPen<T>(ICorePen corePen) where T : class, IDisposable
        {
            return default(T);
        }

        public T GetBrush<T>(ICoreBrush brush) where T : class, IDisposable
        {
            return default(T);
        }

        public T GetBrush<T>(ICoreBitmap bitmap) where T : class, IDisposable
        {
            return default(T);
        }

        public T GetBrush<T>(ICore2DDrawingDocument document) where T : class, IDisposable
        {
            return default(T);
        }

        public void Reload(ICoreBrush coreBrush)
        {
            
        }

        public void Dispose()
        {
            this.m_brushes.Clear();
        }
    }
}
