

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: XMLEditorTextArea.cs
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
using IGK.DrSStudio.Drawing2D.WinUI;
/*
IGKDEV @ 2008-2016
author: C.A.D . BONDJE DOUE
file:XMLEditorTextArea.cs
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
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
namespace IGK.DrSStudio.XMLEditorAddIn.WinUI
{
    using Configuration;
    using IGK.ICore.WinCore;
    using IGK.ICore.WinCore.WinUI.Controls;
    /// <summary>
    /// represent the xml control text area
    /// </summary>
    class XMLEditorTextArea : IGKXControl
    {        
        IXMLEditorSurface m_owner;
        public IXMLEditorDocument Document { get { return this.m_owner.Document; } }
        public IXMLEditorSetting EditorSetting { get { return XMLEditorSetting.Instance; } }
        public IXMLEditorSurface Surface { get { return this.m_owner; } }
        public override System.Windows.Forms.DockStyle Dock
        {
            get
            {
                return base.Dock;
            }
            set
            {
                base.Dock = value;
            }
        }
        internal XMLEditorTextArea(IXMLEditorSurface surface)
        {
            this.SetStyle(System.Windows.Forms.ControlStyles.ResizeRedraw, true);
            this.SetStyle(System.Windows.Forms.ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(System.Windows.Forms.ControlStyles.OptimizedDoubleBuffer, true);
            this.m_owner = surface;
            this.Dock = System.Windows.Forms.DockStyle.None;
            this.Surface.ScrollChanged += new EventHandler(Surface_ScrollChanged);
        }
        void Surface_ScrollChanged(object sender, EventArgs e)
        {
            this.Invalidate();
        }
        protected override bool IsInputChar(char charCode)
        {
            return base.IsInputChar(charCode);
        }
        protected override bool IsInputKey(System.Windows.Forms.Keys keyData)
        {
            return base.IsInputKey(keyData);
        }
        protected override void OnKeyDown(System.Windows.Forms.KeyEventArgs e)
        {
            if (Tools.ShortCutManager.Instance.CallAction(e.KeyData))
            {
                e.Handled = true;
            }
            base.OnKeyDown(e);
        }
        protected override void OnKeyPress(System.Windows.Forms.KeyPressEventArgs e)
        {
            base.OnKeyPress(e);
        }
        internal void SaveAs(string filename)
        {
            throw new NotImplementedException();
        }
        internal void Save()
        {
            throw new NotImplementedException();
        }
        protected override void OnPaint(System.Windows.Forms.PaintEventArgs e)
        {
            e.Graphics.Clear(Colorf.FromFloat (0.98f));
            int y = 0;
            int offsetx = this.Surface.ScrollX; // offset col
            int offsety = this.Surface.ScrollY; // offset line
            Rectangle v_rc = this.ClientRectangle;
            CoreFont ft = CoreFont.CreateFrom(EditorSetting.Font, null);
            Font v_ft = ft.ToGdiFont();
            int v_LineHeight = v_ft.Height;
 
            GraphicsState v_state = e.Graphics.Save();
            e.Graphics.TranslateTransform(-offsetx, 0);
            if (this.Document != null)
            {
                XMLRendereringEventArgs l= new XMLRendereringEventArgs(
                    e.Graphics, 
                    v_ft, 
                    v_LineHeight, 
                    EditorSetting.ShowTab ? EditorSetting.TabSpace : 0,
                    v_rc);
                this.Document.Draw(l);
                y = l.OffsetY;
            }
            else
            {
                e.Graphics.DrawString("Document is Empty",
                    this.Font,
                    WinCoreBrushRegister.GetBrush(Colorf.Black),
                    0, y);
                y += v_LineHeight;
            }
            while (y < v_rc.Height )
            {
                e.Graphics.DrawString("~",
                   v_ft,
                   WinCoreBrushRegister.GetBrush(EditorSetting.TiltColor),
                     0, y);
                y += v_LineHeight;
            }
            //render tilt
            e.Graphics.Restore(v_state);
            base.OnPaint(e);
        }
    }
}

