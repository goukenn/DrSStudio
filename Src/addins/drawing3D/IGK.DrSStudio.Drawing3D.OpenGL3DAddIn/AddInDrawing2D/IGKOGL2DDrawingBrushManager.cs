

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: IGKOGL2DDrawingBrushManager.cs
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
﻿using IGK.OGLGame.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IGK.DrSStudio.Drawing3D.OpenGL
{
    class IGKOGL2DDrawingBrushManager
    {
        static Dictionary<ICoreBrush, IGKOGL2DBrushDefinition> sm_brushDef;
        static Dictionary<ICoreBrush, IGKOGL2DBrushDefinition> sm_strokeDef;
        static IGKOGL2DDrawingBrushManager() {
            sm_brushDef = new Dictionary<ICoreBrush, IGKOGL2DBrushDefinition>();
            sm_strokeDef = new Dictionary<ICoreBrush, IGKOGL2DBrushDefinition>();
            Application.ApplicationExit += _ApplicationExit;
        }

        static void _ApplicationExit(object sender, EventArgs e)
        {
            
        }
        public static IGKOGL2DBrushDefinition GetBrush(OGLGraphicsDevice device, ICoreBrush brush) {
            return null;
        }
        public static IGKOGL2DBrushDefinition GetPen(OGLGraphicsDevice device, ICorePen pen)
        {
            return null;
        }

        public void Register(ICoreBrush brush) {
            brush.BrushDefinitionChanged += brush_BrushDefinitionChanged;
            brush.Disposed += brush_Disposed;
        }

        void brush_Disposed(object sender, EventArgs e)
        {
            ICoreBrush br = (ICoreBrush)sender;
            Unregister(br);
        }
        public void Unregister(ICoreBrush brush)
        { 

        }

        void brush_BrushDefinitionChanged(object sender, EventArgs e)
        {
            
        }
    }
}
