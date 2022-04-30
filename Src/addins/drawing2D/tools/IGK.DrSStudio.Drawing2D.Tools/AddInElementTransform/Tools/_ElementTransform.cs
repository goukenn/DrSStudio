

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: _ElementTransform.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/

using IGK.ICore.WinCore;

using IGK.ICore;
using IGK.ICore.Menu;
using IGK.ICore.WinUI;
using IGK.ICore.Drawing2D;
using IGK.ICore.Drawing2D.Menu;
using IGK.ICore.Drawing2D.WinUI;
using IGK.DrSStudio;
using IGK.DrSStudio.Menu;
using IGK.DrSStudio.WinUI;
using IGK.DrSStudio.Drawing2D.Menu;
using IGK.DrSStudio.Drawing2D.WinUI;
/*
IGKDEV @ 2008-2016
author: C.A.D . BONDJE DOUE
file:_ElementTransform.cs
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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace IGK.DrSStudio.ElementTransform.Tools
{
    using IGK.ICore.Tools;
    using IGK.DrSStudio.Drawing2D;
    using IGK.ICore.Drawing2D.Tools;
    using WinUI;
    [CoreTools("Tool.D2DElementTransform")]
    class _ElementTransform : Core2DDrawingToolBase 
    {
        private static _ElementTransform sm_instance;
        private _ElementTransform()
        {
        }
        public static _ElementTransform Instance
        {
            get
            {
                return sm_instance;
            }
        }
        static _ElementTransform()
        {
            sm_instance = new _ElementTransform();
        }
        protected override void GenerateHostedControl()
        {
            XElementTransformToolStrip v_ctr = new XElementTransformToolStrip(this);
            this.HostedControl = v_ctr;
        }
        internal void RotateRight(float Angle)
        {
            if ((Angle % 360) == 0) 
                return;
            ICore2DDrawingLayeredElement[] elements = CurrentSurface.CurrentLayer.SelectedElements.ToArray();
            Rectanglef docBound = this.CurrentSurface.CurrentDocument.Bounds;
            Vector2f v_center = Vector2f.Zero;
            for (int i = 0; i < elements.Length; i++)
            {
                v_center = CoreMathOperation.GetCenter(elements[i].GetBound());
                elements[i].Rotate(Angle, v_center, enuMatrixOrder.Append);
            }
            this.CurrentSurface.RefreshScene();
        }
        internal void RotateLeft(float Angle)
        {
            if ((Angle % 360) == 0)
                return;
            ICore2DDrawingLayeredElement[] elements = CurrentSurface.CurrentLayer.SelectedElements.ToArray();
            Rectanglef docBound = this.CurrentSurface.CurrentDocument.Bounds;
            Vector2f v_center = Vector2f.Zero ;
            for (int i = 0; i < elements.Length; i++)
            {
                v_center = CoreMathOperation.GetCenter (elements[i].GetBound ());
                elements[i].Rotate(-Angle, v_center, enuMatrixOrder.Append);
            }
            this.CurrentSurface.RefreshScene();
        }
    }
}

