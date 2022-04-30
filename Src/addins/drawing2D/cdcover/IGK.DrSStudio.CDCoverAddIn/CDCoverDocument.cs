

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: CDCoverDocument.cs
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
file:CDCoverDocument.cs
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
/* 
-------------------------------------------------------------
This file is part of iGK-DEV-DrawingStudio
-------------------------------------------------------------
-------------------------------------------------------------
-------------------------------------------------------------
view license file in Documentation folder to get more info
Copyright (c) 2008-2010 
Author  : Charles A.D. BONDJE DOUE 
mail : bondje.doue@hotmail.com
-------------------------------------------------------------
-------------------------------------------------------------
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D ;
using IGK.ICore.WinCore;
using IGK.ICore;
using IGK.ICore.WinUI;
using IGK.ICore.Drawing2D;
using IGK.ICore.GraphicModels;
using IGK.ICore.Drawing2D.WinUI;
using IGK.ICore.WinUI.Configuration;
namespace IGK.DrSStudio.CDCoverAddin
{
    [Core2DDrawingDocumentAttribute("CDCoverDocument")]
    sealed class CDCoverDocument : Core2DDrawingLayerDocument
    {
        private Region m_circleRegion;
        private GraphicsPath m_circlePath;
        private IGK.ICore.Matrix m_circlePathMatrix;
        public GraphicsPath CirclePath {
            get { return this.m_circlePath; 
            }
        }
        public override ICoreParameterConfigCollections GetParameters(ICoreParameterConfigCollections parameters)
        {
            ICoreParameterConfigCollections v_p= base.GetParameters(parameters);
            v_p.RemoveGroup("Size");
            return v_p ;
        }
        //CONSTRUCTOR
        public CDCoverDocument():this(
            (int)Math.Ceiling (((ICoreUnitPixel)CDPrintManager.Width).Value),
            (int)Math.Ceiling (((ICoreUnitPixel)CDPrintManager.Height).Value)
            )
        {
        }
        public CDCoverDocument(int width, int height)
            : base((CoreUnit)width,(CoreUnit ) height)
        {
            InitDocument();
        }
        /// <summary>
        /// init document
        /// </summary>
        void InitDocument()
        {           
            GraphicsPath path = new GraphicsPath();
            //interio
            //--------------------------------------
            //radius
            CoreUnit c = "15 mm";
            CoreUnit v_rh = "35 mm";
            int w = (int)((ICoreUnitPixel)v_rh).Value;
            int W = (int)this.Width;
            Rectanglef rc = new Rectanglef(0, 0, W,W);
            Vector2f  center = CoreMathOperation.GetCenter(rc);
            Rectanglef rdim = CoreMathOperation.GetBounds(center,w/ 2);
            path.SetMarkers();
            path.AddRectangle(rc);
            path.AddEllipse(rc);
            path.CloseFigure();
            //inner Large center
            path.SetMarkers();
            path.AddEllipse(rdim);
            path.CloseFigure();
            //inner small center
            path.SetMarkers();
            w = (int)((ICoreUnitPixel)c).Value;
            rdim = CoreMathOperation.GetBounds(center, w / 2);
            path.AddEllipse(rdim);
            path.CloseFigure();
            this.m_circlePath = path;           
        }
        public override void Dispose()
        {
            if (this.m_circleRegion != null)
            {
                this.m_circleRegion.Dispose();
                this.m_circleRegion = null;
            }
            if (this.m_circlePathMatrix != null)
            {
                this.m_circlePathMatrix.Dispose();
                this.m_circlePathMatrix = null;
            }
            base.Dispose();
        }
        public void RenderSelection(ICoreGraphics g, 
            ICore2DDrawingSurface surface)
        {
            //base.RenderSelection(g, surface);
            CoreUnit c = "15 mm";
            CoreUnit v_rh = "35 mm";
            int w = (int)((ICoreUnitPixel)v_rh).Value;
            int W = (int)this.Width;
            object s = g.Save();
            //g.ResetTransform();
            //Rectanglef v_rc = surface.GetScreenBound(this.Bounds);
            //Rectanglef v_rc2 = surface.GetScreenBound(CoreMathOperation.GetBounds(CoreMathOperation.GetCenter(this.Bounds),
            //    w / 2.0f));
            //Pen v_pen = CoreBrushRegister.GetPen<Pen>(Colorf.Black);
            //g.InterpolationMode = global::System.Drawing.Drawing2D.InterpolationMode.High;
            //g.SmoothingMode = global::System.Drawing.Drawing2D.SmoothingMode.None;
            //for (int i = 0; i < 360; )
            //{
            //    g.DrawArc(v_pen, v_rc, i, 5);
            //    g.DrawArc(v_pen, v_rc2, i, 5);
            //    i += 7;
            //}
            g.Restore(s);
        }
    }
}

