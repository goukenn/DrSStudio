

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: AndroidButton.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
﻿

using IGK.ICore;using IGK.ICore.Drawing2D;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IGK.DrSStudio.Android.WinUI;

namespace IGK.DrSStudio.Android
{
    [AndroidGroup("Button", typeof(Mecanism))]
    public class AndroidButton : AndroidLayoutItem 
    {
        StringElement m_stringElement;

        protected override void InitializeElement()
        {
            base.InitializeElement();
            this.m_stringElement = new StringElement();
            Rectanglef rc = new Rectanglef(0, 0, 100, 24);

            this.m_stringElement.SuspendLayout();
            this.m_stringElement.Bounds = rc;
            this.m_stringElement.Font.FontName = "consolas";
            this.m_stringElement.Font.FontSize = 8;
            this.m_stringElement.Text = "button";
            this.m_stringElement.Font.HorizontalAlignment = enuStringAlignment.Center;
            this.m_stringElement.Font.VerticalAlignment  = enuStringAlignment.Center;
            this.m_stringElement.ResumeLayout();
            this.Bounds = new Rectanglef(0, 0, 100, 24);
            this.FillBrush.SetSolidColor(Colorf.DarkGray);
            this.StrokeBrush.SetSolidColor(Colorf.Transparent);
        }
        protected override void InitGraphicPath(CoreGraphicsPath p)
        {
            base.InitGraphicPath(p);
            this.m_stringElement.Bounds = this.Bounds;
        }

        public override void Visit(ICore2DDrawingVisitor visitor)
        {
            if (visitor == null)
                return;
            object obj = visitor.Save();
            var g = this.GetPath();
            visitor.FillPath(this.FillBrush, g);
            visitor.DrawPath(this.StrokeBrush, g);
            if (this.m_stringElement.View && this.m_stringElement.Accept(visitor))
                this.m_stringElement.Visit(visitor);
            visitor.Restore(obj);
        }
        new class Mecanism : AndroidLayoutItem.Mecanism
        { 

        }
    }
}
