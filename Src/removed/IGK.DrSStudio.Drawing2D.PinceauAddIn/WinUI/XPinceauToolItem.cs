

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: XPinceauToolItem.cs
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
file:XPinceauToolItem.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;
namespace IGK.DrSStudio.Drawing2D.WinUI
{
    /// <summary>
    /// represent a pinceau tool item
    /// </summary>
    class XPinceauToolItem : IGK.DrSStudio.WinUI.XControl
    {
        private ICore2DDrawingLayeredElement m_PinceauElement;
        public ICore2DDrawingLayeredElement PinceauElement
        {
            get { return m_PinceauElement; }            
        }
        protected override Size DefaultSize
        {
            get
            {
                return new Size(32, 32);
            }
        }
        public XPinceauToolItem(ICore2DDrawingLayeredElement element)
        {
            this.m_PinceauElement = element;
            this.SetStyle(ControlStyles.FixedHeight, true);
            this.SetStyle(ControlStyles.FixedWidth , true);
            this.Paint += new System.Windows.Forms.PaintEventHandler(_Paint);    
        }
        void _Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {
            e.Graphics.Clear(Color.White);
            this.m_PinceauElement.Draw(e.Graphics);
            ControlPaint.DrawBorder(e.Graphics,
                this.ClientRectangle,
                Color.Black,
                ButtonBorderStyle.Solid);
        }
    }
}

