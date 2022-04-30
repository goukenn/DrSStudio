

using IGK.ICore.WinCore;
using IGK.ICore;
/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: XSplitterDragManager.cs
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
file:XSplitterDragManager.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using IGK.ICore.WinCore.WinUI.Controls;
namespace IGK.DrSStudio.WinUI
{
    public sealed class XSplitterDragManager
    {
        IGKXSplitter c_splitter;
        Control c_control;
        public XSplitterDragManager(IGKXSplitter splitter, Control parent)
        {
            this.c_splitter = splitter;
            this.c_control = parent;
            this.c_splitter.MouseDown +=  xSplitter1_MouseDown;
            this.c_splitter.MouseUp +=  xSplitter1_MouseUp;
            this.c_splitter.MouseMove +=  xSplitter1_MouseMove;
        }
        void xSplitter1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                m_startDrag += new Vector2i(e.X, e.Y);
                this.c_control.Width = m_startDrag.X;
                //this.xSplitter1.Bounds= new System.Drawing.Point(m_startDrag.X , 0);
            }
        }
        void xSplitter1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                m_startDrag += new Vector2i(e.X, e.Y);
                this.c_control.Width = m_startDrag.X;
            }
        }
        Vector2i m_startDrag;
        void xSplitter1_MouseDown(object sender, MouseEventArgs e)
        {
            //begin resize;
            if (e.Button == MouseButtons.Left)
            {
                //save current location
                m_startDrag = new Vector2i (
                    c_splitter.Location.X ,
                    c_splitter.Location.Y) ;
                c_splitter.Capture = true;
            }
        }
    }
}

